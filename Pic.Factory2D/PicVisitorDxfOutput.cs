#region Using directives
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.IO;

using Dxflib4NET;
#endregion

namespace Pic.Factory2D
{
    public class PicVisitorDxfOutput: PicVisitorOutput, IDisposable
    {
        #region Private fields
        private DL_Dxf dxf;
        private DL_Writer dw;
        #endregion

        #region Public constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PicVisitorDxfOutput()
        {
        }
        #endregion

        #region Helpers
        public string LineTypeToDxfLayer(PicGraphics.LT lType)
        { 
            switch (lType)
            {
                case PicGraphics.LT.LT_CUT:             return "L5-113";
                case PicGraphics.LT.LT_PERFOCREASING:   return "L6-133";
                case PicGraphics.LT.LT_CONSTRUCTION:    return "LCN-27";
                case PicGraphics.LT.LT_PERFO:           return "EC1-193";
                case PicGraphics.LT.LT_HALFCUT:         return "LI5-103";
                case PicGraphics.LT.LT_CREASING:        return "L8-123";
                case PicGraphics.LT.LT_AXIS:            return "L2-106";
                case PicGraphics.LT.LT_COTATION:        return "LDM-4";
                case PicGraphics.LT.LT_GRID:            return "L2-106";
                default:    return "";
            }

            // **
            // "Cut", "L5-113"
            // "Perfo-Crease", "L6-133"
            // "Construction", "LCN-27"
            // "Perfo", "EC1-193"
            // "Half-Cut", "LI5-103"
            // "Crease", "L8-123"
            // "Axis", "L2-106"
            // "Dimension", "LDM-4"
            // **
        }
        #endregion

        #region PicFactoryVisitor overrides
        /// <summary>
        /// Initialize string builder with initial sections + line types
        /// Instantiate DL_Dxf class and DL_Writer class
        /// </summary>
        /// <param name="factory"></param>
        public override void Initialize(PicFactory factory)
        {
            DL_Codes.dxfversion version = DL_Codes.dxfversion.AC1012;
            dw = new DL_Writer(version);
            dxf = new DL_Dxf();
            dxf.writeHeader(dw);
            dw.sectionEnd();
            // opening the table section
            dw.sectionTables();
            // writing viewports
            dxf.writeVPort(dw);
            // writing line types
            dw.tableLineTypes(25);
            dxf.writeLineType(dw, new DL_LineTypeData("BYBLOCK", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("BYLAYER", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("CONTINUOUS", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("ACAD_ISO02W100", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("ACAD_ISO03W100", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("ACAD_ISO04W100", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("ACAD_ISO05W100", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("BORDER", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("BORDER2", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("BORDERX2", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("CENTER", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("CENTER2", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("CENTERX2", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("DASHDOT", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("DASHDOT2", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("DASHDOTX2", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("DASHED", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("DASHED2", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("DASHEDX2", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("DIVIDE", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("DIVIDE2", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("DIVIDEX2", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("DOT", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("DOT2", 0));
            dxf.writeLineType(dw, new DL_LineTypeData("DOTX2", 0));
            dw.tableEnd();
            // writing the layers
            int numberOfLayers = 3;
            dw.tableLayers(numberOfLayers);
            // CUT
            dxf.writeLayer(dw, new DL_LayerData("L5-113", 0),
                new DL_Attributes("",                       // leave empty
                    (int)DL_Codes.dxfcolor.red,             // default color
                    100,                                    // default width
                    "CONTINUOUS"));                         // default line style
            // FOLD
            dxf.writeLayer(dw, new DL_LayerData("L8-123", 0),
                new DL_Attributes("",                       // leave empty
                    (int)DL_Codes.dxfcolor.blue,            // default color
                    100,                                    // default width
                    "CONTINUOUS"));                         // default line style
            // COTATION
            dxf.writeLayer(dw, new DL_LayerData("LDM-4", 0),
                new DL_Attributes("",                       // leave empty
                    (int)DL_Codes.dxfcolor.green,           // default color
                    100,                                    // default width
                    "CONTINUOUS"));                         // default line style
            dw.tableEnd();
            dw.sectionEnd();
            // write all entities
            dw.sectionEntities();
        }
        /// <summary>
        /// ProcessEntity : write entity corresponding dxf description in line buffer 
        /// </summary>
        /// <param name="entity">Entity</param>
        public override void ProcessEntity(PicEntity entity)
        {
            PicTypedDrawable drawable = (PicTypedDrawable)entity;
            if (null != drawable)
            {
                switch (drawable.Code)
                {
                    case PicEntity.eCode.PE_POINT:
                        break;
                    case PicEntity.eCode.PE_SEGMENT:
                        {
                            PicSegment seg = (PicSegment)entity;
                             dxf.writeLine(
                                dw
                                , new DL_LineData(
                                    seg.Pt0.X		// start point
                                    , seg.Pt0.Y
                                    , 0.0
                                    , seg.Pt1.X	// end point
                                    , seg.Pt1.Y
                                    , 0.0
                                    , 256
                                    , LineTypeToDxfLayer(seg.LineType)
                                    )
                                , new DL_Attributes(LineTypeToDxfLayer(seg.LineType), 256, -1, "BYLAYER")
                                );

                        }
                        break;
                    case PicEntity.eCode.PE_ARC:
                        {
                            PicArc arc = (PicArc)entity;
                            double ang = arc.AngleEnd - arc.AngleBeg, angd = arc.AngleBeg, ango = arc.AngleEnd - arc.AngleBeg;
                           	if ( ang <  0.0 )
	                        {
		                        angd += ang;
		                        ango = - ang;
	                        }
	                        else
		                        ango = ang;
                            
                            dxf.writeArc(dw,
                                new DL_ArcData(
                                    arc.Center.X, arc.Center.Y, 0.0,
                                    arc.Radius,
                                    angd, angd + ango,
                                    256,
                                    LineTypeToDxfLayer(arc.LineType)
                                    ),
                                new DL_Attributes(LineTypeToDxfLayer(arc.LineType), 256, -1, "BYLAYER")
                            );
                        }
                        break;
                    case PicEntity.eCode.PE_ELLIPSE:
                        break;
                    case PicEntity.eCode.PE_NURBS:
                        break;
                    default:
                        throw new Exception("Can not export this kind of entity!");
                }
            }
        }
        /// <summary>
        /// Finish : writes end file statements to the stringbuilder
        /// </summary>
        public override void Finish()
        {
            // end section entities
            dw.sectionEnd();

            // writing the object section
            dxf.writeObjects(dw);
            dxf.writeObjectsEnd(dw);

            // ending and closing the file
            dw.dxfEOF();
            dw.close();
        }

        public void Dispose()
        {
        }
        #endregion

        #region Public methods
        public override byte[] GetResultByteArray()
        {
            // convert string to byte array
            string textOutput = dw.ToString();
            byte[] byteArray = new byte[textOutput.Length];
            for (int i = 0; i < textOutput.Length; ++i)
                byteArray[i] = Convert.ToByte(textOutput[i]);
            return byteArray;
        }
        #endregion

        #region Public properties
        public String TextOutput
        {
            get
            {
                return dw.ToString();
            }
        }
        #endregion
    }
}
