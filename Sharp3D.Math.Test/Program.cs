#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharp3D.Math.Core;
using Sharp3D.Math.Geometry2D;
#endregion

namespace Sharp3D.Math.Test
{
    class Program
    {
        #region Main
        static void Main(string[] args)
        {
            TestArcToAboveSegment();
            TestArcToAboveArc();
            TestArcToAboveArcTransformed();
            TestSegmentToAboveArc();
        }
        #endregion

        #region Test Vertical distance
        private static void TestArcToAboveSegment()
        {
            Arc arc = new Arc(Vector2D.Zero, 1.0f, -45.0f, -135.0f);
            Segment seg = new Segment(new Vector2D(-10.0f, 10.0f), new Vector2D(10.0f, 10.0f));
            double distance = 0.0;
            VerticalDistance.ArcToAboveSegment(arc, seg, ref distance);
            Console.WriteLine(string.Format("ArcToAboveSegment = {0}", distance));
        }

        private static void TestArcToAboveArc()
        {
            Arc arc0 = new Arc(Vector2D.Zero, 1.0f, -45.0f, -135.0f);
            Arc arc1 = new Arc(new Vector2D(0.0, 10.0), 1.0f, 45.0f, 135.0f);
            double distance = 0.0;
            VerticalDistance.ArcToAboveArc(arc0, arc1, ref distance);
            Console.WriteLine(string.Format("TestArcToAboveArc = {0}", distance));
        }

        private static void TestArcToAboveArcTransformed()
        {
            Transform2D transf = Transform2D.Rotation(90.0);
            Arc arc0 = new Arc(Vector2D.Zero, 1.0f, -45.0f, 45.0f);
            arc0.Transform(transf);
            Arc arc1 = new Arc(new Vector2D(10.0, 0.0), 1.0f, 0.0f, 360.0f);
            arc1.Transform(transf);
            double distance = 0.0;
            VerticalDistance.ArcToAboveArc(arc0, arc1, ref distance);
            Console.WriteLine(string.Format("TestArcToAboveArcTransformed = {0}", distance));
        }

        private static void TestSegmentToAboveArc()
        { 
        }
        #endregion
    }
}
