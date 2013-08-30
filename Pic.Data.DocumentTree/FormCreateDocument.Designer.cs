namespace Pic.Data
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
            this.bnOk = new System.Windows.Forms.Button();
            this.bnCancel = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelFile = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.fileSelectCtrl = new TreeDim.UserControls.FileSelect();
            this.pictureBoxFileView = new System.Windows.Forms.PictureBox();
            this.groupBoxMajorations = new System.Windows.Forms.GroupBox();
            this.lblProfile = new System.Windows.Forms.Label();
            this.comboBoxProfile = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFileView)).BeginInit();
            this.groupBoxMajorations.SuspendLayout();
            this.SuspendLayout();
            // 
            // bnOk
            // 
            this.bnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bnOk.Location = new System.Drawing.Point(412, 12);
            this.bnOk.Name = "bnOk";
            this.bnOk.Size = new System.Drawing.Size(75, 23);
            this.bnOk.TabIndex = 0;
            this.bnOk.Text = "&Ok";
            this.bnOk.UseVisualStyleBackColor = true;
            this.bnOk.Click += new System.EventHandler(this.bnOk_Click);
            // 
            // bnCancel
            // 
            this.bnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bnCancel.Location = new System.Drawing.Point(412, 41);
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.Size = new System.Drawing.Size(75, 23);
            this.bnCancel.TabIndex = 1;
            this.bnCancel.Text = "&Cancel";
            this.bnCancel.UseVisualStyleBackColor = true;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(16, 12);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(35, 13);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Name";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(16, 41);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(60, 13);
            this.labelDescription.TabIndex = 3;
            this.labelDescription.Text = "Description";
            // 
            // labelFile
            // 
            this.labelFile.AutoSize = true;
            this.labelFile.Location = new System.Drawing.Point(16, 72);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(23, 13);
            this.labelFile.TabIndex = 4;
            this.labelFile.Text = "File";
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(86, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(300, 20);
            this.textBoxName.TabIndex = 5;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(86, 41);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(300, 20);
            this.textBoxDescription.TabIndex = 6;
            // 
            // fileSelectCtrl
            // 
            this.fileSelectCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fileSelectCtrl.Location = new System.Drawing.Point(86, 72);
            this.fileSelectCtrl.Name = "fileSelectCtrl";
            this.fileSelectCtrl.Size = new System.Drawing.Size(401, 20);
            this.fileSelectCtrl.TabIndex = 7;
            // 
            // pictureBoxFileView
            // 
            this.pictureBoxFileView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxFileView.Location = new System.Drawing.Point(12, 263);
            this.pictureBoxFileView.Name = "pictureBoxFileView";
            this.pictureBoxFileView.Size = new System.Drawing.Size(474, 400);
            this.pictureBoxFileView.TabIndex = 8;
            this.pictureBoxFileView.TabStop = false;
            // 
            // groupBoxMajorations
            // 
            this.groupBoxMajorations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMajorations.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBoxMajorations.Controls.Add(this.lblProfile);
            this.groupBoxMajorations.Controls.Add(this.comboBoxProfile);
            this.groupBoxMajorations.Location = new System.Drawing.Point(12, 99);
            this.groupBoxMajorations.Name = "groupBoxMajorations";
            this.groupBoxMajorations.Size = new System.Drawing.Size(475, 158);
            this.groupBoxMajorations.TabIndex = 9;
            this.groupBoxMajorations.TabStop = false;
            this.groupBoxMajorations.Text = "Majorations";
            // 
            // lblProfile
            // 
            this.lblProfile.AutoSize = true;
            this.lblProfile.Location = new System.Drawing.Point(16, 20);
            this.lblProfile.Name = "lblProfile";
            this.lblProfile.Size = new System.Drawing.Size(36, 13);
            this.lblProfile.TabIndex = 1;
            this.lblProfile.Text = "Profile";
            // 
            // comboBoxProfile
            // 
            this.comboBoxProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfile.FormattingEnabled = true;
            this.comboBoxProfile.Location = new System.Drawing.Point(83, 16);
            this.comboBoxProfile.Name = "comboBoxProfile";
            this.comboBoxProfile.Size = new System.Drawing.Size(300, 21);
            this.comboBoxProfile.TabIndex = 0;
            // 
            // FormCreateDocument
            // 
            this.AcceptButton = this.bnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bnCancel;
            this.ClientSize = new System.Drawing.Size(499, 676);
            this.Controls.Add(this.groupBoxMajorations);
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
            this.Text = "Create new document...";
            this.Load += new System.EventHandler(this.FormCreateDocument_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFileView)).EndInit();
            this.groupBoxMajorations.ResumeLayout(false);
            this.groupBoxMajorations.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBoxMajorations;
        private System.Windows.Forms.ComboBox comboBoxProfile;
        private System.Windows.Forms.Label lblProfile;
    }
}