namespace PicParam
{
    partial class FormSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearch));
            this.bnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.chkSearchNameOnly = new System.Windows.Forms.CheckBox();
            this.lbMinTextLength = new System.Windows.Forms.Label();
            this.lbResults = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // bnCancel
            // 
            resources.ApplyResources(this.bnCancel, "bnCancel");
            this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tbSearch
            // 
            resources.ApplyResources(this.tbSearch, "tbSearch");
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.TextChanged += new System.EventHandler(this.onTextChanged);
            // 
            // chkSearchNameOnly
            // 
            resources.ApplyResources(this.chkSearchNameOnly, "chkSearchNameOnly");
            this.chkSearchNameOnly.Name = "chkSearchNameOnly";
            this.chkSearchNameOnly.UseVisualStyleBackColor = true;
            // 
            // lbMinTextLength
            // 
            resources.ApplyResources(this.lbMinTextLength, "lbMinTextLength");
            this.lbMinTextLength.Name = "lbMinTextLength";
            // 
            // lbResults
            // 
            resources.ApplyResources(this.lbResults, "lbResults");
            this.lbResults.FormattingEnabled = true;
            this.lbResults.Name = "lbResults";
            this.lbResults.SelectedIndexChanged += new System.EventHandler(this.onItemSelected);
            // 
            // FormSearch
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bnCancel;
            this.Controls.Add(this.lbResults);
            this.Controls.Add(this.lbMinTextLength);
            this.Controls.Add(this.chkSearchNameOnly);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSearch";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.CheckBox chkSearchNameOnly;
        private System.Windows.Forms.Label lbMinTextLength;
        private System.Windows.Forms.ListBox lbResults;
    }
}