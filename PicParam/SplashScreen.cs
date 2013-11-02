#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using System.Threading;
#endregion

namespace PicParam
{
    public partial class SplashScreen : Form
    {
        #region Data members
        private bool _transparent = false;
        #endregion

        #region Constructor
        public SplashScreen()
        {
            InitializeComponent();

            if (null != this.BackgroundImage)
            {
                Bitmap b = PicParam.Properties.Settings.Default.UseRebrandedVersion
                    ? PicParam.Properties.Resources.splashpic_ocecanon
                    : PicParam.Properties.Resources.splashpic;
                // make lower right pixel color transparent
                if (Transparent)
                    b.MakeTransparent(b.GetPixel(1, 1));
                this.BackgroundImage = b;
            }

            // version
            lblVersion.Text = String.Format("Version {0}", AssemblyVersion);
        }
        #endregion

        #region Public properties
        /// <summary>
        /// retrieves assembly version
        /// </summary>
        public string AssemblyVersion
        { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }
        /// <summary>
        /// set / get transparency
        /// </summary>
        public bool Transparent
        {
            get { return _transparent; }
            set { _transparent = value; }
        }
        #endregion
    }
}
