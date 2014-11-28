#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

using Pic.Factory2D;
using Pic.Factory2D.Control;
using Sharp3D.Math.Core;
#endregion

namespace Pic.Factory2D.Control
{
    public partial class FormImposition : Form, IEntitySupplier
    {
        #region Data members
        private List<ImpositionSolution> solutions;
        private CardboardFormat _cardboardFormat;
        private static SolidBrush _textBrush = new SolidBrush(SystemColors.WindowText);
        #endregion

        #region Constructor
        public FormImposition()
        {
            InitializeComponent();
            listBoxSolutions.SelectedValueChanged += new EventHandler(listBoxSolutions_SelectedValueChanged);

            this.factoryViewer.ReflectionX = false;
            this.factoryViewer.ReflectionY = false;
            this.factoryViewer.ShowCotations = false;

            // hide nesting and about menu
            this.factoryViewer.ShowNestingMenu = false;
            this.factoryViewer.ShowAboutMenu = false;
        }
        #endregion

        #region IEntitySupplier implementation
        public void CreateEntities(PicFactory factory)
        {
            ImpositionSolution solution = listBoxSolutions.SelectedItem as ImpositionSolution;
            if (null == solution) return;
            solution.CreateEntities(factory);
            factory.InsertCardboardFormat(solution.CardboardPosition, solution.CardboardDimensions);
        }
        #endregion

        #region Public properties
        public List<ImpositionSolution> Solutions
        {
            set
            {
                solutions = value;
                // insert in listbox
                listBoxSolutions.Items.Clear();
                listBoxSolutions.Items.AddRange(solutions.ToArray());
            }
        }
        public CardboardFormat Format
        {
            get { return _cardboardFormat; }
            set { _cardboardFormat = value; }
        }
        #endregion

        #region System.windows.Forms.Form override
        protected override void OnLoad(EventArgs e)
        {
            // make ToolStripButton available
            toolStripButtonOceProCut.Available = Properties.Settings.Default.TSButtonAvailableOceProCut;

            if (listBoxSolutions.Items.Count > 0)
                listBoxSolutions.SelectedIndex = 0;
            factoryViewer.Refresh();

            factoryViewer.ShowCotationsToggled += new FactoryViewer.StatusChanged(factoryViewer_ShowCotationsToggled);

            base.OnLoad(e);
        }

        void factoryViewer_ShowCotationsToggled(object sender, EventArgsStatus e)
        {}

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Z:
                    factoryViewer.FitView();
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        #endregion

        #region Paint event handlers
        /// <summary>
        /// Paint event handler for Cut length label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCut_Paint(object sender, PaintEventArgs e)
        {
            Size sz = pbCut.Size;
            e.Graphics.DrawLine(new Pen(Color.Red), new Point(0, sz.Height / 2), new Point(sz.Width / 2, sz.Height / 2));
        }
        /// <summary>
        /// Paint event handler for Fold length
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbFold_Paint(object sender, PaintEventArgs e)
        {
            Size sz = pbFold.Size;
            e.Graphics.DrawLine(new Pen(Color.Blue), new Point(0, sz.Height / 2), new Point(sz.Width / 2, sz.Height / 2));
        }
        #endregion

        #region listBoxSolutions event handlers
        /// <summary>
        /// listBoxSolution: selected item changed
        /// </summary>
        void listBoxSolutions_SelectedValueChanged(object sender, EventArgs e)
        {
            factoryViewer.Refresh();

            ImpositionSolution solution = listBoxSolutions.SelectedItem as ImpositionSolution;
            if (null == solution) return;

            // numbers
            lblValueCardboardFormat.Text = string.Format(": {0} x {1}",
                (int)Math.Ceiling(solution.CardboardDimensions.X),
                (int)Math.Ceiling(solution.CardboardDimensions.Y));
            lblValueCardboardEfficiency.Text = string.Format(": {0:0.#} %",
                100.0 * solution.Area / (solution.CardboardDimensions.X * solution.CardboardDimensions.Y));
            lblValueNumbers.Text = string.Format(": {0} ({1} x {2})",
                solution.PositionCount, solution.Rows, solution.Cols);
            // lengthes
            lblValueLengthCut.Text = string.Format(": {0:0.###} m", solution.LengthCut / 1000.0);
            lblValueLengthFold.Text = string.Format(": {0:0.###} m ", solution.LengthFold / 1000.0);
            lblValueLengthTotal.Text = string.Format(": {0:0.###} m", (solution.LengthCut + solution.LengthFold) / 1000.0);
            // area
            lblValueArea.Text = string.Format(": {0:0.###} m²", solution.Area * 1.0E-06);
            lblValueFormat.Text = string.Format(": {0:0.#} x {1:0.#}", solution.Width, solution.Height);
            lblValueEfficiency.Text = string.Format(": {0:0.#} %", 100.0 * solution.Area / (solution.Width * solution.Height));
        }
        /// <summary>
        /// close button clicked
        /// </summary>
        public void onClose(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        /// <summary>
        /// Owner draw mode item drawing method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxSolutions_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;

            int itemHeight = listBoxSolutions.ItemHeight;
            // draw image
            Rectangle _drawRect = new Rectangle(0, 0, itemHeight, itemHeight);
            Rectangle scaledRect = _drawRect;
            Rectangle imageRect = e.Bounds;
            imageRect.Y += 1;
            imageRect.Height = scaledRect.Height;
            imageRect.X += 2;
            imageRect.Width = scaledRect.Width;

            if (null != solutions[e.Index].Thumbnail)
                g.DrawImage(solutions[e.Index].Thumbnail, imageRect);
            g.DrawRectangle(Pens.Black, imageRect);

            // draw string
            Rectangle textRect = new Rectangle(
                imageRect.Right + 2
                , imageRect.Y
                , e.Bounds.Width - imageRect.Width - 4
                , itemHeight);

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                _textBrush.Color = SystemColors.Highlight;
                g.FillRectangle(_textBrush, textRect);
                _textBrush.Color = SystemColors.HighlightText;
            }
            else
            {
                _textBrush.Color = SystemColors.Window;
                g.FillRectangle(_textBrush, textRect);
                _textBrush.Color = SystemColors.WindowText;
            }
            g.DrawString(
                solutions[e.Index].ToString()
                , e.Font
                , _textBrush
                , textRect);
        }
        #endregion

