#region Using directives
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Drawing;
using System.Linq;

using Sharp3D.Math.Core;

using Pic.Factory2D;
#endregion

namespace Pic.Plugin
{
    #region Exceptions
    #region PluginException
    public class PluginException : System.Exception
    {
        #region Constructors
        public PluginException()
            :base()
        { 
        }
        public PluginException(string message)
            : base(message)
        {
        }
        public PluginException(string message, Exception innerException)
            :base(message, innerException)
        { 
        }
        #endregion
    }
    #endregion

    #region ParameterException
    public class ParameterNotFound : PluginException
    {
        public ParameterNotFound(string parameterName)
            : base(string.Format("{0} does not exist.", parameterName))
        {
        }
    }
    #endregion

    #region ParameterNoValidValue
    public class ParameterNoValidValue : PluginException
    {
        public ParameterNoValidValue(string parameterName, double minValue, double maxValue)
            : base(string.Format("Parameter {0} has invalid min/max values ({1}/{2})", parameterName, minValue, maxValue))
        {
        }
    }
    #endregion
    #region ParameterInvalidType
    public class ParameterInvalidType : PluginException
    {
        public ParameterInvalidType(string parameterName, string type)
            : base(string.Format("Parameter {0} exists but has not {1} type", parameterName, type))
        {            
        }
    }
    #endregion
    #endregion

    #region Parameter classes
    [Serializable]
    [XmlInclude(typeof(Parameter))]
    public abstract class Parameter
	{
		#region Private fields
		private string _name;
        private string _description;
        private Parameter _parent;
        private static int _offset = 10;
		#endregion

        #region Constructors
        public Parameter()
        { 
        }
        public Parameter(string name, string description)
        {
            _name = name;
            _description = description;
        }
        public Parameter Parent
        {
            set { _parent = value; }
        }
        public int IndentValue
        {
            get
            {
                if (null == _parent) return 0;
                else return _parent.IndentValue + _offset;
            }
        }
        #endregion

