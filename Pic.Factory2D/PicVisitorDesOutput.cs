#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using DesLib4NET;
#endregion

namespace Pic.Factory2D
{
    public class PicVisitorDesOutput : PicVisitorOutput, IDisposable
    {
        #region Private fields
        private DES_WriterMem _desWriter;
        private PicFactory _factory;
        #endregion

        #region Public constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PicVisitorDesOutput()
        {
        }
        #endregion

        #region Helpers
        public byte LineTypeToDesPen(PicGraphics.LT lType)
        {
            switch (lType)
            {
                case PicGraphics.LT.LT_CUT:             return 1;
                case PicGraphics.LT.LT_PERFOCREASING:   return 2;
                case PicGraphics.LT.LT_CONSTRUCTION:    return 3;
                case PicGraphics.LT.LT_PERFO:           return 4;
                case PicGraphics.LT.LT_HALFCUT:         return 5;
                case PicGraphics.LT.LT_CREASING:        return 6;
                case PicGraphics.LT.LT_AXIS:            return 7;
                case PicGraphics.LT.LT_COTATION:        return 8;
                default:
                    throw new Exception("LineType export not implemented!");
            }
        }
        #endregion

        #region PicFactoryVisitor overrides
        /// <summary>
        /// Initialize DES_Writer object
        /// </summary>
        /// <param name="factory"></param>
        public override void Initialize(PicFactory factory)
        {
            // save factory
            _factory = factory;
            // build .des file header
            DES_Header header = new DES_Header();
            PicVisitorBoundingBox bbVisitor = new PicVisitorBoundingBox();
            factory.ProcessVisitor(bbVisitor);
            header._xmin = (float)bbVisitor.Box.XMin;
            header._xmax = (float)bbVisitor.Box.XMax;
            header._ymin = (float)bbVisitor.Box.YMin;
            header._ymax = (float)bbVisitor.Box.YMax;
            // initialize des writer
            _desWriter = new DES_WriterMem(header);
            DES_SuperBaseHeader superBaseHeader = new DES_SuperBaseHeader();
            _desWriter.WriteSuperBaseHeader(superBaseHeader);
        }
        /// <summary>
        /// ProcessEntity : write entity corresponding des description
        /// </summary>
        /// <param name="entity"></param>
        public override void ProcessEntity(PicEntity entity)
        {
            switch (entity.Code)
            {
                case PicEntity.eCode.PE_POINT:
                    break;
                case PicEntity.eCode.PE_SEGMENT:
                    {
                        PicSegment seg = (PicSegment)entity;
                        _desWriter.WriteSegment(
                            new DES_Segment(
                            (float)seg.Pt0.X
                            , (float)seg.Pt0.Y
                            , (float)seg.Pt1.X
                            , (float)seg.Pt1.Y
                            , LineTypeToDesPen(seg.LineType)
                            , (byte)seg.Group
                            , (byte)seg.Layer
                            )
                        );
                    }
                    break;
                case PicEntity.eCode.PE_ARC:
                    {
                        PicArc arc = (PicArc)entity;
                        _desWriter.WriteArc(
                            new DES_Arc(
                            (float)arc.Center.X
                            , (float)arc.Center.Y
                            , (float)arc.Radius
                            , (float)arc.AngleBeg
                            , (float)arc.AngleEnd
                            , LineTypeToDesPen(arc.LineType)
                            , (byte)arc.Group
                            , (byte)arc.Layer
                            )
                        );
                    }
                    break;
                case PicEntity.eCode.PE_COTATIONDISTANCE:
                    {
                        PicCotationDistance cotation = entity as PicCotationDistance;
                        /*
                        _desWriter.WriteCotationDistance(
                            new DES_CotationDistance(
                                (float)cotation.Pt0.X
                                , (float)cotation.Pt0.Y
                                , (float)cotation.Pt1.X
                                , (float)cotation.Pt1.Y
                                , (float)cotation.Offset
                                , (byte)cotation.Group
                                , (byte)cotation.Layer
                                )
                            );
                         */ 
                    }
                    break;
                case PicEntity.eCode.PE_COTATIONHORIZONTAL:
                    {
                    }
                    break;
                case PicEntity.eCode.PE_COTATIONVERTICAL:
                    {
                    }
                    break;
                case PicEntity.eCode.PE_COTATIONRADIUSEXT:
                    {
                    }
                    break;
                case PicEntity.eCode.PE_COTATIONRADIUSINT:
                    {
                    }
                    break;
                case PicEntity.eCode.PE_BLOCK:
                    {
                        PicBlock block = entity as PicBlock;
                        foreach (PicEntity blockEntity in block)
                        {
                            ProcessEntity(blockEntity);
                        }
                    }
                    break;
                case PicEntity.eCode.PE_BLOCKREF:
                    {
                        PicBlockRef blockRef = entity as PicBlockRef;
                        _desWriter.WriteBlockRef(
                            new DES_Pose(
                                (float)blockRef.Position.X
                                , (float)blockRef.Position.Y
                                , (float)blockRef.Angle
                                , (byte)blockRef.Block.ExportedGroup)
                                );
                    }
                    break;
                case PicEntity.eCode.PE_CARDBOARDFORMAT:
                    {

                    }
                    break;
                default:
                    throw new Exception("Can not export this kind of entity!");
            }
        }
        /// <summary>
        /// Finish : writes end file statements
        /// </summary>
        public override void Finish()
        {
            if (_factory.HasCardboardFormat)
            {
                _desWriter.WriteCardboardFormat(
                    new DES_CardboardFormat(
                        1
                        , (float)_factory.Format.Position.X
                        , (float)_factory.Format.Position.Y
                        , (float)_factory.Format.Width
                        , (float)_factory.Format.Height
                        , (float)_factory.Format.Thickness)
                    );
            }
            _desWriter.Finish();
        }
        public void Dispose()
        {
            if (null != _desWriter)
                _desWriter.Close();
        }
        #endregion

        #region Public methods
        public override byte[] GetResultByteArray()
        {
            return _desWriter.GetResultByteArray();
        }
        #endregion

        #region Public properties
        #endregion
    }
}
