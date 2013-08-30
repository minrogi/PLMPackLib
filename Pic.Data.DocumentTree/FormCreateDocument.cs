#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Pic.Logging;
using Pic.Plugin;
#endregion

namespace Pic.Data
{
    public partial class FormCreateDocument : Form
    {
        #region Contructor
        public FormCreateDocument()
        {
            InitializeComponent();
        }
        #endregion

        #region Ui event handlers
        private void FormCreateDocument_Load(object sender, EventArgs e)
        {
            textBoxName.TextChanged += new EventHandler(textBox_TextChanged);
            textBoxDescription.TextChanged += new EventHandler(textBox_TextChanged);
            fileSelectCtrl.FileNameChanged +=new EventHandler(textBox_TextChanged);
            // file select control
            fileSelectCtrl.Filter = "Plugin file (*.dll)|*.dll";
            // initialize profile combo box
            comboBoxProfile.Items.Clear();
            List<CardboardProfile> listProfile = CardboardProfile.getAll();
            foreach (CardboardProfile profile in listProfile)
                comboBoxProfile.Items.Add(profile);
            if (comboBoxProfile.Items.Count > 0)
                comboBoxProfile.SelectedIndex = 0;
            // disable Ok button
            bnOk.Enabled = false;
        }

        void textBox_TextChanged(object sender, EventArgs e)
        {
            string filePath = fileSelectCtrl.FileName;
            string fileExt = System.IO.Path.GetExtension(filePath);
            bool successfullyLoaded = false;

            if (System.IO.File.Exists(filePath) && (fileExt == ".dll"))
            {
                // try and load plugin
                try
                {
                    // load component
                    Pic.Plugin.Component component = null;
                    using (Pic.Plugin.ComponentLoader loader = new ComponentLoader())
                    {
                        loader.SearchMethod = new ComponentSearchMethodDB();
                        component = loader.LoadComponent(filePath);
                    }
                    if (null == component)
                        return;
                    _componentGuid = component.Guid;

                    // generate image
                    Image image;
                    using (Pic.Plugin.Tools pluginTools = new Pic.Plugin.Tools(component, new ComponentSearchMethodDB()))
                    {
                        pluginTools.ShowCotations = false;
                        pluginTools.GenerateImage(pictureBoxFileView.Size, out image);
                    }
                    pictureBoxFileView.Image = image;

                    // get parameters
                    Pic.Plugin.ParameterStack stack = null;
                    stack = component.Parameters;
                    // fill name/description if empty
                    if (textBoxName.Text.Length == 0)
                        textBoxName.Text = component.Name;
                    if (textBoxDescription.Text.Length == 0)
                        textBoxDescription.Text = component.Description;

                    // insert majoration label and textbox controls
                    int lblX = 16, lblY = 41;
                    int offsetX = 100, offsetY = 29;
                    int tabIndex = comboBoxProfile.TabIndex;
                    groupBoxMajorations.Controls.Clear();
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
                        lbl.Size = new Size(24, 13);
                        lbl.TabIndex = ++tabIndex;
                        groupBoxMajorations.Controls.Add(lbl);

                        TextBox tb = new TextBox();
                        tb.Name = string.Format("tb_m{0}", i);
                        tb.Text = string.Format("{0:0.##}", stack.GetDoubleParameterValue(paramName));
                        tb.Location = new Point(
                            lblX + (iCount / 4) * offsetX + lbl.Size.Width + 1
                            , lblY + (iCount % 4) * offsetY);
                        tb.Size = new Size(50, 20);
                        tb.TabIndex = ++tabIndex;
                        groupBoxMajorations.Controls.Add(tb);

                        ++iCount;
                    }

                    // Ok button enabled!
                    successfullyLoaded = true;
                }
                catch (Exception ex)
                {
                    Logger.Write(ex.ToString(), Category.General, Priority.Highest);
                }
            }

            // enable Ok button
            bnOk.Enabled = (textBoxName.TextLength != 0 && textBoxDescription.TextLength != 0 && successfullyLoaded);
        }

        private void bnOk_Click(object sender, EventArgs e)
        {
            // save selected profile
            _profile = comboBoxProfile.SelectedItem as CardboardProfile;

            // save majorations
            _dictMajo = new Dictionary<string,double>();
            foreach (Control ctrl in groupBoxMajorations.Controls)
            {
                TextBox tb = ctrl as TextBox;
                if (null == tb) continue;
                if (tb.Name.Contains("tb_m"))
                    _dictMajo.Add(tb.Name.Substring(3), Convert.ToDouble(tb.Text));
            }
        }
        #endregion

        #region Result properties
        public string DocumentName
        {
            get { return textBoxName.Text; }
        }

        public string DocumentDescription
        {
            get { return textBoxDescription.Text; }
        }

        public string FilePath
        {
            get { return fileSelectCtrl.FileName; }
        }

        public Guid ComponentGuid
        {
            get { return _componentGuid; }
        }

        public Dictionary<string, double> Majorations
        {
            get { return _dictMajo; }
        }

        public CardboardProfile Profile
        {
            get { return _profile; }
        }
        #endregion

        #region Data members
        Dictionary<string, double> _dictMajo;
        CardboardProfile _profile;
        Guid _componentGuid;
        #endregion

    }
}