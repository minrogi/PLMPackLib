#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using Pic.Plugin;
#endregion

namespace Pic.Data
{
    public class ComponentSearchMethodDB : IComponentSearchMethod
    {
        #region Constructors
        public ComponentSearchMethodDB()
        {
        }
        #endregion

        #region IPluginSearchMethod implementation
        public string GetFilePathFromGuid(Guid guid)
        {
            using (Pic.Data.File file = ParametricComponent.getFileByComponentGuid(guid))
            {
                if (null != file && file.Valid)
                    return file.FullPath;
            }
            throw new PluginException(string.Format("Failed to load Component with Guid = {0}", guid.ToString()));
        }
        #endregion
    }
}
