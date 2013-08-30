#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
#endregion

namespace Pic.Factory2D
{
    public class PicGraphicsImage : PicGraphicsGdiPlus
    {
        #region Private fields
        private Bitmap _bitmap;
        #endregion

        #region Public constructors
        public PicGraphicsImage()
        {
        }
        public PicGraphicsImage(Bitmap bitmap)
        {
            _bitmap = bitmap;
            _gdiGraphics = Graphics.FromImage(_bitmap);
        }
        public PicGraphicsImage(Size size, Box2D box)
        {
            ImageSize = size;
            DrawingBox = box;
            
        }
        #endregion

        #region Public methods
        public void SaveAs(string filename)
        {
            ImageFormat format = ImageFormat.Jpeg;
            if (null == _bitmap)
                throw new Exception("No valid bitmap : size must be set before using method SaveAs.");
            _bitmap.Save(filename, format);
        }
        public byte[] GetBuffer()
        {
            ImageFormat format = ImageFormat.Jpeg;
            MemoryStream ms = new MemoryStream();
            _bitmap.Save(ms, ImageFormat.Jpeg);
            return ms.GetBuffer();
        }
        #endregion

        #region Public properties
        public Bitmap Bitmap
        {
            get { return _bitmap; }
        }
        #endregion

        #region Public PicGraphicsGdiPlus overrides
        public override Size GetSize()
        {
            return _bitmap.Size;
		}
		#endregion

		#region Public properties
		public Size ImageSize
        {
            get { return _bitmap.Size; }
            set
            {
                _bitmap = new Bitmap(value.Width, value.Height);
                _gdiGraphics = Graphics.FromImage(_bitmap);
            }
        }
        #endregion
    }
}
