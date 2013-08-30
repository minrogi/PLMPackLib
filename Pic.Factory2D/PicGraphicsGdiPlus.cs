﻿#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Sharp3D.Math.Core;
#endregion

namespace Pic.Factory2D
{
    public abstract class PicGraphicsGdiPlus : PicGraphics
    {
        #region Protected and private fields
        protected Graphics _gdiGraphics;
        private Color _colorBackground;
        private System.Collections.Generic.Dictionary<PicGraphics.LT, Pen> _ltToPen = new Dictionary<LT, Pen>();
        #endregion

        #region Protected constructors
        protected PicGraphicsGdiPlus()
        {
            _colorBackground = Color.White;
        }
        #endregion

        #region Pic.Factory2D.PicGraphics overrides
        public override void Initialize()
        {
            if (null == _gdiGraphics)
                throw new Exception("PicGraphics size must be set before atempting to draw.");
            InitializeLineTypeToPenDictionnary();
            _gdiGraphics.Clear(BackgroundColor);
            PicCotation._globalCotationProperties._arrowLength = 5.0;


        }
        private void InitializeLineTypeToPenDictionnary()
        {
            _ltToPen.Clear();

            _ltToPen.Add(LT.LT_CUT, new Pen(System.Drawing.ColorTranslator.FromWin32(255), 1.0f));
            _ltToPen.Add(LT.LT_PERFOCREASING, new Pen(System.Drawing.ColorTranslator.FromWin32(16711680), 1.0f));
            _ltToPen.Add(LT.LT_CONSTRUCTION, new Pen(System.Drawing.ColorTranslator.FromWin32(7798903), 1.0f));
            _ltToPen.Add(LT.LT_PERFO, new Pen(System.Drawing.ColorTranslator.FromWin32(255), 1.0f));
            _ltToPen.Add(LT.LT_HALFCUT, new Pen(System.Drawing.ColorTranslator.FromWin32(16776960), 1.0f));
            _ltToPen.Add(LT.LT_CREASING, new Pen(System.Drawing.ColorTranslator.FromWin32(16711680), 1.0f));
            _ltToPen.Add(LT.LT_AXIS, new Pen(System.Drawing.ColorTranslator.FromWin32(16711680), 1.0f));
            _ltToPen.Add(LT.LT_COTATION, new Pen(Color.Green/*System.Drawing.ColorTranslator.FromWin32(8453888)*/, 1.0f));
            _ltToPen.Add(LT.LT_GRID, new Pen(System.Drawing.ColorTranslator.FromWin32(8388608), 1.0f));
        }
        /// <summary>
        /// Draw a point (actually a small cross) using defined line type
        /// </summary>
        /// <param name="lineType"></param>
        /// <param name="pt"></param>
        public override void DrawPoint(PicGraphics.LT lineType, Vector2D pt)
        {
        }
        public override void DrawLine(PicGraphics.LT lineType, Vector2D pointBeg, Vector2D pointEnd)
        {
            _gdiGraphics.DrawLine(ToPen(lineType), ToPointF(pointBeg), ToPointF(pointEnd));
        }
        public override void DrawArc(PicGraphics.LT lineType, Vector2D ptCenter, double radius, double angle0, double angle1)
		{
            if (radius > 0.0)
            _gdiGraphics.DrawArc(ToPen(lineType)
                , X(ptCenter.X) - DX(radius)
                , Y(ptCenter.Y) - DY(radius)
                , (float)(2.0*DX(radius))	// width
                , (float)(2.0*DY(radius))	// height
                , -(float)angle0			// start angle
                , -(float)(angle1-angle0));	// sweep angle
		}

