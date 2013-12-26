#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.Mapping;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Pic.DAL.SQLite;

using Pic.Plugin;
using Pic.Plugin.ViewCtrl;
#endregion

namespace PicParam
{
    public partial class FormEditMajorations : Form
    {
        #region Constructor
        public FormEditMajorations(int compId, Profile currentProfile, ProfileLoader profileLoader)
        {
            InitializeComponent();

            _componentId = compId;
            _profileLoader = profileLoader;

            if (!DesignMode)
            {
                // plugin viewer
                _pluginViewCtrl = new PluginViewCtrl();
                _pluginViewCtrl.Size = _pb.Size;
                _pluginViewCtrl.Location = _pb.Location;
                _pluginViewCtrl.Visible = true;
                this.Controls.Add(_pluginViewCtrl);

                // hide
                _pb.Visible = false;
            }

            // fill combo box
            FillProfileComboBox(currentProfile.ToString());

            // get parameter stack
            Pic.Plugin.ParameterStack stack = null;
            using (Pic.Plugin.ComponentLoader loader = new Pic.Plugin.ComponentLoader())
            {
                PPDataContext db = new PPDataContext();
                Pic.DAL.SQLite.Component comp = Pic.DAL.SQLite.Component.GetById(db, _componentId);
                Pic.Plugin.Component component = loader.LoadComponent(comp.Document.File.Path(db));
                stack = component.BuildParameterStack(null);
                // load component in pluginviewer
                _pluginViewCtrl.Component = component;
            }

            // insert majoration label and textbox controls
            int lblX = 20, lblY = 60;
            int offsetX = 110, offsetY = 29;
            int tabIndex = bnCancel.TabIndex;
            int iCount = 0;
            foreach (Parameter param in stack.ParameterList)
            {
                // only shows majorations
                if (!param.IsMajoration) continue;

                Label lbl = new Label();
                lbl.Name = string.Format("lbl_{0}", param.Name);
                lbl.Text = param.Name;
                lbl.Location = new Point(
                    lblX + (iCount / 5) * offsetX
                    , lblY + (iCount % 5) * offsetY);
                lbl.Size = new Size(30, 13);
                lbl.TabIndex = ++tabIndex;
                this.Controls.Add(lbl);


                ParameterDouble paramDouble = param as ParameterDouble;

                NumericUpDown nud = new NumericUpDown();
                nud.Name = string.Format("nud_{0}", param.Name);
                nud.Increment = 0.1M;
                nud.Minimum = paramDouble.HasValueMin ? (decimal)paramDouble.ValueMin : -10000.0M;
                nud.Maximum = paramDouble.HasValueMax ? (decimal)paramDouble.ValueMax : 10000.0M;
                nud.DecimalPlaces = 1;
                nud.Value = (decimal)stack.GetDoubleParameterValue(param.Name);
                nud.Location = new Point(
                    lblX + (iCount / 5) * offsetX + lbl.Size.Width + 1
                    , lblY + (iCount % 5) * offsetY);
                nud.Size = new Size(60, 20);
                nud.TabIndex = ++tabIndex;
                nud.ValueChanged += new EventHandler(nudValueChanged);
                this.Controls.Add(nud);

                ++iCount;
            }

            UpdateMajorationValues();
        }

        private void FillProfileComboBox(string selectedProfileName)
        {
            // initialize profile combo box
            comboBoxProfile.Items.Clear();
            PPDataContext db = new PPDataContext();
            CardboardProfile[] profiles = CardboardProfile.GetAll(db);
            foreach (CardboardProfile profile in profiles)
            {
                comboBoxProfile.Items.Add(profile);
                if (profile.Name == selectedProfileName)
                    comboBoxProfile.SelectedItem = profile;
            }
            _profile = comboBoxProfile.SelectedItem as CardboardProfile;
        }

        void nudValueChanged(object sender, EventArgs e)
        {    _dirty = true;   }
        #endregion

        #region Loading
        private void EditMajorationsForm_Load(object sender, EventArgs e)
        {
        }

