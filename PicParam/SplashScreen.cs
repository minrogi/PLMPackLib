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
        // Threading
        static SplashScreen ms_frmSplash = null;
        static Thread ms_oThread = null;
        #endregion

        #region Static members
        // A static method to create the thread and 
        // launch the SplashScreen.
        static public void ShowSplashScreen()
        {
            // Make sure it's only launched once.
            if (ms_frmSplash != null)
                return;
            ms_oThread = new Thread(new ThreadStart(SplashScreen.ShowForm));
            ms_oThread.IsBackground = true;
            ms_oThread.TrySetApartmentState( ApartmentState.STA );
            ms_oThread.Start();
        }

        // A property returning the splash screen instance
        static public SplashScreen SplashForm
        {
            get
            {
                return ms_frmSplash;
            }
        }

        // A private entry point for the thread.
        static private void ShowForm()
        {
            ms_frmSplash = new SplashScreen();
            Application.Run(ms_frmSplash);
        }
        #endregion

        #region Constructor
        public SplashScreen()
        {
            InitializeComponent();

            if (null != this.BackgroundImage)
            {
                // make lower right pixel color transparent
                Bitmap b = new Bitmap(this.BackgroundImage);
                b.MakeTransparent(b.GetPixel(1, 1));
                this.BackgroundImage = b;
            }

            // version
            lblVersion.Text = String.Format("Version {0}", AssemblyVersion); ;
        }
        #endregion

        #region Public properties
        /// <summary>
        /// retrieves assembly version
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
        /// <summary>
        ///  set / get time interval after which the splash screen will close
        /// </summary>
        public int TimerInterval
        {
            set { timerClose.Interval = value; }
            get { return timerClose.Interval; }
        }
        #endregion

        #region Timer handler
        /// <summary>
        /// Handles timer tick and closes form
        /// </summary>
        private void timerClose_Tick(object sender, EventArgs e)
        {
            timerClose.Stop();
            Close();
        }
        #endregion
    }
}
