namespace Pic.Data
{
    partial class FormCreateBranch
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
            this.label_name = new System.Windows.Forms.Label();
            this.label_description = new System.Windows.Forms.Label();
            this.checkBoxImage = new System.Windows.Forms.CheckBox();
            this.bnOk = new System.Windows.Forms.Button();
            this.bnCancel = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.fileSelectCtrl = new TreeDim.UserControls.FileSelect();
            this.SuspendLayout();
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(19, 9);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(35, 13);
            this.label_name.TabIndex = 0;
            this.label_name.Text = "Name";
            // 
            // label_description
            // 
            this.label_description.AutoSize = true;
            this.label_description.Location = new System.Drawing.Point(16, 39);
            this.label_description.Name = "label_description";
            this.label_description.Size = new System.Drawing.Size(60, 13);
            this.label_description.TabIndex = 1;
            this.label_description.Text = "Description";
            // 
            // checkBoxImage
            // 
            this.checkBoxImage.AutoSize = true;
            this.checkBoxImage.Location = new System.Drawing.Point(19, 76);
            this.checkBoxImage.Name = "checkBoxImage";
            this.checkBoxImage.Size = new System.Drawing.Size(55, 17);
            this.checkBoxImage.TabIndex = 2;
            this.checkBoxImage.Text = "Image";
            this.checkBoxImage.UseVisualStyleBackColor = true;
            this.checkBoxImage.CheckedChanged += new System.EventHandler(this.checkBoxImage_CheckedChanged);
            // 
            // bnOk
            // 
            this.bnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bnOk.Location = new System.Drawing.Point(475, 9);
            this.bnOk.Name = "bnOk";
            this.bnOk.Size = new System.Drawing.Size(75, 23);
            this.bnOk.TabIndex = 3;
            this.bnOk.Text = "&Ok";
            this.bnOk.UseVisualStyleBackColor = true;
            this.bnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            // 
            // bnCancel
            // 
            this.bnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bnCancel.Location = new System.Drawing.Point(475, 39);
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.Size = new System.Drawing.Size(75, 23);
            this.bnCancel.TabIndex = 4;
            this.bnCancel.Text = "&Cancel";
            this.bnCancel.UseVisualStyleBackColor = true;
            this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(114, 9);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(338, 20);
            this.textBoxName.TabIndex = 5;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(114, 39);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(338, 20);
            this.textBoxDescription.TabIndex = 6;
            // 
            // fileSelectCtrl
            // 
            this.fileSelectCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fileSelectCtrl.Location = new System.Drawing.Point(114, 76);
            this.fileSelectCtrl.Name = "fileSelectCtrl";
            this.fileSelectCtrl.Size = new System.Drawing.Size(436, 20);
            this.fileSelectCtrl.TabIndex = 7;
            // 
            // FormCreateBranch
            // 
            this.AcceptButton = this.bnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bnCancel;
            this.ClientSize = new System.Drawing.Size(562, 113);
            this.Controls.Add(this.fileSelectCtrl);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.bnCancel);
            this.Controls.Add(this.bnOk);
            this.Controls.Add(this.checkBoxImage);
            this.Controls.Add(this.label_description);
            this.Controls.Add(this.label_name);
            this.Name = "FormCreateBranch";
            this.Text = "Create new branch...";
            this.Load += new System.EventHandler(this.FormCreateBranch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label_description;
        private System.Windows.Forms.CheckBox checkBoxImage;
        private System.Windows.Forms.Button bnOk;
        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private TreeDim.UserControls.FileSelect fileSelectCtrl;
    }
}