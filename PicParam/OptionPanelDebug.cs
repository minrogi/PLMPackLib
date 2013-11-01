#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
#endregion

namespace PicParam
{
    public partial class OptionPanelDebug : GLib.Options.OptionsPanel
    {
        #region Constructor
        public OptionPanelDebug()
        {
            InitializeComponent();
        }
        #endregion
        #region Load & Save handlers
        private void OptionPanelDebug_Load(object sender, EventArgs e)
        {
            checkBoxDebug.Checked = Properties.Settings.Default.DebugMode;
            this._OptionsForm.OptionsSaving += new EventHandler(OptionPanelDebug_OptionsSaving);
        }

        private void OptionPanelDebug_OptionsSaving(object sender, EventArgs e)
        {
            Properties.Settings.Default.DebugMode = checkBoxDebug.Checked;
        }
        #endregion
    }
}
