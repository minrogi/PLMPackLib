#region Using directives
using System;
using System.Collections.Generic;
using System.Text;

using Sharp3D.Math.Core;
#endregion

namespace Pic.Factory2D
{
    public class PicAutoQuotation
    {
        #region Data members
        private double _tolerance = 5.0;
        #endregion

        #region Constructor
        public PicAutoQuotation()
        { }
        #endregion

        #region Public static methods
        public static void BuildQuotation(PicFactory factory)
        {
            PicAutoQuotation autoQuotation = new PicAutoQuotation();
            for (short grp = 0; grp < 16; ++grp)
            {
                // get min max box grp
                PicVisitorBoundingBox visitor = new PicVisitorBoundingBox();
                factory.ProcessVisitor(visitor, new PicFilterGroup(grp));

                autoQuotation.BuildHQuotation(factory, grp, visitor.Box);
                autoQuotation.BuildVQuotation(factory, grp, visitor.Box);
                autoQuotation.BuildGlobalQuotation(factory, grp, visitor.Box);
            }
        }
        #endregion

        #region Private methods
        // build horizontal quotation
        private void BuildHQuotation(PicFactory factory, short grp, Box2D box)
        {
            // get entities
            PicVisitorAutoQuotationEntities visitor = new PicVisitorAutoQuotationEntities(grp);
            factory.ProcessVisitor(visitor);
            List<PicTypedDrawable> list = visitor.Entities;
            // list of vertical segments
            List<PicSegment> vSegments = new List<PicSegment>();
            Box2D bbox = new Box2D();
            double minY_Xmin = double.MaxValue;
            double minY_Xmax = double.MaxValue;
            foreach (PicTypedDrawable typedDrawable in list)
            {
                // get entity bounding box
                Box2D entityBox = typedDrawable.Box;
                // save y of minx
                if (entityBox.XMin <= bbox.XMin)
                {
                    if (Math.Abs(entityBox.XMin - bbox.XMin) < 0.1)
                        minY_Xmin = Math.Min(entityBox.YMin, minY_Xmin);
                    else
                        minY_Xmin = entityBox.YMin;
                }
                if (entityBox.XMax >= bbox.XMax)
                {
                    if (Math.Abs(entityBox.XMax - bbox.XMax) < 0.1)
                        minY_Xmax = Math.Min(entityBox.YMin, minY_Xmax);
                    else
                        minY_Xmax = entityBox.YMin;
                }
                // expand global bounding box
                bbox.Extend(entityBox);
                // save vertical fold segments
                if ( typedDrawable is PicSegment
                    && (
                    (typedDrawable.LineType == PicGraphics.LT.LT_CREASING)
                    || (typedDrawable.LineType == PicGraphics.LT.LT_HALFCUT) 
                    || (typedDrawable.LineType == PicGraphics.LT.LT_PERFO) 
                    || (typedDrawable.LineType == PicGraphics.LT.LT_PERFOCREASING)
                    )
                    )
                {
                    PicSegment seg = typedDrawable as PicSegment;
                    if (Math.Abs(seg.Pt1.X - seg.Pt0.X)/seg.Length < 0.01)
                        vSegments.Add(seg);
                }
            }
            // exit if no horizontal folds
            if (vSegments.Count == 0) return;
            // sort vertical segments by increasing x
            vSegments.Sort(new ComparerVerticalFoldsByIncreasingX());
            // remove redundants x abscissa
            double prevx = bbox.XMin;
            List<PicSegment> sameAbscissaSegments = new List<PicSegment>();
            List<PicSegment> resultSegments = new List<PicSegment>();
            foreach (PicSegment seg in vSegments)
            {
                if (sameAbscissaSegments.Count > 0)
                {
                    if (seg.Pt0.X - sameAbscissaSegments[0].Pt0.X > _tolerance)
                    {
                        // sort segments vertically
                        sameAbscissaSegments.Sort(new ComparerVerticalFoldsByIncreasingY());
                        resultSegments.Add(sameAbscissaSegments[0]);
                        sameAbscissaSegments.Clear();
                    }
                }
                sameAbscissaSegments.Add(seg);
            }

            // add last segment if any
            if (sameAbscissaSegments.Count > 0)
            {
                // sort segments vertically
                sameAbscissaSegments.Sort(new ComparerVerticalFoldsByIncreasingY());
                resultSegments.Add(sameAbscissaSegments[0]);
                sameAbscissaSegments.Clear();
            }

            // ### create horizontal quotations
            double delta = 0.05 * Math.Max(bbox.Width, bbox.Height);
            double ypos = bbox.YMin - delta;
            Vector2D ptPrev = bbox.PtMin;
            int i = 0;
            if (resultSegments.Count > 0 && (bbox.XMin + _tolerance < resultSegments[0].Pt0.X))
                ptPrev = new Vector2D(bbox.XMin, minY_Xmin);
            else
            { 
                ptPrev = GetLowestPoint(resultSegments[i]);
                ++i;
            }

            // between folds
            for (; i < resultSegments.Count; ++i)
            {
                Vector2D pt = GetLowestPoint(resultSegments[i]);
                CreateQuotationH(factory, ptPrev, pt, ypos, grp);
                ptPrev = pt;
            }
            // last : from last fold to maxx
            double yposTemp = bbox.XMax - ptPrev.X > delta ? ypos : ptPrev.Y;
            if (resultSegments.Count == 0)
                CreateQuotationH(factory, ptPrev, new Vector2D(), ypos, grp);
            else if (resultSegments[resultSegments.Count - 1].Pt0.X + _tolerance < bbox.XMax)
                CreateQuotationH(factory, ptPrev, new Vector2D(bbox.XMax, minY_Xmax), ypos, grp);
        }
        // build vertical quotation
        private void BuildVQuotation(PicFactory factory, short grp, Box2D box)
        {
            // get entities
            PicVisitorAutoQuotationEntities visitor = new PicVisitorAutoQuotationEntities(grp);
            factory.ProcessVisitor(visitor);
            List<PicTypedDrawable> list = visitor.Entities;
            // list of vertical segments
            List<PicSegment> hSegments = new List<PicSegment>();
            Box2D bbox = new Box2D();
            double minX_Ymin = double.MaxValue;
            double minX_Ymax = double.MaxValue;
            foreach (PicTypedDrawable typedDrawable in list)
            {
                // get entity bounding box
                Box2D entityBox = typedDrawable.Box;
                // save y of minx
                if (entityBox.YMin <= bbox.YMin)
                {
                    if (Math.Abs(entityBox.YMin - bbox.YMin) < 0.1)
                        minX_Ymin = Math.Min(entityBox.XMin, minX_Ymin);
                    else
                        minX_Ymin = entityBox.XMin;
                }
                if (entityBox.YMax >= bbox.YMax)
                {
                    if (Math.Abs(entityBox.YMax - bbox.YMax) < 0.1)
                        minX_Ymax = Math.Max(entityBox.YMax, minX_Ymax);
                    else
                        minX_Ymax = entityBox.XMin;
                }
                // expand global bounding box
                bbox.Extend(entityBox);
                // save vertical fold segments
                if (typedDrawable is PicSegment
                    && (
                    (typedDrawable.LineType == PicGraphics.LT.LT_CREASING)
                    || (typedDrawable.LineType == PicGraphics.LT.LT_HALFCUT)
                    || (typedDrawable.LineType == PicGraphics.LT.LT_PERFO)
                    || (typedDrawable.LineType == PicGraphics.LT.LT_PERFOCREASING)
                    )
                    )
                {
                    PicSegment seg = typedDrawable as PicSegment;
                    if (Math.Abs(seg.Pt1.Y - seg.Pt0.Y)/seg.Length < 0.01)
                        hSegments.Add(seg);
                }
            }

            // exit if no vertical folds
            if (hSegments.Count == 0) return;
            // sort vertical segments by increasing x
            hSegments.Sort(new ComparerHorizontalFoldsByIncreasingY());
            // remove redundants x abscissa
            double prevy = bbox.YMin;
            List<PicSegment> sameOrdSegments = new List<PicSegment>();
            List<PicSegment> resultSegments = new List<PicSegment>();
            foreach (PicSegment seg in hSegments)
            {
                if (sameOrdSegments.Count > 0)
                {
                    if (seg.Pt0.Y - sameOrdSegments[0].Pt0.Y > _tolerance)
                    {
                        // sort segments vertically
                        sameOrdSegments.Sort(new ComparerHorizontalFoldsByIncreasingX());
                        resultSegments.Add(sameOrdSegments[0]);
                        sameOrdSegments.Clear();
                    }
                }
                sameOrdSegments.Add(seg);
            }

            // add last segment if any
            if (sameOrdSegments.Count > 0)
            {
                // sort segments vertically
                sameOrdSegments.Sort(new ComparerHorizontalFoldsByIncreasingX());
                resultSegments.Add(sameOrdSegments[0]);
                sameOrdSegments.Clear();
            }

            // ### create horizontal quotations
            double delta = 0.05 * Math.Max(bbox.Width, bbox.Height);
            double xpos = bbox.XMin - delta;
            Vector2D ptPrev = bbox.PtMin;
            int i = 0;
            if (resultSegments.Count > 0 && (bbox.YMin + _tolerance < resultSegments[0].Pt0.Y))
                ptPrev = new Vector2D(minX_Ymin, bbox.YMin);
            else
            {
                ptPrev = GetLeftPoint(resultSegments[i]);
                ++i;
            }

            // between folds
            for (; i < resultSegments.Count; ++i)
            {
                Vector2D pt = GetLeftPoint(resultSegments[i]);
                CreateQuotationV(factory, ptPrev, pt, xpos, grp);
                ptPrev = pt;
            }
            // last : from last fold to maxx
            double xposTemp = bbox.XMax - ptPrev.X > delta ? xpos : ptPrev.Y;

            if (resultSegments.Count == 0)
                CreateQuotationV(factory, ptPrev, new Vector2D(), xpos, grp);
            else if (resultSegments[resultSegments.Count - 1].Pt0.Y + _tolerance < bbox.YMax)
                CreateQuotationV(factory, ptPrev, new Vector2D(minX_Ymax, bbox.YMax), xpos, grp);
 
        }
        // global quotation
        private void BuildGlobalQuotation(PicFactory factory, short grp, Box2D box)
        {
            if (!box.IsValid) return;
            double delta = 0.1 * Math.Max(box.Width, box.Height);
            PicCotationDistance cotH = factory.AddCotation(PicCotation.CotType.COT_HORIZONTAL, grp, 0, box.PtMin, new Vector2D(box.XMax, box.YMin), -delta, "", 1) as PicCotationDistance;
            cotH.UseShortLines = true;
            PicCotationDistance cotV = factory.AddCotation(PicCotation.CotType.COT_VERTICAL, grp, 0, box.PtMin, new Vector2D(box.XMin, box.YMax), delta, "", 1) as PicCotationDistance;
            cotV.UseShortLines = true;
        }
        #endregion

