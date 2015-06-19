using System;
using System.Collections.Generic;
using System.Text;

namespace DesLib4NET
{
    public abstract class DES_Entity
    {
        #region Constructors
        public DES_Entity(byte pen, byte grp, byte layer)
        {
            _pen = pen;
            _grp = grp;
            _layer = layer;
        }
        #endregion
        #region Data members
        public float _x = 0.0f, _y = 0.0f;
        public float _dim = 0.0f, _dir = 0.0f;
        public byte _pen = 0, _layer = 0, _grp = 0;
        public byte _lock = 0;
        #endregion
    }

    public class DES_Point : DES_Entity
    {
        #region Constructor
        public DES_Point(float x, float y, byte pen, byte grp, byte layer)
            : base(pen, grp, layer)
        {
            base._x = x;
            base._y = y;
        }
        #endregion
        #region Object override
        public override string ToString()
        {
            return "Point: Coord(" + _x.ToString() + ", " + _y.ToString() + ") Pen = " + _pen.ToString();
        }
        #endregion
    }

    public class DES_Segment : DES_Entity
    {
        #region Constructor
        public DES_Segment(float x1, float y1, float x2, float y2, byte pen, byte grp, byte layer)
            : base(pen, grp, layer)
        {
            double length = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            base._x = 0.5f*(x1+x2);
            base._y = 0.5f*(y1+y2);
            base._dim = 0.5f*(float)length;
            // dir
            double vecx = (x2-x1)/length;
	        double vecy = (y2-y1)/length;
	        double angleRad = 0.0;
	        if (vecy > 1.0)		vecy = 1.0;
	        if (vecy < -1.0)	vecy = -1.0;
	        if (vecx >= 0.0)
	        {
		        if (vecy >= 0.0)
			        angleRad = Math.Asin( vecy );
		        else
                    angleRad = 2 * Math.PI - Math.Asin(-vecy);
	        }
	        else
	        {
		        if (vecy >= 0.0)
                    angleRad = Math.PI - Math.Asin(vecy);
		        else
                    angleRad = Math.PI + Math.Asin(-vecy);	
	        }
            base._dir = (float)(angleRad * 180.0 / Math.PI);
        }
        #endregion
        #region Object override
        public override string ToString()
        {
            return "Segment: Coord(" + _x.ToString() + ", "+ _y.ToString() + ") Dim = " + _dim.ToString() + " Dir = " + _dir.ToString();
        }
        #endregion
        #region Properties
        public float X1
        {
            get { return _x - _dim * (float)Math.Cos(_dir * Math.PI / 180.0); }
        }
        public float Y1
        {
            get { return _y - _dim * (float)Math.Sin(_dir * Math.PI / 180.0); }
        }
        public float X2
        {
            get { return _x + _dim * (float)Math.Cos(_dir * Math.PI / 180.0); }
        }
        public float Y2
        {
            get { return _y + _dim * (float)Math.Sin(_dir * Math.PI / 180.0); }
        }
        #endregion
    }

    public class DES_Arc : DES_Entity
    {
        #region Constructor
        public DES_Arc(float x, float y, float radius, float angle1, float angle2, byte pen, byte grp, byte layer)
            :base(pen, grp, layer)
        {
            base._x = x;
            base._y = y;
            base._dim = radius;
            base._dir = angle1;
            _angle = angle2 - angle1;
        }
        #endregion
        #region Object override
        public override string ToString()
        {
            return "Arc: Coord(" + _x.ToString() + ", " + _y.ToString() + ") Dim = " + _dim.ToString() + " Dir = " + _dir.ToString();
        }
        #endregion
        #region Data members
        public float _angle = 0.0f;
        #endregion
    }

    public class DES_Bezier : DES_Entity
    {
        public DES_Bezier(byte pen, byte grp, byte layer)
            : base(pen, grp, layer)
        { 
        }
    }

    public class DES_Nurbs : DES_Entity
    {
        public DES_Nurbs(byte pen, byte grp, byte layer)
            : base(pen, grp, layer)
        { 
        }
    }

    public class DES_Ellipse : DES_Entity
    { 
        public DES_Ellipse(byte pen, byte grp, byte layer)
            : base(pen, grp, layer)
        { 
        }
    }

    public class DES_Surface : DES_Entity
    { 
         public DES_Surface(byte pen, byte grp, byte layer)
            : base(pen, grp, layer)
        { 
        }
   }

    public class DES_Pose : DES_Entity
    {
        #region Constructor
        public DES_Pose(float x, float y, float angle, byte grp)
            : base(0, grp, 0)
        {
            _dx = x; _dy = y;
            _dir = angle;
            _mir = 0;
        }
        #endregion
        #region DATA MEMBERS
        public float _dx = 0.0f, _dy = 0.0f;
        public short _mir;
        #endregion
    }

    public class DES_CardboardFormat : DES_Entity
    {
        #region Constructors
        public DES_CardboardFormat(byte pen, byte grp, byte layer)
            : base(pen, grp, layer)
        { 
        }
        public DES_CardboardFormat(
            byte grp
            , float x, float y
            , float width, float height
            , float thickness)
            : base(grp, 0, 0)
        {
            _dim = 1.0f;
            _dir = 0.0f;

            _width = width;
            _height = height;
            _thickness = thickness;
        }
        #endregion
        #region DATA MEMBERS
        public float _width = 0.0f, _height = 0.0f;
        public float _thickness = 0.0f;
        #endregion
    }

    public class DES_Text : DES_Entity
    { 
        public DES_Text(byte pen, byte grp, byte layer)
            : base(pen, grp, layer)
        { 
        }
    }

    public class DES_Image : DES_Entity
    {
        public DES_Image(byte pen, byte grp, byte layer)
            : base(pen, grp, layer)
        { 
        }
    }

    public class DES_CotationDistance : DES_Entity
    { 
        #region Constructor
        public DES_CotationDistance(
            float x, float y, float dir, float dim
            , byte pen, byte grp, byte layer
            , float offset, float reduction
            , float ecartSup, float ecartInf, bool invDep
            , bool aText, bool aTolerance, bool aEspace
            , short noDecimals, string text, char houv)
            : base(pen, grp, layer)
        {
            _x = x;
            _y = y;
            _dir = dir;
            _dim = dim;

            _offset = offset;
            _reduction = reduction;

            _ecarSup = ecartSup;
            _ecartInf = ecartInf;
            _invDep = invDep;

            _noDecimals = noDecimals;
            _text = text;
            _houv = houv;
             
        }
        #endregion

        #region Public properties
        public float X1 { get { return _x - _dim * (float)Math.Cos(_dir * Math.PI / 180.0); } }
        public float Y1 { get { return _y - _dim * (float)Math.Sin(_dir * Math.PI / 180.0); } }
        public float X2 { get { return _x + _dim * (float)Math.Cos(_dir * Math.PI / 180.0); } }
        public float Y2 { get { return _y + _dim * (float)Math.Sin(_dir * Math.PI / 180.0); } }
        #endregion

        #region Object overrides
        public override string ToString()
        {
            return string.Format("Cotation distance: Coord = ({0}, {1})) Dim = {2} Dir = {3} Offset = {4}"
                , _x, _y, _dim, _dir
                , _offset);
        }
        #endregion

        #region Data members
        public float _offset, _reduction;
        public float _ecarSup, _ecartInf;
        public bool _invDep;
        public short _noDecimals;
        public string _text;
        public char _houv;
        #endregion
    }
}
