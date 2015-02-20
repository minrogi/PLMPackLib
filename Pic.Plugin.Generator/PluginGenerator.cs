#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Resources.Tools;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Windows.Forms;
#endregion

namespace Pic.Plugin
{
    public class PluginGenerator
    {
        #region Language enum
        /// <summary>
        /// Language of automatically generated source files
        /// </summary>
        public enum Languages
        {
            VB,
            CSharp
        }
        #endregion

        #region Compiler API helpers
        /// <summary>
        /// Compile a set of source files written in a specified language 
        /// </summary>
        /// <param name="files">Array of source files</param>
        /// <param name="referencedAssemblies">Array of refered assemblies</param>
        /// <param name="outputFilePath">Output assembly file path</param>
        /// <param name="language">Language of source files</param>
        /// <returns>Compiler results</returns>
        public static CompilerResults Compile(List<string> files, List<string> referencedAssemblies, List<string> embeddedResourcesFiles, string outputFilePath, Languages language)
        {
            CodeDomProvider provider = null;

            switch (language)
            {
                case Languages.VB:
                    provider = new Microsoft.VisualBasic.VBCodeProvider();
                    break;
                case Languages.CSharp:
                    provider = new Microsoft.CSharp.CSharpCodeProvider();
                    break;
            }

            return Compile(files, referencedAssemblies, embeddedResourcesFiles, outputFilePath, provider);
        }
        /// <summary>
        /// Compile a set of source files with a specific provider
        /// </summary>
        /// <param name="files">Array of source files</param>
        /// <param name="referencedAssemblies">Array of referenced assemblies</param>
        /// <param name="outputFilePath">Output assembly file path</param>
        /// <param name="provider">Code dom provider</param>
        /// <returns>Compiler results</returns>
        public static CompilerResults Compile(List<string> files, List<string> referencedAssemblies, List<string> embeddedResourcesFiles, string outputFilePath, CodeDomProvider provider)
        {
            // Configure parameters
            CompilerParameters parms = new CompilerParameters();
            parms.GenerateExecutable = false;
            parms.GenerateInMemory = false;
            parms.OutputAssembly = outputFilePath;
            parms.IncludeDebugInformation = false;
            if (provider.Supports(GeneratorSupport.Resources))
                parms.EmbeddedResources.AddRange(embeddedResourcesFiles.ToArray());
            parms.ReferencedAssemblies.AddRange(referencedAssemblies.ToArray());
            // Compile
            return provider.CompileAssemblyFromFile(parms, files.ToArray());
        }
        #endregion

