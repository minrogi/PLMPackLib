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
    public partial class OptionsFormPLMPackLib : GLib.Options.OptionsForm
    {
        public OptionsFormPLMPackLib()
            :base(PicParam.Properties.Settings.Default)
        {
            InitializeComponent();
            Panels.Add(new OptionPanelWindow());
            Panels.Add(new OptionPanelDatabase());
            Panels.Add(new OptionPanelComputing());
            Panels.Add(new OptionPanelThumbnail());
            Panels.Add(new OptionPanelOutputFileOpening());
            Panels.Add(new OptionPanelMajorations());
            Panels.Add(new OptionPanelDebug());
        }
    }
}
