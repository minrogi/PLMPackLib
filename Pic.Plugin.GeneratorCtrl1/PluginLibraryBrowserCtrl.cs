#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using log4net;
#endregion

namespace Pic.Plugin.GeneratorCtrl
{
    public partial class PluginLibraryBrowserCtrl : UserControl
    {
        #region Constants
        private const int cxButton = 150, cyButton = 150;   // image button size
        #endregion

        #region Data members
        protected static readonly ILog _log = LogManager.GetLogger(typeof(PluginLibraryBrowserCtrl));
        private Timer timer = new Timer();
        private ToolTip tooltip = new ToolTip();
        int i, x, y;
        Component[] _components;
        private string _libraryPath;
        #endregion

        #region Constructor
        public PluginLibraryBrowserCtrl()
        {
            InitializeComponent();
            // layout
            AutoScroll = true;
            timer.Interval = 50;
            timer.Tick += new EventHandler(timer_Tick);
        }
        #endregion

        #region Browsable properties
        [Browsable(true)]
        [Category("PluginLibraryBrowserCtrl")]
        [Description("Sets the plugin directory path")]
        [DisplayName("LibraryPath")]
        [DefaultValue("")]
        public string LibraryPath
        {
            set {
                _libraryPath = value;
                ComponentLoader loader = new ComponentLoader();
                _components = loader.LoadComponents(_libraryPath);

                i = x = y = 0;
                timer.Start();
            }
            get { return _libraryPath; }
         }
        #endregion

        #region Overrides UserControl
        protected override void OnResize(EventArgs e)
        {
 	        base.OnResize(e);
            AutoScrollPosition = Point.Empty;
            int x = 0, y = 0;
            foreach (Control cntl in Controls)
            {
                cntl.Location = new Point(x, y) + (Size)AutoScrollPosition;
                AdjustXY(ref x, ref y);
            }
        }
        #endregion

        #region Display methods
        #endregion

        #region Event handlers
        private void timer_Tick(object sender, EventArgs e)
        {
            if (null ==_components || i == _components.GetLength(0))
            {
                timer.Stop();
                return;
            }

            Image image;
            Guid guid;
            string name, description;
            Size size = new Size(cxButton, cyButton);
            try
            {
                Pic.Plugin.Tools tools = new Pic.Plugin.Tools(_components[i], new Pic.Plugin.ComponentSearchDirectory(_libraryPath));
                tools.GenerateImage( size, out image );
                guid = _components[i].Guid;
                name = _components[i].Name;
                description = _components[i].Description;
            }
            catch (Exception exception)
            {
                _log.Error(exception.ToString());
                ++i;
                return;
            }
 
            // create button and add to panel
            Button btn = new Button();
            btn.Image = image;
            btn.Location = new Point(x, y) + (Size)AutoScrollPosition;
            btn.Size = new Size(cxButton, cyButton);
            btn.Tag = (object)guid;
            btn.Click += new EventHandler(btn_Click);
            Controls.Add(btn);

            // give button a tooltip
            tooltip.SetToolTip(btn, String.Format("{0}\n{1}", name, description));

            // adjust i, x and y for next image
            AdjustXY(ref x, ref y);
            ++i;
        }
        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (null == btn) return;
                Guid guid = (Guid)btn.Tag;

                ComponentSelected(this, new PluginEventArgs(guid));
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
                _log.Debug(ex.ToString());
            }
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// computes position of next button
        /// </summary>
        /// <param name="x">Abscissa</param>
        /// <param name="y">Ordinate</param>
        private void AdjustXY(ref int x, ref int y)
        {
            x += cxButton;
            if (x + cxButton > Width - SystemInformation.VerticalScrollBarWidth)
            {
                x = 0;
                y += cyButton;
            }
        }
        #endregion

        #region Delegates
        public delegate void ComponentSelectHandler(object sender, PluginEventArgs e);
        #endregion

        #region Events
        public event ComponentSelectHandler ComponentSelected;
        #endregion
    }

    #region Event argument class : PluginEventArgs
    public class PluginEventArgs : EventArgs
    {
        // constructor
        public PluginEventArgs(Guid guid)
        { _pluginGuid = guid; }
        // specific property
        public Guid Guid
        { get { return _pluginGuid;  } }
        // data members
        private Guid _pluginGuid;
    }
    #endregion
}
