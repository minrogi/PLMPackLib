namespace PicParam
{
    partial class OptionPanelDebug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionPanelDebug));
            this.checkBoxDebug = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBoxDebug
            // 
            resources.ApplyResources(this.checkBoxDebug, "checkBoxDebug");
            this.checkBoxDebug.Name = "checkBoxDebug";
            this.checkBoxDebug.UseVisualStyleBackColor = true;
            // 
            // OptionPanelDebug
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CategoryPath = "Paramètres\\Outils de débogage";
            this.Controls.Add(this.checkBoxDebug);
            this.DisplayName = "Outils de débogages";
            this.Name = "OptionPanelDebug";
            this.Load += new System.EventHandler(this.OptionPanelDebug_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxDebug;
    }
}
