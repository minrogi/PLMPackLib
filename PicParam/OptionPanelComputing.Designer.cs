namespace PicParam
{
    partial class OptionPanelComputing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionPanelComputing));
            this.lbMaximumNumberOfEntities = new System.Windows.Forms.Label();
            this.nudMaxNumberOfEntities = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxNumberOfEntities)).BeginInit();
            this.SuspendLayout();
            // 
            // lbMaximumNumberOfEntities
            // 
            resources.ApplyResources(this.lbMaximumNumberOfEntities, "lbMaximumNumberOfEntities");
            this.lbMaximumNumberOfEntities.Name = "lbMaximumNumberOfEntities";
            // 
            // nudMaxNumberOfEntities
            // 
            resources.ApplyResources(this.nudMaxNumberOfEntities, "nudMaxNumberOfEntities");
            this.nudMaxNumberOfEntities.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudMaxNumberOfEntities.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudMaxNumberOfEntities.Name = "nudMaxNumberOfEntities";
            this.nudMaxNumberOfEntities.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // OptionPanelComputing
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CategoryPath = "Paramètres\\Performance";
            this.Controls.Add(this.nudMaxNumberOfEntities);
            this.Controls.Add(this.lbMaximumNumberOfEntities);
            this.DisplayName = "Performance";
            this.Name = "OptionPanelComputing";
            this.Load += new System.EventHandler(this.OptionPanelComputing_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxNumberOfEntities)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbMaximumNumberOfEntities;
        private System.Windows.Forms.NumericUpDown nudMaxNumberOfEntities;
    }
}
