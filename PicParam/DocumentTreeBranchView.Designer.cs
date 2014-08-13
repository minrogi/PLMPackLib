namespace PicParam
{
    partial class DocumentTreeBranchView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentTreeBranchView));
            this.contextMenuStripDefault = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewBranchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripDefault.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripDefault
            // 
            resources.ApplyResources(this.contextMenuStripDefault, "contextMenuStripDefault");
            this.contextMenuStripDefault.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewBranchToolStripMenuItem,
            this.addNewDocumentToolStripMenuItem});
            this.contextMenuStripDefault.Name = "contextMenuStrip1";
            // 
            // addNewBranchToolStripMenuItem
            // 
            resources.ApplyResources(this.addNewBranchToolStripMenuItem, "addNewBranchToolStripMenuItem");
            this.addNewBranchToolStripMenuItem.Name = "addNewBranchToolStripMenuItem";
            this.addNewBranchToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItemNewBranch);
            // 
            // addNewDocumentToolStripMenuItem
            // 
            resources.ApplyResources(this.addNewDocumentToolStripMenuItem, "addNewDocumentToolStripMenuItem");
            this.addNewDocumentToolStripMenuItem.Name = "addNewDocumentToolStripMenuItem";
            this.addNewDocumentToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItemNewDocument);
            // 
            // DocumentTreeBranchView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStripDefault;
            this.Name = "DocumentTreeBranchView";
            this.contextMenuStripDefault.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripDefault;
        private System.Windows.Forms.ToolStripMenuItem addNewBranchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewDocumentToolStripMenuItem;
    }
}
