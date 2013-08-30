namespace Pic.Factory2D.Control
{
    partial class FactoryDataCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FactoryDataCtrl));
            this.lblValueLengthCut = new System.Windows.Forms.Label();
            this.lblValueLengthFold = new System.Windows.Forms.Label();
            this.lblValueLengthTotal = new System.Windows.Forms.Label();
            this.pbCut = new System.Windows.Forms.PictureBox();
            this.pbFold = new System.Windows.Forms.PictureBox();
            this.lblAreaValue = new System.Windows.Forms.Label();
            this.lblNameTotal = new System.Windows.Forms.Label();
            this.lblNameArea = new System.Windows.Forms.Label();
            this.groupBoxSepator = new System.Windows.Forms.GroupBox();
            this.lblNameLength = new System.Windows.Forms.Label();
            this.lblNameWidth = new System.Windows.Forms.Label();
            this.lblValueLength = new System.Windows.Forms.Label();
            this.lblValueWidth = new System.Windows.Forms.Label();
            this.lblValueEfficiency = new System.Windows.Forms.Label();
            this.lblNameEfficiency = new System.Windows.Forms.Label();
            this.lblValueFormat = new System.Windows.Forms.Label();
            this.lblNameFormat = new System.Windows.Forms.Label();
            this.tabControlData = new System.Windows.Forms.TabControl();
            this.tabLength = new System.Windows.Forms.TabPage();
            this.tabBoundingBox = new System.Windows.Forms.TabPage();
            this.tabArea = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.pbCut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFold)).BeginInit();
            this.tabControlData.SuspendLayout();
            this.tabLength.SuspendLayout();
            this.tabBoundingBox.SuspendLayout();
            this.tabArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblValueLengthCut
            // 
            resources.ApplyResources(this.lblValueLengthCut, "lblValueLengthCut");
            this.lblValueLengthCut.Name = "lblValueLengthCut";
            // 
            // lblValueLengthFold
            // 
            resources.ApplyResources(this.lblValueLengthFold, "lblValueLengthFold");
            this.lblValueLengthFold.Name = "lblValueLengthFold";
            // 
            // lblValueLengthTotal
            // 
            resources.ApplyResources(this.lblValueLengthTotal, "lblValueLengthTotal");
            this.lblValueLengthTotal.Name = "lblValueLengthTotal";
            // 
            // pbCut
            // 
            resources.ApplyResources(this.pbCut, "pbCut");
            this.pbCut.Name = "pbCut";
            this.pbCut.TabStop = false;
            this.pbCut.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCut_Paint);
            // 
            // pbFold
            // 
            resources.ApplyResources(this.pbFold, "pbFold");
            this.pbFold.Name = "pbFold";
            this.pbFold.TabStop = false;
            this.pbFold.Paint += new System.Windows.Forms.PaintEventHandler(this.pbFold_Paint);
            // 
            // lblAreaValue
            // 
            resources.ApplyResources(this.lblAreaValue, "lblAreaValue");
            this.lblAreaValue.Name = "lblAreaValue";
            // 
            // lblNameTotal
            // 
            resources.ApplyResources(this.lblNameTotal, "lblNameTotal");
            this.lblNameTotal.Name = "lblNameTotal";
            // 
            // lblNameArea
            // 
            resources.ApplyResources(this.lblNameArea, "lblNameArea");
            this.lblNameArea.Name = "lblNameArea";
            // 
            // groupBoxSepator
            // 
            resources.ApplyResources(this.groupBoxSepator, "groupBoxSepator");
            this.groupBoxSepator.Name = "groupBoxSepator";
            this.groupBoxSepator.TabStop = false;
            // 
            // lblNameLength
            // 
            resources.ApplyResources(this.lblNameLength, "lblNameLength");
            this.lblNameLength.Name = "lblNameLength";
            // 
            // lblNameWidth
            // 
            resources.ApplyResources(this.lblNameWidth, "lblNameWidth");
            this.lblNameWidth.Name = "lblNameWidth";
            // 
            // lblValueLength
            // 
            resources.ApplyResources(this.lblValueLength, "lblValueLength");
            this.lblValueLength.Name = "lblValueLength";
            // 
            // lblValueWidth
            // 
            resources.ApplyResources(this.lblValueWidth, "lblValueWidth");
            this.lblValueWidth.Name = "lblValueWidth";
            // 
            // lblValueEfficiency
            // 
            resources.ApplyResources(this.lblValueEfficiency, "lblValueEfficiency");
            this.lblValueEfficiency.Name = "lblValueEfficiency";
            // 
            // lblNameEfficiency
            // 
            resources.ApplyResources(this.lblNameEfficiency, "lblNameEfficiency");
            this.lblNameEfficiency.Name = "lblNameEfficiency";
            // 
            // lblValueFormat
            // 
            resources.ApplyResources(this.lblValueFormat, "lblValueFormat");
            this.lblValueFormat.Name = "lblValueFormat";
            // 
            // lblNameFormat
            // 
            resources.ApplyResources(this.lblNameFormat, "lblNameFormat");
            this.lblNameFormat.Name = "lblNameFormat";
            // 
            // tabControlData
            // 
            resources.ApplyResources(this.tabControlData, "tabControlData");
            this.tabControlData.Controls.Add(this.tabLength);
            this.tabControlData.Controls.Add(this.tabBoundingBox);
            this.tabControlData.Controls.Add(this.tabArea);
            this.tabControlData.Name = "tabControlData";
            this.tabControlData.SelectedIndex = 0;
            this.tabControlData.SelectedIndexChanged += new System.EventHandler(this.tabControlData_SelectedIndexChanged);
            // 
            // tabLength
            // 
            resources.ApplyResources(this.tabLength, "tabLength");
            this.tabLength.Controls.Add(this.lblNameTotal);
            this.tabLength.Controls.Add(this.pbFold);
            this.tabLength.Controls.Add(this.pbCut);
            this.tabLength.Controls.Add(this.groupBoxSepator);
            this.tabLength.Controls.Add(this.lblValueLengthCut);
            this.tabLength.Controls.Add(this.lblValueLengthTotal);
            this.tabLength.Controls.Add(this.lblValueLengthFold);
            this.tabLength.Name = "tabLength";
            this.tabLength.UseVisualStyleBackColor = true;
            // 
            // tabBoundingBox
            // 
            resources.ApplyResources(this.tabBoundingBox, "tabBoundingBox");
            this.tabBoundingBox.Controls.Add(this.lblValueWidth);
            this.tabBoundingBox.Controls.Add(this.lblNameLength);
            this.tabBoundingBox.Controls.Add(this.lblValueLength);
            this.tabBoundingBox.Controls.Add(this.lblNameWidth);
            this.tabBoundingBox.Name = "tabBoundingBox";
            this.tabBoundingBox.UseVisualStyleBackColor = true;
            // 
            // tabArea
            // 
            resources.ApplyResources(this.tabArea, "tabArea");
            this.tabArea.Controls.Add(this.lblValueEfficiency);
            this.tabArea.Controls.Add(this.lblValueFormat);
            this.tabArea.Controls.Add(this.lblNameEfficiency);
            this.tabArea.Controls.Add(this.lblAreaValue);
            this.tabArea.Controls.Add(this.lblNameArea);
            this.tabArea.Controls.Add(this.lblNameFormat);
            this.tabArea.Name = "tabArea";
            this.tabArea.UseVisualStyleBackColor = true;
            // 
            // FactoryDataCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlData);
            this.Name = "FactoryDataCtrl";
            ((System.ComponentModel.ISupportInitialize)(this.pbCut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFold)).EndInit();
            this.tabControlData.ResumeLayout(false);
            this.tabLength.ResumeLayout(false);
            this.tabLength.PerformLayout();
            this.tabBoundingBox.ResumeLayout(false);
            this.tabBoundingBox.PerformLayout();
            this.tabArea.ResumeLayout(false);
            this.tabArea.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblValueLengthCut;
        private System.Windows.Forms.Label lblValueLengthFold;
        private System.Windows.Forms.Label lblValueLengthTotal;
        private System.Windows.Forms.PictureBox pbCut;
        private System.Windows.Forms.PictureBox pbFold;
        private System.Windows.Forms.Label lblAreaValue;
        private System.Windows.Forms.Label lblNameTotal;
        private System.Windows.Forms.Label lblNameArea;
        private System.Windows.Forms.GroupBox groupBoxSepator;
        private System.Windows.Forms.Label lblNameLength;
        private System.Windows.Forms.Label lblNameWidth;
        private System.Windows.Forms.Label lblValueLength;
        private System.Windows.Forms.Label lblValueWidth;
        private System.Windows.Forms.Label lblValueEfficiency;
        private System.Windows.Forms.Label lblNameEfficiency;
        private System.Windows.Forms.Label lblValueFormat;
        private System.Windows.Forms.Label lblNameFormat;
        private System.Windows.Forms.TabControl tabControlData;
        private System.Windows.Forms.TabPage tabLength;
        private System.Windows.Forms.TabPage tabBoundingBox;
        private System.Windows.Forms.TabPage tabArea;

    }
}
