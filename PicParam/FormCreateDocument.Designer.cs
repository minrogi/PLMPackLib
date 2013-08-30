namespace PicParam
{
    partial class FormCreateDocument
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCreateDocument));
            this.bnOk = new System.Windows.Forms.Button();
            this.bnCancel = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelFile = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.fileSelectCtrl = new TreeDim.UserControls.FileSelect();
            this.pictureBoxFileView = new System.Windows.Forms.PictureBox();
            this.thumbnailSelectCtrl = new TreeDim.UserControls.FileSelect();
            this.chkThumbnail = new System.Windows.Forms.CheckBox();
            this.chkDefaultParameters = new System.Windows.Forms.CheckBox();
            this.factoryViewer = new Pic.Factory2D.Control.FactoryViewerBase();
            this.webBrowser4PDF = new System.Windows.Forms.WebBrowser();
            this.componentLoaderControl = new PicParam.ComponentLoaderControl();
            this.pb4ImageFiles = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFileView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb4ImageFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // bnOk
            // 
            resources.ApplyResources(this.bnOk, "bnOk");
            this.bnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bnOk.Name = "bnOk";
            this.bnOk.UseVisualStyleBackColor = true;
            this.bnOk.Click += new System.EventHandler(this.bnOk_Click);
            // 
            // bnCancel
            // 
            resources.ApplyResources(this.bnCancel, "bnCancel");
            this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.UseVisualStyleBackColor = true;
            // 
            // labelName
            // 
            resources.ApplyResources(this.labelName, "labelName");
            this.labelName.Name = "labelName";
            // 
            // labelDescription
            // 
            resources.ApplyResources(this.labelDescription, "labelDescription");
            this.labelDescription.Name = "labelDescription";
            // 
            // labelFile
            // 
            resources.ApplyResources(this.labelFile, "labelFile");
            this.labelFile.Name = "labelFile";
            // 
            // textBoxName
            // 
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.Name = "textBoxName";
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.Name = "textBoxDescription";
            // 
            // fileSelectCtrl
            // 
            resources.ApplyResources(this.fileSelectCtrl, "fileSelectCtrl");
            this.fileSelectCtrl.Name = "fileSelectCtrl";
            // 
            // pictureBoxFileView
            // 
            resources.ApplyResources(this.pictureBoxFileView, "pictureBoxFileView");
            this.pictureBoxFileView.Name = "pictureBoxFileView";
            this.pictureBoxFileView.TabStop = false;
            // 
            // thumbnailSelectCtrl
            // 
            resources.ApplyResources(this.thumbnailSelectCtrl, "thumbnailSelectCtrl");
            this.thumbnailSelectCtrl.Name = "thumbnailSelectCtrl";
            // 
            // chkThumbnail
            // 
            resources.ApplyResources(this.chkThumbnail, "chkThumbnail");
            this.chkThumbnail.Name = "chkThumbnail";
            this.chkThumbnail.UseVisualStyleBackColor = true;
            this.chkThumbnail.CheckedChanged += new System.EventHandler(this.chbThumbnail_CheckedChanged);
            // 
            // chkDefaultParameters
            // 
            resources.ApplyResources(this.chkDefaultParameters, "chkDefaultParameters");
            this.chkDefaultParameters.Name = "chkDefaultParameters";
            this.chkDefaultParameters.UseVisualStyleBackColor = true;
            // 
            // factoryViewer
            // 
            resources.ApplyResources(this.factoryViewer, "factoryViewer");
            this.factoryViewer.Name = "factoryViewer";
            this.factoryViewer.ReflectionX = false;
            this.factoryViewer.ReflectionY = false;
            this.factoryViewer.ShowAboutMenu = false;
            this.factoryViewer.ShowCotations = false;
            this.factoryViewer.ShowNestingMenu = false;
            this.factoryViewer.TabStop = false;
            // 
            // webBrowser4PDF
            // 
            resources.ApplyResources(this.webBrowser4PDF, "webBrowser4PDF");
            this.webBrowser4PDF.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser4PDF.Name = "webBrowser4PDF";
            // 
            // componentLoaderControl
            // 
            resources.ApplyResources(this.componentLoaderControl, "componentLoaderControl");
            this.componentLoaderControl.Name = "componentLoaderControl";
            this.componentLoaderControl.TabStop = false;
            // 
            // pb4ImageFiles
            // 
            resources.ApplyResources(this.pb4ImageFiles, "pb4ImageFiles");
            this.pb4ImageFiles.Name = "pb4ImageFiles";
            this.pb4ImageFiles.TabStop = false;
            // 
            // FormCreateDocument
            // 
            this.AcceptButton = this.bnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bnCancel;
            this.Controls.Add(this.pb4ImageFiles);
            this.Controls.Add(this.webBrowser4PDF);
            this.Controls.Add(this.chkDefaultParameters);
            this.Controls.Add(this.chkThumbnail);
            this.Controls.Add(this.thumbnailSelectCtrl);
            this.Controls.Add(this.componentLoaderControl);
            this.Controls.Add(this.factoryViewer);
            this.Controls.Add(this.pictureBoxFileView);
            this.Controls.Add(this.fileSelectCtrl);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.bnCancel);
            this.Controls.Add(this.bnOk);
            this.Name = "FormCreateDocument";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.FormCreateDocument_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFileView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb4ImageFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bnOk;
        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private TreeDim.UserControls.FileSelect fileSelectCtrl;

        private System.Windows.Forms.PictureBox pictureBoxFileView;
        private TreeDim.UserControls.FileSelect thumbnailSelectCtrl;
        private System.Windows.Forms.CheckBox chkThumbnail;
        private System.Windows.Forms.CheckBox chkDefaultParameters;

        private ComponentLoaderControl componentLoaderControl;
        private Pic.Factory2D.Control.FactoryViewerBase factoryViewer;
        private System.Windows.Forms.WebBrowser webBrowser4PDF;
        private System.Windows.Forms.PictureBox pb4ImageFiles;
    }
}