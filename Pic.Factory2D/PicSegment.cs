#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using Sharp3D.Math.Core;
using Sharp3D.Math.Geometry2D;
#endregion

namespace Pic
{
    namespace Factory2D
    {
        public class PicSegment : PicTypedDrawable 
        {
            #region Private fields
            Vector2D _pt0, _pt1;
            #endregion

            #region Protected Constructors
            protected PicSegment(uint id, PicGraphics.LT lType)
                : base(id, lType)
            {
            }
            #endregion

            #region Pic.Factory.PicEntity Overrides
            protected override eCode GetCode()
            {
                return PicEntity.eCode.PE_SEGMENT;
            }
            public override PicEntity Clone(IEntityContainer factory)
            {
                PicSegment segment = new PicSegment(factory.GetNewEntityId(), LineType);
                segment._pt0 = this._pt0;
                segment._pt1 = this._pt1;
                return segment;
            }
            #endregion

            #region Creation methods
            public static PicSegment CreateNewSegment(uint id, PicGraphics.LT lType, Vector2D pt0, Vector2D pt1)
            {
                PicSegment segment = new PicSegment(id, lType);
                segment._pt0 = pt0;
                segment._pt1 = pt1;
                return segment;
            }
			#endregion

            #region PicTypedDrawable overrides
            protected override bool Evaluate()
			{
				_box.Reset();
				_box.Extend(_pt0);
				_box.Extend(_pt1);
				return true;
			}
			protected override void DrawSpecific(PicGraphics graphics)
			{
				graphics.DrawLine(LineType, _pt0, _pt1);
			}
            protected override void DrawSpecific(PicGraphics graphics, Transform2D transform)
            {
                graphics.DrawLine(LineType, transform.transform(_pt0), transform.transform(_pt1));
            }
            public override void Transform(Transform2D transform)
            {
                _pt0 = transform.transform(_pt0);
                _pt1 = transform.transform(_pt1);
                SetModified();
            }

            public override double Length
            { get { return (_pt1 - _pt0).GetLength(); } }

            public override Box2D ComputeBox(Transform2D transform)
            {
                Box2D box = Box2D.Initial;
                box.Extend(transform.transform(_pt0));
                box.Extend(transform.transform(_pt1));
                return box;
            }

            public override Segment[] Segments
            {
                get
                {
                    Segment[] segments = null;
                    if (LineType == PicGraphics.LT.LT_CUT)
                    {
                        segments = new Segment[1];
                        segments[0] = new Segment(_pt0, _pt1);
                    }
                    else
                        segments = new Segment[0];
                    return segments;
                }
            }
            #endregion

            #region Public properties
            public Vector2D Pt0
            {
                get { return _pt0; }
                set { _pt0 = value; }
            }
            public Vector2D Pt1
            {
                get { return _pt1; }
                set { _pt1 = value; }
            }
            #endregion

            #region System.Object Overrides
            public override string ToString()
            {
                return string.Format("PicSegment id = {0}, ext0 = {1}, ext1 = {2}\n"
                    , this.Id, _pt0, _pt1);
            }
            #endregion
        }
    }
}
