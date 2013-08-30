using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pic.Plugin.GeneratorCtrl
{
    public partial class PluginLibraryBrowser : Form
    {
        #region Constructor
        public PluginLibraryBrowser(string libraryPath)
        {
            InitializeComponent();
            browserCtrl.LibraryPath = libraryPath;
        }
        #endregion

        #region Event handlers
        private void onComponentSelected(object sender, PluginEventArgs e)
        {
            _guid = e.Guid;            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Data members
        public Guid _guid;
        #endregion
    }
}