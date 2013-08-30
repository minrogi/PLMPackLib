namespace Pic.Plugin.GeneratorCtrl
{
    partial class PluginViewer
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
            this.ctrlPluginViewer = new Pic.Plugin.ViewCtrl.PluginViewCtrl();
            this.ctrlPluginViewer.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlPluginViewer
            // 
            this.ctrlPluginViewer.CloseButtonVisible = true;
            this.ctrlPluginViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlPluginViewer.Location = new System.Drawing.Point(0, 0);
            this.ctrlPluginViewer.Name = "ctrlPluginViewer";
            this.ctrlPluginViewer.ReflectionX = false;
            this.ctrlPluginViewer.ReflectionY = false;
            this.ctrlPluginViewer.ShowCotations = true;
            this.ctrlPluginViewer.ShowSummary = true;
            this.ctrlPluginViewer.Size = new System.Drawing.Size(649, 520);
            this.ctrlPluginViewer.SplitterDistance = 434;
            this.ctrlPluginViewer.SplitterWidth = 1;
            this.ctrlPluginViewer.TabIndex = 0;
            // 
            // PluginViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.ctrlPluginViewer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluginViewer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Validate generated component...";
            this.ResumeLayout(false);
        }

        #endregion
        private Pic.Plugin.ViewCtrl.PluginViewCtrl ctrlPluginViewer;
    }
}