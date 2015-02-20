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
        private IPluginExt3 _ext3 = null;
        private List<Guid> _dependancyList = new List<Guid>();
        #endregion

        #region Static data members
        static string _SC1_to_SC3_ = 
@"
/// <summary>
/// Is supporting palletization ?
/// </summary>
public bool IsSupportingPalletization { get { return false; } }
/// <summary>
/// Outer dimensions
/// Method should only be called if component supports palletization
/// </summary>
public void OuterDimensions(ParameterStack stack, out double[] dimensions)
{
    dimensions = new double[3];    
}
/// <summary>
/// Returns case type to be used for ECT computation 
/// </summary>
public int CaseType { get { return 0; } }
/// <summary>
/// Is supporting automatic folding
/// </summary>
public bool IsSupportingAutomaticFolding { get { return false; } }
/// <summary>
/// Reference point that defines anchored face
/// </summary>
public List<Vector2D> ReferencePoints(ParameterStack stack)
{
    List<Vector2D> ltPoints = new List<Vector2D>();
    return ltPoints;
}
///
/// flat palletization
///
public bool IsSupportingFlatPalletization
{   get { return false; }   }
///
/// flat dimensions
///
public void FlatDimensions(ParameterStack stack, out double[] flatDimensions)
{
    flatDimensions = new double[3];
}
/// <summary>
/// Number of parts
/// </summary>
public int NoParts
{   get { return 1; } }
/// <summary>
/// Part name
/// </summary>
public string PartName(int i)
{
    string[] partNames = {""Part0""};
    return partNames[i];
}
/// <summary>
/// Layer name
/// </summary>
public string LayerName(int i)
{
    string[] layerName = {""Layer0""};
    return layerName[i];
}";
        static string _SC2_to_SC3_ =
