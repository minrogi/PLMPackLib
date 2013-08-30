namespace PicParam
{
    partial class StartPageControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartPageControl));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.webBrowserStartPage = new System.Windows.Forms.WebBrowser();
            this.button1 = new System.Windows.Forms.Button();
            this.bnMerge = new System.Windows.Forms.Button();
            this.listBoxLibraries = new ListBoxLibraries();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.MinimumSize = new System.Drawing.Size(20, 20);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.webBrowserStartPage);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.bnMerge);
            this.splitContainer1.Panel2.Controls.Add(this.listBoxLibraries);
            // 
            // webBrowserStartPage
            // 
            resources.ApplyResources(this.webBrowserStartPage, "webBrowserStartPage");
            this.webBrowserStartPage.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserStartPage.Name = "webBrowserStartPage";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.bnOverwrite_Click);
            // 
            // bnMerge
            // 
            resources.ApplyResources(this.bnMerge, "bnMerge");
            this.bnMerge.Name = "bnMerge";
            this.bnMerge.UseVisualStyleBackColor = true;
            this.bnMerge.Click += new System.EventHandler(this.bnMerge_Click);
            // 
            // listBoxLibraries
            // 
            resources.ApplyResources(this.listBoxLibraries, "listBoxLibraries");
            this.listBoxLibraries.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxLibraries.FormattingEnabled = true;
            this.listBoxLibraries.MinimumSize = new System.Drawing.Size(20, 20);
            this.listBoxLibraries.Name = "listBoxLibraries";
            this.listBoxLibraries.SelectedIndexChanged += new System.EventHandler(this.listBoxLibraries_SelectedIndexChanged);
            // 
            // StartPageControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "StartPageControl";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser webBrowserStartPage;
        private ListBoxLibraries listBoxLibraries;
        private System.Windows.Forms.Button bnMerge;
        private System.Windows.Forms.Button button1;
    }
}
