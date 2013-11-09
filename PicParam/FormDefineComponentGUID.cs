#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
#endregion

namespace PicParam
{
    public partial class FormDefineComponentGUID : Form
    {
        #region Constructor
        public FormDefineComponentGUID()
        {
            InitializeComponent();
            tbGUID.TextChanged += new EventHandler(tbGUID_TextChanged);
        }
        #endregion

        #region Event handlers
        private void tbGUID_TextChanged(object sender, EventArgs e)
        {
            bnOK.Enabled = HasValidGUID;
        }
        #endregion

        #region Public properties
        public bool HasValidGUID
        {
            get
            {
                Guid guid = Guid.Empty;
                return Guid.TryParse(tbGUID.Text, out guid);
            }
        }

        public Guid Guid
        {
            get
            {
                Guid guid = Guid.Empty;
                Guid.TryParse(tbGUID.Text, out guid);
                return guid;
            }
            set
            {
                tbGUID.Text = value.ToString();
            }
        }
        #endregion
    }
}
