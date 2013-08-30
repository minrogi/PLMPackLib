#region Using directives
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using log4net;
using log4net.Config;
#endregion

namespace Pic.Plugin.Host.Test.Windows
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
            XmlConfigurator.Configure();
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
    }
}