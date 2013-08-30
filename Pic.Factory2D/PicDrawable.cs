#region Using directives
using System;
using System.Collections.Generic;
using System.Text;

using Sharp3D.Math.Core;
using Sharp3D.Math;
using Sharp3D.Math.Geometry2D;
using System.Runtime.InteropServices;
#endregion

namespace Pic
{
	namespace Factory2D
	{
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
		public abstract class PicDrawable:PicEntity
		{
			#region Protected fields
            [NonSerialized]
			protected Box2D _box = new Box2D();
			#endregion

			#region Protected constructors
			protected PicDrawable(uint id)
				: base(id)
			{
			}
			#endregion

			#region Drawing methods
			public void Draw(PicGraphics graphics)
			{
				Compute();
				DrawSpecific(graphics);
			}
            public void Draw(PicGraphics graphics, Transform2D transform)
            {
                DrawSpecific(graphics, transform);
            }
            protected abstract void DrawSpecific(PicGraphics graphics);
            protected abstract void DrawSpecific(PicGraphics graphics, Transform2D transform);
			#endregion

            #region Abstract methods
            public abstract void Transform(Transform2D transform);
            #endregion

            #region Abstract properties
            abstract public Segment[] Segments { get; }
            #endregion

			#region Public properties
			public virtual Box2D Box
			{
				get
				{
					Compute();
                    if (!_box.IsValid)
                        throw new Exception(string.Format("Box {0} is invalid", _box.ToString()));
					return _box;
				}
			}
            public abstract Box2D ComputeBox(Transform2D transform);
			#endregion
		}
	}
}
