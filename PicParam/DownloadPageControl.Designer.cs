﻿namespace PicParam
{
    partial class DownloadPageControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadPageControl));
            this.bnMerge = new System.Windows.Forms.Button();
            this.bnOverwrite = new System.Windows.Forms.Button();
            this.listBoxLibraries = new ListBoxLibraries();
            this.SuspendLayout();
            // 
            // bnMerge
            // 
            resources.ApplyResources(this.bnMerge, "bnMerge");
            this.bnMerge.Name = "bnMerge";
            this.bnMerge.UseVisualStyleBackColor = true;
            this.bnMerge.Click += new System.EventHandler(this.bnMerge_Click);
            // 
            // bnOverwrite
            // 
            resources.ApplyResources(this.bnOverwrite, "bnOverwrite");
            this.bnOverwrite.Name = "bnOverwrite";
            this.bnOverwrite.UseVisualStyleBackColor = true;
            this.bnOverwrite.Click += new System.EventHandler(this.bnOverwrite_Click);
            // 
            // listBoxLibraries
            // 
            resources.ApplyResources(this.listBoxLibraries, "listBoxLibraries");
            this.listBoxLibraries.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxLibraries.FormattingEnabled = true;
            this.listBoxLibraries.MinimumSize = new System.Drawing.Size(20, 20);
            this.listBoxLibraries.Name = "listBoxLibraries";
            // 
            // DownloadPageControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBoxLibraries);
            this.Controls.Add(this.bnOverwrite);
            this.Controls.Add(this.bnMerge);
            this.Name = "DownloadPageControl";
            this.ResumeLayout(false);

        }

        #endregion

        private ListBoxLibraries listBoxLibraries;
        private System.Windows.Forms.Button bnMerge;
        private System.Windows.Forms.Button bnOverwrite;
    }
}