namespace PicParam
{
    partial class FormEditMajorations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditMajorations));
            this.bnEditProfiles = new System.Windows.Forms.Button();
            this.lblProfile = new System.Windows.Forms.Label();
            this.comboBoxProfile = new System.Windows.Forms.ComboBox();
            this.bnOK = new System.Windows.Forms.Button();
            this.bnApply = new System.Windows.Forms.Button();
            this.bnCancel = new System.Windows.Forms.Button();
            this._pb = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._pb)).BeginInit();
            this.SuspendLayout();
            // 
            // bnEditProfiles
            // 
            resources.ApplyResources(this.bnEditProfiles, "bnEditProfiles");
            this.bnEditProfiles.Name = "bnEditProfiles";
            this.bnEditProfiles.UseVisualStyleBackColor = true;
            this.bnEditProfiles.Click += new System.EventHandler(this.bnEditProfiles_Click);
            // 
            // lblProfile
            // 
            resources.ApplyResources(this.lblProfile, "lblProfile");
            this.lblProfile.Name = "lblProfile";
            // 
            // comboBoxProfile
            // 
            resources.ApplyResources(this.comboBoxProfile, "comboBoxProfile");
            this.comboBoxProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfile.FormattingEnabled = true;
            this.comboBoxProfile.Name = "comboBoxProfile";
            this.comboBoxProfile.SelectedIndexChanged += new System.EventHandler(this.comboBoxProfile_selectedIndexChanged);
            // 
            // bnOK
            // 
            resources.ApplyResources(this.bnOK, "bnOK");
            this.bnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bnOK.Name = "bnOK";
            this.bnOK.UseVisualStyleBackColor = true;
            this.bnOK.Click += new System.EventHandler(this.bnOK_Click);
            // 
            // bnApply
            // 
            resources.ApplyResources(this.bnApply, "bnApply");
            this.bnApply.Name = "bnApply";
            this.bnApply.UseVisualStyleBackColor = true;
            this.bnApply.Click += new System.EventHandler(this.bnApply_Click);
            // 
            // bnCancel
            // 
            resources.ApplyResources(this.bnCancel, "bnCancel");
            this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.UseVisualStyleBackColor = true;
            // 
            // _pb
            // 
            resources.ApplyResources(this._pb, "_pb");
            this._pb.Name = "_pb";
            this._pb.TabStop = false;
            // 
            // FormEditMajorations
            // 
            this.AcceptButton = this.bnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bnCancel;
            this.Controls.Add(this._pb);
            this.Controls.Add(this.bnCancel);
            this.Controls.Add(this.bnApply);
            this.Controls.Add(this.bnOK);
            this.Controls.Add(this.comboBoxProfile);
            this.Controls.Add(this.lblProfile);
            this.Controls.Add(this.bnEditProfiles);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditMajorations";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.EditMajorationsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._pb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Button bnEditProfiles;
        private System.Windows.Forms.Label lblProfile;
        private System.Windows.Forms.ComboBox comboBoxProfile;
        private System.Windows.Forms.Button bnOK;
        private System.Windows.Forms.Button bnApply;
        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.PictureBox _pb;
    }
}