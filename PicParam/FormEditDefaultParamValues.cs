#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using log4net;
using PicParam.Properties;
#endregion

namespace PicParam
{
    #region FormEditDefaultParamValues
    public partial class FormEditDefaultParamValues : Form
    {
        #region Constructor
        public FormEditDefaultParamValues(List<Guid> dependanciesGUID)
        {
            InitializeComponent();

            // fill combo
            Pic.DAL.SQLite.PPDataContext db = new Pic.DAL.SQLite.PPDataContext();
            // load component from Guids
            foreach (Guid guid in dependanciesGUID)
            {
                Pic.DAL.SQLite.Component cp = Pic.DAL.SQLite.Component.GetByGuid(db, guid);
                cbComponent.Items.Add(new CompComboItem(cp.Document.Name, cp.Guid));
            }
            // initialize _pluginViewCtrl
            _pluginViewCtrl.SearchMethod = new ComponentSearchMethodDB();
            _pluginViewCtrl.ShowSummary = false;
            // initialize combo
            if (cbComponent.Items.Count > 0)
                cbComponent.SelectedIndex = 0;
        }
        #endregion

        #region Event handlers
        private void cbComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            // save parameters
            SaveParameters();
            // get selected component guid
            CompComboItem item = cbComponent.SelectedItem as CompComboItem;
            if (null == item) return;
            // change guid
            _guid = item.Guid;
            // Update parameters
            UpdateParameters();
        }
        private void bnOk_Click(object sender, EventArgs e)
        {
            SaveParameters();
        }
        #endregion

        #region Updating / saving parameters
        private void UpdateParameters()
        {
            // get selected component
            Pic.DAL.SQLite.PPDataContext db = new Pic.DAL.SQLite.PPDataContext();
            Pic.DAL.SQLite.Component comp = Pic.DAL.SQLite.Component.GetByGuid(db, _guid);
            // set description
            lbDescription.Text = comp.Document.Description;
            // initialize plugin viewer
            _pluginViewCtrl.PluginPath = comp.Document.File.Path(db);
            _pluginViewCtrl.ParamValues = Pic.DAL.SQLite.Component.GetParamDefaultValues(db, _guid);
        }

        private void SaveParameters()
        {
            // data context
            Pic.DAL.SQLite.PPDataContext db = new Pic.DAL.SQLite.PPDataContext();
            // get component by guid
            Pic.DAL.SQLite.Component comp = Pic.DAL.SQLite.Component.GetByGuid(db, _guid);
            if (null == comp) return;
            // get user entered values
            comp.UpdateDefaultParamValueSet(db, _pluginViewCtrl.ParamValues);
        }
        #endregion

        #region Deprecated code
        private void DisplayParameters()
        {
            // build list of controls starting with lb_ and nud_
            List<Control> listControl = new List<Control>();
            foreach (Control ctrl in Controls)
                if (ctrl.Name.StartsWith("nud_") || ctrl.Name.StartsWith("lb_"))
                    listControl.Add(ctrl);
            // remove these controls
            foreach (Control ctrl in listControl)
                Controls.Remove(ctrl);
            listControl.Clear();
            // get selected combo item
            CompComboItem item = cbComponent.SelectedItem as CompComboItem;
            if (null == item) return;
            // get selected component parameters
            Pic.DAL.SQLite.PPDataContext db = new Pic.DAL.SQLite.PPDataContext();
            Dictionary<string, double> dict = Pic.DAL.SQLite.Component.GetParamDefaultValues(db, item.Guid);
            // create controls
            const int lblX = 10, nudX = 160, incY = 26;
            Size sizelb = new Size(150, 18), sizenud = new Size(70, 12);
            int posY = 10;
            int tabIndex = 0;

            foreach (KeyValuePair<string, double> param in dict)
            {
                Label lb = new Label();
                lb.Name = "lb_" + param.Key;
                lb.Text = param.Key;
                lb.Size = sizelb;
                lb.Location = new System.Drawing.Point(lblX, posY);
                lb.TabIndex = ++tabIndex;
                Controls.Add(lb);
                NumericUpDown nud = new NumericUpDown();
                nud.Name = "nud_" + param.Key;
                nud.Value = (decimal)param.Value;
                nud.Size = sizenud;
                nud.Location = new System.Drawing.Point(nudX, posY);
                nud.TabIndex = ++tabIndex;
                Controls.Add(nud);

                // increment Y
                posY += incY;
            }
        }
        #endregion

        #region Data members
        private Guid _guid;
        #endregion

        #region Form loading / closing
        private void FormEditDefaultParamValues_Load(object sender, EventArgs e)
        {
            if (null != Settings.Default.FormEditDefaultParamValueSettings)
                Settings.Default.FormEditDefaultParamValueSettings.Restore(this);
        }

        private void FormEditDefaultParamValues_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null == Settings.Default.MainFormSettings)
                Settings.Default.MainFormSettings = new WindowSettings();
            Settings.Default.MainFormSettings.Record(this);
            Settings.Default.Save();
        }
        #endregion
    }
    #endregion

    #region CompComboItem
    public class CompComboItem
    {
        #region Constructor
        public CompComboItem(string name, Guid guid)
        {
            _name = name;
            _guid = guid;
        }
        #endregion

        #region Public properties
        public string Name { get { return _name; } }
        public Guid Guid { get { return _guid; } }
        #endregion

        #region Object override
        public override string ToString() { return _name; }
        #endregion

        #region Data members
        private string _name;
        private Guid _guid;
        #endregion
    }
    #endregion
}
