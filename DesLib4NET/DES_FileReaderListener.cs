#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
#endregion

namespace DesLib4NET
{
    /// <summary>
    /// DES_FileReader listener interface
    /// </summary>
    public interface DES_FileReaderListener
    {
        void Begin();
        void End();
        void SetRange(uint min, uint max);
        void SetPosition(uint pos);
    }

    public class DES_FileReaderListenerConsole : DES_FileReaderListener
    {
        #region Constructor
        public DES_FileReaderListenerConsole()
        {
        }
        #endregion

        #region Public properties
        public void Begin()
        {
            Console.WriteLine("Begin...");
        }
        public void End()
        { 
            Console.WriteLine("End...");
        }
        public void SetRange(uint min, uint max)
        {
            Console.WriteLine("Range Min = " + min.ToString() + "Max = " + max.ToString());
        }
        public void SetPosition(uint pos)
        {
            Console.WriteLine("Position " + pos.ToString());
        }
        #endregion
    }
}