        #region Public properties
        /// <summary>
        /// parameter name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// parameter description
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
		}
        /// <summary>
        /// check if parameter value is valid
        /// </summary>
        abstract public bool IsValid { get; }
        /// <summary>
        /// a parameter is a majoration if:
        /// - its name starts with "m", 
        /// - next characters can be parsed as an integer
        /// </summary>
        public bool IsMajoration
        {
            get
            {
                int iIndex = 0;
                return (Name.StartsWith("m") && Int32.TryParse(Name.Substring(1), out iIndex));
            }
        }
        /// <summary>
        /// copy property generate a clone
        /// </summary>
        public abstract Parameter Clone();
		#endregion
	}

    [Serializable]
    [XmlInclude(typeof(ParameterDouble))]
    public class ParameterDouble : Parameter
	{
		#region Private fields
		private double _valueDefault;
        private double _valueMin;
        private double _valueMax;
        private bool _hasValueMin;
        private bool _hasValueMax;
        private double _value;
		#endregion

		#region Constructor
		public ParameterDouble()
		{
			_valueDefault = 0;
			_value = 0;
			_valueMin = Double.MinValue;
			_valueMax = Double.MaxValue;
		}
        public ParameterDouble(string name, string description, bool hasValueMin, double valueMin, bool hasValueMax, double valueMax, double valueDefault)
            : base(name, description)
        {
            _hasValueMin = hasValueMin;
            _valueMin = valueMin;
            _hasValueMax = hasValueMax;
            _valueMax = valueMax;
            _valueDefault = valueDefault;
            _value = valueDefault;
        }

		#endregion

		#region Public Properties
		public double ValueDefault
        {
            get { return _valueDefault; }
            set { _valueDefault = value; }
        }

        public double ValueMin
        {
            get { return _valueMin; }
            set { _hasValueMin = true; _valueMin = value; }
        }

        public double ValueMax
        {
            get { return _valueMax; }
            set { _hasValueMax = true; _valueMax = value; }
        }

        public double Value
        {
            get
            {
                if (_hasValueMin && _value < _valueMin) return _valueMin;
                if (_hasValueMax && _value > _valueMax) return _valueMax;
                return _value; 
            }
            set
            {
                _value = value;
                if (_hasValueMin && _value < _valueMin) _value = _valueMin;
                if (_hasValueMax && _value > _valueMax) _value = _valueMax;
            }
		}

        public bool HasValueMin
        {
            get { return _hasValueMin; }
            set { _hasValueMin = value; }
        }

        public bool HasValueMax
        {
            get { return _hasValueMax; }
            set { _hasValueMax = value; }
        }

        public override bool IsValid
        {
            get { return (!_hasValueMin || _valueMin <= _value) && (!_hasValueMax && _value <= _valueMax); }
        }
        #endregion

        #region Override parameter
        public override Parameter Clone()
        {
            ParameterDouble param =  new ParameterDouble(Name, Description, _hasValueMin, _valueMin, _hasValueMax, _valueMax, _valueDefault);
            param._value = _value;
            return param;
        }
        #endregion

	}

    [Serializable]
    [XmlInclude(typeof(ParameterInt))]
    public class ParameterInt : Parameter
    {
        #region Private fields
        private int _valueDefault;
        private int _value;
        private int _valueMin;
        private bool _hasValueMin;
        private int _valueMax;
        private bool _hasValueMax;
        #endregion

        #region Constructor
        public ParameterInt()
        {
            _valueDefault = 0;
            _hasValueMin = false;
            _valueMin = Int32.MinValue;
            _hasValueMax = false;
            _valueMax = Int32.MaxValue;
            _value = 0;
        }
        public ParameterInt(string name, string description, bool hasValueMin, int valueMin, bool hasValueMax, int valueMax, int valueDefault)
            : base(name, description)
        {
            _hasValueMin = hasValueMin;
            _valueMin = valueMin;
            _hasValueMax = hasValueMax;
            _valueMax = valueMax;
            _valueDefault = valueDefault;
            _value = valueDefault;
        }
        #endregion

		#region Public Properties
		public int ValueDefault
        {
            get { return _valueDefault; }
            set { _valueDefault = value; }
        }
        public bool HasValueMin
        {
            get { return _hasValueMin; }
        }
        public int ValueMin
        {
            get { return _valueMin; }
            set { _hasValueMin = true; _valueMin = value; }
        }
        public bool HasValueMax
        {
            get { return _hasValueMax; }
        }
        public int ValueMax
        {
            get { return _valueMax; }
            set { _hasValueMax = true; _valueMax = value; }
        }
        public int Value
        {
            get { return _value; }
            set { _value = value; }
		}
        public override bool IsValid
        {
            get { return (!_hasValueMin || _valueMin <= _value) && (!_hasValueMax && _value <= _valueMax); }
        }
		#endregion

        #region Override parameter
        public override Parameter Clone()
        {
            return new ParameterInt(Name, Description, _hasValueMin, _valueMin, _hasValueMax, _valueMax, _value);
        }
        #endregion
    }

    [Serializable]
    [XmlInclude(typeof(ParameterBool))]
    public class ParameterBool : Parameter
	{
		#region Private fields
		bool _value;
		bool _valueDefault;
		#endregion

		#region Constructors
		public ParameterBool()
		{
			_value = false;
			_valueDefault = false;
		}
		public ParameterBool(string name, string description, bool bValue)
            : base(name, description)
		{
			_value = bValue;
            _valueDefault = bValue;
		}
		#endregion

		#region Public properties
		public bool Value
		{
			get { return _value; }
			set { _value = value; }
		}
		public bool ValueDefault
		{
			get { return _valueDefault; }
			set { _valueDefault = value;}
		}
        public override bool IsValid
        {
            get { return true; }
        }
		#endregion

        #region Override parameter
        public override Parameter Clone()
        {
            return new ParameterBool(Name, Description, _value);
        }
        #endregion
	}
    [Serializable]
    [XmlInclude(typeof(ParameterString))]
    public class ParameterString : Parameter
    {
        #region Private fields
        private string _defaultValue;
        private string _value;
        private bool _allowEmpty;
        #endregion

        #region Constructors
        public ParameterString()
        {
            _allowEmpty = true;
        }
        public ParameterString(string name, string description, string s)
            :base(name, description)
        {
            _value = s;
            _defaultValue = s;
        }
        #endregion

        #region Public properties
        public string DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public override bool IsValid
        {
            get { return _allowEmpty || _value.Length > 0; }
        }
        #endregion

        #region Override parameter
        public override Parameter Clone()
        {
            return new ParameterString(Name, Description, _value);
        }
        #endregion
    }
    [Serializable]
    [XmlInclude(typeof(ParameterMulti))]
    public class ParameterMulti : Parameter
    {
        #region Private fields
        List<string> _valueList = new List<string>();
        List<string> _displayList = new List<string>();
        int _value;
        #endregion

        #region Constructors
        public ParameterMulti(string name, string description, string[] displayList, string[] valueList, int defaultValue)
            :base(name,description)
        {
            _displayList.AddRange(displayList);
            _valueList.AddRange(valueList);
            _value = defaultValue;
        }
        #endregion

        #region Public properties
        public int Value
        {
            get { return _value; }
            set
            {
                if (value >= 0 && value < _valueList.Count)
                    _value = value;
                else
                    throw new PluginException(string.Format("Value {0} is out of range (0, {1})", value, _valueList.Count - 1 ));
            }
        }
        public override bool IsValid
        {
            get { return null != _valueList && 0 <= _value && _value < _valueList.Count; }
        }
        public string[] ValueList
        {
            get { return _valueList.ToArray(); }
        }
        public string[] DisplayList
        {
            get { return _displayList.ToArray(); }
        }
        #endregion
        #region Override parameter
        public override Parameter Clone()
        {
            return new ParameterMulti(Name, Description, _displayList.ToArray(), _valueList.ToArray(), _value);
        }
        #endregion
    }
    #endregion

    #region ParameterStack class
    [Serializable]
    [XmlInclude(typeof(ParameterDouble))]
    [XmlInclude(typeof(ParameterInt))]
    [XmlInclude(typeof(ParameterBool))]
    [XmlInclude(typeof(ParameterString))]
    [XmlInclude(typeof(ParameterStack))]
    public class ParameterStack: IEnumerable
    {
		#region Private fields
		private List<Parameter> _parameterList;
        private int _version;
		#endregion

		#region Constructor
		public ParameterStack()
        {
            _parameterList = new List<Parameter>();
            _version = 1;
		}
		#endregion

        #region Clear
        public void Clear()
        {
            _parameterList.Clear();
        }
        #endregion

        #region Public properties
        public List<Parameter> ParameterList
        {
            get { return _parameterList; }
            set { _parameterList = value; }
        }
        public int Count
        {
            get
            {
                if (null != _parameterList)
                    return _parameterList.Count;
                else
                    return 0;
            }
        }
        public ParameterStack Clone()
        {
            ParameterStack stackCopy = new ParameterStack();
            foreach (Parameter p in _parameterList)
                stackCopy.AddParameter(p.Clone());
            return stackCopy;
        }
        #endregion

        #region Adding parameter
        public void AddParameter(Parameter p)
        {
            _parameterList.Add(p);
        }
        public void AddDoubleParameter(string name, string description, double valueDefaut, double valueMin, double valueMax)
        {
            AddDoubleParameter(name, description, valueDefaut, true, valueMin, true, valueMax);
        }

        public void AddDoubleParameter(string name, string description, double valueDefault, double valueMin)
        {
            AddDoubleParameter(name, description, valueDefault, true, valueMin, false, Double.MaxValue);
        }

        public void AddDoubleParameter(string name, string description, double valueDefault)
        {
            AddDoubleParameter(name, description, valueDefault, false, Double.MinValue, false, Double.MaxValue);
        }

        public void AddDoubleParameter(string name, string description, double valueDefault, bool hasMinValue, double valueMin, bool hasMaxValue, double valueMax)
        {
            ParameterDouble param = new ParameterDouble();
            param.Name              = name;
            param.Description       = description;
            param.ValueDefault      = valueDefault;
            param.Value             = valueDefault;
            if (hasMinValue)
                param.ValueMin      = valueMin;
            if (hasMaxValue)
                param.ValueMax      = valueMax;
            _parameterList.Add(param);
		}

        public void AddIntParameter(string name, string description, int valueDefault, bool hasMinValue, int valueMin, bool hasMaxValue, int valueMax)
        {
            ParameterInt param = new ParameterInt();
            param.Name = name;
            param.Description = description;
            param.ValueDefault = valueDefault;
            param.Value = valueDefault;
            if (hasMinValue)
                param.ValueMin = valueMin;
            if (hasMaxValue)
                param.ValueMax = valueMax;
            _parameterList.Add(param);
        }

		public void AddBoolParameter(string name, string description, bool valueDefault)
		{
            _parameterList.Add(new ParameterBool(name, description, valueDefault));
		}

        public void AddMultiParameter(string name, string description, string[] displayList, string[] valueList, int value)
        {
            _parameterList.Add(new ParameterMulti(name, description, displayList, valueList, value));
        }
		#endregion

		#region Setting parameters
        public bool HasParameter(string name)
        {
            Parameter param = _parameterList.Find(x => x.Name == name);
            return (null != param); 
        }
        public Parameter GetParameterByName(string name)
        {
            Parameter param = _parameterList.Find(x => x.Name == name);
            if (null == param) throw new ParameterNotFound(name);
            return param;
        }
		public void SetDoubleParameter(string name, double value)
        {
            ParameterDouble param = GetParameterByName(name) as ParameterDouble;
            if (null == param) throw new ParameterInvalidType(name, "ParameterDouble");
            param.Value = value;
		}
        public void SetIntParameter(string name, int value)
        {
             ParameterInt param = GetParameterByName(name) as ParameterInt;
             if (null == param) throw new ParameterInvalidType(name, "ParameterInt");
             param.Value = value;
        }
		public void SetBoolParameter(string name, bool value)
		{
            ParameterBool param = GetParameterByName(name) as ParameterBool;
            if (null == param) throw new ParameterInvalidType(name, "ParameterBool");
            param.Value = value;
		}
        public void SetIntegerParameter(string name, int value)
        {
            ParameterInt param = GetParameterByName(name) as ParameterInt;
            if (null == param) throw new ParameterInvalidType(name, "ParameterInt");
            param.Value = value;
        }
        public void SetMultiParameter(string name, string value)
        {
            ParameterMulti param = GetParameterByName(name) as ParameterMulti;
            if (null == param) throw new ParameterInvalidType(name, "ParameterMulti");
            for (int index = 0; index < param.ValueList.Length; ++index)
            {
                if (0 == string.Compare(param.ValueList[index], value))
                {
                    param.Value = index;
                    return;
                }
            }
            string sMessage = string.Format("Could not find '{0}' among the available values : ", value);
            foreach (string s in param.ValueList)
                sMessage += s + " ";
            throw new PluginException(sMessage);
        }
        public void SetMultiParameter(string name, int index)
        {
            ParameterMulti param = GetParameterByName(name) as ParameterMulti;
            if (null == param) throw new ParameterInvalidType(name, "ParameterMulti");
            param.Value = index;
        }
        /// <summary>
        /// checks if parameter stack contains some majorations
        /// i.e. parameter with name equal to m[1..16]
        /// </summary>
        public bool HasMajorations
        {
            get
            {
                foreach (Parameter p in _parameterList)
                    if (p.IsMajoration) return true;
                return false;
            }
        }

        public int MajorationCount
        {
            get
            {
                int majoCount = 0;
                foreach (Parameter p in _parameterList)
                    majoCount += p.IsMajoration ? 1 : 0;
                return majoCount;
            }
        }
		#endregion

		#region Accessing parameters
		public double GetDoubleParameterValue(string name)
        {
            ParameterDouble param = GetParameterByName(name) as ParameterDouble;
            if (null == param) throw new ParameterInvalidType(name, "ParameterDouble");
            return param.Value;
        }

        public int GetIntParameterValue(string name)
        {
            ParameterInt param = GetParameterByName(name) as ParameterInt;
            if (null == param) throw new ParameterInvalidType(name, "ParameterInt");
            return param.Value;
        }

        public bool GetBoolParameterValue(string name)
        {
            ParameterBool param = GetParameterByName(name) as ParameterBool;
            if (null == param) throw new ParameterInvalidType(name, "ParameterBool");
            return param.Value;
        }

        public int GetMultiParameterValue(string name)
        {
            ParameterMulti param = GetParameterByName(name) as ParameterMulti;
            if (null == param) throw new ParameterInvalidType(name, "ParameterMulti");
            return param.Value;
        }
		#endregion

        #region IEnumerable overrides
        public IEnumerator GetEnumerator() {  return new Enumerator(this); }
        #endregion

        #region Enumerator class
        private class Enumerator : IEnumerator
        {

            public Enumerator(ParameterStack obj)
            {
                oThis = obj;
                cursor = -1;
                version = oThis._version;
            }

            public bool MoveNext()
            {
                ++cursor;
                if (cursor > (oThis._parameterList.Count - 1))
                {
                    return false;
                }
                return true;
            }
            public void Reset()
            {
                cursor = -1;
            }

            public object Current
            {
                get
                {
                    if (oThis._version != version)
                    {
                        throw new InvalidOperationException("Collection was modified");
                    }
                    if (cursor > (oThis._parameterList.Count - 1))
                    {
                        throw new InvalidOperationException("Enumeration already finished");
                    }
                    if (cursor == -1)
                    {
                        throw new InvalidOperationException("Enumeration not started");
                    }
                    return oThis._parameterList[cursor];
                }
            }

            private int version;
            private int cursor;
            private ParameterStack oThis;
        }
        #endregion
    }
	#endregion

    #region ParameterStackUpdater
    public class ParameterStackUpdater
    {
        #region Constructor
        public ParameterStackUpdater(ParameterStack stackIn)
        {
            _stackIn = null != stackIn ? stackIn : new ParameterStack();
            _stackOut = new ParameterStack();
        }
        #endregion

        #region Creating methods
        public ParameterDouble CreateDoubleParameter(string name, string description, double valueDefault, bool hasMinValue, double valueMin, bool hasMaxValue, double valueMax)
        {
            _stackOut.AddDoubleParameter(name, description
                , _stackIn.HasParameter(name) ? _stackIn.GetDoubleParameterValue(name) : valueDefault
                , hasMinValue, valueMin, hasMaxValue, valueMax);
            return _stackOut.GetParameterByName(name) as ParameterDouble;
        }
        public ParameterDouble CreateDoubleParameter(string name, string description, double valueDefault)
        {   return CreateDoubleParameter(name, description, valueDefault, false, double.MinValue, false, double.MaxValue);  }
        public ParameterDouble CreateDoubleParameter(string name, string description, double valueDefault, double minValue)
        {   return CreateDoubleParameter(name, description, valueDefault, true, minValue, false, double.MaxValue);  }
        public ParameterDouble CreateDoubleParameter(string name, string description, double valueDefault, double minValue, double maxValue)
        {   return CreateDoubleParameter(name, description, valueDefault, true, minValue, true, maxValue);  }
        public ParameterInt CreateIntParameter(string name, string description, int valueDefault, bool hasMinValue, int minValue, bool hasMaxValue, int maxValue)
        {
            _stackOut.AddIntParameter(name, description
                , _stackIn.HasParameter(name) ? _stackIn.GetIntParameterValue(name) : valueDefault
                , hasMinValue, minValue, hasMaxValue, maxValue);
            return _stackOut.GetParameterByName(name) as ParameterInt;
        }
        public ParameterBool CreateBoolParameter(string name, string description, bool valueDefault)
        {
            _stackOut.AddBoolParameter(name, description
                , _stackIn.HasParameter(name) ? _stackIn.GetBoolParameterValue(name) : valueDefault);
            return _stackOut.GetParameterByName(name) as ParameterBool;
        }
        public ParameterMulti CreateMultiParameter(string name, string description, string[] displayList, string[] valueList, int valueDefault)
        {
            _stackOut.AddMultiParameter(name, description, displayList, valueList
                , _stackIn.HasParameter(name) ? _stackIn.GetMultiParameterValue(name) : valueDefault); 
            return _stackOut.GetParameterByName(name) as ParameterMulti;
        }
        #endregion

        #region Accessing parameters
        public double GetDoubleParameterValue(string name)
        {   return _stackOut.GetDoubleParameterValue(name);  }
        public int GetIntParameterValue(string name)
        {   return _stackOut.GetIntParameterValue(name);    }
        public bool GetBoolParameterValue(string name)
        {   return _stackOut.GetBoolParameterValue(name);   }
        public int GetMultiParameterValue(string name)
        {   return _stackOut.GetMultiParameterValue(name);  }
        #endregion

        #region Retrieve updated stack
        /// <summary>
        /// return the updated stack
        /// </summary>
        public ParameterStack UpdatedStack
        {
            get { return _stackOut; }
        }
        #endregion

        #region Data members
        private ParameterStack _stackIn;
        private ParameterStack _stackOut;
        #endregion
    }
    #endregion

    #region IPlugin common interface
    /// <summary>
    /// http://www.codeproject.com/KB/cs/pluginsincsharp.aspx
    /// </summary>
    public interface IPlugin
    { 
        /// <summary>
        /// Component name
        /// </summary>
        string Name {get;}
        /// <summary>
        /// Component description
        /// </summary>
        string Description {get;}
        /// <summary>
        /// Author / Company
        /// </summary>
        string Author {get;}
        /// <summary>
        /// Plugin version
        /// </summary>
        string Version {get;}
        /// <summary>
        /// Plugin guid used to access it (unique name)
        /// </summary>
        Guid Guid { get;}    
        /// <summary>
        /// List of parameters with their default values
        /// </summary>
        ParameterStack Parameters {get;}
        /// <summary>
        /// Create factory entities
        /// </summary>
        /// <param name="factory">Factory in which entities should be created</param>
        /// <param name="stack">List of parameters with their associated values</param>
        void CreateFactoryEntities(PicFactory factory, ParameterStack stack, Transform2D transform);
        /// <summary>
        /// Method called before generating entities
        /// </summary>
        void Initialize();
        /// <summary>
        /// Method called by destructor
        /// </summary>
        void Dispose();
        /// <summary>
        /// IPluginHost accessor
        /// </summary>
        IPluginHost Host { get;set;}
    }
    #endregion

    #region IPluginExt1 (additionnal interface version1) --> Deprecated
    public interface IPluginExt1
    {
        /// <summary>
        /// source code used to build component
        /// </summary>
        string SourceCode {get;}
        /// <summary>
        /// return true if file has an embedded bitmap
        /// </summary>
        bool HasEmbeddedThumbnail {get;}
        /// <summary>
        /// embedded bitmap
        /// </summary>
        Bitmap Thumbnail { get;}
        /// <summary>
        /// Method called by destructor
        /// </summary>
        void Dispose();
        /// <summary>
        /// recommended X/Y offsets for imposition
        /// </summary>
        double ImpositionOffsetX(ParameterStack stack);
        double ImpositionOffsetY(ParameterStack stack);
    }
    #endregion

    #region IPluginExt2 (additionnal interface version2) --> Comes as a replacement of IPluginExt1
    public interface IPluginExt2
    {
        #region Methods available in IPluginExt1
        /// <summary>
        /// source code used to build component
        /// </summary>
        string SourceCode { get; }
        /// <summary>
        /// return true if file has an embedded bitmap
        /// </summary>
        bool HasEmbeddedThumbnail { get; }
        /// <summary>
        /// embedded bitmap
        /// </summary>
        Bitmap Thumbnail { get; }
        /// <summary>
        /// Method called by destructor
        /// </summary>
        void Dispose();
        /// <summary>
        /// recommended X/Y offsets for imposition
        /// </summary>
        double ImpositionOffsetX(ParameterStack stack);
        double ImpositionOffsetY(ParameterStack stack);
        #endregion

        #region Additional methods
        /// <summary>
        /// Build / rebuild parameter stack 
        /// </summary>
        ParameterStack BuildParameterStack(ParameterStack stackIn);
        /// <summary>
        /// Is supporting palletization ?
        /// </summary>
        bool IsSupportingPalletization { get; }
        /// <summary>
        /// Outer dimensions
        /// Method should only be called if component supports palletization
        /// </summary>
        void OuterDimensions(ParameterStack stack, out double[] dimensions);
        /// <summary>
        /// Returns case type to be used for ECT computation 
        /// </summary>
        int CaseType { get; }
        /// <summary>
        /// Is supporting automatic folding
        /// </summary>
        bool IsSupportingAutomaticFolding { get; }
        /// <summary>
        /// Reference point that defines anchored face
        /// </summary>
        List<Vector2D> ReferencePoints(ParameterStack stack);
        #endregion
    }
    #endregion

    #region IPluginExt3 (additional interface version3) --> Comes as a replacement of IPluginExt3
    public interface IPluginExt3
    {
        #region Methods available in IPluginExt1
        /// <summary>
        /// source code used to build component
        /// </summary>
        string SourceCode { get; }
        /// <summary>
        /// return true if file has an embedded bitmap
        /// </summary>
        bool HasEmbeddedThumbnail { get; }
        /// <summary>
        /// embedded bitmap
        /// </summary>
        Bitmap Thumbnail { get; }
        /// <summary>
        /// Method called by destructor
        /// </summary>
        void Dispose();
        /// <summary>
        /// recommended X/Y offsets for imposition
        /// </summary>
        double ImpositionOffsetX(ParameterStack stack);
        double ImpositionOffsetY(ParameterStack stack);
        #endregion

        #region Methods available in IPluginExt2
        /// <summary>
        /// Build / rebuild parameter stack 
        /// </summary>
        ParameterStack BuildParameterStack(ParameterStack stackIn);
        /// <summary>
        /// Is supporting palletization ?
        /// </summary>
        bool IsSupportingPalletization { get; }
        /// <summary>
        /// Outer dimensions
        /// Method should only be called if component supports palletization
        /// </summary>
        void OuterDimensions(ParameterStack stack, out double[] dimensions);
        /// <summary>
        /// Returns case type to be used for ECT computation 
        /// </summary>
        int CaseType { get; }
        /// <summary>
        /// Is supporting automatic folding
        /// </summary>
        bool IsSupportingAutomaticFolding { get; }
        /// <summary>
        /// Reference point that defines anchored face
        /// </summary>
        List<Vector2D> ReferencePoints(ParameterStack stack);
        #endregion

        #region Additional methods & properties
        /// <summary>
        /// Is supporting bundling
        /// </summary>
        bool IsSupportingFlatPalletization { get; }
        /// <summary>
        /// Flat dimensions
        /// </summary>
        void FlatDimensions(ParameterStack stack, out double[] flatDimensions);
        /// <summary>
        /// Number of parts
        /// </summary>
        int NoParts { get; }
        /// <summary>
        /// Part name
        /// </summary>
        string PartName(int i);
        /// <summary>
        /// Layer name
        /// </summary>
        string LayerName(int i);
        #endregion
    }
    #endregion

    #region IPluginHost
    public interface IPluginHost
    {
        void Feedback(string Feedback, IPlugin Plugin);
        IPlugin GetPluginByGuid(Guid guid);
        IPlugin GetPluginByGuid(string sGuid);
        ParameterStack GetInitializedParameterStack(IPlugin plugin);
    }
    #endregion
}
