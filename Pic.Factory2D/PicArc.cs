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
        public class PicArc : PicTypedDrawable 
        {
            #region Private fields
            Vector2D _center;
            double _radius, _angleBeg, _angleEnd;
            #endregion

            #region Protected Constructors
            protected PicArc(uint id, PicGraphics.LT lType)
                : base(id, lType)
            {
            }
            #endregion

            #region Pic.Factory.PicEntity Overrides
            protected override eCode GetCode()
            {
                return PicEntity.eCode.PE_ARC;
            }
            #endregion

            #region PicDrawable overrides
            public override Box2D ComputeBox(Transform2D transform)
            {
                // instantiate bounding box
                Box2D box = Box2D.Initial;

                // 6 points to be considered
                // arc extremities
                _box.Extend(transform.transform(PointAtAngle(AngleBegActual)));
                _box.Extend(transform.transform(PointAtAngle(AngleEndActual)));

                // horizontal and vertical tangent points
                double[] tab = new double[] { 0.0, 90.0, 180.0, 270.0 };
                foreach (double d in tab)
                    if (PointOnArc(d))
                        box.Extend(transform.transform(PointAtAngle(d)));
                return box;
            }

            public override Segment[] Segments
            {
                get
                {
                    double angleStep = 5.0;
                    int iNoStep = (int)((_angleEnd - _angleBeg) / angleStep);
                    if (iNoStep < 1) iNoStep = 1;
                    angleStep = (_angleEnd - _angleBeg) / iNoStep;

                    Sharp3D.Math.Geometry2D.Segment[] segs = null;
                    if (LineType == PicGraphics.LT.LT_CUT)
                    {
                        segs = new Sharp3D.Math.Geometry2D.Segment[iNoStep];
                        for (int i = 0; i < iNoStep; ++i)
                        {
                            Vector2D pt0 = PointAtAngle(_angleBeg + i * angleStep);
                            Vector2D pt1 = PointAtAngle(_angleBeg + (i + 1) * angleStep);
                            segs[i] = new Segment(pt0, pt1);
                        }
                    }
                    else
                        segs = new Segment[0];
                    return segs;
                }
            }
            #endregion

            #region Public Methods
            public static PicArc CreateNewArc(uint id, PicGraphics.LT lType, Vector2D center, double radius, double angleBeg, double angleEnd)
            {
                PicArc arc = new PicArc(id, lType);
                arc._center = center;
                arc._radius = radius;
                arc._angleBeg = angleBeg;
                arc._angleEnd = angleEnd;
                return arc;
            }
            public static PicArc CreateNewArc(uint id, PicGraphics.LT lType, Vector2D center, Vector2D pt0, Vector2D pt1)
            {
                PicArc arc = new PicArc(id, lType);
                arc._center = center;
                arc._radius = (pt0 - center).GetLength();
                arc._angleBeg = vecToAngleDeg(pt0 - center);
                arc._angleEnd = vecToAngleDeg(pt1 - center);
                while (arc._angleBeg < 0.0)                 arc._angleBeg += 360.0;
                while (arc._angleEnd < arc._angleBeg)       arc._angleEnd += 360.0;
                return arc;
            }
			#endregion

            #region Private methods
            protected Vector2D PointAtAngle(double angle)
            { 
                return new Vector2D(
                    _center.X + _radius * Math.Cos(angle * Math.PI / 180.0),
                    _center.Y + _radius * Math.Sin(angle * Math.PI / 180.0) );
            }
            protected bool PointOnArc(double angle)
            {
                double openingAngle = _angleEnd - _angleBeg;
                if (openingAngle >= 0)
                {
                    return ((_angleBeg <= angle) && (angle - _angleBeg <= openingAngle))
                        || ((_angleEnd > 360.0) && (angle + 360.0 < _angleEnd) && (_angleEnd - angle - 360.0 <= openingAngle));
                }
                else
                    return (angle >= _angleEnd) && (angle - _angleBeg >= openingAngle);
            }

            // floating point remainder function
            static protected double fmod(double value, double mod)
            {
                // he fmod() function returns the value x - i * y,
                //for some integer i such that, if y is non-zero,
                // the result has the same sign as x and magnitude less than the magnitude of y.
                double i = Math.Floor(Math.Abs(value/mod));
                return Math.Sign(value)*(Math.Abs(value) - i * Math.Abs(mod));
            }
            static protected double Mod360R(double ang)
            {
                ang = fmod( ang , 360.0) ;
                const double epsilon = 0.001;
	            if(ang >= 360 - epsilon)
		            ang = 0.0;
	            else if ((ang > -epsilon) && (ang < epsilon))
			        ang = 0.0;
		        else if ((ang > 90.0 - epsilon) && (ang < 90.0 + epsilon))
				    ang = 90.0;
			    else if ((ang > 270.0 - epsilon) && (ang < 270.0 + epsilon))
				    ang = 270.0;
				else if ((ang > 180.0 - epsilon) && (ang < 180.0 + epsilon))
				    ang = 180.0;
                return ang;
            }

            static public double vecToAngleDeg(Vector2D vec)
            {
                return vecToAngleRad(vec) * 180.0 / Math.PI;    
            }

            static public double vecToAngleRad(Vector2D vec)
            {
                // handling null vector case
                const double epsilon = 0.001;
                if (vec.GetLengthSquared() < epsilon) return 0.0;
                // non null vector
                vec.Normalize();
                if (vec.Y >= 0.0)
                    return Math.Acos(vec.X);
                else
                    return 2.0 * Math.PI - Math.Acos(vec.X);
            }
            #endregion

            #region Overrides
            protected override void DrawSpecific(PicGraphics graphics)
			{
				graphics.DrawArc(LineType, _center, _radius, _angleBeg, _angleEnd);
			}
            protected override void DrawSpecific(PicGraphics graphics, Transform2D transform)
            {                
                Vector2D vecCenterTransformed = Vector2D.Zero;
                double angleBeg = 0.0, angleEnd = 0.0;
                TransformData(transform, _radius, _center, _angleBeg, _angleEnd
                    , ref vecCenterTransformed, ref angleBeg, ref angleEnd);

                graphics.DrawArc(
                    LineType
                    , vecCenterTransformed
                    , _radius
                    , angleBeg
                    , angleEnd
                    );
            }
			protected override bool Evaluate()
			{
                _box.Reset();
/*
                if ((_angleBeg <= 0) && (_angleEnd <= 0))
                {
                    _angleBeg += 360.0;
                    _angleEnd += 360.0;
                }
*/
                // 6 points to be considered
                // arc extremities
                _box.Extend(PointAtAngle(_angleBeg/*AngleBegActual*/));
				_box.Extend(PointAtAngle(_angleEnd/*AngleEndActual*/));
                // horizontal and vertical tangent points
                double[] tab = new double[]{0.0, 90.0, 180.0, 270.0};
                foreach (double d in tab)
                    if (PointOnArc(d))
                        _box.Extend(PointAtAngle(d));

 				return true;
			}
            public override void Transform(Transform2D transform)
            {
                Vector2D[] vec4Pts = FourPointDefinition;
                int index = 0;
                foreach (Vector2D vec in vec4Pts)
                    vec4Pts[index++] = transform.transform(vec);
                FourPointDefinition = vec4Pts;
                SetModified();
            }
            public override PicEntity Clone(IEntityContainer factory)
            {
                PicArc arc = new PicArc(factory.GetNewEntityId(), LineType);
                arc._center = this._center;
                arc._radius = this._radius;
                arc._angleBeg = this._angleBeg;
                arc._angleEnd = this._angleEnd;
                return arc;
            }
            public override double Length
            {
                get { return _radius * Math.PI * Math.Abs(_angleEnd - _angleBeg) / 180.0; }
            }
            #endregion

            #region Public properties
            public Vector2D Center
            {
                get { return _center; }
            }
            public double Radius
            {
                get { return _radius; }
            }
            public double AngleBeg
            {
                get { return _angleBeg; }
            }
            public double AngleEnd
            {
                get { return _angleEnd; }
            }
            public Vector2D Source
            {
                get
                {
                    return new Vector2D(
                        _center.X + _radius * Math.Cos(_angleBeg * Math.PI / 180.0)
                        , _center.Y + _radius * Math.Sin(_angleBeg * Math.PI / 180.0)
                        );
                }
                set
                {
                    double ax = (value.X - _center.X) / _radius;
                    double ay = (value.Y - _center.Y) / _radius;

                    if (ay >= 0.0)
                        _angleBeg = Math.Acos(ax) * 180.0 / Math.PI;
                    else
                        _angleBeg = 360.0 - Math.Acos(ax) * 180.0 / Math.PI;
                }
            }
            public Vector2D Target
            {
                get
                {
                    return new Vector2D(
                        _center.X + _radius * Math.Cos(_angleEnd * Math.PI / 180.0)
                        , _center.Y + _radius * Math.Sin(_angleEnd * Math.PI / 180.0)
                        );
                }
                set
                {
                    double ax = (value.X - _center.X) / _radius;
                    double ay = (value.Y - _center.Y) / _radius;

                    if (ay >= 0.0)
                        _angleEnd = Math.Acos(ax) * 180.0 / Math.PI;
                    else
                        _angleEnd = 360.0 - Math.Acos(ax) * 180.0 / Math.PI;
                }
            }
            public void Swap()
            {
                double temp = _angleBeg;
                _angleBeg = _angleEnd;
                _angleEnd = temp;
            }

            public void Complement()
            {
                double openingAngle = _angleEnd - _angleBeg;
                if (openingAngle > 0)
                {
                    _angleBeg = _angleEnd;
                    _angleEnd = _angleBeg + 360.0 - openingAngle;
                }
                else
                {
                    _angleEnd = _angleBeg + 360.0 + openingAngle;
                }
            }

            public Vector2D[] FourPointDefinition
            {
                get
                {
                    Vector2D[] points = new Vector2D[4];
                    points[0] = _center;
                    points[1] = PointAtAngle(_angleBeg);
                    points[2] = PointAtAngle(0.5 * (_angleBeg + _angleEnd));
                    points[3] = PointAtAngle(_angleEnd);
                    return points;
                }
                set
                {
                    _center = value[0];
                    _angleBeg = vecToAngleDeg(value[1]-value[0]);
                    _angleEnd = vecToAngleDeg(value[3]-value[0]);

                    Vector2D ptDiff =  PointAtAngle(0.5 * (_angleEnd +_angleBeg)) - value[2];
                    if (ptDiff.GetLength() > 1)
                        Complement();
                 }
            }
            #endregion

            #region Private properties
            private double AngleBegActual
            {
                get { return (_angleBeg < _angleEnd ? _angleBeg : _angleEnd); }
            }
            private double AngleEndActual
            {
                get { return (_angleBeg < _angleEnd ? _angleEnd : _angleBeg); }
            }
            #endregion

            #region System.Object Overrides
            public override string ToString()
            {
                return string.Format("Arc id = {0}, center = {1}, radius = {2}, angleBeg = {3}, angleEnd = {4}\n"
                    , this.Id, _center, _radius, _angleBeg, _angleEnd);
            }
            #endregion

            #region Helpers
            private void TransformData(Transform2D transf, double radius, Vector2D center, double angleBeg, double angleEnd
                , ref Vector2D centerOut, ref double angleBegOut, ref double angleEndOut)
            { 
                centerOut = transf.transform(center);
                Vector2D ptBeg = transf.transform(PointAtAngle(center, radius, angleBeg));
                Vector2D ptEnd = transf.transform(PointAtAngle(center, radius, angleEnd));
                Vector2D ptIntExpected = transf.transform(PointAtAngle(center, radius, 0.5 * (angleBeg + angleEnd)));
                angleBegOut = vecToAngleDeg(ptBeg - centerOut);
                angleEndOut = vecToAngleDeg(ptEnd - centerOut);
                Vector2D ptInt = PointAtAngle(centerOut, radius, 0.5 * (angleBegOut + angleEndOut));
                if ((ptInt - ptIntExpected).GetLength() > 1)
                {
                    double openingAngle = angleEndOut - angleBegOut;
                    if (openingAngle > 0)
                    {
                        angleBegOut = angleEndOut;
                        angleEndOut = angleBegOut + 360.0 - openingAngle;
                    }
                    else
                    {
                        angleEndOut = angleBegOut + 360.0 + openingAngle;
                    }
                }

            }
            private Vector2D PointAtAngle(Vector2D center, double radius, double angle)
            {
                return new Vector2D(
                    center.X + radius * Math.Cos(angle * Math.PI / 180.0),
                    center.Y + radius * Math.Sin(angle * Math.PI / 180.0));
            }
            #endregion
        }
    }
}
