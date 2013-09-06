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
    public partial class FormEditDatabasePath : Form
    {
        #region Constructor
        public FormEditDatabasePath()
        {
            InitializeComponent();
        }
        #endregion

        #region Load/Close
        private void FormEditDatabasePath_Load(object sender, EventArgs e)
        {
            fileSelect.FileName = Pic.DAL.ApplicationConfiguration.CustomSection.DatabasePath;
            fileSelect.Filter = "SQLite database (*.db)|*.db";
            fileSelect.FileNameChanged += new EventHandler(fileSelect_FileNameChanged);
        }
        #endregion

        #region Event handlers
        void fileSelect_FileNameChanged(object sender, EventArgs e)
        {
            bnOK.Enabled = File.Exists(fileSelect.FileName);
        }
        private void bnOK_Click(object sender, EventArgs e)
        {
            if (!Path.Equals(Pic.DAL.ApplicationConfiguration.CustomSection.DatabasePath, Path.GetDirectoryName(fileSelect.FileName)))
            {
                MessageBox.Show(string.Format(PicParam.Properties.Resources.ID_APPLICATIONEXITING, Application.ProductName));
                Pic.DAL.ApplicationConfiguration.SaveDatabasePath( fileSelect.FileName );
                Application.Restart();
                Application.ExitThread();
            }
        }
        #endregion
    }
}
