#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;
using log4net;
#endregion

namespace Pic.DAL.LibraryLoader
{
    public class LibraryFetcher
    {
        #region Constructor
        /// <summary>
        /// LibraryFetcher 
        /// </summary>
        public LibraryFetcher()
        {
            try
            {
                // 1. download xmlList
                byte[] data;
                using (WebClient client = new WebClient())
                {
                    string uriPlmPackLib = Settings.Default.UriPlmPackLib;
                    if (!uriPlmPackLib.EndsWith("/"))
                        uriPlmPackLib += "/";
                    data = client.DownloadData(uriPlmPackLib + "xml/LibraryList.xml" );
                }
                string localFilePath = Path.Combine(Path.GetTempPath(), "LibraryList.xml");
                File.WriteAllBytes(localFilePath, data);

                // 2. if directory PLMPackLib_ThumbCache does not exist create it
                string thumbCacheDirectory = Path.Combine(Path.GetTempPath(), Library.ImageDirectory);
                if (!Directory.Exists(thumbCacheDirectory))
                    Directory.CreateDirectory(thumbCacheDirectory);

                // 3. load file
                ListOfLibraries listOfLibraries = ListOfLibraries.LoadFromFile(localFilePath);
                _libraries = listOfLibraries.Library;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
        #endregion

        #region Public properties
        public List<Library> Libraries
        {
            get { return _libraries; }
        }
        #endregion

        #region Public methods
        public bool DownloadLibraryAndMerge(int iSel)
        {
            // sanity check
            if (-1 == iSel) return false;
            // get library
            Library lib = _libraries[iSel];
            // show FormMergeLib dialog and start downloading
            FormMergeLib form = new FormMergeLib(FormMergeLib.Mode.Mode_Merge);
            form.LibraryName = lib.Name;
            form.FileName = lib.FileName;
            if (DialogResult.Cancel == form.ShowDialog())
                return false;
            // success
            return true;
        }
        public bool DownloadLibraryAndOverwrite(int iSel)
        {
            // sanity check
            if (-1 == iSel) return false;
            // get library
            Library lib = _libraries[iSel];
            // show FormMergeLib dialog and start downloading
            FormMergeLib form = new FormMergeLib(FormMergeLib.Mode.Mode_Overwrite);
            form.LibraryName = lib.Name;
            form.FileName = lib.FileName;
            if (DialogResult.Cancel == form.ShowDialog())
                return false;
            // success
            return true;
        }
        #endregion
        
        #region Data members
        protected List<Library> _libraries;
        protected static readonly ILog _log = LogManager.GetLogger(typeof(LibraryFetcher));
        #endregion
    }
}
