#region Using directives
using System;
using System.Collections.Generic;
using System.Text;

using Sharp3D.Math.Core;
using Sharp3D.Math.Geometry2D;
#endregion

namespace Pic.Factory2D
{
    public class PicToolRound : PicTool
    {
        #region Private fields
        private PicSegment _picSeg0, _picSeg1;
        private double _radius;
        #endregion

        #region Constructor
        /// <summary>
        /// instantiate the tool with 2 segments and a radius
        /// </summary>
        /// <param name="seg0">first segment (order does not matter)</param>
        /// <param name="seg1">second segment (order does not matter)</param>
        /// <param name="radius"></param>
        public PicToolRound(PicEntity seg0, PicEntity seg1, double radius)
        {
            _picSeg0 = seg0 as PicSegment;
            _picSeg1 = seg1 as PicSegment;
            _radius = radius;
        }
        #endregion

        #region Public override of Process factory
        /// <summary>
        /// 
        /// </summary>
        /// <param name="factory"></param>
        public override void ProcessFactory(PicFactory factory)
        {
            // will only work with 2 segments
            if (null == _picSeg0 || null == _picSeg1)
                throw new NotImplementedException("Only segment entities are supported");

            // get inner segments
            Segment seg0 = new Segment(_picSeg0.Pt0, _picSeg0.Pt1);
            Segment seg1 = new Segment(_picSeg1.Pt0, _picSeg1.Pt1);

            // find line intersection and extend segments so that they intersect
            Intersection2D interobj;
            if (!IntersectMethods.IntersectLines(seg0, seg1, out interobj) || interobj == null || interobj.Type != Intersection2D.IntersectionType.I2D_POINT)  return;
            Vector2D ptExt = (Vector2D)interobj.Result;
            seg0 = extendSegment(seg0, ptExt);
            seg1 = extendSegment(seg1, ptExt);

            // create parallel segments
            Segment seg00, seg01;
            getParalleleSegments(seg0, _radius, out seg00, out seg01);
            Segment seg10, seg11;
            getParalleleSegments(seg1, _radius, out seg10, out seg11);

            // compute intersection
            if (IntersectMethods.Intersect(seg00, seg10, out interobj)) { }
            else if (IntersectMethods.Intersect(seg00, seg11, out interobj)) { }
            else if (IntersectMethods.Intersect(seg01, seg10, out interobj)) { }
            else if (IntersectMethods.Intersect(seg01, seg11, out interobj)) { }

            if (interobj == null || interobj.Type != Intersection2D.IntersectionType.I2D_POINT) return;
            Vector2D ptCenter = (Vector2D)interobj.Result;

            // get intersection of normal with seg0
            if (IntersectMethods.Intersect(seg0, new Segment(ptCenter, ptCenter + new Vector2D(-(seg0.P1 - seg0.P0).Y, (seg0.P1 - seg0.P0).X)), out interobj)) { }
            else if (IntersectMethods.Intersect(seg0, new Segment(ptCenter, ptCenter + new Vector2D((seg0.P1 - seg0.P0).Y, -(seg0.P1 - seg0.P0).X)), out interobj)) { }
            else return;
            if (interobj == null && interobj.Type != Intersection2D.IntersectionType.I2D_POINT) return;
            Vector2D pt0 = (Vector2D)interobj.Result;

            if (IntersectMethods.Intersect(seg1, new Segment(ptCenter, ptCenter + new Vector2D(-(seg1.P1 - seg1.P0).Y, (seg1.P1 - seg1.P0).X)), out interobj)) { }
            else if (IntersectMethods.Intersect(seg1, new Segment(ptCenter, ptCenter + new Vector2D((seg1.P1 - seg1.P0).Y, -(seg1.P1 - seg1.P0).X)), out interobj)) { }
            else return;
            if (interobj == null && interobj.Type != Intersection2D.IntersectionType.I2D_POINT) return;
            Vector2D pt1 = (Vector2D)interobj.Result;

            // intersection of 2D segments
            if (!IntersectMethods.Intersect(seg0, seg1, out interobj)) return;
            if (interobj == null && interobj.Type != Intersection2D.IntersectionType.I2D_POINT) return;
            Vector2D ptInter = (Vector2D)interobj.Result;

            // modify segments
            if ((seg0.P0 - ptInter).GetLength() < (seg0.P1 - ptInter).GetLength())
            {
                _picSeg0.Pt0 = pt0;
                _picSeg0.Pt1 = seg0.P1;
            }
            else
            {
                _picSeg0.Pt0 = pt0;
                _picSeg0.Pt1 = seg0.P0;
            }

            if ((seg1.P0 - ptInter).GetLength() < (seg1.P1 - ptInter).GetLength())
            {
                _picSeg1.Pt0 = pt1;
                _picSeg1.Pt1 = seg1.P1;
            }
            else
            {
                _picSeg1.Pt0 = pt1;
                _picSeg1.Pt1 = seg1.P0;
            }

            // create new arc of circle
            if (Vector2D.KrossProduct(pt0 - ptCenter, pt1 - ptCenter) > 0) // go from pt0 to pt1
                factory.AddArc(_picSeg0.LineType, _picSeg0.Group, _picSeg0.Layer, ptCenter, pt0, pt1);
            else // from pt1 to pt0
                factory.AddArc(_picSeg0.LineType, _picSeg0.Group, _picSeg0.Layer, ptCenter, pt1, pt0);
        }
        #endregion

