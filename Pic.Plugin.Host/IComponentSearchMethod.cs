#region Using directives
using System;
using System.Text;
using System.IO;
#endregion

namespace Pic.Plugin
{
    #region IComponentSearchMethod (interface)
    public interface IComponentSearchMethod
    {
        /// <summary>
        /// gets a plugin path from a given guid
        /// </summary>
        /// <param name="guid">Guid</param>
        /// <returns>a file path</returns>
        string GetFilePathFromGuid(Guid guid);
        /// get parameter default value
        double GetDoubleParameterDefaultValue(Guid guid, string name);
        int GetIntParameterDefaultValue(Guid guid, string name);
        bool GetBoolParameterDefaultValue(Guid guid, string name);
        int GetMultiParameterDefaultValue(Guid guid, string name);
    }
    #endregion

    #region ComponentSearchDirectory (implements IComponentSearchMethod)
    public class ComponentSearchDirectory : IComponentSearchMethod
    {
        #region Constructors
        public ComponentSearchDirectory(string directoryPath)
        {
            _directoryPath = directoryPath;
        }
        #endregion

        #region IComponentSearchMethod implementation
        public string GetFilePathFromGuid(Guid guid)
        {
            ComponentLoader loader = new ComponentLoader();

            DirectoryInfo dirInfo = new DirectoryInfo(_directoryPath);
            foreach (FileInfo fileInfo in dirInfo.GetFiles())
            {
                Component component = loader.LoadComponent(fileInfo.FullName);
                if (guid == component.Guid)
                    return fileInfo.FullName;
            }
            throw new PluginException(string.Format("Failed to load Component with Guid = {0} in directory {1}", guid.ToString(), _directoryPath));
        }
        public double GetDoubleParameterDefaultValue(Guid guid, string name)
        {
            Component comp = GetComponentFromGuid(guid);
            return comp.BuildParameterStack(null).GetDoubleParameterValue(name);
        }
        public int GetIntParameterDefaultValue(Guid guid, string name)
        {
            Component comp = GetComponentFromGuid(guid);
            return comp.BuildParameterStack(null).GetIntParameterValue(name);
        }
        public bool GetBoolParameterDefaultValue(Guid guid, string name)
        {
            Component comp = GetComponentFromGuid(guid);
            return comp.BuildParameterStack(null).GetBoolParameterValue(name);
        }
        public int GetMultiParameterDefaultValue(Guid guid, string name)
        {
            Component comp = GetComponentFromGuid(guid);
            return comp.BuildParameterStack(null).GetMultiParameterValue(name);
        }
        #endregion

        #region Helpers
        public Component GetComponentFromGuid(Guid guid)
        {
            ComponentLoader loader = new ComponentLoader();

            DirectoryInfo dirInfo = new DirectoryInfo(_directoryPath);
            foreach (FileInfo fileInfo in dirInfo.GetFiles())
            {
                Component component = loader.LoadComponent(fileInfo.FullName);
                if (guid == component.Guid)
                    return component;
            }
            throw new PluginException(string.Format("Failed to load Component with Guid = {0} in directory {1}", guid.ToString(), _directoryPath));
        }
        #endregion

        #region Data members
        private string _directoryPath;
        #endregion
    }
    #endregion
}
