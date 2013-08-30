using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.Linq;
using System.Text;
using System.Windows.Forms;

namespace PicParam
{
    public partial class FormCreateCardboardProfile : Form
    {
        #region Constructor
        public FormCreateCardboardProfile()
        {
            InitializeComponent();
        }
        #endregion

        #region Public properties
        public string ProfileName
        {
            get { return tbName.Text; }
            set { tbName.Text = value; }
        }
        public string Code
        {
            get { return tbCode.Text; }
            set { tbCode.Text = value; }
        }

        public float Thickness
        {
            get { return float.Parse(tbThickness.Text, System.Globalization.CultureInfo.InvariantCulture.NumberFormat); }
            set { tbThickness.Text = string.Format(System.Globalization.CultureInfo.InvariantCulture.NumberFormat, "{0:0.00}", value); }
        }
        #endregion
    }
}
