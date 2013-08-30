namespace PicParam
{
    partial class FormExportFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExportFile));
            this.label_fileFormat = new System.Windows.Forms.Label();
            this.cbFileFormat = new System.Windows.Forms.ComboBox();
            this.bnOk = new System.Windows.Forms.Button();
            this.bnCancel = new System.Windows.Forms.Button();
            this.label_filePath = new System.Windows.Forms.Label();
            this.checkBox_openFile = new System.Windows.Forms.CheckBox();
            this.fileSelectCtrl = new TreeDim.UserControls.FileSelect();
            this.SuspendLayout();
            // 
            // label_fileFormat
            // 
            resources.ApplyResources(this.label_fileFormat, "label_fileFormat");
            this.label_fileFormat.Name = "label_fileFormat";
            // 
            // cbFileFormat
            // 
            resources.ApplyResources(this.cbFileFormat, "cbFileFormat");
            this.cbFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFileFormat.FormattingEnabled = true;
            this.cbFileFormat.Name = "cbFileFormat";
            this.cbFileFormat.SelectedIndexChanged += new System.EventHandler(this.cbFileFormat_SelectedIndexChanged);
            // 
            // bnOk
            // 
            resources.ApplyResources(this.bnOk, "bnOk");
            this.bnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bnOk.Name = "bnOk";
            this.bnOk.UseVisualStyleBackColor = true;
            // 
            // bnCancel
            // 
            resources.ApplyResources(this.bnCancel, "bnCancel");
            this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.UseVisualStyleBackColor = true;
            // 
            // label_filePath
            // 
            resources.ApplyResources(this.label_filePath, "label_filePath");
            this.label_filePath.Name = "label_filePath";
            // 
            // checkBox_openFile
            // 
            resources.ApplyResources(this.checkBox_openFile, "checkBox_openFile");
            this.checkBox_openFile.Name = "checkBox_openFile";
            this.checkBox_openFile.UseVisualStyleBackColor = true;
            // 
            // fileSelectCtrl
            // 
            resources.ApplyResources(this.fileSelectCtrl, "fileSelectCtrl");
            this.fileSelectCtrl.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.fileSelectCtrl.Name = "fileSelectCtrl";
            this.fileSelectCtrl.SaveMode = true;
            this.fileSelectCtrl.FileNameChanged += new System.EventHandler(this.fileSelectCtrl_FileNameChanged);
            // 
            // FormExportFile
            // 
            this.AcceptButton = this.bnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bnCancel;
            this.Controls.Add(this.checkBox_openFile);
            this.Controls.Add(this.fileSelectCtrl);
            this.Controls.Add(this.label_filePath);
            this.Controls.Add(this.bnCancel);
            this.Controls.Add(this.bnOk);
            this.Controls.Add(this.cbFileFormat);
            this.Controls.Add(this.label_fileFormat);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExportFile";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.FormExportFile_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormExportFile_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_fileFormat;
        private System.Windows.Forms.ComboBox cbFileFormat;
        private System.Windows.Forms.Button bnOk;
        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.Label label_filePath;
        private TreeDim.UserControls.FileSelect fileSelectCtrl;
        private System.Windows.Forms.CheckBox checkBox_openFile;
    }
}