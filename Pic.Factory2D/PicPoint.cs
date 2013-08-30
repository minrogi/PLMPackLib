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
        public class PicPoint : PicTypedDrawable 
        {
            #region Private fields
            Vector2D _pt;
            #endregion

            #region Constructors
            protected PicPoint(uint id, PicGraphics.LT lType)
                : base(id, lType)
            {
            }
            #endregion

            #region Pic.Factory2D.PicEntity Overrides 
            protected override eCode GetCode()
            {
                return PicEntity.eCode.PE_POINT;
            }
            public override PicEntity Clone(IEntityContainer factory)
            {
                PicPoint point = new PicPoint(factory.GetNewEntityId(), LineType);
                point._pt = this._pt;
                return point;
            }
            #endregion

            #region Creation methods
            public static PicPoint CreateNewPoint(uint id, PicGraphics.LT lType, Vector2D pt)
            {
                PicPoint point = new PicPoint(id, lType);
                return point;
            }
            #endregion

            #region PicDrawable overrides
            protected override void DrawSpecific(PicGraphics graphics)
            {
                graphics.DrawPoint(LineType, _pt);
            }
            protected override void DrawSpecific(PicGraphics graphics, Transform2D transform)
			{
				graphics.DrawPoint(LineType, transform.transform(_pt));
			}
            public override void Transform(Transform2D transform)
            {
                _pt = transform.transform(_pt);
                SetModified();
            }
            public override double Length
            {
                get { return 0.0; }
            }
            public override Box2D  ComputeBox(Transform2D transform)
            {
                Box2D box = Box2D.Initial;
                box.Extend(transform.transform(_pt));
                return box;
            }
            public override Segment[] Segments
            {
                get { return new Segment[0]; }
            }

            #endregion

            #region Public properties
            public Vector2D Coord
            {
                get {   return _pt;     }
            }
            #endregion

            #region System.Object Overrides
            public override string ToString()
            {
                 return string.Format("Point id = {0}, coord = {1}\n", this.Id, _pt);
            }
            #endregion
        }
    }
}
