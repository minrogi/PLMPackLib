#region Using directives
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Diagnostics;

using log4net;
using log4net.Config;

using Microsoft.VisualBasic.ApplicationServices; // WindowsFormsApplicationBase
using System.Threading;
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
                // force CultureToUse culture if specified in config file
                string specifiedCulture = PicParam.Properties.Settings.Default.CultureToUse;
                if (!string.IsNullOrEmpty(specifiedCulture))
                {
                    try
                    {
                        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(specifiedCulture);
                        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(specifiedCulture);
                    }
                    catch (Exception ex)
                    {
                        _log.Error(string.Format("Specified culture in config file ({0}) appears to be invalid: {1}", specifiedCulture, ex.Message));
                    }
                }

                _log.Info(string.Format("Starting {0} with culture {1}", Application.ProductName, Thread.CurrentThread.CurrentUICulture));

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
        #region Overrides WindowsFormsApplicationBase
        protected override void OnCreateSplashScreen()
        {
            if (!_showRoot)
                this.SplashScreen = new SplashScreen();
        }
        protected override void OnCreateMainForm()
        {
            // Then create the main form, the splash screen will close automatically
            this.MainForm = new FormMain(_showRoot);
        }
        protected override bool OnInitialize(ReadOnlyCollection<string> commandLineArgs)
        {
            _showRoot = false;
            foreach (string arg in commandLineArgs)
            {
                if (arg.Contains("/r") || arg.Contains("--root"))
                    _showRoot = true;
            }
            return base.OnInitialize(commandLineArgs);
        }
        #endregion
        #region Data members
        public bool _showRoot = false;
        #endregion
    }
}