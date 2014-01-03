namespace PicParam
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripContainerDefault = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._treeViewCtrl = new PicParam.DocumentTreeView();
            this._webBrowser4PDF = new System.Windows.Forms.WebBrowser();
            this._branchViewCtrl = new PicParam.DocumentTreeBranchView();
            this._pluginViewCtrl = new Pic.Plugin.ViewCtrl.PluginViewCtrl();
            this._factoryViewCtrl = new Pic.Factory2D.Control.FactoryViewerBase();
            this._startPageCtrl = new PicParam.StartPageControl();
            this._downloadPageCtrl = new PicParam.DownloadPageControl();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPicGEOM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemBrowseFile = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cotationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cotationShortLinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.reflectionXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reflectionYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeDatabaseToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.clearDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.defineDatabasePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileMRU = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editprofilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editmaterialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.layoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutTreeDimPicParamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonRoot = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDownload = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCotations = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonReflectionX = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonReflectionY = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripEditComponentCode = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEditParameters = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonLayout = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonHelp = new System.Windows.Forms.ToolStripButton();
            this.toolStripExport = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonPicGEOM = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPicDecoup = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPicador3D = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDXF = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAI = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCFF2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOceProCut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCaseOptimization = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPalletization = new System.Windows.Forms.ToolStripButton();
            this.toolStripDebug = new System.Windows.Forms.ToolStrip();
            this.toolStripEditDLL = new System.Windows.Forms.ToolStripButton();
            this.openFileDialogRestore = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogBackup = new System.Windows.Forms.SaveFileDialog();
            this.toolStripContainerDefault.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainerDefault.ContentPanel.SuspendLayout();
            this.toolStripContainerDefault.TopToolStripPanel.SuspendLayout();
            this.toolStripContainerDefault.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pluginViewCtrl)).BeginInit();
            this._pluginViewCtrl.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.toolStripExport.SuspendLayout();
            this.toolStripDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainerDefault
            // 
            resources.ApplyResources(this.toolStripContainerDefault, "toolStripContainerDefault");
            // 
            // toolStripContainerDefault.BottomToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainerDefault.BottomToolStripPanel, "toolStripContainerDefault.BottomToolStripPanel");
            this.toolStripContainerDefault.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainerDefault.ContentPanel
            // 
            resources.ApplyResources(this.toolStripContainerDefault.ContentPanel, "toolStripContainerDefault.ContentPanel");
            this.toolStripContainerDefault.ContentPanel.Controls.Add(this._splitContainer);
            // 
            // toolStripContainerDefault.LeftToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainerDefault.LeftToolStripPanel, "toolStripContainerDefault.LeftToolStripPanel");
            this.toolStripContainerDefault.LeftToolStripPanelVisible = false;
            this.toolStripContainerDefault.Name = "toolStripContainerDefault";
            // 
            // toolStripContainerDefault.RightToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainerDefault.RightToolStripPanel, "toolStripContainerDefault.RightToolStripPanel");
            this.toolStripContainerDefault.RightToolStripPanelVisible = false;
            // 
            // toolStripContainerDefault.TopToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainerDefault.TopToolStripPanel, "toolStripContainerDefault.TopToolStripPanel");
            this.toolStripContainerDefault.TopToolStripPanel.Controls.Add(this.menuStripMain);
            this.toolStripContainerDefault.TopToolStripPanel.Controls.Add(this.toolStripMain);
            this.toolStripContainerDefault.TopToolStripPanel.Controls.Add(this.toolStripExport);
            this.toolStripContainerDefault.TopToolStripPanel.Controls.Add(this.toolStripDebug);
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // _splitContainer
            // 
            resources.ApplyResources(this._splitContainer, "_splitContainer");
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel1
            // 
            resources.ApplyResources(this._splitContainer.Panel1, "_splitContainer.Panel1");
            this._splitContainer.Panel1.Controls.Add(this._treeViewCtrl);
            // 
            // _treeViewCtrl
            // 
            resources.ApplyResources(this._treeViewCtrl, "_treeViewCtrl");
            this._treeViewCtrl.AllowDrop = true;
            this._treeViewCtrl.Name = "_treeViewCtrl";
            this._treeViewCtrl.ShowNodeToolTips = true;
            // 
            // _webBrowser4PDF
            // 
            resources.ApplyResources(this._webBrowser4PDF, "_webBrowser4PDF");
            this._webBrowser4PDF.MinimumSize = new System.Drawing.Size(20, 20);
            this._webBrowser4PDF.Name = "_webBrowser4PDF";
            // 
            // _branchViewCtrl
            // 
            resources.ApplyResources(this._branchViewCtrl, "_branchViewCtrl");
            this._branchViewCtrl.Name = "_branchViewCtrl";
            // 
            // _pluginViewCtrl
            // 
            resources.ApplyResources(this._pluginViewCtrl, "_pluginViewCtrl");
            this._pluginViewCtrl.CloseButtonVisible = false;
            this._pluginViewCtrl.Component = null;
            this._pluginViewCtrl.HasDependancies = false;
            this._pluginViewCtrl.Localizer = null;
            this._pluginViewCtrl.Name = "_pluginViewCtrl";
            // 
            // _pluginViewCtrl.Panel1
            // 
            resources.ApplyResources(this._pluginViewCtrl.Panel1, "_pluginViewCtrl.Panel1");
            // 
            // _pluginViewCtrl.Panel2
            // 
            resources.ApplyResources(this._pluginViewCtrl.Panel2, "_pluginViewCtrl.Panel2");
            this._pluginViewCtrl.ParamValues = null;
            this._pluginViewCtrl.ReflectionX = false;
            this._pluginViewCtrl.ReflectionY = false;
            this._pluginViewCtrl.ShowCotations = true;
            this._pluginViewCtrl.ShowSummary = true;
            this._pluginViewCtrl.ValidateButtonVisible = false;
            // 
            // _factoryViewCtrl
            // 
            resources.ApplyResources(this._factoryViewCtrl, "_factoryViewCtrl");
            this._factoryViewCtrl.Name = "_factoryViewCtrl";
            this._factoryViewCtrl.ReflectionX = false;
            this._factoryViewCtrl.ReflectionY = false;
            this._factoryViewCtrl.ShowAboutMenu = false;
            this._factoryViewCtrl.ShowCotations = false;
            this._factoryViewCtrl.ShowNestingMenu = false;
            // 
            // _startPageCtrl
            // 
            resources.ApplyResources(this._startPageCtrl, "_startPageCtrl");
            this._startPageCtrl.Name = "_startPageCtrl";
            this._startPageCtrl.Url = new System.Uri("about:blank", System.UriKind.Absolute);
            // 
            // _downloadPageCtrl
            // 
            resources.ApplyResources(this._downloadPageCtrl, "_downloadPageCtrl");
            this._downloadPageCtrl.Name = "_downloadPageCtrl";
            // 
            // _splitContainer.Panel2
            // 
            resources.ApplyResources(this._splitContainer.Panel2, "_splitContainer.Panel2");
            this._splitContainer.Panel2.Controls.Add(this._webBrowser4PDF);
            this._splitContainer.Panel2.Controls.Add(this._branchViewCtrl);
            this._splitContainer.Panel2.Controls.Add(this._pluginViewCtrl);
            this._splitContainer.Panel2.Controls.Add(this._factoryViewCtrl);
            this._splitContainer.Panel2.Controls.Add(this._startPageCtrl);
            this._splitContainer.Panel2.Controls.Add(this._downloadPageCtrl);
            // 
            // menuStripMain
            // 
            resources.ApplyResources(this.menuStripMain, "menuStripMain");
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.databaseToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // fileToolStripMenuItem
            // 
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.toolStripMenuItemPicGEOM,
            this.toolStripSeparator8,
            this.toolStripMenuItemBrowseFile,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            // 
            // exportToolStripMenuItem
            // 
            resources.ApplyResources(this.exportToolStripMenuItem, "exportToolStripMenuItem");
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonExport_Click);
            // 
            // toolStripMenuItemPicGEOM
            // 
            resources.ApplyResources(this.toolStripMenuItemPicGEOM, "toolStripMenuItemPicGEOM");
            this.toolStripMenuItemPicGEOM.Name = "toolStripMenuItemPicGEOM";
            this.toolStripMenuItemPicGEOM.Click += new System.EventHandler(this.toolStripButtonPicGEOM_Click);
            // 
            // toolStripSeparator8
            // 
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            // 
            // toolStripMenuItemBrowseFile
            // 
            resources.ApplyResources(this.toolStripMenuItemBrowseFile, "toolStripMenuItemBrowseFile");
            this.toolStripMenuItemBrowseFile.Name = "toolStripMenuItemBrowseFile";
            this.toolStripMenuItemBrowseFile.Click += new System.EventHandler(this.toolStripMenuItemBrowseFile_Click);
            // 
            // exitToolStripMenuItem
            // 
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cotationsToolStripMenuItem,
            this.cotationShortLinesToolStripMenuItem,
            this.toolStripSeparator2,
            this.reflectionXToolStripMenuItem,
            this.reflectionYToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            // 
            // cotationsToolStripMenuItem
            // 
            resources.ApplyResources(this.cotationsToolStripMenuItem, "cotationsToolStripMenuItem");
            this.cotationsToolStripMenuItem.Checked = true;
            this.cotationsToolStripMenuItem.CheckOnClick = true;
            this.cotationsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cotationsToolStripMenuItem.Name = "cotationsToolStripMenuItem";
            this.cotationsToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonCotations_Click);
            // 
            // cotationShortLinesToolStripMenuItem
            // 
            resources.ApplyResources(this.cotationShortLinesToolStripMenuItem, "cotationShortLinesToolStripMenuItem");
            this.cotationShortLinesToolStripMenuItem.Checked = true;
            this.cotationShortLinesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cotationShortLinesToolStripMenuItem.Name = "cotationShortLinesToolStripMenuItem";
            this.cotationShortLinesToolStripMenuItem.Click += new System.EventHandler(this.cotationShortLinesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // reflectionXToolStripMenuItem
            // 
            resources.ApplyResources(this.reflectionXToolStripMenuItem, "reflectionXToolStripMenuItem");
            this.reflectionXToolStripMenuItem.CheckOnClick = true;
            this.reflectionXToolStripMenuItem.Name = "reflectionXToolStripMenuItem";
            this.reflectionXToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonReflectionX_Click);
            // 
            // reflectionYToolStripMenuItem
            // 
            resources.ApplyResources(this.reflectionYToolStripMenuItem, "reflectionYToolStripMenuItem");
            this.reflectionYToolStripMenuItem.CheckOnClick = true;
            this.reflectionYToolStripMenuItem.Name = "reflectionYToolStripMenuItem";
            this.reflectionYToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonReflectionY_Click);
            // 
            // databaseToolStripMenuItem
            // 
            resources.ApplyResources(this.databaseToolStripMenuItem, "databaseToolStripMenuItem");
            this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backupToolStripMenuItem1,
            this.restoreToolStripMenuItem1,
            this.mergeDatabaseToolStripMenuItem1,
            this.clearDatabaseToolStripMenuItem,
            this.toolStripSeparator11,
            this.defineDatabasePathToolStripMenuItem,
            this.toolStripSeparator13,
            this.mnuFileMRU});
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            // 
            // backupToolStripMenuItem1
            // 
            resources.ApplyResources(this.backupToolStripMenuItem1, "backupToolStripMenuItem1");
            this.backupToolStripMenuItem1.Name = "backupToolStripMenuItem1";
            this.backupToolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItemBackup_Click);
            // 
            // restoreToolStripMenuItem1
            // 
            resources.ApplyResources(this.restoreToolStripMenuItem1, "restoreToolStripMenuItem1");
            this.restoreToolStripMenuItem1.Name = "restoreToolStripMenuItem1";
            this.restoreToolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItemRestore_Click);
            // 
            // mergeDatabaseToolStripMenuItem1
            // 
            resources.ApplyResources(this.mergeDatabaseToolStripMenuItem1, "mergeDatabaseToolStripMenuItem1");
            this.mergeDatabaseToolStripMenuItem1.Name = "mergeDatabaseToolStripMenuItem1";
            this.mergeDatabaseToolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItemMerge_Click);
            // 
            // clearDatabaseToolStripMenuItem
            // 
            resources.ApplyResources(this.clearDatabaseToolStripMenuItem, "clearDatabaseToolStripMenuItem");
            this.clearDatabaseToolStripMenuItem.Name = "clearDatabaseToolStripMenuItem";
            this.clearDatabaseToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItemClearDatabase_Click);
            // 
            // toolStripSeparator11
            // 
            resources.ApplyResources(this.toolStripSeparator11, "toolStripSeparator11");
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            // 
            // defineDatabasePathToolStripMenuItem
            // 
            resources.ApplyResources(this.defineDatabasePathToolStripMenuItem, "defineDatabasePathToolStripMenuItem");
            this.defineDatabasePathToolStripMenuItem.Name = "defineDatabasePathToolStripMenuItem";
            this.defineDatabasePathToolStripMenuItem.Click += new System.EventHandler(this.defineDatabasePathToolStripMenuItem_Click);
            // 
            // toolStripSeparator13
            // 
            resources.ApplyResources(this.toolStripSeparator13, "toolStripSeparator13");
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            // 
            // mnuFileMRU
            // 
            resources.ApplyResources(this.mnuFileMRU, "mnuFileMRU");
            this.mnuFileMRU.Name = "mnuFileMRU";
            // 
            // toolsToolStripMenuItem
            // 
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editprofilesToolStripMenuItem,
            this.editmaterialsToolStripMenuItem,
            this.toolStripSeparator4,
            this.layoutToolStripMenuItem,
            this.toolStripSeparator9,
            this.customizeToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            // 
            // editprofilesToolStripMenuItem
            // 
            resources.ApplyResources(this.editprofilesToolStripMenuItem, "editprofilesToolStripMenuItem");
            this.editprofilesToolStripMenuItem.Name = "editprofilesToolStripMenuItem";
            this.editprofilesToolStripMenuItem.Click += new System.EventHandler(this.editProfilesToolStripMenuItem_Click);
            // 
            // editmaterialsToolStripMenuItem
            // 
            resources.ApplyResources(this.editmaterialsToolStripMenuItem, "editmaterialsToolStripMenuItem");
            this.editmaterialsToolStripMenuItem.Name = "editmaterialsToolStripMenuItem";
            // 
            // toolStripSeparator4
            // 
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // layoutToolStripMenuItem
            // 
            resources.ApplyResources(this.layoutToolStripMenuItem, "layoutToolStripMenuItem");
            this.layoutToolStripMenuItem.Name = "layoutToolStripMenuItem";
            this.layoutToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonLayout_Click);
            // 
            // toolStripSeparator9
            // 
            resources.ApplyResources(this.toolStripSeparator9, "toolStripSeparator9");
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            // 
            // customizeToolStripMenuItem
            // 
            resources.ApplyResources(this.customizeToolStripMenuItem, "customizeToolStripMenuItem");
            this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            this.customizeToolStripMenuItem.Click += new System.EventHandler(this.customizeToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutTreeDimPicParamToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            // 
            // aboutTreeDimPicParamToolStripMenuItem
            // 
            resources.ApplyResources(this.aboutTreeDimPicParamToolStripMenuItem, "aboutTreeDimPicParamToolStripMenuItem");
            this.aboutTreeDimPicParamToolStripMenuItem.Name = "aboutTreeDimPicParamToolStripMenuItem";
            this.aboutTreeDimPicParamToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripMain
            // 
            resources.ApplyResources(this.toolStripMain, "toolStripMain");
            this.toolStripMain.AllowItemReorder = true;
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonRoot,
            this.toolStripButtonDownload,
            this.toolStripSeparator1,
            this.toolStripButtonExport,
            this.toolStripSeparator10,
            this.toolStripButtonCotations,
            this.toolStripButtonReflectionX,
            this.toolStripButtonReflectionY,
            this.toolStripSeparator3,
            this.toolStripEditComponentCode,
            this.toolStripButtonEditParameters,
            this.toolStripSeparator5,
            this.toolStripButtonLayout,
            this.toolStripSeparator6,
            this.toolStripButtonHelp});
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // toolStripButtonRoot
            // 
            resources.ApplyResources(this.toolStripButtonRoot, "toolStripButtonRoot");
            this.toolStripButtonRoot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRoot.Name = "toolStripButtonRoot";
            this.toolStripButtonRoot.Click += new System.EventHandler(this.toolStripButtonRoot_Click);
            // 
            // toolStripButtonDownload
            // 
            resources.ApplyResources(this.toolStripButtonDownload, "toolStripButtonDownload");
            this.toolStripButtonDownload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDownload.Name = "toolStripButtonDownload";
            this.toolStripButtonDownload.Click += new System.EventHandler(this.toolStripButtonDownload_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // toolStripButtonExport
            // 
            resources.ApplyResources(this.toolStripButtonExport, "toolStripButtonExport");
            this.toolStripButtonExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExport.Name = "toolStripButtonExport";
            this.toolStripButtonExport.Click += new System.EventHandler(this.toolStripButtonExport_Click);
            // 
            // toolStripSeparator10
            // 
            resources.ApplyResources(this.toolStripSeparator10, "toolStripSeparator10");
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            // 
            // toolStripButtonCotations
            // 
            resources.ApplyResources(this.toolStripButtonCotations, "toolStripButtonCotations");
            this.toolStripButtonCotations.Checked = true;
            this.toolStripButtonCotations.CheckOnClick = true;
            this.toolStripButtonCotations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonCotations.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCotations.Name = "toolStripButtonCotations";
            this.toolStripButtonCotations.Click += new System.EventHandler(this.toolStripButtonCotations_Click);
            // 
            // toolStripButtonReflectionX
            // 
            resources.ApplyResources(this.toolStripButtonReflectionX, "toolStripButtonReflectionX");
            this.toolStripButtonReflectionX.CheckOnClick = true;
            this.toolStripButtonReflectionX.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonReflectionX.Name = "toolStripButtonReflectionX";
            this.toolStripButtonReflectionX.Click += new System.EventHandler(this.toolStripButtonReflectionX_Click);
            // 
            // toolStripButtonReflectionY
            // 
            resources.ApplyResources(this.toolStripButtonReflectionY, "toolStripButtonReflectionY");
            this.toolStripButtonReflectionY.CheckOnClick = true;
            this.toolStripButtonReflectionY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonReflectionY.Name = "toolStripButtonReflectionY";
            this.toolStripButtonReflectionY.Click += new System.EventHandler(this.toolStripButtonReflectionY_Click);
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // toolStripEditComponentCode
            // 
            resources.ApplyResources(this.toolStripEditComponentCode, "toolStripEditComponentCode");
            this.toolStripEditComponentCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEditComponentCode.Name = "toolStripEditComponentCode";
            this.toolStripEditComponentCode.Click += new System.EventHandler(this.toolStripEditComponentCode_Click);
            // 
            // toolStripButtonEditParameters
            // 
            resources.ApplyResources(this.toolStripButtonEditParameters, "toolStripButtonEditParameters");
            this.toolStripButtonEditParameters.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEditParameters.Name = "toolStripButtonEditParameters";
            this.toolStripButtonEditParameters.Click += new System.EventHandler(this.toolStripButtonEditParameters_Click);
            // 
            // toolStripSeparator5
            // 
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // toolStripButtonLayout
            // 
            resources.ApplyResources(this.toolStripButtonLayout, "toolStripButtonLayout");
            this.toolStripButtonLayout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLayout.Name = "toolStripButtonLayout";
            this.toolStripButtonLayout.Click += new System.EventHandler(this.toolStripButtonLayout_Click);
            // 
            // toolStripSeparator6
            // 
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            // 
            // toolStripButtonHelp
            // 
            resources.ApplyResources(this.toolStripButtonHelp, "toolStripButtonHelp");
            this.toolStripButtonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonHelp.Name = "toolStripButtonHelp";
            this.toolStripButtonHelp.Click += new System.EventHandler(this.toolStripButtonHelp_Click);
            // 
            // toolStripExport
            // 
            resources.ApplyResources(this.toolStripExport, "toolStripExport");
            this.toolStripExport.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripExport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonPicGEOM,
            this.toolStripButtonPicDecoup,
            this.toolStripButtonPicador3D,
            this.toolStripButtonDXF,
            this.toolStripButtonAI,
            this.toolStripButtonCFF2,
            this.toolStripSeparator12,
            this.toolStripButtonOceProCut,
            this.toolStripSeparator7,
            this.toolStripButtonCaseOptimization,
            this.toolStripButtonPalletization});
            this.toolStripExport.Name = "toolStripExport";
            this.toolStripExport.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // toolStripButtonPicGEOM
            // 
            resources.ApplyResources(this.toolStripButtonPicGEOM, "toolStripButtonPicGEOM");
            this.toolStripButtonPicGEOM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPicGEOM.Name = "toolStripButtonPicGEOM";
            this.toolStripButtonPicGEOM.Click += new System.EventHandler(this.toolStripButtonPicGEOM_Click);
            // 
            // toolStripButtonPicDecoup
            // 
            resources.ApplyResources(this.toolStripButtonPicDecoup, "toolStripButtonPicDecoup");
            this.toolStripButtonPicDecoup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPicDecoup.Name = "toolStripButtonPicDecoup";
            this.toolStripButtonPicDecoup.Click += new System.EventHandler(this.toolStripButtonPicDecoup_Click);
            // 
            // toolStripButtonPicador3D
            // 
            resources.ApplyResources(this.toolStripButtonPicador3D, "toolStripButtonPicador3D");
            this.toolStripButtonPicador3D.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPicador3D.Name = "toolStripButtonPicador3D";
            this.toolStripButtonPicador3D.Click += new System.EventHandler(this.toolStripButtonPicador3D_Click);
            // 
            // toolStripButtonDXF
            // 
            resources.ApplyResources(this.toolStripButtonDXF, "toolStripButtonDXF");
            this.toolStripButtonDXF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDXF.Name = "toolStripButtonDXF";
            this.toolStripButtonDXF.Click += new System.EventHandler(this.toolStripButtonDXF_Click);
            // 
            // toolStripButtonAI
            // 
            resources.ApplyResources(this.toolStripButtonAI, "toolStripButtonAI");
            this.toolStripButtonAI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAI.Name = "toolStripButtonAI";
            this.toolStripButtonAI.Click += new System.EventHandler(this.toolStripButtonAI_Click);
            // 
            // toolStripButtonCFF2
            // 
            resources.ApplyResources(this.toolStripButtonCFF2, "toolStripButtonCFF2");
            this.toolStripButtonCFF2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCFF2.Name = "toolStripButtonCFF2";
            this.toolStripButtonCFF2.Click += new System.EventHandler(this.toolStripButtonCFF2_Click);
            // 
            // toolStripSeparator12
            // 
            resources.ApplyResources(this.toolStripSeparator12, "toolStripSeparator12");
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            // 
            // toolStripButtonOceProCut
            // 
            resources.ApplyResources(this.toolStripButtonOceProCut, "toolStripButtonOceProCut");
            this.toolStripButtonOceProCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOceProCut.Name = "toolStripButtonOceProCut";
            this.toolStripButtonOceProCut.Click += new System.EventHandler(this.toolStripButtonOceProCut_Click);
            // 
            // toolStripSeparator7
            // 
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            // 
            // toolStripButtonCaseOptimization
            // 
            resources.ApplyResources(this.toolStripButtonCaseOptimization, "toolStripButtonCaseOptimization");
            this.toolStripButtonCaseOptimization.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCaseOptimization.Name = "toolStripButtonCaseOptimization";
            this.toolStripButtonCaseOptimization.Click += new System.EventHandler(this.toolStripButtonCaseOptimization_Click);
            // 
            // toolStripButtonPalletization
            // 
            resources.ApplyResources(this.toolStripButtonPalletization, "toolStripButtonPalletization");
            this.toolStripButtonPalletization.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPalletization.Name = "toolStripButtonPalletization";
            this.toolStripButtonPalletization.Click += new System.EventHandler(this.toolStripButtonPalletization_Click);
            // 
            // toolStripDebug
            // 
            resources.ApplyResources(this.toolStripDebug, "toolStripDebug");
            this.toolStripDebug.AllowItemReorder = true;
            this.toolStripDebug.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripDebug.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripEditDLL});
            this.toolStripDebug.Name = "toolStripDebug";
            this.toolStripDebug.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripDebug.TabStop = true;
            // 
            // toolStripEditDLL
            // 
            resources.ApplyResources(this.toolStripEditDLL, "toolStripEditDLL");
            this.toolStripEditDLL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEditDLL.Name = "toolStripEditDLL";
            this.toolStripEditDLL.Click += new System.EventHandler(this.toolStripEditDLL_Click);
            // 
            // openFileDialogRestore
            // 
            resources.ApplyResources(this.openFileDialogRestore, "openFileDialogRestore");
            // 
            // saveFileDialogBackup
            // 
            resources.ApplyResources(this.saveFileDialogBackup, "saveFileDialogBackup");
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainerDefault);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.UpdateTextPosition);
            this.toolStripContainerDefault.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainerDefault.BottomToolStripPanel.PerformLayout();
            this.toolStripContainerDefault.ContentPanel.ResumeLayout(false);
            this.toolStripContainerDefault.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainerDefault.TopToolStripPanel.PerformLayout();
            this.toolStripContainerDefault.ResumeLayout(false);
            this.toolStripContainerDefault.PerformLayout();
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel2.ResumeLayout(false);
            this._splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._pluginViewCtrl)).EndInit();
            this._pluginViewCtrl.ResumeLayout(false);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.toolStripExport.ResumeLayout(false);
            this.toolStripExport.PerformLayout();
            this.toolStripDebug.ResumeLayout(false);
            this.toolStripDebug.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private Pic.Plugin.ViewCtrl.PluginViewCtrl _pluginViewCtrl;
        private PicParam.DocumentTreeBranchView _branchViewCtrl;
        private Pic.Factory2D.Control.FactoryViewerBase _factoryViewCtrl;
        private System.Windows.Forms.WebBrowser _webBrowser4PDF;
        private PicParam.StartPageControl _startPageCtrl;
        private PicParam.DownloadPageControl _downloadPageCtrl;

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutTreeDimPicParamToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonCotations;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cotationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editprofilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editmaterialsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonReflectionX;
        private System.Windows.Forms.ToolStripButton toolStripButtonReflectionY;
        private System.Windows.Forms.ToolStripMenuItem reflectionXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reflectionYToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.OpenFileDialog openFileDialogRestore;
        private System.Windows.Forms.SaveFileDialog saveFileDialogBackup;
        private System.Windows.Forms.ToolStripButton toolStripButtonLayout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem layoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPicGEOM;
        private System.Windows.Forms.ToolStripButton toolStripEditComponentCode;
        private System.Windows.Forms.ToolStripMenuItem cotationShortLinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonEditParameters;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBrowseFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem customizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonPalletization;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mergeDatabaseToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem clearDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonRoot;
        private System.Windows.Forms.ToolStrip toolStripExport;
        private System.Windows.Forms.ToolStripButton toolStripButtonPicDecoup;
        private System.Windows.Forms.ToolStripButton toolStripButtonExport;
        private System.Windows.Forms.ToolStripButton toolStripButtonPicGEOM;
        private System.Windows.Forms.ToolStripButton toolStripButtonPicador3D;
        private System.Windows.Forms.ToolStripButton toolStripButtonDXF;
        private System.Windows.Forms.ToolStripButton toolStripButtonCaseOptimization;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private DocumentTreeView _treeViewCtrl;
        private System.Windows.Forms.ToolStripButton toolStripButtonHelp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripContainer toolStripContainerDefault;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem defineDatabasePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonOceProCut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton toolStripButtonAI;
        private System.Windows.Forms.ToolStripButton toolStripButtonCFF2;
        private System.Windows.Forms.ToolStripButton toolStripButtonDownload;
        private System.Windows.Forms.ToolStrip toolStripDebug;
        private System.Windows.Forms.ToolStripButton toolStripEditDLL;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem mnuFileMRU;
    }
}