        #region Create quotation
        private void CreateQuotationH(PicFactory factory, Vector2D pt0, Vector2D pt1, double ypos, short grp)
        {
            factory.AddCotation(PicCotation.CotType.COT_HORIZONTAL, grp, 0, pt0, pt1, ypos-pt0.Y, string.Empty, 1);
        }
        private void CreateQuotationV(PicFactory factory, Vector2D pt0, Vector2D pt1, double xpos, short grp)
        {
            factory.AddCotation(PicCotation.CotType.COT_VERTICAL, grp, 0, pt0, pt1, pt0.X - xpos, string.Empty, 1);
        }
        #endregion

        #region Helpers
        private Vector2D GetLowestPoint(PicSegment seg)
        {
            return seg.Pt0.Y < seg.Pt1.Y ? seg.Pt0 : seg.Pt1;
        }
        private Vector2D GetLeftPoint(PicSegment seg)
        {
            return seg.Pt0.X < seg.Pt1.X ? seg.Pt0 : seg.Pt1;
        }
        #endregion
    }

    #region Segment comparers
    internal class ComparerVerticalFoldsByIncreasingX : IComparer<PicSegment>
    {
        public int Compare(PicSegment seg1, PicSegment seg2)
        {
            if (seg1.Pt0.X < seg2.Pt0.X)
                return -1;
            else if (seg1.Pt0.X == seg2.Pt0.X)
                return 0;
            else
                return 1;
        }
    }
    internal class ComparerVerticalFoldsByIncreasingY : IComparer<PicSegment>
    {
        public int Compare(PicSegment seg1, PicSegment seg2)
        {
            double miny1 = Math.Min(seg1.Pt0.Y, seg1.Pt1.Y);
            double miny2 = Math.Min(seg2.Pt0.Y, seg2.Pt1.Y);
            if (miny1 < miny2) return -1;
            else if (miny1 == miny2) return 0;
            else return 1;
        }
    }
    internal class ComparerHorizontalFoldsByIncreasingY : IComparer<PicSegment>
    {
        public int Compare(PicSegment seg1, PicSegment seg2)
        {
            if (seg1.Pt0.Y < seg2.Pt0.Y)
                return -1;
            else if (seg1.Pt0.Y == seg2.Pt0.Y)
                return 0;
            else
                return 1;
        }
    }
    internal class ComparerHorizontalFoldsByIncreasingX : IComparer<PicSegment>
    {
        public int Compare(PicSegment seg1, PicSegment seg2)
        {
            double minx1 = Math.Min(seg1.Pt0.X, seg1.Pt1.X);
            double minx2 = Math.Min(seg2.Pt0.X, seg2.Pt1.X);
            if (minx1 < minx2) return -1;
            else if (minx1 == minx2) return 0;
            else return 1;
        }
    }
    #endregion

    #region Visitor that builds list of entities
    internal class PicVisitorAutoQuotationEntities : PicFactoryVisitor
    {
        #region Constructor
        public PicVisitorAutoQuotationEntities(short grp)
        {
            _grp = grp;
        }
        #endregion

        #region PicFactory overrides
        public override void  ProcessEntity(PicEntity entity)
        {
            PicTypedDrawable typedDrawable = entity as PicTypedDrawable;
            if (null != typedDrawable
                && typedDrawable.Group == _grp
                && typedDrawable.LineType != PicGraphics.LT.LT_CONSTRUCTION
                && typedDrawable.Code == PicEntity.eCode.PE_SEGMENT)
                _entities.Add(typedDrawable);
        }
        public override void Initialize(PicFactory factory) { _entities.Clear(); }
        public override void Finish() {}
        #endregion

        #region Public properties
        public List<PicTypedDrawable> Entities { get { return _entities; } }
        #endregion

        #region Data members
        private List<PicTypedDrawable> _entities = new List<PicTypedDrawable>();
        private short _grp = 0;
        #endregion
    }
    #endregion
}
