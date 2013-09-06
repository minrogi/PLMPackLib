#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using GLib.Options;
#endregion

namespace PicParam
{
    public partial class OptionPanelDatabase : GLib.Options.OptionsPanel
    {
        #region Constructor
        public OptionPanelDatabase()
        {
            InitializeComponent();
        }
        #endregion

        #region Loading / Saving
        private void OptionPanelDatabase_Load(object sender, EventArgs e)
        {
            fileSelect.Filter = "SQLite database (*.db)|*.db";
            fileSelect.FileName = Pic.DAL.ApplicationConfiguration.CustomSection.DatabasePath;
            this._OptionsForm.OptionsSaving += new EventHandler(_OptionsForm_OptionsSaving);
        }

        void _OptionsForm_OptionsSaving(object sender, EventArgs e)
        {
            if (!Path.Equals(Pic.DAL.ApplicationConfiguration.CustomSection.DatabasePath, fileSelect.FileName)
                && File.Exists(fileSelect.FileName) )
            {
                Pic.DAL.ApplicationConfiguration.SaveDatabasePath(fileSelect.FileName);
                this.ApplicationMustRestart = true;
            }
        }
        #endregion

        #region Handler
        private void btDirectory_Click(object sender, EventArgs e)
        {
            folderBrowserDlg.SelectedPath = fileSelect.Text;
            if (DialogResult.OK == folderBrowserDlg.ShowDialog())
                fileSelect.Text = folderBrowserDlg.SelectedPath;
        }
        #endregion
    }
}
