namespace PicParam
{
    partial class FormEditProfiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditProfiles));
            this.btClose = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.listViewProfile = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colThickness = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btCreate = new System.Windows.Forms.Button();
            this.bnModify = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btClose
            // 
            resources.ApplyResources(this.btClose, "btClose");
            this.btClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btClose.Name = "btClose";
            this.btClose.UseVisualStyleBackColor = true;
            // 
            // btDelete
            // 
            resources.ApplyResources(this.btDelete, "btDelete");
            this.btDelete.Name = "btDelete";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // listViewProfile
            // 
            resources.ApplyResources(this.listViewProfile, "listViewProfile");
            this.listViewProfile.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colCode,
            this.colThickness});
            this.listViewProfile.FullRowSelect = true;
            this.listViewProfile.GridLines = true;
            this.listViewProfile.HideSelection = false;
            this.listViewProfile.MultiSelect = false;
            this.listViewProfile.Name = "listViewProfile";
            this.listViewProfile.ShowItemToolTips = true;
            this.listViewProfile.UseCompatibleStateImageBehavior = false;
            this.listViewProfile.View = System.Windows.Forms.View.Details;
            this.listViewProfile.SelectedIndexChanged += new System.EventHandler(this.listViewProfile_SelectedIndexChanged);
            // 
            // colName
            // 
            resources.ApplyResources(this.colName, "colName");
            // 
            // colCode
            // 
            resources.ApplyResources(this.colCode, "colCode");
            // 
            // colThickness
            // 
            resources.ApplyResources(this.colThickness, "colThickness");
            // 
            // btCreate
            // 
            resources.ApplyResources(this.btCreate, "btCreate");
            this.btCreate.Name = "btCreate";
            this.btCreate.UseVisualStyleBackColor = true;
            this.btCreate.Click += new System.EventHandler(this.btCreate_Click);
            // 
            // bnModify
            // 
            resources.ApplyResources(this.bnModify, "bnModify");
            this.bnModify.Name = "bnModify";
            this.bnModify.UseVisualStyleBackColor = true;
            this.bnModify.Click += new System.EventHandler(this.bnModify_Click);
            // 
            // FormEditProfiles
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btClose;
            this.Controls.Add(this.bnModify);
            this.Controls.Add(this.btCreate);
            this.Controls.Add(this.listViewProfile);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btClose);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditProfiles";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.ListView listViewProfile;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colCode;
        private System.Windows.Forms.ColumnHeader colThickness;
        private System.Windows.Forms.Button btCreate;
        private System.Windows.Forms.Button bnModify;
    }
}