        #region Plugin generation
        /// <summary>
        /// Generate a PicSharp plugin with given values of assemblyTitle, assemblyDescription,
        /// assemblyCompany, assemblyVersion, drawingDescription, drawingCode
        /// </summary>
        /// <returns></returns>
        public CompilerResults Build()
        { 
            // ----------------------------------------------------------------
            // File generation
            string tempPath = Path.GetTempFileName();
            string sourceFilePath = Path.ChangeExtension( tempPath, "cs");

            // get version 
            int iVersion = 3;
            string assemblyVersionNew = "3.0.0.0";

            using (StreamWriter sw = File.CreateText(sourceFilePath))
            {
                lineOffset = 0;
                // write using directives
                sw.WriteLine("#region Using directives"); ++lineOffset;
                sw.WriteLine("using System.Reflection;"); ++lineOffset;
                sw.WriteLine("using System.Runtime.CompilerServices;"); ++lineOffset;
                sw.WriteLine("using System.Runtime.InteropServices;"); ++lineOffset;

                sw.WriteLine("using System;"); ++lineOffset;
                sw.WriteLine("using System.Text;"); ++lineOffset;
                sw.WriteLine("using System.IO;"); ++lineOffset;
                sw.WriteLine("using System.Drawing;"); ++lineOffset;
                sw.WriteLine("using System.Collections.Generic;"); ++lineOffset;
                sw.WriteLine("using Pic.Factory2D;"); ++lineOffset;
                sw.WriteLine("using Sharp3D.Math.Core;"); ++lineOffset;
                sw.WriteLine("#endregion"); ++lineOffset;
                // write assembly properties
                sw.WriteLine("[assembly: AssemblyTitle(\"{0}\")]", assemblyTitle); ++lineOffset;
                sw.WriteLine("[assembly: AssemblyDescription(\"{0}\")]", assemblyDescription); ++lineOffset;
                sw.WriteLine("[assembly: AssemblyConfiguration(\"\")]"); ++lineOffset;
                sw.WriteLine("[assembly: AssemblyCompany(\"{0}\")]", assemblyCompany); ++lineOffset;
                sw.WriteLine("[assembly: AssemblyProduct(\"TreeDim PicParam\")]"); ++lineOffset;
                sw.WriteLine("[assembly: AssemblyCopyright(\"Copyright ©  2011\")]"); ++lineOffset;
                sw.WriteLine("[assembly: AssemblyTrademark(\"TreeDim\")]"); ++lineOffset;
                sw.WriteLine("[assembly: AssemblyCulture(\"\")]"); ++lineOffset;
                sw.WriteLine("[assembly: ComVisible(false)]"); ++lineOffset;
                sw.WriteLine("[assembly: AssemblyVersion(\"{0}\")]", assemblyVersionNew); ++lineOffset;
                sw.WriteLine("[assembly: AssemblyFileVersion(\"{0}\")]", assemblyVersionNew); ++lineOffset;
                // write generic class code
                sw.WriteLine("namespace Pic.Plugin.{0}", drawingName.Replace(' ', '_')); ++lineOffset;
                sw.WriteLine("{"); ++lineOffset;
                if (1 == iVersion)
                {
                    sw.WriteLine("\tpublic class Plugin : Pic.Plugin.IPlugin, Pic.Plugin.IPluginExt1");
                }
                else if (2 == iVersion)
                {
                    sw.WriteLine("\tpublic class Plugin : Pic.Plugin.IPlugin, Pic.Plugin.IPluginExt2"); 
                }
                else if (3 == iVersion)
                {
                    sw.WriteLine("\tpublic class Plugin : Pic.Plugin.IPlugin, Pic.Plugin.IPluginExt3");
                }
                ++lineOffset;
                sw.WriteLine("\t{"); ++lineOffset;
                sw.WriteLine("\t\tpublic Plugin()  {}"); ++lineOffset;
                sw.WriteLine("\t\tIPluginHost myHost = null;"); ++lineOffset;
                sw.WriteLine("\t\tpublic string Description   { get { return \"" + drawingDescription + "\"; } }"); ++lineOffset;
                sw.WriteLine("\t\tpublic string Author        { get { return \"" + assemblyCompany + "\"; } }"); ++lineOffset;
                sw.WriteLine("\t\tpublic string Name          { get { return \"" + drawingName + "\"; } }"); ++lineOffset;
                sw.WriteLine("\t\tpublic string Version       { get { return \"" + assemblyVersionNew + "\"; } }"); ++lineOffset;
                sw.WriteLine("\t\tpublic string SourceCode"); ++lineOffset;
                sw.WriteLine("\t\t{"); ++lineOffset;
                sw.WriteLine("\t\t\tget"); ++lineOffset;
                sw.WriteLine("\t\t\t{"); ++lineOffset;
                sw.WriteLine("\t\t\t\tStream st = Assembly.GetExecutingAssembly().GetManifestResourceStream(\"pluginCode.cs\");"); ++lineOffset;
                sw.WriteLine("\t\t\t\tStreamReader r = new StreamReader(st);"); ++lineOffset;
                sw.WriteLine("\t\t\t\tstring sourceCode = r.ReadToEnd();"); ++lineOffset;
                sw.WriteLine("\t\t\t\tr.Close();"); ++lineOffset;
                sw.WriteLine("\t\t\t\tst.Close();"); ++lineOffset;
                sw.WriteLine("\t\t\t\treturn sourceCode;"); ++lineOffset;
                sw.WriteLine("\t\t\t}"); ++lineOffset;
                sw.WriteLine("\t\t}"); ++lineOffset;
                if (HasThumbnail)
                {
                    sw.WriteLine("\t\tpublic bool HasEmbeddedThumbnail  { get { return true; } }"); ++lineOffset;
                    sw.WriteLine("\t\tpublic Bitmap Thumbnail"); ++lineOffset;
                    sw.WriteLine("\t\t{"); ++lineOffset;
                    sw.WriteLine("\t\t\tget"); ++lineOffset;
                    sw.WriteLine("\t\t\t{"); ++lineOffset;
                    sw.WriteLine("\t\t\t\treturn (Bitmap)Bitmap.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(\"Thumbnail.bmp\"));"); ++lineOffset;
                    sw.WriteLine("\t\t\t}"); ++lineOffset;
                    sw.WriteLine("\t\t}"); ++lineOffset;
                }
                else
                {
                    sw.WriteLine("\t\tpublic bool HasEmbeddedThumbnail  { get { return false; } }"); ++lineOffset;
                    sw.WriteLine("\t\tpublic Bitmap Thumbnail { get { return null; } }"); ++lineOffset;
                }
                sw.WriteLine("\t\tpublic void Initialize()    {}"); ++lineOffset;
                sw.WriteLine("\t\tpublic void Dispose()       {}"); ++lineOffset;
                sw.WriteLine("\t\tpublic IPluginHost Host"); ++lineOffset;
                sw.WriteLine("\t\t{"); ++lineOffset;
                sw.WriteLine("\t\t\tget { return myHost; }   set { myHost = value; }"); ++lineOffset;
                sw.WriteLine("\t\t}"); ++lineOffset;
                sw.WriteLine("\t\tprivate double deg2rad = 2.0 * Math.Asin(1.0) / 180.0;"); ++lineOffset;
                sw.WriteLine("\t\tprivate double rad2deg = 180.0 / (2.0 * Math.Asin(1.0));"); ++lineOffset;
                sw.WriteLine("\t\tprivate double sind(double x)  {   return Math.Sin(x*deg2rad); }"); ++lineOffset;
                sw.WriteLine("\t\tprivate double cosd(double x)  {   return Math.Cos(x*deg2rad); }"); ++lineOffset;
                sw.WriteLine("\t\tprivate double tand(double x)  {   return Math.Tan(x*deg2rad); }"); ++lineOffset;
                sw.WriteLine("\t\tprivate double Sind(double x)  {   return Math.Sin(x*deg2rad); }"); ++lineOffset;
                sw.WriteLine("\t\tprivate double Cosd(double x)  {   return Math.Cos(x*deg2rad); }"); ++lineOffset;
                sw.WriteLine("\t\tprivate double Tand(double x)  {   return Math.Tan(x*deg2rad); }"); ++lineOffset;
                sw.WriteLine("\t\tprivate double sqr(double x)  {  return Math.Sqrt(x); }"); ++lineOffset;
                sw.WriteLine("\t\tprivate double asind(double x)  {  return Math.Asin(x)*rad2deg; }"); ++lineOffset;
                sw.WriteLine("\t\tprivate double acosd(double x)  {  return Math.Acos(x)*rad2deg; }"); ++lineOffset;
                sw.WriteLine("\t\tprivate double atand(double x)  {  return Math.Atan(x*rad2deg); }"); ++lineOffset;
                sw.WriteLine("\t\tprivate double Sqrt(double x)  {  return Math.Sqrt(x); }"); ++lineOffset;
                sw.WriteLine("\t\tprivate double sqrt(double x)  {  return Math.Sqrt(x); }"); ++lineOffset;
                sw.WriteLine("\t\tprivate double Asind(double x)  {  return Math.Asin(x)*rad2deg; }"); ++lineOffset;
                sw.WriteLine("\t\tprivate double Acosd(double x)  {  return Math.Acos(x)*rad2deg; }"); ++lineOffset;
                sw.WriteLine("\t\tprivate double Atand(double x)  {  return Math.Atan(x)*rad2deg; }"); ++lineOffset;
                sw.WriteLine("\t\tprivate double ATan(double x)  {  return Math.Atan(x)*rad2deg; }"); ++lineOffset;
                sw.WriteLine("\t\tprivate double ASin(double x)  {  return Math.Asin(x)*rad2deg; }"); ++lineOffset;
                sw.WriteLine("\t\tprivate double ACos(double x)  {  return Math.Acos(x)*rad2deg; }"); ++lineOffset;
                sw.WriteLine("\t\tpublic Guid Guid { get {  return new Guid(\"" + _guid.ToString() + "\"); } }"); ++lineOffset;
                if (2 == iVersion || 3 == iVersion)
                { sw.WriteLine("\t\tpublic ParameterStack Parameters { get { throw new NotImplementedException(\"Plugin.Parameters not supported with IPluginExt2 or IPluginExt3\"); } }"); } ++lineOffset;
                // write generated code
                sw.WriteLine(drawingCode);
                sw.WriteLine("\t} // class Plugin");
                sw.WriteLine("}");

                // close file
                sw.Close();
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            // save plugin code
            string pluginCodePath = Path.Combine(Path.GetTempPath(), "pluginCode.cs");
            using (StreamWriter sw = File.CreateText(pluginCodePath))
            {
                sw.WriteLine(drawingCode);
                sw.Close();
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            string bitmapPath = Path.Combine(Path.GetTempPath(), "Thumbnail.bmp");
            if (HasThumbnail)
            {
                using (Bitmap bmp = new Bitmap(_thumbnailPath))
                {
                    bmp.Save(bitmapPath, System.Drawing.Imaging.ImageFormat.Bmp);
                }
            }
            // ----------------------------------------------------------------
            // ----------------------------------------------------------------
            // Build
            string applicationDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            // source file
            List<string> sourceFiles = new List<string>();
            sourceFiles.Add(sourceFilePath);
            // embedded resources
            List<string> embeddedResourcesFiles = new List<string>();
            embeddedResourcesFiles.Add(pluginCodePath);
            if (HasThumbnail)
                embeddedResourcesFiles.Add(bitmapPath);
            // referenced assemblies
            List<string> referencedAssemblies = new List<string>();
            referencedAssemblies.Add("System.dll");
            referencedAssemblies.Add("System.Drawing.dll");
            referencedAssemblies.Add(Path.Combine(applicationDirectory, "Sharp3D.Math.dll"));
            referencedAssemblies.Add(Path.Combine(applicationDirectory, "Pic.Factory2D.dll"));
            referencedAssemblies.Add(Path.Combine(applicationDirectory, "Pic.Plugin.PluginInterface.dll"));

            if (null == outputName || 0 == outputName.Length)
                outputName = Guid.NewGuid().ToString().Replace('-', '_')+ ".dll";

            return Compile(
                sourceFiles // files
                , referencedAssemblies // referenced assemblies
                , embeddedResourcesFiles // embedded ressource file
                , Path.Combine(outputDirectory, outputName) // output file path
                , Languages.CSharp // language enum
                );
            // ----------------------------------------------------------------
        }
        #endregion

        #region Properties
        public string AssemblyTitle
        {
            get { return assemblyTitle; }
            set { assemblyTitle = value; }
        }
        public string AssemblyCompany
        {
            get { return assemblyCompany; }
            set { assemblyCompany = value; }
        }
        public string AssemblyVersion
        {
            get { return assemblyVersion; }
            set { assemblyVersion = value;}
        }
        public string AssemblyDescription
        {
            get { return assemblyDescription; }
            set { assemblyDescription = value; }
        }
        public string DrawingName
        {
            get { return drawingName;   }
            set { drawingName = value;  }
        }
        public string DrawingDescription
        {
            get { return drawingDescription; }
            set { drawingDescription = value; }
        }
        public string DrawingCode
        {
            get { return drawingCode; }
            set { drawingCode = value; }
        }
        public string OutputDirectory
        {
            get { return outputDirectory; }
            set { outputDirectory = value; }
        }
        public string OutputName
        {
            get { return outputName; }
            set { outputName = value; }
        }
        public int LineOffset
        {
            get { return lineOffset;     }
        }
        public string ThumbnailPath
        {
            get { return _thumbnailPath; }
            set { _thumbnailPath = value; }
        }
        public bool HasThumbnail
        {
            get
            {
                if (null != _thumbnailPath)
                    return File.Exists(_thumbnailPath);
                else
                    return false;
            }
        }
        public Guid Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }
        #endregion

        #region Data members
        private string assemblyVersion;
        private string assemblyCompany;
        private string assemblyTitle;
        private string assemblyDescription;

        private string drawingName;
        private string drawingDescription;
        private string drawingCode;

        private string outputDirectory;
        private string outputName;
        private int lineOffset;

        private string _thumbnailPath;

        private Guid _guid;
        #endregion

        #region Static methods
        public static bool Regenerate(string inputPath, string outputPath, Guid guid, string name, string description)
        {
            // load existing component
            Pic.Plugin.IComponentSearchMethod searchMethod = null;
            ComponentLoader componentLoader = new ComponentLoader();
            componentLoader.SearchMethod = searchMethod;
            Component comp = componentLoader.LoadComponent(inputPath);

            // instantiate PluginGenerator
            PluginGenerator generator = new PluginGenerator();
            generator.AssemblyCompany = comp.Author;
            generator.AssemblyDescription = comp.Description;
            generator.AssemblyVersion = comp.Version;
            generator.DrawingName = string.IsNullOrEmpty(name) ? comp.Name : name;
            generator.DrawingDescription = string.IsNullOrEmpty(description) ? comp.Description : description;
            generator.Guid = guid != Guid.Empty ? guid : Guid.NewGuid();
            generator.DrawingCode = comp.SourceCode;
            if (comp.HasEmbeddedThumbnail)
            {
                // get thumbnail image
                Bitmap bmp = comp.Thumbnail;
                // save thumbnail image as bmp
                string bitmapPath = Path.ChangeExtension(Path.GetTempFileName(), "bmp");
                bmp.Save(bitmapPath, System.Drawing.Imaging.ImageFormat.Bmp);
                // set thumbnail path in generator
                generator.ThumbnailPath = bitmapPath;
            }
            generator.OutputDirectory = Path.GetTempPath();
            CompilerResults res = generator.Build();
            if (res.Errors.Count > 0)
                return false;

            // copy compiler output to new path
            File.Copy(
                res.PathToAssembly
                , outputPath
                , true /*overwrite*/
                );

            return true;
        }
        #endregion
    }
}
