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
    /// <summary>
    /// Wraps a IPlugin + IPluginExt component
    /// </summary>
    public class Component : IDisposable
    {
        #region Data members
        private IPlugin _instance = null;
        private IPluginExt1 _ext1 = null;
        private IPluginExt2 _ext2 = null;
        List<Guid> _dependancyList = new List<Guid>();
        #endregion

        #region Private constructor
        internal Component(IPlugin instance)
        {
            _instance = instance;

            // depending on plugin interface version, an other interface might be available
            if (_instance.GetType().GetInterface("Pic.Plugin.IPluginExt1") != null)
                _ext1 = _instance as IPluginExt1;
            else if (_instance.GetType().GetInterface("Pic.Plugin.IPluginExt2") != null)
                _ext2 = _instance as IPluginExt2;
            else
                throw new PluginException(string.Format("Failed to load plugin {0}\nOne of the following interface is expected: \n{1}"
                    , _instance.Name
                    , "Pic.Plugin.IPluginExt1\nPic.Plugin.IPluginExt2"));
        }
        #endregion

        #region Public properties
        public string Name
        { get { return _instance.Name; } }
        public string Description
        { get { return _instance.Description; } }
        public string Author
        { get { return _instance.Author; } }
        public string Version
        { get { return _instance.Version; } }
        public Guid Guid
        { get { return _instance.Guid; } }
        public string SourceCode
        {
            get
            {
                if (null != _ext1)
                    return _ext1.SourceCode;
                else if (null != _ext2)
                    return _ext2.SourceCode;
                else
                    return string.Empty;
            }
        }
        public bool HasEmbeddedThumbnail
        {
            get
            {
                if (null != _ext1)
                    return _ext1.HasEmbeddedThumbnail;
                else if (null != _ext2)
                    return _ext2.HasEmbeddedThumbnail;
                else
                    return false;
            }
        }
        public Bitmap Thumbnail
        {
            get
            {
                if (null != _ext1)
                    return _ext1.Thumbnail;
                else if (null != _ext2)
                    return _ext2.Thumbnail;
                else
                    return null;
            }
        }
        public Vector2D ImpositionOffset(ParameterStack stack)
        {
            if (null != _ext1)
                return new Vector2D(_ext1.ImpositionOffsetX(stack), _ext1.ImpositionOffsetY(stack));
            else if (null != _ext2)
                return new Vector2D(_ext2.ImpositionOffsetX(stack), _ext2.ImpositionOffsetY(stack));
            else
                return Vector2D.Zero;
        }
 
        /// <summary>
        /// Try to get outer dimensions to use for palletisation
        /// </summary>
        /// <param name="stack">Parameter stack</param>
        /// <param name="length">Length</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <returns>true if a valid set of dimensions could be retrieved</returns>
        public bool GetDimensions(ParameterStack stack, ref double length, ref double width, ref double height)
        {
            if (null != _ext1)
            {
                // search three parameters A/L, B, H 
                bool foundL = false, foundW = false, foundH = false;
                foreach (Pic.Plugin.Parameter param in stack)
                {
                    ParameterDouble paramDouble = param as ParameterDouble;
                    if (null == paramDouble) continue;

                    if (0 == string.Compare(paramDouble.Name.ToLower(), "a") || 0 == string.Compare(paramDouble.Name.ToLower(), "l"))
                    {
                        foundL = true;
                        length = paramDouble.Value;
                    }
                    else if (0 == string.Compare(paramDouble.Name.ToLower(), "b"))
                    {
                        foundW = true;
                        width = paramDouble.Value;
                    }
                    else if (0 == string.Compare(paramDouble.Name.ToLower(), "h"))
                    {
                        foundH = true;
                        height = paramDouble.Value;
                    }
                }
                // to support palletization, all three parameters must be found
                return foundL && foundW && foundH;
            }
            else if (null != _ext2)
            {
                if (_ext2.IsSupportingPalletization)
                {
                    double[] dim;
                    _ext2.OuterDimensions(stack, out dim);
                    length = dim[0]; width = dim[1]; height = dim[2];
                    return true;
                }
                else
                    return false;
            }
            else
                return false; 
        }
        /// <summary>
        /// Internal plugin accessor
        /// </summary>
        internal IPlugin Plugin
        {
            get { return _instance; }
        }
        /// <summary>
        /// Build parameter stack
		/// IPluginExt1 simply returns a static list of parameters or the same current list
		/// IpluginExt2 builds a new list depending on the current list state
        /// </summary>
        public ParameterStack BuildParameterStack(ParameterStack stackIn)
        {
            ParameterStack stackOut = null;
            if (null != _ext1)
			{
                if (null != stackIn)
                    stackOut = stackIn;
                else
                    stackOut = _instance.Parameters;
			}
            else if (null != _ext2)
                stackOut = _ext2.BuildParameterStack(stackIn);
            else
                stackOut = new ParameterStack();

            // adding thickness parameters 
            // these parameters are always available but are not shown in the list of parameters in the plugin viewer
            // ep1 and th1 are reserved name
            if (!stackOut.HasParameter("ep1"))   stackOut.AddDoubleParameter("ep1", "Epaisseur", 1.0);
            if (!stackOut.HasParameter("th1"))   stackOut.AddDoubleParameter("th1", "Thickness", 1.0);

            return stackOut;
        }
        public ParameterStack GetParameters(IComponentSearchMethod compSearchMethod)
        {
            ParameterStack parameters = _instance.Parameters;
            if (null != compSearchMethod)
            {
                foreach (Parameter p in parameters)
                {
                    ParameterDouble pd = p as ParameterDouble;
                    if (null != pd) parameters.SetDoubleParameter(pd.Name, compSearchMethod.GetDoubleParameterDefaultValue(Guid, pd.Name));
                    ParameterBool pb = p as ParameterBool;
                    if (null != pb) parameters.SetBoolParameter(pb.Name, compSearchMethod.GetBoolParameterDefaultValue(Guid, pb.Name));
                    ParameterInt pi = p as ParameterInt;
                    if (null != pi) parameters.SetIntegerParameter(pb.Name, compSearchMethod.GetIntParameterDefaultValue(Guid, pi.Name));
                    ParameterMulti pm = p as ParameterMulti;
                    if (null != pm) parameters.SetMultiParameter(pm.Name, 0);
                }
            }
            return parameters;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Creates factory entities (mostly segment, arcs, cotations...) in <see cref="PicFactory"/> class instance passed as first argument
        /// </summary>
        /// <param name="factory">Instance of class <see cref="PicFactory"/> to be populated with entities</param>
        /// <param name="stack">Array of <see cref="Paramerter"/> to apply while generating entities</param>
        public void CreateFactoryEntities(PicFactory factory, ParameterStack stack)
        {
            _instance.CreateFactoryEntities(factory, stack, Transform2D.Identity);
        }

        public List<Guid> GetDependancies(ParameterStack stack)
        {
            // clear list of dependancies
            _dependancyList.Clear();
            // instanciate handler of ComponentLoader.OnPluginAccessed
            ComponentLoader.PluginAccessed handler = new ComponentLoader.PluginAccessed(OnGuidLoaded);
            ComponentLoader.OnPluginAccessed += handler;
            // instantiate factory
            PicFactory factory = new PicFactory();
            _instance.CreateFactoryEntities(factory, stack, Transform2D.Identity);
            // on plugin accessed
            ComponentLoader.OnPluginAccessed -= handler; 
            // dependancy list
            return _dependancyList;
        }

        public void OnGuidLoaded(Guid pluginGuid)
        {
            if (!_dependancyList.Exists(g => g == pluginGuid))
                _dependancyList.Add(pluginGuid);
        }
        #endregion

        #region IDisposable members
        /// <summary>
        /// Implements IDisposable Dispose methos
        /// </summary>
        public void Dispose()
        {
            _instance.Dispose();
            _instance = null;

            _ext1.Dispose();
            _ext1 = null;
        }
        #endregion
    }
}
