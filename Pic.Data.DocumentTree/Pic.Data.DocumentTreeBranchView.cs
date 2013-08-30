#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Pic.Factory2D;

using Microsoft.Practices.EnterpriseLibrary.Logging;
using Pic.Logging;
#endregion

namespace Pic.Data
{
    public partial class DocumentTreeBranchView : UserControl
    {
        #region Constants
        private const int cxButton = 150, cyButton = 150;   // image button size
        #endregion

        #region Data members
        private Timer timer = new Timer();
        private ToolTip tooltip = new ToolTip();
        int i, x, y;
        List<DocTreeNode> _listDocumentNodes;
        #endregion

        #region Constructor
        public DocumentTreeBranchView()
        {
            InitializeComponent();
            // thumbnail generator
            DocTreeNode.ThumbnailGenerator = new Pic.Data.ThumbnailGeneratorPlugin();
            // layout
            AutoScroll = true;
            timer.Interval = 1;
            timer.Tick += new EventHandler(timer_Tick);
        }
        #endregion

        #region Overrides
        protected override void OnResize(EventArgs e)
        {
 	        base.OnResize(e);
            AutoScrollPosition = Point.Empty;
            int x = 0, y = 0;
            foreach (Control cntl in Controls)
            {
                cntl.Location = new Point(x, y) + (Size)AutoScrollPosition;
                AdjustXY(ref x, ref y);
            }
        }
        #endregion

        #region Display methods
        public void ShowBranch(DocTreeNodeBranch branch)
        {
            Controls.Clear();
            tooltip.RemoveAll();

            try
            {
                _listDocumentNodes = branch.childrens;
            }
            catch (Exception /*ex*/)
            {
            }
            i = x = y = 0;
            timer.Start();
        }
        #endregion

        #region Event handlers
        private void timer_Tick(object sender, EventArgs e)
        {
            if (i == _listDocumentNodes.Count)
            {
                timer.Stop();
                return;
            }

            Image image;
            SizeF sizef;
            try
            {
                image = _listDocumentNodes[i].GetThumbnail();
                sizef = new SizeF(image.Width / image.HorizontalResolution, image.Height / image.VerticalResolution);
                float fScale = Math.Min(cxButton / sizef.Width, cyButton / sizef.Height);
                sizef.Width *= fScale;
                sizef.Height *= fScale;
            }
            catch (Exception exception)
            {
                Logger.Write(exception.ToString(), Category.General, Priority.High);
                ++i;
                return;
            }
            // convert image to small size for button
            Bitmap bitmap = new Bitmap(image, Size.Ceiling(sizef));
            image.Dispose();

            // create button and add to panel
            Button btn = new Button();
            btn.Image = bitmap;
            btn.Location = new Point(x, y) + (Size)AutoScrollPosition;
            btn.Size = new Size(cxButton, cyButton);
            btn.Tag = _listDocumentNodes[i];
            btn.Click += new EventHandler(btn_Click);
            Controls.Add(btn);

            // give button a tooltip
            tooltip.SetToolTip(btn, String.Format("{0}\n{1}", _listDocumentNodes[i].Name, _listDocumentNodes[i].Description));

            // adjust i, x and y for next image
            AdjustXY(ref x, ref y);
            ++i;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (null == btn) return;
                DocTreeNode docNode = btn.Tag as DocTreeNode;
                if (null == docNode) return;
                if (docNode.IsBranch)
                {
                    if (null != BranchSelected)
                        BranchSelected(this, new DocumentTreeViewEventArgs(docNode));
                }
                else
                {
                    if (null != LeafSelected)
                        LeafSelected(this, new DocumentTreeViewEventArgs(docNode));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Logger.Write(ex.ToString(), Category.General, Priority.High);
            }
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// computes position of next button
        /// </summary>
        /// <param name="x">Abscissa</param>
        /// <param name="y">Ordinate</param>
        private void AdjustXY(ref int x, ref int y)
        {
            x += cxButton;
            if (x + cxButton > Width - SystemInformation.VerticalScrollBarWidth)
            {
                x = 0;
                y += cyButton;
            }
        }
        #endregion

        #region Delegates
        public delegate void BranchSelectHandler(object sender, DocumentTreeViewEventArgs e);
        public delegate void LeafSelectHandler(object sender, DocumentTreeViewEventArgs e);
        #endregion

        #region Events
        public event BranchSelectHandler BranchSelected;
        public event LeafSelectHandler LeafSelected;
        #endregion
    }
}
