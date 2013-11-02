#region Using directives
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

using log4net;
using log4net.Config;

using Microsoft.VisualBasic.ApplicationServices; // WindowsFormsApplicationBase
#endregion

namespace PicParam
{
    static class Program
    {
        static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // set up a simple configuration
            try
            {
                _log.Info("Starting " + Application.ProductName);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                bool bSuccess = true;
                string errorMessage = string.Empty;
                // check database availaibility
                string databasePath = Pic.DAL.ApplicationConfiguration.CustomSection.DatabasePath;
                if (!System.IO.File.Exists(databasePath))
                {
                    bSuccess = false;
                    errorMessage = string.Format(Properties.Resources.ID_DATABASENOTFOUND, databasePath);
                    if (DialogResult.Yes == MessageBox.Show(errorMessage, Application.ProductName, MessageBoxButtons.YesNo))
                    {
                        OpenFileDialog openFileDialogRestore = new OpenFileDialog();
                        if (DialogResult.OK == openFileDialogRestore.ShowDialog())
                        {
                            Pic.DAL.IProcessingCallback callback = null;
                            if (!Pic.DAL.BackupRestore.Restore(
                                openFileDialogRestore.FileName
                                , callback))
                            {
                                MessageBox.Show(string.Format(Properties.Resources.ID_RESTOREFAILURE, errorMessage));
                                bSuccess = false;
                            }
                            else
                                bSuccess = true;
                        }
                    }
                    else
                        errorMessage = string.Format(Properties.Resources.ID_APPLICATIONEXITING, Application.ProductName);
                }

                if (bSuccess)
                {   // database and default pictures available -> show mainform
                    new PLMPackLibApp().Run(args);
                }
                else
                {   // not available -> show error message, log and terminate application
                    MessageBox.Show(errorMessage, Application.ProductName, MessageBoxButtons.OK);
                    _log.Error(errorMessage);
                }
                _log.Info("Ending " + Application.ProductName);
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
                _log.Error(ex.ToString());
            }
        }
    }

    class PLMPackLibApp : WindowsFormsApplicationBase
    {
        protected override void OnCreateSplashScreen()
        {
            this.SplashScreen = new SplashScreen();
        }
        protected override void OnCreateMainForm()
        {
            // Then create the main form, the splash screen will close automatically
            this.MainForm = new MainForm();
        }
    }
}