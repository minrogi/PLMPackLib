#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
#endregion

namespace Pic.Factory2D.Control
{
    public partial class DxfLineTypeConvertionCtrl : UserControl
    {
        #region Constructor
        public DxfLineTypeConvertionCtrl()
        {
            InitializeComponent();
        }
        #endregion

        #region Paint event handlers
        public void DxfLineTypePaint(object sender, PaintEventArgs e)
        {
            System.Drawing.Bitmap bmp = sender as System.Drawing.Bitmap;
            Size sz = bmp.Size;
            e.Graphics.DrawLine(new Pen(Color.Red), new Point(0, sz.Height / 2), new Point(sz.Width / 2, sz.Height / 2));
        }
        #endregion

        #region Public properties
        public Dictionary<netDxf.Tables.LineType, Pic.Factory2D.PicGraphics.LT> LineTypes
        {
            set
            {
                _lineTypes = value;

                // create controls
                // ...
            }
            get
            {
                // load control values
                // ...
                return _lineTypes;
            }
        }
        #endregion

        #region Private data members
        private Dictionary<netDxf.Tables.LineType, Pic.Factory2D.PicGraphics.LT> _lineTypes;
        #endregion
    }
}
