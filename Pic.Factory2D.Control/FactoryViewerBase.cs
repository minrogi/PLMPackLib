#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

using Sharp3D.Math.Core;

using Pic.Factory2D;

using TreeDim.UserControls;
using log4net;
#endregion

namespace Pic.Factory2D.Control
{
    #region FactoryViewerBase
    public partial class FactoryViewerBase : UserControl
    {
        #region Constructor
        public FactoryViewerBase()
        {
            // set double buffering
            MethodInfo mi = typeof(System.Windows.Forms.Control).GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] args = new object[] { ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true };
            mi.Invoke(this, args);

            InitializeComponent();

            this.Load += new EventHandler(FactoryViewerBase_Load);
        }

        void FactoryViewerBase_Load(object sender, EventArgs e)
        {
            // do not process if design mode
            if (this.DesignMode) return;

            _picGraphics = new PicGraphicsControl(this);

            // define event handlers
            Paint += new PaintEventHandler(FactoryViewerBase_Paint);

            // mouse event handling
            MouseMove += new MouseEventHandler(FactoryViewerBase_MouseMove);
            MouseWheel += new MouseEventHandler(FactoryViewerBase_MouseWheel);
            MouseDown += new MouseEventHandler(FactoryViewerBase_MouseDown);
            MouseUp += new MouseEventHandler(FactoryViewerBase_MouseUp);

            showCotationsToolStripMenuItem.Checked = _showCotations;
            reflectionXToolStripMenuItem.Checked = _reflectionX;
            reflectionYToolStripMenuItem.Checked = _reflectionY;
        }
        #endregion

        #region User control override (prevent background painting)
        protected override void OnPaintBackground(PaintEventArgs e) { }
        #endregion

