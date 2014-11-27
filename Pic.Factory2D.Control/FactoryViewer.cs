#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Pic.Factory2D;
using Pic.Factory2D.Control;

using Sharp3D.Math.Core;

using Pic.Factory2D.Control.Properties;

using log4net;
using TreeDim.UserControls;
#endregion

namespace Pic.Factory2D.Control
{
    #region FactoryViewer
    public partial class FactoryViewer : UserControl
    {
        #region Constructor
        public FactoryViewer()
        {
            // set double buffering
            MethodInfo mi = typeof(System.Windows.Forms.Control).GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] args = new object[] { ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true };
            mi.Invoke(this, args);

            InitializeComponent();

            this.Load += new EventHandler(FactoryViewer_Load);
        }

        void FactoryViewer_Load(object sender, EventArgs e)
        {
            // do not process if design mode
            if (this.DesignMode)
                return;

            _picGraphics = new PicGraphicsControl(this);

            // define event handlers
            Paint += new PaintEventHandler(FactoryViewer_Paint);

            // mouse event handling
            MouseMove += new MouseEventHandler(FactoryViewer_MouseMove);
            MouseWheel += new MouseEventHandler(FactoryViewer_MouseWheel);
            MouseDown += new MouseEventHandler(FactoryViewer_MouseDown);
            MouseUp += new MouseEventHandler(FactoryViewer_MouseUp);

            // event global cotation properties notification
            PicGlobalCotationProperties.Modified += new PicGlobalCotationProperties.OnGlobalCotationPropertiesModified(PicGlobalCotationProperties_Modified);

            showCotationsToolStripMenuItem.Checked = _showCotations;
            reflectionXToolStripMenuItem.Checked = _reflectionX;
            reflectionYToolStripMenuItem.Checked = _reflectionY;
        }

        void PicGlobalCotationProperties_Modified()
        {
            Refresh();
        }
        #endregion

        #region Mouse event handlers
        void FactoryViewer_MouseWheel(object sender, MouseEventArgs e)
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
        void FactoryViewer_MouseMove(object sender, MouseEventArgs e)
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

