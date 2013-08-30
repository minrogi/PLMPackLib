#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;
using log4net.Config;
#endregion

namespace Pic.DAL.SQLite
{
    public class Test
    {
        protected static readonly ILog _log = LogManager.GetLogger(typeof(Test));

        static void Main(string[] args)
        {
            // set up a simple configuration that logs on the console.
            XmlConfigurator.Configure();

            bool deleteAll = true;
            try
            {
                // ================================ CardboardFormat
                try
                {
                    PPDataContext db = new PPDataContext();
                    CardboardFormat cbf1 = CardboardFormat.CreateNew(db, "F1", "Format 1", 1000.0f, 1000.0f);
                    _log.Info(string.Format("Created new cardboard format with Id = {0}", cbf1.ID));
                    CardboardFormat cbf2 = CardboardFormat.CreateNew(db, "F2", "Format 2", 2000.0f, 2000.0f);
                    _log.Info(string.Format("Created new cardboard format with Id = {0}", cbf2.ID));
                    CardboardFormat cbf3 = CardboardFormat.CreateNew(db, "F3", "Format 3", 3000.0f, 3000.0f);
                    _log.Info(string.Format("Created new cardboard format with Id = {0}", cbf3.ID));

                    foreach (CardboardFormat cbf in CardboardFormat.GetAll(db))
                        _log.Info(cbf.ToString());
                }
                catch (System.Exception ex) { _log.Error(ex.Message); }
                // ================================
                // ================================ CardboardProfile
                // get list of existing cardboards
                try
                {
                    PPDataContext db = new PPDataContext();
                    IEnumerable<CardboardProfile> profiles = from cp in db.CardboardProfiles select cp;
                    foreach (CardboardProfile cbp in profiles)
                        _log.Info(cbp.ToString());
                    db.Dispose();
                }
                catch (System.Exception ex) { _log.Error(ex.Message); }
                // add first CardboardProfile ->expected to throw exception if already exists
                try
                {
                    PPDataContext db = new PPDataContext();
                    CardboardProfile cbprofile1 = CardboardProfile.CreateNew(db, "Profile1", "P1", 0.1f);
                    _log.Info(string.Format("created cardboard profile with Id = {0}", cbprofile1.ID));
                    db.Dispose();
                }
                catch (ExceptionDAL ex) { _log.Debug(ex.Message); }
                catch (Exception ex) { _log.Error(ex.Message); }
                // add second CardboardProfile
                try
                {
                    PPDataContext db = new PPDataContext();
                    CardboardProfile cbprofile2 = CardboardProfile.CreateNew(db, "Profile2", "P2", 0.2f);
                    _log.Info(string.Format("created cardboard profile with Id = {0}", cbprofile2.ID));
                    db.Dispose();
                }
                catch (ExceptionDAL ex) { _log.Debug(ex.Message); }
                catch (Exception ex) { _log.Error(ex.Message); }
                // delete CardboardProfile
                {
                    PPDataContext db = new PPDataContext();
                    CardboardProfile profile = db.CardboardProfiles.Single<CardboardProfile>(cp => cp.Code == "P2");
                    db.CardboardProfiles.DeleteOnSubmit(profile);
                    db.SubmitChanges();
                }
                // ================================ 
                // ================================ DocumentType
                // add new DocumentType
                try
                {
                    PPDataContext db = new PPDataContext();
                    DocumentType docType = DocumentType.CreateNew(db, "Parametric component", "Parametric component to be used in PicParam", "PicParam");
                    _log.Info(string.Format("created document type with Id = {0}", docType.ID));
                    db.Dispose();
                }
                catch (ExceptionDAL ex) { _log.Debug(ex.Message); }
                catch (Exception ex) { _log.Error(ex.Message); }

                // ================================
                // ================================ TreeNode
                // create new rootNodes
                string imageFilePath = string.Empty;
                try
                {
                    PPDataContext db = new PPDataContext();
                    TreeNode rootNode1 = TreeNode.CreateNew(db, "Parametric components", "Parametric component to be used in PicParam", imageFilePath);
                    db.Dispose();
                }
                catch (ExceptionDAL ex) { _log.Debug(ex.Message); }
                catch (Exception ex) { _log.Error(ex.Message); }

                try
                {
                    PPDataContext db = new PPDataContext();
                    TreeNode rootNode2 = TreeNode.CreateNew(db, "Drawing files", "Either PicGEOM or Autocad dxf files", imageFilePath);
                }
                catch (ExceptionDAL ex) { _log.Debug(ex.Message); }
                catch (Exception ex) { _log.Error(ex.Message); }

                // create new child treenode
                try
                {
                    PPDataContext db = new PPDataContext();
                    TreeNode parentNode = TreeNode.GetRootNodes(db)[0];
                    TreeNode childNode1 = parentNode.CreateChild(db, "Node1", "Node1", imageFilePath);
                    db.Dispose();
                }
                catch (ExceptionDAL ex) { _log.Debug(ex.Message); }
                catch (Exception ex) { _log.Error(ex.Message); }

                Guid guidComponent = Guid.Empty;

                // ================================
                // ================================ Component
                // create new child node + component
                try
                {
                    PPDataContext db = new PPDataContext();
                    TreeNode parentNode = TreeNode.GetRootNodes(db)[0];
                    TreeNode childNode1 = parentNode.Childrens(db)[0];
                    TreeNode childNode2 = null;
                    try
                    {
                        childNode2 = childNode1.CreateChild(db, "Node2", "Node2", imageFilePath);
                        _log.Info(string.Format("successfully created Node with ID = {0}", childNode2.ID));
                    }
                    catch (ExceptionDAL ex) { _log.Debug(ex.Message); }
                    if (null == childNode2)
                        childNode2 = childNode1.Childrens(db)[0];
                    // insert component
                    string filePath = @"K:\Codesion\PicSharp\Pic.Plugin.SimpleRectangle\bin\Release\Pic.Plugin.SimpleRectangle.dll";
                    Component component = childNode2.InsertComponent(db, filePath, Guid.NewGuid(), "Rounded rectangle", "Rounded rectangle", "");
                    guidComponent = component.Guid;
                    // set majorations
                    Dictionary<string, double> majorations = new Dictionary<string, double>();
                    majorations.Add("m1", 1.0);
                    majorations.Add("m2", 2.0);
                    component.InsertNewMajorationSet(db, "Profile1", majorations);
                    db.Dispose();
                }
                catch (ExceptionDAL ex) { _log.Debug(ex.Message); }
                catch (Exception ex) { _log.Error(ex.Message); }
                // ================================
                // ================================ Document
                // create documents
                try
                {
                    PPDataContext db = new PPDataContext();
                    TreeNode parentNode = TreeNode.GetRootNodes(db)[0];
                    string filePath = @"K:\SVN Projects\PicSharp\Pic.Plugin.SimpleRectangle\bin\Release\Pic.Plugin.SimpleRectangle.dll";
                    parentNode.InsertDocument(db, filePath, "Rounded rectangle", "Rounded Rectangle", "Parametric component", "");
                    db.Dispose();
                }
                catch (ExceptionDAL ex) { _log.Debug(ex.Message); }
                catch (Exception ex) { _log.Error(ex.Message); }

                // ================================
                // ================================ TreeBuilder
                // show document tree
                TreeBuilder builder = new TreeBuilder();
                TreeConsole treeConsole = new TreeConsole();
                builder.BuildTree(treeConsole);
                // ================================
                // ================================
                try
                {
                    // retrieve component
                    PPDataContext db = new PPDataContext();
                    Component component = Component.GetByGuid(db, guidComponent);
                    if (null == component)
                        throw new ExceptionDAL(string.Format("No component with Guid = {0}", guidComponent));

                    CardboardProfile profile3 = CardboardProfile.GetByName(db, "P3");
                    if (null == profile3)
                        profile3 = CardboardProfile.CreateNew(db, "P3", "P3", 3.0f);
                    Dictionary<string, double> defaultMajorations = Component.GetDefaultMajorations(db, component.ID, profile3);
                    component.InsertNewMajorationSet(db, profile3.Name, defaultMajorations);
                    defaultMajorations["m1"] = 100.0;
                    defaultMajorations["m2"] = 100.0;
                    component.UpdateMajorationSet(db, profile3, defaultMajorations);
                    db.Dispose();
                }
                catch (ExceptionDAL ex) { _log.Debug(ex.Message); }
                catch (Exception ex) { _log.Error(ex.Message); }
                // ================================
                // ================================ TreeBuilder
                // show document tree
                builder.BuildTree(treeConsole);
                // ================================
                // ================================ Delete components / documents
                try
                {
                    if (deleteAll)
                    {
                        PPDataContext db = new PPDataContext();
                        Component.DeleteAll(db);
                        _log.Info("Successfully deleted Components...");
                        Document.DeleteAll(db);
                        _log.Info("Successfully deleted Documents...");
                        DocumentType.DeleteAll(db);
                        _log.Info("Successfully deleted DocumentTypes...");
                        TreeNode.DeleteAll(db);
                        _log.Info("Successfully deleted TreeNodes...");
                        CardboardProfile.DeleteAll(db);
                        _log.Info("Successfully deleted CardboardProfile...");
                        CardboardFormat.DeleteAll(db);
                        _log.Info("Successfully deleted Cardboard formats...");
                        db.Dispose();
                    }
                }
                catch (ExceptionDAL ex) { _log.Debug(ex.Message); }
                catch (System.Exception ex) { _log.Error(ex.Message); }
                // ================================
                // ================================ TreeBuilder
                // show document tree
                builder.BuildTree(treeConsole);
                // ================================
            }
            catch (System.Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
    }
}
