#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Microsoft.Win32;
using Pic.Factory2D;

using PicParam.Properties;
#endregion

namespace PicParam
{
    public partial class FormExportFile : Form
    {
        #region Constructor
        public FormExportFile()
        {
            InitializeComponent();
            // set file format
            foreach (FileFormat ff in Pic.Plugin.ViewCtrl.PluginViewCtrl.GetExportFormatList())
                cbFileFormat.Items.Add(ff);
            // file extension
            foreach (System.Object o in cbFileFormat.Items)
            {
                FileFormat ff = (Pic.Factory2D.FileFormat)o;
                if (ff._fileExtension == Settings.Default.FileExportExtension)
                    cbFileFormat.SelectedItem = o;
            }
        }
        #endregion

        #region Helpers
        private void UpdateFilePath()
        {
            FileFormat ff = (FileFormat)cbFileFormat.SelectedItem;
            if (null != ff && !string.IsNullOrEmpty(fileSelectCtrl.FileName))
                fileSelectCtrl.FileName = Path.ChangeExtension(fileSelectCtrl.FileName, ff._fileExtension);
        }

        private void EnableDisableOk()
        {
            bnOk.Enabled = Directory.Exists(Path.GetDirectoryName(fileSelectCtrl.FileName));
        }

        private string FileExtension
        {
            get
            {
                FileFormat ff = cbFileFormat.SelectedItem as FileFormat;
                if (null == ff && cbFileFormat.Items.Count > 0)
                    ff = cbFileFormat.Items[0] as FileFormat;
                return ff._fileExtension;
            }
            set
            {
                // file extension
                foreach (System.Object o in cbFileFormat.Items)
                {
                    Pic.Factory2D.FileFormat ff = o as Pic.Factory2D.FileFormat;
                    if (ff._fileExtension == Settings.Default.FileExportExtension)
                        cbFileFormat.SelectedItem = o;
                }
            }
        }
        public string FileName
        {
            set { fileName = value; }
            get { return fileName; }
        }
        public string FilePath
        {
            get { return fileSelectCtrl.FileName; }
        }
        public string ActualFileExtension
        {
            get
            {
                string fileExtension = Path.GetExtension(fileSelectCtrl.FileName);
                return fileExtension.TrimStart('.').ToLower();
            }
        }
        public bool OpenFile
        {
            get { return checkBox_openFile.Checked; }
        }
        #endregion

        #region Form event handling
        private void FormExportFile_Load(object sender, EventArgs e)
        {
            checkBox_openFile.Checked = Settings.Default.FileExportOpen;
            FileExtension = Settings.Default.FileExportExtension;
            fileSelectCtrl.FileName = Path.Combine(Settings.Default.FileExportDirectory, fileName + "." + FileExtension);

            UpdateFilePath();
            EnableDisableOk();
        }
        private void FormExportFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.FileExportOpen = checkBox_openFile.Checked;
            Settings.Default.FileExportExtension = FileExtension;
            Settings.Default.FileExportDirectory = System.IO.Path.GetDirectoryName(fileSelectCtrl.FileName);
            Settings.Default.Save();
        }
        private void cbFileFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFilePath();
        }

        private void fileSelectCtrl_FileNameChanged(object sender, EventArgs e)
        {
            EnableDisableOk();
        }
        #endregion

        #region Data members
        private string fileName;
        #endregion
    }
}