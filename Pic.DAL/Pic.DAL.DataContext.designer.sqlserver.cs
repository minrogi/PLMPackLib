#region Auto-generated classes for PicParamDb database on 2010-03-30 10:11:04Z

//
//  ____  _     __  __      _        _
// |  _ \| |__ |  \/  | ___| |_ __ _| |
// | | | | '_ \| |\/| |/ _ \ __/ _` | |
// | |_| | |_) | |  | |  __/ || (_| | |
// |____/|_.__/|_|  |_|\___|\__\__,_|_|
//
// Auto-generated from PicParamDb on 2010-03-30 10:11:04Z
// Please visit http://linq.to/db for more information

#endregion

using System;
using System.Data;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using System.Reflection;
#if MONO_STRICT
using System.Data.Linq;
#else   // MONO_STRICT
using DbLinq.Data.Linq;
using DbLinq.Vendor;
#endif  // MONO_STRICT
using System.ComponentModel;

namespace Pic.DAL
{
	public partial class PicParamDbDataContext : DataContext
	{
		#region Extensibility Method Definitions

		partial void OnCreated();

		#endregion

		public PicParamDbDataContext(string connectionString)
			: base(connectionString)
		{
			OnCreated();
		}

		public PicParamDbDataContext(IDbConnection connection)
		#if MONO_STRICT
			: base(connection)
		#else   // MONO_STRICT
			: base(connection, new DbLinq.SqlServer.SqlServerVendor())
		#endif  // MONO_STRICT
		{
			OnCreated();
		}

		public PicParamDbDataContext(string connection, MappingSource mappingSource)
			: base(connection, mappingSource)
		{
			OnCreated();
		}

		public PicParamDbDataContext(IDbConnection connection, MappingSource mappingSource)
			: base(connection, mappingSource)
		{
			OnCreated();
		}

		#if !MONO_STRICT
		public PicParamDbDataContext(IDbConnection connection, IVendor vendor)
			: base(connection, vendor)
		{
			OnCreated();
		}
		#endif  // !MONO_STRICT

		#if !MONO_STRICT
		public PicParamDbDataContext(IDbConnection connection, MappingSource mappingSource, IVendor vendor)
			: base(connection, mappingSource, vendor)
		{
			OnCreated();
		}
		#endif  // !MONO_STRICT

		public Table<CardboardProfile> CardboardProfiles { get { return GetTable<CardboardProfile>(); } }
		public Table<CardboardQuality> CardboardQualities { get { return GetTable<CardboardQuality>(); } }
		public Table<Component> Components { get { return GetTable<Component>(); } }
		public Table<ComponentDependancy> ComponentDependancies { get { return GetTable<ComponentDependancy>(); } }
		public Table<DataVersion> DataVersions { get { return GetTable<DataVersion>(); } }
		public Table<Document> Documents { get { return GetTable<Document>(); } }
		public Table<DocumentType> DocumentTypes { get { return GetTable<DocumentType>(); } }
		public Table<File> Files { get { return GetTable<File>(); } }
		public Table<Majoration> Majorations { get { return GetTable<Majoration>(); } }
		public Table<MajorationSet> MajorationSets { get { return GetTable<MajorationSet>(); } }
		public Table<MajorationSet1> MajorationSet1s { get { return GetTable<MajorationSet1>(); } }
		public Table<Thumbnail> Thumbnails { get { return GetTable<Thumbnail>(); } }
		public Table<TreeNode> TreeNodes { get { return GetTable<TreeNode>(); } }
		public Table<TreeNodeDocument> TreeNodeDocuments { get { return GetTable<TreeNodeDocument>(); } }

	}

	[Table(Name = "dbo.CardboardProfile")]
	public partial class CardboardProfile : INotifyPropertyChanging, INotifyPropertyChanged
	{
		#region INotifyPropertyChanging handling

