#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using Sharp3D.Math.Core;

using System.Drawing;
#endregion

namespace Pic.Factory2D
{
    public class InvalidBoxException : Exception
    { 
        public InvalidBoxException(string message)
            : base(message)
        {
        }
    }
    public class Box2D : ICloneable
    {
        #region Private fields
        Vector2D _ptMin = new Vector2D(double.MaxValue, double.MaxValue);
        Vector2D _ptMax = new Vector2D(double.MinValue, double.MinValue);
        #endregion

        #region Constructors
        public Box2D()
        {
            _ptMin = new Vector2D(double.MaxValue, double.MaxValue);
            _ptMax = new Vector2D(double.MinValue, double.MinValue);
        }
        public Box2D(double xmin, double ymin, double xmax, double ymax)
        {
            Extend( new Vector2D(xmin, ymin) );
            Extend( new Vector2D(xmax, ymax) );
        }
        public Box2D(Vector2D pt0, Vector2D pt1)
        {
            Extend(pt0);
            Extend(pt1);
        }
        public Box2D(Box2D box)
        {
            Extend(box._ptMin);
            Extend(box._ptMax);
        }
        #endregion

        #region Constants
        public static Box2D Initial { get { return new Box2D(); } }
        #endregion

        #region Public properties
        public Vector2D PtMin
        {
            get { return _ptMin; }
        }
        public Vector2D PtMax
        {
            get { return _ptMax; }
        }
        public double XMin
        {
            get { return _ptMin.X; }
            set { _ptMin.X = value; }
        }
        public double YMin
        {
            get { return _ptMin.Y; }
            set { _ptMin.Y = value; }
        }
        public double XMax
        {
            get { return _ptMax.X; }
            set { _ptMax.X = value; }
        }
        public double YMax
        {
            get { return _ptMax.Y; }
            set { _ptMax.Y = value; }
        }
        public Vector2D Center
        {
            get
            {
                if (!IsValid)
                    throw new InvalidBoxException("Box is invalid: can not get center");
                return 0.5 * (_ptMin + _ptMax);
            }
            set
            {
                if (!IsValid)
                    throw new InvalidBoxException("Box is invalid: can not set center");
                Vector2D diff = _ptMax-_ptMin;
                _ptMin.X = value.X - 0.5 * diff.X;
                _ptMin.Y = value.Y - 0.5 * diff.Y;
                _ptMax.X = value.X + 0.5 * diff.X;
                _ptMax.Y = value.Y + 0.5 * diff.Y;
            }
        }

        public double Width
        {
            get
            {
                if (!IsValid)
                    throw new InvalidBoxException("Box is invalid: can not get width");
                return (_ptMax - _ptMin).X;
            }
            set
            {
                Vector2D center = this.Center;
                _ptMin.X = center.X - 0.5 * value;
                _ptMax.X = center.X + 0.5 * value;
            }
        }

        public double Height
        {
            get
            {
                if (!IsValid)
                    throw new InvalidBoxException("Box is invalid: can not get height");
                return (_ptMax - _ptMin).Y;
            }
            set
            {
                Vector2D center = this.Center;
                _ptMin.Y = center.Y - 0.5 * value;
                _ptMax.Y = center.Y + 0.5 * value;
            }
        }
        #endregion

        #region Public methods
        public bool IsValid
        {
            get { return (_ptMin.X <= _ptMax.X) && (_ptMin.Y <= _ptMax.Y); }
        }
        public void Reset()
        {
            _ptMin = new Vector2D(double.MaxValue, double.MaxValue);
            _ptMax = new Vector2D(double.MinValue, double.MinValue);
        }
        public void Extend(Vector2D vec)
        {
            _ptMin.X = Math.Min(_ptMin.X, vec.X);
            _ptMin.Y = Math.Min(_ptMin.Y, vec.Y);
            _ptMax.X = Math.Max(_ptMax.X, vec.X);
            _ptMax.Y = Math.Max(_ptMax.Y, vec.Y);
        }
		public void Extend(Box2D box)
		{
            _ptMin.X = Math.Min(_ptMin.X, box._ptMin.X);
            _ptMin.Y = Math.Min(_ptMin.Y, box._ptMin.Y);
            _ptMax.X = Math.Max(_ptMax.X, box._ptMax.X);
            _ptMax.Y = Math.Max(_ptMax.Y, box._ptMax.Y);
		}
		public void AddMargin(double margin)
		{
			if (!IsValid)
                throw new InvalidBoxException("Box is invalid: can not set margin");
            _ptMin.X -= margin;
            _ptMax.X += margin;
            _ptMin.Y -= margin;
            _ptMax.Y += margin;
		}
        public void AddMarginRatio(double ratio)
        {
            if (!IsValid)
                throw new InvalidBoxException("Box is invalid: can not set margin");
            double ratioX = ratio * Width;
            _ptMin.X -= ratioX;
            _ptMax.X += ratioX;
            double ratioY = ratio * Height;
            _ptMin.Y -= ratioY;
            _ptMax.Y += ratioY;
        }
        public void AddMarginHorizontal(double margin)
        {
			if (!IsValid)
                throw new InvalidBoxException("Box is invalid: can not set margin");
            _ptMin.X -= margin;
            _ptMax.X += margin;
        }
        public void AddMarginVertical(double margin)
        {
			if (!IsValid)
                throw new InvalidBoxException("Box is invalid: can not set margin");
            _ptMin.Y -= margin;
            _ptMax.Y += margin;
        }
        public void TranslateByRatio(double transRatioX, double transRatioY)
        {
            Vector2D translation = new Vector2D(Width * transRatioX, Height * transRatioY);
            _ptMin += translation;
            _ptMax += translation;
        }

