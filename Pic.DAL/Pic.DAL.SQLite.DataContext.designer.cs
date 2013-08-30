// 
//  ____  _     __  __      _        _ 
// |  _ \| |__ |  \/  | ___| |_ __ _| |
// | | | | '_ \| |\/| |/ _ \ __/ _` | |
// | |_| | |_) | |  | |  __/ || (_| | |
// |____/|_.__/|_|  |_|\___|\__\__,_|_|
//
// Auto-generated from PPDataContext on 2011-06-22 00:52:24Z.
// Please visit http://code.google.com/p/dblinq2007/ for more information.
//
namespace Pic.DAL.SQLite
{
	using System;
	using System.ComponentModel;
	using System.Data;
#if MONO_STRICT
	using System.Data.Linq;
#else   // MONO_STRICT
	using DbLinq.Data.Linq;
	using DbLinq.Vendor;
#endif  // MONO_STRICT
	using System.Data.Linq.Mapping;
	using System.Diagnostics;
	
	
	public partial class PPDataContext : DataContext
	{
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		#endregion
		
		
		public PPDataContext(string connectionString) : 
				base(connectionString)
		{
			this.OnCreated();
		}
		
		public PPDataContext(string connection, MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			this.OnCreated();
		}
		
		public PPDataContext(IDbConnection connection, MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			this.OnCreated();
		}
		
		public Table<CardboardFormat> CardboardFormats
		{
			get
			{
				return this.GetTable<CardboardFormat>();
			}
		}
		
		public Table<CardboardProfile> CardboardProfiles
		{
			get
			{
				return this.GetTable<CardboardProfile>();
			}
		}
		
		public Table<CardboardQuality> CardboardQualities
		{
			get
			{
				return this.GetTable<CardboardQuality>();
			}
		}
		
		public Table<Component> Components
		{
			get
			{
				return this.GetTable<Component>();
			}
		}
		
		public Table<ComponentDependancy> ComponentDependancy
		{
			get
			{
				return this.GetTable<ComponentDependancy>();
			}
		}
		
		public Table<DataVersion> DataVersions
		{
			get
			{
				return this.GetTable<DataVersion>();
			}
		}
		
		public Table<Document> Documents
		{
			get
			{
				return this.GetTable<Document>();
			}
		}
		
		public Table<DocumentType> DocumentTypes
		{
			get
			{
				return this.GetTable<DocumentType>();
			}
		}
		
		public Table<File> Files
		{
			get
			{
				return this.GetTable<File>();
			}
		}
		
		public Table<Majoration> Majoration
		{
			get
			{
				return this.GetTable<Majoration>();
			}
		}
		
		public Table<MajorationSet> MajorationSets
		{
			get
			{
				return this.GetTable<MajorationSet>();
			}
		}
		
		public Table<ParamDefaultValue> ParamDefaultValues
		{
			get
			{
				return this.GetTable<ParamDefaultValue>();
			}
		}
		
		public Table<sysdiagram> sysdiagrams
		{
			get
			{
				return this.GetTable<sysdiagram>();
			}
		}
		
		public Table<Thumbnail> Thumbnail
		{
			get
			{
				return this.GetTable<Thumbnail>();
			}
		}
		
		public Table<TreeNode> TreeNodes
		{
			get
			{
				return this.GetTable<TreeNode>();
			}
		}
		
		public Table<TreeNodeDocument> TreeNodeDocuments
		{
			get
			{
				return this.GetTable<TreeNodeDocument>();
			}
		}
	}
	
	#region Start MONO_STRICT
#if MONO_STRICT

	public partial class PPDataContext
	{
		
		public PPDataContext(IDbConnection connection) : 
				base(connection)
		{
			this.OnCreated();
		}
	}
	#region End MONO_STRICT
	#endregion
#else     // MONO_STRICT
	
	public partial class PPDataContext
	{
		
		public PPDataContext(IDbConnection connection) : 
				base(connection, new DbLinq.Sqlite.SqliteVendor())
		{
			this.OnCreated();
		}
		
		public PPDataContext(IDbConnection connection, IVendor sqlDialect) : 
				base(connection, sqlDialect)
		{
			this.OnCreated();
		}
		
		public PPDataContext(IDbConnection connection, MappingSource mappingSource, IVendor sqlDialect) : 
				base(connection, mappingSource, sqlDialect)
		{
			this.OnCreated();
		}
	}
	#region End Not MONO_STRICT
	#endregion
#endif     // MONO_STRICT
	#endregion
	
	[Table(Name="main.CardboardFormat")]
	public partial class CardboardFormat : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _description;
		
		private int _id;
		
		private float _length;
		
		private string _name;
		
		private float _width;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnDescriptionChanged();
		
		partial void OnDescriptionChanging(string value);
		
		partial void OnIDChanged();
		
		partial void OnIDChanging(int value);
		
		partial void OnLengthChanged();
		
