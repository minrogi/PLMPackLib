namespace Pic.Plugin.GeneratorCtrl.Test
{
    partial class FormMain
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
            this.generatorCtrl = new Pic.Plugin.GeneratorCtrl.GeneratorCtrl();
            this.SuspendLayout();
            // 
            // generatorCtrl
            //
            this.generatorCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generatorCtrl.Location = new System.Drawing.Point(0, 0);
            this.generatorCtrl.MinimumSize = new System.Drawing.Size(20, 20);
            this.generatorCtrl.Name = "generatorCtrl";
            this.generatorCtrl.Size = new System.Drawing.Size(680, 512);
            this.generatorCtrl.TabIndex = 0;
            // 
            // GeneratorControlTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 512);
            this.Controls.Add(this.generatorCtrl);
            this.Name = "GeneratorControlTestForm";
            this.Text = "Generator control test form";
            this.ResumeLayout(false);
        }

        void generatorCtrl_CloseClicked(object sender, System.EventArgs e)
        {
            Close();
        }
        #endregion

        private Pic.Plugin.GeneratorCtrl.GeneratorCtrl generatorCtrl;
    }
}

