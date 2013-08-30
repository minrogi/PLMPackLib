namespace PicParam
{
    partial class FormPluginEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPluginEditor));
            this._generatorCtrl = new Pic.Plugin.GeneratorCtrl.GeneratorCtrl();
            this.SuspendLayout();
            // 
            // _generatorCtrl
            // 
            resources.ApplyResources(this._generatorCtrl, "_generatorCtrl");
            this._generatorCtrl.Name = "_generatorCtrl";
            this._generatorCtrl.OutputPath = "C:\\Users\\HidaS\\AppData\\Local\\Temp\\.dll";
            this._generatorCtrl.PluginVersion = "2.0.0.0";
            this._generatorCtrl.TabStop = false;
            this._generatorCtrl.Load += new System.EventHandler(this._generatorCtrl_Load);
            // 
            // FormPluginEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._generatorCtrl);
            this.Name = "FormPluginEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private Pic.Plugin.GeneratorCtrl.GeneratorCtrl _generatorCtrl;
    }
}