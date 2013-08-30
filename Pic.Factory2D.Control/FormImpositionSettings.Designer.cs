namespace Pic.Factory2D.Control
{
    partial class FormImpositionSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImpositionSettings));
            this.bnOk = new System.Windows.Forms.Button();
            this.bnCancel = new System.Windows.Forms.Button();
            this.cbAllowRowRotation = new System.Windows.Forms.CheckBox();
            this.cbAllowColumnRotation = new System.Windows.Forms.CheckBox();
            this.lblCardboardFormat = new System.Windows.Forms.Label();
            this.cbCardboardFormat = new System.Windows.Forms.ComboBox();
            this.bnEditCardboardFormats = new System.Windows.Forms.Button();
            this.nudOffsetY = new System.Windows.Forms.NumericUpDown();
            this.nudOffsetX = new System.Windows.Forms.NumericUpDown();
            this.lbOffsetY = new System.Windows.Forms.Label();
            this.lbOffsetX = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabAllowedPatterns = new System.Windows.Forms.TabPage();
            this.tabOffsets = new System.Windows.Forms.TabPage();
            this.tabMargins = new System.Windows.Forms.TabPage();
            this.lbmm5 = new System.Windows.Forms.Label();
            this.lbmm3 = new System.Windows.Forms.Label();
            this.lbmm1 = new System.Windows.Forms.Label();
            this.nudSpaceBetween = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.lbmm4 = new System.Windows.Forms.Label();
            this.lbmm2 = new System.Windows.Forms.Label();
            this.nudLeftRightMargin = new System.Windows.Forms.NumericUpDown();
            this.lbRemainingLeftRight = new System.Windows.Forms.Label();
            this.nudTopBottomRemaining = new System.Windows.Forms.NumericUpDown();
            this.lbRemainingBottomTop = new System.Windows.Forms.Label();
            this.nudLeftRightRemaining = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cbRightLeft = new System.Windows.Forms.ComboBox();
            this.nudTopBottomMargin = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTopBottom = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetX)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabAllowedPatterns.SuspendLayout();
            this.tabOffsets.SuspendLayout();
            this.tabMargins.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpaceBetween)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftRightMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopBottomRemaining)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftRightRemaining)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopBottomMargin)).BeginInit();
            this.SuspendLayout();
            // 
            // bnOk
            // 
            resources.ApplyResources(this.bnOk, "bnOk");
            this.bnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bnOk.Name = "bnOk";
            this.bnOk.UseVisualStyleBackColor = true;
            // 
            // bnCancel
            // 
            resources.ApplyResources(this.bnCancel, "bnCancel");
            this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.UseVisualStyleBackColor = true;
            // 
            // cbAllowRowRotation
            // 
            resources.ApplyResources(this.cbAllowRowRotation, "cbAllowRowRotation");
            this.cbAllowRowRotation.Checked = true;
            this.cbAllowRowRotation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllowRowRotation.Name = "cbAllowRowRotation";
            this.cbAllowRowRotation.UseVisualStyleBackColor = true;
            // 
            // cbAllowColumnRotation
            // 
            resources.ApplyResources(this.cbAllowColumnRotation, "cbAllowColumnRotation");
            this.cbAllowColumnRotation.Checked = true;
            this.cbAllowColumnRotation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllowColumnRotation.Name = "cbAllowColumnRotation";
            this.cbAllowColumnRotation.UseVisualStyleBackColor = true;
            // 
            // lblCardboardFormat
            // 
            resources.ApplyResources(this.lblCardboardFormat, "lblCardboardFormat");
            this.lblCardboardFormat.Name = "lblCardboardFormat";
            // 
            // cbCardboardFormat
            // 
            resources.ApplyResources(this.cbCardboardFormat, "cbCardboardFormat");
            this.cbCardboardFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCardboardFormat.FormattingEnabled = true;
            this.cbCardboardFormat.Name = "cbCardboardFormat";
            this.cbCardboardFormat.SelectedIndexChanged += new System.EventHandler(this.cbCardboardFormat_SelectedIndexChanged);
            // 
            // bnEditCardboardFormats
            // 
            resources.ApplyResources(this.bnEditCardboardFormats, "bnEditCardboardFormats");
            this.bnEditCardboardFormats.Name = "bnEditCardboardFormats";
            this.bnEditCardboardFormats.UseVisualStyleBackColor = true;
            this.bnEditCardboardFormats.Click += new System.EventHandler(this.bnEditCardboardFormats_Click);
            // 
            // nudOffsetY
            // 
            this.nudOffsetY.DecimalPlaces = 1;
            resources.ApplyResources(this.nudOffsetY, "nudOffsetY");
            this.nudOffsetY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudOffsetY.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudOffsetY.Name = "nudOffsetY";
            // 
            // nudOffsetX
            // 
            this.nudOffsetX.DecimalPlaces = 1;
            resources.ApplyResources(this.nudOffsetX, "nudOffsetX");
            this.nudOffsetX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudOffsetX.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudOffsetX.Name = "nudOffsetX";
            // 
            // lbOffsetY
            // 
            resources.ApplyResources(this.lbOffsetY, "lbOffsetY");
            this.lbOffsetY.Name = "lbOffsetY";
            // 
            // lbOffsetX
            // 
            resources.ApplyResources(this.lbOffsetX, "lbOffsetX");
            this.lbOffsetX.Name = "lbOffsetX";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabAllowedPatterns);
            this.tabControl.Controls.Add(this.tabOffsets);
            this.tabControl.Controls.Add(this.tabMargins);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabAllowedPatterns
            // 
            this.tabAllowedPatterns.Controls.Add(this.cbAllowColumnRotation);
            this.tabAllowedPatterns.Controls.Add(this.cbAllowRowRotation);
            resources.ApplyResources(this.tabAllowedPatterns, "tabAllowedPatterns");
            this.tabAllowedPatterns.Name = "tabAllowedPatterns";
            this.tabAllowedPatterns.UseVisualStyleBackColor = true;
            // 
            // tabOffsets
            // 
            this.tabOffsets.Controls.Add(this.nudOffsetY);
            this.tabOffsets.Controls.Add(this.lbOffsetX);
            this.tabOffsets.Controls.Add(this.nudOffsetX);
            this.tabOffsets.Controls.Add(this.lbOffsetY);
            resources.ApplyResources(this.tabOffsets, "tabOffsets");
            this.tabOffsets.Name = "tabOffsets";
            this.tabOffsets.UseVisualStyleBackColor = true;
            // 
            // tabMargins
            // 
            this.tabMargins.Controls.Add(this.lbmm5);
            this.tabMargins.Controls.Add(this.lbmm3);
            this.tabMargins.Controls.Add(this.lbmm1);
            this.tabMargins.Controls.Add(this.nudSpaceBetween);
            this.tabMargins.Controls.Add(this.label9);
            this.tabMargins.Controls.Add(this.lbmm4);
            this.tabMargins.Controls.Add(this.lbmm2);
            this.tabMargins.Controls.Add(this.nudLeftRightMargin);
            this.tabMargins.Controls.Add(this.lbRemainingLeftRight);
            this.tabMargins.Controls.Add(this.nudTopBottomRemaining);
            this.tabMargins.Controls.Add(this.lbRemainingBottomTop);
            this.tabMargins.Controls.Add(this.nudLeftRightRemaining);
            this.tabMargins.Controls.Add(this.label4);
            this.tabMargins.Controls.Add(this.cbRightLeft);
            this.tabMargins.Controls.Add(this.nudTopBottomMargin);
            this.tabMargins.Controls.Add(this.label2);
            this.tabMargins.Controls.Add(this.cbTopBottom);
            resources.ApplyResources(this.tabMargins, "tabMargins");
            this.tabMargins.Name = "tabMargins";
            this.tabMargins.UseVisualStyleBackColor = true;
            // 
            // lbmm5
            // 
            resources.ApplyResources(this.lbmm5, "lbmm5");
            this.lbmm5.Name = "lbmm5";
            // 
            // lbmm3
            // 
            resources.ApplyResources(this.lbmm3, "lbmm3");
            this.lbmm3.Name = "lbmm3";
            // 
            // lbmm1
            // 
            resources.ApplyResources(this.lbmm1, "lbmm1");
            this.lbmm1.Name = "lbmm1";
            // 
            // nudSpaceBetween
            // 
            this.nudSpaceBetween.DecimalPlaces = 1;
            resources.ApplyResources(this.nudSpaceBetween, "nudSpaceBetween");
            this.nudSpaceBetween.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudSpaceBetween.Name = "nudSpaceBetween";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // lbmm4
            // 
            resources.ApplyResources(this.lbmm4, "lbmm4");
            this.lbmm4.Name = "lbmm4";
            // 
            // lbmm2
            // 
            resources.ApplyResources(this.lbmm2, "lbmm2");
            this.lbmm2.Name = "lbmm2";
            // 
            // nudLeftRightMargin
            // 
            this.nudLeftRightMargin.DecimalPlaces = 1;
            resources.ApplyResources(this.nudLeftRightMargin, "nudLeftRightMargin");
            this.nudLeftRightMargin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudLeftRightMargin.Name = "nudLeftRightMargin";
            // 
            // lbRemainingLeftRight
            // 
            resources.ApplyResources(this.lbRemainingLeftRight, "lbRemainingLeftRight");
            this.lbRemainingLeftRight.Name = "lbRemainingLeftRight";
            // 
            // nudTopBottomRemaining
            // 
            this.nudTopBottomRemaining.DecimalPlaces = 1;
            resources.ApplyResources(this.nudTopBottomRemaining, "nudTopBottomRemaining");
            this.nudTopBottomRemaining.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudTopBottomRemaining.Name = "nudTopBottomRemaining";
            // 
            // lbRemainingBottomTop
            // 
            resources.ApplyResources(this.lbRemainingBottomTop, "lbRemainingBottomTop");
            this.lbRemainingBottomTop.Name = "lbRemainingBottomTop";
            // 
            // nudLeftRightRemaining
            // 
            this.nudLeftRightRemaining.DecimalPlaces = 1;
            resources.ApplyResources(this.nudLeftRightRemaining, "nudLeftRightRemaining");
            this.nudLeftRightRemaining.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudLeftRightRemaining.Name = "nudLeftRightRemaining";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // cbRightLeft
            // 
            this.cbRightLeft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRightLeft.FormattingEnabled = true;
            this.cbRightLeft.Items.AddRange(new object[] {
            resources.GetString("cbRightLeft.Items"),
            resources.GetString("cbRightLeft.Items1"),
            resources.GetString("cbRightLeft.Items2")});
            resources.ApplyResources(this.cbRightLeft, "cbRightLeft");
            this.cbRightLeft.Name = "cbRightLeft";
            this.cbRightLeft.SelectedIndexChanged += new System.EventHandler(this.cbPlacement_SelectedIndexChanged);
            // 
            // nudTopBottomMargin
            // 
            this.nudTopBottomMargin.DecimalPlaces = 1;
            resources.ApplyResources(this.nudTopBottomMargin, "nudTopBottomMargin");
            this.nudTopBottomMargin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudTopBottomMargin.Name = "nudTopBottomMargin";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cbTopBottom
            // 
            this.cbTopBottom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTopBottom.FormattingEnabled = true;
            this.cbTopBottom.Items.AddRange(new object[] {
            resources.GetString("cbTopBottom.Items"),
            resources.GetString("cbTopBottom.Items1"),
            resources.GetString("cbTopBottom.Items2")});
            resources.ApplyResources(this.cbTopBottom, "cbTopBottom");
            this.cbTopBottom.Name = "cbTopBottom";
            this.cbTopBottom.SelectedIndexChanged += new System.EventHandler(this.cbPlacement_SelectedIndexChanged);
            // 
            // FormImpositionSettings
            // 
            this.AcceptButton = this.bnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bnCancel;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.bnEditCardboardFormats);
            this.Controls.Add(this.cbCardboardFormat);
            this.Controls.Add(this.lblCardboardFormat);
            this.Controls.Add(this.bnCancel);
            this.Controls.Add(this.bnOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormImpositionSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormImpositionSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormImpositionSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetX)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabAllowedPatterns.ResumeLayout(false);
            this.tabAllowedPatterns.PerformLayout();
            this.tabOffsets.ResumeLayout(false);
            this.tabOffsets.PerformLayout();
            this.tabMargins.ResumeLayout(false);
            this.tabMargins.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpaceBetween)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftRightMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopBottomRemaining)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftRightRemaining)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopBottomMargin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bnOk;
        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.CheckBox cbAllowRowRotation;
        private System.Windows.Forms.CheckBox cbAllowColumnRotation;
        private System.Windows.Forms.Label lblCardboardFormat;
        private System.Windows.Forms.ComboBox cbCardboardFormat;
        private System.Windows.Forms.Button bnEditCardboardFormats;
        private System.Windows.Forms.Label lbOffsetY;
        private System.Windows.Forms.Label lbOffsetX;
        private System.Windows.Forms.NumericUpDown nudOffsetY;
        private System.Windows.Forms.NumericUpDown nudOffsetX;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabAllowedPatterns;
        private System.Windows.Forms.TabPage tabOffsets;
        private System.Windows.Forms.TabPage tabMargins;
        private System.Windows.Forms.Label lbmm4;
        private System.Windows.Forms.Label lbmm2;
        private System.Windows.Forms.NumericUpDown nudLeftRightMargin;
        private System.Windows.Forms.Label lbRemainingLeftRight;
        private System.Windows.Forms.NumericUpDown nudTopBottomRemaining;
        private System.Windows.Forms.Label lbRemainingBottomTop;
        private System.Windows.Forms.NumericUpDown nudLeftRightRemaining;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbRightLeft;
        private System.Windows.Forms.NumericUpDown nudTopBottomMargin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbTopBottom;
        private System.Windows.Forms.NumericUpDown nudSpaceBetween;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbmm3;
        private System.Windows.Forms.Label lbmm1;
        private System.Windows.Forms.Label lbmm5;
    }
}