#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

using Pic.DAL.SQLite;

using log4net;
using System.Security.AccessControl;
using System.Security.Principal;
#endregion

namespace Pic.DAL
{
    /// <summary>
    /// Backup / Restore / Merge functions
    /// </summary>
    public class BackupRestore
    {
        #region Public static methods
        /// <summary>
        /// Creates a backup of the database
        /// </summary>
        public static bool BackupFull(string zipFilePath, IProcessingCallback callback)
        {
            try
            {
                // build "Root" node path
                List<string> nodePathRoot = new List<string>();
                nodePathRoot.Add("Root");
                // backup branch "Root"
                BackupBranch(nodePathRoot, zipFilePath, callback);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return false;
            }
            return true;
        }

        public static bool BackupBranch(List<string> nodePath, string zipFilePath, IProcessingCallback callback)
        {
            // remove zip path if it already exists
            if (System.IO.File.Exists(zipFilePath))
                System.IO.File.Delete(zipFilePath);
            // build destination database path
            DBDescriptor dbDescTo = DBDescriptor.CreateTemp();
            {
                // build data contexts
                PPDataContext dbFrom = new PPDataContext();
                using (PPDataContext dbTo = new PPDataContext(dbDescTo))
                {
                    // copy format table
                    CopyCardboardFormats(dbFrom, dbTo);
                    // copy cardboard profiles
                    CopyCardboardProfiles(dbFrom, dbTo);
                    // copy document types
                    CopyDocumentTypes(dbFrom, dbTo);
                    // copy branch nodes recursively
                    TreeNode nodeFrom = TreeNode.GetNodeByPath(dbFrom, null, nodePath, 0);
                    TreeNode nodeTo = TreeNode.GetNodeByPath(dbTo, null, nodePath, 0); ;
                    CopyTreeNodesRecursively(dbFrom, dbTo, nodeFrom, nodeTo, callback);
                }
                GC.Collect();
            }
            Thread.Sleep(1000);
            // archive temp database
            dbDescTo.Archive(zipFilePath, callback);
            return true;
        }

        /// <summary>
        /// restores a backup database : callback version
        /// </summary>
        public static bool Restore(string zipFilePath, IProcessingCallback callback)
        {
            try
            {
                // clear existing directories
                DBDescriptor dbDescTo = DBDescriptor.Current;
                if (!dbDescTo.Clear())
                {
                    if (null != callback)
                        callback.Error("Failed to clear current database!");
                    return false;
                }
                // extract new database
                DBDescriptor dbDescFrom = DBDescriptor.CreateTempFromArchive(zipFilePath, callback);
                // build data contexts
                PPDataContext dbFrom = new PPDataContext(dbDescFrom);
                PPDataContext dbTo = new PPDataContext(dbDescTo);
                // copy format table
                CopyCardboardFormats(dbFrom, dbTo);
                // copy cardboard profiles
                CopyCardboardProfiles(dbFrom, dbTo);
                // copy document types
                CopyDocumentTypes(dbFrom, dbTo);
                // copy branch nodes recursively
                TreeNode nodeFrom = TreeNode.GetRootNodes(dbFrom)[0];
                TreeNode nodeTo = TreeNode.GetRootNodes(dbTo)[0]; ;
                CopyTreeNodesRecursively(dbFrom, dbTo, nodeFrom, nodeTo, callback);
                GC.Collect();
            }
            catch (Exception ex)
            {
                if (null != callback)
                    callback.Error(ex.Message);
                _log.Error(ex.ToString());
                return false;
            }
            return true;
        }

