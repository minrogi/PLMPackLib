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
    public partial class OptionPanelThumbnail : GLib.Options.OptionsPanel
    {
        #region Constructor
        public OptionPanelThumbnail()
        {
            InitializeComponent();
        }
        #endregion

        #region Override
        private void OptionPanelThumbnail_Load(object sender, EventArgs e)
        {
            int iModeAnnotation = Properties.Settings.Default.ThumbnailAnnotationMode;
            rbAnnotations1.Checked = (iModeAnnotation == 0);
            rbAnnotations2.Checked = (iModeAnnotation == 1);
            rbAnnotations3.Checked = (iModeAnnotation == 2);

            nudFontSize.Value = (decimal)Properties.Settings.Default.ThumbnailAnnotationFont;

            this._OptionsForm.OptionsSaving += new EventHandler(_OptionsForm_OptionsSaving);
        }

        void _OptionsForm_OptionsSaving(object sender, EventArgs e)
        {
            // save mode annotation
            int iMode = 0;
            if (rbAnnotations1.Checked) iMode = 0;
            else if (rbAnnotations2.Checked) iMode = 1;
            else if (rbAnnotations3.Checked) iMode = 2;
            Properties.Settings.Default.ThumbnailAnnotationMode = iMode;
            // save font size
            Properties.Settings.Default.ThumbnailAnnotationFont = (int)nudFontSize.Value;

            this.ApplicationMustRestart = true;
        }
        #endregion
    }
}
