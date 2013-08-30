using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PicParam
{
    public partial class FormCreateCardboardFormat : Form
    {
        #region Constructor
        public FormCreateCardboardFormat()
        {
            InitializeComponent();
            UpdateButtonOk();
        }
        #endregion

        #region Public properties
        public string FormatName
        {
            get { return tbName.Text; }
            set { tbName.Text = value; }
        }
        public string Description
        {
            get { return tbDescription.Text; }
            set { tbDescription.Text = value; }
        }
        public float FormatWidth
        {
            get { return (float)nudWidth.Value; }
            set { nudWidth.Value = (decimal)value; }
        }
        public float FormatHeight
        {
            get { return (float)nudHeight.Value; }
            set { nudHeight.Value = (decimal)value; }
        }
        #endregion

        #region Event handlers
        private void tbName_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonOk();
        }
        private void UpdateButtonOk()
        { 
            bnOk.Enabled = !string.IsNullOrEmpty(tbName.Text) && !string.IsNullOrEmpty(tbDescription.Text);
        }
        #endregion

        private void FormCreateCardboardFormat_Load(object sender, EventArgs e)
        {

        }
    }
}
