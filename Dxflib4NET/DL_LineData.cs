using System;
using System.Collections.Generic;
using System.Text;

namespace Dxflib4NET
{
    public class DL_LineData
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lx1">X start coordinate</param>
        /// <param name="ly1">Y start coordinate</param>
        /// <param name="lz1">Z start coordinate</param>
        /// <param name="lx2">X end coordinate</param>
        /// <param name="ly2">Y end coordinate</param>
        /// <param name="lz2">Z end coordinate</param>
        /// <param name="lColor">Color</param>
        /// <param name="lLayer">Layer</param>
        public DL_LineData(double lx1, double ly1, double lz1,
            double lx2, double ly2, double lz2,
            int lColor, string lLayer)
        {
            x1 = lx1;
            y1 = ly1;
            z1 = lz1;

            x2 = lx2;
            y2 = ly2;
            z2 = lz2;

            color = lColor;
            layer = lLayer;
        }
        #endregion

        #region Data members
        /// <summary>
        /// Start coordinates
        /// </summary>
        public double x1, y1, z1;
        /// <summary>
        /// End coordinates
        /// </summary>
        public double x2, y2, z2;
        /// <summary>
        /// Color
        /// </summary>
        public int color;
        /// <summary>
        /// Layer
        /// </summary>
        public string layer;
        #endregion
    }
}