		partial void OnLengthChanging(float value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		
		partial void OnWidthChanged();
		
		partial void OnWidthChanging(float value);
		#endregion
		
		
		public CardboardFormat()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_description", Name="Description", DbType="nvarchar", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (((_description == value) 
							== false))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="ID", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_length", Name="Length", DbType="real", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public float Length
		{
			get
			{
				return this._length;
			}
			set
			{
				if ((_length != value))
				{
					this.OnLengthChanging(value);
					this.SendPropertyChanging();
					this._length = value;
					this.SendPropertyChanged("Length");
					this.OnLengthChanged();
				}
			}
		}
		
		[Column(Storage="_name", Name="Name", DbType="nvarchar(50)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (((_name == value) 
							== false))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_width", Name="Width", DbType="real", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public float Width
		{
			get
			{
				return this._width;
			}
			set
			{
				if ((_width != value))
				{
					this.OnWidthChanging(value);
					this.SendPropertyChanging();
					this._width = value;
					this.SendPropertyChanged("Width");
					this.OnWidthChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="main.CardboardProfile")]
	public partial class CardboardProfile : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _code;
		
		private int _id;
		
		private string _name;
		
		private float _thickness;
		
		private EntitySet<CardboardQuality> _cardboardQualities;
		
		private EntitySet<MajorationSet> _majorationSets;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCodeChanged();
		
		partial void OnCodeChanging(string value);
		
		partial void OnIDChanged();
		
		partial void OnIDChanging(int value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		
		partial void OnThicknessChanged();
		
		partial void OnThicknessChanging(float value);
		#endregion
		
		
		public CardboardProfile()
		{
			_cardboardQualities = new EntitySet<CardboardQuality>(new Action<CardboardQuality>(this.CardboardQualities_Attach), new Action<CardboardQuality>(this.CardboardQualities_Detach));
			_majorationSets = new EntitySet<MajorationSet>(new Action<MajorationSet>(this.MajorationSets_Attach), new Action<MajorationSet>(this.MajorationSets_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_code", Name="Code", DbType="char(10)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Code
		{
			get
			{
				return this._code;
			}
			set
			{
				if (((_code == value) 
							== false))
				{
					this.OnCodeChanging(value);
					this.SendPropertyChanging();
					this._code = value;
					this.SendPropertyChanged("Code");
					this.OnCodeChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="ID", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_name", Name="Name", DbType="nvarchar(50)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (((_name == value) 
							== false))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_thickness", Name="Thickness", DbType="real", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public float Thickness
		{
			get
			{
				return this._thickness;
			}
			set
			{
				if ((_thickness != value))
				{
					this.OnThicknessChanging(value);
					this.SendPropertyChanging();
					this._thickness = value;
					this.SendPropertyChanged("Thickness");
					this.OnThicknessChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_cardboardQualities", OtherKey="CardboardProfileID", ThisKey="ID", Name="fk_CardboardQuality_0")]
		[DebuggerNonUserCode()]
		public EntitySet<CardboardQuality> CardboardQualities
		{
			get
			{
				return this._cardboardQualities;
			}
			set
			{
				this._cardboardQualities = value;
			}
		}
		
		[Association(Storage="_majorationSets", OtherKey="CardboardProfileID", ThisKey="ID", Name="fk_MajorationSet_1")]
		[DebuggerNonUserCode()]
		public EntitySet<MajorationSet> MajorationSets
		{
			get
			{
				return this._majorationSets;
			}
			set
			{
				this._majorationSets = value;
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void CardboardQualities_Attach(CardboardQuality entity)
		{
			this.SendPropertyChanging();
			entity.CardboardProfile = this;
		}
		
		private void CardboardQualities_Detach(CardboardQuality entity)
		{
			this.SendPropertyChanging();
			entity.CardboardProfile = null;
		}
		
		private void MajorationSets_Attach(MajorationSet entity)
		{
			this.SendPropertyChanging();
			entity.CardboardProfile = this;
		}
		
		private void MajorationSets_Detach(MajorationSet entity)
		{
			this.SendPropertyChanging();
			entity.CardboardProfile = null;
		}
		#endregion
	}
	
	[Table(Name="main.CardboardQuality")]
	public partial class CardboardQuality : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _cardboardProfileID;
		
		private string _description;
		
		private float _ect;
		
		private int _id;
		
		private string _name;
		
		private float _rigidityX;
		
		private float _rigidityY;
		
		private float _surfacicMass;
		
		private float _youngModulus;
		
		private EntityRef<CardboardProfile> _cardboardProfile = new EntityRef<CardboardProfile>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCardboardProfileIDChanged();
		
		partial void OnCardboardProfileIDChanging(int value);
		
		partial void OnDescriptionChanged();
		
		partial void OnDescriptionChanging(string value);
		
		partial void OnECTChanged();
		
		partial void OnECTChanging(float value);
		
		partial void OnIDChanged();
		
		partial void OnIDChanging(int value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		
		partial void OnRigidityXChanged();
		
		partial void OnRigidityXChanging(float value);
		
		partial void OnRigidityYChanged();
		
		partial void OnRigidityYChanging(float value);
		
		partial void OnSurfacicMassChanged();
		
		partial void OnSurfacicMassChanging(float value);
		
		partial void OnYoungModulusChanged();
		
		partial void OnYoungModulusChanging(float value);
		#endregion
		
		
		public CardboardQuality()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_cardboardProfileID", Name="CardboardProfileID", DbType="integer", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int CardboardProfileID
		{
			get
			{
				return this._cardboardProfileID;
			}
			set
			{
				if ((_cardboardProfileID != value))
				{
					if (_cardboardProfile.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCardboardProfileIDChanging(value);
					this.SendPropertyChanging();
					this._cardboardProfileID = value;
					this.SendPropertyChanged("CardboardProfileID");
					this.OnCardboardProfileIDChanged();
				}
			}
		}
		
		[Column(Storage="_description", Name="Description", DbType="nvarchar", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (((_description == value) 
							== false))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Column(Storage="_ect", Name="ECT", DbType="real", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public float ECT
		{
			get
			{
				return this._ect;
			}
			set
			{
				if ((_ect != value))
				{
					this.OnECTChanging(value);
					this.SendPropertyChanging();
					this._ect = value;
					this.SendPropertyChanged("ECT");
					this.OnECTChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="ID", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_name", Name="Name", DbType="nvarchar(50)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (((_name == value) 
							== false))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_rigidityX", Name="RigidityX", DbType="real", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public float RigidityX
		{
			get
			{
				return this._rigidityX;
			}
			set
			{
				if ((_rigidityX != value))
				{
					this.OnRigidityXChanging(value);
					this.SendPropertyChanging();
					this._rigidityX = value;
					this.SendPropertyChanged("RigidityX");
					this.OnRigidityXChanged();
				}
			}
		}
		
		[Column(Storage="_rigidityY", Name="RigidityY", DbType="real", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public float RigidityY
		{
			get
			{
				return this._rigidityY;
			}
			set
			{
				if ((_rigidityY != value))
				{
					this.OnRigidityYChanging(value);
					this.SendPropertyChanging();
					this._rigidityY = value;
					this.SendPropertyChanged("RigidityY");
					this.OnRigidityYChanged();
				}
			}
		}
		
		[Column(Storage="_surfacicMass", Name="SurfacicMass", DbType="real", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public float SurfacicMass
		{
			get
			{
				return this._surfacicMass;
			}
			set
			{
				if ((_surfacicMass != value))
				{
					this.OnSurfacicMassChanging(value);
					this.SendPropertyChanging();
					this._surfacicMass = value;
					this.SendPropertyChanged("SurfacicMass");
					this.OnSurfacicMassChanged();
				}
			}
		}
		
		[Column(Storage="_youngModulus", Name="YoungModulus", DbType="real", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public float YoungModulus
		{
			get
			{
				return this._youngModulus;
			}
			set
			{
				if ((_youngModulus != value))
				{
					this.OnYoungModulusChanging(value);
					this.SendPropertyChanging();
					this._youngModulus = value;
					this.SendPropertyChanged("YoungModulus");
					this.OnYoungModulusChanged();
				}
			}
		}
		
		#region Parents
		[Association(Storage="_cardboardProfile", OtherKey="ID", ThisKey="CardboardProfileID", Name="fk_CardboardQuality_0", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public CardboardProfile CardboardProfile
		{
			get
			{
				return this._cardboardProfile.Entity;
			}
			set
			{
				if (((this._cardboardProfile.Entity == value) 
							== false))
				{
					if ((this._cardboardProfile.Entity != null))
					{
						CardboardProfile previousCardboardProfile = this._cardboardProfile.Entity;
						this._cardboardProfile.Entity = null;
						previousCardboardProfile.CardboardQualities.Remove(this);
					}
					this._cardboardProfile.Entity = value;
					if ((value != null))
					{
						value.CardboardQualities.Add(this);
						_cardboardProfileID = value.ID;
					}
					else
					{
						_cardboardProfileID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="main.Component")]
	public partial class Component : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _documentID;
		
		private System.Guid _guid;
		
		private int _id;
		
		private EntitySet<ComponentDependancy> _componentDependancy;
		
		private EntitySet<ComponentDependancy> _componentDependancy1;
		
		private EntitySet<MajorationSet> _majorationSets;
		
		private EntitySet<ParamDefaultValue> _paramDefaultValues;
		
		private EntityRef<Document> _document = new EntityRef<Document>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnDocumentIDChanged();
		
		partial void OnDocumentIDChanging(int value);
		
		partial void OnGuidChanged();
		
		partial void OnGuidChanging(System.Guid value);
		
		partial void OnIDChanged();
		
		partial void OnIDChanging(int value);
		#endregion
		
		
		public Component()
		{
			_componentDependancy = new EntitySet<ComponentDependancy>(new Action<ComponentDependancy>(this.ComponentDependancy_Attach), new Action<ComponentDependancy>(this.ComponentDependancy_Detach));
			_componentDependancy1 = new EntitySet<ComponentDependancy>(new Action<ComponentDependancy>(this.ComponentDependancy1_Attach), new Action<ComponentDependancy>(this.ComponentDependancy1_Detach));
			_majorationSets = new EntitySet<MajorationSet>(new Action<MajorationSet>(this.MajorationSets_Attach), new Action<MajorationSet>(this.MajorationSets_Detach));
			_paramDefaultValues = new EntitySet<ParamDefaultValue>(new Action<ParamDefaultValue>(this.ParamDefaultValues_Attach), new Action<ParamDefaultValue>(this.ParamDefaultValues_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_documentID", Name="DocumentID", DbType="integer", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int DocumentID
		{
			get
			{
				return this._documentID;
			}
			set
			{
				if ((_documentID != value))
				{
					if (_document.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnDocumentIDChanging(value);
					this.SendPropertyChanging();
					this._documentID = value;
					this.SendPropertyChanged("DocumentID");
					this.OnDocumentIDChanged();
				}
			}
		}
		
		[Column(Storage="_guid", Name="Guid", DbType="guid", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.Guid Guid
		{
			get
			{
				return this._guid;
			}
			set
			{
				if ((_guid != value))
				{
					this.OnGuidChanging(value);
					this.SendPropertyChanging();
					this._guid = value;
					this.SendPropertyChanged("Guid");
					this.OnGuidChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="ID", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_componentDependancy", OtherKey="DependsOfID", ThisKey="ID", Name="fk_ComponentDependancy_0")]
		[DebuggerNonUserCode()]
		public EntitySet<ComponentDependancy> ComponentDependancy
		{
			get
			{
				return this._componentDependancy;
			}
			set
			{
				this._componentDependancy = value;
			}
		}
		
		[Association(Storage="_componentDependancy1", OtherKey="ComponentID", ThisKey="ID", Name="fk_ComponentDependancy_1")]
		[DebuggerNonUserCode()]
		public EntitySet<ComponentDependancy> ComponentDependancy1
		{
			get
			{
				return this._componentDependancy1;
			}
			set
			{
				this._componentDependancy1 = value;
			}
		}
		
		[Association(Storage="_majorationSets", OtherKey="ComponentID", ThisKey="ID", Name="fk_MajorationSet_0")]
		[DebuggerNonUserCode()]
		public EntitySet<MajorationSet> MajorationSets
		{
			get
			{
				return this._majorationSets;
			}
			set
			{
				this._majorationSets = value;
			}
		}
		
		[Association(Storage="_paramDefaultValues", OtherKey="ComponentID", ThisKey="ID", Name="fk_ParamDefaultValue_0")]
		[DebuggerNonUserCode()]
		public EntitySet<ParamDefaultValue> ParamDefaultValues
		{
			get
			{
				return this._paramDefaultValues;
			}
			set
			{
				this._paramDefaultValues = value;
			}
		}
		#endregion
		
		#region Parents
		[Association(Storage="_document", OtherKey="ID", ThisKey="DocumentID", Name="fk_Component_0", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Document Document
		{
			get
			{
				return this._document.Entity;
			}
			set
			{
				if (((this._document.Entity == value) 
							== false))
				{
					if ((this._document.Entity != null))
					{
						Document previousDocument = this._document.Entity;
						this._document.Entity = null;
						previousDocument.Components.Remove(this);
					}
					this._document.Entity = value;
					if ((value != null))
					{
						value.Components.Add(this);
						_documentID = value.ID;
					}
					else
					{
						_documentID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void ComponentDependancy_Attach(ComponentDependancy entity)
		{
			this.SendPropertyChanging();
			entity.Component = this;
		}
		
		private void ComponentDependancy_Detach(ComponentDependancy entity)
		{
			this.SendPropertyChanging();
			entity.Component = null;
		}
		
		private void ComponentDependancy1_Attach(ComponentDependancy entity)
		{
			this.SendPropertyChanging();
			entity.Component1 = this;
		}
		
		private void ComponentDependancy1_Detach(ComponentDependancy entity)
		{
			this.SendPropertyChanging();
			entity.Component1 = null;
		}
		
		private void MajorationSets_Attach(MajorationSet entity)
		{
			this.SendPropertyChanging();
			entity.Component = this;
		}
		
		private void MajorationSets_Detach(MajorationSet entity)
		{
			this.SendPropertyChanging();
			entity.Component = null;
		}
		
		private void ParamDefaultValues_Attach(ParamDefaultValue entity)
		{
			this.SendPropertyChanging();
			entity.Component = this;
		}
		
		private void ParamDefaultValues_Detach(ParamDefaultValue entity)
		{
			this.SendPropertyChanging();
			entity.Component = null;
		}
		#endregion
	}
	
	[Table(Name="main.ComponentDependancy")]
	public partial class ComponentDependancy : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _componentID;
		
		private int _dependsOfID;
		
		private EntityRef<Component> _component = new EntityRef<Component>();
		
		private EntityRef<Component> _component1 = new EntityRef<Component>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnComponentIDChanged();
		
		partial void OnComponentIDChanging(int value);
		
		partial void OnDependsOfIDChanged();
		
		partial void OnDependsOfIDChanging(int value);
		#endregion
		
		
		public ComponentDependancy()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_componentID", Name="ComponentID", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ComponentID
		{
			get
			{
				return this._componentID;
			}
			set
			{
				if ((_componentID != value))
				{
					if (_component1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnComponentIDChanging(value);
					this.SendPropertyChanging();
					this._componentID = value;
					this.SendPropertyChanged("ComponentID");
					this.OnComponentIDChanged();
				}
			}
		}
		
		[Column(Storage="_dependsOfID", Name="DependsOfID", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int DependsOfID
		{
			get
			{
				return this._dependsOfID;
			}
			set
			{
				if ((_dependsOfID != value))
				{
					if (_component.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnDependsOfIDChanging(value);
					this.SendPropertyChanging();
					this._dependsOfID = value;
					this.SendPropertyChanged("DependsOfID");
					this.OnDependsOfIDChanged();
				}
			}
		}
		
		#region Parents
		[Association(Storage="_component", OtherKey="ID", ThisKey="DependsOfID", Name="fk_ComponentDependancy_0", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Component Component
		{
			get
			{
				return this._component.Entity;
			}
			set
			{
				if (((this._component.Entity == value) 
							== false))
				{
					if ((this._component.Entity != null))
					{
						Component previousComponent = this._component.Entity;
						this._component.Entity = null;
						previousComponent.ComponentDependancy.Remove(this);
					}
					this._component.Entity = value;
					if ((value != null))
					{
						value.ComponentDependancy.Add(this);
						_dependsOfID = value.ID;
					}
					else
					{
						_dependsOfID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_component1", OtherKey="ID", ThisKey="ComponentID", Name="fk_ComponentDependancy_1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Component Component1
		{
			get
			{
				return this._component1.Entity;
			}
			set
			{
				if (((this._component1.Entity == value) 
							== false))
				{
					if ((this._component1.Entity != null))
					{
						Component previousComponent = this._component1.Entity;
						this._component1.Entity = null;
						previousComponent.ComponentDependancy1.Remove(this);
					}
					this._component1.Entity = value;
					if ((value != null))
					{
						value.ComponentDependancy1.Add(this);
						_componentID = value.ID;
					}
					else
					{
						_componentID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="main.DataVersion")]
	public partial class DataVersion : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _name;
		
		private string _value;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		
		partial void OnValueChanged();
		
		partial void OnValueChanging(string value);
		#endregion
		
		
		public DataVersion()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_name", Name="Name", DbType="nvarchar(50)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (((_name == value) 
							== false))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_value", Name="Value", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (((_value == value) 
							== false))
				{
					this.OnValueChanging(value);
					this.SendPropertyChanging();
					this._value = value;
					this.SendPropertyChanged("Value");
					this.OnValueChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="main.Document")]
	public partial class Document : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _description;
		
		private int _documentTypeID;
		
		private int _fileID;
		
		private System.Guid _guid;
		
		private int _id;
		
		private string _name;
		
		private EntitySet<Component> _components;
		
		private EntitySet<TreeNodeDocument> _treeNodeDocuments;
		
		private EntityRef<DocumentType> _documentType = new EntityRef<DocumentType>();
		
		private EntityRef<File> _file = new EntityRef<File>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnDescriptionChanged();
		
		partial void OnDescriptionChanging(string value);
		
		partial void OnDocumentTypeIDChanged();
		
		partial void OnDocumentTypeIDChanging(int value);
		
		partial void OnFileIDChanged();
		
		partial void OnFileIDChanging(int value);
		
		partial void OnGuidChanged();
		
		partial void OnGuidChanging(System.Guid value);
		
		partial void OnIDChanged();
		
		partial void OnIDChanging(int value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		#endregion
		
		
		public Document()
		{
			_components = new EntitySet<Component>(new Action<Component>(this.Components_Attach), new Action<Component>(this.Components_Detach));
			_treeNodeDocuments = new EntitySet<TreeNodeDocument>(new Action<TreeNodeDocument>(this.TreeNodeDocuments_Attach), new Action<TreeNodeDocument>(this.TreeNodeDocuments_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_description", Name="Description", DbType="nvarchar", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (((_description == value) 
							== false))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Column(Storage="_documentTypeID", Name="DocumentTypeID", DbType="integer", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int DocumentTypeID
		{
			get
			{
				return this._documentTypeID;
			}
			set
			{
				if ((_documentTypeID != value))
				{
					if (_documentType.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnDocumentTypeIDChanging(value);
					this.SendPropertyChanging();
					this._documentTypeID = value;
					this.SendPropertyChanged("DocumentTypeID");
					this.OnDocumentTypeIDChanged();
				}
			}
		}
		
		[Column(Storage="_fileID", Name="FileID", DbType="integer", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int FileID
		{
			get
			{
				return this._fileID;
			}
			set
			{
				if ((_fileID != value))
				{
					if (_file.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnFileIDChanging(value);
					this.SendPropertyChanging();
					this._fileID = value;
					this.SendPropertyChanged("FileID");
					this.OnFileIDChanged();
				}
			}
		}
		
		[Column(Storage="_guid", Name="Guid", DbType="guid", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.Guid Guid
		{
			get
			{
				return this._guid;
			}
			set
			{
				if ((_guid != value))
				{
					this.OnGuidChanging(value);
					this.SendPropertyChanging();
					this._guid = value;
					this.SendPropertyChanged("Guid");
					this.OnGuidChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="ID", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_name", Name="Name", DbType="nvarchar(50)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (((_name == value) 
							== false))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_components", OtherKey="DocumentID", ThisKey="ID", Name="fk_Component_0")]
		[DebuggerNonUserCode()]
		public EntitySet<Component> Components
		{
			get
			{
				return this._components;
			}
			set
			{
				this._components = value;
			}
		}
		
		[Association(Storage="_treeNodeDocuments", OtherKey="DocumentID", ThisKey="ID", Name="fk_TreeNodeDocument_1")]
		[DebuggerNonUserCode()]
		public EntitySet<TreeNodeDocument> TreeNodeDocuments
		{
			get
			{
				return this._treeNodeDocuments;
			}
			set
			{
				this._treeNodeDocuments = value;
			}
		}
		#endregion
		
		#region Parents
		[Association(Storage="_documentType", OtherKey="ID", ThisKey="DocumentTypeID", Name="fk_Document_0", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public DocumentType DocumentType
		{
			get
			{
				return this._documentType.Entity;
			}
			set
			{
				if (((this._documentType.Entity == value) 
							== false))
				{
					if ((this._documentType.Entity != null))
					{
						DocumentType previousDocumentType = this._documentType.Entity;
						this._documentType.Entity = null;
						previousDocumentType.Documents.Remove(this);
					}
					this._documentType.Entity = value;
					if ((value != null))
					{
						value.Documents.Add(this);
						_documentTypeID = value.ID;
					}
					else
					{
						_documentTypeID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_file", OtherKey="ID", ThisKey="FileID", Name="fk_Document_1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public File File
		{
			get
			{
				return this._file.Entity;
			}
			set
			{
				if (((this._file.Entity == value) 
							== false))
				{
					if ((this._file.Entity != null))
					{
						File previousFile = this._file.Entity;
						this._file.Entity = null;
						previousFile.Documents.Remove(this);
					}
					this._file.Entity = value;
					if ((value != null))
					{
						value.Documents.Add(this);
						_fileID = value.ID;
					}
					else
					{
						_fileID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void Components_Attach(Component entity)
		{
			this.SendPropertyChanging();
			entity.Document = this;
		}
		
		private void Components_Detach(Component entity)
		{
			this.SendPropertyChanging();
			entity.Document = null;
		}
		
		private void TreeNodeDocuments_Attach(TreeNodeDocument entity)
		{
			this.SendPropertyChanging();
			entity.Document = this;
		}
		
		private void TreeNodeDocuments_Detach(TreeNodeDocument entity)
		{
			this.SendPropertyChanging();
			entity.Document = null;
		}
		#endregion
	}
	
	[Table(Name="main.DocumentType")]
	public partial class DocumentType : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _application;
		
		private string _description;
		
		private int _id;
		
		private string _name;
		
		private EntitySet<Document> _documents;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnApplicationChanged();
		
		partial void OnApplicationChanging(string value);
		
		partial void OnDescriptionChanged();
		
		partial void OnDescriptionChanging(string value);
		
		partial void OnIDChanged();
		
		partial void OnIDChanging(int value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		#endregion
		
		
		public DocumentType()
		{
			_documents = new EntitySet<Document>(new Action<Document>(this.Documents_Attach), new Action<Document>(this.Documents_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_application", Name="Application", DbType="nvarchar(50)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Application
		{
			get
			{
				return this._application;
			}
			set
			{
				if (((_application == value) 
							== false))
				{
					this.OnApplicationChanging(value);
					this.SendPropertyChanging();
					this._application = value;
					this.SendPropertyChanged("Application");
					this.OnApplicationChanged();
				}
			}
		}
		
		[Column(Storage="_description", Name="Description", DbType="nvarchar", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (((_description == value) 
							== false))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="ID", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_name", Name="Name", DbType="nvarchar(50)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (((_name == value) 
							== false))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_documents", OtherKey="DocumentTypeID", ThisKey="ID", Name="fk_Document_0")]
		[DebuggerNonUserCode()]
		public EntitySet<Document> Documents
		{
			get
			{
				return this._documents;
			}
			set
			{
				this._documents = value;
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void Documents_Attach(Document entity)
		{
			this.SendPropertyChanging();
			entity.DocumentType = this;
		}
		
		private void Documents_Detach(Document entity)
		{
			this.SendPropertyChanging();
			entity.DocumentType = null;
		}
		#endregion
	}
	
	[Table(Name="main.File")]
	public partial class File : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private System.DateTime _dateCreated;
		
		private string _extension;
		
		private System.Guid _guid;
		
		private int _id;
		
		private EntitySet<Document> _documents;
		
		private EntitySet<Thumbnail> _thumbnail;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnDateCreatedChanged();
		
		partial void OnDateCreatedChanging(System.DateTime value);
		
		partial void OnExtensionChanged();
		
		partial void OnExtensionChanging(string value);
		
		partial void OnGuidChanged();
		
		partial void OnGuidChanging(System.Guid value);
		
		partial void OnIDChanged();
		
		partial void OnIDChanging(int value);
		#endregion
		
		
		public File()
		{
			_documents = new EntitySet<Document>(new Action<Document>(this.Documents_Attach), new Action<Document>(this.Documents_Detach));
			_thumbnail = new EntitySet<Thumbnail>(new Action<Thumbnail>(this.Thumbnail_Attach), new Action<Thumbnail>(this.Thumbnail_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_dateCreated", Name="DateCreated", DbType="datetime", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime DateCreated
		{
			get
			{
				return this._dateCreated;
			}
			set
			{
				if ((_dateCreated != value))
				{
					this.OnDateCreatedChanging(value);
					this.SendPropertyChanging();
					this._dateCreated = value;
					this.SendPropertyChanged("DateCreated");
					this.OnDateCreatedChanged();
				}
			}
		}
		
		[Column(Storage="_extension", Name="Extension", DbType="char(10)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Extension
		{
			get
			{
				return this._extension;
			}
			set
			{
				if (((_extension == value) 
							== false))
				{
					this.OnExtensionChanging(value);
					this.SendPropertyChanging();
					this._extension = value;
					this.SendPropertyChanged("Extension");
					this.OnExtensionChanged();
				}
			}
		}
		
		[Column(Storage="_guid", Name="Guid", DbType="guid", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.Guid Guid
		{
			get
			{
				return this._guid;
			}
			set
			{
				if ((_guid != value))
				{
					this.OnGuidChanging(value);
					this.SendPropertyChanging();
					this._guid = value;
					this.SendPropertyChanged("Guid");
					this.OnGuidChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="ID", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_documents", OtherKey="FileID", ThisKey="ID", Name="fk_Document_1")]
		[DebuggerNonUserCode()]
		public EntitySet<Document> Documents
		{
			get
			{
				return this._documents;
			}
			set
			{
				this._documents = value;
			}
		}
		
		[Association(Storage="_thumbnail", OtherKey="FileID", ThisKey="ID", Name="fk_Thumbnail_0")]
		[DebuggerNonUserCode()]
		public EntitySet<Thumbnail> Thumbnail
		{
			get
			{
				return this._thumbnail;
			}
			set
			{
				this._thumbnail = value;
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void Documents_Attach(Document entity)
		{
			this.SendPropertyChanging();
			entity.File = this;
		}
		
		private void Documents_Detach(Document entity)
		{
			this.SendPropertyChanging();
			entity.File = null;
		}
		
		private void Thumbnail_Attach(Thumbnail entity)
		{
			this.SendPropertyChanging();
			entity.File = this;
		}
		
		private void Thumbnail_Detach(Thumbnail entity)
		{
			this.SendPropertyChanging();
			entity.File = null;
		}
		#endregion
	}
	
	[Table(Name="main.Majoration")]
	public partial class Majoration : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _majorationSetID;
		
		private string _name;
		
		private float _value;
		
		private EntityRef<MajorationSet> _majorationSet = new EntityRef<MajorationSet>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnMajorationSetIDChanged();
		
		partial void OnMajorationSetIDChanging(int value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		
		partial void OnValueChanged();
		
		partial void OnValueChanging(float value);
		#endregion
		
		
		public Majoration()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_majorationSetID", Name="MajorationSetID", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int MajorationSetID
		{
			get
			{
				return this._majorationSetID;
			}
			set
			{
				if ((_majorationSetID != value))
				{
					if (_majorationSet.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMajorationSetIDChanging(value);
					this.SendPropertyChanging();
					this._majorationSetID = value;
					this.SendPropertyChanged("MajorationSetID");
					this.OnMajorationSetIDChanged();
				}
			}
		}
		
		[Column(Storage="_name", Name="Name", DbType="nvarchar(50)", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (((_name == value) 
							== false))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_value", Name="Value", DbType="real", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public float Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if ((_value != value))
				{
					this.OnValueChanging(value);
					this.SendPropertyChanging();
					this._value = value;
					this.SendPropertyChanged("Value");
					this.OnValueChanged();
				}
			}
		}
		
		#region Parents
		[Association(Storage="_majorationSet", OtherKey="ID", ThisKey="MajorationSetID", Name="fk_Majoration_0", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public MajorationSet MajorationSet
		{
			get
			{
				return this._majorationSet.Entity;
			}
			set
			{
				if (((this._majorationSet.Entity == value) 
							== false))
				{
					if ((this._majorationSet.Entity != null))
					{
						MajorationSet previousMajorationSet = this._majorationSet.Entity;
						this._majorationSet.Entity = null;
						previousMajorationSet.Majorations.Remove(this);
					}
					this._majorationSet.Entity = value;
					if ((value != null))
					{
						value.Majorations.Add(this);
						_majorationSetID = value.ID;
					}
					else
					{
						_majorationSetID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="main.MajorationSet")]
	public partial class MajorationSet : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _cardboardProfileID;
		
		private int _componentID;
		
		private int _id;
		
		private EntitySet<Majoration> _majorations;
		
		private EntityRef<Component> _component = new EntityRef<Component>();
		
		private EntityRef<CardboardProfile> _cardboardProfile = new EntityRef<CardboardProfile>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCardboardProfileIDChanged();
		
		partial void OnCardboardProfileIDChanging(int value);
		
		partial void OnComponentIDChanged();
		
		partial void OnComponentIDChanging(int value);
		
		partial void OnIDChanged();
		
		partial void OnIDChanging(int value);
		#endregion
		
		
		public MajorationSet()
		{
			_majorations = new EntitySet<Majoration>(new Action<Majoration>(this.Majorations_Attach), new Action<Majoration>(this.Majorations_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_cardboardProfileID", Name="CardboardProfileID", DbType="integer", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int CardboardProfileID
		{
			get
			{
				return this._cardboardProfileID;
			}
			set
			{
				if ((_cardboardProfileID != value))
				{
					if (_cardboardProfile.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCardboardProfileIDChanging(value);
					this.SendPropertyChanging();
					this._cardboardProfileID = value;
					this.SendPropertyChanged("CardboardProfileID");
					this.OnCardboardProfileIDChanged();
				}
			}
		}
		
		[Column(Storage="_componentID", Name="ComponentID", DbType="integer", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ComponentID
		{
			get
			{
				return this._componentID;
			}
			set
			{
				if ((_componentID != value))
				{
					if (_component.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnComponentIDChanging(value);
					this.SendPropertyChanging();
					this._componentID = value;
					this.SendPropertyChanged("ComponentID");
					this.OnComponentIDChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="ID", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_majorations", OtherKey="MajorationSetID", ThisKey="ID", Name="fk_Majoration_0")]
		[DebuggerNonUserCode()]
		public EntitySet<Majoration> Majorations
		{
			get
			{
				return this._majorations;
			}
			set
			{
				this._majorations = value;
			}
		}
		#endregion
		
		#region Parents
		[Association(Storage="_component", OtherKey="ID", ThisKey="ComponentID", Name="fk_MajorationSet_0", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Component Component
		{
			get
			{
				return this._component.Entity;
			}
			set
			{
				if (((this._component.Entity == value) 
							== false))
				{
					if ((this._component.Entity != null))
					{
						Component previousComponent = this._component.Entity;
						this._component.Entity = null;
						previousComponent.MajorationSets.Remove(this);
					}
					this._component.Entity = value;
					if ((value != null))
					{
						value.MajorationSets.Add(this);
						_componentID = value.ID;
					}
					else
					{
						_componentID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_cardboardProfile", OtherKey="ID", ThisKey="CardboardProfileID", Name="fk_MajorationSet_1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public CardboardProfile CardboardProfile
		{
			get
			{
				return this._cardboardProfile.Entity;
			}
			set
			{
				if (((this._cardboardProfile.Entity == value) 
							== false))
				{
					if ((this._cardboardProfile.Entity != null))
					{
						CardboardProfile previousCardboardProfile = this._cardboardProfile.Entity;
						this._cardboardProfile.Entity = null;
						previousCardboardProfile.MajorationSets.Remove(this);
					}
					this._cardboardProfile.Entity = value;
					if ((value != null))
					{
						value.MajorationSets.Add(this);
						_cardboardProfileID = value.ID;
					}
					else
					{
						_cardboardProfileID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void Majorations_Attach(Majoration entity)
		{
			this.SendPropertyChanging();
			entity.MajorationSet = this;
		}
		
		private void Majorations_Detach(Majoration entity)
		{
			this.SendPropertyChanging();
			entity.MajorationSet = null;
		}
		#endregion
	}
	
	[Table(Name="main.ParamDefaultValue")]
	public partial class ParamDefaultValue : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _componentID;
		
		private string _name;
		
		private float _value;
		
		private EntityRef<Component> _component = new EntityRef<Component>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnComponentIDChanged();
		
		partial void OnComponentIDChanging(int value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		
		partial void OnValueChanged();
		
		partial void OnValueChanging(float value);
		#endregion
		
		
		public ParamDefaultValue()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_componentID", Name="ComponentID", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ComponentID
		{
			get
			{
				return this._componentID;
			}
			set
			{
				if ((_componentID != value))
				{
					if (_component.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnComponentIDChanging(value);
					this.SendPropertyChanging();
					this._componentID = value;
					this.SendPropertyChanged("ComponentID");
					this.OnComponentIDChanged();
				}
			}
		}
		
		[Column(Storage="_name", Name="Name", DbType="nvarchar(50)", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (((_name == value) 
							== false))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_value", Name="Value", DbType="real", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public float Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if ((_value != value))
				{
					this.OnValueChanging(value);
					this.SendPropertyChanging();
					this._value = value;
					this.SendPropertyChanged("Value");
					this.OnValueChanged();
				}
			}
		}
		
		#region Parents
		[Association(Storage="_component", OtherKey="ID", ThisKey="ComponentID", Name="fk_ParamDefaultValue_0", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Component Component
		{
			get
			{
				return this._component.Entity;
			}
			set
			{
				if (((this._component.Entity == value) 
							== false))
				{
					if ((this._component.Entity != null))
					{
						Component previousComponent = this._component.Entity;
						this._component.Entity = null;
						previousComponent.ParamDefaultValues.Remove(this);
					}
					this._component.Entity = value;
					if ((value != null))
					{
						value.ParamDefaultValues.Add(this);
						_componentID = value.ID;
					}
					else
					{
						_componentID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="main.sysdiagrams")]
	public partial class sysdiagram : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private byte[] _definition;
		
		private int _diagramid;
		
		private string _name;
		
		private int _principalid;
		
		private System.Nullable<int> _version;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OndefinitionChanged();
		
		partial void OndefinitionChanging(byte[] value);
		
		partial void OndiagramidChanged();
		
		partial void OndiagramidChanging(int value);
		
		partial void OnnameChanged();
		
		partial void OnnameChanging(string value);
		
		partial void OnprincipalidChanged();
		
		partial void OnprincipalidChanging(int value);
		
		partial void OnversionChanged();
		
		partial void OnversionChanging(System.Nullable<int> value);
		#endregion
		
		
		public sysdiagram()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_definition", Name="definition", DbType="blob", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public byte[] definition
		{
			get
			{
				return this._definition;
			}
			set
			{
				if (((_definition == value) 
							== false))
				{
					this.OndefinitionChanging(value);
					this.SendPropertyChanging();
					this._definition = value;
					this.SendPropertyChanged("definition");
					this.OndefinitionChanged();
				}
			}
		}
		
		[Column(Storage="_diagramid", Name="diagram_id", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int diagramid
		{
			get
			{
				return this._diagramid;
			}
			set
			{
				if ((_diagramid != value))
				{
					this.OndiagramidChanging(value);
					this.SendPropertyChanging();
					this._diagramid = value;
					this.SendPropertyChanged("diagramid");
					this.OndiagramidChanged();
				}
			}
		}
		
		[Column(Storage="_name", Name="name", DbType="nvarchar(128)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (((_name == value) 
							== false))
				{
					this.OnnameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("name");
					this.OnnameChanged();
				}
			}
		}
		
		[Column(Storage="_principalid", Name="principal_id", DbType="integer", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int principalid
		{
			get
			{
				return this._principalid;
			}
			set
			{
				if ((_principalid != value))
				{
					this.OnprincipalidChanging(value);
					this.SendPropertyChanging();
					this._principalid = value;
					this.SendPropertyChanged("principalid");
					this.OnprincipalidChanged();
				}
			}
		}
		
		[Column(Storage="_version", Name="version", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> version
		{
			get
			{
				return this._version;
			}
			set
			{
				if ((_version != value))
				{
					this.OnversionChanging(value);
					this.SendPropertyChanging();
					this._version = value;
					this.SendPropertyChanged("version");
					this.OnversionChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="main.Thumbnail")]
	public partial class Thumbnail : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _fileID;
		
		private int _height;
		
		private int _id;
		
		private string _mimeType;
		
		private byte[] _thumbCache;
		
		private int _width;
		
		private EntitySet<TreeNode> _treeNodes;
		
		private EntityRef<File> _file = new EntityRef<File>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnFileIDChanged();
		
		partial void OnFileIDChanging(int value);
		
		partial void OnHeightChanged();
		
		partial void OnHeightChanging(int value);
		
		partial void OnIDChanged();
		
		partial void OnIDChanging(int value);
		
		partial void OnMimeTypeChanged();
		
		partial void OnMimeTypeChanging(string value);
		
		partial void OnThumbCacheChanged();
		
		partial void OnThumbCacheChanging(byte[] value);
		
		partial void OnWidthChanged();
		
		partial void OnWidthChanging(int value);
		#endregion
		
		
		public Thumbnail()
		{
			_treeNodes = new EntitySet<TreeNode>(new Action<TreeNode>(this.TreeNodes_Attach), new Action<TreeNode>(this.TreeNodes_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_fileID", Name="FileID", DbType="integer", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int FileID
		{
			get
			{
				return this._fileID;
			}
			set
			{
				if ((_fileID != value))
				{
					if (_file.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnFileIDChanging(value);
					this.SendPropertyChanging();
					this._fileID = value;
					this.SendPropertyChanged("FileID");
					this.OnFileIDChanged();
				}
			}
		}
		
		[Column(Storage="_height", Name="Height", DbType="integer", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int Height
		{
			get
			{
				return this._height;
			}
			set
			{
				if ((_height != value))
				{
					this.OnHeightChanging(value);
					this.SendPropertyChanging();
					this._height = value;
					this.SendPropertyChanged("Height");
					this.OnHeightChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="ID", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_mimeType", Name="MimeType", DbType="nvarchar(50)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string MimeType
		{
			get
			{
				return this._mimeType;
			}
			set
			{
				if (((_mimeType == value) 
							== false))
				{
					this.OnMimeTypeChanging(value);
					this.SendPropertyChanging();
					this._mimeType = value;
					this.SendPropertyChanged("MimeType");
					this.OnMimeTypeChanged();
				}
			}
		}
		
		[Column(Storage="_thumbCache", Name="ThumbCache", DbType="blob(2147483647)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public byte[] ThumbCache
		{
			get
			{
				return this._thumbCache;
			}
			set
			{
				if (((_thumbCache == value) 
							== false))
				{
					this.OnThumbCacheChanging(value);
					this.SendPropertyChanging();
					this._thumbCache = value;
					this.SendPropertyChanged("ThumbCache");
					this.OnThumbCacheChanged();
				}
			}
		}
		
		[Column(Storage="_width", Name="Width", DbType="integer", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int Width
		{
			get
			{
				return this._width;
			}
			set
			{
				if ((_width != value))
				{
					this.OnWidthChanging(value);
					this.SendPropertyChanging();
					this._width = value;
					this.SendPropertyChanged("Width");
					this.OnWidthChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_treeNodes", OtherKey="ThumbnailID", ThisKey="ID", Name="fk_TreeNode_1")]
		[DebuggerNonUserCode()]
		public EntitySet<TreeNode> TreeNodes
		{
			get
			{
				return this._treeNodes;
			}
			set
			{
				this._treeNodes = value;
			}
		}
		#endregion
		
		#region Parents
		[Association(Storage="_file", OtherKey="ID", ThisKey="FileID", Name="fk_Thumbnail_0", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public File File
		{
			get
			{
				return this._file.Entity;
			}
			set
			{
				if (((this._file.Entity == value) 
							== false))
				{
					if ((this._file.Entity != null))
					{
						File previousFile = this._file.Entity;
						this._file.Entity = null;
						previousFile.Thumbnail.Remove(this);
					}
					this._file.Entity = value;
					if ((value != null))
					{
						value.Thumbnail.Add(this);
						_fileID = value.ID;
					}
					else
					{
						_fileID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void TreeNodes_Attach(TreeNode entity)
		{
			this.SendPropertyChanging();
			entity.Thumbnail = this;
		}
		
		private void TreeNodes_Detach(TreeNode entity)
		{
			this.SendPropertyChanging();
			entity.Thumbnail = null;
		}
		#endregion
	}
	
	[Table(Name="main.TreeNode")]
	public partial class TreeNode : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _description;
		
		private int _id;
		
		private string _name;
		
		private System.Nullable<int> _parentNodeID;
		
		private int _thumbnailID;
		
		private EntitySet<TreeNode> _treeNodes;
		
		private EntitySet<TreeNodeDocument> _treeNodeDocuments;
		
		private EntityRef<TreeNode> _parentNodeIdtReeNode = new EntityRef<TreeNode>();
		
		private EntityRef<Thumbnail> _thumbnail = new EntityRef<Thumbnail>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnDescriptionChanged();
		
		partial void OnDescriptionChanging(string value);
		
		partial void OnIDChanged();
		
		partial void OnIDChanging(int value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		
		partial void OnParentNodeIDChanged();
		
		partial void OnParentNodeIDChanging(System.Nullable<int> value);
		
		partial void OnThumbnailIDChanged();
		
		partial void OnThumbnailIDChanging(int value);
		#endregion
		
		
		public TreeNode()
		{
			_treeNodes = new EntitySet<TreeNode>(new Action<TreeNode>(this.TreeNodes_Attach), new Action<TreeNode>(this.TreeNodes_Detach));
			_treeNodeDocuments = new EntitySet<TreeNodeDocument>(new Action<TreeNodeDocument>(this.TreeNodeDocuments_Attach), new Action<TreeNodeDocument>(this.TreeNodeDocuments_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_description", Name="Description", DbType="nvarchar", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (((_description == value) 
							== false))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="ID", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_name", Name="Name", DbType="nvarchar(50)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (((_name == value) 
							== false))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_parentNodeID", Name="ParentNodeID", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> ParentNodeID
		{
			get
			{
				return this._parentNodeID;
			}
			set
			{
				if ((_parentNodeID != value))
				{
					if (_parentNodeIdtReeNode.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnParentNodeIDChanging(value);
					this.SendPropertyChanging();
					this._parentNodeID = value;
					this.SendPropertyChanged("ParentNodeID");
					this.OnParentNodeIDChanged();
				}
			}
		}
		
		[Column(Storage="_thumbnailID", Name="ThumbnailID", DbType="integer", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ThumbnailID
		{
			get
			{
				return this._thumbnailID;
			}
			set
			{
				if ((_thumbnailID != value))
				{
					if (_thumbnail.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnThumbnailIDChanging(value);
					this.SendPropertyChanging();
					this._thumbnailID = value;
					this.SendPropertyChanged("ThumbnailID");
					this.OnThumbnailIDChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_treeNodes", OtherKey="ParentNodeID", ThisKey="ID", Name="fk_TreeNode_0")]
		[DebuggerNonUserCode()]
		public EntitySet<TreeNode> TreeNodes
		{
			get
			{
				return this._treeNodes;
			}
			set
			{
				this._treeNodes = value;
			}
		}
		
		[Association(Storage="_treeNodeDocuments", OtherKey="TreeNodeID", ThisKey="ID", Name="fk_TreeNodeDocument_0")]
		[DebuggerNonUserCode()]
		public EntitySet<TreeNodeDocument> TreeNodeDocuments
		{
			get
			{
				return this._treeNodeDocuments;
			}
			set
			{
				this._treeNodeDocuments = value;
			}
		}
		#endregion
		
		#region Parents
		[Association(Storage="_parentNodeIdtReeNode", OtherKey="ID", ThisKey="ParentNodeID", Name="fk_TreeNode_0", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public TreeNode ParentNodeIDTreeNode
		{
			get
			{
				return this._parentNodeIdtReeNode.Entity;
			}
			set
			{
				if (((this._parentNodeIdtReeNode.Entity == value) 
							== false))
				{
					if ((this._parentNodeIdtReeNode.Entity != null))
					{
						TreeNode previousTreeNode = this._parentNodeIdtReeNode.Entity;
						this._parentNodeIdtReeNode.Entity = null;
						previousTreeNode.TreeNodes.Remove(this);
					}
					this._parentNodeIdtReeNode.Entity = value;
					if ((value != null))
					{
						value.TreeNodes.Add(this);
						_parentNodeID = value.ID;
					}
					else
					{
						_parentNodeID = null;
					}
				}
			}
		}
		
		[Association(Storage="_thumbnail", OtherKey="ID", ThisKey="ThumbnailID", Name="fk_TreeNode_1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Thumbnail Thumbnail
		{
			get
			{
				return this._thumbnail.Entity;
			}
			set
			{
				if (((this._thumbnail.Entity == value) 
							== false))
				{
					if ((this._thumbnail.Entity != null))
					{
						Thumbnail previousThumbnail = this._thumbnail.Entity;
						this._thumbnail.Entity = null;
						previousThumbnail.TreeNodes.Remove(this);
					}
					this._thumbnail.Entity = value;
					if ((value != null))
					{
						value.TreeNodes.Add(this);
						_thumbnailID = value.ID;
					}
					else
					{
						_thumbnailID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void TreeNodes_Attach(TreeNode entity)
		{
			this.SendPropertyChanging();
			entity.ParentNodeIDTreeNode = this;
		}
		
		private void TreeNodes_Detach(TreeNode entity)
		{
			this.SendPropertyChanging();
			entity.ParentNodeIDTreeNode = null;
		}
		
		private void TreeNodeDocuments_Attach(TreeNodeDocument entity)
		{
			this.SendPropertyChanging();
			entity.TreeNode = this;
		}
		
		private void TreeNodeDocuments_Detach(TreeNodeDocument entity)
		{
			this.SendPropertyChanging();
			entity.TreeNode = null;
		}
		#endregion
	}
	
	[Table(Name="main.TreeNodeDocument")]
	public partial class TreeNodeDocument : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _documentID;
		
		private int _id;
		
		private int _treeNodeID;
		
		private EntityRef<TreeNode> _treeNode = new EntityRef<TreeNode>();
		
		private EntityRef<Document> _document = new EntityRef<Document>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnDocumentIDChanged();
		
		partial void OnDocumentIDChanging(int value);
		
		partial void OnIDChanged();
		
		partial void OnIDChanging(int value);
		
		partial void OnTreeNodeIDChanged();
		
		partial void OnTreeNodeIDChanging(int value);
		#endregion
		
		
		public TreeNodeDocument()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_documentID", Name="DocumentID", DbType="integer", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int DocumentID
		{
			get
			{
				return this._documentID;
			}
			set
			{
				if ((_documentID != value))
				{
					if (_document.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnDocumentIDChanging(value);
					this.SendPropertyChanging();
					this._documentID = value;
					this.SendPropertyChanged("DocumentID");
					this.OnDocumentIDChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="ID", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_treeNodeID", Name="TreeNodeID", DbType="integer", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int TreeNodeID
		{
			get
			{
				return this._treeNodeID;
			}
			set
			{
				if ((_treeNodeID != value))
				{
					if (_treeNode.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnTreeNodeIDChanging(value);
					this.SendPropertyChanging();
					this._treeNodeID = value;
					this.SendPropertyChanged("TreeNodeID");
					this.OnTreeNodeIDChanged();
				}
			}
		}
		
		#region Parents
		[Association(Storage="_treeNode", OtherKey="ID", ThisKey="TreeNodeID", Name="fk_TreeNodeDocument_0", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public TreeNode TreeNode
		{
			get
			{
				return this._treeNode.Entity;
			}
			set
			{
				if (((this._treeNode.Entity == value) 
							== false))
				{
					if ((this._treeNode.Entity != null))
					{
						TreeNode previousTreeNode = this._treeNode.Entity;
						this._treeNode.Entity = null;
						previousTreeNode.TreeNodeDocuments.Remove(this);
					}
					this._treeNode.Entity = value;
					if ((value != null))
					{
						value.TreeNodeDocuments.Add(this);
						_treeNodeID = value.ID;
					}
					else
					{
						_treeNodeID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_document", OtherKey="ID", ThisKey="DocumentID", Name="fk_TreeNodeDocument_1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Document Document
		{
			get
			{
				return this._document.Entity;
			}
			set
			{
				if (((this._document.Entity == value) 
							== false))
				{
					if ((this._document.Entity != null))
					{
						Document previousDocument = this._document.Entity;
						this._document.Entity = null;
						previousDocument.TreeNodeDocuments.Remove(this);
					}
					this._document.Entity = value;
					if ((value != null))
					{
						value.TreeNodeDocuments.Add(this);
						_documentID = value.ID;
					}
					else
					{
						_documentID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
