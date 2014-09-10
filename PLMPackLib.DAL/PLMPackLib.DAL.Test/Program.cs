#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// log4net
using log4net;
using log4net.Config;
#endregion

namespace PLMPackLib.DAL.Test
{
    class Program
    {
        #region log4net
        protected static readonly ILog _log = LogManager.GetLogger(typeof(Program));
        #endregion

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            try
            {
                PLMPackLibDb db = new PLMPackLibDb();
                #region Cardboad formats
                // create formats
                CardboardFormat cbf = null;
                if (!CardboardFormat.Exists(db, "F1"))
                    cbf = CardboardFormat.CreateNew(db, "F1", "Form1", 1000.0f, 1000.0f);
                if (!CardboardFormat.Exists(db, "F2"))
                    cbf = CardboardFormat.CreateNew(db, "F2", "Form2", 2000.0f, 2000.0f);
                if (!CardboardFormat.Exists(db, "F3"))
                    cbf = CardboardFormat.CreateNew(db, "F3", "Form3", 3000.0f, 3000.0f);
                // get list of formats
                foreach (CardboardFormat f in CardboardFormat.GetAll(db))
                    _log.Info(f.ToString());
                #endregion

                #region Cardboard profile
                CardboardProfile cbp = null;
                if (!CardboardProfile.Exists(db, "P1"))
                    cbp = CardboardProfile.CreateNew(db, "P1", "Profile 1", 1.0f);
                #endregion

            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
    }
}
