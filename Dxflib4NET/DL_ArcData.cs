using System;
using System.Collections.Generic;
using System.Text;

namespace Dxflib4NET
{
    public class DL_ArcData
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="acx">X coordinate of center point</param>
        /// <param name="acy">Y coordinate of center point</param>
        /// <param name="acz">Z coordinate of center point</param>
        /// <param name="aRadius">Radius of arc</param>
        /// <param name="aAngle1">Start angle in degree</param>
        /// <param name="aAngle2">End angle in degree</param>
        /// <param name="lColor">Type</param>
        /// <param name="lLayer">Layer</param>
        public DL_ArcData(double acx, double acy, double acz,
            double aRadius,
            double aAngle1, double aAngle2,
            int lColor, string lLayer)
        {
            cx = acx;
            cy = acy;
            cz = acz;
            radius = aRadius;
            angle1 = aAngle1;
            angle2 = aAngle2;
            color = lColor;
            layer = lLayer;
        }
        #endregion
        #region Data members
        /// <summary>
        /// Coordinates of center point.
        /// </summary>
        public double cx, cy, cz;
        /// <summary>
        /// Radius of arc.
        /// </summary>
        public double radius;
        /// <summary>
        /// Startangle, Endangle of arc in degrees.
        /// </summary>
        public double angle1, angle2;
        /// <summary>
        /// Type.
        /// </summary>
        public int color;
        /// <summary>
        /// Layer.
        /// </summary>
        public string layer;
        #endregion
    }
}
