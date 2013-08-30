#region Using directives
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using log4net;
using log4net.Config;
#endregion

namespace Pic.Plugin.ViewCtrl.Test
{
    static class Program
    {
        public static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            XmlConfigurator.Configure();

            string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            _log.Info("Starting " + assemblyName);
            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
            _log.Info("Finishing " + assemblyName);
        }
    }
}