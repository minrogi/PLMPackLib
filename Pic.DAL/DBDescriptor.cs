#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// IO
using System.IO;
// ACL
using System.Security.AccessControl;
using System.Security.Principal;
// SharpZipLib
using ICSharpCode.SharpZipLib.Zip;
// log4net
using log4net;
// settings
using Pic.DAL.Properties;
#endregion

namespace Pic.DAL
{
    internal class DBDescriptor
    {
        #region Constants
        public static int BUFFER_SIZE = 2048;
        #endregion

        #region Data members
        private string _dbFilePath;
        private string _docDirPath;
        // logging
        protected static readonly ILog _log = LogManager.GetLogger(typeof(DBDescriptor));
        #endregion

        #region Construction
        /// <summary>
        /// private constructor
        /// </summary>
        private DBDescriptor(string dbFilePath, string repositoryPath)
        {   _dbFilePath = dbFilePath; _docDirPath = repositoryPath; }
        /// <summary>
        /// get DBDescriptor for current database
        /// </summary>
        public static DBDescriptor Current
        {
            get
            {
                return new DBDescriptor(
                    Pic.DAL.ApplicationConfiguration.CustomSection.DatabasePath
                    , Pic.DAL.ApplicationConfiguration.CustomSection.RepositoryPath);
            }
        }
        /// <summary>
        /// get DBDecriptor for an existing database/document structure
        /// </summary>
        /// <returns>Existing</returns>
        public static DBDescriptor GetExisting(string dirPath)
        {
            return new DBDescriptor(
                Path.Combine(dirPath, "Database\\PicParam.db")
                , Path.Combine(dirPath, "Documents"));
        }
        /// <summary>
        /// create in temp directory with empty database file
        /// </summary>
        /// <returns>Ready to use descriptor</returns>
        public static DBDescriptor CreateTemp()
        {
            return DBDescriptor.CreateTemp(true);
        }
        /// <summary>
        /// create in temp directory with or without empty database file
        /// </summary>
        public static DBDescriptor CreateTemp(bool addEmptyDatabase)
        {
            // get temp directory
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            return DBDescriptor.Create(tempDirectory, addEmptyDatabase);
        }
        /// <summary>
        /// create under given location with or without empty database
        /// </summary>
        public static DBDescriptor Create(string dirPath, bool addEmptyDatabase)
        {
            Directory.CreateDirectory(dirPath);
            // create "Database" directory
            string dataDirPath = Path.Combine(dirPath, "Database");
            CreateDirectoryWSecurity(dataDirPath);
            // copy empty database file
            string dbFilePath = Path.Combine(dataDirPath, "PicParam.db");
            if (addEmptyDatabase)
                File.Copy(Settings.Default.EmptyDBFilePath, dbFilePath);
            // create "Documents" directory
            string docDirPath = Path.Combine(dirPath, "Documents");
            CreateDirectoryWSecurity(docDirPath);
            return new DBDescriptor(dbFilePath, docDirPath);        
        }
        #endregion

        #region Public properties
        public string DBFilePath { get { return _dbFilePath; } }
        public string RepositoryPath { get { return _docDirPath; } }
        public bool IsValid
        {
            get { return File.Exists(_dbFilePath) && Directory.Exists(_docDirPath); }
        }
        #endregion

        #region Public methods
        public bool Clear()
        { 
            // replace database file
            try
            {
                if (File.Exists(_dbFilePath))
                    File.Delete(_dbFilePath);
            }
            catch (Exception /*ex*/) { return false; }
            File.Copy(Settings.Default.EmptyDBFilePath, _dbFilePath);
            // delete files in repositoryPath
            foreach (string f in Directory.GetFiles(_docDirPath))
            {
                try { File.Delete(f); }
                catch (Exception /*ex*/) { }
            }
            return true;
        }
        #endregion

        #region Save as zip
        /// <summary>
        /// Archive database files as zip
        /// </summary>
        /// <returns>true when successfull, false when failed</returns>
        public bool Archive(string zipFilePath, IProcessingCallback callback)
        {
            // removing existing
            if (File.Exists(zipFilePath))
            {
                callback.Info(string.Format("Deleting existing file {0}", zipFilePath));
                try
                {   File.Delete(zipFilePath); }
                catch (Exception ex)
                {
                    callback.Error(string.Format("Failed to delete file {0} with error {1}", zipFilePath, ex.Message));
                    return false;
                }
            }
            // open file stream + zip out stream
            FileStream fileStreamOut = new FileStream(zipFilePath, FileMode.CreateNew, FileAccess.Write);
            bool result = true;
            using (ZipOutputStream zipOutStream = new ZipOutputStream(fileStreamOut))
            {
                // database file
                result = ArchiveFile(DBFilePath, zipOutStream, callback);
                if (result)
                {
                    // document directory
                    foreach (string filePath in Directory.GetFiles(_docDirPath))
                    {
                        if (!(result = ArchiveFile(filePath, zipOutStream, callback)))
                            break;
                    }
                }
            }
            // closing
            fileStreamOut.Close();
            return result;
        }
        /// <summary>
        /// Add zip entry to opened ZipOutStream
        /// </summary>
        /// <returns>true if successfull, false if failed</returns>
        private bool ArchiveFile(string filePath, ZipOutputStream zipOutStream, IProcessingCallback callback)
        {
            try
            {
                if (null != callback)
                    callback.Info(string.Format("Adding zip entry {0}...", Path.GetFileName(filePath)));
                using (FileStream fileStreamIn = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    int size = BUFFER_SIZE;
                    byte[] buffer = new byte[size];
                    ZipEntry entry = new ZipEntry(Path.GetFileName(filePath));
                    zipOutStream.PutNextEntry(entry);
                    do
                    {
                        size = fileStreamIn.Read(buffer, 0, buffer.Length);
                        zipOutStream.Write(buffer, 0, size);
                    }
                    while (size > 0);
                }
            }
            catch (Exception ex)
            {
                if (null != callback)
                    callback.Error(string.Format("Zipping file {0} failed with error {1}", filePath, ex.Message));
                return false;
            }
            return true; // success
        }
        #endregion