        void FactoryViewer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip.Show(this, e.Location);
            else if (e.Button == MouseButtons.Left)
                Cursor.Current = new Cursor(new System.IO.MemoryStream(Pic.Factory2D.Control.Properties.Resources.CursorPan));
        }

        void FactoryViewer_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }
        #endregion

        #region Event handlers
        private void FactoryViewer_Paint(object sender, PaintEventArgs e)
        {
            _picGraphics.GdiGraphics = e.Graphics;
            _factory.Draw(_picGraphics, _showCotations ? PicFilter.FilterNone : PicFilter.FilterCotation );
        }
        #endregion

        #region Public methods
        protected override void OnResize(EventArgs e)
        {
            Invalidate();
            base.OnResize(e);
        }
        public override void Refresh()
        {
			// sanity check
			if (null == this.Parent)
				return;
            try
            {
                // clear factory
                _factory.Clear();
                // create entities
                if (this.Parent is IEntitySupplier)
                    _entitySupplier = this.Parent as IEntitySupplier;
                if (null != _entitySupplier)
                    _entitySupplier.CreateEntities(_factory);
                else
                    _log.Debug(string.Format("Interface IEntitySupplier is not implemented by parent {0}", this.Parent.Name));
                // apply transformations
                if (_reflectionX) _factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionX));
                if (_reflectionY) _factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionY));
                // compute bounding box
                PicVisitorBoundingBox visitor = new PicVisitorBoundingBox();
                _factory.ProcessVisitor(visitor);
                Box2D box = visitor.Box;
                box.AddMarginRatio(0.01);
                // set as drawing box
                _picGraphics.DrawingBox = box;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
            base.Refresh();            
        }

        public void FitView()
        {
            if (this.DesignMode) return;
            try
            {
                // compute bounding box
                PicVisitorBoundingBox visitor = new PicVisitorBoundingBox();
                _factory.ProcessVisitor(visitor);
                Box2D box = visitor.Box;
                box.AddMarginRatio(0.01);
                // update PicGraphics drawing box
                _picGraphics.DrawingBox = box;
                // force redraw
                Invalidate();
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }

        public void ToggleCotations()
        {
            _showCotations = !_showCotations;
            Refresh();
            if (null != ShowCotationsToggled)
                ShowCotationsToggled(this, new EventArgsStatus(_showCotations, _reflectionX, _reflectionY));
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

                string author = Application.ProductName + " (" + Application.CompanyName + ")";
                string title = System.IO.Path.GetFileNameWithoutExtension(filePath);
                string fileFormat = fileExt.TrimStart('.').ToLower();

                byte[] byteArray;
                // get file content
                if ("des" == fileFormat)
                {
                    using (Pic.Factory2D.PicVisitorDesOutput visitor = new Pic.Factory2D.PicVisitorDesOutput())
                    {
                        visitor.Author = author;
                        _factory.ProcessVisitor(visitor, filter);
                        byteArray = visitor.GetResultByteArray();
                    }
                }
                else if ("dxf" == fileFormat)
                {
                    using (Pic.Factory2D.PicVisitorDxfOutput visitor = new Pic.Factory2D.PicVisitorDxfOutput())
                    {
                        visitor.Author = author;
                        _factory.ProcessVisitor(visitor, filter);
                        byteArray = visitor.GetResultByteArray();
                    }
                }
                else if ("pdf" == fileFormat)
                {
                    using (Pic.Factory2D.PicGraphicsPdf graphics = new PicGraphicsPdf(box))
                    {
                        graphics.Author = author;
                        graphics.Title = title;
                        _factory.Draw(graphics, filter);
                        byteArray = graphics.GetResultByteArray();
                    }
                }
                else if ("ai" == fileFormat || "cf2" == fileFormat)
                { 
                    using (Pic.Factory2D.PicVisitorDiecutOutput visitor = new PicVisitorDiecutOutput(fileExt))
                    {
                        visitor.Author = author;
                        visitor.Title = title;
                        _factory.ProcessVisitor(visitor, filter);
                        byteArray = visitor.GetResultByteArray();
                    }
                }
                else
                    throw new Exception("Invalid file format: " + fileFormat);
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
            if (DialogResult.OK == form.ShowDialog()) {}
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

                try
                {
                    // build imposition solutions
                    if (formSettings.Mode == 0)
                        _impositionTool = new ImpositionToolCardboardFormat(_factory, formSettings.CardboardFormat);
                    else
                        _impositionTool = new ImpositionToolXY(_factory, formSettings.NumberDirX, formSettings.NumberDirY);
                    // -> margins
                    _impositionTool.HorizontalAlignment = formSettings.HorizontalAlignment;
                    _impositionTool.VerticalAlignment = formSettings.VerticalAlignment;
                    _impositionTool.SpaceBetween = new Vector2D(formSettings.ImpSpaceX, formSettings.ImpSpaceY);
                    _impositionTool.Margin = new Vector2D(formSettings.ImpMarginLeftRight, formSettings.ImpMarginBottomTop);
                    _impositionTool.MinMargin = new Vector2D(formSettings.ImpRemainingMarginLeftRight, formSettings.ImpRemainingMarginBottomTop);
                    // -> allowed patterns
                    _impositionTool.AllowRotationInColumnDirection = formSettings.AllowColumnRotation;
                    _impositionTool.AllowRotationInRowDirection = formSettings.AllowRowRotation;
                    // -> offsets
                    _impositionTool.ImpositionOffset = new Vector2D(formSettings.OffsetX, formSettings.OffsetY);
             
                    _solutions = new List<ImpositionSolution>();

                    // instantiate ProgressWindow and launch process
                    ProgressWindow progress = new ProgressWindow();
                    System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(GenerateImpositionSolutions), progress);
                    progress.ShowDialog();

                    if (null != _solutions && _solutions.Count > 0)
                    {
                        FormImposition form = new FormImposition();
                        // set solutions
                        form.Solutions = _solutions;
                        form.Format = formSettings.CardboardFormat;
                        // show imposition dlg
                        if (DialogResult.OK == form.ShowDialog(this)) {}
                    }                     
                }
                catch (PicToolTooLongException /*ex*/)
                {
                    MessageBox.Show(
                        string.Format(Properties.Resources.ID_ABANDONPROCESSING
                        , Pic.Factory2D.Properties.Settings.Default.AreaMaxNoSegments)
                        , Application.ProductName
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Stop);
                }
                catch (Exception ex)
                {
                    _log.Error(ex.ToString());
                }
            }
        }
        private void GenerateImpositionSolutions(object status)
        {
            IProgressCallback callback = status as IProgressCallback;
            _impositionTool.GenerateSortedSolutionList(callback, out _solutions);
        }

        private void ComputeLengthAndArea(ref double unitLengthCut, ref double unitLengthFold, ref double unitArea)
        { 
            unitLengthCut = 0.0;
            unitLengthFold = 0.0;
            unitArea = 0.0;
        }
        #endregion
        
        #region User control override
        protected override void OnPaintBackground(PaintEventArgs e) { }
        #endregion

        #region Public properties
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
                Refresh();
                if (null != ReflectionXToggled)
                    ReflectionXToggled(this, new EventArgsStatus(_showCotations, _reflectionX, _reflectionY));

            }
        }
        public bool ReflectionY
        {
            get { return _reflectionY; }
            set
            {
                _reflectionY = value;
                reflectionYToolStripMenuItem.Checked = _reflectionY;
                Refresh();
                if (null != ReflectionYToggled)
                    ReflectionYToggled(this, new EventArgsStatus(_showCotations, _reflectionX, _reflectionY));
            }
        }
        public bool ShowCotations
        {
            get { return _showCotations; }
            set
            {
                _showCotations = value;
                showCotationsToolStripMenuItem.Checked = _showCotations;
                Refresh();
                if (null != ShowCotationsToggled)
                    ShowCotationsToggled(this, new EventArgsStatus(_showCotations, _reflectionX, _reflectionY));
            }
        }
        public CardboardFormatLoader CardBoardFormatLoader
        {
            set
            {
                _cardboardFormatLoader = value; 
            }
        }

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
            FitView();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ExportDialog( item.Text );
        }

        public void ExportDialog(string fileExt)
        {
            // filter string
            if (string.IsNullOrEmpty(saveFileDialog.Filter))
                saveFileDialog.Filter = "Picador file (*.des)|*.des|" +
                                        "Autocad dxf (*.dxf)|*.dxf|" +
                                        "Adobe Illustrator (*.ai)|*.ai|" +
                                        "Common File Format2 (*.cf2)|*.cf2|" +
                                        "Adobe Acrobat Reader (*.pdf)|*.pdf|" +
                                        "All Files (*.*)|*.*";
            // filter index
            if (string.Equals("DES", fileExt))
                saveFileDialog.FilterIndex = 1;
            else if (string.Equals("DXF", fileExt, StringComparison.CurrentCultureIgnoreCase))
                saveFileDialog.FilterIndex = 2;
            else if (string.Equals("AI", fileExt, StringComparison.CurrentCultureIgnoreCase))
                saveFileDialog.FilterIndex = 3;
            else if (string.Equals("CF2", fileExt, StringComparison.CurrentCultureIgnoreCase))
                saveFileDialog.FilterIndex = 4;
            else if (string.Equals("PDF", fileExt, StringComparison.CurrentCultureIgnoreCase))
                saveFileDialog.FilterIndex = 5;
            // show dialog and export
            if (DialogResult.OK == saveFileDialog.ShowDialog())
                Export(saveFileDialog.FileName);           
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

        #region Data members
        protected static readonly ILog _log = LogManager.GetLogger(typeof(FactoryViewer));
        IEntitySupplier _entitySupplier;
        PicGraphicsControl _picGraphics;
        PicFactory _factory = new PicFactory();
        private Point _mousePositionPrev;
        private bool _reflectionX = false, _reflectionY = false, _showCotations = false;
        private CardboardFormatLoader _cardboardFormatLoader;

        private ImpositionTool _impositionTool;
        private List<ImpositionSolution> _solutions;

        public delegate void StatusChanged(object sender, EventArgsStatus e);
        public event StatusChanged ShowCotationsToggled;
        public event StatusChanged ReflectionXToggled;
        public event StatusChanged ReflectionYToggled;
        #endregion
    }
    #endregion

    #region EventArgsStatus class
    public class EventArgsStatus : EventArgs
    {
        #region Constructors
        public EventArgsStatus(bool showCotations, bool reflectionX, bool reflectionY)
            :base()
        {
            _showCotations = showCotations;
            _reflectionX = reflectionX;
            _reflectionY = reflectionY;
        }
        #endregion

        #region Public properties
        public bool ShowCotations { get { return _showCotations; } }
        public bool ReflectionX { get { return _reflectionX; } }
        public bool ReflectionY { get { return _reflectionY; } }
        #endregion

        #region Data members
        private bool _reflectionX, _reflectionY, _showCotations;
        #endregion
    }
    #endregion

    #region PicGraphicsControl
    internal class PicGraphicsControl : PicGraphicsGdiPlus
    {
        #region Contructor
        public PicGraphicsControl(System.Windows.Forms.Control parentControl)
        {
            _parentControl = parentControl;
            _parentControl.SizeChanged += new EventHandler(_parentControl_SizeChanged);
        }

        void _parentControl_SizeChanged(object sender, EventArgs e)
        {
            // force box recomputation
            if (_box.IsValid)
            {
                Pic.Factory2D.Box2D box = _box;
                DrawingBox = box;
            }
        }
        #endregion

        #region PicGraphicsGdiPlus override
        public Graphics GdiGraphics
        {
            set { _gdiGraphics = value; }
        }

        public override Size GetSize()
        {
            return _parentControl.Size;
        }
        #endregion

        #region Fields
        private System.Windows.Forms.Control _parentControl;
        #endregion
    }
    #endregion

    #region  IEntitySupplier interface
    public interface IEntitySupplier
    {
        void CreateEntities(PicFactory factory);
    }
    #endregion
}