		public event PropertyChangingEventHandler PropertyChanging;

		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs("");
		protected virtual void SendPropertyChanging()
		{
			if (PropertyChanging != null)
			{
				PropertyChanging(this, emptyChangingEventArgs);
			}
		}

		#endregion

		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void SendPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region Extensibility Method Definitions

		partial void OnCreated();
		partial void OnChanged();
		partial void OnChanging(int value);
		partial void OnChanged();
		partial void OnChanging(string value);
		partial void OnChanged();
		partial void OnChanging(string value);
		partial void OnChanged();
		partial void OnChanging(float value);

		#endregion

		#region int

		[DebuggerNonUserCode]
		[Column(Storage = null, Name = "ID", DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.Never, CanBeNull = false)]
		public int
		{
			get; set;
		}

		#endregion

		#region string

		[DebuggerNonUserCode]
		[Column(Storage = null, Name = "Name", DbType = "NVarChar(50) NOT NULL", AutoSync = AutoSync.Never, CanBeNull = false)]
		public string
		{
			get; set;
		}

		#endregion

		#region string

		[DebuggerNonUserCode]
		[Column(Storage = null, Name = "Code", DbType = "NChar(10) NOT NULL", AutoSync = AutoSync.Never, CanBeNull = false)]
		public string
		{
			get; set;
		}

		#endregion

		#region float

		[DebuggerNonUserCode]
		[Column(Storage = null, Name = "Thickness", DbType = "Real NOT NULL", AutoSync = AutoSync.Never, CanBeNull = false)]
		public float
		{
			get; set;
		}

		#endregion

		#region Children

		[Association(Storage = null, OtherKey = "CardboardProfileID", ThisKey = "ID", Name = "CardboardProfile_CardboardQuality")]
		[DebuggerNonUserCode]
		public EntitySet<CardboardQuality> CardboardQualities
		{
			get; set;
		}

		[Association(Storage = null, OtherKey = "CardboardProfileID", ThisKey = "ID", Name = "CardboardProfile_MajorationSet")]
		[DebuggerNonUserCode]
		public EntitySet<MajorationSet> MajorationSets
		{
			get; set;
		}

		[Association(Storage = null, OtherKey = "CardboardProfileID", ThisKey = "ID", Name = "CardboardProfile_MajorationSet1")]
		[DebuggerNonUserCode]
		public EntitySet<MajorationSet1> MajorationSet1s
		{
			get; set;
		}


		#endregion

		#region Attachement handlers

		private void CardboardQualities_Attach(CardboardQuality entity)
		{
			entity.CardboardProfile = this;
		}

		private void CardboardQualities_Detach(CardboardQuality entity)
		{
			entity.CardboardProfile = null;
		}

		private void MajorationSets_Attach(MajorationSet entity)
		{
			entity.CardboardProfile = this;
		}

		private void MajorationSets_Detach(MajorationSet entity)
		{
			entity.CardboardProfile = null;
		}

		private void MajorationSet1s_Attach(MajorationSet1 entity)
		{
			entity.CardboardProfile = this;
		}

		private void MajorationSet1s_Detach(MajorationSet1 entity)
		{
			entity.CardboardProfile = null;
		}


		#endregion

		#region ctor

		public CardboardProfile()
		{
			CardboardQualities = new EntitySet<CardboardQuality>(CardboardQualities_Attach, CardboardQualities_Detach);
			MajorationSets = new EntitySet<MajorationSet>(MajorationSets_Attach, MajorationSets_Detach);
			MajorationSet1s = new EntitySet<MajorationSet1>(MajorationSet1s_Attach, MajorationSet1s_Detach);
			OnCreated();
		}

		#endregion

	}

	[Table(Name = "dbo.CardboardQuality")]
	public partial class CardboardQuality : INotifyPropertyChanging, INotifyPropertyChanged
	{
		#region INotifyPropertyChanging handling

