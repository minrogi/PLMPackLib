using System;
using System.Collections.Generic;
using System.Text;

namespace DesLib4NET
{
    public class DES_SuperBaseHeader
    {
        #region Constructor
        public DES_SuperBaseHeader()
        {
            // set a default date time
            _dateCreated.nDay = 11;
            _dateCreated.nMonth = 05;
            _dateCreated.nYear = 2007;
            _dateCreated.nHour = 12;
            _dateCreated.nMin = 0;
            _dateCreated.nSec = 0;

            _dateModified.nDay = 11;
            _dateModified.nMonth = 05;
            _dateModified.nYear = 2007;
            _dateModified.nHour = 12;
            _dateModified.nMin = 0;
            _dateModified.nSec = 0;
        }
        #endregion

        #region Data members
        public uint _positionListOfEntities = 0;
        public uint _positionFileTable = 0;
        public uint _fileVersion = 0;
        public string _companyName="", _userName="", _comment="";
        public DES_Date _dateCreated, _dateModified;
        #endregion
    }
}
