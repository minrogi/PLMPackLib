#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Reflection;

using System.IO;
using System.CodeDom.Compiler;

using TreeDim.UserControls;
using Pic.Plugin;

using ICSharpCode.TextEditor.Actions;
#endregion

namespace Pic.Plugin.GeneratorCtrl
{
    #region GeneratorCtrl
    [Guid("AE77B256-5C3E-47ce-96A5-8E1852E2403F")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Pic.Plugin.GeneratorCtrl.GeneratorCtrl")]
    [ComVisible(true)]
    public partial class GeneratorCtrl : UserControl, IGeneratorCtrl
    {
        #region Data members
        public string _localPluginDirectory;
        private string _pluginPath;
        private IComponentSearchMethod _componentSearchMethod;
        private string _outputPath = string.Empty;
        private bool _outputPathInitialized = false;
        private string _pluginVersion = "2.0.0.0";
        private FindAndReplaceForm _findForm = new FindAndReplaceForm();
        #endregion

        #region PluginValidated event (+ associated delegate)
        public delegate void GeneratorCtlrHandler(object sender, GeneratorCtrlEventArgs e);
        public event GeneratorCtlrHandler PluginValidated;
        #endregion

        #region Constructor
        public GeneratorCtrl()
        {
            InitializeComponent();

            // file select control
            fileSelectCtrl.Filter = "Image files (*.bmp;*.gif;*.jpg;*.png)|*.bmp;*.gif;*.jpg;*.png";
            fileSelectCtrl.Enabled = chbThumbnail.Checked;

            // initialize localPluginDirectory with temp folder path
            _localPluginDirectory = Path.GetTempPath();

            // initialize textbox controls
            txtCompany.Text = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\TreeDim\PicSharp", "PluginCompany", "");
            txtName.Text = "";
            txtDescription.Text = "";
         }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_CHAR = 0x102;
            const int WM_SYSCHAR = 0x106;
            const int WM_SYSKEYDOWN = 0x104;
            const int WM_IME_CHAR = 0x286;
            KeyEventArgs e = null;
            if ((m.Msg != WM_CHAR) && (m.Msg != WM_SYSCHAR) && (m.Msg != WM_IME_CHAR))
            {
                e = new KeyEventArgs(((Keys)((int)((long)m.WParam))) | ModifierKeys);
                if ((m.Msg == WM_KEYDOWN) || (m.Msg == WM_SYSKEYDOWN))
                {
                    TrappedKeyDown(e);
                }

                if (e.Handled)
                    return e.Handled;
            }
            return base.ProcessKeyPreview(ref m);
        }

        public void TrappedKeyDown(KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.F:
                        _findForm.ShowFor(codeEditorCtrl, false);
                        break;
                    case Keys.H:
                        _findForm.ShowFor(codeEditorCtrl, true);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                { 
                    case Keys.F3:
                        _findForm.FindNext(true, false, string.Format("Search text «{0}» not found.", _findForm.LookFor));
                        break;
                    default:
                        break;
                }
            }
            if (e.Shift)
            {
                switch (e.KeyCode)
                { 
                    case Keys.F3:
                        _findForm.FindNext(true, true, string.Format("Search text «{0}» not found.", _findForm.LookFor));
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region Implementation IGeneratorCtrl
        public void setDrawingName(String strDrawingName)
        {
            txtName.Text = strDrawingName;
        }
        public void setDrawingDescription(String strDrawingDescription)
        {
            txtDescription.Text = strDrawingDescription;
        }
        public void setGeneratedSourceCodeFile(String strGeneratedSourceCodeFile)
        {
            try
            {
                // plugin version
                PluginVersion = "2.0.0.0"; // assuming that generated code version is always 2.0.0.0
                // generate new guid
                txtGuid.Text = Guid.NewGuid().ToString();
                // source file path
                codeEditorCtrl.LoadFile(strGeneratedSourceCodeFile);
            }
            catch (Exception exception)
            {
                codeEditorCtrl.Text = exception.ToString();
            }
        }
        public void setComponentDirectory(String strComponentDirectory)
        {
            _localPluginDirectory = strComponentDirectory;
        }
        /// <summary>
        /// Loads a component and get its sources
        /// </summary>
        /// <param name="strGenerator"></param>
        public void setComponentFilePath(string pluginPath)
        {
            // load plugin and get source code
            using (Pic.Plugin.Tools pluginTools = new Pic.Plugin.Tools(pluginPath, ComponentSearchMethod ))
            {
                // path
                _pluginPath = pluginPath;
                // guid
                txtGuid.Text = pluginTools.Guid.ToString();
                // name / description
                txtName.Text = pluginTools.Name;
                txtDescription.Text = pluginTools.Description;
                // source
                codeEditorCtrl.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategyForFile(@"PluginCode.cs");
                codeEditorCtrl.Text = pluginTools.SourceCode;

                // version
                PluginVersion = pluginTools.Version;
            }
        }
        public void setPluginVersion(string pluginVersion)
        {
            _pluginVersion = pluginVersion;
        }
        public string PluginVersion
        {
            get { return _pluginVersion; }
            set { _pluginVersion = value; }
        }
        #endregion

        #region Component search method
        public IComponentSearchMethod ComponentSearchMethod
        {
            set { _componentSearchMethod = value; }
            get
            {
                if (null == _componentSearchMethod)
                {
                    string libraryPath = Path.Combine(_localPluginDirectory, "Library");
                    _componentSearchMethod = new ComponentSearchDirectory(libraryPath);
                }
                return _componentSearchMethod;
            }
        }

        /// <summary>
        /// Output file path
        /// Derives a new file path 
        /// </summary>
        public string OutputPath
        {
            set
            {
                _outputPath = value;
                _outputPathInitialized = !string.IsNullOrEmpty(_outputPath);
            }
            get
            {
                if (!_outputPathInitialized || string.IsNullOrEmpty(_outputPath))
                    _outputPath = Path.Combine(_localPluginDirectory, txtName.Text.Replace(' ', '_') + ".dll");
                return _outputPath;
            }
        }
        #endregion

        #region ActiveX Dll functions
        /// <summary>
        /// Register ActiveX dll function
        /// </summary>
        /// <param name="i_Key">registration key</param>
        [ComRegisterFunction()]
        public static void RegisterClass(string i_Key)
        {
            // strip off HKEY_CLASSES_ROOT\ from the passed key as I don't need it
            StringBuilder sb = new StringBuilder(i_Key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");

            // open the CLSID\{guid} key for write access
            RegistryKey registerKey = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // and create the 'Control' key - this allows it to show up in 
            // the ActiveX control container 
            RegistryKey ctrl = registerKey.CreateSubKey("Control");
            ctrl.Close();

            // next create the CodeBase entry - needed if not string named and GACced.
            RegistryKey inprocServer32 = registerKey.OpenSubKey("InprocServer32", true);
            inprocServer32.SetValue("CodeBase", Assembly.GetExecutingAssembly().CodeBase);
            inprocServer32.Close();

            // finally close the main key
            registerKey.Close();
        }

        /// <summary>
        /// Unregister ActiveX dll function
        /// </summary>
        /// <param name="i_Key"></param>
        [ComUnregisterFunction()]
        public static void UnregisterClass(string i_Key)
        {
            // strip off HKEY_CLASSES_ROOT\ from the passed key as I don't need it
            StringBuilder sb = new StringBuilder(i_Key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");

            // open HKCR\CLSID\{guid} for write access
            RegistryKey registerKey = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // delete the 'Control' key, but don't throw an exception if it does not exist
            registerKey.DeleteSubKey("Control", false);

            // next open up InprocServer32
            RegistryKey inprocServer32 = registerKey.OpenSubKey("InprocServer32", true);

            // and delete the CodeBase key, again not throwing if missing
            inprocServer32.DeleteSubKey("CodeBase", false);

            // finally close the main key
            registerKey.Close();
        }
        #endregion

        #region MAPPING_OF_USER32_DLL_SECTION
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern IntPtr SendMessage(
            int hwnd, uint wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            int hwnd, uint wMsg, int wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            int hwnd, uint wMsg, int wParam, out int lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int GetNbFiles(
            int hwnd, uint wMsg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int GetFileNames(
            int hwnd, uint wMsg,
            [MarshalAs(UnmanagedType.LPArray)]IntPtr[] wParam,
            int lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            int hwnd, uint wMsg, int wParam, StringBuilder lParam);
        #endregion

        #region Event handlers
        private bool ValidateControlContent()
        {
            bool success = true;
            string message = "";
            if (txtCompany.Text.Length == 0)
            {
                success = false;
                message += "\n- a valid company name (field can not be left empty)";
            }
            if (txtName.Text.Length == 0)
            {
                success = false;
                message += "\n- a valid package name";
            }
            if (txtDescription.Text.Length == 0)
            {
                success = false;
                message += "\n- a valid description";
            }
            if (chbThumbnail.Checked && !File.Exists(fileSelectCtrl.FileName))
            {
                success = false;
                message += "\n\nFile " + fileSelectCtrl.FileName + " does not exist.";
            }

            if (!success)
                MessageBox.Show(this, message, "Invalid input", MessageBoxButtons.OK);
            return success;
        }
        private void lvwErrors_onSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvwErrors.SelectedItems.Count > 0)
                {
                    int iLine = Convert.ToInt32(lvwErrors.SelectedItems[0].SubItems[1].Text);
                    if (iLine >= 0)
                        codeEditorCtrl.ActiveTextAreaControl.JumpTo(iLine);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void onGenerate(object sender, EventArgs e)
        {
            try
            {
                // check text boxes content
                if (!ValidateControlContent())
                    return; // invalid content

                // save some control content
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\TreeDim\PicSharp", "PluginCompany", txtCompany.Text);

                // instantiate plugin generator and compile
                PluginGenerator generator = new PluginGenerator();
                generator.AssemblyTitle = txtName.Text;
                generator.AssemblyDescription = txtDescription.Text;
                generator.AssemblyVersion = PluginVersion;
                generator.AssemblyCompany = txtCompany.Text;
                generator.DrawingName = txtName.Text;
                generator.DrawingDescription = txtDescription.Text;
                generator.Guid = new Guid(txtGuid.Text);
                generator.DrawingCode = codeEditorCtrl.Text;
                if (chbThumbnail.Checked)
                    generator.ThumbnailPath = fileSelectCtrl.FileName;
                generator.OutputDirectory = Path.GetTempPath();
                CompilerResults res = generator.Build();
                // show any errors in list view control
                lvwErrors.Items.Clear();
                foreach (CompilerError err in res.Errors)
                {
                    ListViewItem lvi = new ListViewItem(err.ErrorText);
                    lvi.SubItems.Add((err.Line - generator.LineOffset).ToString());
                    lvwErrors.Items.Add(lvi);
                }
                // send event if no errors were found
                if (res.Errors.Count == 0)
                {
                    using (PluginViewer frmPluginViewer = new PluginViewer(_componentSearchMethod, res.PathToAssembly))
                    {
                        if (frmPluginViewer.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                File.Copy(
                                    res.PathToAssembly
                                    , OutputPath
                                    , true /*overwrite*/
                                    );
                            }
                            catch (System.IO.IOException /*ex*/)
                            {
                                MessageBox.Show(string.Format("File {0} appears to be locked. Please, provide a different path for copy", OutputPath), Application.ProductName, MessageBoxButtons.OK);
                                OpenFileDialog fd = new OpenFileDialog();
                                fd.FileName = OutputPath;
                                fd.Filter = "Component (*.dll)|*.dll|All Files|*.*";
                                fd.FilterIndex = 0;

                                if (DialogResult.OK == fd.ShowDialog())
                                    File.Copy(
                                        res.PathToAssembly
                                        , fd.FileName
                                        , true /*overwrite*/
                                        );
                                else
                                    return;
                            }
                            // emit event if an event handler was defined
                            if (null != PluginValidated)
                                PluginValidated(this, new GeneratorCtrlEventArgs(OutputPath));
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void chbThumbnail_CheckedChanged(object sender, EventArgs e)
        {
            fileSelectCtrl.Enabled = chbThumbnail.Checked;
        }
        private void bnNewGuid_Click(object sender, EventArgs e)
        {
            txtGuid.Text = Guid.NewGuid().ToString();
        }
        private void bnComponentRef_Click(object sender, EventArgs e)
        {
            try
            {
                string libraryPath = Path.Combine(_localPluginDirectory, "Library");
                if (!Directory.Exists(libraryPath))
                { 
                    MessageBox.Show( string.Format("Directory {0} does not exist...", libraryPath) );
                    return;
                }
                    
                PluginLibraryBrowser form = new PluginLibraryBrowser(libraryPath);
                if (DialogResult.OK == form.ShowDialog())
                {
                    // tools
                    ComponentLoader loader = new ComponentLoader();
                    loader.SearchMethod = new ComponentSearchDirectory(libraryPath);
                    Component component = loader.LoadComponent(form._guid);

                    Pic.Plugin.Tools tools = new Pic.Plugin.Tools(component, new ComponentSearchDirectory(libraryPath));
                    // insert generated code
                    string sCode = codeEditorCtrl.Text;
                    int startIndex = sCode.IndexOf("factory.AddEntities(fTemp, transform);");
                    sCode = sCode.Insert(startIndex, tools.GetInsertionCode());
                    codeEditorCtrl.Text = sCode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region Find & replace methods
        public void Find()
        {
            _findForm.ShowFor(codeEditorCtrl, false);
        }
        public void FindAndReplace()
        {
            _findForm.ShowFor(codeEditorCtrl, true);
        }
        public void RepeatFind()
        {
            _findForm.FindNext(true, false, string.Format("Search text «{0}» not found.", _findForm.LookFor));
        }
        public void RepeatFindBackward()
        {
            _findForm.FindNext(true, true, string.Format("Search text «{0}» not found.", _findForm.LookFor)); 
        }
        #endregion

        #region Context menu strip events
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            codeEditorCtrl.Undo();
        }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            codeEditorCtrl.Redo();
        }
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteIEditAction(new Cut());
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteIEditAction(new Copy());
        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteIEditAction(new Paste());
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Executes the edit action.
        /// </summary>
        /// <param name="command">The command.</param>
        private void ExecuteIEditAction(IEditAction command)
        {
            command.Execute(codeEditorCtrl.ActiveTextAreaControl.TextArea);
        }
        #endregion
    }
    #endregion

    #region COM Interface IGeneratorCtrl
    /// <summary>
    /// COM Interface - enables to run c# code from c++
    /// </summary>
    [Guid("7DCC2AC8-85C6-4ede-860B-6B793B0C4F89")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [ComVisible(true)]
    public interface IGeneratorCtrl
    {
        void setDrawingName(String strDrawingName);
        void setDrawingDescription(String strDrawingDescription);
        void setGeneratedSourceCodeFile(String strGeneratedSourceCodeFile);
        void setComponentDirectory(String strComponentDirectory);
        void setComponentFilePath(String strFilePath);
        void setPluginVersion(String strVersion);
    }
    #endregion

    #region Public event args
    public class GeneratorCtrlEventArgs
    {
        #region Constructor
        public GeneratorCtrlEventArgs(string outputPath)
        {
            _outputPath = outputPath;
        }
        #endregion

        #region Public properties
        public string OutputPath
        {
            get { return _outputPath; }
        }
        #endregion

        #region Data members
        public string _outputPath;
        #endregion
    }
    #endregion
}
