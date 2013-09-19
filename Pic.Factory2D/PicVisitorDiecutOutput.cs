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
            _exporter = ExporterSet.GetExporterFromExtension(fileExt);
        }
        #endregion

        #region Helpers
        ExpPen.ToolAttribute LineTypeToExpPen(PicGraphics.LT lineType)
        {
            ExpPen.ToolAttribute toolAttribute;
            switch (lineType)
            {
                case PicGraphics.LT.LT_CUT: toolAttribute = ExpPen.ToolAttribute.LT_CUT; break;
                case PicGraphics.LT.LT_CREASING: toolAttribute = ExpPen.ToolAttribute.LT_CREASING; break;
                case PicGraphics.LT.LT_PERFOCREASING: toolAttribute = ExpPen.ToolAttribute.LT_PERFOCREASING; break;
                case PicGraphics.LT.LT_HALFCUT: toolAttribute = ExpPen.ToolAttribute.LT_HALFCUT; break;
                case PicGraphics.LT.LT_COTATION: toolAttribute = ExpPen.ToolAttribute.LT_COTATION; break;
                case PicGraphics.LT.LT_CONSTRUCTION: toolAttribute = ExpPen.ToolAttribute.LT_CONSTRUCTION; break;
                default: toolAttribute = ExpPen.ToolAttribute.LT_CONSTRUCTION; break;
            }
            return toolAttribute;        
        }
        #endregion

        #region PicFactoryVisitor overrides
        public override void Initialize(PicFactory factory)
        {
        }
        public override void ProcessEntity(PicEntity entity)
        {
            PicTypedDrawable drawable = entity as PicTypedDrawable;
            ExpBlock defblock = _exporter.GetBlock("default");
            ExpLayer layer = null;
            ExpPen pen = null;
            if (null != drawable)
            {
                pen = _exporter.GetPenByAttribute(LineTypeToExpPen(drawable.LineType));
                layer = _exporter.GetLayerByName(string.Format("Layer {0}", drawable.Layer));
            }
            PicSegment seg = entity as PicSegment;
            if (null != seg)
            {
                _exporter.AddSegment(defblock, layer, pen, seg.Pt0.X, seg.Pt0.Y, seg.Pt1.X, seg.Pt1.Y);
            }
            PicArc arc = entity as PicArc;
            if (null != arc)
            {
                _exporter.AddArc(defblock, layer, pen, arc.Center.X, arc.Center.Y, arc.Radius, arc.AngleBeg, arc.AngleEnd);           
            }
            PicBlock block = entity as PicBlock;
            if (null != block)
            {
                //_exporter.AddBlock();
            }
            PicBlockRef blockRef = entity as PicBlockRef;
            if (null != blockRef)
            {
                //_exporter.AddBlockRef();
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
    }
}
