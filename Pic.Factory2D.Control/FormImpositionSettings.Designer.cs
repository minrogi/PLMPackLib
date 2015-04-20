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
            this.lbmm3 = new System.Windows.Forms.Label();
            this.lbmm1 = new System.Windows.Forms.Label();
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
            this.tabSpacing = new System.Windows.Forms.TabPage();
            this.lbmm6 = new System.Windows.Forms.Label();
            this.nudSpaceY = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbmm5 = new System.Windows.Forms.Label();
            this.nudSpaceX = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.rbLayoutMode1 = new System.Windows.Forms.RadioButton();
            this.rbLayoutMode2 = new System.Windows.Forms.RadioButton();
            this.lbDirX = new System.Windows.Forms.Label();
            this.nudNumberDirX = new System.Windows.Forms.NumericUpDown();
            this.lbDirY = new System.Windows.Forms.Label();
            this.nudNumberDirY = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetX)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabAllowedPatterns.SuspendLayout();
            this.tabOffsets.SuspendLayout();
            this.tabMargins.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftRightMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopBottomRemaining)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftRightRemaining)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopBottomMargin)).BeginInit();
            this.tabSpacing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpaceY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpaceX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberDirX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberDirY)).BeginInit();
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
            resources.ApplyResources(this.nudOffsetY, "nudOffsetY");
            this.nudOffsetY.DecimalPlaces = 1;
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
            resources.ApplyResources(this.nudOffsetX, "nudOffsetX");
            this.nudOffsetX.DecimalPlaces = 1;
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
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabAllowedPatterns);
            this.tabControl.Controls.Add(this.tabOffsets);
            this.tabControl.Controls.Add(this.tabMargins);
            this.tabControl.Controls.Add(this.tabSpacing);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabAllowedPatterns
            // 
            resources.ApplyResources(this.tabAllowedPatterns, "tabAllowedPatterns");
            this.tabAllowedPatterns.Controls.Add(this.cbAllowColumnRotation);
            this.tabAllowedPatterns.Controls.Add(this.cbAllowRowRotation);
            this.tabAllowedPatterns.Name = "tabAllowedPatterns";
            this.tabAllowedPatterns.UseVisualStyleBackColor = true;
            // 
            // tabOffsets
            // 
            resources.ApplyResources(this.tabOffsets, "tabOffsets");
            this.tabOffsets.Controls.Add(this.nudOffsetY);
            this.tabOffsets.Controls.Add(this.lbOffsetX);
            this.tabOffsets.Controls.Add(this.nudOffsetX);
            this.tabOffsets.Controls.Add(this.lbOffsetY);
            this.tabOffsets.Name = "tabOffsets";
            this.tabOffsets.UseVisualStyleBackColor = true;
            // 
            // tabMargins
            // 
            resources.ApplyResources(this.tabMargins, "tabMargins");
            this.tabMargins.Controls.Add(this.lbmm3);
            this.tabMargins.Controls.Add(this.lbmm1);
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
            this.tabMargins.Name = "tabMargins";
            this.tabMargins.UseVisualStyleBackColor = true;
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
            resources.ApplyResources(this.nudLeftRightMargin, "nudLeftRightMargin");
            this.nudLeftRightMargin.DecimalPlaces = 1;
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
            resources.ApplyResources(this.nudTopBottomRemaining, "nudTopBottomRemaining");
            this.nudTopBottomRemaining.DecimalPlaces = 1;
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
            resources.ApplyResources(this.nudLeftRightRemaining, "nudLeftRightRemaining");
            this.nudLeftRightRemaining.DecimalPlaces = 1;
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
            resources.ApplyResources(this.cbRightLeft, "cbRightLeft");
            this.cbRightLeft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRightLeft.FormattingEnabled = true;
            this.cbRightLeft.Items.AddRange(new object[] {
            resources.GetString("cbRightLeft.Items"),
            resources.GetString("cbRightLeft.Items1"),
            resources.GetString("cbRightLeft.Items2")});
            this.cbRightLeft.Name = "cbRightLeft";
            this.cbRightLeft.SelectedIndexChanged += new System.EventHandler(this.cbPlacement_SelectedIndexChanged);
            // 
            // nudTopBottomMargin
            // 
            resources.ApplyResources(this.nudTopBottomMargin, "nudTopBottomMargin");
            this.nudTopBottomMargin.DecimalPlaces = 1;
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
            resources.ApplyResources(this.cbTopBottom, "cbTopBottom");
            this.cbTopBottom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTopBottom.FormattingEnabled = true;
            this.cbTopBottom.Items.AddRange(new object[] {
            resources.GetString("cbTopBottom.Items"),
            resources.GetString("cbTopBottom.Items1"),
            resources.GetString("cbTopBottom.Items2")});
            this.cbTopBottom.Name = "cbTopBottom";
            this.cbTopBottom.SelectedIndexChanged += new System.EventHandler(this.cbPlacement_SelectedIndexChanged);
            // 
            // tabSpacing
            // 
            resources.ApplyResources(this.tabSpacing, "tabSpacing");
            this.tabSpacing.Controls.Add(this.lbmm6);
            this.tabSpacing.Controls.Add(this.nudSpaceY);
            this.tabSpacing.Controls.Add(this.label3);
            this.tabSpacing.Controls.Add(this.label1);
            this.tabSpacing.Controls.Add(this.lbmm5);
            this.tabSpacing.Controls.Add(this.nudSpaceX);
            this.tabSpacing.Controls.Add(this.label9);
            this.tabSpacing.Name = "tabSpacing";
            this.tabSpacing.UseVisualStyleBackColor = true;
            // 
            // lbmm6
            // 
            resources.ApplyResources(this.lbmm6, "lbmm6");
            this.lbmm6.Name = "lbmm6";
            // 
            // nudSpaceY
            // 
            resources.ApplyResources(this.nudSpaceY, "nudSpaceY");
            this.nudSpaceY.DecimalPlaces = 1;
            this.nudSpaceY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudSpaceY.Name = "nudSpaceY";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lbmm5
            // 
            resources.ApplyResources(this.lbmm5, "lbmm5");
            this.lbmm5.Name = "lbmm5";
            // 
            // nudSpaceX
            // 
            resources.ApplyResources(this.nudSpaceX, "nudSpaceX");
            this.nudSpaceX.DecimalPlaces = 1;
            this.nudSpaceX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudSpaceX.Name = "nudSpaceX";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // rbLayoutMode1
            // 
            resources.ApplyResources(this.rbLayoutMode1, "rbLayoutMode1");
            this.rbLayoutMode1.Name = "rbLayoutMode1";
            this.rbLayoutMode1.TabStop = true;
            this.rbLayoutMode1.UseVisualStyleBackColor = true;
            this.rbLayoutMode1.CheckedChanged += new System.EventHandler(this.rbLayoutMode_CheckedChanged);
            // 
            // rbLayoutMode2
            // 
            resources.ApplyResources(this.rbLayoutMode2, "rbLayoutMode2");
            this.rbLayoutMode2.Name = "rbLayoutMode2";
            this.rbLayoutMode2.TabStop = true;
            this.rbLayoutMode2.UseVisualStyleBackColor = true;
            this.rbLayoutMode2.CheckedChanged += new System.EventHandler(this.rbLayoutMode_CheckedChanged);
            // 
            // lbDirX
            // 
            resources.ApplyResources(this.lbDirX, "lbDirX");
            this.lbDirX.Name = "lbDirX";
            // 
            // nudNumberDirX
            // 
            resources.ApplyResources(this.nudNumberDirX, "nudNumberDirX");
            this.nudNumberDirX.Name = "nudNumberDirX";
            // 
            // lbDirY
            // 
            resources.ApplyResources(this.lbDirY, "lbDirY");
            this.lbDirY.Name = "lbDirY";
            // 
            // nudNumberDirY
            // 
            resources.ApplyResources(this.nudNumberDirY, "nudNumberDirY");
            this.nudNumberDirY.Name = "nudNumberDirY";
            // 
            // FormImpositionSettings
            // 
            this.AcceptButton = this.bnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bnCancel;
            this.Controls.Add(this.nudNumberDirY);
            this.Controls.Add(this.lbDirY);
            this.Controls.Add(this.nudNumberDirX);
            this.Controls.Add(this.lbDirX);
            this.Controls.Add(this.rbLayoutMode2);
            this.Controls.Add(this.rbLayoutMode1);
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
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftRightMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopBottomRemaining)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftRightRemaining)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopBottomMargin)).EndInit();
            this.tabSpacing.ResumeLayout(false);
            this.tabSpacing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpaceY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpaceX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberDirX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberDirY)).EndInit();
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
        private System.Windows.Forms.Label lbmm3;
        private System.Windows.Forms.Label lbmm1;
        private System.Windows.Forms.RadioButton rbLayoutMode1;
        private System.Windows.Forms.RadioButton rbLayoutMode2;
        private System.Windows.Forms.Label lbDirX;
        private System.Windows.Forms.NumericUpDown nudNumberDirX;
        private System.Windows.Forms.Label lbDirY;
        private System.Windows.Forms.NumericUpDown nudNumberDirY;
        private System.Windows.Forms.TabPage tabSpacing;
        private System.Windows.Forms.Label lbmm6;
        private System.Windows.Forms.NumericUpDown nudSpaceY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbmm5;
        private System.Windows.Forms.NumericUpDown nudSpaceX;
        private System.Windows.Forms.Label label9;
    }
}