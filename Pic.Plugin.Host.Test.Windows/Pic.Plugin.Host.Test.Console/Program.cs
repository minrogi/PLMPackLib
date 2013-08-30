#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Pic.Plugin;
using Pic.Factory2D;
using System.Configuration;

using Sharp3D.Math.Core;
using log4net;
using log4net.Config;
#endregion

namespace Pic.Plugin.Host.Test.Console
{
    class Program
    {
        #region Data members
        public static readonly bool launchPaint = false;
        public static readonly ILog _log = LogManager.GetLogger(typeof(Program));
        #endregion

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            try
            {
                // instantiate factory
                PicFactory factory = new PicFactory();
                
                // load plugins
                PluginPathData configData = ConfigurationManager.GetSection("PluginSettings") as PluginPathData;
                if (null == configData)
                    throw new Exception("Failed to load valid PluinPathData object!");
                if (!Directory.Exists(configData.Path))
                    throw new Exception(string.Format("Directory \"{0}\" does not exist.", configData.Path));

                ComponentLoader loader = new ComponentLoader();
                DirectoryInfo dir = new DirectoryInfo(configData.Path);
                foreach (FileInfo fileInfo in dir.GetFiles())
                {
					try
					{
                        Component component = loader.LoadComponent(fileInfo.FullName);
                        if (null == component)
                            continue;
                        System.Console.WriteLine("Name = " + component.Name);
                        System.Console.WriteLine("Description = " + component.Description);
                        System.Console.WriteLine("Author = " + component.Author);
                        System.Console.WriteLine("Source code = " + component.SourceCode);
                        System.Console.WriteLine("------------");
                        // get thumbnail
                        if (component.HasEmbeddedThumbnail)
                        {
                            string thumbPath = Path.Combine(Path.GetTempPath(), component.Name + "_thumbnail.bmp");
                            System.Drawing.Bitmap thumbnail = component.Thumbnail;
                            thumbnail.Save(thumbPath);
                            if (launchPaint)
                                System.Diagnostics.Process.Start(Path.Combine(Environment.SystemDirectory, "mspaint.exe"), "\"" + thumbPath + "\"");
                        }

                        // generate entities
                        ParameterStack stack = component.Parameters;
						factory.Clear();
						component.CreateFactoryEntities(factory, stack);

						// get bounding box
						PicVisitorBoundingBox visitor = new PicVisitorBoundingBox();
						factory.ProcessVisitor(visitor);

						// save
						string filePath = Path.Combine(Path.GetTempPath(), component.Name + ".jpeg");
						PicGraphicsImage picImage = new PicGraphicsImage();
						picImage.ImageSize = new System.Drawing.Size(512, 512);
						Box2D box = visitor.Box;
						box.AddMargin(5);
						picImage.DrawingBox = box;
						factory.Draw(picImage);
						picImage.SaveAs(filePath);

                        if (launchPaint)
						    System.Diagnostics.Process.Start(Path.Combine(Environment.SystemDirectory, "mspaint.exe"), "\"" + filePath + "\"");
					}
					catch (System.Exception ex)
					{
                        _log.Debug(ex.Message);
					}
                }
            }
            catch (System.Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
    }
}
