#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
#endregion

namespace Pic.Plugin.GeneratorCtrl.Test
{
    public partial class FormMain : Form
    {
        #region Constructor
        public FormMain()
        {
            InitializeComponent();

            bool newComponent = true;
            if (newComponent)
            {
                // source code file
                string sourceCodeFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"..\..\..\..\PicPlugins\ParamDemo.cs");
                if (File.Exists(sourceCodeFile))
                    this.generatorCtrl.setGeneratedSourceCodeFile(sourceCodeFile);
                // plugin name
                this.generatorCtrl.setDrawingName("ParamDemo");
                // plugin description
                this.generatorCtrl.setDrawingDescription("using code from file ParamDemo.cs...");
                // outputDirectory
                this.generatorCtrl.setComponentDirectory(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"..\..\..\..\PicPlugins\"));
            }
            else
            {
                this.generatorCtrl.setComponentFilePath(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"..\..\..\..\PicPlugins\f421.dll"));
            }
        }
        #endregion
    }
}