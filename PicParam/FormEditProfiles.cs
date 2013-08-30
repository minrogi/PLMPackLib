#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using Pic.DAL.SQLite;

using log4net;
#endregion

namespace PicParam
{
    public partial class FormEditProfiles : Form
    {
        protected static readonly ILog _log = LogManager.GetLogger(typeof(FormEditProfiles));

        #region Constructor
        public FormEditProfiles()
        {
            InitializeComponent();
            // disable delete button
            btDelete.Enabled = false;
            // fill list view with existing cardboard profiles
            FillListView();
            // select first item
            if (listViewProfile.Items.Count > 0)
                listViewProfile.Items[0].Selected = true;
        }
        #endregion

        #region FillListView with profiles
        private void FillListView()
        {
            try
            {
                // clear all existing items
                listViewProfile.Items.Clear();

                PPDataContext db = new PPDataContext();
                CardboardProfile[] cardboardProfiles = CardboardProfile.GetAll(db);
                foreach (Pic.DAL.SQLite.CardboardProfile profile in cardboardProfiles)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = profile.Name;
                    item.SubItems.Add(profile.Code);
                    item.SubItems.Add(string.Format("{0}",profile.Thickness));
                    listViewProfile.Items.Add(item);
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
        private void listViewProfile_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (this.listViewProfile.SelectedIndices.Count > 0)
                {
                    int iSel = this.listViewProfile.SelectedIndices[0];
                    PPDataContext db = new PPDataContext();
                    CardboardProfile[] cardboardProfiles = CardboardProfile.GetAll(db);
                    this.btDelete.Enabled = !cardboardProfiles[iSel].HasMajorationSets;
                }
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
                _log.Debug(ex.ToString());
            }
        }

        void bnModify_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.listViewProfile.SelectedIndices.Count > 0)
                {
                    int iSel = this.listViewProfile.SelectedIndices[0];
                    Pic.DAL.SQLite.PPDataContext db = new Pic.DAL.SQLite.PPDataContext();
                    Pic.DAL.SQLite.CardboardProfile[] cardboardProfiles = Pic.DAL.SQLite.CardboardProfile.GetAll(db);
                    Pic.DAL.SQLite.CardboardProfile currentCardboardProfile = cardboardProfiles[iSel];
                    FormCreateCardboardProfile dlg = new FormCreateCardboardProfile();
                    dlg.ProfileName = currentCardboardProfile.Name;
                    dlg.Code = currentCardboardProfile.Code;
                    dlg.Thickness = currentCardboardProfile.Thickness;
                    if (DialogResult.OK == dlg.ShowDialog())
                    {
                        // set new values
                        currentCardboardProfile.Name = dlg.ProfileName;
                        currentCardboardProfile.Code = dlg.Code;
                        currentCardboardProfile.Thickness = dlg.Thickness;
                        // update database
                        currentCardboardProfile.Update(db);
                        // refill list view
                        FillListView();
                        // select current item
                        listViewProfile.Items[iSel].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
                _log.Debug(ex.ToString());
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewProfile.SelectedIndices.Count > 0)
                {
                    Pic.DAL.SQLite.PPDataContext db = new Pic.DAL.SQLite.PPDataContext();
                    // delete selected cardboard profile
                    int iSel = this.listViewProfile.SelectedIndices[0];
                    Pic.DAL.SQLite.CardboardProfile[] cardboardProfiles = Pic.DAL.SQLite.CardboardProfile.GetAll(db);
                    cardboardProfiles[iSel].Delete(db);
                    // fill list view again
                    FillListView();
                    // select first item
                    if (listViewProfile.Items.Count > 0)
                        listViewProfile.Items[0].Selected = true;
                }
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
                FormCreateCardboardProfile dlg = new FormCreateCardboardProfile();
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    Pic.DAL.SQLite.PPDataContext db = new Pic.DAL.SQLite.PPDataContext();
                    Pic.DAL.SQLite.CardboardProfile.CreateNew(db, dlg.ProfileName, dlg.Code, dlg.Thickness);
                    FillListView();
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