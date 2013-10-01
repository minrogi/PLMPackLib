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
        #endregion

        #region User control override
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
        private DocumentTreeView _treeViewCtrl;
        #endregion
    }
}
