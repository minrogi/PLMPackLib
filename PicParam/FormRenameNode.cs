#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
#endregion

namespace PicParam
{
    public partial class FormRenameNode : Form
    {
        #region Data members
        private Image _thumbnail;
        #endregion

        #region Constructor
        public FormRenameNode()
        {
            InitializeComponent();
        }
        #endregion

        #region Form load
        private void FormRenameNode_Load(object sender, EventArgs e)
        {
            // change caption based on NodeName
            this.Text = string.Format(this.Text, NodeName);
            // initialize controls
            EnableDisableControls();
            // display picture
        }
        #endregion

        #region Public properties
        public string NodeName
        {
            get { return textBoxName.Text; }
            set { textBoxName.Text = value; }
        }
        public string NodeDescription
        {
            get { return textBoxDescription.Text; }
            set { textBoxDescription.Text = value; }
        }
        public bool HasValidThumbnailPath
        {   get { return chkCustomImage.Checked && File.Exists(thumbnailSelectCtrl.FileName); }   }

        public Image Image
        {
            get { return _thumbnail; }
            set { _thumbnail = value; }
        }

        public string ThumbnailPath
        {   get { return thumbnailSelectCtrl.FileName; } }
        #endregion

        #region Event handlers
        private void thumbnailSelectCtrl_TextChanged(object sender, EventArgs e)
        {
            EnableDisableControls();
        }
        private void chkCustomImage_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableControls();
        }
        private void thumbnailSelectCtrl_FileNameChanged(object sender, EventArgs e)
        {
            EnableDisableControls();
        }
        #endregion

        #region Helpers
        private void EnableDisableControls()
        {
            thumbnailSelectCtrl.Enabled = chkCustomImage.Checked;
            bnOk.Enabled = !chkCustomImage.Checked || File.Exists(thumbnailSelectCtrl.FileName);
            DisplayPictures();
        }

        private void DisplayPictures()
        { 
            // show existing image
            if (!chkCustomImage.Checked)
                pictureBoxThumbnail.Image = Image;
            // show new image
            else if (chkCustomImage.Checked && File.Exists(thumbnailSelectCtrl.FileName))
                pictureBoxThumbnail.Image = Bitmap.FromFile(thumbnailSelectCtrl.FileName);
            else
                pictureBoxThumbnail.Image = null;
        }
        #endregion
    }
}
