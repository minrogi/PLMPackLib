#region Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;

using Sharp3D.Math.Core;
using Sharp3D.Math.Geometry2D;

using Pic.Plugin;
using Pic.Factory2D;

using TreeDim.UserControls;

using log4net;
#endregion

namespace Pic.Plugin.ViewCtrl
{
    [
    ComVisible(true),
    ClassInterface(ClassInterfaceType.AutoDispatch),
    Docking(DockingBehavior.AutoDock)
    ]
    public partial class PluginViewCtrl : SplitContainer
    {
        #region Data members
        [NonSerialized]private Pic.Plugin.Component _component;
        [NonSerialized]private Pic.Plugin.IComponentSearchMethod _searchMethod;
        [NonSerialized]private ProfileLoader _profileLoader;
        [NonSerialized]private CardboardFormatLoader _cardboardFormatLoader;
        [NonSerialized]private PicGraphicsCtrlBased _picGraphics;
        private Point _mousePositionPrev;
        private bool _computeBbox = true;
        private bool _reflectionX = false, _reflectionY= false;
        private bool _buttonCloseVisible = false;
        private bool _buttonValidateVisible = false;
        private ComboBox _comboProfile;
        private bool _showCotations = true;
        private bool _showSummary = false;
        private Box2D _box;
        private Button btClose;
        private Button btValidate;

        private ImpositionTool _impositionTool;
        private List<ImpositionSolution> _solutions;

        private double _thickness = 0.0;
        private ILocalizer _localizer = null;
        /// <summary>
        /// 
        /// </summary>
        [NonSerialized]private Pic.Factory2D.Control.FactoryDataCtrl factoryDataCtrl;
        /// <summary>
        /// current parameter stack
        /// </summary>
        [NonSerialized]private Pic.Plugin.ParameterStack _paramStackCurrent;
        [NonSerialized]private bool _dirtyParameters = true;
        [NonSerialized]private bool _hasDependancies = false;
        public delegate void DependancyStatusChangedHandler(bool hasDependancy);
        public event DependancyStatusChangedHandler DependancyStatusChanged;
        // log4net
        protected static readonly ILog _log = LogManager.GetLogger(typeof(PluginViewCtrl));
        // *** parameter animation ***
        protected ParameterStack tempParameterStack = null;
        protected string _parameterName = string.Empty;
        protected int _timerStep = 0;
        protected int _noStepsTimer = 10;
        protected double _parameterInitialValue = 0.0;
        // timer
        protected Timer timer = null;
        // *** parameter animation ***
       #endregion

        #region Browsable events
        [Category("Configuration"), Browsable(true), Description("Event raised by user click on Close button")]
        public event EventHandler CloseClicked;
        [Category("Configuration"), Browsable(true), Description("Event raised by user click on Validate button")]
        public event EventHandler ValidateClicked;
        #endregion

        #region Constructor
        public PluginViewCtrl()
        {
            try
            {
                //must use reflection to set double buffering on the child panel controls  
                MethodInfo mi = typeof(Control).GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance);
                object[] args = new object[] { ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true };
                mi.Invoke(this.Panel1, args);
                mi.Invoke(this.Panel2, args);

                InitializeComponent();

                this.SplitterWidth = 1;

                if (Properties.Settings.Default.AllowParameterAnimation)
                {
                    // *** timer used for parameter animation
                    timer = new Timer();
                    timer.Interval = 100;
                    timer.Tick += new EventHandler(_timer_Tick);
                    _noStepsTimer = Properties.Settings.Default.NumberOfAnimationSteps;
                }
                else
                {
                    timer = null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        protected override void  InitLayout()
        {
 	         base.InitLayout();

            // define event handlers
            Panel1.Paint += new PaintEventHandler(Panel1_Paint);
            // mouse event handling
            Panel1.MouseDown += new MouseEventHandler(Panel1_MouseDown);
            Panel1.MouseMove += new MouseEventHandler(Panel1_MouseMove);
            Panel1.MouseWheel += new MouseEventHandler(Panel1_MouseWheel);

            _computeBbox = true;

            // event global cotation properties notification
            PicGlobalCotationProperties.Modified += new PicGlobalCotationProperties.OnGlobalCotationPropertiesModified(PicGlobalCotationProperties_Modified);


            // context menu
            this.showCotationsToolStripMenuItem.Checked = _showCotations;
            this.reflectionXToolStripMenuItem.Checked = _reflectionX;
            this.reflectionYToolStripMenuItem.Checked = _reflectionY;

            // splitter
            this.SplitterWidth = 1;
        }
        void PicGlobalCotationProperties_Modified()
        {
            Refresh();
        }
        #endregion

        #region Mouse event handlers
        private void Panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                Pic.Factory2D.Box2D box = new Pic.Factory2D.Box2D(_picGraphics.DrawingBox);
                box.Zoom((double)-e.Delta / 1000.0);
                _picGraphics.DrawingBox = box;
            }
            catch (InvalidBoxException /*ex*/)
            {
                _computeBbox = true;
            }
            catch (Exception ex)
            {
                _log.Debug(ex.ToString());
            }
            finally
            {
                Panel1.Invalidate();
            }
        }
        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Panel1.Focus();
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    Point ptDiff = new Point(e.Location.X - _mousePositionPrev.X, e.Location.Y - _mousePositionPrev.Y);
                    double aspectRatio = 1.0;

                    Box2D box = _picGraphics.DrawingBox;

                    Vector2D centerScreen = new Vector2D(
                        box.Center.X - (double)ptDiff.X * box.Width / (double)Panel1.ClientSize.Width
                        , box.Center.Y + (double)ptDiff.Y * box.Height / ((double)Panel1.ClientSize.Height * aspectRatio));
                    box.Center = centerScreen;
                    _picGraphics.DrawingBox = box;

