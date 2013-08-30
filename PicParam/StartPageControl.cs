#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using log4net;

using Pic.DAL.LibraryLoader;
#endregion

namespace PicParam
{
    public partial class StartPageControl : UserControl
    {
        #region Constructor
        public StartPageControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Public properties
        public System.Uri Url
        {
            get { return webBrowserStartPage.Url; }
            set { webBrowserStartPage.Url = value; webBrowserStartPage.Refresh(); }
        }
        public DocumentTreeView TreeViewCtrl
        {
            set { _treeViewCtrl = value; }
        }
        #endregion

        #region Event handlers
        private void listBoxLibraries_SelectedIndexChanged(object sender, EventArgs e)
        {
            bnMerge.Enabled = -1 != listBoxLibraries.SelectedIndex;
        }
        private void bnMerge_Click(object sender, EventArgs e)
        {
            try
            {
                // collapse tree and replace "Root" childrens with _DUMMY_ element
                _treeViewCtrl.CollapseRoot();
                // get listbox selected index
                int iSel = listBoxLibraries.SelectedIndex;
                if (-1 == iSel) return;
                // download
                if (libFetcher.DownloadLibraryAndMerge(iSel))
                {}
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
        private void bnOverwrite_Click(object sender, EventArgs e)
        {
            // warning message
            if (DialogResult.No == MessageBox.Show(
                PicParam.Properties.Resources.ID_DATABASEOVERWRITE
                , Application.ProductName, MessageBoxButtons.YesNo))
                return;

            try
            {
                // collapse tree and replace "Root" childrens with _DUMMY_ element
                _treeViewCtrl.CollapseRoot();
                // get listbox selected index
                int iSel = listBoxLibraries.SelectedIndex;
                if (-1 == iSel) return;
                // download
                if (libFetcher.DownloadLibraryAndOverwrite(iSel))
                {}
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }        
        }
        #endregion

        #region User control override
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                if (IsWebSiteReachable)
                {
                    libFetcher = new LibraryFetcher();
                    // fill fetcher
                    List<Library> libraries = libFetcher.Libraries;
                    foreach (Library lib in libraries)
                        listBoxLibraries.Items.Add(lib);
                    if (listBoxLibraries.Items.Count > 0)
                        listBoxLibraries.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }

            base.OnLoad(e);
        }
        /// <summary>
        /// This property is aimed at finding out if the machine is connected to the web
        /// It actually checks if StartPageUrl define in configutation file can still be reached
        /// </summary>
       	public bool IsWebSiteReachable
        {
            get
            {
                try
                {
                    System.Uri uri = new System.Uri(PicParam.Properties.Settings.Default.StartPageUrl);
                    System.Net.IPHostEntry objIPHE = System.Net.Dns.GetHostEntry(uri.DnsSafeHost);
                    return true;
                }
                catch (Exception ex)
                {
                    _log.Error(ex.ToString());
                    return false;
                }
            }
        }
        #endregion

        #region Data members
        protected static readonly ILog _log = LogManager.GetLogger(typeof(StartPageControl));
        private LibraryFetcher libFetcher;
        private DocumentTreeView _treeViewCtrl;
        #endregion
    }
}
