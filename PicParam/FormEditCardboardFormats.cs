#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using log4net;

using Pic.DAL.SQLite;
#endregion

namespace PicParam
{
    public partial class FormEditCardboardFormats : Form
    {
        #region Data members
        protected static readonly ILog _log = LogManager.GetLogger(typeof(FormEditCardboardFormats));
        #endregion

        #region Constructor
        public FormEditCardboardFormats()
        {
            InitializeComponent();
            // disable delete button
            btDelete.Enabled = false;
            // fill list view with existing cardboard formats
            FillListView();
            // select first item
            if (listViewCardboardFormats.Items.Count > 0)
                listViewCardboardFormats.Items[0].Selected = true;
        }
        #endregion

        #region FillListView with formats
        private void FillListView()
        {
            try
            {
                // clear all existing items
                listViewCardboardFormats.Items.Clear();

                Pic.DAL.SQLite.PPDataContext db = new Pic.DAL.SQLite.PPDataContext();
                Pic.DAL.SQLite.CardboardFormat[] cardboardFormats = Pic.DAL.SQLite.CardboardFormat.GetAll(db);
                foreach (Pic.DAL.SQLite.CardboardFormat format in cardboardFormats)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = format.Name;
                    item.SubItems.Add(format.Description);
                    item.SubItems.Add(string.Format("{0}", format.Length));
                    item.SubItems.Add(string.Format("{0}", format.Width));
                    listViewCardboardFormats.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
                _log.Debug(ex.ToString());
            }
        }
        #endregion

        #region Handlers
        private void listViewCardboardFormats_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.btDelete.Enabled = (this.listViewCardboardFormats.SelectedIndices.Count > 0);
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
                _log.Debug(ex.ToString());
            }
        }

        private void btCreate_Click(object sender, EventArgs e)
        {
            try
            {
                FormCreateCardboardFormat form = new FormCreateCardboardFormat();
                if (DialogResult.OK == form.ShowDialog())
                {
                    Pic.DAL.SQLite.PPDataContext db = new Pic.DAL.SQLite.PPDataContext();
                    Pic.DAL.SQLite.CardboardFormat.CreateNew(db, form.FormatName, form.Description, form.FormatWidth, form.FormatHeight);
                    FillListView();
                }
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
                _log.Debug(ex.ToString());
            }
        }

        private void bnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewCardboardFormats.SelectedIndices.Count > 0)
                {
                    Pic.DAL.SQLite.PPDataContext db = new Pic.DAL.SQLite.PPDataContext();
                    // delete selected cardboard profile
                    int iSel = this.listViewCardboardFormats.SelectedIndices[0];
                    Pic.DAL.SQLite.CardboardFormat[] formats = Pic.DAL.SQLite.CardboardFormat.GetAll(db);
                    formats[iSel].Delete(db);
                    // fill list view again
                    FillListView();
                    // select first item
                    if (listViewCardboardFormats.Items.Count > 0)
                        listViewCardboardFormats.Items[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
                _log.Debug(ex.ToString());
            }
        }
        #endregion
    }
}