                    Panel1.Invalidate();
                }
                _mousePositionPrev = e.Location;
            }
            catch (InvalidBoxException /*ex*/)
            {
                _computeBbox = true;
            }
            catch (Exception ex)
            {
                _log.Debug(ex.ToString());
            }
            finally
            {
            }
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip.Show(this, e.Location);
            else if (e.Button == MouseButtons.Left)
                Cursor.Current = new Cursor(new System.IO.MemoryStream(Pic.Plugin.ViewCtrl.Properties.Resource.CursorPan));
        }

        void Panel2_MouseEnter(object sender, EventArgs e)
        {
            if (Panel1.Focused)
                Panel2.Focus();
        }
        #endregion

        #region Public methods
        public void BuildLayout()
        {
            Pic.Factory2D.Control.FormImpositionSettings formSettings = new Pic.Factory2D.Control.FormImpositionSettings();
            if (null != _cardboardFormatLoader)
                formSettings.FormatLoader = _cardboardFormatLoader;

            // get offset
            Vector2D impositionOffset = Component.ImpositionOffset(CurrentParameterStack);
            // initialize form with offsets
            formSettings.OffsetX = impositionOffset.X;
            formSettings.OffsetY = impositionOffset.Y;

            if (DialogResult.OK == formSettings.ShowDialog())
            {
                using (PicFactory factory = new PicFactory())
                {
                    // build factory entities
                    Component.CreateFactoryEntities(factory, CurrentParameterStack);
                    if (_reflectionX) factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionX));
                    if (_reflectionY) factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionY));
                    // build imposition solutions
                    if (formSettings.Mode == 0)
                        _impositionTool = new ImpositionToolCardboardFormat(factory, formSettings.CardboardFormat);
                    else
                        _impositionTool = new ImpositionToolXY(factory, formSettings.NumberDirX, formSettings.NumberDirY);
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
                    // instantiate ProgressWindow and launch process
                    ProgressWindow progress = new ProgressWindow();
                    System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(GenerateImpositionSolutions), progress);
                    progress.ShowDialog();
                    // show dialog
                    if (null != _solutions && _solutions.Count > 0)
                    {
                        Pic.Factory2D.Control.FormImposition form = new Pic.Factory2D.Control.FormImposition();
                        form.Solutions = _solutions;
                        form.Format = formSettings.CardboardFormat;
                        if (DialogResult.OK == form.ShowDialog(this))
                        {
                        }
                    }
                }
            }            
        }

        private void GenerateImpositionSolutions(object status)
        {
            IProgressCallback callback = status as IProgressCallback;
            _impositionTool.GenerateSortedSolutionList(callback, out _solutions);
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
            ReflectionX = !_reflectionX;
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            item.Checked = _reflectionX;
        }

        private void reflectionYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReflectionY = !_reflectionY;
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            item.Checked = _reflectionY;
        }

        private void fitViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FitView();
        }

        private void impositionToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (Component == null) return;
            BuildLayout();
        }
        #endregion

        #region Setting loaders
        public IComponentSearchMethod SearchMethod { set { _searchMethod = value; } }
        public ProfileLoader ProfileLoader
        {
            set
            {
                _profileLoader = value;
                if (null != _profileLoader)
                    _profileLoader.MajorationModified += new ProfileLoader.MajorationModifiedHandler(_profileLoader_MajorationModified);
            }
        }

        void _profileLoader_MajorationModified(object sender)
        {
            Refresh();
        }
        public CardboardFormatLoader CardboardFormatLoader
        {
            set
            {
                _cardboardFormatLoader = value;
            }
        }
        #endregion

        #region Public properties
        /// <summary>
        /// set/get the current component
        /// </summary>
        [Browsable(false)]
        [Category("PluginViewCtrl")]
        [Description("Access component object")]
        [DisplayName("Component")]
        public Pic.Plugin.Component Component
        {
            get
            {
                return _component;
            }
            set
            {
                // recomputation of parameter list
                ClearParameterStack();
                // set dependancies
                HasDependancies = false;

                _component = value;

                // update panel2 controls
                this.Panel2.AutoScroll = true;
                CreatePluginControls();
                // fit view
                FitView();
            }
        }

        /// <summary>
        ///  set the plugin path
        /// </summary>
        [Browsable(true)]
        [Category("PluginViewCtrl")]
        [Description("Sets the plugin path")]
        [DisplayName("PluginPath")]
        [DefaultValue("")]
        public string PluginPath
        {
            set
            {
                // recomputation of parameter list
                ClearParameterStack();
                // set dependancies
                HasDependancies = false;

                // load component
                using (ComponentLoader loader = new ComponentLoader())
                {
                    // load plugin -> will throw exception if plugin file cannot be found
                    if (null != _searchMethod)
                        loader.SearchMethod = _searchMethod;
                    _component = loader.LoadComponent(value);
                }
                // update panel2 controls
                this.Panel2.AutoScroll = true;
                CreatePluginControls();
                // fit view
                FitView();
            }
            get { return ""; }  // is needed to have "PluginPath" appear as a property of the control
        }

        public bool ReflectionX
        {
            get { return _reflectionX; }
            set
            {
                if (DesignMode)
                    return;
                try
                {
                    _reflectionX = value;
                    _computeBbox = true;
                    Panel1.Invalidate();
                }
                catch (Exception /*ex*/)
                {
                }
            }
        }

        public bool ReflectionY
        {
            get { return _reflectionY; }
            set
            {
                if (DesignMode)
                    return;
                try
                {
                    _reflectionY = value;
                    _computeBbox = true;
                    Panel1.Invalidate();
                }
                catch (Exception /*ex*/)
                {
                }
            }
        }

        public bool ShowCotations
        {
            get { return _showCotations; }
            set
            {
                if (DesignMode)
                    return;
                try
                {
                    _showCotations = value;
                    Panel1.Invalidate();
                }
                catch (Exception /*ex*/)
                {
                }
            }
        }

        public bool ShowSummary
        {
            get { return _showSummary; }
            set { _showSummary = value; }
        }

        public bool ValidateButtonVisible
        {
            get { return _buttonValidateVisible; }
            set
            {
                _buttonValidateVisible = value;
                if (null != btValidate) btValidate.Visible = value;
                if (_buttonValidateVisible) this.CloseButtonVisible = true;
            }
        }

        public bool CloseButtonVisible
        {
            get { return _buttonCloseVisible; }
            set
            {
                _buttonCloseVisible = value;
                if (null != btClose) btClose.Visible = value;
            }
        }
        public Dictionary<string, double> ParamValues
        {
            get
            {
                if (this.DesignMode || null == Component) return null;
                // copy parameters to Dictionary<string, double>
                Dictionary<string, double> dict = new Dictionary<string, double>();
                foreach (Parameter param in CurrentParameterStack)
                {
                    ParameterDouble paramDouble = param as ParameterDouble;
                    if (null != paramDouble)
                        dict.Add(paramDouble.Name, paramDouble.Value);
                }
                return dict;
            }
            set
            {
                foreach (Control ctrl in Panel2.Controls)
                {
                    if (ctrl.GetType() == typeof(NumericUpDown))
                    {
                        NumericUpDown nud = ctrl as NumericUpDown;
                        if (nud.Name.Contains("nud")
                            && value.ContainsKey(nud.Name.Substring(3)))
                        {
                            nud.Value = (decimal)value[nud.Name.Substring(3)];
                        }
                    }
                }
                SetParametersDirty();
                Panel1.Invalidate();
            }
        }

        /// <summary>
        /// Accessing entities bounding box
        /// </summary>
        public Pic.Factory2D.Box2D BoundingBox
        {
            get
            {
                Pic.Factory2D.Box2D box;
                using (Pic.Factory2D.PicFactory factory = new Pic.Factory2D.PicFactory())
                {
                    // generate entities
                    if (Component != null)
                    {
                        Component.CreateFactoryEntities(factory, CurrentParameterStack);
                        if (_reflectionX) factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionX));
                        if (_reflectionY) factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionY));
                    }
                    // get bounding box
                    Pic.Factory2D.PicVisitorBoundingBox visitor = new Pic.Factory2D.PicVisitorBoundingBox();
                    factory.ProcessVisitor(visitor);
                    box = visitor.Box;
                    box.AddMarginRatio(0.05);

                }
                return box;
            }
        }
        /// <summary>
        /// Loaded component name
        /// </summary>
        public string LoadedComponentName
        {
            get { return _component.Name; }
        }
        #endregion

        #region FitView method
        public void FitView()
        {
            _computeBbox = true;
            Panel1.Invalidate();
        }
        #endregion

        #region Painting
        /// <summary>
        /// Paint handler
        /// </summary>
        /// <param name="obj">Sender</param>
        /// <param name="e">Argument</param>
        protected void Panel1_Paint(object obj, PaintEventArgs e)
        {
            if (null == Component || DesignMode)
                return;
            RePaint(e, null != tempParameterStack ? tempParameterStack : CurrentParameterStack);
        }

        private void RePaint(PaintEventArgs e, ParameterStack stack)
        { 
            try
            {
                // instantiate PicGraphics
                if (_picGraphics == null)
                    _picGraphics = new PicGraphicsCtrlBased(this.Panel1.ClientSize, e.Graphics);
                _picGraphics.GdiGraphics = e.Graphics;
                _picGraphics.Size = this.Panel1.ClientSize;

                // instantiate filter
                PicFilter filter = _showCotations ? PicFilter.FilterNone : PicFilter.FilterCotation;

                // build factory
                using (Pic.Factory2D.PicFactory factory = new Pic.Factory2D.PicFactory())
                {
                    // create entities
                    Component.CreateFactoryEntities(factory, stack);
                    if (_reflectionX) factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionX));
                    if (_reflectionY) factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionY));

                    // remove existing quotations
                    factory.Remove((new PicFilterCode(PicEntity.eCode.PE_COTATIONDISTANCE))
                                    | (new PicFilterCode(PicEntity.eCode.PE_COTATIONHORIZONTAL))
                                    | (new PicFilterCode(PicEntity.eCode.PE_COTATIONVERTICAL))
                                    );
                    // build auto quotation
                    if (_showCotations)
                        PicAutoQuotation.BuildQuotation(factory);

                    // update drawing box?
                    if (_computeBbox)
                    {
                        Pic.Factory2D.PicVisitorBoundingBox visitor = new Pic.Factory2D.PicVisitorBoundingBox();
                        factory.ProcessVisitor(visitor);
                        _box = new Box2D(visitor.Box);
                        Box2D box = visitor.Box;
                        box.AddMarginRatio(0.05);
                        _picGraphics.DrawingBox = box;

                        // update factory data
                        if (null != factoryDataCtrl)
                            factoryDataCtrl.Factory = factory;

                        _computeBbox = false;
                    }
                    // draw
                    factory.Draw(_picGraphics, filter);
                }
            }
            catch (Pic.Plugin.PluginException ex)
            {
                // might happen
                _picGraphics.ShowMessage(ex.Message);
            }
            catch (Exception ex)
            {
                _picGraphics.ShowMessage(ex.ToString());
                _log.Error(ex.ToString());
            }        
        }

        /// <summary>
        /// OnPaintBackground is overriden to be disabled for double buffering
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e) { }
        #endregion

        #region File export
        public static List<FileFormat> GetExportFormatList()
        {
            List<FileFormat> list = new List<FileFormat>();
            list.Add(new Pic.Factory2D.FileFormat("treeDim Picador" , "des", ""));
            list.Add(new Pic.Factory2D.FileFormat("AutoCAD dxf"     , "dxf", ""));
            list.Add(new Pic.Factory2D.FileFormat("Adobe Acrobat"   , "pdf", ""));
            list.Add(new Pic.Factory2D.FileFormat("Adobe Illustrator", "ai", ""));
            list.Add(new Pic.Factory2D.FileFormat("Common File Format 2", "cf2", ""));
            return list;
        }

        public void WriteExportFile(string filePath, string fileExt)
        {
            // get export byte array
            byte[] byteArray = GetExportFile(fileExt, CurrentParameterStack);
            // write byte array to stream
            using (System.IO.FileStream fstream = new FileStream(filePath, FileMode.Create))
                fstream.Write(byteArray, 0, byteArray.GetLength(0));
        }

        public byte[] GetExportFile(string fileExt, Pic.Plugin.ParameterStack stack)
        {
            // build factory
            Pic.Factory2D.PicFactory factory = new Pic.Factory2D.PicFactory();
            Component.CreateFactoryEntities(factory, stack);
            if (_reflectionX) factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionX));
            if (_reflectionY) factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionY));

            // instantiate filter
            PicFilter filter = (_showCotations ? PicFilter.FilterNone : PicFilter.FilterCotation) & PicFilter.FilterNoZeroEntities;

            // get bounding box
            Pic.Factory2D.PicVisitorBoundingBox visitorBoundingBox = new Pic.Factory2D.PicVisitorBoundingBox();
            factory.ProcessVisitor(visitorBoundingBox, filter);
            Pic.Factory2D.Box2D box = visitorBoundingBox.Box;
            // add margins : 5 % of the smallest value among height 
            box.AddMarginHorizontal(box.Width * 0.05);
            box.AddMarginVertical(box.Height * 0.05);

            string author = Application.ProductName + " (" + Application.CompanyName + ")";
            string title = Application.ProductName + " export";
            string fileFormat = fileExt.StartsWith(".") ? fileExt.Substring(1) : fileExt;
            fileFormat = fileFormat.ToLower();

            // get file content
            if (string.Equals("des", fileFormat))
            {
                Pic.Factory2D.PicVisitorDesOutput visitor = new Pic.Factory2D.PicVisitorDesOutput();
                visitor.Author = author;
                factory.ProcessVisitor(visitor, filter);
                return visitor.GetResultByteArray();
            }
            else if (string.Equals("dxf", fileFormat))
            {
                Pic.Factory2D.PicVisitorOutput visitor = new Pic.Factory2D.PicVisitorDxfOutput();
                visitor.Author = author;
                factory.ProcessVisitor(visitor, filter);
                return visitor.GetResultByteArray();
            }
            else if (string.Equals("pdf", fileFormat))
            {
                Pic.Factory2D.PicGraphicsPdf graphics = new PicGraphicsPdf(box);
                graphics.Author = author;
                graphics.Title = title;
                factory.Draw(graphics, filter);
                return graphics.GetResultByteArray();
            }
            else if (string.Equals("ai", fileFormat) || string.Equals("cf2", fileFormat))
            {
                Pic.Factory2D.PicVisitorDiecutOutput visitor = new Pic.Factory2D.PicVisitorDiecutOutput(fileFormat);
                visitor.Author = author;
                visitor.Title = title;
                factory.ProcessVisitor(visitor, filter);
                return visitor.GetResultByteArray();
            }
            else
                throw new Exception("Invalid file format:" + fileFormat);
        }

        public bool GetReferencePointAndThickness(ref Vector2D v, ref double thickness)
        {
            if (!Component.IsSupportingAutomaticFolding)
                return false;
            ParameterStack stack = CurrentParameterStack;
            thickness = stack.GetDoubleParameterValue("th1");
            v = Component.ReferencePoint(stack);
            return true;
        }

        #endregion

        #region Plugin parameter controls
        /// <summary>
        /// Instantiate all parameters
        /// </summary>
        protected void CreatePluginControls()
        {
            Component component = Component;
            if (component == null)
                return;

            const int lblX = 10, nudX = 160, incY = 26;
            Size lblSize = new Size(150, 18);
            Size lblValueSize = new Size(70, 12);
            Size tbSize = new Size(40, 12);
            Size chkbSize = new Size(150, 18);
            Size nudSize = new Size(70, 12);
            Size lblComboSize = new Size(140, 12);
            Size cbSize = new Size(140, 22);
            int posY = 10;
            int tabIndex = 0;

            ParameterStack stack = CurrentParameterStack;

            // clear controls
            Panel2.Controls.Clear();

            // check if plugin has some majorations
            if (stack.HasMajorations)
            {
                if (null != _profileLoader)
                {
                    Size btSize = new Size(50, 22);

                    // lbl 
                    Label lbl = new Label();
                    lbl.Text = Pic.Plugin.ViewCtrl.Properties.Resource.STR_PROFILE;
                    lbl.Location = new System.Drawing.Point(lblX, posY);
                    lbl.Size = new Size(nudX - cbSize.Width + nudSize.Width - lblX, lblSize.Height);
                    lbl.TabIndex = ++tabIndex;
                    Panel2.Controls.Add(lbl);
                    // create combo
                    _comboProfile = new ComboBox();
                    _comboProfile.DropDownStyle = ComboBoxStyle.DropDownList;
                    _comboProfile.Location = new System.Drawing.Point(nudX - cbSize.Width + nudSize.Width, posY);
                    _comboProfile.Size = cbSize;
                    _comboProfile.TabIndex = ++tabIndex;
                    _comboProfile.Items.AddRange(_profileLoader.Profiles);
                    _comboProfile.SelectedItem = _profileLoader.Selected;
                    _comboProfile.SelectedIndexChanged += new EventHandler(OnProfileSelected);
                    Panel2.Controls.Add(_comboProfile);
                    // increment Y
                    posY += incY;
                    // create edit majorations button
                    Button bt = new Button();
                    bt.Text = Pic.Plugin.ViewCtrl.Properties.Resource.STR_TOL;
                    bt.Location = new System.Drawing.Point(nudX, posY);
                    bt.Size = new Size(nudSize.Width, cbSize.Height);
                    bt.Click += new EventHandler(btEditMajorations_Click);
                    Panel2.Controls.Add(bt);

                    // increment Y
                    posY += incY;
                }
                else
                    _log.Info("Plugin has majorations but no profile loader implementation is available!\nMajoration handled as default parameters");
            }
            else // no majorations
            {
                _comboProfile = null;
				if (stack.HasParameter("th1"))
                	_thickness = stack.GetDoubleParameterValue("th1");

                // lbl
                Label lbl = new Label();
                lbl.Text = Pic.Plugin.ViewCtrl.Properties.Resource.STR_THICKNESS;
                lbl.Location = new System.Drawing.Point(lblX, posY);
                lbl.Size = new Size(nudX - cbSize.Width + nudSize.Width - lblX, lblSize.Height);
                lbl.TabIndex = ++tabIndex;
                Panel2.Controls.Add(lbl);
                // create edit field
                NumericUpDown nudThickness = new NumericUpDown();
                nudThickness.Name = "nudThickness";
                nudThickness.Minimum = 0.0M;
                nudThickness.Maximum = 10000.0M;
                nudThickness.DecimalPlaces = 2;
                nudThickness.ThousandsSeparator = false;
                nudThickness.Value = Convert.ToDecimal(_thickness);
                nudThickness.Location = new System.Drawing.Point(nudX, posY);
                nudThickness.Size = nudSize;
                nudThickness.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
                nudThickness.ValueChanged += new EventHandler(ParameterChanged);
                nudThickness.LostFocus += new EventHandler(nud_LostFocus);
                nudThickness.GotFocus += new EventHandler(nud_GotFocus);
                nudThickness.Click += new EventHandler(nud_GotFocus);
                nudThickness.TabIndex = ++tabIndex;
                Panel2.Controls.Add(nudThickness);

                // increment Y
                posY += incY;
            }

            // create unknown parameters
            foreach (Pic.Plugin.Parameter param in CurrentParameterStack.ParameterList)
            {
                // do not create default parameters
                if (_profileLoader != null && _profileLoader.HasParameter(param)
                    || param.IsMajoration)
                    continue;

                if (param.GetType() == typeof(ParameterDouble))
                {
                    ParameterDouble paramDouble = param as ParameterDouble;

                    // do not show special parameters
                    if (string.Equals(paramDouble.Name, "ep1") || string.Equals(paramDouble.Name, "th1"))
                        continue;

                    Label lbl = new Label();
                    lbl.Text = Translate(paramDouble.Description) + " (" + paramDouble.Name + ")";
                    lbl.Location = new System.Drawing.Point(lblX + param.IndentValue, posY);
                    lbl.Size = lblSize;
                    lbl.TabIndex = ++tabIndex;
                    Panel2.Controls.Add(lbl);

                    NumericUpDown nud = new NumericUpDown();
                    nud.Name = "nud" + paramDouble.Name;
                    nud.Minimum = paramDouble.HasValueMin ? (decimal)paramDouble.ValueMin : 0;
                    nud.Maximum = paramDouble.HasValueMax ? (decimal)paramDouble.ValueMax : 10000;
                    nud.DecimalPlaces = 2;
                    nud.ThousandsSeparator = false;
                    nud.Value = (decimal)paramDouble.Value;
                    nud.Location = new System.Drawing.Point(nudX + param.IndentValue, posY);
                    nud.Size = nudSize;
                    nud.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
                    nud.ValueChanged += new EventHandler(ParameterChanged);
                    nud.LostFocus += new EventHandler(nud_LostFocus);
                    nud.GotFocus += new EventHandler(nud_GotFocus);
                    nud.Click +=new EventHandler(nud_GotFocus);
                    if (null != timer)
                        nud.MouseEnter += new EventHandler(nud_MouseEnter);
                    nud.TabIndex = ++tabIndex;
                    Panel2.Controls.Add(nud);
                }
                else if (param.GetType() == typeof(ParameterInt))
                {
                    ParameterInt paramInt = param as ParameterInt;

                    Label lbl = new Label();
                    lbl.Text = Translate(paramInt.Description) + " (" + Translate(paramInt.Name) + ")";
                    lbl.Location = new System.Drawing.Point(lblX + param.IndentValue, posY);
                    lbl.Size = lblSize;
                    lbl.TabIndex = ++tabIndex;
                    Panel2.Controls.Add(lbl);

                    NumericUpDown nud = new NumericUpDown();
                    nud.Name = "nud" + paramInt.Name;
                    nud.Minimum = paramInt.HasValueMin ? paramInt.ValueMin : 0;
                    nud.Maximum = paramInt.HasValueMax ? paramInt.ValueMax : 10000;
                    nud.DecimalPlaces = 0;
                    nud.ThousandsSeparator = false;
                    nud.Value = paramInt.Value;
                    nud.Location = new System.Drawing.Point(nudX + param.IndentValue, posY);
                    nud.Size = nudSize;
                    nud.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
                    nud.ValueChanged += new EventHandler(ParameterChanged_WithRecreate);
                    nud.TabIndex = ++tabIndex;
                    Panel2.Controls.Add(nud);
                }
                else if (param.GetType() == typeof(ParameterBool))
                {
                    ParameterBool paramBool = param as ParameterBool;

                    CheckBox chkb = new CheckBox();
                    chkb.Name = "chkb" + paramBool.Name;
                    chkb.Text = Translate(paramBool.Description) + " (" + Translate(paramBool.Name) + ")";
                    chkb.Location = new System.Drawing.Point(lblX + param.IndentValue, posY);
                    chkb.Size = chkbSize;
                    chkb.Checked = paramBool.Value;
                    chkb.CheckedChanged += new EventHandler(ParameterChanged_WithRecreate);
                    chkb.TabIndex = ++tabIndex;
                    Panel2.Controls.Add(chkb);
                }
                else if (param.GetType() == typeof(ParameterMulti))
                {
                    ParameterMulti paramMulti = param as ParameterMulti;

                    Label lbl = new Label();
                    lbl.Text = Translate(paramMulti.Description) + " (" + Translate(paramMulti.Name) + ")";
                    lbl.Location = new System.Drawing.Point(lblX + param.IndentValue, posY);
                    lbl.Size = new Size(nudX - cbSize.Width + nudSize.Width - lblX, lblSize.Height);
                    lbl.TabIndex = ++tabIndex;
                    Panel2.Controls.Add(lbl);

                    ComboBox combo = new ComboBox();
                    combo.Name = "cb" + paramMulti.Name;
                    combo.DropDownStyle = ComboBoxStyle.DropDownList;
                    combo.Location = new System.Drawing.Point(nudX - cbSize.Width + nudSize.Width+param.IndentValue, posY);
                    combo.Size = cbSize;
                    combo.TabIndex = ++tabIndex;
                    combo.Items.AddRange(Translate(paramMulti.DisplayList));
                    combo.SelectedIndex = paramMulti.Value;
                    combo.SelectedIndexChanged += new EventHandler(ParameterChanged_WithRecreate);
                    Panel2.Controls.Add(combo);
                }
                posY += incY;
            }

            // FactoryDataCtrl
            if (_showSummary && !_buttonCloseVisible && !_buttonValidateVisible)
            {
                factoryDataCtrl = new Pic.Factory2D.Control.FactoryDataCtrl();
                factoryDataCtrl.Anchor = (System.Windows.Forms.AnchorStyles)
                    (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left);
                factoryDataCtrl.Location = new Point(lblX, this.Panel2.Height - 110);
                factoryDataCtrl.Size = new Size(this.Panel2.Width - 2 * lblX, 100);
                factoryDataCtrl.Visible = true;
                factoryDataCtrl.TabChanged += new Factory2D.Control.FactoryDataCtrl.onTabChanged(factoryDataCtrl_TabChanged);
                Panel2.Controls.Add(factoryDataCtrl);
            }

            // btValidate
            btValidate = new Button();
            btValidate.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            btValidate.Click += new EventHandler(onValidate);
            btValidate.Name = "ValidateButton";
            btValidate.Text = Properties.Resource.STR_VALIDATE;
            btValidate.Location = new System.Drawing.Point(this.Panel2.Width - 80, this.Panel2.Height - 50);
            btValidate.Size = new Size(70, 20);
            btValidate.Visible = _buttonValidateVisible;
            Panel2.Controls.Add(btValidate);

            // btClose
            btClose = new Button();
            btClose.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            btClose.Name = "CloseButton";
            btClose.Text = Properties.Resource.STR_CLOSE;
            btClose.Location = new System.Drawing.Point(this.Panel2.Width - 80, this.Panel2.Height - 25);
            btClose.Size = new Size(70, 20);
            btClose.Visible = _buttonCloseVisible;
            btClose.Click += new EventHandler(onClose);
            Panel2.Controls.Add(btClose);

            //  force to update parameter stack, once control have been recreated
            SetParametersDirty();
        }


        /// <summary>
        /// gets outer dimensions of a case
        /// </summary>
        /// <param name="length">outer length</param>
        /// <param name="width">outer width</param>
        /// <param name="height">outer height</param>
        /// <returns>true if component supports palletization</returns> 
        public bool GetDimensions(ref double length, ref double width, ref double height)
        {
            Component component = Component;
            if (component == null)  return false;
            return component.GetDimensions(CurrentParameterStack, ref length, ref width, ref height);
        }

        public bool GetFlatDimensions(ref double length, ref double width, ref double height)
        {
            Component component = Component;
            if (component == null) return false;
            return component.GetFlatDimensions(CurrentParameterStack, ref length, ref width, ref height);
        }

        public bool AllowPalletization
        {
            get
            {
                double length = 0.0, width = 0.0, height = 0.0;
                Component component = Component;
                if (component == null) return false;
                return component.GetDimensions(CurrentParameterStack, ref length, ref width, ref height);
            }
        }

        public bool AllowFlatPalletization
        {
            get
            { 
                double length = 0.0, width = 0.0, height = 0.0;
                Component component = Component;
                if (component == null) return false;
                return component.GetFlatDimensions(CurrentParameterStack, ref length, ref width, ref height);                
            }
        }

        private void nud_GotFocus(object sender, EventArgs e)
        {
            NumericUpDown nud = sender as NumericUpDown;
            nud.Select(0, nud.ToString().Length);
        }

        private void factoryDataCtrl_TabChanged(int currentIndex)
        {
            _computeBbox = true;
            Panel1.Invalidate();
        }

        /// <summary>
        /// Save control values in parameter stack
        /// </summary>
        /// <param name="stack">Parameter stack passed as ref</param>
        private bool SaveControlValues(ref Pic.Plugin.ParameterStack stack)
        {
            if (null == stack) return false;
            // load some parameters from majorations
            if (null != _profileLoader)
            {
                foreach (Parameter param in stack.ParameterList)
                {
                    if (_profileLoader.HasParameter(param))
                        stack.SetDoubleParameter(param.Name, _profileLoader.GetParameterDValue(param.Name));
                }
            }
            // load other parameters
            foreach (Control ctrl in Panel2.Controls)
            {
                if (ctrl.GetType() == typeof(NumericUpDown))
                {
                    NumericUpDown nud = ctrl as NumericUpDown;
                    if (nud.Name.Equals("nudThickness"))
                    {
                        _thickness = Convert.ToDouble(nud.Value);
                        if (stack.HasParameter("ep1"))  stack.SetDoubleParameter("ep1", _thickness);
                        if (stack.HasParameter("th1"))  stack.SetDoubleParameter("th1", _thickness);
                    }
                    else
                        foreach (Parameter param in stack.ParameterList)
                        {
                            if (nud.Name.Equals("nud" + param.Name))
                            {
                                if (param.GetType() == typeof(ParameterDouble))
                                {
                                    ParameterDouble paramDouble = param as ParameterDouble;
                                    try {   paramDouble.Value = Convert.ToDouble(nud.Value);    }
                                    catch (Exception ex)
                                    {   _log.Error(ex.ToString());  }
                                }
                                else if (param.GetType() == typeof(ParameterInt))
                                {
                                    ParameterInt paramInt = param as ParameterInt;
                                    try { paramInt.Value = Convert.ToInt32(nud.Value); }
                                    catch (Exception /*ex*/) { }
                                }
                            }
                        }
                }
                else if (ctrl.GetType() == typeof(CheckBox))
                {
                    CheckBox chkb = ctrl as CheckBox;
                    foreach (Parameter param in stack.ParameterList)
                    {
                        ParameterBool paramBool = param as ParameterBool;
                        if (null != paramBool && chkb.Name.Equals("chkb" + param.Name))
                            paramBool.Value = chkb.Checked;
                    }
                }
                else if (ctrl.GetType() == typeof(ComboBox))
                {
                    ComboBox cb = ctrl as ComboBox;
                    foreach (Parameter param in stack.ParameterList)
                    {
                        ParameterMulti paramMulti = param as ParameterMulti;
                        if (null != paramMulti && cb.Name.Equals("cb" + param.Name))
                            paramMulti.Value = cb.SelectedIndex;
                    }
                }
            }
            return true;
        }

        private void ClearParameterStack()
        {
            _paramStackCurrent = null;
            SetParametersDirty();
            Panel2.Controls.Clear();
        }
        /// <summary>
        /// Current parameter stack
        /// </summary>
        public ParameterStack CurrentParameterStack
        {
            get
            {
                if (_dirtyParameters || null == _paramStackCurrent)
                {
                    SaveControlValues(ref _paramStackCurrent);
                    _paramStackCurrent = Component.BuildParameterStack(_paramStackCurrent);
                    HasDependancies = Component.GetDependancies(_paramStackCurrent).Count > 0;
                    _dirtyParameters = false;
                }
                System.Diagnostics.Debug.Assert(null != _paramStackCurrent, "Current parameter stack shall not be null");
                return _paramStackCurrent;            
            }
        }

        /// <summary>
        /// this method will force the property CurrentParameterStack
        /// - to save current parameter values
        /// - rebuild parameter stack by component
        /// </summary>
        private void SetParametersDirty()
        {
            _dirtyParameters = true;
            _computeBbox = true;
        }

        public void SetParameterValue(string paramName, double paramValue)
        {
            if (null == _paramStackCurrent)
               _paramStackCurrent = Component.BuildParameterStack(_paramStackCurrent);
            _paramStackCurrent.SetDoubleParameter(paramName, paramValue);

            _picGraphics.DrawingBox = BoundingBox;
            FitView();
        }
        #endregion

        #region Dependancies
        /// <summary>
        /// Allows checking dependancy status :
        /// - true : component has some dependancies,
        /// - false : component has no dependancy with the current parameter stack
        /// Setting this property also triggers event DependancyStatusChanged
        /// </summary>
        public bool HasDependancies
        {
            get { return _hasDependancies; }
            set
            {
                if (value != _hasDependancies && null != DependancyStatusChanged)
                    DependancyStatusChanged(value);
                _hasDependancies = value;
            }        
        }
        /// <summary>
        /// Build a list of component dependancie GUIDs
        /// </summary>
        public List<Guid> Dependancies
        {
            get {  return _component.GetDependancies(CurrentParameterStack);  }
        }
        #endregion

        #region Event handlers
        /// <summary>
        /// Handle control value modifications
        /// </summary>
        /// <param name="o">Sender</param>
        /// <param name="e">Event arguments</param>
        protected void ParameterChanged(object o, EventArgs e)
        {
            try
            {
               SetParametersDirty();

                NumericUpDown nud = o as NumericUpDown;
                if (null != nud)
                    nud.Increment = (nud.Value < 10.0m) ? 0.5m : 1.0m;

                _picGraphics.DrawingBox = BoundingBox;
                FitView();
            }
            catch (Exception /*ex*/)
            {
            }
        }

        protected void ParameterChanged_WithRecreate(object o, EventArgs e)
        {
            try
            {
                SetParametersDirty();

                NumericUpDown nud = o as NumericUpDown;
                if (null != nud)
                    nud.Increment = (nud.Value < 10.0m) ? 0.5m : 1.0m;

                // recreate plugin controls
                CreatePluginControls();

                _picGraphics.DrawingBox = BoundingBox;
                FitView();
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }

        protected void nud_LostFocus(object sender, EventArgs e)
        {
            try
            {
                FitView();
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }

        void btEditMajorations_Click(object sender, EventArgs e)
        {
            try
            {
                if (null != _profileLoader)
                    _profileLoader.EditMajorations();
                SetParametersDirty();
                Panel1.Invalidate();                
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void OnProfileSelected(object o, EventArgs e)
        {
            try
            {
                if (null != _profileLoader)
                    _profileLoader.Selected = _comboProfile.SelectedItem as Profile;
                SetParametersDirty();
                FitView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        protected void onClose(object o, EventArgs e)
        {
            // send event so that container is notified that the user has pressed Cancel
            if (CloseClicked != null)
                CloseClicked(this, e);
        }

        protected void onValidate(object o, EventArgs e)
        { 
            // send event so that container is notified that the user has pressed Validate
            if (null != ValidateClicked)
                ValidateClicked(this, e);
        }
        #endregion

        #region Nud enter event handler
        private void nud_MouseEnter(object sender, EventArgs e)
        {
            NumericUpDown nudControl = sender as NumericUpDown;
            if (null != nudControl && null != timer)
                AnimateParameterName( nudControl.Name.Substring(3) );
        }

        public void AnimateParameterName(string parameterName)
        {
            _parameterName = parameterName;
            // copies parameter stack
            tempParameterStack = CurrentParameterStack.Clone();
            // retrieve initial value for parameter
            _parameterInitialValue = tempParameterStack.GetDoubleParameterValue(_parameterName);
            // start timer
            _timerStep = 0;
            timer.Start();
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            ++_timerStep;
            if (_timerStep < _noStepsTimer)
            {
                double parameterValue = 0;
                int iStep = _timerStep < _noStepsTimer / 2 ? _timerStep : _noStepsTimer - 1 - _timerStep;
                if (_parameterInitialValue > 10)
                    parameterValue = _parameterInitialValue + iStep * 0.1 * _parameterInitialValue;
                else if (_parameterInitialValue <= 10)
                    parameterValue = _parameterInitialValue + iStep * 2;

                tempParameterStack.SetDoubleParameter(_parameterName, parameterValue);
                _computeBbox = false;
                Panel1.Invalidate();
            }
            else
            {   // reset original parameter stack + stop timer
                tempParameterStack = null;
                timer.Stop();
                // redraw a last timer
                Panel1.Invalidate();
            }
        }
        #endregion

        #region Parameter translation
        public ILocalizer Localizer
        {
            get { return _localizer; }
            set { _localizer = value; }
        }
        private string Translate(string term)
        {
            if (null == _localizer)
                return term;
            else
                return _localizer.GetTranslation(term);
        }
        private string[] Translate(string[] terms)
        {
            if (null == _localizer)
                return terms;
            else
            {
                List<string> sArray = new List<string>();
                foreach (string s in terms)
                    sArray.Add(Translate(s));
                return sArray.ToArray();
            }
        }
        #endregion
    }

    #region Quality loader
    public class Profile
    {
        #region Data members
        private string _name;
        #endregion
        #region Constructor
        public Profile(string name)
        {
            _name = name;
        }
        #endregion
        #region Object overrides
        public override string ToString()
        {
            return _name;
        }
        #endregion
    }

    public abstract class ProfileLoader
    {
        #region Constructor
        public ProfileLoader()
        {
            Reset();
        }
        #endregion

        #region Public methods
        public abstract double Thickness { get; }
        public bool HasParameter(Pic.Plugin.Parameter param)
        {
            if (null == _majorationList)
                _majorationList = LoadMajorationList();
            return _majorationList.ContainsKey(param.Name);
        }

        public void GetParameterValue(ref Pic.Plugin.Parameter param)
        {
            if (null == _majorationList)
                _majorationList = LoadMajorationList();
            ParameterDouble parameterDouble = param as ParameterDouble;
            if (null != parameterDouble)
                parameterDouble.Value = _majorationList[param.Name];
        }

        public double GetParameterDValue(string name)
        {
            if (null == _majorationList)
                _majorationList = LoadMajorationList();
            if (_majorationList.ContainsKey(name))
                return _majorationList[name];
            else
                throw new Exception(string.Format("ProfileLoader : No majoration with name = {0}!", name));
        }
        #endregion

        #region Abstract method
        protected abstract Profile[] LoadProfiles();
        protected abstract Dictionary<string, double> LoadMajorationList();
        public abstract void EditMajorations();
        public abstract void BuildCardboardProfile();
        #endregion

        #region Public properties
        public Profile Selected
        {
            get { return _selectedProfile; }
            set
            {
                _selectedProfile = value;
                _majorationList = null;
            }
        }
        public Profile[] Profiles
        {
            get
            {
                if (null == _profiles)
                    _profiles = LoadProfiles();
                return _profiles;
            }
        }
        #endregion

        #region Private methods
        private void Reset()
        {
            _selectedProfile = null;
            _majorationList = null;
        }
        #endregion

        #region Notification mechanism
        public delegate void MajorationModifiedHandler(object sender);
        public event MajorationModifiedHandler MajorationModified;

        public virtual void NotifyModifications()
        {
            BuildCardboardProfile();
            if (null != MajorationModified)
                MajorationModified(this);
        }
        #endregion

        #region Data members
        // list of profiles
        protected Profile[] _profiles;
        // selected profile
        protected Profile _selectedProfile;
        protected Dictionary<string, double> _majorationList;
        // logging
        protected static readonly ILog _log = LogManager.GetLogger(typeof(PluginViewCtrl));
        #endregion
    }
    #endregion
}
