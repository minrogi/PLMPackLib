#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;

using Pic.Plugin;
#endregion

namespace Pic.Plugin.Generator.Test.Console
{
    class Program
    {
        static void help(string[] args)
        {
            System.Console.WriteLine("Usage : Pic.Plugin.Generator.Test.Console.exe sourceFilePath pluginPath");
            System.Console.WriteLine("Actual arguments are:");
            int i = 0;
            foreach (string arg in args)
                System.Console.WriteLine("arg {0}:" + arg, i++);
        }
        static void Main(string[] args)
        {
            try
            {
                // sourceCode1
                {
                    string sourceCodePath = @"K:\GitHub\PLMPackLib\Pic.Plugin.PluginGenerator.Test.Console\sourceCode1.cs";
                    string outputPath = @"K:\GitHub\PLMPackLib\PicPlugins\";
                    string thumbnailPath = string.Empty;
                    if (!File.Exists(sourceCodePath))
                    {
                        System.Console.WriteLine(string.Format("{0} could not be found!", sourceCodePath));
                        return;
                    }
                    // instantiate plugin generator
                    Pic.Plugin.PluginGenerator pluginGenerator = new Pic.Plugin.PluginGenerator();
                    // company / author
                    pluginGenerator.AssemblyCompany = "treeDiM";
                    // description
                    pluginGenerator.AssemblyDescription = "Sample test for plugin generator version 1";
                    // assembly description
                    pluginGenerator.AssemblyTitle = "Generated plugin 1";
                    // version
                    pluginGenerator.AssemblyVersion = "1.0.0.0";
                    // description
                    pluginGenerator.DrawingDescription = "Sample test for plugin generator";
                    // drawing name
                    pluginGenerator.DrawingName = "SampleTest1";
                    pluginGenerator.OutputName = "component1.dll";
                    // read and set source
                    System.IO.StreamReader sr = new System.IO.StreamReader(sourceCodePath, Encoding.ASCII);
                    pluginGenerator.DrawingCode = sr.ReadToEnd();
                    sr.Close();
                    pluginGenerator.OutputDirectory = outputPath;
                    // thumbnail file path
                    if (!string.IsNullOrEmpty(thumbnailPath) && File.Exists(thumbnailPath))
                        pluginGenerator.ThumbnailPath = thumbnailPath;

                    System.CodeDom.Compiler.CompilerResults compilerRes = pluginGenerator.Build();
                    if (compilerRes.Errors.Count != 0)
                    {
                        foreach (System.CodeDom.Compiler.CompilerError error in compilerRes.Errors)
                            System.Console.WriteLine(error.ToString());
                    }
                    else
                        System.Console.WriteLine("Success!");
                }
                // sourceCode2
                {
                    string sourceCodePath = @"K:\GitHub\PLMPackLib\Pic.Plugin.PluginGenerator.Test.Console\sourceCode2.cs";
                    string outputPath = @"K:\GitHub\PLMPackLib\PicPlugins\";
                    string thumbnailPath = string.Empty;
                    if (!File.Exists(sourceCodePath))
                    {
                        System.Console.WriteLine(string.Format("{0} could not be found!", sourceCodePath));
                        return;
                    }

                    // instantiate plugin generator
                    Pic.Plugin.PluginGenerator pluginGenerator = new Pic.Plugin.PluginGenerator();
                    // company / author
                    pluginGenerator.AssemblyCompany = "treeDiM";
                    // description
                    pluginGenerator.AssemblyDescription = "Sample test for plugin generator";
                    // assembly description
                    pluginGenerator.AssemblyTitle = "Generated plugin 2";
                    // version
                    pluginGenerator.AssemblyVersion = "2.0.0.0";
                    // description
                    pluginGenerator.DrawingDescription = "Sample test for plugin generator version 2";
                    // drawing name
                    pluginGenerator.DrawingName = "SampleTest2";
                    // read and set source
                    System.IO.StreamReader sr = new System.IO.StreamReader(sourceCodePath, Encoding.ASCII);
                    pluginGenerator.DrawingCode = sr.ReadToEnd();
                    sr.Close();
                    pluginGenerator.OutputDirectory = outputPath;
                    pluginGenerator.OutputName = "component2.dll";
                    // thumbnail file path
                    if (!string.IsNullOrEmpty(thumbnailPath) && File.Exists(thumbnailPath))
                        pluginGenerator.ThumbnailPath = thumbnailPath;

                    System.CodeDom.Compiler.CompilerResults compilerRes = pluginGenerator.Build();
                    if (compilerRes.Errors.Count != 0)
                    {
                        foreach (System.CodeDom.Compiler.CompilerError error in compilerRes.Errors)
                            System.Console.WriteLine(error.ToString());
                    }
                    else
                        System.Console.WriteLine("Success!");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception:");
                System.Console.WriteLine(ex.ToString());
            }
        }
    }
}
