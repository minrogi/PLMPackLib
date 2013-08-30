#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Sharp3D.Math.Core;
using Pic.Factory2D;

using log4net;
using log4net.Config;
#endregion

namespace Pic.Factory2D.Test
{
    class Program
    {
        protected static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            // set up a simple configuration that logs on the console.
            XmlConfigurator.Configure();

            try
            {
                _log.Info("Pic.Factory2D.Test.exe starting...");
                // testing Sharp3D.Math.Core.Matrix3D
                Transform2D transf0 = Transform2D.Translation(new Vector2D(10.0, 10.0)) * Transform2D.Rotation(45.0);
                Vector2D pt0 = transf0.transform(new Vector2D(100.0, 100.0));
                _log.Info(pt0.ToString());
                Transform2D transf1 = transf0.Inverse();
                Vector2D pt1 = transf1.transform(pt0);
                _log.Info(pt1.ToString());

                // instantiate factory1
                PicFactory factory0 = new PicFactory();
                factory0.AddPoint(PicGraphics.LT.LT_CUT, new Vector2D(0.0, 0.0));
                factory0.AddSegment(PicGraphics.LT.LT_CUT, new Vector2D(50.0, 50.0), new Vector2D(100.0, 100.0));
                factory0.AddSegment(PicGraphics.LT.LT_CUT, new Vector2D(-100.0, 100.0), new Vector2D(100.0, -100.0));
                factory0.AddArc(PicGraphics.LT.LT_CUT, new Vector2D(50.0, 50.0), 50.0 * Math.Sqrt(2.0), 0.0, 360.0);
                factory0.AddArc(PicGraphics.LT.LT_CUT, new Vector2D(75.0, 75.0), 25.0 * Math.Sqrt(2.0), 0.0, 360.0);
                factory0.AddNurb(PicGraphics.LT.LT_CUT);
                _log.Debug(factory0.ToString());

                // get bounding box + draw
                using (PicVisitorBoundingBox visitor = new PicVisitorBoundingBox())
                {
                    factory0.ProcessVisitor(visitor);
                    _log.Info(visitor.Box.ToString());

                    // save as image
                    string filePath = Path.Combine(Path.GetTempPath(), "PicImage0.jpeg");
                    PicGraphicsImage picImage = new PicGraphicsImage();
                    picImage.ImageSize = new System.Drawing.Size(512, 512);
                    Box2D box = visitor.Box;
                    box.AddMargin(5);
                    picImage.DrawingBox = box;
                    factory0.Draw(picImage);
                    picImage.SaveAs(filePath);
                    _log.Debug("File path = " + filePath);
                    _log.Debug("Path = " + Path.Combine(Environment.SystemDirectory, "mspaint.exe"));
                    System.Diagnostics.Process.Start(Path.Combine(Environment.SystemDirectory, "mspaint.exe"), filePath);
                }

                // output to dxf file
                Pic.Factory2D.PicVisitorDxfOutput dxfOutputVisitor = new Pic.Factory2D.PicVisitorDxfOutput();
                factory0.ProcessVisitor(dxfOutputVisitor);

                // load dxf file
                PicFactory factory1 = new PicFactory();
                PicLoaderDxf loaderDxf = new PicLoaderDxf(factory1);
                loaderDxf.Load(@"K:\Codesion\PicSharp\Samples\F1034.EV.DXF");
                loaderDxf.FillFactory();
                // save as image
                // get bounding box + draw
                using (PicVisitorBoundingBox visitor1 = new PicVisitorBoundingBox())
                {
                    factory1.ProcessVisitor(visitor1);
                    _log.Info(visitor1.Box.ToString());
                    string filePath1 = Path.Combine(Path.GetTempPath(), "PicImage1.jpeg");
                    PicGraphicsImage picImage1 = new PicGraphicsImage();
                    picImage1.ImageSize = new System.Drawing.Size(512, 512);
                    Box2D box1 = visitor1.Box;
                    box1.AddMargin(5);
                    picImage1.DrawingBox = box1;
                    factory1.Draw(picImage1);
                    picImage1.SaveAs(filePath1);
                    _log.Debug("File path = " + filePath1);
                    _log.Debug("Path = " + Path.Combine(Environment.SystemDirectory, "mspaint.exe"));
                    System.Diagnostics.Process.Start(Path.Combine(Environment.SystemDirectory, "mspaint.exe"), filePath1);
                }

                // instantiate factory2
                PicFactory factory2 = new PicFactory();
                PicBlock block = factory2.AddBlock(factory0);
                factory2.AddBlockRef(block, new Vector2D(0.0, 0.0), 0.0);
                factory2.AddBlockRef(block, new Vector2D(400.0, 0.0), 0.0);
                factory2.AddBlockRef(block, new Vector2D(0.0, 400.0), 0.0);
                factory2.AddBlockRef(block, new Vector2D(400.0, 400.0), 45.0);

                // get bounding box of factory2
                using (PicVisitorBoundingBox visitor = new PicVisitorBoundingBox())
                {
                    factory2.ProcessVisitor(visitor);
                    _log.Info(visitor.Box.ToString());

                    // save as image
                    string filePath = Path.Combine(Path.GetTempPath(), "PicImage2.jpeg");
                    PicGraphicsImage picImage = new PicGraphicsImage();
                    picImage.ImageSize = new System.Drawing.Size(512, 512);
                    Box2D box = visitor.Box;
                    box.AddMargin(5);
                    picImage.DrawingBox = box;
                    factory2.Draw(picImage);
                    picImage.SaveAs(filePath);
                    _log.Debug("File path = " + filePath);
                    _log.Debug("Path = " + Path.Combine(Environment.SystemDirectory, "mspaint.exe"));
                    System.Diagnostics.Process.Start(Path.Combine(Environment.SystemDirectory, "mspaint.exe"), filePath);
                }

                // compute area
                PicFactory factory3 = new PicFactory();
                factory3.AddSegment(PicGraphics.LT.LT_CUT, new Vector2D(-100.0, -100.0), new Vector2D(100.0, -100.0));
                factory3.AddSegment(PicGraphics.LT.LT_CUT, new Vector2D(100.0, -100.0), new Vector2D(100.0, 100.0));
                factory3.AddSegment(PicGraphics.LT.LT_CUT, new Vector2D(100.0, 100.0), new Vector2D(-100.0, 100.0));
                factory3.AddSegment(PicGraphics.LT.LT_CUT, new Vector2D(-100.0, 100.0), new Vector2D(-100.0, -100.0));

                PicToolArea picToolArea = new PicToolArea();
                factory3.ProcessTool(picToolArea);
                _log.Info(string.Format("Area of factory3 is {0}", picToolArea.Area));

                _log.Info("Pic.Factory2D.Test.exe finishing...");
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
    }
}
