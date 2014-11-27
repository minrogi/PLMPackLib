namespace Pic.Factory2D.Control
{
    partial class FormImposition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImposition));
            this.buttonClose = new System.Windows.Forms.Button();
            this.lblSolutions = new System.Windows.Forms.Label();
            this.listBoxSolutions = new System.Windows.Forms.ListBox();
            this.lblNameNumbers = new System.Windows.Forms.Label();
            this.grpbLengthes = new System.Windows.Forms.GroupBox();
            this.lblNameTotal = new System.Windows.Forms.Label();
            this.pbFold = new System.Windows.Forms.PictureBox();
            this.pbCut = new System.Windows.Forms.PictureBox();
            this.lblValueLengthTotal = new System.Windows.Forms.Label();
            this.lblValueLengthFold = new System.Windows.Forms.Label();
            this.lblValueLengthCut = new System.Windows.Forms.Label();
            this.lblNameEfficiency = new System.Windows.Forms.Label();
            this.lblValueFormat = new System.Windows.Forms.Label();
            this.grpbArea = new System.Windows.Forms.GroupBox();
            this.lblValueEfficiency = new System.Windows.Forms.Label();
            this.lblNameFormat = new System.Windows.Forms.Label();
            this.lblNameArea = new System.Windows.Forms.Label();
            this.lblValueArea = new System.Windows.Forms.Label();
            this.lblValueNumbers = new System.Windows.Forms.Label();
            this.grpbCardboardFormat = new System.Windows.Forms.GroupBox();
            this.lblValueCardboardEfficiency = new System.Windows.Forms.Label();
            this.lblCardboardEfficiency = new System.Windows.Forms.Label();
            this.lblValueCardboardFormat = new System.Windows.Forms.Label();
            this.lblFormat = new System.Windows.Forms.Label();
            this.factoryViewer = new Pic.Factory2D.Control.FactoryViewer();
            this.toolStripImposition = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonExport = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripExportDES = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripExportDXF = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStriExportAI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripExportCF2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPicGEOM = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDXF = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAI = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCFF2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPDF = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOceProCut = new System.Windows.Forms.ToolStripButton();
            this.grpbLengthes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCut)).BeginInit();
            this.grpbArea.SuspendLayout();
            this.grpbCardboardFormat.SuspendLayout();
            this.toolStripImposition.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.onClose);
            // 
            // lblSolutions
            // 
            resources.ApplyResources(this.lblSolutions, "lblSolutions");
            this.lblSolutions.Name = "lblSolutions";
            // 
            // listBoxSolutions
            // 
            resources.ApplyResources(this.listBoxSolutions, "listBoxSolutions");
            this.listBoxSolutions.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxSolutions.FormattingEnabled = true;
            this.listBoxSolutions.Name = "listBoxSolutions";
            this.listBoxSolutions.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBoxSolutions_DrawItem);
            // 
            // lblNameNumbers
            // 
            resources.ApplyResources(this.lblNameNumbers, "lblNameNumbers");
            this.lblNameNumbers.Name = "lblNameNumbers";
            // 
            // grpbLengthes
            // 
            resources.ApplyResources(this.grpbLengthes, "grpbLengthes");
            this.grpbLengthes.Controls.Add(this.lblNameTotal);
            this.grpbLengthes.Controls.Add(this.pbFold);
            this.grpbLengthes.Controls.Add(this.pbCut);
            this.grpbLengthes.Controls.Add(this.lblValueLengthTotal);
            this.grpbLengthes.Controls.Add(this.lblValueLengthFold);
            this.grpbLengthes.Controls.Add(this.lblValueLengthCut);
            this.grpbLengthes.Name = "grpbLengthes";
            this.grpbLengthes.TabStop = false;
            // 
            // lblNameTotal
            // 
            resources.ApplyResources(this.lblNameTotal, "lblNameTotal");
            this.lblNameTotal.Name = "lblNameTotal";
            // 
            // pbFold
            // 
            resources.ApplyResources(this.pbFold, "pbFold");
            this.pbFold.Name = "pbFold";
            this.pbFold.TabStop = false;
            this.pbFold.Paint += new System.Windows.Forms.PaintEventHandler(this.pbFold_Paint);
            // 
            // pbCut
            // 
            resources.ApplyResources(this.pbCut, "pbCut");
            this.pbCut.Name = "pbCut";
            this.pbCut.TabStop = false;
            this.pbCut.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCut_Paint);
            // 
            // lblValueLengthTotal
            // 
            resources.ApplyResources(this.lblValueLengthTotal, "lblValueLengthTotal");
            this.lblValueLengthTotal.Name = "lblValueLengthTotal";
            // 
            // lblValueLengthFold
            // 
            resources.ApplyResources(this.lblValueLengthFold, "lblValueLengthFold");
            this.lblValueLengthFold.Name = "lblValueLengthFold";
            // 
            // lblValueLengthCut
            // 
            resources.ApplyResources(this.lblValueLengthCut, "lblValueLengthCut");
            this.lblValueLengthCut.Name = "lblValueLengthCut";
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
            // grpbArea
            // 
            resources.ApplyResources(this.grpbArea, "grpbArea");
            this.grpbArea.Controls.Add(this.lblValueEfficiency);
            this.grpbArea.Controls.Add(this.lblNameEfficiency);
            this.grpbArea.Controls.Add(this.lblValueFormat);
            this.grpbArea.Controls.Add(this.lblNameFormat);
            this.grpbArea.Controls.Add(this.lblNameArea);
            this.grpbArea.Controls.Add(this.lblValueArea);
            this.grpbArea.Name = "grpbArea";
            this.grpbArea.TabStop = false;
            // 
            // lblValueEfficiency
            // 
            resources.ApplyResources(this.lblValueEfficiency, "lblValueEfficiency");
            this.lblValueEfficiency.Name = "lblValueEfficiency";
            // 
            // lblNameFormat
            // 
            resources.ApplyResources(this.lblNameFormat, "lblNameFormat");
            this.lblNameFormat.Name = "lblNameFormat";
            // 
            // lblNameArea
            // 
            resources.ApplyResources(this.lblNameArea, "lblNameArea");
            this.lblNameArea.Name = "lblNameArea";
            // 
            // lblValueArea
            // 
            resources.ApplyResources(this.lblValueArea, "lblValueArea");
            this.lblValueArea.Name = "lblValueArea";
            // 
            // lblValueNumbers
            // 
            resources.ApplyResources(this.lblValueNumbers, "lblValueNumbers");
            this.lblValueNumbers.Name = "lblValueNumbers";
            // 
            // grpbCardboardFormat
            // 
            resources.ApplyResources(this.grpbCardboardFormat, "grpbCardboardFormat");
            this.grpbCardboardFormat.Controls.Add(this.lblValueCardboardEfficiency);
            this.grpbCardboardFormat.Controls.Add(this.lblCardboardEfficiency);
            this.grpbCardboardFormat.Controls.Add(this.lblValueCardboardFormat);
            this.grpbCardboardFormat.Controls.Add(this.lblFormat);
            this.grpbCardboardFormat.Controls.Add(this.lblValueNumbers);
            this.grpbCardboardFormat.Controls.Add(this.lblNameNumbers);
            this.grpbCardboardFormat.Name = "grpbCardboardFormat";
            this.grpbCardboardFormat.TabStop = false;
            // 
            // lblValueCardboardEfficiency
            // 
            resources.ApplyResources(this.lblValueCardboardEfficiency, "lblValueCardboardEfficiency");
            this.lblValueCardboardEfficiency.Name = "lblValueCardboardEfficiency";
            // 
            // lblCardboardEfficiency
            // 
            resources.ApplyResources(this.lblCardboardEfficiency, "lblCardboardEfficiency");
            this.lblCardboardEfficiency.Name = "lblCardboardEfficiency";
            // 
            // lblValueCardboardFormat
            // 
            resources.ApplyResources(this.lblValueCardboardFormat, "lblValueCardboardFormat");
            this.lblValueCardboardFormat.Name = "lblValueCardboardFormat";
            // 
            // lblFormat
            // 
            resources.ApplyResources(this.lblFormat, "lblFormat");
            this.lblFormat.Name = "lblFormat";
            // 
            // factoryViewer
            // 
            resources.ApplyResources(this.factoryViewer, "factoryViewer");
            this.factoryViewer.Name = "factoryViewer";
            this.factoryViewer.ReflectionX = false;
            this.factoryViewer.ReflectionY = false;
            this.factoryViewer.ShowAboutMenu = false;
            this.factoryViewer.ShowCotations = false;
            this.factoryViewer.ShowNestingMenu = false;
            // 
            // toolStripImposition
            // 
            this.toolStripImposition.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripImposition.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExport,
            this.toolStripSeparator1,
            this.toolStripButtonPicGEOM,
            this.toolStripButtonDXF,
            this.toolStripButtonAI,
            this.toolStripButtonCFF2,
            this.toolStripButtonPDF,
            this.toolStripButtonOceProCut});
            resources.ApplyResources(this.toolStripImposition, "toolStripImposition");
            this.toolStripImposition.Name = "toolStripImposition";
            this.toolStripImposition.Stretch = true;
            // 
            // toolStripButtonExport
            // 
            this.toolStripButtonExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripExportDES,
            this.toolStripExportDXF,
            this.toolStripExportPDF,
            this.toolStriExportAI,
            this.toolStripExportCF2});
            resources.ApplyResources(this.toolStripButtonExport, "toolStripButtonExport");
            this.toolStripButtonExport.Name = "toolStripButtonExport";
            // 
            // toolStripExportDES
            // 
            this.toolStripExportDES.Name = "toolStripExportDES";
            resources.ApplyResources(this.toolStripExportDES, "toolStripExportDES");
            this.toolStripExportDES.Click += new System.EventHandler(this.toolStripExport_Click);
            // 
            // toolStripExportDXF
            // 
            this.toolStripExportDXF.Name = "toolStripExportDXF";
            resources.ApplyResources(this.toolStripExportDXF, "toolStripExportDXF");
            this.toolStripExportDXF.Click += new System.EventHandler(this.toolStripExport_Click);
            // 
            // toolStripExportPDF
            // 
            this.toolStripExportPDF.Name = "toolStripExportPDF";
            resources.ApplyResources(this.toolStripExportPDF, "toolStripExportPDF");
            this.toolStripExportPDF.Click += new System.EventHandler(this.toolStripExport_Click);
            // 
            // toolStriExportAI
            // 
            this.toolStriExportAI.Name = "toolStriExportAI";
            resources.ApplyResources(this.toolStriExportAI, "toolStriExportAI");
            this.toolStriExportAI.Click += new System.EventHandler(this.toolStripExport_Click);
            // 
            // toolStripExportCF2
            // 
            this.toolStripExportCF2.Name = "toolStripExportCF2";
            resources.ApplyResources(this.toolStripExportCF2, "toolStripExportCF2");
            this.toolStripExportCF2.Click += new System.EventHandler(this.toolStripExport_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripButtonPicGEOM
            // 
            this.toolStripButtonPicGEOM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonPicGEOM, "toolStripButtonPicGEOM");
            this.toolStripButtonPicGEOM.Name = "toolStripButtonPicGEOM";
            this.toolStripButtonPicGEOM.Click += new System.EventHandler(this.toolStripButtonPicGEOM_Click);
            // 
            // toolStripButtonDXF
            // 
            this.toolStripButtonDXF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonDXF, "toolStripButtonDXF");
            this.toolStripButtonDXF.Name = "toolStripButtonDXF";
            this.toolStripButtonDXF.Click += new System.EventHandler(this.toolStripButtonDXF_Click);
            // 
            // toolStripButtonAI
            // 
            this.toolStripButtonAI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonAI, "toolStripButtonAI");
            this.toolStripButtonAI.Name = "toolStripButtonAI";
            this.toolStripButtonAI.Click += new System.EventHandler(this.toolStripButtonAI_Click);
            // 
            // toolStripButtonCFF2
            // 
            this.toolStripButtonCFF2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonCFF2, "toolStripButtonCFF2");
            this.toolStripButtonCFF2.Name = "toolStripButtonCFF2";
            this.toolStripButtonCFF2.Click += new System.EventHandler(this.toolStripButtonCFF2_Click);
            // 
            // toolStripButtonPDF
            // 
            this.toolStripButtonPDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonPDF, "toolStripButtonPDF");
            this.toolStripButtonPDF.Name = "toolStripButtonPDF";
            this.toolStripButtonPDF.Click += new System.EventHandler(this.toolStripButtonPDF_Click);
            // 
            // toolStripButtonOceProCut
            // 
            this.toolStripButtonOceProCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonOceProCut, "toolStripButtonOceProCut");
            this.toolStripButtonOceProCut.Name = "toolStripButtonOceProCut";
            this.toolStripButtonOceProCut.Click += new System.EventHandler(this.toolStripButtonOceProCut_Click);
            // 
            // FormImposition
            // 
            this.AcceptButton = this.buttonClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.toolStripImposition);
            this.Controls.Add(this.grpbLengthes);
            this.Controls.Add(this.grpbArea);
            this.Controls.Add(this.listBoxSolutions);
            this.Controls.Add(this.lblSolutions);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.factoryViewer);
            this.Controls.Add(this.grpbCardboardFormat);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormImposition";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.grpbLengthes.ResumeLayout(false);
            this.grpbLengthes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCut)).EndInit();
            this.grpbArea.ResumeLayout(false);
            this.grpbArea.PerformLayout();
            this.grpbCardboardFormat.ResumeLayout(false);
            this.grpbCardboardFormat.PerformLayout();
            this.toolStripImposition.ResumeLayout(false);
            this.toolStripImposition.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private Pic.Factory2D.Control.FactoryViewer factoryViewer;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label lblSolutions;
        private System.Windows.Forms.ListBox listBoxSolutions;
        private System.Windows.Forms.Label lblNameNumbers;
        private System.Windows.Forms.GroupBox grpbLengthes;
        private System.Windows.Forms.Label lblNameTotal;
        private System.Windows.Forms.PictureBox pbFold;
        private System.Windows.Forms.PictureBox pbCut;
        private System.Windows.Forms.Label lblValueLengthTotal;
        private System.Windows.Forms.Label lblValueLengthFold;
        private System.Windows.Forms.Label lblValueLengthCut;
        private System.Windows.Forms.Label lblNameEfficiency;
        private System.Windows.Forms.Label lblValueFormat;
        private System.Windows.Forms.GroupBox grpbArea;
        private System.Windows.Forms.Label lblValueEfficiency;
        private System.Windows.Forms.Label lblNameFormat;
        private System.Windows.Forms.Label lblNameArea;
        private System.Windows.Forms.Label lblValueArea;
        private System.Windows.Forms.Label lblValueNumbers;
        private System.Windows.Forms.GroupBox grpbCardboardFormat;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.Label lblValueCardboardFormat;
        private System.Windows.Forms.Label lblCardboardEfficiency;
        private System.Windows.Forms.Label lblValueCardboardEfficiency;
        private System.Windows.Forms.ToolStrip toolStripImposition;
        private System.Windows.Forms.ToolStripButton toolStripButtonPicGEOM;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton toolStripButtonExport;
        private System.Windows.Forms.ToolStripMenuItem toolStripExportDES;
        private System.Windows.Forms.ToolStripMenuItem toolStripExportDXF;
        private System.Windows.Forms.ToolStripMenuItem toolStripExportPDF;
        private System.Windows.Forms.ToolStripMenuItem toolStriExportAI;
        private System.Windows.Forms.ToolStripMenuItem toolStripExportCF2;
        private System.Windows.Forms.ToolStripButton toolStripButtonDXF;
        private System.Windows.Forms.ToolStripButton toolStripButtonAI;
        private System.Windows.Forms.ToolStripButton toolStripButtonCFF2;
        private System.Windows.Forms.ToolStripButton toolStripButtonOceProCut;
        private System.Windows.Forms.ToolStripButton toolStripButtonPDF;
    }
}