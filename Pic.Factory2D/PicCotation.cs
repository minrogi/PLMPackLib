#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using Sharp3D.Math.Core;
#endregion

namespace Pic
{
    namespace Factory2D
    {
        public class PicGlobalCotationProperties
        {
            #region Constructor
            public PicGlobalCotationProperties()
            { 
                _hrap = 10.0;
                _arrowLength = 5.0F;
                _arrowHeadAngle = 30.0;
                _lengthCoef = 1.2;
            }
            #endregion

            #region Change notification
            public delegate void OnGlobalCotationPropertiesModified();
            public static event OnGlobalCotationPropertiesModified Modified;
            #endregion

            #region Public properties
            public static bool ShowShortCotationLines
            {
                get { return _showShortLines; }
                set
                {
                    _showShortLines = value;
                    if (null != Modified)
                        Modified();
                }
            }
            public bool ShowDeportedCotations
            {
                get { return _showDeportedCotations; }
                set
                {
                    _showDeportedCotations = value;
                    if (null != Modified)
                        Modified();
                }
            }
            #endregion


            #region Public fields
            public double _hrap;
            public double _arrowLength;
            public double _arrowHeadAngle;
            public double _lengthCoef;
            public bool _showDeportedCotations = true;
            static bool _showShortLines = true;
            #endregion
        }

        public abstract class PicCotation : PicTypedDrawable
        {
            #region Public enum
            public enum CotType
            {
                COT_DISTANCE
                , COT_HORIZONTAL
                , COT_VERTICAL
                , COT_RADIUS_INT
                , COT_RADIUS_EXT
            }
            #endregion

            #region Private fields
            private string _text;
            #endregion

            #region Global cotation properties
            static public PicGlobalCotationProperties _globalCotationProperties = new PicGlobalCotationProperties();
            #endregion

            #region Protected Constructors
            protected PicCotation(uint id)
                : base(id, PicGraphics.LT.LT_COTATION)
            {
            }
            #endregion

            #region Helpers
            protected void DrawArrowHead(Pic.Factory2D.PicGraphics graphics, Vector2D pt, double angle)
            { 
                DrawArrowHead( graphics, pt, new Vector2D( Math.Cos(angle * Math.PI / 180.0), Math.Sin(angle * Math.PI / 180.0) ) );
            }
            protected void DrawArrowHead(Pic.Factory2D.PicGraphics graphics, Vector2D pt, Vector2D director)
            {
                director.Normalize();
                Vector2D normal = new Vector2D(-director.Y, director.X);

                double angleRad = _globalCotationProperties._arrowHeadAngle * Math.PI / 180.0;
                double arrowLength = graphics.DX_inv(_globalCotationProperties._arrowLength);

                // lower part of arrow head
                graphics.DrawLine(LineType
                    , pt - arrowLength * (Math.Cos(angleRad) * director + Math.Sin(angleRad) * normal)
                    , pt);
                // upper part of arrow head
                graphics.DrawLine(LineType
                    , pt - arrowLength * (Math.Cos(angleRad) * director - Math.Sin(angleRad) * normal)
                    , pt);     
            }
            #endregion

            #region Public properties
            public abstract double Value();

            public string Text
            {
                get
                {
                    if (null != _text && _text.Length > 0)
                        return _text;
                    else
                    {
                        double value = Value();
                        if (value - Math.Round(value) < 0.1)
                            return string.Format("{0:0}", Value());
                        else
                            return string.Format("{0:0.0}", Value());
                    }
                }
                set { _text = value; }
            }
           #endregion
        }
    }
}
