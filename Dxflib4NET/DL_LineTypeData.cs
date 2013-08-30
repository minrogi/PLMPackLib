using System;
using System.Collections.Generic;
using System.Text;

namespace Dxflib4NET
{
    public class DL_LineTypeData
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lName">Line type name</param>
        /// <param name="lFlags">Line type flags</param>
        public DL_LineTypeData(string lName, int lFlags)
        {
            name = lName;
            flags = lFlags;
        }
        #endregion

        #region Data members
        /// <summary>
        /// Line type name.
        /// </summary>
        public string name;
        /// <summary>
        /// Line type flags.
        /// </summary>
        public int flags;
        #endregion
    }
}
