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
            }

            // insert majoration label and textbox controls
            int lblX = 20, lblY = 60;
            int offsetX = 110, offsetY = 29;
            int tabIndex = bnCancel.TabIndex;
            int iCount = 0;
            for (int i = 1; i < 16; ++i)
            {
                string paramName = string.Format("m{0}", i);
                if (!stack.HasParameter(paramName))
                    continue;

                Label lbl = new Label();
                lbl.Name = string.Format("lbl_m{0}", i);
                lbl.Text = string.Format("m{0}", i);
                lbl.Location = new Point(
                    lblX + (iCount / 4) * offsetX
                    , lblY + (iCount % 4) * offsetY);
                lbl.Size = new Size(30, 13);
                lbl.TabIndex = ++tabIndex;
                this.Controls.Add(lbl);

                NumericUpDown nud = new NumericUpDown();
                nud.Name = string.Format("nud_m{0}", i);
                nud.Increment = 1.0M;
                nud.Minimum = -10000.0M;
                nud.Maximum = 10000.0M;
                nud.DecimalPlaces = 3;
                nud.Value = (decimal)stack.GetDoubleParameterValue(paramName);
                nud.Location = new Point(
                    lblX + (iCount / 4) * offsetX + lbl.Size.Width + 1
                    , lblY + (iCount % 4) * offsetY);
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
            PPDataContext db = new PPDataContext();
            Dictionary<string, double> dictMajo = Pic.DAL.SQLite.Component.GetDefaultMajorations(db, _componentId, _profile);
            foreach (Control ctrl in Controls)
            {
                NumericUpDown nud = ctrl as NumericUpDown;
                if ( null == nud || !nud.Name.StartsWith("nud_"))
                    continue;
                nud.Value = (decimal)dictMajo[nud.Name.Substring(4)];
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
        #endregion

        #region Data members
        private int _componentId;
        private CardboardProfile _profile;
        private bool _dirty;
        private ProfileLoader _profileLoader;
        #endregion
    }
}