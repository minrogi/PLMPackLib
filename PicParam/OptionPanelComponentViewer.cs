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
    public partial class OptionPanelComponentViewer : GLib.Options.OptionsPanel
    {
        #region Constructor
        public OptionPanelComponentViewer()
        {
            InitializeComponent();
        }
        #endregion
        #region Load & Save handlers
        private void OptionPanelComponentViewer_Load(object sender, EventArgs e)
        {
            checkBoxAllowParameterAnimation.Checked = Pic.Plugin.ViewCtrl.Properties.Settings.Default.AllowParameterAnimation;
            nudNoStepsAnimation.Value = (decimal)Pic.Plugin.ViewCtrl.Properties.Settings.Default.NumberOfAnimationSteps;
            this._OptionsForm.OptionsSaving +=new EventHandler(OptionsPanelComponentViewer_OptionsSaving);
            // enable/disable animation steps controls
            checkBoxAllowParameterAnimation_CheckedChanged(this, null);
        }

        private void OptionsPanelComponentViewer_OptionsSaving(object sender, EventArgs e)
        {
            Pic.Plugin.ViewCtrl.Properties.Settings.Default.AllowParameterAnimation = checkBoxAllowParameterAnimation.Checked;
            Pic.Plugin.ViewCtrl.Properties.Settings.Default.NumberOfAnimationSteps = (int)nudNoStepsAnimation.Value;
            // force application restart
            // since Plugin.Viewer control is only instantiated as application starts
            this.ApplicationMustRestart = true;            
        }
        #endregion
        #region Control event handlers
        private void checkBoxAllowParameterAnimation_CheckedChanged(object sender, EventArgs e)
        {
            lbNoStepsAnimation.Enabled = checkBoxAllowParameterAnimation.Checked;
            nudNoStepsAnimation.Enabled = checkBoxAllowParameterAnimation.Checked;
        }
        #endregion
    }
}
