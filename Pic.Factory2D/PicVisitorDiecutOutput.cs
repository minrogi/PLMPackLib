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
            _exporter.Initialize();
            // 
            PicVisitorBoundingBox visitor = new PicVisitorBoundingBox();
            factory.ProcessVisitor(visitor);
            _exporter.SetBoundingBox(visitor.Box.XMin, visitor.Box.YMin, visitor.Box.XMax, visitor.Box.YMax);
        }
        public override void ProcessEntity(PicEntity entity)
        {
            PicTypedDrawable drawable = entity as PicTypedDrawable;
            if (entity is PicSegment || entity is PicArc)
            {
                ExpBlock defblock = _exporter.GetBlockOrCreate("default");
                ExportEntity(defblock, entity);
            }
            PicBlock block = entity as PicBlock;
            if (null != block)
            {
                // create block
                ExpBlock expBlock = _exporter.CreateBlock(string.Format("Block_{0}", block.Id));
                // create _x=0.0 _y=0.0 
                ExpBlockRef expBlockRef = _exporter.CreateBlockRef(expBlock);
                // create entities
                foreach (PicEntity blockEntity in block.Entities)
                    ExportEntity(expBlock, blockEntity);
            }
            PicBlockRef blockRef = entity as PicBlockRef;
            if (null != blockRef)
            {
                // retrieve previously created block
                ExpBlock expBlock = _exporter.GetBlock(string.Format("Block_{0}", blockRef.Block.Id));
                ExpBlockRef expBlockRef = _exporter.CreateBlockRef(expBlock);
                expBlockRef._x = blockRef.Position.X;
                expBlockRef._y = blockRef.Position.Y;
                expBlockRef._dir = blockRef.Angle;
                expBlockRef._scaleX = 1.0;
                expBlockRef._scaleY = 1.0;
            }
        }
        public void ExportEntity(ExpBlock block, PicEntity entity)
        {
            PicTypedDrawable drawable = entity as PicTypedDrawable;
            ExpLayer layer = null;
            ExpPen pen = null;
            if (null != drawable)
            {
                pen = _exporter.GetPenByAttribute(LineTypeToExpPen(drawable.LineType));
                layer = _exporter.GetLayerByName(string.Format("Layer {0}", drawable.Layer));
            }
            PicSegment seg = entity as PicSegment;
            if (null != seg)
                _exporter.AddSegment(block, layer, pen, seg.Pt0.X, seg.Pt0.Y, seg.Pt1.X, seg.Pt1.Y);
            PicArc arc = entity as PicArc;
            if (null != arc)
            {
                // using dxf conversions
                double ang = arc.AngleEnd - arc.AngleBeg, angd = arc.AngleBeg, ango = arc.AngleEnd - arc.AngleBeg;
                if (ang < 0.0) {   angd += ang; ango = -ang;   } else ango = ang;
                _exporter.AddArc(block, layer, pen, arc.Center.X, arc.Center.Y, arc.Radius, angd, angd+ango);
            }
        }
        public override void Finish()
        {
        }
        public void Dispose()
        { 
        } 
        #endregion

        #region PicVisitorOutput override
        public override byte[] GetResultByteArray()
        {
            // if no blockref were created, create one from default block
            if (0 == _exporter._blockRefs.Count)
            {
                ExpBlock defblock = _exporter.GetBlockOrCreate("default");
                _exporter.CreateBlockRef(defblock);
            }
            // generate and return file content
            return _exporter.GetResultByteArray();
        }
        #endregion
    }
}
