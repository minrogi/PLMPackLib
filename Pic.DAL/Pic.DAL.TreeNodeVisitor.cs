#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace Pic.DAL.SQLite
{
    #region TreeNodeVisitor
    /// <summary>
    /// Abstract class to be inherited by class which 
    /// </summary>
    public abstract class TreeNodeVisitor
    {
        // initialization
        public virtual void Initialize(Pic.DAL.SQLite.PPDataContext db)   {}
		// process methods to be implemented
		public abstract bool Process(Pic.DAL.SQLite.PPDataContext db, TreeNode tn);
		// finish
        public virtual void Finalize(Pic.DAL.SQLite.PPDataContext db) { }
    }
    #endregion

    #region TreeProcessor
    /// <summary>
    /// TreeNode visitor processor
    /// </summary>
    public class TreeProcessor : IDisposable
    {
        #region Process visitor
        public void ProcessVisitor(TreeNodeVisitor visitor)
        {
            // get data context
            PPDataContext db = new PPDataContext();
            // initialize
            visitor.Initialize(db);
            // get root node
            List<TreeNode> rootNodes = TreeNode.GetRootNodes(db);
            foreach (TreeNode tn in rootNodes)
                if (!tn.ProcessVisitor(db, visitor))
                    break;
            // finilize
            visitor.Finalize(db);
        }
        #endregion

        #region IDisposable implementation
        public void Dispose()
        {
        }
        #endregion
    }
    #endregion
}
