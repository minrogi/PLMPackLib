#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

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
            factory.InsertCardboardFormat(solution.CardboardPosition, _cardboardFormat.Dimensions);
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
            lblValueCardboardFormat.Text = string.Format(": {0:0.###} x {1:0.###}", _cardboardFormat.Width, _cardboardFormat.Height);
            lblValueCardboardEfficiency.Text = string.Format(": {0:0.#} %", 100.0 * solution.Area / _cardboardFormat.Area);
            lblValueNumbers.Text = string.Format(": {0} ({1} x {2})", solution.PositionCount, solution.Rows, solution.Cols);
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
        {
            try
            {
                // build temp file path
                string tempFilePath = System.IO.Path.ChangeExtension(System.IO.Path.GetTempFileName(), "des");
                // write file
                factoryViewer.WriteExportFile(tempFilePath, "des");
                // open using existing file path
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.Verb = "Open";
                startInfo.CreateNoWindow = false;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.FileName = tempFilePath;
                using (Process p = new Process())
                {
                    p.StartInfo = startInfo;
                    p.Start();
                }
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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