        public override void DrawText(string text, TextType font, Vector2D pt, HAlignment hAlignment, VAlignment vAlignment)
        {
            // string format
            StringFormat sf = new StringFormat();
            switch (hAlignment)
            {
                case HAlignment.VA_RIGHT:
                    sf.LineAlignment = StringAlignment.Far;
                    break;
                case HAlignment.VA_CENTER:
                    sf.LineAlignment = StringAlignment.Center;
                    break;
                case HAlignment.VA_LEFT:
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                default:
                    throw new Exception("Unknown horizontal alignment");
            }
            switch (vAlignment)
            { 
                case VAlignment.VA_BOTTOM:
                    sf.Alignment = StringAlignment.Far;
                    break;
                case VAlignment.VA_MIDDLE:
                    sf.Alignment = StringAlignment.Center;
                    break;
                case VAlignment.VA_TOP:
                    sf.Alignment = StringAlignment.Near;
                    break;
                default:
                    throw new Exception("Unknown vertical alignment");
            }
            // font & brush
            Brush tb;
            switch (font)
            { 
                case TextType.FT_COTATION:
                    tb = new SolidBrush(Color.Green);
                    break;
                default:
                    throw new Exception("Unknown text type");
            }
            
            // draw text
            _gdiGraphics.DrawString(text, ToFont(font), tb, ToPointF(pt), sf);
         }

        public override void GetTextSize(string text, TextType font, out double width, out double height)
        {
            Size preferredSize = _gdiGraphics.MeasureString(text, ToFont(font)).ToSize();
            width = DX_inv(preferredSize.Width);
            height = DY_inv(preferredSize.Height);
        }

        public override void Finish()
        {
        }

        public override void ShowMessage(string message)
        {
            // draw text
            StringFormat sf = new StringFormat();
            Brush tb = new SolidBrush(Color.Red);
            _gdiGraphics.DrawString(message, new Font("Arial", 8), tb, new PointF(100.0f, 100.0f));
        }
        #endregion

        #region Helpers
        public PointF ToPointF(Vector2D vec)
        {
            return new PointF(X(vec.X), Y(vec.Y));
        }
        public Pen ToPen(PicGraphics.LT lineType)
        {
            return _ltToPen[lineType];
        }

        public Font ToFont(PicGraphics.TextType textType)
        {
            switch (textType)
            {
                case TextType.FT_COTATION:  return new Font("Arial", 8);
                default: throw new Exception("Unexpected text type");
            }
        }

        public abstract Size GetSize();

        // coordinates transformation
        protected float X(double x)
        {
            return (float)((x - _box.XMin) * GetSize().Width / _box.Width);
        }

        protected float Y(double y)
        {
            return (float)((_box.YMax - y) * GetSize().Height / _box.Height);
        }

        protected float DX(double dx)
        {
            return (float)(dx * GetSize().Width / (_box.Width));
        }

        protected float DY(double dy)
        {
            return (float)(dy * GetSize().Height / (_box.Height));
        }

        public override double DX_inv(double dX)
        {
            return dX * _box.Width / GetSize().Width;
        }

        public override double DY_inv(double dY)
        {
            return dY * _box.Height / GetSize().Height;
        }

        protected Vector2D ConvertMouseCoordinates(System.Drawing.Point pt)
        {
            return new Vector2D(
                _box.XMin + pt.X * (_box.Width) / GetSize().Width
                , _box.YMax - pt.Y * (_box.Height) / GetSize().Height
                );
        }

        // set world coordinate drawing box
        public override Box2D DrawingBox
        {
			set
            {
                double width = value.Width;
                double height = value.Height;
                double xmin = 0.0, ymin = 0.0, xmax = 0.0, ymax = 0.0;

                if ((width / GetSize().Width) > (height / GetSize().Height))
                {	// width is leading
                    xmin = value.XMin;
                    xmax = value.XMax;
                    height = (width * GetSize().Height) / GetSize().Width;
                    ymin = (value.YMin + value.YMax) * 0.5 - height * 0.5;
                    ymax = (value.YMin + value.YMax) * 0.5 + height * 0.5;
                }
                else
                {	// height is leading
                    ymin = value.YMin;
                    ymax = value.YMax;
                    width = (height * GetSize().Width) / GetSize().Height;
                    xmin = (value.XMin + value.XMax) * 0.5 - width * 0.5;
                    xmax = (value.XMin + value.XMax) * 0.5 + width * 0.5;
                }

                _box = new Box2D(xmin, ymin, xmax, ymax);
            }

            get { return _box; }
        }
        #endregion

        #region Object overrides
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

        #region Public properties
        Color BackgroundColor
        {
            get { return _colorBackground; }
            set { _colorBackground = value; }
        }
        #endregion
    }
}