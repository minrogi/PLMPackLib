using System;
using System.Collections.Generic;
using System.Text;

namespace Dxflib4NET
{
    public class DL_LayerData
    {
        #region Constructor
        public DL_LayerData(string lName, int lFlag)
        {
            this.lName = lName;
            this.lFlag = lFlag;
        }
        #endregion
        #region Data members
        /// <summary>
        /// Layer name.
        /// </summary>
        public string lName;
        /// <summary>
        /// Layer flags. (1 = frozen, 2 = frozen by default, 4 = locked)
        /// </summary>
        public int lFlag;
        #endregion
    }
}
