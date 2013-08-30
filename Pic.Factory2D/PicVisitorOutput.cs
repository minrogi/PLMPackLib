using System;
using System.Collections.Generic;
using System.Text;

namespace Pic.Factory2D
{
    public abstract class PicVisitorOutput : PicFactoryVisitor
    {
        #region Private data members
        protected string title;
        protected string author;
        #endregion

        #region Additional abstract method
        public abstract byte[] GetResultByteArray();
        #endregion

        #region Public properties
        /// <summary>
        /// document author
        /// </summary>
        public string Author
        {
            set { author = value; }
        }
        /// <summary>
        /// document title
        /// </summary>
        public string Title
        {
            set { title = value; }
        }
        #endregion
    }
}
