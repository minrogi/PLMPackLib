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
    #region TreeInterface definition + Console implementation
    public interface TreeInterface
    {
        void Initialize();
        void Finish();
        object InsertTreeNode(object parentNode, TreeNode node);
        void InsertDummyNode(object parentNode);
        void AskRootNode();
    }

    public class TreeConsole : TreeInterface
    {
        public void Initialize()
        {
            Console.WriteLine("Initialize tree...");
        }

        public void Finish()
        {
            Console.WriteLine("Finalize tree...");
        }

        public object InsertTreeNode(object parentNode, TreeNode node)
        {
            string slineFrom = parentNode as string;
            Console.WriteLine(slineFrom + string.Format("Node : {0}({1})", node.Name, node.Description));
            string slineTo = slineFrom + "    ";
            return (object)(slineTo);
        }

        public void InsertDummyNode(object parentNode)
        {
            Console.WriteLine("_DUMMY_");
        }

        public void AskRootNode()
        { 
        }
    }
    #endregion

    #region TreeBuilder class
    public class TreeBuilder
    {
        #region Data member
        protected static readonly ILog _log = LogManager.GetLogger(typeof(TreeBuilder));
        #endregion

        #region Public tree building method
        public void BuildTree(TreeInterface treeImplementation)
        {
            // set up a simple configuration that logs on the console.
            XmlConfigurator.Configure();
            // initialize
            treeImplementation.Initialize();

            // check database file
            if (!System.IO.File.Exists(Pic.DAL.ApplicationConfiguration.CustomSection.DatabasePath))
                _log.Error(string.Format("Failed to load database with path {0}", Pic.DAL.ApplicationConfiguration.CustomSection.DatabasePath));
            else
                _log.Info(string.Format("Database found:{0}", Pic.DAL.ApplicationConfiguration.CustomSection.DatabasePath)); 

            // root nodes
            PPDataContext db = new PPDataContext();
            List<TreeNode> rootNodes = TreeNode.GetRootNodes(db);
            if (0 == rootNodes.Count)
            {
                treeImplementation.AskRootNode();
                rootNodes = TreeNode.GetRootNodes(db);
            }
            foreach (TreeNode node in rootNodes)
            {
                Object parentNodeObject = treeImplementation.InsertTreeNode(null, node);
                treeImplementation.InsertDummyNode(parentNodeObject);
            }
    
            db.Dispose();
            // finalize
            treeImplementation.Finish();
        }

        public void PopulateChildren(TreeInterface treeImplementation, object parentNode, int parentNodeID)
        {
            PPDataContext db = new PPDataContext();
            TreeNode parentNodeDB = TreeNode.GetById(db, parentNodeID);

            foreach (TreeNode node in parentNodeDB.Childrens(db))
            {
                if (node.IsDocument)
                   treeImplementation.InsertTreeNode(parentNode, node);
                else
                {
                    Object object1 = treeImplementation.InsertTreeNode(parentNode, node);
                    List<TreeNode> childNodes = node.Childrens(db);
                    // insert _DUMMY_ node
                    if (childNodes.Count > 0)
                        treeImplementation.InsertDummyNode(object1);
                }
            }
        }

        public void Populate(TreeInterface treeImplementation, int nodeId)
        {
            PPDataContext db = new PPDataContext();
            TreeNode tn = TreeNode.GetById(db, nodeId);
            
        }
        #endregion

        #region Helpers
        private void InsertChildNodes(PPDataContext db, TreeInterface treeImplementation, object parentNode, TreeNode node)
        {
            // node itself
            Object object1 = treeImplementation.InsertTreeNode(parentNode, node);
            // child nodes
            List<TreeNode> childNodes = node.Childrens(db);
            foreach (TreeNode tn in childNodes)
                InsertChildNodes(db, treeImplementation, object1, tn);
        }
        #endregion
    }
    #endregion
}