        public void Zoom(double delta)
        {
	        Vector2D vecSize = new Vector2D(Width*(1.0+delta), Height*(1.0+delta));
            Vector2D center = Center;
            _ptMin = center - 0.5 * vecSize;
            _ptMax = center + 0.5 * vecSize;
        }

        public void Translate(Vector2D translateVector)
        {
            _ptMin += translateVector;
            _ptMax += translateVector;
        }

        public bool Contains(Vector2D pt)
        {
            double epsilon = Sharp3D.Math.Core.MathFunctions.EpsilonD;
            return _ptMin.X - pt.X <= epsilon || _ptMin.Y - pt.Y <= epsilon || _ptMax.X - pt.X >= -epsilon || _ptMax.Y -  pt.Y >= -epsilon;
        }

        public bool Contains(Box2D box)
        {
            double epsilon = Sharp3D.Math.Core.MathFunctions.EpsilonD;
            return _ptMin.X - box._ptMin.X <= epsilon && _ptMin.Y - box._ptMin.Y <= epsilon && _ptMax.X - box._ptMax.X >= -epsilon && _ptMax.Y - box._ptMax.Y >= -epsilon;
        }

        public Box2D Transform(Transform2D transf)
        {
            Box2D box = Box2D.Initial;
            box.Extend( transf.transform(_ptMin) );
            box.Extend( transf.transform(_ptMax) );
            return box;
        }
        #endregion

        #region Get image size
        public Size GetSizeFromWidth(int width, int maxHeight)
        {
            if (!IsValid)
                throw new InvalidBoxException("Can not compute Size");
            Size s = new Size(width, (int)(width * (Height / Width)));
            if (s.Height > maxHeight)
            {
                s.Height = maxHeight;
                s.Width = (int)(maxHeight * (Width / Height));
            }
            return s;
        }

        public Size GetSizeFromHeight(int height, int maxWidth)
        {
            if (!IsValid)
                throw new InvalidBoxException("Can not compute Size");
            Size s = new Size( (int)(height * (Width / Height)), height);
            if (s.Width > maxWidth)
            {
                s.Width = maxWidth;
                s.Height = (int)(maxWidth * (Height / Width));
            }
            return s;
        }

        public Size GetSizeFromLeading(int maxDim)
        {
            if (Width > Height)
                return GetSizeFromWidth(maxDim, maxDim);
            else
                return GetSizeFromHeight(maxDim, maxDim);
        }
        #endregion

        #region ICloneable Members
        /// <summary>
        /// Creates an exact copy of this <see cref="Box2D"/> object.
        /// </summary>
        /// <returns>The <see cref="Box2D"/> object this method creates, cast as an object.</returns>
        object ICloneable.Clone()
        {
            return new Box2D(this);
        }
        /// <summary>
        /// Creates an exact copy of this <see cref="Box2D"/> object.
        /// </summary>
        /// <returns>The <see cref="Box2D"/> object this method creates.</returns>
        public Box2D Clone()
        {
            return new Box2D(this);
        }
        #endregion

        #region System.Object overrides
		/// <summary>
		/// Returns the hashcode for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return _ptMin.GetHashCode() ^ _ptMax.GetHashCode();
		}
		/// <summary>
		/// Returns a value indicating whether this instance is equal to
		/// the specified object.
		/// </summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="Box2D"/> and has the same values as this instance; otherwise, <see langword="false"/>.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Box2D)
			{
				Box2D v = (Box2D)obj;
				return (_ptMin == v._ptMin) && (_ptMax == v._ptMax);
			}
			return false;
		}
		/// <summary>
		/// Returns a string representation of this object.
		/// </summary>
		/// <returns>A string representation of this object.</returns>        public override string ToString()
        public override string ToString()
        {
            return string.Format("xmin = {0}, xmax = {1}, ymin = {2}, ymax = {3}", _ptMin.X, _ptMax.X, _ptMin.Y, _ptMax.Y);
		}
		#endregion
	}
}
