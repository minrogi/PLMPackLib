namespace PicParam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCreateBranch));
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
            this.label_name.AccessibleDescription = null;
            this.label_name.AccessibleName = null;
            resources.ApplyResources(this.label_name, "label_name");
            this.label_name.Font = null;
            this.label_name.Name = "label_name";
            // 
            // label_description
            // 
            this.label_description.AccessibleDescription = null;
            this.label_description.AccessibleName = null;
            resources.ApplyResources(this.label_description, "label_description");
            this.label_description.Font = null;
            this.label_description.Name = "label_description";
            // 
            // checkBoxImage
            // 
            this.checkBoxImage.AccessibleDescription = null;
            this.checkBoxImage.AccessibleName = null;
            resources.ApplyResources(this.checkBoxImage, "checkBoxImage");
            this.checkBoxImage.BackgroundImage = null;
            this.checkBoxImage.Font = null;
            this.checkBoxImage.Name = "checkBoxImage";
            this.checkBoxImage.UseVisualStyleBackColor = true;
            this.checkBoxImage.CheckedChanged += new System.EventHandler(this.checkBoxImage_CheckedChanged);
            // 
            // bnOk
            // 
            this.bnOk.AccessibleDescription = null;
            this.bnOk.AccessibleName = null;
            resources.ApplyResources(this.bnOk, "bnOk");
            this.bnOk.BackgroundImage = null;
            this.bnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bnOk.Font = null;
            this.bnOk.Name = "bnOk";
            this.bnOk.UseVisualStyleBackColor = true;
            // 
            // bnCancel
            // 
            this.bnCancel.AccessibleDescription = null;
            this.bnCancel.AccessibleName = null;
            resources.ApplyResources(this.bnCancel, "bnCancel");
            this.bnCancel.BackgroundImage = null;
            this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bnCancel.Font = null;
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.UseVisualStyleBackColor = true;
            // 
            // textBoxName
            // 
            this.textBoxName.AccessibleDescription = null;
            this.textBoxName.AccessibleName = null;
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.BackgroundImage = null;
            this.textBoxName.Font = null;
            this.textBoxName.Name = "textBoxName";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.AccessibleDescription = null;
            this.textBoxDescription.AccessibleName = null;
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.BackgroundImage = null;
            this.textBoxDescription.Font = null;
            this.textBoxDescription.Name = "textBoxDescription";
            // 
            // fileSelectCtrl
            // 
            this.fileSelectCtrl.AccessibleDescription = null;
            this.fileSelectCtrl.AccessibleName = null;
            resources.ApplyResources(this.fileSelectCtrl, "fileSelectCtrl");
            this.fileSelectCtrl.BackgroundImage = null;
            this.fileSelectCtrl.Font = null;
            this.fileSelectCtrl.Name = "fileSelectCtrl";
            // 
            // FormCreateBranch
            // 
            this.AcceptButton = this.bnOk;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.bnCancel;
            this.Controls.Add(this.fileSelectCtrl);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.bnCancel);
            this.Controls.Add(this.bnOk);
            this.Controls.Add(this.checkBoxImage);
            this.Controls.Add(this.label_description);
            this.Controls.Add(this.label_name);
            this.Font = null;
            this.Name = "FormCreateBranch";
            this.ShowIcon = false;
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