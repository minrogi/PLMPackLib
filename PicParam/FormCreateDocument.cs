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
using Pic.Plugin;
using Pic.DAL.SQLite;
using DesLib4NET;
using Pic.DAL;

using log4net;
#endregion

namespace PicParam
{
    public partial class FormCreateDocument : Form
    {
        #region Contructor
        public FormCreateDocument()
        {
            InitializeComponent();
        }
        #endregion

        #region Ui event handlers
        private void FormCreateDocument_Load(object sender, EventArgs e)
        {
            textBoxName.TextChanged += new EventHandler(textBox_TextChanged);
            textBoxDescription.TextChanged += new EventHandler(textBox_TextChanged);
            // file select control
            // supported formats are des3 / doc / docx / xls / stb / ai / jpg / ard (artios) / open-office
            fileSelectCtrl.FileNameChanged += new EventHandler(fileSelectCtrl_FileNameChanged);
            fileSelectCtrl.Filter = FFormatManager.Filters;
            // thumbnail select control
            thumbnailSelectCtrl.FileNameChanged += new EventHandler(thumbnailSelect_FileNameChanged);
            thumbnailSelectCtrl.Filter = "Image files (*.bmp;*.gif;*.jpg;*.png)|*.bmp;*.gif;*.jpg;*.png";
            // disable Ok button
            bnOk.Enabled = false;
            HideLoadingControls();
        }

