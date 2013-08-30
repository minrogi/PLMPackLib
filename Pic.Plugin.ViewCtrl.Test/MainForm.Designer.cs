namespace Pic.Plugin.ViewCtrl.Test
{
    partial class MainForm
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
            this.pluginViewCtrl = new Pic.Plugin.ViewCtrl.PluginViewCtrl();
            this.pluginViewCtrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.pluginViewCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pluginViewCtrl.Location = new System.Drawing.Point(0, 0);
            this.pluginViewCtrl.Name = "PluginViewCtrl1";
            this.pluginViewCtrl.Size = new System.Drawing.Size(1024, 617);
            this.pluginViewCtrl.SplitterDistance = 751;
            this.pluginViewCtrl.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 617);
            this.Controls.Add(this.pluginViewCtrl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.pluginViewCtrl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Pic.Plugin.ViewCtrl.PluginViewCtrl pluginViewCtrl;
    }
}

