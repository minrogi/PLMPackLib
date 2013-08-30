#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
#endregion

namespace Pic.Factory2D
{
    [Serializable]
    public class PicToolTooLongException : Exception
    {
        public PicToolTooLongException(): base() { }
        public PicToolTooLongException(string message): base(message) { }
        public PicToolTooLongException(string message, Exception innerException) : base(message, innerException) { }
        public PicToolTooLongException(string message, SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public abstract class PicTool
    {
        public abstract void ProcessFactory(PicFactory factory);
        public virtual void ProcessEntity(PicEntity entity) { }
    }
}
