#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Plossum.CommandLine;
using ICSharpCode.SharpZipLib.Zip;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.Security.Principal;

using log4net;
using log4net.Config;
using Pic.DAL;
#endregion

namespace ZipExtract
{
    class Program
    {
        #region Constants
        public static int LINE_WIDTH = 78;
        protected static readonly ILog _log = LogManager.GetLogger(typeof(Program));
        #endregion

        #region Command line manager : class Options
        [CommandLineManager(ApplicationName = "ZipExtract.exe", Copyright = "Copyright (c) TreeDim")]
        class Options
        {
            [CommandLineOption(Description = "Displays this help text")]
            public bool Help = false;
            [CommandLineOption(Description = "Specifies the ", MinOccurs = 1)]
            public string m
            {
                get { return mode; }
                set
                {
                    if (!string.Equals(value, "backup", StringComparison.CurrentCultureIgnoreCase) && !string.Equals(value, "restore", StringComparison.CurrentCultureIgnoreCase))
                        throw new InvalidOptionValueException("m needs to be either backup or restore");
                    mode = value.ToLower();
                }
            }
            [CommandLineOption(Description = "Specifies the component tree file (.zip) to backup/restore", MinOccurs = 1)]
            public string f
            {
                get { return zipFile; }
                set
                {
                    if (string.IsNullOrEmpty(value))
                        throw new InvalidOptionValueException("The data file path must not be empty", false);
                    if (!System.IO.File.Exists(value))
                        throw new InvalidOptionValueException(string.Format("The file {0} could not be found.", value), false);
                    zipFile = value;
                }
            }
            [CommandLineOption(Description = "Specifies the directory to backup/restore", MinOccurs = 0, BoolFunction = BoolFunction.TrueIfPresent)]
            public string d
            {
                get { return dir; }
                set
                {
                    if (string.IsNullOrEmpty(value))
                        throw new InvalidOptionValueException("The directory path must not be empty", false);
                    if (!System.IO.Directory.Exists(value))
                        throw new InvalidOptionValueException(string.Format("The path {0} does not exist", value), false);
                    dir = value;
                }
            }

            private string zipFile;
            private string dir;
            private string mode;
        }
        #endregion

        #region Main

        static int Main(string[] args)
        {
            // set up a simple configuration that logs on the console.
            XmlConfigurator.Configure();

            try
            {
                Options options = new Options();
                CommandLineParser parser = new CommandLineParser(options);
                parser.Parse();

                _log.Info(parser.UsageInfo.GetHeaderAsString(LINE_WIDTH));
                if (options.Help)
                {
                    _log.Info(parser.UsageInfo.GetOptionsAsString(LINE_WIDTH));
                    return 0;
                }
                else if (parser.HasErrors)
                {
                    _log.Error(parser.UsageInfo.GetErrorsAsString(LINE_WIDTH));
                    return -1;
                }
                // --- get mode -----------------------------------------------
                string mode = options.m;
                // --- get zip file path --------------------------------------
                string zipFilePath = options.f;
                // --- get destination directory
                string destinationDirectory = options.d;
                ProcessingCallback callback = new ProcessingCallback();
                if (string.Equals(mode, "backup", StringComparison.CurrentCultureIgnoreCase)) // restore
                {
                    string errorMessage = string.Empty;
                    if (!BackupRestore.BackupFull(zipFilePath, callback))
                        return -1;
                }
                else // back
                { 
                    string errorMessage = string.Empty;
                    if (!BackupRestore.Restore(zipFilePath, callback))
                        return -1;                
                }
            }
            catch (System.Exception ex)
            {
                _log.Error(ex.ToString());
            }

            return 0;
        }
        #endregion
    }

    class ProcessingCallback : Pic.DAL.IProcessingCallback
    {
        // processing callback
        public ProcessingCallback()            { _count = 0; }
        public void Begin()                    {}
        public void End()                      {}
        public void Info(string text)          { _log.Info(text); }
        public void Error(string text)         { _log.Error(text); ++_count; }
        public bool IsAborting                 { get { return false; } }
        public bool HadErrors                  { get { return _count != 0; } }
        // data members
        private int _count;
        protected static readonly ILog _log = LogManager.GetLogger(typeof(ProcessingCallback));
    }
}
