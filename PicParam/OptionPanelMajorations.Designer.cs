namespace PicParam
{
    partial class OptionPanelMajorations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionPanelMajorations));
            this.rbRounding1 = new System.Windows.Forms.RadioButton();
            this.rbRounding2 = new System.Windows.Forms.RadioButton();
            this.rbRounding3 = new System.Windows.Forms.RadioButton();
            this.rbRounding4 = new System.Windows.Forms.RadioButton();
            this.gbRounding = new System.Windows.Forms.GroupBox();
            this.gbRounding.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbRounding1
            // 
            resources.ApplyResources(this.rbRounding1, "rbRounding1");
            this.rbRounding1.Name = "rbRounding1";
            this.rbRounding1.TabStop = true;
            this.rbRounding1.UseVisualStyleBackColor = true;
            // 
            // rbRounding2
            // 
            resources.ApplyResources(this.rbRounding2, "rbRounding2");
            this.rbRounding2.Name = "rbRounding2";
            this.rbRounding2.TabStop = true;
            this.rbRounding2.UseVisualStyleBackColor = true;
            // 
            // rbRounding3
            // 
            resources.ApplyResources(this.rbRounding3, "rbRounding3");
            this.rbRounding3.Name = "rbRounding3";
            this.rbRounding3.TabStop = true;
            this.rbRounding3.UseVisualStyleBackColor = true;
            // 
            // rbRounding4
            // 
            resources.ApplyResources(this.rbRounding4, "rbRounding4");
            this.rbRounding4.Name = "rbRounding4";
            this.rbRounding4.TabStop = true;
            this.rbRounding4.UseVisualStyleBackColor = true;
            // 
            // gbRounding
            // 
            resources.ApplyResources(this.gbRounding, "gbRounding");
            this.gbRounding.Controls.Add(this.rbRounding4);
            this.gbRounding.Controls.Add(this.rbRounding3);
            this.gbRounding.Controls.Add(this.rbRounding2);
            this.gbRounding.Controls.Add(this.rbRounding1);
            this.gbRounding.Name = "gbRounding";
            this.gbRounding.TabStop = false;
            // 
            // OptionPanelMajorations
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CategoryPath = "Paramètres\\Majorations";
            this.Controls.Add(this.gbRounding);
            this.DisplayName = "Majorations";
            this.Name = "OptionPanelMajorations";
            this.Load += new System.EventHandler(this.OptionPanelMajorations_Load);
            this.gbRounding.ResumeLayout(false);
            this.gbRounding.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbRounding1;
        private System.Windows.Forms.RadioButton rbRounding2;
        private System.Windows.Forms.RadioButton rbRounding3;
        private System.Windows.Forms.RadioButton rbRounding4;
        private System.Windows.Forms.GroupBox gbRounding;
    }
}