@"
///
/// flat palletization
///
public bool IsSupportingFlatPalletization
{   get { return false; }   }
///
/// flat dimensions
///
public void FlatDimensions(ParameterStack stack, out double[] flatDimensions)
{
    flatDimensions = new double[3];
}
/// <summary>
/// Number of parts
/// </summary>
public int NoParts
{   get { return 1; } }
/// <summary>
/// Part name
/// </summary>
public string PartName(int i)
{
    string[] partNames = {""Part0""};
    return partNames[i];
}
/// <summary>
/// Layer name
/// </summary>
public string LayerName(int i)
{
    string[] layerName = {""Layer0""};
    return layerName[i];
}";
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
            else if (_instance.GetType().GetInterface("Pic.Plugin.IPluginExt3") != null)
                _ext3 = _instance as IPluginExt3;
            else
                throw new PluginException(string.Format("Failed to load plugin {0}\nOne of the following interface is expected: \n{1}"
                    , _instance.Name
                    , "Pic.Plugin.IPluginExt1\nPic.Plugin.IPluginExt2\nPic.Plugin.IPluginExt3"));
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
                    return UpgradeSC1(_ext1.SourceCode);
                else if (null != _ext2)
                    return UpgradeSC2(_ext2.SourceCode);
                else if (null != _ext3)
                    return _ext3.SourceCode;
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
                else if (null != _ext3)
                    return _ext3.HasEmbeddedThumbnail;
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
                else if (null != _ext3)
                    return _ext3.Thumbnail;
                else
                    return null;
            }
        }
        public bool IsSupportingAutomaticFolding
        {
            get
            {
                if (null != _ext1)
                    return false;
                else if (null != _ext2)
                    return _ext2.IsSupportingAutomaticFolding;
                else if (null != _ext3)
                    return _ext3.IsSupportingAutomaticFolding;
                else
                    return false;
            }
        }

        public Sharp3D.Math.Core.Vector2D ReferencePoint(ParameterStack stack)
        {
            List<Vector2D> l = null;
            if (null != _ext2)
                l = _ext2.ReferencePoints(stack);
            else if (null != _ext3)
                l = _ext3.ReferencePoints(stack);
            if (null == l) return Vector2D.Zero;
            else if (l.Count > 0) return l[0];
            else return Vector2D.Zero;
        }

        public Vector2D ImpositionOffset(ParameterStack stack)
        {
            if (null != _ext1)
                return new Vector2D(_ext1.ImpositionOffsetX(stack), _ext1.ImpositionOffsetY(stack));
            else if (null != _ext2)
                return new Vector2D(_ext2.ImpositionOffsetX(stack), _ext2.ImpositionOffsetY(stack));
            else if (null != _ext3)
                return new Vector2D(_ext3.ImpositionOffsetX(stack), _ext3.ImpositionOffsetY(stack));
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
            }
            else if (null != _ext3)
            {
                if (_ext3.IsSupportingPalletization)
                {
                    double[] dim;
                    _ext3.OuterDimensions(stack, out dim);
                    length = dim[0]; width = dim[1]; height = dim[2];
                    return true;
                }
            }
            return false; 
        }

        public bool GetFlatDimensions(ParameterStack stack, ref double length, ref double width, ref double height)
        {
            if (null != _ext3 && _ext3.IsSupportingFlatPalletization)
            {
                double[] dim;
                _ext3.FlatDimensions(stack, out dim);
                length = dim[0]; width = dim[1]; height = dim[2];
                return true;
            }
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
            else  if (null != _ext3)
                stackOut = _ext3.BuildParameterStack(stackIn);
            else
                stackOut = new ParameterStack();


            // adding thickness parameters 
            // these parameters are always available but are not shown in the list of parameters in the plugin viewer
            // ep1 and th1 are reserved name
            double defaultThickness = Pic.Plugin.Host.Properties.Settings.Default.Thickness;
            if (!stackOut.HasParameter("ep1"))
                stackOut.AddDoubleParameter("ep1", "Epaisseur"
                    , null != stackIn && stackIn.HasParameter("ep1") ? stackIn.GetDoubleParameterValue("ep1") : defaultThickness);
            if (!stackOut.HasParameter("th1"))
                stackOut.AddDoubleParameter("th1", "Thickness"
                    , null != stackIn && stackIn.HasParameter("th1") ? stackIn.GetDoubleParameterValue("th1") : defaultThickness);

            return stackOut;
        }
        public ParameterStack GetParameters(IComponentSearchMethod compSearchMethod)
        {
            ParameterStack parameters = _instance.Parameters;
            if (null != compSearchMethod)
            {
                foreach (Parameter p in parameters)
                {
                    try
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
                    catch (Exception /*ex*/)
                    {
                    }
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

        #region Private helper methods
        private string UpgradeSC1(string sourceCode)
        {
            sourceCode = MoveSemiColumnsToPrevLine(sourceCode.Trim());

            List<string> sList = new List<string>();
            using (System.IO.StringReader reader = new System.IO.StringReader(sourceCode))
            {
                string line = null;
                while (null != (line = reader.ReadLine()))
                {
                    if (line.Contains("public ParameterStack Parameters"))
                    {
                        while (!(line = reader.ReadLine()).Contains("new ParameterStack(")) ;
                        sList.Add(@"public ParameterStack BuildParameterStack(ParameterStack stackIn)" );
                        sList.Add(@"{");
                        sList.Add(@"    ParameterStackUpdater paramUpdater = new ParameterStackUpdater(stackIn);");

                        // change AddDoubleParameter / AddIntParameter / AddBoolParameter / AddMultiParameter
                        while (!(line = reader.ReadLine()).Contains("}"))
                        {
                            if (line.Contains("}")) { /* do nothing */ }
                            else if (line.Contains("AddDoubleParameter("))
                            {
                                string sEndLine = line.Substring(line.IndexOf("(") + 1);
                                sList.Add(@"    paramUpdater.CreateDoubleParameter(" + sEndLine);
                            }
                            else if (line.Contains("AddIntParameter"))
                            {
                                string sEndLine = line.Substring(line.IndexOf("(") + 1);
                                sList.Add(@"    paramUpdater.CreateIntParameter(" + sEndLine);
                            }
                            else if (line.Contains("AddBoolParameter"))
                            {
                                string sEndLine = line.Substring(line.IndexOf("(") + 1);
                                sList.Add(@"    paramUpdater.CreateBoolParameter(" + sEndLine);
                            }
                            else if (line.Contains("AddMultiParameter"))
                            {
                                string sEndLine = line.Substring(line.IndexOf("(") + 1);
                                sList.Add(@"    paramUpdater.CreateMultiParameter(" + sEndLine);
                            }
                        }
                        sList.Add("    return paramUpdater.UpdatedStack;");
                    }
                    else
                        sList.Add(line);
                }
            }
            // rebuild string
            System.IO.StringWriter sw = new StringWriter();
            foreach (string s in sList)
                sw.WriteLine(s);
            sw.Write(_SC1_to_SC3_); // additional features
            return sw.ToString();
        }

        private string UpgradeSC2(string sourceCode)
        {
            sourceCode = MoveSemiColumnsToPrevLine(sourceCode.Trim());
            System.IO.StringWriter sw = new StringWriter();
            sw.Write(sourceCode);
            sw.Write(_SC2_to_SC3_); // additional features
            return sw.ToString();
        }

        private string MoveSemiColumnsToPrevLine(string sourceCode)
        {
            List<string> sList = new List<string>();
            using (System.IO.StringReader reader = new System.IO.StringReader(sourceCode))
            {
                string line = null;
                while (null != (line = reader.ReadLine()))
                {
                    string trimmedLine = line.Trim();
                    if (string.Equals(trimmedLine, ";"))
                        sList[sList.Count - 1] = sList[sList.Count - 1] + ";";
                    else
                        sList.Add(line);
                }
            }
            System.IO.StringWriter writer = new StringWriter();
            foreach (string s in sList)
                writer.WriteLine(s);
            return writer.ToString();            
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
