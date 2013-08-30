#region Using directives
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using log4net;
using log4net.Config;
#endregion

#pragma warning disable 162

namespace Pic.Factory2D.Control.Test
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

                const int iIndex = 2;
                if (1 == iIndex)
                    Application.Run(new FormMain1());
                else if (2 == iIndex)
                    Application.Run(new FormMain2());
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
    }
}