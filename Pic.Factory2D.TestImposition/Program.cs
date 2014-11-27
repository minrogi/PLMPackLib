#region Using directives
using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;
using System.IO;
using System.Drawing;

using Sharp3D.Math.Core;
using Pic.Factory2D;
using Pic.Plugin;

using log4net;
using log4net.Config;

using TreeDim.UserControls;
#endregion

namespace Pic.Factory2D.TestImposition
{
    class Program
    {
        protected static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            // set up a simple configuration that logs on the console.
            XmlConfigurator.Configure();

            string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            try
            {
                _log.Info(assemblyName + " starting...");

                const PicGraphics.LT ltCut = PicGraphics.LT.LT_CUT;

                // instantiate factory
                PicFactory initialFactory = new PicFactory();

                ParameterStack stack = Program.Parameters;
                Program.CreateFactoryEntities(initialFactory, stack, Transform2D.Identity);
                // imposition
                double width = 1000.0, height = 1000.0;
                ImpositionTool impositionTool = new ImpositionToolCardboardFormat(initialFactory, new CardboardFormat(0, "Format1", "Format1", width, height));
                impositionTool.SpaceBetween = new Vector2D(0.0, 0.0);
                impositionTool.Margin = new Vector2D(0.0, 0.0);

                List<ImpositionSolution> solutions;
                impositionTool.GenerateSortedSolutionList(null, out solutions);
                _log.Info(string.Format("{0} : Solutions", solutions.Count));
                if (solutions.Count <= 0)
                    return;

                foreach (ImpositionSolution chosenSolution in solutions)
                {
                    _log.Info(string.Format("NoRows={0}, NoCols={1}", chosenSolution.Rows, chosenSolution.Cols));

                    PicFactory factoryOut = new PicFactory();
                    chosenSolution.CreateEntities(factoryOut);
                    factoryOut.AddSegment(ltCut, new Vector2D(0.0, 0.0), new Vector2D(width, 0.0));
                    factoryOut.AddSegment(ltCut, new Vector2D(width, 0.0), new Vector2D(width, height));
                    factoryOut.AddSegment(ltCut, new Vector2D(width, height), new Vector2D(0.0, height));
                    factoryOut.AddSegment(ltCut, new Vector2D(0.0, height), new Vector2D(0.0, 0.0));

                    // get bounding box
                    Box2D box = new Box2D();
                    using (PicVisitorBoundingBox visitor = new PicVisitorBoundingBox())
                    {
                        factoryOut.ProcessVisitor(visitor);
                        box = visitor.Box;
                    }

                    string filePath = Path.Combine(Path.GetTempPath(), "Imposition.bmp");
                    PicGraphicsImage picImage = new PicGraphicsImage();
                    picImage.ImageSize = new System.Drawing.Size(4096, 4096);
                    box.AddMargin(1.0);
                    picImage.DrawingBox = box;
                    factoryOut.Draw(picImage);
                    picImage.SaveAs(filePath);

                    System.Diagnostics.Process.Start(Path.Combine(Environment.SystemDirectory, "mspaint.exe"), filePath);

                    _log.Info(assemblyName + " ending...");
                }
             }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
             
        }

        static private double deg2rad = 2.0 * Math.Asin(1.0) / 180.0;
        static private double rad2deg = 180.0 / (2.0 * Math.Asin(1.0));
        static private double sind(double x)  {   return Math.Sin(x*deg2rad); }
        static private double cosd(double x)  {   return Math.Cos(x*deg2rad); }
        static private double tand(double x)  {   return Math.Tan(x*deg2rad); }
        static private double Sind(double x)  {   return Math.Sin(x*deg2rad); }
        static private double Cosd(double x)  {   return Math.Cos(x*deg2rad); }
        static private double Tand(double x) { return Math.Tan(x * deg2rad); }
        static private double sqr(double x) { return Math.Sqrt(x); }
        static private double asind(double x) { return Math.Asin(x) * rad2deg; }
        static private double acosd(double x) { return Math.Acos(x) * rad2deg; }
        static private double atand(double x) { return Math.Atan(x * rad2deg); }
        static private double Sqrt(double x) { return Math.Sqrt(x); }
        static private double Asind(double x) { return Math.Asin(x) * rad2deg; }
        static private double Acosd(double x) { return Math.Acos(x) * rad2deg; }
        static private double Atand(double x) { return Math.Atan(x) * rad2deg; }


