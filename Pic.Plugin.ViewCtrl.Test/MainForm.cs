#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using log4net;
#endregion

namespace Pic.Plugin.ViewCtrl.Test
{
    public partial class MainForm : Form
    {
        protected static readonly ILog _log = LogManager.GetLogger(typeof(MainForm));

        public MainForm()
        {
            InitializeComponent();
            try
            {
                string componentPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"..\..\..\PicPlugins\Component2.dll");
                if (File.Exists(componentPath))
                    pluginViewCtrl.PluginPath = componentPath;
                else
                {
                    MessageBox.Show(string.Format("You need to run project Pic.Plugin.Generator.Test.Console to generate {0}", componentPath));
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
            pluginViewCtrl.CloseClicked += new EventHandler(pluginViewCtrl_CloseClicked);
        }

        void pluginViewCtrl_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }
    }
}