        private void UpdateMajorationValues()
        {
            // rounding to be applied while building majoration dictionary
            Pic.DAL.SQLite.Component.MajoRounding majoRounding = Pic.DAL.SQLite.Component.MajoRounding.ROUDING_FIRSTDECIMALNEAREST;
            switch (Properties.Settings.Default.MajorationRounding)
            {
                case 0: majoRounding = Pic.DAL.SQLite.Component.MajoRounding.ROUDING_FIRSTDECIMALNEAREST; break;
                case 1: majoRounding = Pic.DAL.SQLite.Component.MajoRounding.ROUNDING_HALFNEAREST; break;
                case 2: majoRounding = Pic.DAL.SQLite.Component.MajoRounding.ROUNDING_HALFTOP; break;
                case 3: majoRounding = Pic.DAL.SQLite.Component.MajoRounding.ROUDING_INT; break;
                default: break;
            }
            // retrieve majoration from database
            PPDataContext db = new PPDataContext();
            Dictionary<string, double> dictMajo = Pic.DAL.SQLite.Component.GetDefaultMajorations(db, _componentId, _profile, majoRounding);
            // update nud control values
            foreach (Control ctrl in Controls)
            {
                NumericUpDown nud = ctrl as NumericUpDown;
                if ( null == nud || !nud.Name.StartsWith("nud_"))
                    continue;
                if (dictMajo.ContainsKey(nud.Name.Substring(4)))
                    nud.Value = (decimal)dictMajo[nud.Name.Substring(4)];

                nud.MouseEnter += new EventHandler(nud_MouseEnter);
                nud.ValueChanged += new EventHandler(nud_ValueChanged);
            }
            _dirty = false;
        }

        private void SaveMajorationValues()
        {
            Dictionary<string, double> dictMajo = new Dictionary<string, double>();
            foreach (Control ctrl in Controls)
            {
                NumericUpDown nud = ctrl as NumericUpDown;
                if (null == nud) continue;
                if (nud.Name.Contains("nud_m"))
                    dictMajo.Add(nud.Name.Substring(4), Convert.ToDouble(nud.Value));
            }
            Pic.DAL.SQLite.PPDataContext db = new Pic.DAL.SQLite.PPDataContext();
            Pic.DAL.SQLite.Component comp = Pic.DAL.SQLite.Component.GetById(db, _componentId);
            comp.UpdateMajorationSet(db,_profile, dictMajo);

            // notify listeners
            _profileLoader.NotifyModifications();
        }
        #endregion

        #region Public properties
        #endregion

        #region Event handlers
        private void comboBoxProfile_selectedIndexChanged(object sender, EventArgs e)
        {
            if (_dirty)
                if (MessageBox.Show(string.Format("Save changes in majorations for profile \"{0}\"", _profile.Name)
                    , Application.ProductName
                    , MessageBoxButtons.YesNo) == DialogResult.Yes)
                    SaveMajorationValues();
            _profile = comboBoxProfile.SelectedItem as CardboardProfile;
            UpdateMajorationValues();
        }
        private void bnApply_Click(object sender, EventArgs e)
        {
            SaveMajorationValues();
        }
        private void bnOK_Click(object sender, EventArgs e)
        {
            SaveMajorationValues();
        }
        private void bnEditProfiles_Click(object sender, EventArgs e)
        {
            // show form to edit profiles
            FormEditProfiles form = new FormEditProfiles();
            form.ShowDialog();
            // refill combo box has some profile might have been added
            FillProfileComboBox(_profile.Name);
        }
        private void nud_MouseEnter(object sender, EventArgs e)
        {
            NumericUpDown nudControl = sender as NumericUpDown;
            if (null != nudControl)
                _pluginViewCtrl.AnimateParameterName(nudControl.Name.Substring(4));
        }
        private void nud_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nudControl = sender as NumericUpDown;
            if (null != nudControl)
                _pluginViewCtrl.SetParameterValue(nudControl.Name.Substring(4), (double)nudControl.Value);
        }
        #endregion

        #region Data members
        private int _componentId;
        private CardboardProfile _profile;
        private bool _dirty;
        private ProfileLoader _profileLoader;
        /// <summary>
        /// Component viewer
        /// </summary>
        private Pic.Plugin.ViewCtrl.PluginViewCtrl _pluginViewCtrl;
        #endregion
    }
}