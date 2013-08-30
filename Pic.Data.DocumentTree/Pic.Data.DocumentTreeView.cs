#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Pic.Logging;
using System.Diagnostics;
#endregion

namespace Pic.Data
{
    public partial class DocumentTreeView : System.Windows.Forms.TreeView
    {
        #region Constructor
        public DocumentTreeView()
        {
            try
            {
                try
                {
                    // get image for tree
                    ImageList = new ImageList();
                    System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(GetType());
                    ImageList.Images.Add(new Bitmap(assembly.GetManifestResourceStream("Pic.Data.Resources.CLSDFOLD.BMP")));
                    ImageList.Images.Add(new Bitmap(assembly.GetManifestResourceStream("Pic.Data.Resources.OPENFOLD.BMP")));
                    ImageList.Images.Add(new Bitmap(assembly.GetManifestResourceStream("Pic.Data.Resources.DOC.BMP")));
                    // events
                    AfterSelect += new TreeViewEventHandler(DocumentTreeView_AfterSelect);
                    NodeMouseClick += new TreeNodeMouseClickEventHandler(DocumentTreeView_NodeMouseClick);

                    // construct tree
                    RefreshTree();
                }
                catch (Pic.Data.DataException ex)
                {
                    Debug.Fail(ex.ToString());
                    Logger.Write(ex.ToString(), Category.General, Priority.High);
                }
                catch (Exception ex)
                {
                    Debug.Fail(ex.ToString());
                    Logger.Write(ex.ToString(), Category.General, Priority.High);
                }
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        void DocumentTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            _tnNodeCurrent = e.Node;
        }
        #endregion

        #region Event handlers
        void DocumentTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                LoadChildrens(e.Node);
                DocTreeNode node = e.Node.Tag as DocTreeNode;
                if (null == node) return;
                if (node.IsBranch && null != BranchSelected)
                    BranchSelected(this, new DocumentTreeViewEventArgs(node));
                else if (node.IsLeaf && null != LeafSelected)
                    LeafSelected(this, new DocumentTreeViewEventArgs(node));
            }
            catch (Pic.Data.DataException ex)
            {
                Debug.Fail(ex.ToString());
                Logger.Write(ex.ToString(), Category.General, Priority.High);
            }
            catch (System.Exception ex)
            {
                Debug.Fail(ex.ToString());
                Logger.Write(ex.ToString(), Category.General, Priority.High);
            }
        }
        #endregion

        #region Tree filling methods
        public void RefreshTree()
        { 
            // turn off visual updating
            BeginUpdate();
            // clear tree
            Nodes.Clear();
            try
            {
                // insert root nodes
                List<DocTreeNode> rootNodes = DocTreeNode.getRootNodes();
                if (0 == rootNodes.Count)
                {
                    FormCreateBranch form = new FormCreateBranch();
                    if (DialogResult.OK == form.ShowDialog())
                    {
                        DocTreeNodeBranch.AddNewRootNode(form.BranchName, form.BranchDescription, form.BranchImage);
                        rootNodes = DocTreeNode.getRootNodes();
                    }
                    else
                        return;
                }

                foreach (DocTreeNode docNode in rootNodes)
                { 
                    TreeNode tnRoot = new TreeNode(docNode.Name, GetImageIndex(docNode), GetSelectedImageIndex(docNode) );
                    Nodes.Add(tnRoot);
                    if (docNode.IsBranch)
                    {
                        // add menu
                        tnRoot.ContextMenuStrip = GetBranchMenu();
                    }

                    tnRoot.Tag = docNode;
                    LoadChildrens(tnRoot);
                }
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
                Logger.Write(ex.ToString(), Category.General, Priority.High);
            }
            
            // turn on visual updating
            EndUpdate();
        }

        public void SelectNode(DocTreeNode docNode)
        {
            TreeNode node = FindByDocTreeNode(Nodes, docNode);
            SelectedNode = node;
            SelectedNode.EnsureVisible();
        }

        protected TreeNode FindByDocTreeNode(TreeNodeCollection nodeCollection, DocTreeNode docNode)
        {
            foreach (TreeNode node in nodeCollection)
            {
                DocTreeNode current = node.Tag as DocTreeNode;
                if (docNode == current)
                    return node;
            }
            TreeNode parentNode = FindByDocTreeNode(nodeCollection, docNode.Parent);
            if (null != parentNode)
            {
               LoadChildrens(parentNode);
               return FindByDocTreeNode(parentNode.Nodes, docNode);
            }
            return null;
        }

