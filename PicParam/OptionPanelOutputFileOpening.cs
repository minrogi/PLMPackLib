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
    public partial class OptionPanelOutputFileOpening : GLib.Options.OptionsPanel
    {
        public OptionPanelOutputFileOpening()
        {
            InitializeComponent();
        }

        private void OptionPanelOutputFileOpening_Load(object sender, EventArgs e)
        {
            // DXF
            checkBoxAppDXF.Checked = !string.IsNullOrEmpty(Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppDXF);
            checkBoxAppDXF.CheckedChanged += new EventHandler(checkBoxApp_CheckedChanged);
            fileSelectOutputDXF.Filter = "Executable (*.exe)|*.exe|All Files|*.*";
            fileSelectOutputDXF.FileName = Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppDXF;
            // AI
            checkBoxAppAI.Checked = !string.IsNullOrEmpty(Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppAI);
            checkBoxAppAI.CheckedChanged += new EventHandler(checkBoxApp_CheckedChanged);
            fileSelectOutputAI.Filter = "Executable (*.exe)|*.exe|All Files|*.*";
            fileSelectOutputAI.FileName = Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppAI;
            // CF2
            checkBoxAppCF2.Checked = !string.IsNullOrEmpty(Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppCF2);
            checkBoxAppCF2.CheckedChanged += new EventHandler(checkBoxApp_CheckedChanged);
            fileSelectOutputCF2.Filter = "Executable (*.exe)|*.exe|All Files|*.*";
            fileSelectOutputCF2.FileName = Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppCF2;

            this._OptionsForm.OptionsSaving += new EventHandler(_OptionsForm_OptionsSaving);

            checkBoxApp_CheckedChanged(this, null);
        }

        void _OptionsForm_OptionsSaving(object sender, EventArgs e)
        {
            Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppDXF = checkBoxAppDXF.Enabled ? fileSelectOutputDXF.FileName : string.Empty;
            Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppAI = checkBoxAppAI.Enabled ? fileSelectOutputAI.FileName : string.Empty;
            Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppCF2 = checkBoxAppCF2.Enabled ? fileSelectOutputCF2.FileName : string.Empty;
        }

        private void checkBoxApp_CheckedChanged(object sender, EventArgs e)
        {
            fileSelectOutputDXF.Enabled = checkBoxAppDXF.Checked;
            fileSelectOutputAI.Enabled = checkBoxAppAI.Checked;
            fileSelectOutputCF2.Enabled = checkBoxAppCF2.Checked;
        }
    }
}
