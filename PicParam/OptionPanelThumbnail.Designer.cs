namespace PicParam
{
    partial class OptionPanelThumbnail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionPanelThumbnail));
            this.gbAnnotations = new System.Windows.Forms.GroupBox();
            this.nudFontSize = new System.Windows.Forms.NumericUpDown();
            this.lbFontSize = new System.Windows.Forms.Label();
            this.rbAnnotations3 = new System.Windows.Forms.RadioButton();
            this.rbAnnotations2 = new System.Windows.Forms.RadioButton();
            this.rbAnnotations1 = new System.Windows.Forms.RadioButton();
            this.gbAnnotations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // gbAnnotations
            // 
            resources.ApplyResources(this.gbAnnotations, "gbAnnotations");
            this.gbAnnotations.Controls.Add(this.nudFontSize);
            this.gbAnnotations.Controls.Add(this.lbFontSize);
            this.gbAnnotations.Controls.Add(this.rbAnnotations3);
            this.gbAnnotations.Controls.Add(this.rbAnnotations2);
            this.gbAnnotations.Controls.Add(this.rbAnnotations1);
            this.gbAnnotations.Name = "gbAnnotations";
            this.gbAnnotations.TabStop = false;
            // 
            // nudFontSize
            // 
            resources.ApplyResources(this.nudFontSize, "nudFontSize");
            this.nudFontSize.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudFontSize.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudFontSize.Name = "nudFontSize";
            this.nudFontSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lbFontSize
            // 
            resources.ApplyResources(this.lbFontSize, "lbFontSize");
            this.lbFontSize.Name = "lbFontSize";
            // 
            // rbAnnotations3
            // 
            resources.ApplyResources(this.rbAnnotations3, "rbAnnotations3");
            this.rbAnnotations3.Name = "rbAnnotations3";
            this.rbAnnotations3.TabStop = true;
            this.rbAnnotations3.UseVisualStyleBackColor = true;
            // 
            // rbAnnotations2
            // 
            resources.ApplyResources(this.rbAnnotations2, "rbAnnotations2");
            this.rbAnnotations2.Name = "rbAnnotations2";
            this.rbAnnotations2.TabStop = true;
            this.rbAnnotations2.UseVisualStyleBackColor = true;
            // 
            // rbAnnotations1
            // 
            resources.ApplyResources(this.rbAnnotations1, "rbAnnotations1");
            this.rbAnnotations1.Name = "rbAnnotations1";
            this.rbAnnotations1.TabStop = true;
            this.rbAnnotations1.UseVisualStyleBackColor = true;
            // 
            // OptionPanelThumbnail
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CategoryPath = "Paramètres\\Miniatures";
            this.Controls.Add(this.gbAnnotations);
            this.DisplayName = "Miniatures";
            this.Name = "OptionPanelThumbnail";
            this.Load += new System.EventHandler(this.OptionPanelThumbnail_Load);
            this.gbAnnotations.ResumeLayout(false);
            this.gbAnnotations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbAnnotations;
        private System.Windows.Forms.NumericUpDown nudFontSize;
        private System.Windows.Forms.Label lbFontSize;
        private System.Windows.Forms.RadioButton rbAnnotations3;
        private System.Windows.Forms.RadioButton rbAnnotations2;
        private System.Windows.Forms.RadioButton rbAnnotations1;
    }
}
