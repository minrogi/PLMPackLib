#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;

using Pic.DAL.SQLite;
using Pic.Plugin;
using Pic.Factory2D;
using DesLib4NET;
using GLib.Options;

using log4net;
using PicParam.Properties;

using TreeDim.StackBuilder.GUIExtension;
using MRU;
#endregion

namespace PicParam
{
    public partial class MainForm : Form, IMRUClient
    {
        #region Constructor
        public MainForm()
        {
            try
            {
                InitializeComponent();
                this.Text = Application.ProductName;

                _startPageCtrl.TreeViewCtrl = _treeViewCtrl;
                _downloadPageCtrl.TreeViewCtrl = _treeViewCtrl;

                // set export application
                ApplicationAvailabilityChecker.AppendApplication("PicGEOM", Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppDES);
                ApplicationAvailabilityChecker.AppendApplication("PicDecoupe", Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppPicDecoupeDES);
                ApplicationAvailabilityChecker.AppendApplication("Picador3D", Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppPic3DDES);

                _pluginViewCtrl.Localizer = LocalizerImpl.Instance;
                _pluginViewCtrl.DependancyStatusChanged += new Pic.Plugin.ViewCtrl.PluginViewCtrl.DependancyStatusChangedHandler(DependancyStatusChanged);
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
        #endregion

        #region Overrides
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _splitContainer.SplitterDistance = 200;
            _downloadPageCtrl.Size = this._splitContainer.Panel2.Size;
        }

        /// <summary>
        /// handle Ctrl+Z key
        /// </summary>
        /// <returns>true if key is correctly handled</returns> 
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Z:
                    if (_factoryViewCtrl.Visible) _factoryViewCtrl.FitView();
                    if (_pluginViewCtrl.Visible) _pluginViewCtrl.FitView();
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        #endregion

        #region Control event handlers
        private void BranchViewBranchSelected(object sender, NodeEventArgs e)
        {
            // get selected tree node
            _treeViewCtrl.PopulateAndSelectNode(new NodeTag(NodeTag.NodeType.NT_TREENODE, e.Node));
        }

        private void LoadParametricComponent(string filePath)
        {
            _pluginViewCtrl.Visible = true;

            _pluginViewCtrl.PluginPath = filePath;

            toolStripButtonCotations.Enabled = true;
            toolStripButtonReflectionX.Enabled = true;
            toolStripButtonReflectionY.Enabled = true;
            toolStripButtonExport.Enabled = true;
            exportToolStripMenuItem.Enabled = true;
            toolStripMenuItemCotations.Enabled = true;
        }

        private void LoadPicadorFile(string filePath, string fileFormat)
        {
            _factoryViewCtrl.Visible = true;

            PicFactory factory = _factoryViewCtrl.Factory;

            if (string.Equals("des", fileFormat, StringComparison.CurrentCultureIgnoreCase))
            {
                PicLoaderDes picLoaderDes = new PicLoaderDes(factory);
                using (DES_FileReader fileReader = new DES_FileReader())
                    fileReader.ReadFile(filePath, picLoaderDes);
                // remove existing quotations
                factory.Remove((new PicFilterCode(PicEntity.eCode.PE_COTATIONDISTANCE))
                                    | (new PicFilterCode(PicEntity.eCode.PE_COTATIONHORIZONTAL))
                                    | (new PicFilterCode(PicEntity.eCode.PE_COTATIONVERTICAL)));
                // build autoquotation
                PicAutoQuotation.BuildQuotation(factory);
            }
            else if (string.Equals("dxf", fileFormat, StringComparison.CurrentCultureIgnoreCase))
            {
                using (PicLoaderDxf picLoaderDxf = new PicLoaderDxf(factory))
                {
                    picLoaderDxf.Load(filePath);
                    picLoaderDxf.FillFactory();
                }
            }
            // update _factoryViewCtrl
            _factoryViewCtrl.FitView();
        }

        private void LoadPdfWithActiveX(string filePath)
        {
            _webBrowser4PDF.Url = new Uri(filePath);
            _webBrowser4PDF.Size = this._splitContainer.Panel2.Size;
            _webBrowser4PDF.Visible = true;
            // show export button
            toolStripButtonExport.Visible = true;
        }

        private void LoadImageFile(string filePath)
        {
            _webBrowser4PDF.Url = new Uri(filePath);
            _webBrowser4PDF.Size = this._splitContainer.Panel2.Size;
            _webBrowser4PDF.Visible = true;
        }

        private void LoadUnknownFileFormat(string filePath)
        {
            // build new file path
            string filePathCopy = Path.Combine(Path.GetTempPath(), Path.GetFileName(filePath));
            // copy file
            System.IO.File.Copy(filePath, filePathCopy, true);
            // open using shell execute 'Open'
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.Verb = "Open";
            startInfo.CreateNoWindow = false;
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.FileName = filePath;
            using (Process p = new Process())
            {
                p.StartInfo = startInfo;
                p.Start();
            }
        }

        Pic.DAL.SQLite.Document GetSelectedDocument()
        {
            NodeTag nodeTag = _treeViewCtrl.SelectedNode.Tag as NodeTag;
            if (null == nodeTag)
                return null;

            PPDataContext db = new PPDataContext();
            Pic.DAL.SQLite.TreeNode treeNode = Pic.DAL.SQLite.TreeNode.GetById(db, nodeTag.TreeNode);
            if (null == treeNode)
                return null;    // failed to retrieve valid document
            if (!treeNode.IsDocument)
                return null;    // not a document
            return treeNode.Documents(db)[0];
        }

        void DependancyStatusChanged(bool hasDependancy)
        {
            toolStripButtonEditParameters.Enabled = hasDependancy && _pluginViewCtrl.Visible;
        }

        private void UpdateTextPosition(object sender, EventArgs e)
        {
            // exit when in design mode
            if ((this.DesignMode)||!(Settings.Default.ShowCenteredTitle)) return;
            // measure text length and compute starting point of text
            Graphics g = this.CreateGraphics();
            double startingPoint = (this.Width / 2) - (g.MeasureString(this.Text.Trim(), this.Font).Width / 2);
            double widthOfASpace = g.MeasureString(" ", this.Font).Width;
            string tmp = " ";
            double tmpWidth = 0;

            // loop adds space characters until researched text starting point is reached
            int iTextLength = this.Text.Length;
            while ((tmpWidth + widthOfASpace) < startingPoint)
            {
                if (tmp.Length < 255 - iTextLength)
                    tmp += " ";
                tmpWidth += widthOfASpace;
            }
            this.Text = tmp + this.Text.Trim();
        }

        #endregion

        #region Menu event handlers
        #region File
        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void toolStripMenuItemBrowseFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.Filter = "Picador file|*.des|Autocad dxf|*.dxf|All Files|*.*";
                if (DialogResult.OK == fd.ShowDialog())
                {
                    FormBrowseFile form = new FormBrowseFile(fd.FileName);
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
				Debug.Fail(ex.ToString());
				_log.Error(ex.ToString());                
            }
        }
        #endregion
        #region Help
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAboutBox form = new FormAboutBox();
            form.ShowDialog();
        }
        #endregion
        #region Database
        /// <summary>
        /// Backup
        /// </summary>
        private void toolStripMenuItemBackup_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == saveFileDialogBackup.ShowDialog())
                FormWorkerThreadTask.Execute(new Pic.DAL.TPTBackup(saveFileDialogBackup.FileName));
        }
        /// <summary>
        /// Restore
        /// </summary>
        private void toolStripMenuItemRestore_Click(object sender, EventArgs e)
        {
            // warning
            if (MessageBox.Show(PicParam.Properties.Resources.ID_RESTOREWARNING
                , ProductName
                , MessageBoxButtons.OKCancel
                , MessageBoxIcon.Warning
                ) == DialogResult.Cancel)
                return;
            if (DialogResult.OK == openFileDialogRestore.ShowDialog())
            {
                FormWorkerThreadTask.Execute(new Pic.DAL.TPTRestore(openFileDialogRestore.FileName));
                // refresh tree
                _treeViewCtrl.RefreshTree();
                // expand all so that modifications can be visualized
                if (_treeViewCtrl.Nodes.Count > 0)
                    _treeViewCtrl.Nodes[0].ExpandAll();
            }
        }
        /// <summary>
        /// Merge
        /// </summary>
        private void toolStripMenuItemMerge_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialogRestore.ShowDialog())
            {
                FormWorkerThreadTask.Execute(new Pic.DAL.TPTMerge(openFileDialogRestore.FileName));
                // refresh tree
                _treeViewCtrl.RefreshTree();
                // back to default cursor
                Cursor.Current = Cursors.Default;
            }
        }
        /// <summary>
        /// Clear
        /// </summary>
        private void toolStripMenuItemClearDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                // form
                if (DialogResult.Yes == MessageBox.Show(PicParam.Properties.Resources.ID_CLEARDATABASEWARNING,
                    ProductName,
                    MessageBoxButtons.YesNo))
                {   // answered 'Yes' 
                    // start thread
                    FormWorkerThreadTask.Execute(new Pic.DAL.TPTClearDatabase());
                    // refresh tree
                    _treeViewCtrl.RefreshTree();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(ex.Message));
            }
        }

        private void toolStripMenuItemDefineDatabasePath_Click(object sender, EventArgs e)
        {
            try
            {
                // first check
                if (!Helpers.IsUserAdministrator)
                {
                    MessageBox.Show(PicParam.Properties.Resources.ID_INFO_RUNASADMIN, Application.ProductName, MessageBoxButtons.OK);
                    return;
                }
                // edit database path
                FormEditDatabasePath form = new FormEditDatabasePath();
                form.ShowDialog();
            }
            catch (Exception ex)
            {   _log.Error(ex.ToString());  }
        }
        #endregion
        #region Tools
        private void toolStripMenuItemEditProfiles_Click(object sender, EventArgs e)
        {
            FormEditProfiles form = new FormEditProfiles();
            form.ShowDialog();
        }
        private void toolStripMenuItemCustomize_Click(object sender, EventArgs e)
        {
            try
            {
                // show option form
                OptionsFormPLMPackLib form = new OptionsFormPLMPackLib();
                DialogResult dres = form.ShowDialog();
                if (DialogResult.OK == dres)
                {
                    // need to force saving of Pic.Factory2D.Properties.Settings
                    Pic.Factory2D.Control.Properties.Settings.Default.Save();
                    // need to force saving of Pic.Factory2D.Properties.Settings
                    Pic.Factory2D.Control.Properties.Settings.Default.Save();
                    // need to force saving of Pic.Plugin.ViewCtrl.Properties.Settings
                    Pic.Plugin.ViewCtrl.Properties.Settings.Default.Save();
                    // if need to restart application, indicate the user that the application will need to restart before exiting
                    if (form.ApplicationMustRestart)
                    {
                        MessageBox.Show(string.Format(Properties.Resources.ID_APPLICATIONMUSTRESTART, Application.ProductName));
                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
                _log.Error(ex.ToString());
            }
        }
        void toolStripMenuItemEditCardboardFormats_Click(object sender, System.EventArgs e)
        {
            try
            {
                FormEditCardboardFormats form = new FormEditCardboardFormats();
                if (DialogResult.OK == form.ShowDialog())
                { }
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
                _log.Error(ex.ToString());
            }
        }
        #endregion
        #endregion

        #region IMRUClient
        public void OpenMRUFile(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
                mruManager.Remove(fileName);
                return;
            }
            if (Path.Equals(Pic.DAL.ApplicationConfiguration.CustomSection.DatabasePath, fileName))
                return;

            // warn user that database path will be changed
            MessageBox.Show(string.Format(PicParam.Properties.Resources.ID_APPLICATIONEXITING, Application.ProductName));
            // change database
            Pic.DAL.ApplicationConfiguration.SaveDatabasePath(fileName);
            // close and restart application
            Application.Restart();
            Application.ExitThread();
        }
        #endregion

        #region Export toolbar event handler
        void toolStripButtonExport_Click(object sender, System.EventArgs e)
        {
            try
            {
                // #### actually exported file from _pluginViewCtrl or _factoryViewCtrl
                if (_pluginViewCtrl.Visible || _factoryViewCtrl.Visible)
                {
                    FormExportFile form = new FormExportFile();
                    form.FileName = DocumentName;
                    if (DialogResult.OK == form.ShowDialog())
                    {
                        if (_pluginViewCtrl.Visible)
                            _pluginViewCtrl.WriteExportFile(form.FilePath, form.ActualFileExtension);
                        else if (_factoryViewCtrl.Visible)
                        {
                            // at first, attempt to copy original file
                            // rather than exporting _factoryViewCtrl content
                            if ("des" == form.ActualFileExtension && System.IO.File.Exists(DocumentPath))
                                System.IO.File.Copy(DocumentPath, form.FilePath, true);
                            else
                                _factoryViewCtrl.WriteExportFile(form.FilePath, form.ActualFileExtension);
                        }
                        else if (_webBrowser4PDF.Visible)
                        {
                            if (null != _treeViewCtrl.SelectedNode)
                            {
                            }
                        }
                        if (form.OpenFile)
                        {
                            using (System.Diagnostics.Process proc = new System.Diagnostics.Process())
                            {
                                if ("des" == form.ActualFileExtension
                                    || "dxf" == form.ActualFileExtension
                                    || "ai" == form.ActualFileExtension
                                    || "cf2" == form.ActualFileExtension)
                                {
                                    string applicationPath = string.Empty;
                                    switch (form.ActualFileExtension)
                                    {
                                        case "des": applicationPath = Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppDES; break;
                                        case "dxf": applicationPath = Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppDXF; break;
                                        case "ai": applicationPath = Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppAI; break;
                                        case "cf2": applicationPath = Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppCF2; break;
                                        default: break;
                                    }
                                    if (string.IsNullOrEmpty(applicationPath) || !System.IO.File.Exists(applicationPath))
                                        return;
                                    proc.StartInfo.FileName = applicationPath;
                                    proc.StartInfo.Arguments = "\"" + form.FilePath + "\"";
                                }
                                else if ("pdf" == form.ActualFileExtension)
                                {
                                    proc.StartInfo.FileName = form.FilePath;
                                    // actually using shell execute
                                    proc.StartInfo.UseShellExecute = true;
                                    proc.StartInfo.Verb = "open";
                                }
                                // checking if called application can be found
                                if (!proc.StartInfo.UseShellExecute && !System.IO.File.Exists(proc.StartInfo.FileName))
                                {
                                    MessageBox.Show(string.Format("Application {0} could not be found!"
                                        , proc.StartInfo.FileName)
                                        , Application.ProductName
                                        , MessageBoxButtons.OK
                                        , MessageBoxIcon.Error);
                                    return;
                                }
                                proc.Start();
                            }
                        }
                    }
                }
                // #### actually PDF file
                else if (_webBrowser4PDF.Visible)
                {
                    SaveFileDialog fd = new SaveFileDialog();
                    fd.Filter = "Adobe Acrobat Reader (*.pdf)|*.pdf|All Files|*.*";
                    fd.FilterIndex = 0;
                    fd.DefaultExt = "pdf";
                    fd.InitialDirectory = Settings.Default.FileExportDirectory;
                    if (DialogResult.OK == fd.ShowDialog())
                    {
                        string filePath = _webBrowser4PDF.Url.AbsolutePath;
                        System.IO.File.Copy(filePath, fd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
                _log.Error(ex.ToString());
            }
        }
        private void toolStripButtonPicGEOM_Click(object sender, EventArgs e)
        {
            ExportAndOpenExtension("des", Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppDES);
        }
        private void toolStripButtonPicDecoup_Click(object sender, EventArgs e)
        {
            ExportAndOpenExtension("des", Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppPicDecoupeDES);
        }
        private void toolStripButtonPicador3D_Click(object sender, EventArgs e) 
        {
            ExportAndOpenExtension("des", Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppPic3DDES);
        }
        private void toolStripButtonDXF_Click(object sender, EventArgs e)
        {
            string appDXF = Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppDXF;
            if (!string.IsNullOrEmpty(appDXF) && System.IO.File.Exists(appDXF))
                ExportAndOpenExtension("dxf", appDXF);
            else
            {
                // set default file path
                string filePath = Path.Combine(Settings.Default.FileExportDirectory, DocumentName);
                filePath = Path.ChangeExtension(filePath, "dxf");
                // initialize SaveFileDialog
                SaveFileDialog fd = new SaveFileDialog();
                fd.FileName = filePath;
                fd.Filter = "Autodesk dxf (*.dxf)|*.dxf|All Files|*.*";
                fd.FilterIndex = 0;
                // show save file dialog
                if (DialogResult.OK == fd.ShowDialog())
                    ExportAndOpen(fd.FileName, string.Empty);
                // save directory
                Settings.Default.FileExportDirectory = Path.GetDirectoryName(fd.FileName);
            }
        }
        private void toolStripButtonAI_Click(object sender, EventArgs e)
        {
            string appAI = Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppAI;
            if (!string.IsNullOrEmpty(appAI) && System.IO.File.Exists(appAI))
                ExportAndOpenExtension("ai", appAI);
            else
            {
                // set default file path
                string filePath = Path.Combine(Settings.Default.FileExportDirectory, DocumentName);
                filePath = Path.ChangeExtension(filePath, "ai");
                // initialize SaveFileDialog
                SaveFileDialog fd = new SaveFileDialog();
                fd.FileName = filePath;
                fd.Filter = "Adobe Illustrator (*.ai)|*.ai|All Files|*.*";
                fd.FilterIndex = 0;
                // show save file dialog
                if (DialogResult.OK == fd.ShowDialog())
                    ExportAndOpen(fd.FileName, string.Empty);
                // save directory
                Settings.Default.FileExportDirectory = Path.GetDirectoryName(fd.FileName);
            }
        }
        private void toolStripButtonCFF2_Click(object sender, EventArgs e)
        {
            string appCF2 = Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppCF2;
            if (!string.IsNullOrEmpty(appCF2) && System.IO.File.Exists(appCF2))
                ExportAndOpenExtension("ai", appCF2);
            else
            {
                // set default file path
                string filePath = Path.Combine(Settings.Default.FileExportDirectory, DocumentName);
                filePath = Path.ChangeExtension(filePath, "cf2");
                // initialize SaveFileDialog
                SaveFileDialog fd = new SaveFileDialog();
                fd.FileName = filePath;
                fd.Filter = "Common File Format (*.cf2)|*.cf2|All Files|*.*";
                fd.FilterIndex = 0;
                // show save file dialog
                if (DialogResult.OK == fd.ShowDialog())
                    ExportAndOpen(fd.FileName, string.Empty);
                // save directory
                Settings.Default.FileExportDirectory = Path.GetDirectoryName(fd.FileName);
            }
        }

        private void toolStripButtonOceProCut_Click(object sender, EventArgs e)
        {
            try
            {
                // set default file path
                string filePath = Path.Combine(Settings.Default.FileExportDirectory, DocumentName);
                filePath = Path.ChangeExtension(filePath, "ai");
                // initialize SaveFileDialog
                SaveFileDialog fd = new SaveFileDialog();
                fd.FileName = filePath;
                fd.Filter = "Adobe Illustrator (*.ai)|*.ai|All Files|*.*";
                fd.FilterIndex = 0;
                // show save file dialog
                if (DialogResult.OK == fd.ShowDialog())
                    ExportAndOpen(fd.FileName, string.Empty );
                // save directory
                Settings.Default.FileExportDirectory = Path.GetDirectoryName(fd.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExportAndOpen(string filePath, string sPathExectable)
        {
            // write file
            if (_pluginViewCtrl.Visible)
                _pluginViewCtrl.WriteExportFile(filePath, Path.GetExtension(filePath));
            else if (_factoryViewCtrl.Visible)
                _factoryViewCtrl.WriteExportFile(filePath, Path.GetExtension(filePath));
            // test if executing application is available
            if (!string.IsNullOrEmpty(sPathExectable) && System.IO.File.Exists(sPathExectable))
            {
                // open using existing file path
                using (System.Diagnostics.Process proc = new System.Diagnostics.Process())
                {
                    proc.StartInfo.FileName = sPathExectable;
                    proc.StartInfo.Arguments = "\"" + filePath + "\"";
                    proc.Start();
                }
            }
        }

        private void ExportAndOpenExtension(string sExt, string sPathExectable)
        {
            // build temp file path
            string tempFilePath = System.IO.Path.ChangeExtension(System.IO.Path.GetTempFileName(), sExt);
            ExportAndOpen(tempFilePath, sPathExectable);
        }
        #endregion

        #region Toolbar event handlers
        void toolStripButtonCotations_Click(object sender, System.EventArgs e)
        {
            if (_pluginViewCtrl.Visible)
                _pluginViewCtrl.ShowCotations = !_pluginViewCtrl.ShowCotations;
            else if (_factoryViewCtrl.Visible)
                _factoryViewCtrl.ShowCotations = !_factoryViewCtrl.ShowCotations;
            UpdateToolCommands();
        }
        private void toolStripButtonReflectionX_Click(object sender, EventArgs e)
        {
            if (_pluginViewCtrl.Visible)
                _pluginViewCtrl.ReflectionX = !_pluginViewCtrl.ReflectionX;
            else if (_factoryViewCtrl.Visible)
                _factoryViewCtrl.ReflectionX = !_factoryViewCtrl.ReflectionX;
            UpdateToolCommands();
        }
        private void toolStripButtonReflectionY_Click(object sender, EventArgs e)
        {
            if (_pluginViewCtrl.Visible)
                _pluginViewCtrl.ReflectionY = !_pluginViewCtrl.ReflectionY;
            else if (_factoryViewCtrl.Visible)
                _factoryViewCtrl.ReflectionY = !_factoryViewCtrl.ReflectionY;
            UpdateToolCommands();
        }
         private void toolStripButtonLayout_Click(object sender, EventArgs e)
        {
            try
            {
                if (_pluginViewCtrl.Visible)
                    _pluginViewCtrl.BuildLayout();
                else if (_factoryViewCtrl.Visible)
                    _factoryViewCtrl.BuildLayout();
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
                _log.Error(ex.ToString());
            }
        }
        private void UpdateToolCommands()
        {
            try
            {
                // enable toolbar buttons
                bool buttonsEnabled = _pluginViewCtrl.Visible || _factoryViewCtrl.Visible;
                toolStripButtonCotations.Enabled = buttonsEnabled;
                toolStripMenuItemCotations.Enabled = buttonsEnabled;
                toolStripButtonReflectionX.Enabled = buttonsEnabled;
                reflectionXToolStripMenuItem.Enabled = buttonsEnabled;
                toolStripButtonReflectionY.Enabled = buttonsEnabled;
                reflectionYToolStripMenuItem.Enabled = buttonsEnabled;
                toolStripButtonLayout.Enabled = buttonsEnabled;
                layoutToolStripMenuItem.Enabled = buttonsEnabled;
                toolStripMenuItemCotationShortLines.Enabled = buttonsEnabled;
                // only allow palletization / case optimisation when a component is selected
                toolStripButtonPalletization.Enabled = _pluginViewCtrl.Visible && _pluginViewCtrl.AllowPalletization;
                toolStripButtonCaseOptimization.Enabled = _pluginViewCtrl.Visible && _pluginViewCtrl.AllowPalletization;
                // enable export toolbar buttons
                toolStripButtonExport.Enabled = buttonsEnabled || _webBrowser4PDF.Visible;
                toolStripButtonPicGEOM.Enabled = buttonsEnabled && ApplicationAvailabilityChecker.IsAvailable("PicGEOM");
                toolStripButtonPicDecoup.Enabled = buttonsEnabled && ApplicationAvailabilityChecker.IsAvailable("PicDecoup");
                toolStripButtonPicador3D.Enabled = buttonsEnabled && ApplicationAvailabilityChecker.IsAvailable("Picador3D");
                toolStripButtonOceProCut.Enabled = buttonsEnabled;
                toolStripButtonDXF.Enabled = buttonsEnabled;
                toolStripButtonAI.Enabled = buttonsEnabled;
                toolStripButtonCFF2.Enabled = buttonsEnabled;
                // "File" menu items
                exportToolStripMenuItem.Enabled = buttonsEnabled;
                toolStripMenuItemPicGEOM.Enabled = buttonsEnabled && ApplicationAvailabilityChecker.IsAvailable("PicGEOM");

                // check state
                bool showCotations = false, reflectionX = false, reflectionY = false;
                if (_pluginViewCtrl.Visible)
                {
                    showCotations = _pluginViewCtrl.ShowCotations;
                    reflectionX = _pluginViewCtrl.ReflectionX;
                    reflectionY = _pluginViewCtrl.ReflectionY;
                }
                else if (_factoryViewCtrl.Visible)
                {
                    showCotations = _factoryViewCtrl.ShowCotations;
                    reflectionX = _factoryViewCtrl.ReflectionX;
                    reflectionY = _factoryViewCtrl.ReflectionY;
                }

                toolStripButtonCotations.CheckState = showCotations ? CheckState.Checked : CheckState.Unchecked;
                toolStripMenuItemCotations.CheckState = showCotations ? CheckState.Checked : CheckState.Unchecked;
                toolStripButtonReflectionX.CheckState = reflectionX ? CheckState.Checked : CheckState.Unchecked;
                reflectionXToolStripMenuItem.CheckState = reflectionX ? CheckState.Checked : CheckState.Unchecked;
                toolStripButtonReflectionY.CheckState = reflectionY ? CheckState.Checked : CheckState.Unchecked;
                reflectionYToolStripMenuItem.CheckState = reflectionY ? CheckState.Checked : CheckState.Unchecked;
                toolStripEditComponentCode.Enabled = _pluginViewCtrl.Visible;

                toolStripButtonEditParameters.Enabled = _pluginViewCtrl.Visible && _pluginViewCtrl.HasDependancies;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
        private void toolStripEditComponentCode_Click(object sender, EventArgs e)
        {
            try
            {
                NodeTag nodeTag = _treeViewCtrl.SelectedNode.Tag as NodeTag;
                if (null == nodeTag) return;
                PPDataContext db = new PPDataContext();
                Pic.DAL.SQLite.TreeNode treeNode = Pic.DAL.SQLite.TreeNode.GetById(db, nodeTag.TreeNode);
                if (null == treeNode) return;
                Pic.DAL.SQLite.Document doc = treeNode.Documents(db)[0];
                if (null == doc) return;
                Pic.DAL.SQLite.Component comp = doc.Components[0];
                if (null == comp) return;
                // output Guid / path
                Guid outputGuid = Guid.NewGuid();
                string outputPath = Pic.DAL.SQLite.File.GuidToPath(db, outputGuid, "dll");
                // form plugin editor
                FormPluginEditor editorForm = new FormPluginEditor();
                editorForm.PluginPath = doc.File.Path(db);
                editorForm.OutputPath = outputPath;
                if (DialogResult.OK == editorForm.ShowDialog())
                {
                    _log.Info("Component successfully modified!");
                    doc.File.Guid = outputGuid;
                    db.SubmitChanges();
                    // clear component cache in plugin viewer
                    ComponentLoader.ClearCache();
                    // update pluginviewer
                    _pluginViewCtrl.PluginPath = outputPath;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }

        private void toolStripButtonEditParameters_Click(object sender, EventArgs e)
        {
            try
            {
                FormEditDefaultParamValues form = new FormEditDefaultParamValues( _pluginViewCtrl.Dependancies );
                if (DialogResult.OK == form.ShowDialog())
                {   // also see on Ok button handler
                    // clear plugin loader cache
                    ComponentLoader.ClearCache();

                    if (_pluginViewCtrl.Visible)
                        _pluginViewCtrl.Refresh();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
        private void toolStripMenuItemCotationShortLines_Click(object sender, EventArgs e)
        {
            try
            {
                // update static flag
                PicGlobalCotationProperties.ShowShortCotationLines = !PicGlobalCotationProperties.ShowShortCotationLines;
                _log.Info(string.Format("Switched cotation short lines. New value : {0}", PicGlobalCotationProperties.ShowShortCotationLines.ToString()));
                // update menu
                toolStripMenuItemCotationShortLines.Checked = PicGlobalCotationProperties.ShowShortCotationLines;
                // save setting
                PicParam.Properties.Settings.Default.UseCotationShortLines = PicGlobalCotationProperties.ShowShortCotationLines;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
        private void toolStripButtonRoot_Click(object sender, EventArgs e)
        {
            try
            {
                _treeViewCtrl.CollapseRootChildrens();                
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
        /// <summary>
        /// Download
        /// </summary>
        private void toolStripButtonDownload_Click(object sender, EventArgs e)
        {
            try
            {
                _treeViewCtrl.SelectedNode = _treeViewCtrl.Nodes[1];
            }
            catch (Exception ex)
            {    _log.Error(ex.ToString());   }
        }
        /// <summary>
        /// Search
        /// </summary>
        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FormSearch form = new FormSearch();
                if (DialogResult.OK == form.ShowDialog())
                {
                     // show selected node
                    _treeViewCtrl.PopulateAndSelectNode(new NodeTag(NodeTag.NodeType.NT_TREENODE, form.ResultNodeId));
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
        private void toolStripButtonStartPageWeb_Click(object sender, EventArgs e)
        {
            try
            {
                _treeViewCtrl.SelectedNode = _treeViewCtrl.Nodes[0];
            }
            catch (Exception ex)
            { _log.Error(ex.ToString()); }
        }
        /// <summary>
        /// Help
        /// </summary>
        private void toolStripButtonHelp_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
        #endregion

        #region Palletization toolbar event handlers
        private void toolStripButtonPalletization_Click(object sender, EventArgs e)
        {
            try
            {
                double length = 0.0, width = 0.0, height = 0.0;
                if (_pluginViewCtrl.GetDimensions(ref length, ref width, ref height))
                {
                    TreeDim.StackBuilder.GUIExtension.Palletization palletization = new Palletization();
                    palletization.StartPalletization(_pluginViewCtrl.LoadedComponentName, length, width, height);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }

        private void toolStripButtonCaseOptimization_Click(object sender, EventArgs e)
        {
            try
            {
                double length = 0.0, width = 0.0, height = 0.0;
                if (_pluginViewCtrl.GetDimensions(ref length, ref width, ref height))
                {
                    TreeDim.StackBuilder.GUIExtension.Palletization palletization = new Palletization();
                    //palletization.StartCaseOptimization(_pluginViewCtrl.LoadedComponentName, length, width, height);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }            
        }
        #endregion

        #region Private properties
        private string DocumentName
        {
            get { return _docName; }
            set { _docName = value; }
        }
        private string DocumentPath
        {
            get { return _docPath; }
            set { _docPath = value; }
        }
        #endregion

        #region MainForm Load/Close event handling
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // load settings only if not in debug mode
                if (!Properties.Settings.Default.DebugMode)
                     ToolStripManager.LoadSettings(this, this.Name);
                toolStripDebug.Visible = Properties.Settings.Default.DebugMode;

                // --- instantiate and start splach screen thread
                // cardboard format loader
                CardboardFormatLoader formatLoader = new CardboardFormatLoaderImpl();
                _pluginViewCtrl.CardboardFormatLoader = formatLoader;
                _factoryViewCtrl.CardBoardFormatLoader = formatLoader;

                // profile loader
                _profileLoaderImpl = new ProfileLoaderImpl();
                _pluginViewCtrl.ProfileLoader = _profileLoaderImpl;
                // search method
                ComponentSearchMethodDB searchmethod = new ComponentSearchMethodDB();
                _pluginViewCtrl.SearchMethod = new ComponentSearchMethodDB();

                _treeViewCtrl.StartPageSelected += new DocumentTreeView.StartPageSelectHandler(ShowStartPage);
                _treeViewCtrl.DownloadPageSelected += new DocumentTreeView.DownloadPageSelectHandler(ShowDownloadPage); 

                _treeViewCtrl.SelectionChanged += new DocumentTreeView.SelectionChangedHandler(_branchViewCtrl.OnSelectionChanged);
                _treeViewCtrl.SelectionChanged += new DocumentTreeView.SelectionChangedHandler(OnSelectionChanged);

                _branchViewCtrl.SelectionChanged += new DocumentTreeBranchView.SelectionChangedHandler(_treeViewCtrl.OnSelectionChanged);
                _branchViewCtrl.SelectionChanged += new DocumentTreeBranchView.SelectionChangedHandler(OnSelectionChanged);
                _branchViewCtrl.TreeNodeCreated += new DocumentTreeBranchView.TreeNodeCreatedHandler(_treeViewCtrl.OnTreeNodeCreated);

                // ---
                // initialize menu state
                PicGlobalCotationProperties.ShowShortCotationLines = PicParam.Properties.Settings.Default.UseCotationShortLines;
                _log.Info(string.Format("ShowShortCotationLines initialized with value : {0}", PicParam.Properties.Settings.Default.UseCotationShortLines.ToString()));
                toolStripMenuItemCotationShortLines.Checked = PicGlobalCotationProperties.ShowShortCotationLines;

                // show start page
                ShowStartPage(this);
                // update tool bars
                UpdateToolCommands();

                // Most recently used databases
                mruManager = new MRUManager();
                mruManager.Initialize(
                    this,                              // owner form
                    databaseToolStripMenuItem,         // Recent Files menu item
                    mnuFileMRU,                        // Recent Files menu item
                    "Software\\treeDiM\\PLMPackLib");  // Registry path to keep MRU list

                mruManager.Add(Pic.DAL.ApplicationConfiguration.CustomSection.DatabasePath);
            }
            catch (System.Exception ex)
            {
                Debug.Fail(ex.ToString());
                _log.Error(ex.ToString());
            }
            // restore window position
            if (null != Settings.Default.MainFormSettings && !Settings.Default.StartMaximized)
            {
                Settings.Default.MainFormSettings.Restore(this);
            }
            // show maximized
            if (Settings.Default.StartMaximized)
                WindowState = FormWindowState.Maximized;
        }

        private void OnSelectionChanged(object sender, NodeEventArgs e, string name)
        {

            // changed caption
            Text = Application.ProductName + " - " + name;
            UpdateTextPosition(null, null);

            // show/hide controls
            _startPageCtrl.Visible      = false;
            _downloadPageCtrl.Visible   = false;
            _branchViewCtrl.Visible     = (NodeTag.NodeType.NT_TREENODE == e.Type);
            _pluginViewCtrl.Visible     = false;
            _factoryViewCtrl.Visible    = false;
            _webBrowser4PDF.Visible     = false;
            if (NodeTag.NodeType.NT_DOCUMENT == e.Type)
            {
                PPDataContext db = new PPDataContext();
                Pic.DAL.SQLite.TreeNode treeNode = Pic.DAL.SQLite.TreeNode.GetById(db, e.Node);
                Document doc = treeNode.Documents(db)[0];

                DocumentName = doc.Name;

                // select document handler depending on document type
                string docTypeName = doc.DocumentType.Name;
                string filePath = doc.File.Path(db);

                DocumentPath = filePath;

                if (string.Equals("Parametric Component", docTypeName, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (doc.Components.Count > 0)
                        _profileLoaderImpl.Component = doc.Components[0];
                    LoadParametricComponent(filePath);
                }
                else if (string.Equals("treeDim des", docTypeName, StringComparison.CurrentCultureIgnoreCase))
                    LoadPicadorFile(filePath, "des");
                else if (string.Equals("autodesk dxf", docTypeName, StringComparison.CurrentCultureIgnoreCase))
                    LoadPicadorFile(filePath, "dxf");
                else if (string.Equals("Adobe Acrobat", docTypeName, StringComparison.CurrentCultureIgnoreCase))
                    LoadPdfWithActiveX(filePath);
                else if (string.Equals("raster image", docTypeName, StringComparison.CurrentCultureIgnoreCase))
                    LoadImageFile(filePath);
                else
                    LoadUnknownFileFormat(filePath);
            }
            // update toolbar
            UpdateToolCommands();
            // select treeview control
            _treeViewCtrl.Select();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // save toolstrip settings
            ToolStripManager.SaveSettings(this, this.Name);
            // do not save window position if StartMaximized property is set
            if (Settings.Default.StartMaximized) return;
            // save window position
            if (null == Settings.Default.MainFormSettings)
            {
                Settings.Default.MainFormSettings = new WindowSettings();
            }
            Settings.Default.MainFormSettings.Record(this);
            Settings.Default.Save();
        }
        #endregion

        #region Start page related methods / properties
        public bool IsWebSiteReachable
        {
            get
            {
                try
                {
                    System.Uri uri = new System.Uri(Settings.Default.StartPageUrl);
                    System.Net.IPHostEntry objIPHE = System.Net.Dns.GetHostEntry(uri.DnsSafeHost);
                    return true;
                }
                catch (Exception ex)
                {
                    _log.Error(ex.ToString());
                    return false;
                }
            }
        }
        private void ShowStartPage(object sender)
        {
            if (!IsWebSiteReachable)
                return;
            _startPageCtrl.Url = new Uri(Properties.Settings.Default.StartPageUrl);
            _startPageCtrl.Visible = true;
            _branchViewCtrl.Visible = false;
            _pluginViewCtrl.Visible = false;
            _factoryViewCtrl.Visible = false;
            _webBrowser4PDF.Visible = false;
        }
        private void ShowDownloadPage(object sender)
        {
            if (!IsWebSiteReachable)
                return;
            _downloadPageCtrl.Visible = true;
            _startPageCtrl.Visible = false;
            _branchViewCtrl.Visible = false;
            _pluginViewCtrl.Visible = false;
            _factoryViewCtrl.Visible = false;
            _webBrowser4PDF.Visible = false;
        }
        #endregion

        #region Helpers
        public void UpdateDocumentView()
        {
            if (_pluginViewCtrl.Visible)
                _pluginViewCtrl.Refresh();
        }
        #endregion

        #region Debug tools
        private void toolStripEditDLL_Click(object sender, EventArgs e)
        {
            // open file dialog
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Component (*.dll)|*.dll|All Files|*.*";
            fd.FilterIndex = 0;
            if (DialogResult.OK == fd.ShowDialog())
            {
                // make a copy
                string filePathCopy = Path.ChangeExtension(Path.GetTempFileName(), "dll");
                System.IO.File.Copy(fd.FileName, filePathCopy, true);
                // form plugin editor
                FormPluginEditor editorForm = new FormPluginEditor();
                editorForm.PluginPath = filePathCopy;
                editorForm.OutputPath = fd.FileName;
                if (DialogResult.OK == editorForm.ShowDialog()) {}
                // try and delete copy file
                try { System.IO.File.Delete(filePathCopy); }
                catch (Exception /*ex*/) {}
            }
        }
        #endregion

        #region Data members
        protected static readonly ILog _log = LogManager.GetLogger(typeof(MainForm));
        [NonSerialized]protected ProfileLoaderImpl _profileLoaderImpl;
        /// <summary>
        /// current document name
        /// </summary>
        protected string _docName;
        /// <summary>
        /// cached document path
        /// </summary>
        protected string _docPath;
        /// <summary>
        /// Most Recently Used (database) manager
        /// </summary>
        private MRUManager mruManager;
        #endregion
    }
}