        #region Static helper functions
        /// <summary>
        /// compute the two parallel segments at a certain distance 
        /// </summary>
        /// <param name="segInit">initial segment</param>
        /// <param name="dist">distance</param>
        /// <param name="s0">first segment (out)</param>
        /// <param name="s1">second segment (out)</param>
        static private void getParalleleSegments(Segment segInit, double dist, out Segment s0, out Segment s1)
        {
            // get dir vector
            Vector2D vecDir = segInit.P1 - segInit.P0;
            vecDir /= vecDir.GetLength();

            // vecNormal
            Vector2D vecNorm = new Vector2D(-vecDir.Y, vecDir.X);

            // compute segment
            Vector2D p00 = segInit.P0 + dist * vecNorm;
            s0 = new Segment(p00, p00 + segInit.P1 - segInit.P0);
            Vector2D p10 = segInit.P0 - dist * vecNorm;
            s1 = new Segment(p10, p10 + segInit.P1 - segInit.P0);
        }
        /// <summary>
        /// get an extended segment using given point if it is not already on the segment
        /// </summary>
        /// <param name="segInit">initial segment</param>
        /// <param name="pt">point to use in order to extend segment</param>
        /// <returns>a segment which contains both the initial segment and the given point</returns>
        static private Segment extendSegment(Segment segInit, Vector2D pt)
        { 
            Vector2D vecSeg = segInit.P1 - segInit.P0;
            double prod0 = Vector2D.DotProduct(segInit.P0 - pt, vecSeg);
            double prod1 = Vector2D.DotProduct(segInit.P1 - pt, vecSeg);
            if (prod0 > 0 && prod1 > 0)
                return new Segment(pt, segInit.P1); // extend segment on the P0 side
            else if (prod0 < 0 && prod1 < 0)
                return new Segment(pt, segInit.P0); // extend segment on the P1 side
            else
                return new Segment(segInit);    // do not need to extend segment
        }
        /// <summary>
        /// convert a float version Vector2F into a double version Vector2D
        /// </summary>
        /// <param name="vec">Vector2F to convert</param>
        /// <returns>Vector2D</returns>
        static public Vector2D Vector2FToVector2D(Vector2F vec)
        {
            return new Vector2D((double)vec.X, (double)vec.Y);
        }
        #endregion
    }
}
