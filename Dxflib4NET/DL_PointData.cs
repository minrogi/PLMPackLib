using System;
using System.Collections.Generic;
using System.Text;

namespace Dxflib4NET
{
    public class DL_PointData
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="px">X coordinate</param>
        /// <param name="py">Y coordinate</param>
        /// <param name="pz">Z coordinate</param>
        DL_PointData(double px, double py, double pz)
        {
            x = px;
            y = py;
            z = pz;
        }
        #endregion

        #region Data members
        /// <summary>
        /// Coordinate of the point
        /// </summary>
        public double x, y, z;
        #endregion
    }
}