        /// <summary>
        /// merge a database 
        /// </summary>
        public static void Merge(string zipFilePath, IProcessingCallback callback)
        {
            // check existence of zip archive
            if (!System.IO.File.Exists(zipFilePath))
                throw new FileNotFoundException(string.Format("File {0} not found", zipFilePath), zipFilePath);
            // extract zip archive to temp
            DBDescriptor dbDescFrom = DBDescriptor.CreateTempFromArchive(zipFilePath, callback);

            PPDataContext dbFrom = new PPDataContext(dbDescFrom);
            TreeNode nodeFrom = TreeNode.GetRootNodes(dbFrom)[0];

            PPDataContext dbTo = new PPDataContext();
            TreeNode nodeTo = TreeNode.GetRootNodes(dbTo)[0];

            // merge format table
            MergeCardboardFormats(dbFrom, dbTo, callback);
            // merge cardboard profiles
            MergeCardboardProfiles(dbFrom, dbTo, callback);
            // merge document types
            MergeDocumentTypes(dbFrom, dbTo, callback);

            MergeTreeNodesRecursively(dbFrom, dbTo, nodeFrom, nodeTo, callback);
        }

        public static void Overwrite(string zipFilePath, IProcessingCallback callback)
        {
            // check existence of zip archive
            if (!System.IO.File.Exists(zipFilePath))
                throw new FileNotFoundException(string.Format("File {0} not found", zipFilePath), zipFilePath);
            // extract zip archive to temp
            DBDescriptor dbDescFrom = DBDescriptor.CreateTempFromArchive(zipFilePath, callback);

            PPDataContext dbFrom = new PPDataContext(dbDescFrom);
            TreeNode nodeFrom = TreeNode.GetRootNodes(dbFrom)[0];

            PPDataContext dbTo = new PPDataContext();
            TreeNode nodeTo = TreeNode.GetRootNodes(dbTo)[0];

            // merge format table
            OverwriteCardboardFormats(dbFrom, dbTo, callback);
            // merge cardboard profiles
            OverwriteCardboardProfiles(dbFrom, dbTo, callback);
            // merge document types
            OverwriteDocumentTypes(dbFrom, dbTo, callback);

            // first clear existing documents
            ClearExistingDocumentsRecursively(dbFrom, nodeFrom, nodeTo, callback);
            // then merge
            using (PPDataContext dbTo1 = new PPDataContext())
            {
                MergeTreeNodesRecursively(dbFrom, dbTo, nodeFrom, nodeTo, callback);
            }
        }

        public static bool Clear(string destinationDirectory)
        {
            try
            {
                string databasePath = Path.Combine(destinationDirectory, "Database");
                if (Directory.Exists(databasePath))
                    Directory.Delete(databasePath, true /*recursive*/);
                string documentsPath = Path.Combine(destinationDirectory, "Documents");
                if (Directory.Exists(documentsPath))
                    Directory.Delete(documentsPath, true /*recursive*/);
            }
            catch (Exception ex)
            {
                _log.Debug(ex.ToString());
                return false;
            }
            return true;
        }

        public static void ClearDatabase(IProcessingCallback callback)
        {
            string databaseFile = ApplicationConfiguration.CustomSection.DatabasePath;

            // other files
            Pic.DAL.SQLite.PPDataContext db = new Pic.DAL.SQLite.PPDataContext();
            // get root node
            List<TreeNode> rootNodes = TreeNode.GetRootNodes(db);
            TreeNode rootNode = rootNodes[0];
            // delete all childs of root node
            foreach (Pic.DAL.SQLite.TreeNode tn in rootNode.Childrens(db))
                tn.Delete(db, true, callback);
            db.SubmitChanges();
        }
        #endregion

