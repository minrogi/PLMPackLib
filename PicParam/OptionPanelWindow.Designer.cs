namespace PicParam
{
    partial class OptionPanelWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionPanelWindow));
            this.checkBoxMaximized = new System.Windows.Forms.CheckBox();
            this.checkBoxCenteredTitleBar = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBoxMaximized
            // 
            resources.ApplyResources(this.checkBoxMaximized, "checkBoxMaximized");
            this.checkBoxMaximized.Name = "checkBoxMaximized";
            this.checkBoxMaximized.UseVisualStyleBackColor = true;
            // 
            // checkBoxCenteredTitleBar
            // 
            resources.ApplyResources(this.checkBoxCenteredTitleBar, "checkBoxCenteredTitleBar");
            this.checkBoxCenteredTitleBar.Name = "checkBoxCenteredTitleBar";
            this.checkBoxCenteredTitleBar.UseVisualStyleBackColor = true;
            // 
            // OptionPanelWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CategoryPath = "Paramètres\\Fenêtres";
            this.Controls.Add(this.checkBoxCenteredTitleBar);
            this.Controls.Add(this.checkBoxMaximized);
            this.DisplayName = "Fenêtres";
            this.Name = "OptionPanelWindow";
            this.Load += new System.EventHandler(this.OptionPanelWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxMaximized;
        private System.Windows.Forms.CheckBox checkBoxCenteredTitleBar;
    }
}
