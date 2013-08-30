#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Pic.Factory2D;
#endregion

namespace Pic.Plugin.ViewCtrl
{
    class PicGraphicsCtrlBased : PicGraphicsGdiPlus
    {
        #region Private fields
        Size _size;
        #endregion

        #region Public constructor
        public PicGraphicsCtrlBased(Size size, Graphics graph)
        {
            _size = size;
            _gdiGraphics = graph;
        }
        #endregion

        #region Public properties
        public Graphics GdiGraphics
        {
            set { _gdiGraphics = value; }
        }
        public Size Size
        {
            get { return _size; }
            set
            {
                _size = value;
                // force box recomputation
                if (_box.IsValid)
                {
                    Pic.Factory2D.Box2D box = _box;
                    DrawingBox = box;
                }
            }
        }

        #endregion

        #region Public PicGraphicsGdiPlus overrides
        public override Size GetSize()
        {
            return _size;
        }
        #endregion
    }
}
