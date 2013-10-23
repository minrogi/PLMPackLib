#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GLib.Options;
#endregion

namespace PicParam
{
    public partial class OptionPanelMajorations : GLib.Options.OptionsPanel
    {
        #region Constructor
        public OptionPanelMajorations()
        {
            InitializeComponent();
        }
        #endregion

        #region Override
        #endregion
        #region Handlers
        private void OptionPanelMajorations_Load(object sender, EventArgs e)
        {
            int iRounding = Properties.Settings.Default.MajorationRounding;
            rbRounding1.Checked = iRounding == 0;
            rbRounding2.Checked = iRounding == 1;
            rbRounding3.Checked = iRounding == 2;
            rbRounding4.Checked = iRounding == 3;

            this._OptionsForm.OptionsSaving += new EventHandler(_OptionsForm_OptionsSaving);
        }

        void _OptionsForm_OptionsSaving(object sender, EventArgs e)
        {
            int iRounding = 0;
            if (rbRounding1.Checked) iRounding = 0;
            else if (rbRounding2.Checked) iRounding = 1;
            else if (rbRounding3.Checked) iRounding = 2;
            else if (rbRounding4.Checked) iRounding = 3;

            Properties.Settings.Default.MajorationRounding = iRounding;
        }
        #endregion
    }
}
