#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Pic.Plugin;
using Pic.DAL.SQLite;

using log4net;
#endregion

namespace PicParam
{
    public partial class ComponentLoaderControl : UserControl
    {
        #region Constructor
        public ComponentLoaderControl()
        {
            InitializeComponent();
            if (this.DesignMode)  return;   // exit when in design mode 
            try
            {
                // initialize profile combo box
                comboBoxProfile.Items.Clear();
                PPDataContext db = new PPDataContext();
                List<CardboardProfile> listProfile = new List<CardboardProfile>(CardboardProfile.GetAll(db));
                foreach (CardboardProfile profile in listProfile)
                    comboBoxProfile.Items.Add(profile);
                if (comboBoxProfile.Items.Count > 0)
                    comboBoxProfile.SelectedIndex = 0;
                // ComponentSearchMethodDB
                pluginViewCtrl.SearchMethod = new ComponentSearchMethodDB();
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Exception: {0}", ex.ToString()));
            }
        }
        #endregion

        #region Component loading
        public bool LoadComponent(string filePath)
        {
            _filePath = filePath;
            if (DesignMode)
                return false;
            try
            {
                // initialize control
                pluginViewCtrl.PluginPath = filePath;

                // load component
                Pic.Plugin.Component component = null;
                using (Pic.Plugin.ComponentLoader loader = new Pic.Plugin.ComponentLoader())
                {
                    loader.SearchMethod = new ComponentSearchMethodDB();
                    component = loader.LoadComponent(filePath);
                }
                if (null == component)
                    return false;
                _componentGuid = component.Guid;

                // get parameters
                Pic.Plugin.ParameterStack stack = null;
                stack = component.BuildParameterStack(null);
                // fill name/description if empty
                _componentName = component.Name;
                _componentDescription = component.Description;
                // build dict of double parameters
                _dictParamDefaultValues.Clear();
                foreach (Parameter param in stack.ParameterList)
                {
                    ParameterDouble paramDouble = param as ParameterDouble;
                    if (null != paramDouble)
                        _dictParamDefaultValues[paramDouble.Name] = paramDouble.ValueDefault;
                }
                // insert majoration label and textbox controls
                int lblX = 16, lblY = 51;
                int offsetX = 100, offsetY = 29;
                int tabIndex = comboBoxProfile.TabIndex;

                groupBoxMajorations.Controls.Clear();

                bool hasMajorations = stack.HasMajorations;
                if (hasMajorations)
                {
                    // add combo box / label again
                    groupBoxMajorations.Controls.Add(lblProfile);
                    groupBoxMajorations.Controls.Add(comboBoxProfile);

                    int iCount = 0;
                    for (int i = 1; i < 16; ++i)
                    {
                        string paramName = string.Format("m{0}", i);
                        if (!stack.HasParameter(paramName))
                            continue;
                        // label
                        Label lbl = new Label();
                        lbl.Name = string.Format("lbl_m{0}", i);
                        lbl.Text = string.Format("m{0}", i);
                        lbl.Location = new Point(
                            lblX + (iCount / 4) * offsetX
                            , lblY + (iCount % 4) * offsetY);
                        lbl.Size = new Size(24, 13);
                        lbl.TabIndex = ++tabIndex;
                        groupBoxMajorations.Controls.Add(lbl);
                        // text box
                        TextBox tb = new TextBox();
                        tb.Name = string.Format("tb_m{0}", i);
                        tb.Text = string.Format("{0:0.##}", stack.GetDoubleParameterValue(paramName));
                        tb.Location = new Point(
                            lblX + (iCount / 4) * offsetX + lbl.Size.Width + 1
                            , lblY + (iCount % 4) * offsetY);
                        tb.Size = new Size(50, 20);
                        tb.TabIndex = ++tabIndex;
                        groupBoxMajorations.Controls.Add(tb);
                        // increment count
                        ++iCount;
                    }
                }
                groupBoxMajorations.Visible = hasMajorations;
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                return false;
            }
        }
        #endregion

        #region Public properties
        public string ComponentName
        {
            get { return _componentName; }
        }
        public string ComponentDescription
        {
            get { return _componentDescription; }
        }
        public Guid ComponentGuid
        {
            get { return _componentGuid; }
        }

        public bool HasMajorations
        {
            get
            {
                using (Pic.Plugin.ComponentLoader loader = new Pic.Plugin.ComponentLoader())
                {
                    loader.SearchMethod = new ComponentSearchMethodDB();
                    Pic.Plugin.Component component = loader.LoadComponent(_filePath);
                    ParameterStack stack = new ParameterStack();
                    stack = component.BuildParameterStack(stack);
                    return stack.HasMajorations;
                }
            }
        }

        public Dictionary<string, double> Majorations
        {
            get
            {
                // save selected profile
                _profile = comboBoxProfile.SelectedItem as CardboardProfile;

                // save majorations
                _dictMajo = new Dictionary<string, double>();
                foreach (Control ctrl in groupBoxMajorations.Controls)
                {
                    TextBox tb = ctrl as TextBox;
                    if (null == tb) continue;
                    if (tb.Name.Contains("tb_m"))
                        _dictMajo.Add(tb.Name.Substring(3), Convert.ToDouble(tb.Text));
                }
                return _dictMajo;
            }
        }
        public Dictionary<string, double> ParamDefaultValues
        {   get { return _dictParamDefaultValues; } }
        public string Profile
        {
            get
            {
                _profile = comboBoxProfile.SelectedItem as Pic.DAL.SQLite.CardboardProfile;
                return _profile.Name;
            }
        }
        #endregion

        #region Data members
        protected static readonly ILog _log = LogManager.GetLogger(typeof(ComponentLoaderControl));
        private Dictionary<string, double> _dictMajo;
        private Dictionary<string, double> _dictParamDefaultValues = new Dictionary<string, double>();
        private CardboardProfile _profile;
        private Guid _componentGuid;
        private string _componentName;
        private string _componentDescription;
        private string _filePath;
        #endregion
    }
}