        static public ParameterStack Parameters
        {
            get
            {
                ParameterStack stack = new ParameterStack();
                stack.AddDoubleParameter("A", "A", 120, 0);
                stack.AddDoubleParameter("B", "B", 120, 0);
                stack.AddDoubleParameter("H", "H", 200, 0);
                stack.AddDoubleParameter("e", "e", 0.5, 0);
                stack.AddDoubleParameter("g", "g", 15, 0);
                stack.AddDoubleParameter("hc", "hc", 30, 0);
                stack.AddDoubleParameter("pr", "pr", 25, 0);
                return stack;
            }
        }

        static public void CreateFactoryEntities(PicFactory factory, ParameterStack stack, Transform2D transform)
        {
            PicFactory fTemp = new PicFactory();
            const PicGraphics.LT ltCut = PicGraphics.LT.LT_CUT;
            const PicGraphics.LT ltFold = PicGraphics.LT.LT_CREASING;

            // free variables
            double A = stack.GetDoubleParameterValue("A");
            double B = stack.GetDoubleParameterValue("B");
            double H = stack.GetDoubleParameterValue("H");
            double e = stack.GetDoubleParameterValue("e");
            double g = stack.GetDoubleParameterValue("g");
            double hc = stack.GetDoubleParameterValue("hc");
            double pr = stack.GetDoubleParameterValue("pr");

            // formulas
            double hp = B / 2 - e;
            double v9 = g * Tand(15);
            double v1 = 8;
            double v2 = 8;
            double v3 = hp * Tand(15);
            double r = pr / 4;
            SortedList<uint, PicEntity> entities = new SortedList<uint, PicEntity>();

            // segments
            double x0 = 0.0, y0 = 0.0, x1 = 0.0, y1 = 0.0;

            // 3 : (481.462, 303.394) <-> (481.462, 467.206)
            x0 = 69.6211 + g + A + B + A;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            x1 = 69.6211 + g + A + B + A;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            entities.Add(3, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 4 : (223.218, 468.17) <-> (352.341, 468.17)
            x0 = 69.6211 + g + A;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e;
            x1 = 69.6211 + g + A + B;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e;
            entities.Add(4, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 5 : (352.341, 467.206) <-> (480.017, 467.206)
            x0 = 69.6211 + g + A + B;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            x1 = 69.6211 + g + A + B + A - e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            entities.Add(5, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 6 : (352.34, 302.431) <-> (223.218, 302.431)
            x0 = 69.6211 + g + A + B;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e;
            x1 = 69.6211 + g + A;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e;
            entities.Add(6, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 7 : (352.34, 303.394) <-> (480.017, 303.394)
            x0 = 69.6211 + g + A + B;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            x1 = 69.6211 + g + A + B + A - e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            entities.Add(7, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 8 : (223.218, 240.761) <-> (101.323, 240.761)
            x0 = 69.6211 + g + A;
            y0 = 120.793 + g + B / 2 - 2 * e + hc;
            x1 = 69.6211 + g + e + v2;
            y1 = 120.793 + g + B / 2 - 2 * e + hc;
            entities.Add(8, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 9 : (223.218, 211.853) <-> (101.323, 211.853)
            x0 = 69.6211 + g + A;
            y0 = 120.793 + g + B / 2 - 2 * e;
            x1 = 69.6211 + g + e + v2;
            y1 = 120.793 + g + B / 2 - 2 * e;
            entities.Add(9, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 10 : (352.34, 211.853) <-> (474.235, 211.853)
            x0 = 69.6211 + g + A + B;
            y0 = 120.793 + g + B / 2 - 2 * e;
            x1 = 69.6211 + g + A + B + A - e - v2;
            y1 = 120.793 + g + B / 2 - 2 * e;
            entities.Add(10, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 11 : (352.34, 240.761) <-> (474.235, 240.761)
            x0 = 69.6211 + g + A + B;
            y0 = 120.793 + g + B / 2 - 2 * e + hc;
            x1 = 69.6211 + g + A + B + A - e - v2;
            y1 = 120.793 + g + B / 2 - 2 * e + hc;
            entities.Add(11, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 12 : (352.341, 530.804) <-> (461.326, 530.804)
            x0 = 69.6211 + g + A + B;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + B / 2 - e;
            x1 = 69.6211 + g + A + B + A - e - v3;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + B / 2 - e;
            entities.Add(12, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 13 : (94.0963, 467.206) <-> (69.6211, 462.388)
            x0 = 69.6211 + g;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            x1 = 69.6211;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H - v9;
            entities.Add(13, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 14 : (69.6211, 308.213) <-> (69.6211, 462.388)
            x0 = 69.6211;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + v9;
            x1 = 69.6211;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H - v9;
            entities.Add(14, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 15 : (94.0963, 303.395) <-> (69.621, 308.213)
            x0 = 69.6211 + g;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            x1 = 69.6211;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + v9;
            entities.Add(15, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 16 : (223.218, 303.394) <-> (95.542, 303.394)
            x0 = 69.6211 + g + A;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            x1 = 69.6211 + g + e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            entities.Add(16, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 17 : (223.218, 467.206) <-> (95.5415, 467.206)
            x0 = 69.6211 + g + A;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            x1 = 69.6211 + g + e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            entities.Add(17, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 18 : (352.34, 149.701) <-> (474.235, 149.701)
            x0 = 69.6211 + g + A + B;
            y0 = 120.793 + g;
            x1 = 69.6211 + g + A + B + A - e - v2;
            y1 = 120.793 + g;
            entities.Add(18, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 19 : (466.489, 120.793) <-> (360.086, 120.793)
            x0 = 69.6211 + g + A + B + A - e - v2 - v9;
            y0 = 120.793;
            x1 = 69.6211 + g + A + B + v9;
            y1 = 120.793;
            entities.Add(19, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 20 : (480.017, 467.206) <-> (480.017, 476.842)
            x0 = 69.6211 + g + A + B + A - e;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            x1 = 69.6211 + g + A + B + A - e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + hp / 5;
            entities.Add(20, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 21 : (474.235, 482.623) <-> (480.017, 476.842)
            x0 = 69.6211 + g + A + B + A - e - v2;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + hp / 5 + v2;
            x1 = 69.6211 + g + A + B + A - e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + hp / 5;
            entities.Add(21, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 22 : (474.235, 482.623) <-> (461.326, 530.804)
            x0 = 69.6211 + g + A + B + A - e - v2;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + hp / 5 + v2;
            x1 = 69.6211 + g + A + B + A - e - v3;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + B / 2 - e;
            entities.Add(22, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 23 : (223.218, 530.804) <-> (114.233, 530.804)
            x0 = 69.6211 + g + A;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + B / 2 - e;
            x1 = 69.6211 + g + e + v3;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + B / 2 - e;
            entities.Add(23, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 24 : (95.5415, 467.206) <-> (95.5415, 476.842)
            x0 = 69.6211 + g + e;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            x1 = 69.6211 + g + e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + hp / 5;
            entities.Add(24, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 25 : (101.323, 482.624) <-> (95.5415, 476.842)
            x0 = 69.6211 + g + e + v2;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + hp / 5 + v2;
            x1 = 69.6211 + g + e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + hp / 5;
            entities.Add(25, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 26 : (101.323, 482.624) <-> (114.233, 530.804)
            x0 = 69.6211 + g + e + v2;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + hp / 5 + v2;
            x1 = 69.6211 + g + e + v3;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + B / 2 - e;
            entities.Add(26, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 27 : (223.218, 149.701) <-> (101.323, 149.701)
            x0 = 69.6211 + g + A;
            y0 = 120.793 + g;
            x1 = 69.6211 + g + e + v2;
            y1 = 120.793 + g;
            entities.Add(27, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 28 : (215.473, 120.793) <-> (109.069, 120.793)
            x0 = 69.6211 + g + A - v9;
            y0 = 120.793;
            x1 = 69.6211 + g + e + v2 + v9;
            y1 = 120.793;
            entities.Add(28, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 29 : (480.017, 303.394) <-> (480.017, 293.759)
            x0 = 69.6211 + g + A + B + A - e;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            x1 = 69.6211 + g + A + B + A - e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - hp / 5;
            entities.Add(29, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 30 : (474.235, 287.977) <-> (480.017, 293.759)
            x0 = 69.6211 + g + A + B + A - e - v2;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - hp / 5 - v2;
            x1 = 69.6211 + g + A + B + A - e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - hp / 5;
            entities.Add(30, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 31 : (95.542, 303.394) <-> (95.542, 293.759)
            x0 = 69.6211 + g + e;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            x1 = 69.6211 + g + e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - hp / 5;
            entities.Add(31, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 32 : (101.323, 287.978) <-> (95.542, 293.759)
            x0 = 69.6211 + g + e + v2;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - hp / 5 - v2;
            x1 = 69.6211 + g + e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - hp / 5;
            entities.Add(32, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 33 : (101.323, 149.701) <-> (109.069, 120.793)
            x0 = 69.6211 + g + e + v2;
            y0 = 120.793 + g;
            x1 = 69.6211 + g + e + v2 + v9;
            y1 = 120.793;
            entities.Add(33, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 34 : (223.218, 149.701) <-> (215.473, 120.793)
            x0 = 69.6211 + g + A;
            y0 = 120.793 + g;
            x1 = 69.6211 + g + A - v9;
            y1 = 120.793;
            entities.Add(34, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 35 : (352.34, 149.699) <-> (360.086, 120.793)
            x0 = 69.6211 + g + A + B;
            y0 = 120.793 + g;
            x1 = 69.6211 + g + A + B + v9;
            y1 = 120.793;
            entities.Add(35, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 36 : (474.235, 149.701) <-> (466.489, 120.793)
            x0 = 69.6211 + g + A + B + A - e - v2;
            y0 = 120.793 + g;
            x1 = 69.6211 + g + A + B + A - e - v2 - v9;
            y1 = 120.793;
            entities.Add(36, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 37 : (609.621, 303.394) <-> (609.621, 467.206)
            x0 = 69.6211 + g + A + B + A + B - e;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            x1 = 69.6211 + g + A + B + A + B - e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            entities.Add(37, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 38 : (94.0963, 467.206) <-> (95.5415, 467.206)
            x0 = 69.6211 + g;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            x1 = 69.6211 + g + e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            entities.Add(38, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 39 : (94.0963, 303.394) <-> (95.542, 303.394)
            x0 = 69.6211 + g;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            x1 = 69.6211 + g + e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            entities.Add(39, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 40 : (94.0963, 303.394) <-> (94.0963, 467.206)
            x0 = 69.6211 + g;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            x1 = 69.6211 + g;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            entities.Add(40, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 41 : (236.709, 594.401) <-> (338.85, 594.401)
            x0 = 69.6211 + g + A + 2 * e + v1;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A;
            x1 = 69.6211 + g + A + B - 2 * e - v1;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A;
            entities.Add(41, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 42 : (340.524, 625.236) <-> (235.034, 625.236)
            x0 = 69.6211 + g + A + B - 2 * e - v1 + 1.6741;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A + pr;
            x1 = 69.6211 + g + A + 2 * e + v1 - 1.67455;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A + pr;
            entities.Add(42, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 43 : (348.524, 617.236) <-> (348.524, 596.328)
            x0 = 69.6211 + g + A + B - 2 * e;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A + pr - 8.00006;
            x1 = 69.6211 + g + A + B - 2 * e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A + e;
            entities.Add(43, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 59 : (227.034, 617.236) <-> (227.034, 596.328)
            x0 = 69.6211 + g + A + 2 * e;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A + pr - 8;
            x1 = 69.6211 + g + A + 2 * e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A + e;
            entities.Add(59, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 69 : (338.85, 176.2) <-> (236.709, 176.2)
            x0 = 69.6211 + g + A + B - 2 * e - v1;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A;
            x1 = 69.6211 + g + A + 2 * e + v1;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A;
            entities.Add(69, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 70 : (235.035, 145.365) <-> (339.525, 145.364)
            x0 = 69.6211 + g + A + 2 * e + v1 - 1.67406;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A - pr;
            x1 = 69.6211 + g + A + B - 2 * e - v1 + 0.674561;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A - pr;
            entities.Add(70, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 71 : (227.035, 153.365) <-> (227.035, 174.272)
            x0 = 69.6211 + g + A + 2 * e;
            y0 = 120.793 + g + 3.66388;
            x1 = 69.6211 + g + A + 2 * e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A - e;
            entities.Add(71, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 73 : (348.525, 174.272) <-> (348.525, 154.364)
            x0 = 69.6211 + g + A + B - 2 * e;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A - e;
            x1 = 69.6211 + g + A + B - 2 * e;
            y1 = 120.793 + g + 4.66336;
            entities.Add(73, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 98 : (227.034, 596.328) <-> (223.218, 596.328)
            x0 = 69.6211 + g + A + 2 * e;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A + e;
            x1 = 69.6211 + g + A;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A + e;
            entities.Add(98, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 99 : (236.709, 596.328) <-> (227.034, 596.328)
            x0 = 69.6211 + g + A + 2 * e + v1;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A + e;
            x1 = 69.6211 + g + A + 2 * e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A + e;
            entities.Add(99, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 100 : (352.341, 303.394) <-> (352.341, 467.206)
            x0 = 69.6211 + g + A + B;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            x1 = 69.6211 + g + A + B;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            entities.Add(100, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 101 : (352.341, 467.206) <-> (352.341, 467.399)
            x0 = 69.6211 + g + A + B;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            x1 = 69.6211 + g + A + B;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            entities.Add(101, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 102 : (223.218, 303.394) <-> (223.218, 467.206)
            x0 = 69.6211 + g + A;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            x1 = 69.6211 + g + A;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            entities.Add(102, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 103 : (223.218, 467.206) <-> (223.218, 467.399)
            x0 = 69.6211 + g + A;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            x1 = 69.6211 + g + A;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            entities.Add(103, fTemp.AddSegment(ltFold, 1, 1, x0, y0, x1, y1));

            // 104 : (480.017, 467.206) <-> (481.462, 467.206)
            x0 = 69.6211 + g + A + B + A - e;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            x1 = 69.6211 + g + A + B + A;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            entities.Add(104, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 105 : (481.462, 467.206) <-> (609.621, 467.206)
            x0 = 69.6211 + g + A + B + A;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            x1 = 69.6211 + g + A + B + A + B - e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            entities.Add(105, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 106 : (480.017, 303.394) <-> (481.462, 303.394)
            x0 = 69.6211 + g + A + B + A - e;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            x1 = 69.6211 + g + A + B + A;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            entities.Add(106, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 107 : (481.462, 303.394) <-> (609.621, 303.394)
            x0 = 69.6211 + g + A + B + A;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            x1 = 69.6211 + g + A + B + A + B - e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            entities.Add(107, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 108 : (352.341, 468.17) <-> (352.341, 467.399)
            x0 = 69.6211 + g + A + B;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e;
            x1 = 69.6211 + g + A + B;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            entities.Add(108, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 109 : (352.341, 530.804) <-> (352.341, 468.17)
            x0 = 69.6211 + g + A + B;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + B / 2 - e;
            x1 = 69.6211 + g + A + B;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e;
            entities.Add(109, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 110 : (352.341, 596.328) <-> (352.341, 530.804)
            x0 = 69.6211 + g + A + B;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A + e;
            x1 = 69.6211 + g + A + B;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + B / 2 - e;
            entities.Add(110, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 111 : (223.218, 467.399) <-> (223.218, 468.17)
            x0 = 69.6211 + g + A;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H;
            x1 = 69.6211 + g + A;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e;
            entities.Add(111, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 112 : (223.218, 468.17) <-> (223.218, 530.804)
            x0 = 69.6211 + g + A;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e;
            x1 = 69.6211 + g + A;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + B / 2 - e;
            entities.Add(112, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 113 : (223.218, 530.804) <-> (223.218, 596.328)
            x0 = 69.6211 + g + A;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + B / 2 - e;
            x1 = 69.6211 + g + A;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A + e;
            entities.Add(113, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 114 : (474.235, 149.701) <-> (474.235, 211.853)
            x0 = 69.6211 + g + A + B + A - e - v2;
            y0 = 120.793 + g;
            x1 = 69.6211 + g + A + B + A - e - v2;
            y1 = 120.793 + g + B / 2 - 2 * e;
            entities.Add(114, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 115 : (474.235, 211.853) <-> (474.235, 240.761)
            x0 = 69.6211 + g + A + B + A - e - v2;
            y0 = 120.793 + g + B / 2 - 2 * e;
            x1 = 69.6211 + g + A + B + A - e - v2;
            y1 = 120.793 + g + B / 2 - 2 * e + hc;
            entities.Add(115, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 116 : (474.235, 240.761) <-> (474.235, 287.977)
            x0 = 69.6211 + g + A + B + A - e - v2;
            y0 = 120.793 + g + B / 2 - 2 * e + hc;
            x1 = 69.6211 + g + A + B + A - e - v2;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - hp / 5 - v2;
            entities.Add(116, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 117 : (101.323, 149.701) <-> (101.323, 211.853)
            x0 = 69.6211 + g + e + v2;
            y0 = 120.793 + g;
            x1 = 69.6211 + g + e + v2;
            y1 = 120.793 + g + B / 2 - 2 * e;
            entities.Add(117, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 118 : (101.323, 211.853) <-> (101.323, 240.761)
            x0 = 69.6211 + g + e + v2;
            y0 = 120.793 + g + B / 2 - 2 * e;
            x1 = 69.6211 + g + e + v2;
            y1 = 120.793 + g + B / 2 - 2 * e + hc;
            entities.Add(118, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 119 : (101.323, 240.761) <-> (101.323, 287.978)
            x0 = 69.6211 + g + e + v2;
            y0 = 120.793 + g + B / 2 - 2 * e + hc;
            x1 = 69.6211 + g + e + v2;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - hp / 5 - v2;
            entities.Add(119, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 120 : (223.218, 149.701) <-> (223.218, 174.272)
            x0 = 69.6211 + g + A;
            y0 = 120.793 + g;
            x1 = 69.6211 + g + A;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A - e;
            entities.Add(120, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 121 : (223.218, 174.272) <-> (223.218, 211.853)
            x0 = 69.6211 + g + A;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A - e;
            x1 = 69.6211 + g + A;
            y1 = 120.793 + g + B / 2 - 2 * e;
            entities.Add(121, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 122 : (223.218, 211.853) <-> (223.218, 240.761)
            x0 = 69.6211 + g + A;
            y0 = 120.793 + g + B / 2 - 2 * e;
            x1 = 69.6211 + g + A;
            y1 = 120.793 + g + B / 2 - 2 * e + hc;
            entities.Add(122, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 123 : (223.218, 240.761) <-> (223.218, 302.431)
            x0 = 69.6211 + g + A;
            y0 = 120.793 + g + B / 2 - 2 * e + hc;
            x1 = 69.6211 + g + A;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e;
            entities.Add(123, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 124 : (223.218, 302.431) <-> (223.218, 303.394)
            x0 = 69.6211 + g + A;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e;
            x1 = 69.6211 + g + A;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            entities.Add(124, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 125 : (352.34, 149.701) <-> (352.34, 174.272)
            x0 = 69.6211 + g + A + B;
            y0 = 120.793 + g;
            x1 = 69.6211 + g + A + B;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A - e;
            entities.Add(125, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 126 : (352.34, 174.272) <-> (352.34, 211.853)
            x0 = 69.6211 + g + A + B;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A - e;
            x1 = 69.6211 + g + A + B;
            y1 = 120.793 + g + B / 2 - 2 * e;
            entities.Add(126, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 127 : (352.34, 211.853) <-> (352.34, 240.761)
            x0 = 69.6211 + g + A + B;
            y0 = 120.793 + g + B / 2 - 2 * e;
            x1 = 69.6211 + g + A + B;
            y1 = 120.793 + g + B / 2 - 2 * e + hc;
            entities.Add(127, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 128 : (352.34, 240.761) <-> (352.34, 302.431)
            x0 = 69.6211 + g + A + B;
            y0 = 120.793 + g + B / 2 - 2 * e + hc;
            x1 = 69.6211 + g + A + B;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e;
            entities.Add(128, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 129 : (352.34, 302.431) <-> (352.34, 303.394)
            x0 = 69.6211 + g + A + B;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e;
            x1 = 69.6211 + g + A + B;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e;
            entities.Add(129, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 130 : (338.85, 596.328) <-> (348.524, 596.328)
            x0 = 69.6211 + g + A + B - 2 * e - v1;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A + e;
            x1 = 69.6211 + g + A + B - 2 * e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A + e;
            entities.Add(130, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 131 : (348.524, 596.328) <-> (352.341, 596.328)
            x0 = 69.6211 + g + A + B - 2 * e;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A + e;
            x1 = 69.6211 + g + A + B;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A + e;
            entities.Add(131, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 132 : (338.85, 592.474) <-> (338.85, 594.401)
            x0 = 69.6211 + g + A + B - 2 * e - v1;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A - e;
            x1 = 69.6211 + g + A + B - 2 * e - v1;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A;
            entities.Add(132, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 133 : (338.85, 594.401) <-> (338.85, 596.328)
            x0 = 69.6211 + g + A + B - 2 * e - v1;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A;
            x1 = 69.6211 + g + A + B - 2 * e - v1;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A + e;
            entities.Add(133, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 134 : (236.709, 594.401) <-> (236.709, 592.474)
            x0 = 69.6211 + g + A + 2 * e + v1;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A;
            x1 = 69.6211 + g + A + 2 * e + v1;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A - e;
            entities.Add(134, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 135 : (236.709, 596.328) <-> (236.709, 594.401)
            x0 = 69.6211 + g + A + 2 * e + v1;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A + e;
            x1 = 69.6211 + g + A + 2 * e + v1;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e + H + e + A;
            entities.Add(135, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 136 : (338.85, 174.272) <-> (348.525, 174.272)
            x0 = 69.6211 + g + A + B - 2 * e - v1;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A - e;
            x1 = 69.6211 + g + A + B - 2 * e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A - e;
            entities.Add(136, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 137 : (348.525, 174.272) <-> (352.341, 174.272)
            x0 = 69.6211 + g + A + B - 2 * e;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A - e;
            x1 = 69.6211 + g + A + B;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A - e;
            entities.Add(137, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 138 : (227.035, 174.272) <-> (223.218, 174.272)
            x0 = 69.6211 + g + A + 2 * e;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A - e;
            x1 = 69.6211 + g + A;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A - e;
            entities.Add(138, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 139 : (236.709, 174.272) <-> (227.035, 174.272)
            x0 = 69.6211 + g + A + 2 * e + v1;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A - e;
            x1 = 69.6211 + g + A + 2 * e;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A - e;
            entities.Add(139, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 140 : (236.709, 176.2) <-> (236.709, 174.272)
            x0 = 69.6211 + g + A + 2 * e + v1;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A;
            x1 = 69.6211 + g + A + 2 * e + v1;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A - e;
            entities.Add(140, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 141 : (236.709, 178.127) <-> (236.709, 176.2)
            x0 = 69.6211 + g + A + 2 * e + v1;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A + e;
            x1 = 69.6211 + g + A + 2 * e + v1;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A;
            entities.Add(141, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 142 : (338.85, 174.272) <-> (338.85, 176.2)
            x0 = 69.6211 + g + A + B - 2 * e - v1;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A - e;
            x1 = 69.6211 + g + A + B - 2 * e - v1;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A;
            entities.Add(142, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // 143 : (338.85, 176.2) <-> (338.85, 178.127)
            x0 = 69.6211 + g + A + B - 2 * e - v1;
            y0 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A;
            x1 = 69.6211 + g + A + B - 2 * e - v1;
            y1 = 120.793 + g + B / 2 - 2 * e + hc + B / 2 - e - e - A + e;
            entities.Add(143, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1));

            // arcs
            // 44 : radius = 8  s0 = 42  s1 = 43
            fTemp.ProcessTool(new PicToolRound(
                  entities[42]
                , entities[43]
                , r						// radius
                ));
            // 60 : radius = 8  s0 = 42  s1 = 59
            fTemp.ProcessTool(new PicToolRound(
                  entities[42]
                , entities[59]
                , r						// radius
                ));
            // 72 : radius = 8  s0 = 70  s1 = 71
            fTemp.ProcessTool(new PicToolRound(
                  entities[70]
                , entities[71]
                , r						// radius
                ));
            // 144 : radius = 9  s0 = 70  s1 = 73
            fTemp.ProcessTool(new PicToolRound(
                  entities[70]
                , entities[73]
                , r						// radius
                ));

            factory.AddEntities(fTemp, transform);
        }
    }
}
