using System;
using System.Collections;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.Design.WebControls;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace DBauer.Web.UI.WebControls
{
	/// <summary>
	/// DynamicControlsPlaceholder solves the problem that dynamically added controls are not automatically recreated on subsequent requests.
	/// The control uses the ViewState to store the types of the child controls recursively and recreates them automatically.
	/// 
	/// Please note that property values that are set before "TrackViewState" is called (usually in Controls.Add) are not persisted
	/// </summary>
	[ControlBuilder(typeof(System.Web.UI.WebControls.PlaceHolderControlBuilder)), 
	Designer("System.Web.UI.Design.ControlDesigner"), 
	DefaultProperty("ID"), 
	ToolboxData("<{0}:DynamicControlsPlaceholder runat=server></{0}:DynamicControlsPlaceholder>")]
	public class DynamicControlsPlaceholder : PlaceHolder
	{
		#region custom events
		/// <summary>
		/// Occurs when a control has been restored from ViewState
		/// </summary>
		public event DynamicControlEventHandler ControlRestored;
		/// <summary>
		/// Occurs when the DynamicControlsPlaceholder is about to restore the child controls from ViewState
		/// </summary>
		public event EventHandler PreRestore;
		/// <summary>
		/// Occurs after the DynamicControlsPlaceholder has restored the child controls from ViewState
		/// </summary>
		public event EventHandler PostRestore;

		/// <summary>
		/// Raises the <see cref="ControlRestored">ControlRestored</see> event.
		/// </summary>
		/// <param name="e">The <see cref="DynamicControlEventArgs">DynamicControlEventArgs</see> object that contains the event data.</param>
		protected virtual void OnControlRestored(DynamicControlEventArgs e)
		{
			if(ControlRestored != null)
				ControlRestored(this, e);
		}

		/// <summary>
		/// Raises the <see cref="PreRestore">PreRestore</see> event.
		/// </summary>
		/// <param name="e">The <see cref="System.EventArgs">EventArgs</see> object that contains the event data.</param>
		protected virtual void OnPreRestore(EventArgs e)
		{
			if(PreRestore != null)
				PreRestore(this, e);
		}

		/// <summary>
		/// Raises the <see cref="PostRestore">PostRestore</see> event.
		/// </summary>
		/// <param name="e">The <see cref="System.EventArgs">EventArgs</see> object that contains the event data.</param>
		protected virtual void OnPostRestore(EventArgs e)
		{
			if(PostRestore != null)
				PostRestore(this, e);
		}
		#endregion custom events

		#region custom propterties
		/// <summary>
		/// Specifies whether Controls without IDs shall be persisted or if an exception shall be thrown
		/// </summary>
		[DefaultValue(HandleDynamicControls.DontPersist)]
		public HandleDynamicControls ControlsWithoutIDs
		{
			get
			{
				if(ViewState["ControlsWithoutIDs"] == null)
					return HandleDynamicControls.DontPersist;
				else
					return (HandleDynamicControls) ViewState["ControlsWithoutIDs"];
			}
			set { ViewState["ControlsWithoutIDs"] = value; }
		}
		#endregion custom propterties

		#region ViewState management
		/// <summary>
		/// Recreates all dynamically added child controls of the Placeholder and then calls the default 
		/// LoadViewState mechanism
		/// </summary>
		/// <param name="savedState">Array of objects that contains the child structure in the first item, 
		/// and the base ViewState in the second item</param>
		protected override void LoadViewState(object savedState) 
		{
			object[] viewState = (object[]) savedState;

			//Raise PreRestore event
			OnPreRestore(EventArgs.Empty);

			//recreate the child controls recursively
			Pair persistInfo = (Pair) viewState[0];
			foreach(Pair pair in (ArrayList) persistInfo.Second)
			{
				RestoreChildStructure(pair, this);
			}

			//Raise PostRestore event
			OnPostRestore(EventArgs.Empty);

			base.LoadViewState(viewState[1]);
		}

		/// <summary>
		/// Walks recursively through all child controls and stores their type in ViewState and then calls the default 
		/// SaveViewState mechanism
		/// </summary>
		/// <returns>Array of objects that contains the child structure in the first item, 
		/// and the base ViewState in the second item</returns>
		protected override object SaveViewState()
		{
			if(HttpContext.Current == null)
				return null;

			object[] viewState = new object[2];
			viewState[0] = PersistChildStructure(this, "C");
			viewState[1] = base.SaveViewState();
			return viewState;
		}

		/// <summary>
		/// Recreates a single control and recursively calls itself for all child controls
		/// </summary>
		/// <param name="persistInfo">A pair that contains the controls persisted information in the first property,
		/// and an ArrayList with the child's persisted information in the second property</param>
		/// <param name="parent">The parent control to which Controls collection it is added</param>
		private void RestoreChildStructure(Pair persistInfo, Control parent)
		{
			Control control;

			string[] persistedString = persistInfo.First.ToString().Split(';');

			string[] typeName = persistedString[1].Split(':');
			switch(typeName[0])
			{
					//restore the UserControl by calling Page.LoadControl
				case "UC":
					//when running under ASP.NET >= 2.0 load the user control based on its type
					if (Environment.Version.Major > 1)
					{
						Type ucType = Type.GetType(typeName[1], true, true);
						try
						{
							//calling the overload Page.LoadControl(ucType, null) via reflection (which is not very nice but necessary when compiled against 1.0)
							MethodInfo mi = typeof(Page).GetMethod("LoadControl", new Type[2] { typeof(Type), typeof(object[]) });
								control = (Control) mi.Invoke(this.Page, new object[2] { ucType, null });
						}
						catch (Exception e)
						{
							throw new ArgumentException(String.Format("The type '{0}' cannot be recreated from ViewState", ucType.ToString()), e);
						}
					}
					else //in ASP.NET 1.0/1.1 load the user control based on the file
					{
						//recreate the Filename from the Typename
						string ucFilename = typeName[2] + "/" + typeName[1].Split('.')[1].Replace("_", ".");
						if(!System.IO.File.Exists(Context.Server.MapPath(ucFilename))) //original filename must have contained a "_"
						{
							string filePattern = typeName[1].Split('.')[1].Replace("_", "*");
							//due to some strange behaviour of windows you can't use the '?' wildcard to find a '.'... We'll use the * instead,
							string[] files = System.IO.Directory.GetFiles(Context.Server.MapPath(typeName[2]), filePattern);
							if(files.Length == 1)
								ucFilename = typeName[2] + "/" + System.IO.Path.GetFileName(files[0]);
							else
								throw new ApplicationException(string.Format("Could not load UserControl '{2}' from VRoot '{0}' with PersistenceString: {1}. Found {3} files that match the pattern {4}",
									this.Context.Request.ApplicationPath, persistedString[1], ucFilename, files.Length.ToString(), Context.Server.MapPath(typeName[2]) + "\\" + filePattern));
						}
						control = Page.LoadControl(ucFilename);
					}
					break;
				case "C":
					//create a new instance of the control's type
					Type type = Type.GetType(typeName[1], true, true);
					try
					{
						control = (Control) Activator.CreateInstance(type);
					}
					catch(Exception e)
					{
						throw new ArgumentException(String.Format("The type '{0}' cannot be recreated from ViewState", type.ToString()), e);
					}
					break;
				default:
					throw new ArgumentException("Unknown type - cannot recreate from ViewState");
			}

			control.ID = persistedString[2];

			switch(persistedString[0])
			{
					//adding control to "Controls" collection
				case "C":
					parent.Controls.Add(control);
					break;
			}

			//Raise OnControlRestoredEvent
			OnControlRestored(new DynamicControlEventArgs(control));

			//recreate all the child controls
			foreach(Pair pair in (ArrayList) persistInfo.Second)
			{
				RestoreChildStructure(pair, control);
			}
		}

		/// <summary>
		/// Saves a single control and recursively calls itself to save all child controls
		/// </summary>
		/// <param name="control">reference to the control</param>
		/// <param name="controlCollectionName">contains an abbreviation to indicate to which control collection the control belongs</param>
		/// <returns>A pair that contains the controls persisted information in the first property,
		/// and an ArrayList with the child's persisted information in the second property</returns>
		private Pair PersistChildStructure(Control control, string controlCollectionName)
		{
			string typeName;
			ArrayList childPersistInfo = new ArrayList();

			//check if the control has an ID
			if(control.ID == null)
			{
				if(ControlsWithoutIDs == HandleDynamicControls.ThrowException)
					throw new NotSupportedException("DynamicControlsPlaceholder does not support child controls whose ID is not set, as this may have unintended side effects: " + control.GetType().ToString());
				else if (ControlsWithoutIDs == HandleDynamicControls.DontPersist)
					return null;
			}

			if (control is UserControl)
			{
				if (Environment.Version.Major > 1)
					typeName = "UC:" + control.GetType().AssemblyQualifiedName; //in ASP.NET >= 2.0 save the full type name
				else
					typeName = "UC:" + ((UserControl)control).GetType().ToString() + ":" + control.TemplateSourceDirectory; //otherwise get the directory
			}
			else
				typeName = "C:" + control.GetType().AssemblyQualifiedName;

			string persistedString = controlCollectionName + ";" + typeName + ";" + control.ID;

			//childs of a UserControl need not be saved as they are recreated on Page.LoadControl, same for CheckBoxList
			if(!(control is UserControl) && !(control is CheckBoxList))
			{
				//saving all child controls from "Controls" collection
				for(int counter = 0; counter < control.Controls.Count; counter++)
				{
					Control child = control.Controls[counter];
					Pair pair = PersistChildStructure(child, "C");
					if(pair != null)
						childPersistInfo.Add(pair);
				}
			}

			return new Pair(persistedString, childPersistInfo);
		}
		#endregion ViewState management

		#region Render method
		/// <summary>
		/// Renders a copyright box in design mode and calls the base method at runtime
		/// </summary>
		/// <param name="writer"></param>
		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			if(HttpContext.Current == null)
			{
				//removed the following line, because it causes the assembly to require full trust (thanks to Willem Schroers)
				//System.Web.UI.Design.ControlDesigner cd = new System.Web.UI.Design.ControlDesigner(); //create a dummy instance that System.Design.DLL is included as a reference
				
				string designTimeHTML = "<p style=\"font-family: verdana; color: #FFFF99; font-size: xx-small; border: outset 1px #000000; padding: 10 10 10 10; background-color: #5a7ab8\">" +
					"<b>DynamicControlsPlaceholder: \"" + this.ID + "\"</b><br>(c) 2002-2006 by Denis Bauer<br>http://www.denisbauer.com</p>";
				writer.Write(designTimeHTML);
			}
			else
				base.Render(writer);
		}
		#endregion Render method
	}

	/// <summary>
	/// Specifies the possibilities if controls shall be persisted or not
	/// </summary>
	public enum HandleDynamicControls
	{
		/// <summary>
		/// DynamicControl shall not be persisted
		/// </summary>
		DontPersist,
		/// <summary>
		/// DynamicControl shall be persisted
		/// </summary>
		Persist,
		/// <summary>
		/// An Exception shall be thrown
		/// </summary>
		ThrowException
	}

	/// <summary>
	/// Represents the method that will handle any DynamicControl event.
	/// </summary>
	[Serializable]
	public delegate void DynamicControlEventHandler(object sender, DynamicControlEventArgs e);

	/// <summary>
	/// Provides data for the ControlRestored event
	/// </summary>
	public class DynamicControlEventArgs : EventArgs
	{
		private Control _dynamicControl;

		/// <summary>
		/// Gets the referenced Control when the event is raised
		/// </summary>
		public Control DynamicControl
		{
			get { return _dynamicControl; }
		}

		/// <summary>
		/// Initializes a new instance of DynamicControlEventArgs class.
		/// </summary>
		/// <param name="dynamicControl">The control that was just restored.</param>
		public DynamicControlEventArgs(Control dynamicControl)
		{
			_dynamicControl = dynamicControl;
		}
	}
}
