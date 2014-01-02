#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;

using log4net;
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
        #region Event handlers
        private void bnUpdateLocalisationFile_Click(object sender, EventArgs e)
        {
            try
            {
                // process all components and collect parameter descriptions
                FormWorkerThreadTask.Execute(new TPTCollectPluginParameterNames());

                // open localisation file
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.Verb = "Open";
                startInfo.CreateNoWindow = false;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.FileName = LocalizerImpl.Instance.LocalisationFileName;
                using (Process p = new Process())
                {
                    p.StartInfo = startInfo;
                    p.Start();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
        #endregion
        #region Data members
        protected static readonly ILog _log = LogManager.GetLogger(typeof(OptionPanelDebug));
        #endregion
    }
}
