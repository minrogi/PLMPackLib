#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using treeDiM.DiecutLib;
#endregion

namespace Pic.Factory2D
{
    public class PicVisitorDiecutOutput : PicVisitorOutput, IDisposable
    {
        #region Private fields
        private BaseExporter _exporter;
        #endregion

        #region Public constructor
        public PicVisitorDiecutOutput(string fileExt)
        {
            _exporter = ExporterSet.GetExporterFromExtension("."+fileExt);
        }
        #endregion

        #region Helpers
        #endregion

        #region PicFactoryVisitor overrides
        public override void Initialize(PicFactory factory)
        {
            // create pens
            _lineType2ExpPen[PicGraphics.LT.LT_CUT] = _exporter.CreatePen("Cut", ExpPen.ToolAttribute.LT_CUT);
            _lineType2ExpPen[PicGraphics.LT.LT_CREASING] = _exporter.CreatePen("Crease", ExpPen.ToolAttribute.LT_CREASING);
            _lineType2ExpPen[PicGraphics.LT.LT_PERFOCREASING] = _exporter.CreatePen("Perfocreasing", ExpPen.ToolAttribute.LT_PERFOCREASING);
            _lineType2ExpPen[PicGraphics.LT.LT_HALFCUT] = _exporter.CreatePen("HalfCut", ExpPen.ToolAttribute.LT_HALFCUT);
            _lineType2ExpPen[PicGraphics.LT.LT_COTATION] = _exporter.CreatePen("Cotation", ExpPen.ToolAttribute.LT_COTATION);
            _lineType2ExpPen[PicGraphics.LT.LT_CONSTRUCTION] = _exporter.CreatePen("Construction", ExpPen.ToolAttribute.LT_CONSTRUCTION);
            // layers
            _layer = _exporter.CreateLayer("Layer1");
            
        }
        public override void ProcessEntity(PicEntity entity)
        {
            PicTypedDrawable drawable = entity as PicTypedDrawable;
            ExpBlock block = _exporter.GetBlock("default");
            ExpPen pen = null;
            if (null != drawable)
                pen = _lineType2ExpPen[drawable.LineType];
            PicSegment seg = entity as PicSegment;
            if (null != seg)
            {
                _exporter.AddSegment(block, _layer, pen, seg.Pt0.X, seg.Pt0.Y, seg.Pt1.X, seg.Pt1.Y);
            }
            PicArc arc = entity as PicArc;
            if (null != arc)
            {
                _exporter.AddArc(block, _layer, pen, arc.Center.X, arc.Center.Y, arc.Radius, arc.AngleBeg, arc.AngleEnd);           
            }
        }
        public override void Finish()
        {
        }
        public void Dispose()
        { 
        } 
        #endregion

        #region Public methods
        public override byte[] GetResultByteArray()
        {
            return _exporter.GetResultByteArray();
        }
        #endregion

        #region Data members
        private Dictionary<PicGraphics.LT, ExpPen> _lineType2ExpPen = new Dictionary<PicGraphics.LT, ExpPen>();
        private ExpLayer _layer = null;
        #endregion
    }
}