        private void fileSelectCtrl_FileNameChanged(object sender, EventArgs e)
        { 
            HideLoadingControls();

            string filePath = fileSelectCtrl.FileName;
            _successfullyLoaded = false;

            if (System.IO.File.Exists(filePath))
            {
                string fileExt = System.IO.Path.GetExtension(filePath);
                fileExt = fileExt.Substring(1);

                if (string.Equals(fileExt, "dll", StringComparison.CurrentCultureIgnoreCase))
                    _successfullyLoaded = LoadComponent(filePath);
                else if (string.Equals(fileExt, "des", StringComparison.CurrentCultureIgnoreCase)
                    || string.Equals(fileExt, "dxf", StringComparison.CurrentCultureIgnoreCase))
                    _successfullyLoaded = LoadDrawing(filePath);
                else if (string.Equals(fileExt, "pdf", StringComparison.CurrentCultureIgnoreCase))
                    _successfullyLoaded = LoadPdf(filePath);
                else if (string.Equals(fileExt, "jpg", StringComparison.CurrentCultureIgnoreCase)
                    || string.Equals(fileExt, "jpeg", StringComparison.CurrentCultureIgnoreCase)
                    || string.Equals(fileExt, "png", StringComparison.CurrentCultureIgnoreCase)
                    || string.Equals(fileExt, "bmp", StringComparison.CurrentCultureIgnoreCase)
                    || string.Equals(fileExt, "gif", StringComparison.CurrentCultureIgnoreCase))
                    _successfullyLoaded = LoadImage(filePath);
                else
                {                    
                    // instead of preview, load image
                    _successfullyLoaded = LoadMiscellaneousFormat(filePath);
                }
            }
            enableDisableOkButton();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {   enableDisableOkButton();   }
        private void thumbnailSelect_FileNameChanged(object sender, EventArgs e)
        {   enableDisableOkButton();   }

        /// <summary>
        /// enable OK button
        /// </summary>
        private void enableDisableOkButton()
        { 
            bnOk.Enabled = textBoxName.TextLength != 0 
                && textBoxDescription.TextLength != 0
                && _successfullyLoaded
                && ((!chkThumbnail.Checked) || System.IO.File.Exists(thumbnailSelectCtrl.FileName));
        }

        private void HideLoadingControls()
        {
            componentLoaderControl.Visible = false;
            factoryViewer.Visible = false;
            webBrowser4PDF.Visible = false;
            pb4ImageFiles.Visible = false;
            chkDefaultParameters.Visible = false;
        }

        private bool LoadComponent(string filePath)
        {
            componentLoaderControl.Visible = true;
            chkDefaultParameters.Visible = true;
            // try and load plugin
            try
            {
                if (componentLoaderControl.LoadComponent(filePath))
                {
                    if (textBoxName.Text.Length == 0)
                        textBoxName.Text = componentLoaderControl.ComponentName;
                    if (textBoxDescription.Text.Length == 0)
                        textBoxDescription.Text = componentLoaderControl.ComponentDescription;                    
                }
                return true;
            }
            catch (System.Exception ex)
            {
                _log.Error(ex.ToString());
                return false;
            }
        }

        private bool LoadPdf(string filePath)
        {
            try
            {
                webBrowser4PDF.Visible = true;
                webBrowser4PDF.Url = new Uri(filePath);

                if (textBoxName.Text.Length == 0)
                    textBoxName.Text = System.IO.Path.GetFileNameWithoutExtension(filePath);
                if (textBoxDescription.Text.Length == 0)
                    textBoxDescription.Text = System.IO.Path.GetFileNameWithoutExtension(filePath);
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
            return true;
        }

        private bool LoadCF2(string filePath)
        {
            try
            {
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
            return true;
        }

        private bool LoadAI(string filePath)
        {
            try
            {
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
            return true;
        }

        private bool LoadImage(string filePath)
        {
            try
            {
                pb4ImageFiles.Visible = true;
                pb4ImageFiles.ImageLocation = filePath;

                if (textBoxName.Text.Length == 0)
                    textBoxName.Text = System.IO.Path.GetFileNameWithoutExtension(filePath);
                if (textBoxDescription.Text.Length == 0)
                    textBoxDescription.Text = System.IO.Path.GetFileNameWithoutExtension(filePath);
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
            return true;        
        }

        private bool LoadMiscellaneousFormat(string filePath)
        {
            try
            {
                FormatHandler fHandler = FFormatManager.GetFormatHandlerFromFilePath(filePath);
                if (null == fHandler)
                    return false;

                pb4ImageFiles.Visible = true;
                pb4ImageFiles.ImageLocation = fHandler.ThumbnailFile;

                if (textBoxName.Text.Length == 0)
                    textBoxName.Text = System.IO.Path.GetFileNameWithoutExtension(filePath);
                if (textBoxDescription.Text.Length == 0)
                    textBoxDescription.Text = System.IO.Path.GetFileNameWithoutExtension(filePath);
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
            return true;
        }

        private bool LoadDrawing(string filePath)
        {
            try
            {
                // show factory viewer control
                factoryViewer.Visible = true;
                // get factory reference
                PicFactory factory = factoryViewer.Factory;
                // clear factory
                factory.Clear();

                string fileExt = System.IO.Path.GetExtension(filePath).ToLower();
                if (string.Equals(".des", fileExt, StringComparison.CurrentCultureIgnoreCase))
                {
                    // load des file
                    DES_FileReader fileReader = new DES_FileReader();
                    PicLoaderDes picLoader = new PicLoaderDes(factory);
                    fileReader.ReadFile(filePath, picLoader);
                }
                else if (string.Equals(".dxf", fileExt, StringComparison.CurrentCultureIgnoreCase))
                {
                    // load dxf file
                    PicLoaderDxf picLoader = new PicLoaderDxf(factory);
                    picLoader.Load(filePath);
                    picLoader.FillFactory();
                }
                else if (string.Equals(".ai", fileExt, StringComparison.CurrentCultureIgnoreCase))
                { 
                    // load ai file
                    
                }
                // fit view to loaded entities
                factoryViewer.FitView();

                if (textBoxName.Text.Length == 0)
                    textBoxName.Text = System.IO.Path.GetFileNameWithoutExtension(filePath);
                if (textBoxDescription.Text.Length == 0)
                    textBoxDescription.Text = System.IO.Path.GetFileNameWithoutExtension(filePath);
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
            return true;
        }

        private void chbThumbnail_CheckedChanged(object sender, EventArgs e)
        {
            thumbnailSelectCtrl.Enabled = chkThumbnail.Checked;
        }
        #endregion

        #region Button OK
        private void bnOk_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = fileSelectCtrl.FileName;
                string fileExt = System.IO.Path.GetExtension(filePath).ToLower();
                fileExt = fileExt.Substring(1);
                if (!System.IO.File.Exists(filePath))
                    return;

                // get tree node for document insertion
                Pic.DAL.SQLite.PPDataContext db = new Pic.DAL.SQLite.PPDataContext();
                if (null == _nodeTag || _nodeTag.IsDocument)
                    throw new Exception("Invalid TreeNode tag");
                Pic.DAL.SQLite.TreeNode treeNode = Pic.DAL.SQLite.TreeNode.GetById(db, _nodeTag.TreeNode);
                if (string.Equals("dll", fileExt, StringComparison.CurrentCultureIgnoreCase))
                {
                    // make sure "Parametric Component" document type exist
                    if (null == Pic.DAL.SQLite.DocumentType.GetByName(db, "Parametric component"))
                        Pic.DAL.SQLite.DocumentType.CreateNew(db, "Parametric component", "Parametric component", "PicParam");

                    // create new document
                    Pic.DAL.SQLite.Component component = treeNode.InsertComponent(
                         db
                         , filePath
                         , componentLoaderControl.ComponentGuid
                         , DocumentName
                         , DocumentDescription
                         , ThumbnailPath);
                    // create associated majorations if any
                    if (componentLoaderControl.HasMajorations)
                        component.InsertNewMajorationSet(db, componentLoaderControl.Profile, componentLoaderControl.Majorations);
                    // create associated default param values
                    if (chkDefaultParameters.Checked)
                        component.InsertNewParamDefaultValues(db, componentLoaderControl.ParamDefaultValues);
                    // save document ID to be used later
                    // -> to retrieve document tree node
                    _documentId = component.DocumentID;
                    _openInsertedDocument = true;
                }
                else
                {
                    // get a format handler
                    FormatHandler fHandler = FFormatManager.GetFormatHandlerFromFileExt(fileExt);
                    if (null == fHandler)
                        throw new Pic.DAL.SQLite.ExceptionDAL(string.Format("No valid format handler from file extension: {0}", fileExt) );
                    // get document type
                    Pic.DAL.SQLite.DocumentType docType = Pic.DAL.SQLite.DocumentType.GetByName(db, fHandler.Name);
                    if (null == docType)
                        docType = Pic.DAL.SQLite.DocumentType.CreateNew(db, fHandler.Name, fHandler.Description, fHandler.Application);
                    // insert document in database
                    Pic.DAL.SQLite.Document document = treeNode.InsertDocument(
                        db
                        , filePath
                        , DocumentName
                        , DocumentDescription
                        , docType.Name
                        , ThumbnailPath);
                    // save document ID
                    _documentId = document.ID;
                    _openInsertedDocument = fHandler.OpenInPLMPackLib;
                }                
            }
            catch (Pic.DAL.SQLite.ExceptionDAL ex)
            {
                MessageBox.Show(
                    ex.Message
                    , Application.ProductName
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
                _log.Error(ex.ToString());
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
        #endregion

        #region Result properties
        public string DocumentName
        {
            get { return textBoxName.Text; }
        }

        public string DocumentDescription
        {
            get { return textBoxDescription.Text; }
        }

        public string FilePath
        {
            get { return fileSelectCtrl.FileName; }
        }

        public NodeTag TreeNode
        {
            set { _nodeTag = value; }
        }

        public int DocumentID
        {
            get { return _documentId; }
        }
        public bool OpenInsertedDocument
        {
            get { return _openInsertedDocument; }
        }
        private bool ThumbnailCallBack()
        {
            return false;
        }
        public string ThumbnailPath
        {
            get
            {
                // build file path
                string tempFilePath = System.IO.Path.GetTempFileName();
                string thumbFilePath = System.IO.Path.ChangeExtension(tempFilePath, "bmp");
                if (chkThumbnail.Checked)
                {
                    Bitmap thb = new Bitmap(thumbnailSelectCtrl.FileName);
                    Image.GetThumbnailImageAbort thumbCallback = new Image.GetThumbnailImageAbort(ThumbnailCallBack);
                    Image image = thb.GetThumbnailImage(_thumbnailWidth, _thumbnailWidth, thumbCallback, IntPtr.Zero);
                    image.Save(thumbFilePath);
                }
                else
                {
                    // get file extension
                    string filePath = fileSelectCtrl.FileName;
                    FormatHandler fHandler = FFormatManager.GetFormatHandlerFromFilePath(filePath);

                    if (fHandler is FormatDLL)
                    {
                        // load component
                        Pic.Plugin.Component component = null;
                        using (Pic.Plugin.ComponentLoader loader = new ComponentLoader())
                        {
                            loader.SearchMethod = new ComponentSearchMethodDB();
                            component = loader.LoadComponent(filePath);
                        }
                        if (null == component)
                            throw new System.Exception(string.Format("Failed to load file {0}", filePath));

                        // generate image
                        Image image;
                        using (Pic.Plugin.Tools pluginTools = new Pic.Plugin.Tools(component, new ComponentSearchMethodDB()))
                        {
                            pluginTools.ShowCotations = false;
                            pluginTools.GenerateImage(new Size(_thumbnailWidth, _thumbnailWidth), out image);
                            image.Save(thumbFilePath);                            
                        }
                    }
                    else if (fHandler is FormatDES || fHandler is FormatDXF)
                    {
                        // generate image
                        Pic.Factory2D.ThumbnailGenerator.GenerateImage(new Size(_thumbnailWidth, _thumbnailWidth), filePath, thumbFilePath);
                    }
                    else if (fHandler is FormatImage)
                    {
                        Image img = Image.FromFile(filePath);
                        Image thumbnail = img.GetThumbnailImage(_thumbnailWidth, _thumbnailWidth, null, IntPtr.Zero);
                        thumbnail.Save(thumbFilePath);
                    }
                    else
                    {
                        if (null != fHandler)
                            thumbFilePath = fHandler.ThumbnailFile;
                    }
                }
                return thumbFilePath;           
            }
        }
        #endregion

        #region Data members
        protected static readonly ILog _log = LogManager.GetLogger(typeof(FormCreateDocument));
        private NodeTag _nodeTag;
        private int _documentId;
        private bool _openInsertedDocument = false;
        private bool _successfullyLoaded;
        private const int _thumbnailWidth = 150;
        Dictionary<string, string> FileExt2Thumbnail = new Dictionary<string, string>();
        #endregion
    }
}