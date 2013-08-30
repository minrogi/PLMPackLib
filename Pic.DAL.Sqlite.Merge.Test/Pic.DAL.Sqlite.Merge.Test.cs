#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// DAL
using Pic.DAL.Sqlite;
// logging
using log4net;
using log4net.Config;

#endregion

namespace Pic.DAL.Sqlite.Merge.Test
{
    class Program
    {
        #region Data members
        protected static readonly ILog _log = LogManager.GetLogger(typeof(Program));
        #endregion

        #region Main
        static void Main(string[] args)
        {
            try
            {
                XmlConfigurator.Configure();

                string sArchiveFile = @"K:\Codesion\PicSharp\PicSharpDb\PicParamData2.zip";
                Pic.DAL.BackupRestore.Merge(sArchiveFile, null);
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString()); 
            }
        }
        #endregion
    }
}