		public event PropertyChangingEventHandler PropertyChanging;

		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs("");
		protected virtual void SendPropertyChanging()
		{
			if (PropertyChanging != null)
			{
				PropertyChanging(this, emptyChangingEventArgs);
			}
		}

		#endregion

		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void SendPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region Extensibility Method Definitions

		partial void OnCreated();
		partial void OnChanged();
		partial void OnChanging(int value);
		partial void OnChanged();
		partial void OnChanging(int value);
		partial void OnChanged();
		partial void OnChanging(string value);
		partial void OnChanged();
		partial void OnChanging(string value);
		partial void OnChanged();
		partial void OnChanging(float value);
		partial void OnChanged();
		partial void OnChanging(float value);
		partial void OnChanged();
		partial void OnChanging(float value);
		partial void OnChanged();
		partial void OnChanging(float value);
		partial void OnChanged();
		partial void OnChanging(float value);

		#endregion

		#region int

		[DebuggerNonUserCode]
		[Column(Storage = null, Name = "ID", DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.Never, CanBeNull = false)]
		public int
		{
			get; set;
		}

		#endregion

		#region int

		[DebuggerNonUserCode]
		[Column(Storage = null, Name = "CardboardProfileID", DbType = "Int NOT NULL", AutoSync = AutoSync.Never, CanBeNull = false)]
		public int
		{
			get; set;
		}

		#endregion

		#region string

		[DebuggerNonUserCode]
		[Column(Storage = null, Name = "Name", DbType = "NVarChar(50) NOT NULL", AutoSync = AutoSync.Never, CanBeNull = false)]
		public string
		{
			get; set;
		}

		#endregion

		#region string

		[DebuggerNonUserCode]
		[Column(Storage = null, Name = "Description", DbType = "NVarChar(MAX) NOT NULL", AutoSync = AutoSync.Never, CanBeNull = false)]
		public string
		{
			get; set;
		}

		#endregion

		#region float

		[DebuggerNonUserCode]
		[Column(Storage = null, Name = "SurfacicMass", DbType = "Real NOT NULL", AutoSync = AutoSync.Never, CanBeNull = false)]
		public float
		{
			get; set;
		}

		#endregion

		#region float

		[DebuggerNonUserCode]
		[Column(Storage = null, Name = "RigidityX", DbType = "Real NOT NULL", AutoSync = AutoSync.Never, CanBeNull = false)]
		public float
		{
			get; set;
		}

		#endregion

		#region float

		[DebuggerNonUserCode]
		[Column(Storage = null, Name = "RigidityY", DbType = "Real NOT NULL", AutoSync = AutoSync.Never, CanBeNull = false)]
		public float
		{
			get; set;
		}

		#endregion

		#region float

		[DebuggerNonUserCode]
		[Column(Storage = null, Name = "YoungModulus", DbType = "Real NOT NULL", AutoSync = AutoSync.Never, CanBeNull = false)]
		public float
		{
			get; set;
		}

		#endregion

		#region float

		[DebuggerNonUserCode]
		[Column(Storage = null, Name = "ECT", DbType = "Real NOT NULL", AutoSync = AutoSync.Never, CanBeNull = false)]
		public float
		{
			get; set;
		}

		#endregion

		#region Parents

		private EntityRef<CardboardProfile> ;
		[Association(Storage = null, OtherKey = "ID", ThisKey = "CardboardProfileID", Name = "CardboardProfile_CardboardQuality", IsForeignKey = true)]
		[DebuggerNonUserCode]
		public CardboardProfile CardboardProfile
		{
			get
			{
				return .Entity;
			}
			set
			{
				if (value != .Entity)
				{
					if (.Entity != null)
					{
						var previousCardboardProfile = .Entity;
						.Entity = null;
						previousCardboardProfile.CardboardQualities.Remove(this);
					}
					.Entity = value;
					if (value != null)
					{
						value.CardboardQualities.Add(this);
					}
				}
			}

			#endregion

		}
	}
