#region Using directive
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Pic.Factory2D;
using DesLib4NET;
using Dxflib4NET;
#endregion

namespace PicParam
{
    public partial class FormBrowseFile : Form
    {
        #region Constructor
        public FormBrowseFile(string filePath)
        {
            InitializeComponent();

            _filePath = filePath;
            this.Load += new EventHandler(FormBrowseFile_Load);
        }
        #endregion

        #region Loading
        private void FormBrowseFile_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;
            LoadDrawing(_filePath);
        }
        #endregion

        #region File loading method
        private bool LoadDrawing(string filePath)
        {
            try
            {
                // show factory viewer control
                _factoryViewer.Visible = true;
                // get factory reference
                PicFactory factory = _factoryViewer.Factory;
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
                // fit view to loaded entities
                _factoryViewer.FitView();
            }
            catch (Exception /*ex*/)
            {
            }
            return true;
        }
        #endregion

        #region Data members
        private string _filePath = string.Empty;
        #endregion
    }
}
