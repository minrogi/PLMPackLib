#region Using directives
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Text;

using log4net;

using Pic.Plugin;
#endregion

namespace PicParam
{
    public class ComponentSearchMethodDB : IComponentSearchMethod
    {
        #region Constructors
        public ComponentSearchMethodDB()
        {
        }
        #endregion

        #region IComponentSearchMethod implementation
        public string GetFilePathFromGuid(Guid guid)
        {
            // get db constext
            Pic.DAL.SQLite.PPDataContext db = new Pic.DAL.SQLite.PPDataContext();
            // retrieve component
            Pic.DAL.SQLite.Component comp = Pic.DAL.SQLite.Component.GetByGuid(db, guid);
            if (null == comp)
                throw new PluginException(string.Format("Failed to load Component with Guid = {0}", guid.ToString()));
            // return path
            return comp.Document.File.Path(db);
        }
        public double GetDoubleParameterDefaultValue(Guid guid, string name)
        {
            Pic.DAL.SQLite.PPDataContext db = new Pic.DAL.SQLite.PPDataContext();
            Pic.DAL.SQLite.Component comp = Pic.DAL.SQLite.Component.GetByGuid(db, guid);
            if (null == comp)
                throw new PluginException(string.Format("Failed to load Component with Guid = {0}", guid.ToString()));
            try
            {   return comp.GetParamDefaultValueDouble(db, name); }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return 0;
            }
        }
        public int GetIntParameterDefaultValue(Guid guid, string name)
        {
            return 0;
        }
        public bool GetBoolParameterDefaultValue(Guid guid, string name)
        {
            return true;
        }
        public int GetMultiParameterDefaultValue(Guid guid, string name)
        {
            return 0;
        }
        #endregion

        #region DATA MEMBERS
        protected static readonly ILog _log = LogManager.GetLogger(typeof(ComponentSearchMethodDB));
        #endregion
    }
}
