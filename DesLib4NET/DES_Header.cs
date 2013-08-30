using System;
using System.Collections.Generic;
using System.Text;

namespace DesLib4NET
{
    public class DES_Header
    {
        #region Constructor
        public DES_Header()
        {
            for (int i=0; i<15; ++i)
                _viewMatrix[i] = _viewMatrixInverse[i] = 0.0;
            for (int i=0; i<4; ++i)
                _viewMatrix[i*5] = _viewMatrixInverse[i*5] = 1.0;
            Set2D();
        }
        #endregion

        #region 2D 3D
        void Set2D()
        {
            _code2D3D = (byte)253;
        }
        void Set3D()
        {
            _code2D3D = (byte)254;
        }
        #endregion

        #region Data members
        public float _xmin, _ymin, _xmax, _ymax;
        public byte _code2D3D;
        public double[] _viewMatrix = new double[16];
        public double[] _viewMatrixInverse = new double[16];
        #endregion
    }

    public struct DES_Date
    {
        #region Data members
        public int nDay, nMonth, nYear, nHour, nMin, nSec;
        #endregion
    }
}
