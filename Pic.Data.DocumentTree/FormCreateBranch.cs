#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using TreeDim.UserControls;
#endregion

namespace Pic.Data
{
    public partial class FormCreateBranch : Form
    {
        public FormCreateBranch()
        {
            InitializeComponent();
        }

        private void FormCreateBranch_Load(object sender, EventArgs e)
        {
            textBoxName.TextChanged += new EventHandler(textBox_TextChanged);
            textBoxDescription.TextChanged += new EventHandler(textBox_TextChanged);

            // file select control
            fileSelectCtrl.Filter = "Image files (*.bmp;*.gif;*.jpg;*.png)|*.bmp;*.gif;*.jpg;*.png";
            fileSelectCtrl.Enabled = checkBoxImage.Checked;
            // disable Ok button
            bnOk.Enabled = false;
        }

        void textBox_TextChanged(object sender, EventArgs e)
        {
            bool bHasValidFile = !checkBoxImage.Checked || System.IO.File.Exists(fileSelectCtrl.FileName);
            bnOk.Enabled = bHasValidFile && textBoxName.Text.Length > 0 && textBoxDescription.Text.Length > 0;
        }

        private void checkBoxImage_CheckedChanged(object sender, EventArgs e)
        {
            fileSelectCtrl.Enabled = checkBoxImage.Checked;
        }

        public string BranchName { get { return textBoxName.Text; } }
        public string BranchDescription { get { return textBoxDescription.Text; } }
        public string BranchImage
        {
            get
            {
                if (checkBoxImage.Checked && fileSelectCtrl.IsValid)
                    return fileSelectCtrl.FileName;
                else
                {
                    string tempPath = Path.GetTempFileName();
                    string bmpFilePath = Path.ChangeExtension( tempPath, "bmp");
                    System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(GetType());
                    using (Bitmap bmp = new Bitmap(assembly.GetManifestResourceStream("Pic.Data.Resources.DEFAULTTHUMBNAIL.BMP")))
                    {
                        bmp.Save(bmpFilePath);
                    }
                    return bmpFilePath;
                }
            }
        }
    }
}