#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
#endregion

namespace Pic.Plugin.GeneratorCtrl
{
    public partial class PluginViewer : Form
    {
        #region Constructor
        public PluginViewer(IComponentSearchMethod pluginSearchMethod, string pluginPath)
        {
            InitializeComponent();
            ctrlPluginViewer.SearchMethod = pluginSearchMethod;
            ctrlPluginViewer.PluginPath = pluginPath;

            ctrlPluginViewer.ValidateButtonVisible = true;
            ctrlPluginViewer.CloseButtonVisible = true;
            ctrlPluginViewer.CloseClicked += new EventHandler(ctrlPluginViewer_CloseClicked);
            ctrlPluginViewer.ValidateClicked += new EventHandler(ctrlPluginViewer_ValidateClicked);
        }
        #endregion

        #region CtrlPluginViewer event handlers
        void ctrlPluginViewer_CloseClicked(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        void ctrlPluginViewer_ValidateClicked(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        #endregion

        #region System.Windows.Forms overrides
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Z:
                    ctrlPluginViewer.FitView();
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        #endregion
    }
}