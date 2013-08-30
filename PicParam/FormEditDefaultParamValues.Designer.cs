namespace PicParam
{
    partial class FormEditDefaultParamValues
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditDefaultParamValues));
            this._pluginViewCtrl = new Pic.Plugin.ViewCtrl.PluginViewCtrl();
            this.lbComponent = new System.Windows.Forms.Label();
            this.cbComponent = new System.Windows.Forms.ComboBox();
            this.lbDescription = new System.Windows.Forms.Label();
            this.bnOk = new System.Windows.Forms.Button();
            this.bnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._pluginViewCtrl)).BeginInit();
            this._pluginViewCtrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // _pluginViewCtrl
            // 
            resources.ApplyResources(this._pluginViewCtrl, "_pluginViewCtrl");
            this._pluginViewCtrl.CloseButtonVisible = false;
            this._pluginViewCtrl.Name = "_pluginViewCtrl";
            // 
            // _pluginViewCtrl.Panel1
            // 
            resources.ApplyResources(this._pluginViewCtrl.Panel1, "_pluginViewCtrl.Panel1");
            // 
            // _pluginViewCtrl.Panel2
            // 
            resources.ApplyResources(this._pluginViewCtrl.Panel2, "_pluginViewCtrl.Panel2");
            this._pluginViewCtrl.ParamValues = null;
            this._pluginViewCtrl.ReflectionX = false;
            this._pluginViewCtrl.ReflectionY = false;
            this._pluginViewCtrl.ShowCotations = true;
            this._pluginViewCtrl.ShowSummary = true;
            this._pluginViewCtrl.ValidateButtonVisible = false;
            // 
            // lbComponent
            // 
            resources.ApplyResources(this.lbComponent, "lbComponent");
            this.lbComponent.Name = "lbComponent";
            // 
            // cbComponent
            // 
            resources.ApplyResources(this.cbComponent, "cbComponent");
            this.cbComponent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComponent.FormattingEnabled = true;
            this.cbComponent.Name = "cbComponent";
            this.cbComponent.SelectedIndexChanged += new System.EventHandler(this.cbComponent_SelectedIndexChanged);
            // 
            // lbDescription
            // 
            resources.ApplyResources(this.lbDescription, "lbDescription");
            this.lbDescription.Name = "lbDescription";
            // 
            // bnOk
            // 
            resources.ApplyResources(this.bnOk, "bnOk");
            this.bnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bnOk.Name = "bnOk";
            this.bnOk.UseVisualStyleBackColor = true;
            this.bnOk.Click += new System.EventHandler(this.bnOk_Click);
            // 
            // bnCancel
            // 
            resources.ApplyResources(this.bnCancel, "bnCancel");
            this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.UseVisualStyleBackColor = true;
            // 
            // FormEditDefaultParamValues
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bnCancel);
            this.Controls.Add(this.bnOk);
            this.Controls.Add(this.lbDescription);
            this.Controls.Add(this.cbComponent);
            this.Controls.Add(this.lbComponent);
            this.Controls.Add(this._pluginViewCtrl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditDefaultParamValues";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEditDefaultParamValues_FormClosing);
            this.Load += new System.EventHandler(this.FormEditDefaultParamValues_Load);
            ((System.ComponentModel.ISupportInitialize)(this._pluginViewCtrl)).EndInit();
            this._pluginViewCtrl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private Pic.Plugin.ViewCtrl.PluginViewCtrl _pluginViewCtrl;
        private System.Windows.Forms.Label lbComponent;
        private System.Windows.Forms.ComboBox cbComponent;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.Button bnOk;
        private System.Windows.Forms.Button bnCancel;
    }
}