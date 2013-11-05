namespace PicParam
{
    partial class OptionPanelComponentViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionPanelComponentViewer));
            this.checkBoxAllowParameterAnimation = new System.Windows.Forms.CheckBox();
            this.lbNoStepsAnimation = new System.Windows.Forms.Label();
            this.nudNoStepsAnimation = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudNoStepsAnimation)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxAllowParameterAnimation
            // 
            resources.ApplyResources(this.checkBoxAllowParameterAnimation, "checkBoxAllowParameterAnimation");
            this.checkBoxAllowParameterAnimation.Name = "checkBoxAllowParameterAnimation";
            this.checkBoxAllowParameterAnimation.UseVisualStyleBackColor = true;
            this.checkBoxAllowParameterAnimation.CheckedChanged += new System.EventHandler(this.checkBoxAllowParameterAnimation_CheckedChanged);
            // 
            // lbNoStepsAnimation
            // 
            resources.ApplyResources(this.lbNoStepsAnimation, "lbNoStepsAnimation");
            this.lbNoStepsAnimation.Name = "lbNoStepsAnimation";
            // 
            // nudNoStepsAnimation
            // 
            resources.ApplyResources(this.nudNoStepsAnimation, "nudNoStepsAnimation");
            this.nudNoStepsAnimation.Name = "nudNoStepsAnimation";
            this.nudNoStepsAnimation.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // OptionPanelComponentViewer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CategoryPath = "Options\\Afficheur composants paramétriques";
            this.Controls.Add(this.nudNoStepsAnimation);
            this.Controls.Add(this.lbNoStepsAnimation);
            this.Controls.Add(this.checkBoxAllowParameterAnimation);
            this.DisplayName = "Afficheur composant paramétriques";
            this.Name = "OptionPanelComponentViewer";
            this.Load += new System.EventHandler(this.OptionPanelComponentViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudNoStepsAnimation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxAllowParameterAnimation;
        private System.Windows.Forms.Label lbNoStepsAnimation;
        private System.Windows.Forms.NumericUpDown nudNoStepsAnimation;
    }
}
