namespace PicParam
{
    partial class FormBrowseFile
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
            this._factoryViewer = new Pic.Factory2D.Control.FactoryViewerBase();
            this.SuspendLayout();
            // 
            // _factoryViewer
            // 
            this._factoryViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._factoryViewer.Location = new System.Drawing.Point(0, 0);
            this._factoryViewer.Name = "_factoryViewer";
            this._factoryViewer.ReflectionX = false;
            this._factoryViewer.ReflectionY = false;
            this._factoryViewer.ShowCotations = false;
            this._factoryViewer.Size = new System.Drawing.Size(499, 474);
            this._factoryViewer.TabIndex = 0;
            this._factoryViewer.TabStop = false;
            // 
            // FormBrowseFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 474);
            this.Controls.Add(this._factoryViewer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBrowseFile";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Browse file...";
            this.ResumeLayout(false);
        }
        #endregion
        private Pic.Factory2D.Control.FactoryViewerBase _factoryViewer;
    }
}