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
        public class PicCotationVertical : PicCotationDistance
        {
            #region Protected constructor
            protected PicCotationVertical(uint id, Vector2D pt0, Vector2D pt1, double offset, short noDecimals)
                : base(id, pt0, pt1, offset, noDecimals)
            { 
            }
            #endregion

            #region Public creation method
            public static new PicCotation CreateNewCotation(uint id, Vector2D pt0, Vector2D pt1, double offset, short noDecimals)
            {
                return new PicCotationVertical(id, pt0, pt1, offset, noDecimals);
            }
            #endregion

            #region PicCotationDistanceOverrides
            /// <summary>
            /// get offset points used to draw cotation
            /// </summary>
            /// <param name="pt2">out : offset point for _pt0</param>
            /// <param name="pt3">out : offset point for _pt1</param>
            protected override void GetOffsetPoints(out Vector2D pt2, out Vector2D pt3)
            { 
                double delta = -_globalCotationProperties._hrap;
                pt2 = new Vector2D(_pt0.X - _offset + Math.Sign(_offset) * delta, _pt0.Y);
                pt3 = new Vector2D(_pt0.X - _offset + Math.Sign(pt2.X - _pt0.X) * Math.Sign(pt2.X - _pt1.X) * Math.Sign(_offset) * delta, _pt1.Y);
            }

            protected override void DrawArrowLine(PicGraphics graphics, Vector2D pt2, Vector2D pt3, double textWidth, double textHeight, out Vector2D ptText, out bool middle)
            {
                double length = (pt3 - pt2).GetLength();
                middle = length > textHeight * _globalCotationProperties._lengthCoef;

                double textX = (TextDirection == 270.0f) ? textHeight : textWidth;
                double textY = (TextDirection == 270.0f) ? textWidth : textHeight;

                if (middle)
                {
                    ptText = 0.5 * (pt2 + pt3);
                    graphics.DrawLine(LineType, pt2, pt2 + (pt3 - pt2) * 0.5 * (length - textY) / length);
                    graphics.DrawLine(LineType, pt2 + (pt3 - pt2) * 0.5 * (length + textY) / length, pt3);
                }
                else
                {
                    // text is on top
                    graphics.DrawLine(LineType, pt2, pt2 + (pt3 - pt2) * _globalCotationProperties._lengthCoef);
                    ptText = pt2 + (pt3 - pt2) * ((_globalCotationProperties._lengthCoef * length + 0.5 * textX) / length);
                }
            }

            public override float TextDirection
            { get { return 270.0f; } }
            #endregion

            #region PicCotation override
            public override double Value()
            {
                return Math.Abs(_pt1.Y - _pt0.Y);
            }
            #endregion

            #region PicTypedDrawable overrides
            /// <returns>An entity to be saved in a new factory</returns>
            public override Pic.Factory2D.PicEntity Clone(IEntityContainer factory)
            {
                return new PicCotationVertical(factory.GetNewEntityId(), new Vector2D(_pt0), new Vector2D(_pt1), _offset, 1);
            }
            /// <returns>A value of enum eCode</returns>
            protected override Pic.Factory2D.PicEntity.eCode GetCode()
            {
                return PicEntity.eCode.PE_COTATIONVERTICAL;
            }
            #endregion

            #region System.Object overrides
            /// <returns>A string representation of this object.</returns>
            public override string ToString()
            {
                return string.Format("PicCotationVertical id = {0}, ext0={1}, ext1{2}, offset={3}\n"
                    , this.Id, _pt0, _pt1, _offset);
            }
            #endregion
        }
    }
}
