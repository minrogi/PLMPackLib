#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pic.DAL.SQLite;
#endregion

namespace Pic.DAL
{
    public class SearchResult
    {
        #region Constructor
        public SearchResult(int treeNodeId, string name, string description, bool isDoc)
        {
            _treeNodeId = treeNodeId;
            _name = name;
            _description = description;
            _isDoc = isDoc;
        }
        #endregion
        #region Public properties
        public bool IsValid
        {
            get { return !string.IsNullOrEmpty(_name); }
        }
        public int TreeNodeId
        {
            get { return _treeNodeId; }
        }
        public override string ToString()
        {
            string text = _isDoc ? "doc    :" : "branch :";
            text += _name;
            while (text.Length < 30) text += " ";
            text += "=> ";
            text += _description;

            return text;
        }
        #endregion
        #region Data members
        private string _name;
        private string _description;
        private int _treeNodeId;
        private bool _isDoc;
        #endregion
    }

    public class Search
    {
        #region Static methods
        public static List<SearchResult> SearchTreeNodes(string searchText, bool searchNamesOnly)
        {
            List<SearchResult> results = new List<SearchResult>();

            Pic.DAL.SQLite.PPDataContext db = new SQLite.PPDataContext();
            List<Pic.DAL.SQLite.TreeNode> treeNodes = Pic.DAL.SQLite.TreeNode.SearchTreeNodeNames(db, searchText, searchNamesOnly, true);
            foreach (Pic.DAL.SQLite.TreeNode tn in treeNodes)
                results.Add(new SearchResult(tn.ID, tn.Name, tn.Description, tn.IsDocument));
            return results;
        }
        #endregion
    }
}