        #region Public properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PicFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Box2D BoundingBox
        {
            get { return _picGraphics.DrawingBox; }
        }
        public bool ReflectionX
        {
            get { return _reflectionX; }
            set
            {
                _reflectionX = value;
                reflectionXToolStripMenuItem.Checked = _reflectionX;

                // remove existing quotations
                _factory.Remove((new PicFilterCode(PicEntity.eCode.PE_COTATIONDISTANCE))
                 | (new PicFilterCode(PicEntity.eCode.PE_COTATIONHORIZONTAL))
                 | (new PicFilterCode(PicEntity.eCode.PE_COTATIONVERTICAL))
                 );
                // transform entities
                _factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionX));
                // add quotations
                PicAutoQuotation.BuildQuotation(_factory);
                FitView();
            }
        }
        public bool ReflectionY
        {
            get { return _reflectionY; }
            set
            {
                _reflectionY = value;
                reflectionYToolStripMenuItem.Checked = _reflectionY;

                // remove existing quotations
                _factory.Remove((new PicFilterCode(PicEntity.eCode.PE_COTATIONDISTANCE))
                 | (new PicFilterCode(PicEntity.eCode.PE_COTATIONHORIZONTAL))
                 | (new PicFilterCode(PicEntity.eCode.PE_COTATIONVERTICAL))
                 );
                // transform entities
                _factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionY));
                // add quotations
                PicAutoQuotation.BuildQuotation(_factory);
                FitView();
            }
        }
        public bool ShowCotations
        {
            get { return _showCotations; }
            set
            {
                _showCotations = value;
                showCotationsToolStripMenuItem.Checked = _showCotations;
                Invalidate();
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CardboardFormatLoader CardBoardFormatLoader
        {
            set
            {
                _cardboardFormatLoader = value;
            }
        }
        #endregion

        #region Public methods
        protected override void OnResize(EventArgs e)
        {
            Invalidate();
            base.OnResize(e);
        }

        public void FitView()
        {
            if (null == _picGraphics || null == _factory || this.DesignMode) return;
            // compute bounding box
            PicVisitorBoundingBox visitor = new PicVisitorBoundingBox();
            _factory.ProcessVisitor(visitor);
            Box2D box = visitor.Box;
            if (!box.IsValid)
                return;
            // set margin 1%
            box.AddMarginRatio(0.01);
            // update PicGraphics drawing box
            _picGraphics.DrawingBox = box;
            // force redraw
            Invalidate();
        }
        public bool Export(string filePath)
        {
            return WriteExportFile(filePath, System.IO.Path.GetExtension(filePath));
        }

        public bool WriteExportFile(string filePath, string fileExt)
        {
            try
            {
                // instantiate filter
                PicFilter filter = (_showCotations ? PicFilter.FilterNone : PicFilter.FilterCotation) & PicFilter.FilterNoZeroEntities;
                // get bounding box
                Pic.Factory2D.PicVisitorBoundingBox visitorBoundingBox = new Pic.Factory2D.PicVisitorBoundingBox();
                _factory.ProcessVisitor(visitorBoundingBox, filter);
                Pic.Factory2D.Box2D box = visitorBoundingBox.Box;
                // add margins : 5 % of the smallest value among height 
                box.AddMarginHorizontal(box.Width * 0.01);
                box.AddMarginVertical(box.Height * 0.01);

                string fileFormat = fileExt.TrimStart('.').ToLower();

                byte[] byteArray;
                // get file content
                if (string.Equals("des", fileFormat))
                {   // DES
                    using (Pic.Factory2D.PicVisitorDesOutput visitor = new Pic.Factory2D.PicVisitorDesOutput())
                    {
                        visitor.Author = "treeDiM";
                        _factory.ProcessVisitor(visitor, filter);
                        byteArray = visitor.GetResultByteArray();
                    }
                }
                else if (string.Equals("dxf", fileFormat))
                {   // DXF
                    using (Pic.Factory2D.PicVisitorDxfOutput visitor = new Pic.Factory2D.PicVisitorDxfOutput())
                    {
                        visitor.Author = "treeDiM";
                        _factory.ProcessVisitor(visitor, filter);
                        byteArray = visitor.GetResultByteArray();
                    }
                }
                else if (string.Equals("pdf", fileFormat))
                {   // PDF
                    using (Pic.Factory2D.PicGraphicsPdf graphics = new PicGraphicsPdf(box))
                    {
                        graphics.Author = Application.ProductName + "(" + Application.CompanyName + ")";
                        graphics.Title = Path.GetFileNameWithoutExtension(filePath);
                        _factory.Draw(graphics, filter);
                        byteArray = graphics.GetResultByteArray();
                    }
                }
                else if (string.Equals("ai", fileFormat) || string.Equals("cf2", fileFormat))
                { // AI || CF2
                    using (Pic.Factory2D.PicVisitorDiecutOutput visitor = new PicVisitorDiecutOutput(fileExt))
                    {
                        visitor.Author = Application.ProductName + "(" + Application.CompanyName + ")";
                        visitor.Title = Path.GetFileNameWithoutExtension(filePath);
                        _factory.ProcessVisitor(visitor, filter);
                        byteArray = visitor.GetResultByteArray();
                    }
                }
                else
                    throw new Exception("Invalid file format:" + fileFormat);
                // write byte array to stream
                using (System.IO.FileStream fstream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                    fstream.Write(byteArray, 0, byteArray.GetLength(0));

                return true;
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Exception: {0}", ex.ToString()));
                return false;
            }
        }

        public void ShowAboutBox()
        {
            AboutBox form = new AboutBox();
            if (DialogResult.OK == form.ShowDialog()) { }
        }

        public void BuildLayout()
        {
            FormImpositionSettings formSettings = new FormImpositionSettings();
            if (null != _cardboardFormatLoader)
                formSettings.FormatLoader = _cardboardFormatLoader;

            if (DialogResult.OK == formSettings.ShowDialog())
            {
                // build factory entities
                if (_reflectionX) _factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionX));
                if (_reflectionY) _factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionY));

                // build imposition solutions
                if (formSettings.Mode == 0)
                {
                    _impositionTool = new ImpositionToolCardboardFormat(_factory, formSettings.CardboardFormat);
                }
                else
                {
                    _impositionTool = new ImpositionToolXY(_factory, formSettings.NumberDirX, formSettings.NumberDirY);
                }
                // -> margins
                _impositionTool.SpaceBetween = new Vector2D(formSettings.ImpSpaceX, formSettings.ImpSpaceX);
                _impositionTool.Margin = new Vector2D(formSettings.ImpMarginLeftRight, formSettings.ImpMarginBottomTop);
                _impositionTool.MinMargin = new Vector2D(formSettings.ImpRemainingMarginLeftRight, formSettings.ImpRemainingMarginBottomTop);
                _impositionTool.HorizontalAlignment = formSettings.HorizontalAlignment;
                _impositionTool.VerticalAlignment = formSettings.VerticalAlignment;
                // -> patterns
                _impositionTool.AllowRotationInColumnDirection = formSettings.AllowColumnRotation;
                _impositionTool.AllowRotationInRowDirection = formSettings.AllowRowRotation;
                // -> offset
                _impositionTool.ImpositionOffset = Vector2D.Zero;

                try
                {
                    // instantiate ProgressWindow and launch process
                    ProgressWindow progress = new ProgressWindow();
                    System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(GenerateImpositionSolutions), progress);
                    progress.ShowDialog();

                    if (null != _solutions && _solutions.Count > 0)
                    {
                        FormImposition form = new FormImposition();
                        form.Solutions = _solutions;
                        form.Format = formSettings.CardboardFormat;
                        if (DialogResult.OK == form.ShowDialog(this))
                        {
                        }
                    }
                }
                catch (PicToolTooLongException /*ex*/)
                {
                    MessageBox.Show(
                        string.Format(Properties.Resources.ID_ABANDONPROCESSING
                        , Pic.Factory2D.Properties.Settings.Default.AreaMaxNoSegments)
                        , Application.ProductName
                        , MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void GenerateImpositionSolutions(object status)
        {
            IProgressCallback callback = status as IProgressCallback;
            _impositionTool.GenerateSortedSolutionList(callback, out _solutions);
        }
        #endregion

        #region Public properties
        public bool ShowNestingMenu
        {
            get
            {
                if (null != impositionToolStripMenuItem)
                    return impositionToolStripMenuItem.Visible;
                else
                    return false;
            }
            set
            {
                if (null != impositionToolStripMenuItem)
                {
                    toolStripSeparator3.Visible = value;
                    impositionToolStripMenuItem.Visible = value;
                }
            }
        }

        public bool ShowAboutMenu
        {
            get
            {
                if (null != aboutToolStripMenuItem)
                    return aboutToolStripMenuItem.Visible;
                else
                    return false;
            }
            set
            {
                if (null != aboutToolStripMenuItem)
                {
                    toolStripSeparator4.Visible = value;
                    aboutToolStripMenuItem.Visible = value;
                }
            }        
        }
        #endregion

        #region Mouse event handlers
        void FactoryViewerBase_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                Pic.Factory2D.Box2D box = new Pic.Factory2D.Box2D(_picGraphics.DrawingBox);
                box.Zoom((double)-e.Delta / 1000.0);
                _picGraphics.DrawingBox = box;
            }
            catch (InvalidBoxException ex)
            {
                _log.Error(ex.ToString());
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
            finally
            {
                Invalidate();
            }
        }
        void FactoryViewerBase_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    Point ptDiff = new Point(e.Location.X - _mousePositionPrev.X, e.Location.Y - _mousePositionPrev.Y);
                    Box2D box = _picGraphics.DrawingBox;
                    Vector2D centerScreen = new Vector2D(
                        box.Center.X - (double)ptDiff.X * box.Width / ClientSize.Width
                        , box.Center.Y + (double)ptDiff.Y * box.Height / ClientSize.Height);
                    box.Center = centerScreen;
                    _picGraphics.DrawingBox = box;

                    Invalidate();
                }
                _mousePositionPrev = e.Location;
            }
            catch (InvalidBoxException ex)
            {
                _log.Error(ex.ToString());
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
            finally
            {
            }
        }

        void FactoryViewerBase_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip.Show(this, e.Location);
            else if (e.Button == MouseButtons.Left)
                Cursor.Current = new Cursor(new System.IO.MemoryStream(Pic.Factory2D.Control.Properties.Resources.CursorPan));
        }

        void FactoryViewerBase_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }
        #endregion

        #region Context menu
        private void showCotationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCotations = !_showCotations;
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            item.Checked = _showCotations;
        }

        private void reflectionXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ReflectionX = !_reflectionX;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                item.Checked = _reflectionX;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }

        private void reflectionYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ReflectionY = !_reflectionY;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                item.Checked = _reflectionY;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }

        private void fitViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FitView();
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Exception : {0}", ex.ToString()));
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                if ("DES" == item.Text)
                {
                    saveFileDialog.Filter = "Picador file|*.des|All Files|*.*";
                }
                else if ("DXF" == item.Text)
                {
                    saveFileDialog.Filter = "Autocad dxf|*.dxf|All Files|*.*";
                }
                else if ("PDF" == item.Text)
                {
                    saveFileDialog.Filter = "Adobe Acrobat Reader (*.pdf)|*.pdf|All Files|*.*";
                }
                else if ("AI" == item.Text)
                {
                    saveFileDialog.Filter = "Adobe Illustrator|*.ai|All Files|*.*";
                }
                else if ("CF2" == item.Text)
                {
                    saveFileDialog.Filter = "Common File Format 2|*.cf2|All Files|*.*";
                }
                if (DialogResult.OK == saveFileDialog.ShowDialog())
                    Export(saveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Exception : {0}", ex.ToString()));
            }
        }

        private void impositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuildLayout();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAboutBox();
        }        
        #endregion

        #region Mouse event handlers
        private void FactoryViewerBase_Paint(object sender, PaintEventArgs e)
        {
            _picGraphics.GdiGraphics = e.Graphics;
            if (null != _factory)
                _factory.Draw(_picGraphics, _showCotations ? PicFilter.FilterNone : PicFilter.FilterCotation);
        }
        #endregion

        #region Data members
        protected static readonly ILog _log = LogManager.GetLogger(typeof(FactoryViewer));
        PicGraphicsControl _picGraphics;
        PicFactory _factory = new PicFactory();
        private Point _mousePositionPrev;
        private bool _reflectionX = false, _reflectionY = false, _showCotations = false;
        private CardboardFormatLoader _cardboardFormatLoader;

        private ImpositionTool _impositionTool;
        private List<ImpositionSolution> _solutions;
        #endregion
    }
    #endregion
}