        protected void LoadChildrens(TreeNode tn)
        { 
            DocTreeNodeBranch docNodeBranch = tn.Tag as DocTreeNodeBranch;
            if (null == docNodeBranch)
                return;
            List<DocTreeNode> childNodes = docNodeBranch.childrens;
            foreach (DocTreeNode docNode in childNodes)
            {
                // check if node already inserted
                bool found = false;
                foreach (TreeNode tnChild in tn.Nodes)
                {
                    DocTreeNode docNodeChild = tnChild.Tag as DocTreeNode;
                    if (docNodeChild.Id == docNode.Id)
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                    continue;
                // insert new node
                TreeNode tnNew = new TreeNode(docNode.Name, GetImageIndex(docNode), GetSelectedImageIndex(docNode) );
                tn.Nodes.Add(tnNew);
                if (docNode.IsBranch)
                    tnNew.ContextMenuStrip = GetBranchMenu();
                else if (docNode.IsLeaf)
                    tnNew.ContextMenuStrip = GetLeafMenu();
                tnNew.Tag = docNode;
            }
        }
        protected int GetImageIndex(DocTreeNode docNode)
        {
            if (docNode.IsBranch) return 0;
            if (docNode.IsLeaf) return 2;
            else throw new Exception("Unexpected document node type");
        }

        protected int GetSelectedImageIndex(DocTreeNode docNode)
        {
            if (docNode.IsBranch) return 1;
            if (docNode.IsLeaf) return 2;
            else throw new Exception("Unexpected document node type");
        }
        #endregion

        #region Context menu creation
        protected ContextMenuStrip GetBranchMenu()
        {
            if (null == _branchMenu)
            {
                // create the ContextMenuStrip
                _branchMenu = new ContextMenuStrip();
                // create some menu items
                ToolStripMenuItem insertNewBranchLabel = new ToolStripMenuItem();
                insertNewBranchLabel.Text = "New branch...";
                insertNewBranchLabel.Click +=new EventHandler(insertNewBranchLabel_Click);
                ToolStripMenuItem insertNewDocumentLabel = new ToolStripMenuItem();
                insertNewDocumentLabel.Text = "New document...";
                insertNewDocumentLabel.Click += new EventHandler(insertNewDocumentLabel_Click);
                ToolStripMenuItem deleteLabel = new ToolStripMenuItem();
                deleteLabel.Text = "Delete";
                deleteLabel.Click += new EventHandler(deleteNode);
                // add the menu items to the menu.
                _branchMenu.Items.AddRange(
                    new ToolStripMenuItem[]{
                        insertNewBranchLabel
                        , insertNewDocumentLabel
                        , deleteLabel
                    }
                    );                
            }
            return _branchMenu;         
        }

        protected ContextMenuStrip GetLeafMenu()
        {
            if (null ==_leafMenu)
            {
                // create the ContextMenuStrip
                _leafMenu = new ContextMenuStrip();
                // create some menu items
                ToolStripMenuItem deleteLabel = new ToolStripMenuItem();
                deleteLabel.Text = "Delete";
                deleteLabel.Click += new EventHandler(deleteNode);
                // add the menu items to the menu.
                _leafMenu.Items.AddRange( new ToolStripMenuItem[]{ deleteLabel } );
            }
            return _leafMenu;
        }
        #endregion

        #region Context menu event handlers
        private TreeNode GetCurrentTreeNode()
        {
            return _tnNodeCurrent;
        }

        private void  deleteNode(object sender, EventArgs e)
        {
            try
            {
                // get current tree node
                TreeNode tnNode = GetCurrentTreeNode();
                // clear parent node childrens
                DocTreeNodeBranch parentBranch = tnNode.Parent.Tag as DocTreeNodeBranch;
                parentBranch.ClearChildrens();
                // remove DocumentTreeNode
                DocTreeNode docTreeNode = tnNode.Tag as DocTreeNode;
                docTreeNode.delete();
                // remove tree node
                tnNode.Remove();
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
                Logger.Write(ex.ToString(), Category.General, Priority.Highest);
            }
        }

        private void  insertNewDocumentLabel_Click(object sender, EventArgs e)
        {
            try
            {
                // get current tree node
                TreeNode tnNode = GetCurrentTreeNode();
                DocTreeNodeBranch docTreeNode = tnNode.Tag as DocTreeNodeBranch;
                if (null == docTreeNode) return;

                // show dialog
                FormCreateDocument dlg = new FormCreateDocument();
                if (DialogResult.OK == dlg.ShowDialog())
                { 
                    // create new document
                    Document doc = ParametricComponent.create(
                        dlg.DocumentName
                        , dlg.DocumentDescription
                        , dlg.FilePath
                        , dlg.ComponentGuid);
                    // create associated majorations
                    MajorationList.create(doc, dlg.Profile, dlg.Majorations);

                    // add child document node
                    docTreeNode.AddChildDocument(doc);

                    // Refresh tree
                    RefreshTree();
                    SelectedNode = GetCurrentTreeNode();
                }
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
                Logger.Write(ex.ToString(), Category.General, Priority.Highest);
            }
        }

        private void  insertNewBranchLabel_Click(object sender, EventArgs e)
        {
            try
            {
                // get current tree node
                TreeNode tnNode = GetCurrentTreeNode();
                DocTreeNodeBranch docTreeNode = tnNode.Tag as DocTreeNodeBranch;
                if (null == docTreeNode) return;

                // show dialog
                FormCreateBranch dlg = new FormCreateBranch();
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    // create new branch
                    docTreeNode.AddChildBranch( dlg.BranchName, dlg.BranchDescription, dlg.BranchImage);

                    // refresh tree
                    RefreshTree();
                    SelectedNode = GetCurrentTreeNode();
                }
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
                Logger.Write(ex.ToString(), Category.General, Priority.Highest);
            }
        }
        #endregion

        #region Delegates
        public delegate void BranchSelectHandler(object sender, DocumentTreeViewEventArgs e);
        public delegate void LeafSelectHandler(object sender, DocumentTreeViewEventArgs e);
        #endregion

        #region Events
        public event BranchSelectHandler BranchSelected;
        public event LeafSelectHandler LeafSelected;
        #endregion

        #region Data members
        private ContextMenuStrip _branchMenu;
        private ContextMenuStrip _leafMenu;
        private TreeNode _tnNodeCurrent;
        #endregion
    }

    #region Event argument class : DocumentTreeViewEventArgs
    public class DocumentTreeViewEventArgs : EventArgs
    {
        public DocumentTreeViewEventArgs(DocTreeNode docNode)
        {
            _docNode = docNode;
        }
        public DocTreeNode DocNode
        {
            get { return _docNode; }
        }
        // data members
        private DocTreeNode _docNode;
    }
    #endregion
}

