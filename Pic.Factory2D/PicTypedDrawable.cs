#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
#endregion

namespace Pic
{
	namespace Factory2D
	{
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
		public abstract class PicTypedDrawable : PicDrawable
		{
			#region Private Fields
			private PicGraphics.LT _lineType;
            private short _group, _layer;
			#endregion

			#region Protected constructors
			protected PicTypedDrawable(uint id, PicGraphics.LT lType)
				: base(id)
			{
                _lineType = lType;
                _group = 1;
                _layer = 1;
			}
			#endregion

            #region Public properties
            public PicGraphics.LT LineType
			{
				get { return _lineType; }
				set { _lineType = value; }
			}
            public abstract double Length { get; }
            public short Group
            {
                get { return _group; }
                set { _group = value; }
            }
            public short Layer
            {
                get { return _layer; }
                set { _layer = value; }
            }
			#endregion
        }
	}
}