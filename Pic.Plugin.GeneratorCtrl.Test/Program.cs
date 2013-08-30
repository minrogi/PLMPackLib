#region Using directives
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using log4net;
using log4net.Config;
#endregion

namespace Pic.Plugin.GeneratorCtrl.Test
{
    static class Program
    {
        static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // set up a simple configuration that logs on the console.
                XmlConfigurator.Configure();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}