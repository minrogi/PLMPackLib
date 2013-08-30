#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Pic.Factory2D
{
    public class FileFormat
    {
        #region Constructor
        public FileFormat(string fileFormat, string fileExtension, string fileApplication)
        {
            _fileFormat = fileFormat;
            _fileExtension = fileExtension;
            _fileApplication = fileApplication;
        }
        #endregion

        #region Object overrides
        public override string ToString()
        {
            return _fileFormat + " (" + _fileExtension + ")";
        }
        #endregion

        #region Data members
        public string _fileFormat;
        public string _fileExtension;
        public string _fileApplication;
        #endregion
    }
}