        #region Extract zip archive and return ready to use DBDescriptor
        /// <summary>
        /// Create at given location and fill with archive content
        /// </summary>
        public static DBDescriptor CreateFromArchive(string dirPath, string zipFilePath, IProcessingCallback callback)
        {
            DBDescriptor desc = DBDescriptor.Create(dirPath, false);
            FillFromArchive(desc, zipFilePath, callback);
            return desc;        
        }
        /// <summary>
        /// Create under new temp directory and fill with archive content 
        /// </summary>
        /// <returns>created DBDescriptor</returns>
        public static DBDescriptor CreateTempFromArchive(string zipFilePath, IProcessingCallback callback)
        {
            DBDescriptor desc = DBDescriptor.CreateTemp(false);
            FillFromArchive(desc, zipFilePath, callback);
            return desc;
        }
        private static void FillFromArchive(DBDescriptor desc, string zipFilePath, IProcessingCallback callback)
        {        
            FileStream fileStreamIn = new FileStream(zipFilePath, FileMode.Open, FileAccess.Read);
            using (ZipInputStream zipInStream = new ZipInputStream(fileStreamIn))
            {
                ZipEntry entry;
                while ((entry = zipInStream.GetNextEntry()) != null)
                {
                    bool isDB = string.Equals("PicParam.db", entry.Name, StringComparison.CurrentCultureIgnoreCase);

                    if (null != callback)
                        callback.Info(string.Format("Extracting zip entry {0}", entry.Name));

                    string destFilePath = isDB ? desc.DBFilePath : Path.Combine(desc.RepositoryPath, entry.Name);
                    if (!ExtractZipEntry(entry, zipInStream, destFilePath, callback))
                        break;
                }
            }
            fileStreamIn.Close();
        }

        private static bool ExtractZipEntry(ZipEntry entry, ZipInputStream zipInStream, string destFilePath, IProcessingCallback callback)
        {
            if (!entry.IsFile) return false;
            // prevent overwrite
            if (System.IO.File.Exists(destFilePath))
            {
                if (null != callback) callback.Info(string.Format("{0} already exists : Skipping...", entry.Name));
                return false;
            }
            // *** extract file : begin
            if (null != callback) callback.Info(string.Format("Extracting {0}...", entry.Name));
            // instantiate output stream
            FileStream fileStreamOut = new FileStream(destFilePath, FileMode.Create, FileAccess.Write);
            int size = BUFFER_SIZE;
            byte[] buffer = new byte[size];
            do
            {
                size = zipInStream.Read(buffer, 0, buffer.Length);
                fileStreamOut.Write(buffer, 0, size);
            } while (size > 0);
            fileStreamOut.Close();
            // *** extract file : end 
            return true;
        }
        #endregion

        #region ACL
        public static void CreateDirectoryWSecurity(string dirPath)
        {
            Directory.CreateDirectory(dirPath);
            try
            {
                AddDirectorySecurity(
                    dirPath
                    , FileSystemRights.Modify
                    , InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit
                    , PropagationFlags.None
                    , AccessControlType.Allow);
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }

        // Adds an ACL entry on the specified directory for the 'Users' account.
        public static void AddDirectorySecurity(string FileName, FileSystemRights Rights,
                                                InheritanceFlags Inheritance, PropagationFlags Propogation,
                                                AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object. 
            DirectoryInfo dInfo = new DirectoryInfo(FileName);
            // Get a DirectorySecurity object that represents the  
            // current security settings. 
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            // Add the FileSystemAccessRule to the security settings.
            SecurityIdentifier sid = new System.Security.Principal.SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
            dSecurity.AddAccessRule(new FileSystemAccessRule(sid,
                                                             Rights,
                                                             Inheritance,
                                                             Propogation,
                                                             ControlType));
            // Set the new access settings. 
            dInfo.SetAccessControl(dSecurity);
        }

        // Removes an ACL entry on the specified directory for the 'Users' account. 
        public static void RemoveDirectorySecurity(string FileName, FileSystemRights Rights,
                                                   AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object. 
            DirectoryInfo dInfo = new DirectoryInfo(FileName);
            // Get a DirectorySecurity object that represents the  
            // current security settings. 
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            // Add the FileSystemAccessRule to the security settings.
            SecurityIdentifier sid = new System.Security.Principal.SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
            dSecurity.RemoveAccessRule(new FileSystemAccessRule(sid,
                                                                Rights,
                                                                ControlType));
            // Set the new access settings. 
            dInfo.SetAccessControl(dSecurity);
        }
        #endregion
    }
}
