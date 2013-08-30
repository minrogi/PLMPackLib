#region Using directives
using System;
using System.Collections.Generic;
using System.Text;

using Sharp3D.Math.Core;
using Sharp3D.Math.Geometry2D;

using System.Runtime.InteropServices;
#endregion

namespace Pic.Factory2D
{
    /// <summary>
    /// Represents a cardboard format
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class PicCardboardFormat : PicTypedDrawable
    {
        #region Private fields
        private double _thickness;
        private Vector2D _ptOrigin, _dimensions;
        #endregion

        #region Constructor
        internal PicCardboardFormat(uint id)
            : base(id, PicGraphics.LT.LT_CUT)
        {
        }
        internal PicCardboardFormat(uint id, Vector2D position, Vector2D dimensions)
            : base(id, PicGraphics.LT.LT_CUT)
        {
            _ptOrigin = position;
            _dimensions = dimensions;
        }
        #endregion

        #region PicDrawable overrides
        public override double Length
        { get { return 2.0 * (_dimensions.X + _dimensions.Y); } }

        protected override bool Evaluate()
        {
            _box = ComputeBox(Transform2D.Identity);
            return true;
        }
        public override Box2D ComputeBox(Transform2D transform)
        {
            // instantiate bounding box
            Box2D box = Box2D.Initial;
            box.XMin = 0.0;
            box.YMin = 0.0;
            box.XMax = _dimensions.X;
            box.YMax = _dimensions.Y;
            return box;
        }

        protected override void DrawSpecific(PicGraphics graphics, Transform2D transform)
        {
            graphics.DrawLine(
                LineType
                , transform.transform(new Vector2D(0.0,0.0))
                , transform.transform(new Vector2D(_dimensions.X, 0.0))
                );
            graphics.DrawLine(
                LineType
                , transform.transform(new Vector2D(_dimensions.X, 0.0))
                , transform.transform(new Vector2D(_dimensions.X, _dimensions.Y))
                );
            graphics.DrawLine(
                LineType
                , transform.transform(new Vector2D(_dimensions.X, _dimensions.Y))
                , transform.transform(new Vector2D(0.0, _dimensions.Y))
                );
            graphics.DrawLine(
                LineType
                , transform.transform(new Vector2D(0.0, _dimensions.Y))
                , transform.transform(new Vector2D(0.0, 0.0))
                );
        }

        protected override void DrawSpecific(PicGraphics graphics)
        {
            DrawSpecific(graphics, Transform2D.Identity);
        }

        public override void Transform(Transform2D transform)
        {
        }

        public override Segment[] Segments
        {
            get
            {
                Segment[] segments = new Segment[0];
                return segments;
            }
        }
        #endregion

        #region PicEntity overrides
        protected override PicEntity.eCode GetCode()
        {
            return eCode.PE_CARDBOARDFORMAT;
        }
        public override PicEntity Clone(IEntityContainer factory)
        {
            PicCardboardFormat cbf = new PicCardboardFormat(factory.GetNewEntityId(), Vector2D.Zero, _dimensions);
            return cbf;            
        }
        #endregion

        #region Object overrides
        public override int GetHashCode()
        {
            return _dimensions.GetHashCode() ^ _ptOrigin.GetHashCode();
        }
        public override string ToString()
        {
            return string.Format("Format : {0} * {1},  Position : ({2},{3})", _dimensions.X, _dimensions.Y, _ptOrigin.X, _ptOrigin.Y);
        }
        #endregion

        #region Specific properties
        public double Width { get { return _dimensions.X; } }
        public double Height { get { return _dimensions.Y; } }
        public double Thickness { get { return _thickness; } }
        public Vector2D Position { get { return _ptOrigin; } }
        #endregion
    }
}