        #region Copy methods
        /// <summary>
        /// copy document types from dbFrom to dbTo
        /// </summary>
        public static void CopyDocumentTypes(PPDataContext dbFrom, PPDataContext dbTo)
        {
            foreach (DocumentType dt in dbFrom.DocumentTypes)
            { DocumentType.CreateNew(dbTo, dt.Name, dt.Description, dt.Application); }
        }
        public static void MergeDocumentTypes(PPDataContext dbFrom, PPDataContext dbTo, IProcessingCallback callback)
        {
            foreach (DocumentType dt in dbFrom.DocumentTypes)
            {
                if (DocumentType.HasByName(dbTo, dt.Name))
                { if (null != callback) callback.Info(string.Format("Document type {0} already exists. Skipping...", dt.Name)); }
                else
                {
                    if (null != callback) callback.Info(string.Format("Creating document type {0}...", dt.Name));
                    DocumentType.CreateNew(dbTo, dt.Name, dt.Description, dt.Application);
                }
            }
        }
        public static void OverwriteDocumentTypes(PPDataContext dbFrom, PPDataContext dbTo, IProcessingCallback callback)
        {
            foreach (DocumentType dt in dbFrom.DocumentTypes)
            {
                if (DocumentType.HasByName(dbTo, dt.Name))
                {
                    if (null != callback) callback.Info(string.Format("Updating document type {0} already exists...", dt.Name));
                    DocumentType docType = DocumentType.GetByName(dbTo, dt.Name);
                    docType.Description = dt.Description;
                    docType.Application = dt.Application;
                    dbTo.SubmitChanges();
                }
                else
                {
                    if (null != callback) callback.Info(string.Format("Creating document type {0}...", dt.Name));
                    DocumentType.CreateNew(dbTo, dt.Name, dt.Description, dt.Application);
                }
            }
        }
        /// <summary>
        /// copy cardboard profile from dbFrom to dbTo
        /// </summary>
        public static void CopyCardboardProfiles(PPDataContext dbFrom, PPDataContext dbTo)
        {
            foreach (CardboardProfile cp in dbFrom.CardboardProfiles)
            { CardboardProfile.CreateNew(dbTo, cp.Name, cp.Code, cp.Thickness); }
        }
        public static void MergeCardboardProfiles(PPDataContext dbFrom, PPDataContext dbTo, IProcessingCallback callback)
        {
            foreach (CardboardProfile cp in dbFrom.CardboardProfiles)
            {
                if (CardboardProfile.HasByName(dbTo, cp.Name))
                { if (null != callback) callback.Info(string.Format("Cardboard profile {0} already exists. Skipping...", cp.Name)); }
                else
                {
                    if (null != callback) callback.Info(string.Format("Creating carboard profile {0}...", cp.Name));
                    CardboardProfile.CreateNew(dbTo, cp.Name, cp.Code, cp.Thickness);
                }
            }
        }
        public static void OverwriteCardboardProfiles(PPDataContext dbFrom, PPDataContext dbTo, IProcessingCallback callback)
        {
            foreach (CardboardProfile cp in dbFrom.CardboardProfiles)
            {
                if (CardboardProfile.HasByName(dbTo, cp.Name))
                {
                    if (null != callback) callback.Info(string.Format("Cardboard profile {0} already exists. Skipping...", cp.Name));
                    CardboardProfile cardboardProf = CardboardProfile.GetByName(dbTo, cp.Name);
                    cardboardProf.Code = cp.Code;
                    cardboardProf.Thickness = cp.Thickness;
                    dbTo.SubmitChanges();
                }
                else
                {
                    if (null != callback) callback.Info(string.Format("Creating carboard profile {0}...", cp.Name));
                    CardboardProfile.CreateNew(dbTo, cp.Name, cp.Code, cp.Thickness);
                }
            }
        }
        /// <summary>
        /// copy cardboard formats from dbFrom to dbTo
        /// </summary>
        public static void CopyCardboardFormats(PPDataContext dbFrom, PPDataContext dbTo)
        {
            foreach (CardboardFormat cf in dbFrom.CardboardFormats)
            { CardboardFormat.CreateNew(dbTo, cf.Name, cf.Description, cf.Length, cf.Width); }
        }
        public static void MergeCardboardFormats(PPDataContext dbFrom, PPDataContext dbTo, IProcessingCallback callback)
        {
            foreach (CardboardFormat cf in dbFrom.CardboardFormats)
            {
                if (CardboardFormat.HasByName(dbTo, cf.Name))
                { if (null != callback) callback.Info(string.Format("Cardboard format {0} already exists. Skipping...", cf.Name)); }
                else
                {
                    if (null != callback) callback.Info(string.Format("Creating carboard format {0}...", cf.Name));
                    CardboardFormat.CreateNew(dbTo, cf.Name, cf.Description, cf.Length, cf.Width);
                }
            }
        }
        public static void OverwriteCardboardFormats(PPDataContext dbFrom, PPDataContext dbTo, IProcessingCallback callback)
        {
            foreach (CardboardFormat cf in dbFrom.CardboardFormats)
            {
                if (CardboardFormat.HasByName(dbTo, cf.Name))
                {
                    if (null != callback) callback.Info(string.Format("Cardboard format {0} already exists. Skipping...", cf.Name));
                    CardboardFormat cardboardFormat = CardboardFormat.GetByName(dbTo, cf.Name);

                }
                else
                {
                    if (null != callback) callback.Info(string.Format("Creating carboard format {0}...", cf.Name));
                    CardboardFormat.CreateNew(dbTo, cf.Name, cf.Description, cf.Length, cf.Width);
                }
            }
        }
        // TreeNodes
        public static void CopyTreeNodesRecursively(PPDataContext dbFrom, PPDataContext dbTo, TreeNode nodeFrom, TreeNode nodeTo, IProcessingCallback callback)
        {
            if (null != callback && !nodeFrom.IsDocument)
                callback.Info(string.Format("Processing branch {0}", nodeFrom.Name));
            // get thumbnail path of node to insert
            string thumbnailPath = nodeFrom.Thumbnail.File.Path(dbFrom);
            if (null == nodeTo)
            {
                // get root node
                TreeNode rootNodeTo = TreeNode.GetRootNodes(dbTo)[0];
                // insert as child node
                nodeTo = rootNodeTo.CreateChild(dbTo, nodeFrom.Name, nodeFrom.Description, thumbnailPath);
            }

            // handle childrens
            foreach (TreeNode childFrom in nodeFrom.Childrens(dbFrom))
            {
                // get thumbnail of node to insert
                thumbnailPath = childFrom.Thumbnail.File.Path(dbFrom);

                if (childFrom.IsDocument)
                {
                    Document docFrom = childFrom.Documents(dbFrom)[0];
                    string docTypeName = docFrom.DocumentType.Name;

                    if (string.Equals("Parametric component", docTypeName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (null != callback)
                            callback.Info(string.Format("Inserting component {0}...", childFrom.Name));
                        // insert as component
                        Component compFrom = docFrom.Components[0];
                        Component compTo = nodeTo.InsertComponent(dbTo, docFrom.File.Path(dbFrom), compFrom.Guid, childFrom.Name, childFrom.Description, thumbnailPath);
                        // parameter default values
                        Dictionary<string, double> dictNameValues = compFrom.GetParamDefaultValues();
                        if (dictNameValues.Count > 0)
                        {
                            if (null != callback)
                            {
                                string sParameters = string.Empty;
                                foreach (string defParamName in dictNameValues.Keys)
                                {
                                    StringBuilder sb = new StringBuilder();
                                    sb.Append(defParamName);
                                    sb.Append("=");
                                    sb.Append(dictNameValues[defParamName]);
                                    sb.Append(", ");
                                    sParameters += sb.ToString();
                                }
                                sParameters.Trim();
                                sParameters.Trim(',');
                                callback.Info(string.Format("Default parameter values : {0}", sParameters));
                            }
                            compTo.InsertNewParamDefaultValues(dbTo, dictNameValues);
                        }
                        // majorations
                        foreach (MajorationSet mjset in compFrom.MajorationSets)
                        {
                            // retrieve profile
                            string profileName = mjset.CardboardProfile.Name;
                            CardboardProfile profileTo = CardboardProfile.GetByName(dbTo, profileName);
                            if (null == profileTo)
                            {
                                if (null != callback) callback.Error(string.Format("Failed to retrieve profile {0}", mjset.CardboardProfile.Name));
                                continue;
                            }
                            // get majorations
                            Dictionary<string, double> majorations = new Dictionary<string, double>();
                            string sMajo = string.Format("prof = {0} -> ", profileName);
                            foreach (Majoration mj in mjset.Majorations)
                            {
                                majorations.Add(mj.Name, mj.Value);
                                sMajo += string.Format("{0}={1}, ", mj.Name, mj.Value);
                            }
                            // insert
                            if (null != callback) callback.Info(sMajo);
                            compTo.InsertNewMajorationSet(dbTo, profileTo.Name, majorations);
                        }
                    }
                    else // normal document
                    {
                        if (null != callback)
                            callback.Info(string.Format("Inserting document {0}...", childFrom.Name));
                        // insert as document
                        nodeTo.InsertDocument(dbTo, docFrom.File.Path(dbFrom), childFrom.Name, childFrom.Description, docTypeName, thumbnailPath);
                    }
                }
                else
                {
                    TreeNode childTo = nodeTo.CreateChild(dbTo, childFrom.Name, childFrom.Description, childFrom.Thumbnail.File.Path(dbFrom));
                    CopyTreeNodesRecursively(dbFrom, dbTo, childFrom, childTo, callback);
                }
            }
        }

        public static void MergeTreeNodesRecursively(PPDataContext dbFrom, PPDataContext dbTo, TreeNode nodeFrom, TreeNode nodeTo, IProcessingCallback callback)
        {
            if (null != callback && !nodeFrom.IsDocument)
                callback.Info(string.Format("Processing branch {0}", nodeFrom.Name));

            // get thumbnail path of node to insert
            string thumbnailPath = nodeFrom.Thumbnail.File.Path(dbFrom);

            // handle childrens
            foreach (TreeNode childFrom in nodeFrom.Childrens(dbFrom))
            {
                // get thumbnail of node to insert
                thumbnailPath = childFrom.Thumbnail.File.Path(dbFrom);

                if (childFrom.IsDocument)
                {
                    Document docFrom = childFrom.Documents(dbFrom)[0];
                    string docTypeName = docFrom.DocumentType.Name;

                    if (nodeTo.HasChild(dbTo, childFrom.Name))
                    { if (null != callback) callback.Info(string.Format("Document {0} already exists...", childFrom.Name)); }
                    else
                    {
                        if (string.Equals("Parametric component", docTypeName, StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (null != callback) callback.Info(string.Format("Parametric component {0} already exists...", childFrom.Name));
                            // insert as component
                            Component compFrom = docFrom.Components[0];
                            Component compTo = Component.GetByGuid(dbTo, compFrom.Guid);
                            if (null == compTo)
                            {
                                if (null != callback) callback.Info(string.Format("Inserting component {0}...", childFrom.Name));
                                compTo = nodeTo.InsertComponent(dbTo, docFrom.File.Path(dbFrom), compFrom.Guid, childFrom.Name, childFrom.Description, thumbnailPath);

                                // parameter default values
                                Dictionary<string, double> dictNameValues = compFrom.GetParamDefaultValues();
                                if (dictNameValues.Count > 0)
                                {
                                    if (null != callback)
                                    {
                                        string sParameters = string.Empty;
                                        foreach (string defParamName in dictNameValues.Keys)
                                        {
                                            StringBuilder sb = new StringBuilder();
                                            sb.Append(defParamName);
                                            sb.Append("=");
                                            sb.Append(dictNameValues[defParamName]);
                                            sb.Append(", ");
                                            sParameters += sb.ToString();
                                        }
                                        sParameters.Trim();
                                        sParameters.Trim(',');
                                        callback.Info(string.Format("Default parameter values : {0}", sParameters));
                                    }
                                    compTo.InsertNewParamDefaultValues(dbTo, dictNameValues);
                                }
                                // majorations
                                foreach (MajorationSet mjset in compFrom.MajorationSets)
                                {
                                    // retrieve profile
                                    string profileName = mjset.CardboardProfile.Name;
                                    CardboardProfile profileTo = CardboardProfile.GetByName(dbTo, profileName);
                                    if (null == profileTo)
                                    {
                                        if (null != callback) callback.Error(string.Format("Failed to retrieve profile {0}", mjset.CardboardProfile.Name));
                                        continue;
                                    }
                                    // get majorations
                                    Dictionary<string, double> majorations = new Dictionary<string, double>();
                                    string sMajo = string.Format("prof = {0} -> ", profileName);
                                    foreach (Majoration mj in mjset.Majorations)
                                    {
                                        majorations.Add(mj.Name, mj.Value);
                                        sMajo += string.Format("{0}={1}, ", mj.Name, mj.Value);
                                    }
                                    // insert
                                    if (null != callback) callback.Info(sMajo);
                                    compTo.InsertNewMajorationSet(dbTo, profileTo.Name, majorations);
                                }
                            }
                            else
                            { if (null != callback) callback.Info(string.Format("Component with GUID {0} already exists...", compFrom.Guid)); }
                        }
                        else
                        {
                            if (null != callback) callback.Info(string.Format("Inserting document {0}...", childFrom.Name));
                            // insert as document
                            nodeTo.InsertDocument(dbTo, docFrom.File.Path(dbFrom), childFrom.Name, childFrom.Description, docTypeName, thumbnailPath);
                        }
                    }
                }
                else
                {
                    TreeNode childTo = null;
                    if (nodeTo.HasChild(dbTo, childFrom.Name))
                    {
                        if (null != callback) callback.Info(string.Format("Branch {0} already exists.Skipping...", childFrom.Name));
                        childTo = nodeTo.GetChild(dbTo, childFrom.Name);
                    }
                    else
                    {
                        if (null != callback) callback.Info(string.Format("Inserting branch {0}...", childFrom.Name));
                        childTo = nodeTo.CreateChild(dbTo, childFrom.Name, childFrom.Description, thumbnailPath);
                    }
                    MergeTreeNodesRecursively(dbFrom, dbTo, childFrom, childTo, callback);
                }
            }
        }

        public static void ClearExistingDocumentsRecursively(PPDataContext dbFrom, TreeNode nodeFrom, TreeNode nodeTo, IProcessingCallback callback)
        {
            if (null != callback && !nodeFrom.IsDocument)
                callback.Info(string.Format("Processing branch {0}", nodeFrom.Name));

            // get thumbnail path of node to insert
            string thumbnailPath = nodeFrom.Thumbnail.File.Path(dbFrom);

            // handle childrens
            foreach (TreeNode childFrom in nodeFrom.Childrens(dbFrom))
            {
                // get thumbnail of node to insert
                thumbnailPath = childFrom.Thumbnail.File.Path(dbFrom);

                if (childFrom.IsDocument)
                {
                    Document docFrom = childFrom.Documents(dbFrom)[0];
                    string docTypeName = docFrom.DocumentType.Name;

                    // delete existing document
                    // will be using new data context each time a tree node is deleted
                    // in order to avoid exceptions claiming that there is a foreign key violation
                    using (PPDataContext dbTo0 = new PPDataContext())
                    {
                        if (nodeTo.HasChild(dbTo0, childFrom.Name))
                        {
                            string documentName = childFrom.Name;
                            TreeNode childTo = nodeTo.GetChild(dbTo0, documentName);
                            if (null != childTo && childTo.IsDocument)
                            {
                                try
                                {
                                    if (null != callback) callback.Info(string.Format("Deleting tree node {0} ...", childTo.Name));
                                    childTo.Delete(dbTo0, true, callback);
                                    dbTo0.SubmitChanges();
                                }
                                catch (Exception ex)
                                {
                                    callback.Error(string.Format("Deleting document {0} failed with exception {1}", documentName, ex.Message));
                                }
                            }
                        }
                    }
                }
                else // childFrom.IsDocument
                {
                    using (PPDataContext dbTo2 = new PPDataContext())
                    {
                        TreeNode childTo = null;
                        if (nodeTo.HasChild(dbTo2, childFrom.Name))
                        {
                            if (null != callback) callback.Info(string.Format("Branch {0} already exists.Skipping...", childFrom.Name));
                            childTo = nodeTo.GetChild(dbTo2, childFrom.Name);
                        }
                        else
                        {
                            if (null != callback) callback.Info(string.Format("Inserting branch {0}...", childFrom.Name));
                            childTo = nodeTo.CreateChild(dbTo2, childFrom.Name, childFrom.Description, thumbnailPath);
                        }
                        ClearExistingDocumentsRecursively(dbFrom, childFrom, childTo, callback);
                    }
                }
            }
        }
        #endregion

        #region Constants
        public static int BUFFER_SIZE = 2048;
        #endregion

        #region Data members
        protected static readonly ILog _log = LogManager.GetLogger(typeof(BackupRestore));
        #endregion
    }
}
