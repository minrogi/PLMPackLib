namespace Pic.Plugin.GeneratorCtrl
{
    partial class GeneratorCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneratorCtrl));
            this.codeEditorCtrl = new ICSharpCode.TextEditor.TextEditorControl();
            this.fileSelectCtrl = new TreeDim.UserControls.FileSelect();
            this.chbThumbnail = new System.Windows.Forms.CheckBox();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialogBmp = new System.Windows.Forms.OpenFileDialog();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblName = new System.Windows.Forms.Label();
            this.lvwErrors = new System.Windows.Forms.ListView();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGuid = new System.Windows.Forms.TextBox();
            this.bnNewGuid = new System.Windows.Forms.Button();
            this.bnComponentRef = new System.Windows.Forms.Button();
            this.contextMenuStripEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // codeEditorCtrl
            // 
            this.codeEditorCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.codeEditorCtrl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.codeEditorCtrl.ContextMenuStrip = this.contextMenuStripEditor;
            this.codeEditorCtrl.IsReadOnly = false;
            this.codeEditorCtrl.Location = new System.Drawing.Point(0, 125);
            this.codeEditorCtrl.Name = "codeEditorCtrl";
            this.codeEditorCtrl.Size = new System.Drawing.Size(795, 270);
            this.codeEditorCtrl.TabIndex = 0;
            // 
            // fileSelectCtrl
            // 
            this.fileSelectCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileSelectCtrl.Enabled = false;
            this.fileSelectCtrl.Location = new System.Drawing.Point(109, 73);
            this.fileSelectCtrl.Name = "fileSelectCtrl";
            this.fileSelectCtrl.Size = new System.Drawing.Size(374, 20);
            this.fileSelectCtrl.TabIndex = 23;
            // 
            // chbThumbnail
            // 
            this.chbThumbnail.AutoSize = true;
            this.chbThumbnail.Location = new System.Drawing.Point(8, 73);
            this.chbThumbnail.Name = "chbThumbnail";
            this.chbThumbnail.Size = new System.Drawing.Size(75, 17);
            this.chbThumbnail.TabIndex = 22;
            this.chbThumbnail.Text = "Thumbnail";
            this.chbThumbnail.UseVisualStyleBackColor = true;
            this.chbThumbnail.CheckedChanged += new System.EventHandler(this.chbThumbnail_CheckedChanged);
            // 
            // txtCompany
            // 
            this.txtCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompany.Location = new System.Drawing.Point(584, 73);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(211, 20);
            this.txtCompany.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(492, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Company/Author";
            // 
            // openFileDialogBmp
            // 
            this.openFileDialogBmp.FileName = "openFileDialogBmp";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(109, 27);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(688, 42);
            this.txtDescription.TabIndex = 19;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(109, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(688, 20);
            this.txtName.TabIndex = 18;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(4, 27);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 17;
            this.lblDescription.Text = "Description";
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Error";
            this.ColumnHeader1.Width = 320;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Line";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(4, 4);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 16;
            this.lblName.Text = "Name";
            // 
            // lvwErrors
            // 
            this.lvwErrors.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwErrors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.columnHeader2});
            this.lvwErrors.FullRowSelect = true;
            this.lvwErrors.HideSelection = false;
            this.lvwErrors.Location = new System.Drawing.Point(0, 401);
            this.lvwErrors.MultiSelect = false;
            this.lvwErrors.Name = "lvwErrors";
            this.lvwErrors.Size = new System.Drawing.Size(705, 99);
            this.lvwErrors.TabIndex = 14;
            this.lvwErrors.UseCompatibleStateImageBehavior = false;
            this.lvwErrors.View = System.Windows.Forms.View.Details;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(722, 401);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 15;
            this.btnGenerate.Text = "&Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.onGenerate);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Guid";
            // 
            // txtGuid
            // 
            this.txtGuid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGuid.Location = new System.Drawing.Point(109, 98);
            this.txtGuid.Name = "txtGuid";
            this.txtGuid.ReadOnly = true;
            this.txtGuid.Size = new System.Drawing.Size(374, 20);
            this.txtGuid.TabIndex = 25;
            // 
            // bnNewGuid
            // 
            this.bnNewGuid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bnNewGuid.Location = new System.Drawing.Point(495, 98);
            this.bnNewGuid.Name = "bnNewGuid";
            this.bnNewGuid.Size = new System.Drawing.Size(75, 23);
            this.bnNewGuid.TabIndex = 26;
            this.bnNewGuid.Text = "New Guid";
            this.bnNewGuid.UseVisualStyleBackColor = true;
            this.bnNewGuid.Click += new System.EventHandler(this.bnNewGuid_Click);
            // 
            // bnComponentRef
            // 
            this.bnComponentRef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bnComponentRef.Location = new System.Drawing.Point(584, 98);
            this.bnComponentRef.Name = "bnComponentRef";
            this.bnComponentRef.Size = new System.Drawing.Size(129, 23);
            this.bnComponentRef.TabIndex = 27;
            this.bnComponentRef.Text = "Insert component ref.";
            this.bnComponentRef.UseVisualStyleBackColor = true;
            this.bnComponentRef.Click += new System.EventHandler(this.bnComponentRef_Click);
            // 
            // contextMenuStripEditor
            // 
            this.contextMenuStripEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator1,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.contextMenuStripEditor.Name = "contextMenuStripEditor";
            this.contextMenuStripEditor.Size = new System.Drawing.Size(153, 142);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripMenuItem.Image")));
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("redoToolStripMenuItem.Image")));
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // GeneratorCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bnComponentRef);
            this.Controls.Add(this.bnNewGuid);
            this.Controls.Add(this.txtGuid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fileSelectCtrl);
            this.Controls.Add(this.chbThumbnail);
            this.Controls.Add(this.txtCompany);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.lvwErrors);
            this.Controls.Add(this.codeEditorCtrl);
            this.Name = "GeneratorCtrl";
            this.Size = new System.Drawing.Size(800, 500);
            this.contextMenuStripEditor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbThumbnail;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialogBmp;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.ColumnHeader ColumnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ListView lvwErrors;
        private ICSharpCode.TextEditor.TextEditorControl codeEditorCtrl;
        private TreeDim.UserControls.FileSelect fileSelectCtrl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGuid;
        private System.Windows.Forms.Button bnNewGuid;
        private System.Windows.Forms.Button bnComponentRef;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripEditor;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
    }
}
