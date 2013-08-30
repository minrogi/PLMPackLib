namespace GLib.Options
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.OptionsFormSplit = new System.Windows.Forms.SplitContainer();
            this.CatDescrPanel = new System.Windows.Forms.Panel();
            this.CatDescr = new System.Windows.Forms.Label();
            this.CatTreePanel = new System.Windows.Forms.Panel();
            this.CatTree = new System.Windows.Forms.TreeView();
            this.CatHeaderPanel = new System.Windows.Forms.Panel();
            this.CatHeader = new System.Windows.Forms.Label();
            this.OptionsPanelPath = new System.Windows.Forms.Label();
            this.OptionsSplitContainer = new System.Windows.Forms.SplitContainer();
            this.OptionPanelContainer = new System.Windows.Forms.Panel();
            this.DescriptionPanel = new System.Windows.Forms.Panel();
            this.OptDescrSplit = new System.Windows.Forms.SplitContainer();
            this.OptionDescrLabel = new System.Windows.Forms.Label();
            this.AppRestartLabel = new System.Windows.Forms.Label();
            this.OKBtn = new System.Windows.Forms.Button();
            this.ApplyBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.OptionsFormSplit)).BeginInit();
            this.OptionsFormSplit.Panel1.SuspendLayout();
            this.OptionsFormSplit.Panel2.SuspendLayout();
            this.OptionsFormSplit.SuspendLayout();
            this.CatDescrPanel.SuspendLayout();
            this.CatTreePanel.SuspendLayout();
            this.CatHeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OptionsSplitContainer)).BeginInit();
            this.OptionsSplitContainer.Panel1.SuspendLayout();
            this.OptionsSplitContainer.Panel2.SuspendLayout();
            this.OptionsSplitContainer.SuspendLayout();
            this.DescriptionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OptDescrSplit)).BeginInit();
            this.OptDescrSplit.Panel1.SuspendLayout();
            this.OptDescrSplit.Panel2.SuspendLayout();
            this.OptDescrSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // OptionsFormSplit
            // 
            resources.ApplyResources(this.OptionsFormSplit, "OptionsFormSplit");
            this.OptionsFormSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.OptionsFormSplit.MinimumSize = new System.Drawing.Size(480, 375);
            this.OptionsFormSplit.Name = "OptionsFormSplit";
            // 
            // OptionsFormSplit.Panel1
            // 
            resources.ApplyResources(this.OptionsFormSplit.Panel1, "OptionsFormSplit.Panel1");
            this.OptionsFormSplit.Panel1.Controls.Add(this.CatDescrPanel);
            this.OptionsFormSplit.Panel1.Controls.Add(this.CatTreePanel);
            this.OptionsFormSplit.Panel1.Controls.Add(this.CatHeaderPanel);
            // 
            // OptionsFormSplit.Panel2
            // 
            resources.ApplyResources(this.OptionsFormSplit.Panel2, "OptionsFormSplit.Panel2");
            this.OptionsFormSplit.Panel2.Controls.Add(this.OptionsPanelPath);
            this.OptionsFormSplit.Panel2.Controls.Add(this.OptionsSplitContainer);
            this.OptionsFormSplit.Panel2.Controls.Add(this.OKBtn);
            this.OptionsFormSplit.Panel2.Controls.Add(this.ApplyBtn);
            this.OptionsFormSplit.Panel2.Controls.Add(this.CancelBtn);
            this.OptionsFormSplit.Panel2.Controls.Add(this.label2);
            // 
            // CatDescrPanel
            // 
            resources.ApplyResources(this.CatDescrPanel, "CatDescrPanel");
            this.CatDescrPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CatDescrPanel.Controls.Add(this.CatDescr);
            this.CatDescrPanel.Name = "CatDescrPanel";
            // 
            // CatDescr
            // 
            resources.ApplyResources(this.CatDescr, "CatDescr");
            this.CatDescr.AutoEllipsis = true;
            this.CatDescr.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CatDescr.Name = "CatDescr";
            this.CatDescr.MouseEnter += new System.EventHandler(this.MouseEnterDescr);
            this.CatDescr.MouseLeave += new System.EventHandler(this.MouseLeaveDescr);
            // 
            // CatTreePanel
            // 
            resources.ApplyResources(this.CatTreePanel, "CatTreePanel");
            this.CatTreePanel.Controls.Add(this.CatTree);
            this.CatTreePanel.Name = "CatTreePanel";
            // 
            // CatTree
            // 
            resources.ApplyResources(this.CatTree, "CatTree");
            this.CatTree.Name = "CatTree";
            this.CatTree.ShowLines = false;
            this.CatTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.CatTree_AfterSelect);
            this.CatTree.Enter += new System.EventHandler(this.MouseEnterDescr);
            this.CatTree.Leave += new System.EventHandler(this.MouseLeaveDescr);
            this.CatTree.MouseEnter += new System.EventHandler(this.MouseEnterDescr);
            this.CatTree.MouseLeave += new System.EventHandler(this.MouseLeaveDescr);
            // 
            // CatHeaderPanel
            // 
            resources.ApplyResources(this.CatHeaderPanel, "CatHeaderPanel");
            this.CatHeaderPanel.Controls.Add(this.CatHeader);
            this.CatHeaderPanel.Name = "CatHeaderPanel";
            // 
            // CatHeader
            // 
            resources.ApplyResources(this.CatHeader, "CatHeader");
            this.CatHeader.AutoEllipsis = true;
            this.CatHeader.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CatHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CatHeader.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CatHeader.Name = "CatHeader";
            this.CatHeader.MouseEnter += new System.EventHandler(this.MouseEnterDescr);
            this.CatHeader.MouseLeave += new System.EventHandler(this.MouseLeaveDescr);
            // 
            // OptionsPanelPath
            // 
            resources.ApplyResources(this.OptionsPanelPath, "OptionsPanelPath");
            this.OptionsPanelPath.AutoEllipsis = true;
            this.OptionsPanelPath.BackColor = System.Drawing.SystemColors.ControlLight;
            this.OptionsPanelPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OptionsPanelPath.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.OptionsPanelPath.Name = "OptionsPanelPath";
            this.OptionsPanelPath.MouseEnter += new System.EventHandler(this.MouseEnterDescr);
            this.OptionsPanelPath.MouseLeave += new System.EventHandler(this.MouseLeaveDescr);
            // 
            // OptionsSplitContainer
            // 
            resources.ApplyResources(this.OptionsSplitContainer, "OptionsSplitContainer");
            this.OptionsSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.OptionsSplitContainer.MinimumSize = new System.Drawing.Size(340, 288);
            this.OptionsSplitContainer.Name = "OptionsSplitContainer";
            // 
            // OptionsSplitContainer.Panel1
            // 
            resources.ApplyResources(this.OptionsSplitContainer.Panel1, "OptionsSplitContainer.Panel1");
            this.OptionsSplitContainer.Panel1.Controls.Add(this.OptionPanelContainer);
            // 
            // OptionsSplitContainer.Panel2
            // 
            resources.ApplyResources(this.OptionsSplitContainer.Panel2, "OptionsSplitContainer.Panel2");
            this.OptionsSplitContainer.Panel2.Controls.Add(this.DescriptionPanel);
            // 
            // OptionPanelContainer
            // 
            resources.ApplyResources(this.OptionPanelContainer, "OptionPanelContainer");
            this.OptionPanelContainer.MinimumSize = new System.Drawing.Size(340, 215);
            this.OptionPanelContainer.Name = "OptionPanelContainer";
            // 
            // DescriptionPanel
            // 
            resources.ApplyResources(this.DescriptionPanel, "DescriptionPanel");
            this.DescriptionPanel.Controls.Add(this.OptDescrSplit);
            this.DescriptionPanel.Name = "DescriptionPanel";
            // 
            // OptDescrSplit
            // 
            resources.ApplyResources(this.OptDescrSplit, "OptDescrSplit");
            this.OptDescrSplit.BackColor = System.Drawing.SystemColors.ControlLight;
            this.OptDescrSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OptDescrSplit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.OptDescrSplit.MaximumSize = new System.Drawing.Size(0, 64);
            this.OptDescrSplit.Name = "OptDescrSplit";
            // 
            // OptDescrSplit.Panel1
            // 
            resources.ApplyResources(this.OptDescrSplit.Panel1, "OptDescrSplit.Panel1");
            this.OptDescrSplit.Panel1.Controls.Add(this.OptionDescrLabel);
            // 
            // OptDescrSplit.Panel2
            // 
            resources.ApplyResources(this.OptDescrSplit.Panel2, "OptDescrSplit.Panel2");
            this.OptDescrSplit.Panel2.Controls.Add(this.AppRestartLabel);
            this.OptDescrSplit.Panel2Collapsed = true;
            // 
            // OptionDescrLabel
            // 
            resources.ApplyResources(this.OptionDescrLabel, "OptionDescrLabel");
            this.OptionDescrLabel.AutoEllipsis = true;
            this.OptionDescrLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.OptionDescrLabel.Name = "OptionDescrLabel";
            this.OptionDescrLabel.MouseEnter += new System.EventHandler(this.MouseEnterDescr);
            this.OptionDescrLabel.MouseLeave += new System.EventHandler(this.MouseLeaveDescr);
            // 
            // AppRestartLabel
            // 
            resources.ApplyResources(this.AppRestartLabel, "AppRestartLabel");
            this.AppRestartLabel.AutoEllipsis = true;
            this.AppRestartLabel.ForeColor = System.Drawing.Color.Crimson;
            this.AppRestartLabel.Name = "AppRestartLabel";
            this.AppRestartLabel.MouseEnter += new System.EventHandler(this.MouseEnterDescr);
            this.AppRestartLabel.MouseLeave += new System.EventHandler(this.MouseLeaveDescr);
            // 
            // OKBtn
            // 
            resources.ApplyResources(this.OKBtn, "OKBtn");
            this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            this.OKBtn.MouseEnter += new System.EventHandler(this.MouseEnterDescr);
            this.OKBtn.MouseLeave += new System.EventHandler(this.MouseLeaveDescr);
            // 
            // ApplyBtn
            // 
            resources.ApplyResources(this.ApplyBtn, "ApplyBtn");
            this.ApplyBtn.Name = "ApplyBtn";
            this.ApplyBtn.UseVisualStyleBackColor = true;
            this.ApplyBtn.Click += new System.EventHandler(this.ApplyBtn_Click);
            this.ApplyBtn.MouseEnter += new System.EventHandler(this.MouseEnterDescr);
            this.ApplyBtn.MouseLeave += new System.EventHandler(this.MouseLeaveDescr);
            // 
            // CancelBtn
            // 
            resources.ApplyResources(this.CancelBtn, "CancelBtn");
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            this.CancelBtn.MouseEnter += new System.EventHandler(this.MouseEnterDescr);
            this.CancelBtn.MouseLeave += new System.EventHandler(this.MouseLeaveDescr);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Name = "label2";
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.OKBtn;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.Controls.Add(this.OptionsFormSplit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
            this.OptionsFormSplit.Panel1.ResumeLayout(false);
            this.OptionsFormSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OptionsFormSplit)).EndInit();
            this.OptionsFormSplit.ResumeLayout(false);
            this.CatDescrPanel.ResumeLayout(false);
            this.CatTreePanel.ResumeLayout(false);
            this.CatHeaderPanel.ResumeLayout(false);
            this.OptionsSplitContainer.Panel1.ResumeLayout(false);
            this.OptionsSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OptionsSplitContainer)).EndInit();
            this.OptionsSplitContainer.ResumeLayout(false);
            this.DescriptionPanel.ResumeLayout(false);
            this.OptDescrSplit.Panel1.ResumeLayout(false);
            this.OptDescrSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OptDescrSplit)).EndInit();
            this.OptDescrSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer OptionsFormSplit;
        private System.Windows.Forms.Panel CatHeaderPanel;
        private System.Windows.Forms.Panel CatDescrPanel;
        private System.Windows.Forms.Panel CatTreePanel;
        private System.Windows.Forms.Label CatHeader;
        private System.Windows.Forms.Label CatDescr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Panel DescriptionPanel;
        private System.Windows.Forms.Label AppRestartLabel;
        private System.Windows.Forms.Label OptionDescrLabel;
        private System.Windows.Forms.Panel OptionPanelContainer;
        private System.Windows.Forms.SplitContainer OptDescrSplit;
        private System.Windows.Forms.Button ApplyBtn;
        private System.Windows.Forms.SplitContainer OptionsSplitContainer;
        private System.Windows.Forms.Label OptionsPanelPath;
        private System.Windows.Forms.TreeView CatTree;
    }
}