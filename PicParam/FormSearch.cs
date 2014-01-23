#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Pic.DAL;
#endregion

namespace PicParam
{
    public partial class FormSearch : Form
    {
        #region Constructor
        public FormSearch()
        {
            InitializeComponent();
        }
        #endregion

        #region Public properties
        public string SearchText
        {
            get { return tbSearch.Text; }
            set { tbSearch.Text = value; }
        }
        public bool SearchNameOnly
        {
            get { return chkSearchNameOnly.Checked; }
            set { chkSearchNameOnly.Checked = value; }
        }

        public int ResultNodeId
        {
            get { return _searchResult.TreeNodeId; }
        }
        #endregion

        #region Event handlers
        private void onTextChanged(object sender, EventArgs e)
        {
            // clear results in listbox
            lbResults.Items.Clear();
            // only search if more than 3 characters
            lbMinTextLength.Visible = SearchText.Length < 3;
            if (SearchText.Length < 3)
                return;
            // seach in database
            List<SearchResult> searchResults = Search.SearchTreeNodes(SearchText, SearchNameOnly);
            // show result dialog
            foreach (SearchResult result in searchResults)
                lbResults.Items.Add(result);
        }

        private void onItemSelected(object sender, EventArgs e)
        {
            _searchResult = lbResults.Items[lbResults.SelectedIndex] as Pic.DAL.SearchResult;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Data members
        private Pic.DAL.SearchResult _searchResult;
        #endregion
    }
}
