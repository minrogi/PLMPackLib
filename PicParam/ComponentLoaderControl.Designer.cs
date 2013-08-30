namespace PicParam
{
    partial class ComponentLoaderControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComponentLoaderControl));
            this.pluginViewCtrl = new Pic.Plugin.ViewCtrl.PluginViewCtrl();
            this.lblProfile = new System.Windows.Forms.Label();
            this.comboBoxProfile = new System.Windows.Forms.ComboBox();
            this.groupBoxMajorations = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pluginViewCtrl)).BeginInit();
            this.pluginViewCtrl.SuspendLayout();
            this.groupBoxMajorations.SuspendLayout();
            this.SuspendLayout();
            // 
            // pluginViewCtrl
            // 
            resources.ApplyResources(this.pluginViewCtrl, "pluginViewCtrl");
            this.pluginViewCtrl.CloseButtonVisible = false;
            this.pluginViewCtrl.Name = "pluginViewCtrl";
            // 
            // pluginViewCtrl.Panel1
            // 
            resources.ApplyResources(this.pluginViewCtrl.Panel1, "pluginViewCtrl.Panel1");
            // 
            // pluginViewCtrl.Panel2
            // 
            resources.ApplyResources(this.pluginViewCtrl.Panel2, "pluginViewCtrl.Panel2");
            this.pluginViewCtrl.ParamValues = null;
            this.pluginViewCtrl.ReflectionX = false;
            this.pluginViewCtrl.ReflectionY = false;
            this.pluginViewCtrl.ShowCotations = true;
            this.pluginViewCtrl.ShowSummary = false;
            this.pluginViewCtrl.ValidateButtonVisible = false;
            // 
            // lblProfile
            // 
            resources.ApplyResources(this.lblProfile, "lblProfile");
            this.lblProfile.Name = "lblProfile";
            // 
            // comboBoxProfile
            // 
            resources.ApplyResources(this.comboBoxProfile, "comboBoxProfile");
            this.comboBoxProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfile.FormattingEnabled = true;
            this.comboBoxProfile.Name = "comboBoxProfile";
            // 
            // groupBoxMajorations
            // 
            resources.ApplyResources(this.groupBoxMajorations, "groupBoxMajorations");
            this.groupBoxMajorations.Controls.Add(this.lblProfile);
            this.groupBoxMajorations.Controls.Add(this.comboBoxProfile);
            this.groupBoxMajorations.Name = "groupBoxMajorations";
            this.groupBoxMajorations.TabStop = false;
            // 
            // ComponentLoaderControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxMajorations);
            this.Controls.Add(this.pluginViewCtrl);
            this.Name = "ComponentLoaderControl";
            ((System.ComponentModel.ISupportInitialize)(this.pluginViewCtrl)).EndInit();
            this.pluginViewCtrl.ResumeLayout(false);
            this.groupBoxMajorations.ResumeLayout(false);
            this.groupBoxMajorations.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblProfile;
        private System.Windows.Forms.ComboBox comboBoxProfile;
        private System.Windows.Forms.GroupBox groupBoxMajorations;
        private Pic.Plugin.ViewCtrl.PluginViewCtrl pluginViewCtrl;
    }
}
