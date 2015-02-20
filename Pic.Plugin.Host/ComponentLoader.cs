#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Collections.Specialized;
using System.Drawing;

using Pic.Factory2D;

using Sharp3D.Math.Core;
#endregion

namespace Pic.Plugin
{
    #region Configuration (.config file)
    public class PluginPathData : ConfigurationSection
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        public PluginPathData()
        {
        }

        [ConfigurationProperty("Path")]
        public string Path
        {
            get { return (string)this["Path"]; }
            set { this["Path"] = value; }
        }
    }
    #endregion // PluginPathData

    #region ComponentLoader
    public class ComponentLoader : IPluginHost, IDisposable
    {
        #region Data members
        private IComponentSearchMethod _searchMethod = null;
        private static List<Component> _componentCache = new List<Component>();
        private static Dictionary<Guid, ParameterStack> _parameterStackCache = new Dictionary<Guid, ParameterStack>();
        private static uint _cacheSize = 20;
        // delegate
        public delegate void PluginAccessed(Guid guidPlugin);
        // event called when accessing a component by Guid
        public static event PluginAccessed OnPluginAccessed;
        #endregion

        #region Constructor
        public ComponentLoader()
        {
        }
        #endregion

        #region Loading methods
        public Component LoadComponent(string filePath)
        {
            FileInfo file = new FileInfo(filePath);

            // Preliminary check
            // must exist
            if (!file.Exists)
                throw new PluginException(string.Format("File {0} does not exist. Cannot load Component.", filePath));
            // must be a dll file
            if (!file.Extension.Equals(".dll"))
                throw new PluginException(string.Format("File {0} is not a dll file. Cannot load Component.", filePath));

            //Create a new assembly from the plugin file we're adding..
            Assembly pluginAssembly = Assembly.LoadFrom(filePath);
            Component component = null;
            //Next we'll loop through all the Types found in the assembly
            foreach (Type pluginType in pluginAssembly.GetTypes())
            {
                if (pluginType.IsPublic) //Only look at public types
                {
                    if (!pluginType.IsAbstract)  //Only look at non-abstract types
                    {
                        //Gets a type object of the interface we need the plugins to match
                        Type typeInterface = pluginType.GetInterface("Pic.Plugin.IPlugin", true);

                        //Make sure the interface we want to use actually exists
                        if (typeInterface != null)
                        {
                            //Create a new available plugin since the type implements the IPlugin interface
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString()));
                            if (null != plugin)
                            {
                                component = new Component(plugin);

                                //Set the Plugin's host to this class which inherited IPluginHost
                                plugin.Host = this;

                                //Call the initialization sub of the plugin
                                plugin.Initialize();

                                //cleanup a bit
                                plugin = null;
                            }
                        }
                        typeInterface = null; //Mr. Clean			
                    }
                }
            }
            pluginAssembly = null; //more cleanup

            // check that a component was actually created
            if (null == component)
                throw new Pic.Plugin.PluginException(string.Format("Failed to load valid component from existing file {0}", filePath));

            // insert component in cache for fast retrieval
            ComponentLoader.InsertInCache(component);

            return component;
        }

        public Component LoadComponent(Guid guid)
        {
            // verify if component 
            Component comp = ComponentLoader.GetFromCache(guid);
            if (null != comp)
                return comp;
            if (!(_searchMethod is IComponentSearchMethod))
                throw new PluginException("Component loader was not initialized with a valid plugin search method.");
            return LoadComponent(_searchMethod.GetFilePathFromGuid(guid));
        }

        public Component[] LoadComponents(string dirPath)
        {
            // set search method of this (host for components being loaded)
            SearchMethod = new ComponentSearchDirectory(dirPath);

            List<Component> components = new List<Component>();
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            foreach (FileInfo fileInfo in dirInfo.GetFiles())
            {
                Component component = LoadComponent(fileInfo.FullName);
                if (null != component)
                    components.Add(component);
            }
            return components.ToArray();
        }
        #endregion

        #region Cache handling
        private static Component GetFromCache(Guid guid)
        {
            foreach (Component comp in _componentCache)
            {
                if (comp.Guid == guid)
                {
                    _componentCache.Remove(comp);
                    _componentCache.Insert(0, comp);
                    return comp;
                }
            }
            return null;
        }
        private static void InsertInCache(Component comp)
        {
            // verify if not already available
            if (null != GetFromCache(comp.Guid))
                return;
            // actually insert component
            _componentCache.Insert(0, comp);
            // clear exceding components
            while (_componentCache.Count > _cacheSize)
            {
                _parameterStackCache.Remove(_componentCache[_componentCache.Count - 1].Guid);
                _componentCache.RemoveAt(_componentCache.Count - 1);
            }
        }

        private static ParameterStack GetStackFromCache(Guid guid)
        {
            // verify if not already available
            if (!_parameterStackCache.ContainsKey(guid))
                return null;
            return _parameterStackCache[guid];
        }
        private static void InsertParameterStackInCache(Guid guid, ParameterStack stack)
        {
            _parameterStackCache[guid] = stack;
        }
        public static void ClearCache()
        {
            _componentCache.Clear();
            _parameterStackCache.Clear();
        }
        #endregion

        #region Setting SearchMethod
        public IComponentSearchMethod SearchMethod
        {
            set { _searchMethod = value; }
        }
        #endregion

        #region IPluginHost implementation
        public void Feedback(string Feedback, IPlugin Plugin)
        {
        }
        public IPlugin GetPluginByGuid(Guid guid)
        {
            IPlugin searchedPlugin = null;
            // check if component not available in cache
            Component comp = ComponentLoader.GetFromCache(guid);
            if (null != comp) // available from cache
                searchedPlugin = comp.Plugin;
            else // actually load
            {
                if (!(_searchMethod is IComponentSearchMethod))
                    throw new PluginException("Component loader was not initialized with a valid plugin search method.");
                searchedPlugin = LoadComponent(_searchMethod.GetFilePathFromGuid(guid)).Plugin;
            }
            if (null != searchedPlugin && null != OnPluginAccessed)
                OnPluginAccessed(searchedPlugin.Guid);
            return searchedPlugin;
        }
        public IPlugin GetPluginByGuid(string sGuid)
        {
            return GetPluginByGuid(new Guid(sGuid));
        }

        public ParameterStack GetInitializedParameterStack(IPlugin plugin)
        {
            // check if already available in cache
            ParameterStack stack = null;
            stack = ComponentLoader.GetStackFromCache(plugin.Guid);
            if (null == stack)
            {
                if (!(_searchMethod is IComponentSearchMethod))
                    throw new PluginException("Component loader was not initialized with a valid plugin search method.");

                IPluginExt1 pluginExt1 = plugin as IPluginExt1;
                IPluginExt2 pluginExt2 = plugin as IPluginExt2;
                IPluginExt3 pluginExt3 = plugin as IPluginExt3;

                // get parameter stack
                if (null != pluginExt1)
                    stack = plugin.Parameters;
                else if (null != pluginExt2)
                    stack = pluginExt2.BuildParameterStack(null);
                else if (null != pluginExt3)
                    stack = pluginExt3.BuildParameterStack(null);
                // check parameter stack
                if (null == stack)
                    throw new PluginException("Failed to build initial parameter stack.");

                // load parameter values from plugins
                foreach (Parameter param in stack)
                {
                    try
                    {
                        ParameterDouble pd = param as ParameterDouble;
                        if (null != pd) stack.SetDoubleParameter(pd.Name, _searchMethod.GetDoubleParameterDefaultValue(plugin.Guid, pd.Name));
                        ParameterBool pb = param as ParameterBool;
                        if (null != pb) stack.SetBoolParameter(pb.Name, _searchMethod.GetBoolParameterDefaultValue(plugin.Guid, pb.Name));
                        ParameterInt pi = param as ParameterInt;
                        if (null != pi) stack.SetIntParameter(pi.Name, _searchMethod.GetIntParameterDefaultValue(plugin.Guid, pi.Name));
                        ParameterMulti pm = param as ParameterMulti;
                        if (null != pm) stack.SetMultiParameter(pm.Name, _searchMethod.GetMultiParameterDefaultValue(plugin.Guid, pm.Name));
                    }
                    catch (Exception /*ex*/)
                    {
                    }
                }
                // save in cache
                ComponentLoader.InsertParameterStackInCache(plugin.Guid, stack);
            }
            return stack.Clone();
        }
        #endregion

        #region IDisposable members
        public void Dispose()
        {
        }
        #endregion
    }
    #endregion // ComponentLoader
}
