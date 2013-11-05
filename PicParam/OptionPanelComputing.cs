#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Pic.Factory2D;
#endregion

namespace PicParam
{
    public partial class OptionPanelComputing : GLib.Options.OptionsPanel
    {
        #region Constructor
        public OptionPanelComputing()
        {
            InitializeComponent();
        }
        #endregion
        #region Load & Save handlers
        private void OptionPanelComputing_Load(object sender, EventArgs e)
        {
            this.nudMaxNumberOfEntities.Value = (decimal)Pic.Factory2D.Properties.Settings.Default.AreaMaxNoSegments;
            this._OptionsForm.OptionsSaving += new EventHandler(_OptionsForm_OptionsSaving);
        }

        void _OptionsForm_OptionsSaving(object sender, EventArgs e)
        {
            Pic.Factory2D.Properties.Settings.Default.AreaMaxNoSegments = Convert.ToInt32(nudMaxNumberOfEntities.Value);
        }
        #endregion
    }
}
