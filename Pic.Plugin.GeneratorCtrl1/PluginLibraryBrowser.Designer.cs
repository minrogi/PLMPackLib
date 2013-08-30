namespace Pic.Plugin.GeneratorCtrl
{
    partial class PluginLibraryBrowser
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
            this.browserCtrl = new Pic.Plugin.GeneratorCtrl.PluginLibraryBrowserCtrl();
            this.SuspendLayout();
            // 
            // browserCtrl
            // 
            this.browserCtrl.AutoScroll = true;
            this.browserCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserCtrl.Location = new System.Drawing.Point(0, 0);
            this.browserCtrl.Name = "browserCtrl";
            this.browserCtrl.Size = new System.Drawing.Size(558, 475);
            this.browserCtrl.TabIndex = 0;
            this.browserCtrl.ComponentSelected += new Pic.Plugin.GeneratorCtrl.PluginLibraryBrowserCtrl.ComponentSelectHandler(this.onComponentSelected);
            // 
            // PluginLibraryBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 475);
            this.Controls.Add(this.browserCtrl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluginLibraryBrowser";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Select a component to insert...";
            this.ResumeLayout(false);

        }

        #endregion

        private PluginLibraryBrowserCtrl browserCtrl;
    }
}