        #region ToolStrip / Menu
        private void toolStripButtonPicGEOM_Click(object sender, EventArgs e)
        {   ExportAndOpenExtension("des");   }
        private void toolStripButtonDXF_Click(object sender, EventArgs e)
        {   ExportAndOpenExtension("dxf");   }
        private void toolStripButtonAI_Click(object sender, EventArgs e)
        {   ExportAndOpenExtension("ai");    }
        private void toolStripButtonCFF2_Click(object sender, EventArgs e)
        {   ExportAndOpenExtension("cf2");   }
        private void toolStripButtonPDF_Click(object sender, EventArgs e)
        {   ExportAndOpenExtension("pdf");   }
        private void ExportAndOpenExtension(string fileExt)
        {
            try
            {
                string applicationPath = string.Empty;
                switch (fileExt)
                {
                    case "des": applicationPath = Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppDES; break;
                    case "dxf": applicationPath = Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppDXF; break;
                    case "ai": applicationPath = Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppAI; break;
                    case "cf2": applicationPath = Pic.Factory2D.Control.Properties.Settings.Default.FileOutputAppCF2; break;
                    case "pdf": break;
                    default: break;
                }
                ExportAndOpen(fileExt, applicationPath);
            }
            catch (Win32Exception ex)
            {   MessageBox.Show(ex.Message, Application.ProductName); }
            catch (Exception ex)
            {   MessageBox.Show(ex.ToString()); }        
        }

        private void ExportAndOpen(string fileExt, string sPathExectable)
        {
            if (!string.IsNullOrEmpty(sPathExectable) && System.IO.File.Exists(sPathExectable))
            {
                // build temp file path
                string tempFilePath = System.IO.Path.ChangeExtension(System.IO.Path.GetTempFileName(), fileExt);
                factoryViewer.WriteExportFile(tempFilePath, fileExt);
                // open using existing file path
                using (System.Diagnostics.Process proc = new System.Diagnostics.Process())
                {
                    proc.StartInfo.FileName = sPathExectable;
                    proc.StartInfo.Arguments = "\"" + tempFilePath + "\"";
                    proc.Start();
                }
            }
            else
            {
                string fileExportDir = Pic.Factory2D.Control.Properties.Settings.Default.FileExportDirectory;
                SaveFileDialog fd = new SaveFileDialog();
                fd.DefaultExt = fileExt;
                fd.InitialDirectory = fileExportDir;
                fd.Filter = "PicGEOM (*.des)|*.des"
                            + "|Autodesk dxf (*.dxf)|*.dxf"
                            + "|Adobe Illustrator (*.ai)|*.ai"
                            + "|Common File Format (*.cf2)|*.cf2"
                            + "|Adobe Acrobat Reader (*.pdf)|*.pdf" 
                            + "|All Files|*.*";
                switch (fileExt)
                {
                    case "des": fd.FilterIndex = 1; break;
                    case "dxf": fd.FilterIndex  = 2; break;
                    case "ai": fd.FilterIndex   = 3; break;
                    case "cf2": fd.FilterIndex = 4; break;
                    case "pdf": fd.FilterIndex = 5; break;
                    default: break;
                }
                // shaow SaveFileDialog
                if (DialogResult.OK == fd.ShowDialog())
                    factoryViewer.WriteExportFile(fd.FileName, fileExt);
            }        
        }

        private void toolStripButtonOceProCut_Click(object sender, EventArgs e)
        {
           // initialize SaveFileDialog
            SaveFileDialog fd = new SaveFileDialog();
            fd.FileName = Properties.Settings.Default.FileExportDirectory;
            fd.Filter = "Adobe Illustrator (*.ai)|*.ai|All Files|*.*";
            fd.FilterIndex = 0;
            // show save file dialog
            if (DialogResult.OK == fd.ShowDialog())
            {
                factoryViewer.WriteExportFile(fd.FileName, "des");
                Properties.Settings.Default.FileExportDirectory = Path.GetDirectoryName(fd.FileName);
            }
        }

        private void toolStripButtonCotations_Click(object sender, EventArgs e)
        {
            factoryViewer.ToggleCotations();
        }

        private void toolStripExport_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            factoryViewer.ExportDialog(item.Text);
        }
        #endregion
    }
}