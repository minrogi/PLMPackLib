#define PDFSHARP

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sharp3D.Math.Core;
#endregion

#if ITEXTSHARP
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

namespace Pic.Factory2D
{
    public class PicGraphicsPdf : PicGraphics
    {        
        #region Private fields
        /// <summary>
        /// PdfDocument
        /// </summary>
        private Document _pdfDocument;
        /// <summary>
        /// Memory stream
        /// </summary>
        MemoryStream _stream = new MemoryStream();
        /// <summary>
        /// PdfContentByte
        /// </summary>
        PdfContentByte _cb;

        Box2D bbox;
        string author;
        string title;
        #endregion

        #region Constructor
        public PicGraphicsPdf(Box2D box)
        {
            bbox = box;
            author = "";
            title = "";
        }
        #endregion

        #region PicGraphics override
        public override void Initialize()
        {
            // step 1 : creation of document object
            Rectangle rect = new Rectangle((float)bbox.Width * 1.1f, (float)bbox.Height * 1.1f);
            rect.BackgroundColor = BaseColor.WHITE;
            _pdfDocument = new iTextSharp.text.Document(rect);
            // step 2:
            // we create a writer that listens to the document
            // and directs a PDF-stream to a file
            PdfWriter writer = PdfWriter.GetInstance(_pdfDocument, _stream);
            // step 3: we open the document
            _pdfDocument.Open();
            // step 4: we add content to the document
            _pdfDocument.AddCreationDate();
            _pdfDocument.AddAuthor(author);
            _pdfDocument.AddTitle(title);

            _cb = writer.DirectContent;

            // initialize cotation
            PicCotation._globalCotationProperties._arrowLength = bbox.Height / 50.0;
        }
        public override void DrawPoint(LT lineType, Vector2D pt)
        { 
        }
        public override void DrawLine(LT lineType, Vector2D ptBeg, Vector2D ptEnd)
        {
            _cb.SetLineWidth(0.0f);
            _cb.SetColorStroke(LineTypeToBaseColor(lineType));

            _cb.MoveTo(DX(ptBeg.X), DY(ptBeg.Y));
            _cb.LineTo(DX(ptEnd.X), DY(ptEnd.Y));
            _cb.Stroke();
        }
        public override void DrawArc(LT lineType, Vector2D ptCenter, double radius, double angleBeg, double angleEnd)
        {
            _cb.SetLineWidth(0.0f);
            _cb.SetColorStroke(LineTypeToBaseColor(lineType));

            _cb.Arc(
                DX(ptCenter.X-radius), DY(ptCenter.Y-radius)            // Bottom Left(x,y)
                , DX(ptCenter.X + radius), DY(ptCenter.Y + radius)      // Top Right(x,y)
                , -(float)angleBeg                                      // Start Angle
                , -(float)(angleEnd - angleBeg)                         // Extent
                );
            _cb.Stroke();
        }
        public override void DrawText(string text, TextType font, Vector2D pt, HAlignment hAlignment, VAlignment vAlignment, float fAngle)
        {
            int alignment = 0;
            switch (hAlignment)
            {
                case HAlignment.VA_CENTER: alignment = PdfContentByte.ALIGN_CENTER; break;
                case HAlignment.VA_LEFT:   alignment = PdfContentByte.ALIGN_LEFT;   break;
                case HAlignment.VA_RIGHT:  alignment = PdfContentByte.ALIGN_RIGHT;  break;
                default: break;
            }

            float fontSize = FontSize(font);
            float fontRise = 0.0f;
            switch (vAlignment)
            {
                case VAlignment.VA_BOTTOM: fontRise =0.5f; break;
                case VAlignment.VA_MIDDLE: fontRise = 0.0f * fontSize; break;
                case VAlignment.VA_TOP: fontRise = -0.5f * fontSize; break;
                default: break;
            }

            _cb.BeginText();
            BaseFont bf = ToBaseFont(font);
            _cb.SetTextRise(fontRise);
            _cb.SetTextMatrix(1.0f, 0.0f, 0.0f, 1.0f, DX(pt.X), DY(pt.Y) - 1.0f * (bf.GetAscentPoint(text, fontSize) - bf.GetDescentPoint(text, fontSize)) * 25.4f/72.0f);
            _cb.ShowTextAligned(alignment, text, DX(pt.X), DY(pt.Y) - 1.0f * (bf.GetAscentPoint(text, fontSize) - bf.GetDescentPoint(text, fontSize)) * 25.4f / 72.0f, 0.0f);
            _cb.EndText();
        }

        public override void GetTextSize(string text, TextType font, out double width, out double height)
        {
            BaseFont bf = ToBaseFont(font);
            float fontSize = FontSize(font);

            width = DX_inv(bf.GetWidthPoint(text, fontSize));
            height = DY_inv(bf.GetAscentPoint(text, fontSize) - bf.GetDescentPoint(text, fontSize));
        }
        /// <summary>
        /// Finish
        /// </summary>
        public override void Finish()
        {
            // step 5: we close the document
            _pdfDocument.Close();
        }
        public override double DX_inv(double dX)
        {
            // 72 point per inch / 25.4 mm per inch / ? 3.0
            return 3.0 * dX / 72 * 25.4;
        }
        public override double DY_inv(double dY)
        {
            // 72 point per inch / 25.4 mm per inch / ? 3.0
            return 3.0 * dY / 72 * 25.4;
        }
        #endregion

#region Helpers

        private float DX(double x)
        {
            return 1.1f * (float)(x - bbox.XMin);
        }
        private float DY(double y)
        {
            return 1.1f * (float)(0.5f * bbox.Height - 0.5f * (bbox.YMin + bbox.YMax) + y);
        }
        private double DL(double length)
        {
            return length;
        }

        private BaseColor LineTypeToBaseColor(PicGraphics.LT lType)
        {
            switch (lType)
            {
                case PicGraphics.LT.LT_CUT: return BaseColor.RED;
                case PicGraphics.LT.LT_FOLD: return BaseColor.BLUE;
                case PicGraphics.LT.LT_COTATION: return BaseColor.GREEN;
                default: return BaseColor.WHITE;
            }           
        }
        private Font ToFont(PicGraphics.TextType tType)
        {
            switch (tType)
            {
                case TextType.FT_COTATION:
                    {
                        BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        return new Font(bf, FontSize(tType), Font.NORMAL, BaseColor.GREEN);
                    }
                default: throw new Exception("Unknown text type");
            
            }
        }

        private float FontSize(PicGraphics.TextType tType)
        {
            switch (tType)
            {
                case TextType.FT_COTATION: return _pdfDocument.PageSize.Height / 25.0f;
                default: throw new Exception("Unknown text type");
            }
        }
        private BaseFont ToBaseFont(PicGraphics.TextType tType)
        {
            switch (tType)
            {
                case TextType.FT_COTATION:
                    {
                        BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        _cb.SetFontAndSize(bf, FontSize(tType));
                        _cb.SetColorFill( BaseColor.GREEN );
                        return bf;
                    }
                default: throw new Exception("Unknown text type");
            }
        }

        #endregion

#region Public properties
        public string Author
        {
            get { return author; }
            set { author = value;}
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        #endregion

#region Public method
        /// <summary>
        /// Access the result byte array
        /// </summary>
        /// <returns></returns>
        public byte[] GetResultByteArray()
        {
            return _stream.ToArray();
        }
        #endregion
    }
}
#else // PdfSharp

using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Pic.Factory2D
{
    public class PicGraphicsPdf : PicGraphics
    {
        #region Private fields
        /// <summary>
        /// PdfDocument
        /// </summary>
        private PdfDocument pdfDocument;
        /// <summary>
        /// Graphics where entities are output
        /// </summary>
        XGraphics pdfGfx;
        Box2D bbox;
        string author;
        string title;
        #endregion

        #region Constructor
        public PicGraphicsPdf(Box2D box)
        {
            bbox = box;
            author = "";
            title = "";
        }
        #endregion

        #region PicGraphics override
        public override void Initialize()
        {
            // instantiate document
            pdfDocument = new PdfDocument();
            pdfDocument.Info.Title = title;
            pdfDocument.Info.Author = author;
            // add a page
            PdfPage page = pdfDocument.AddPage();
            page.Orientation = PageOrientation.Portrait;
            // set page size
            page.Width = XUnit.FromMillimeter(bbox.Width);
            page.Height = XUnit.FromMillimeter(bbox.Height);
            // get graphics
            this.pdfGfx = XGraphics.FromPdfPage(page);
            // draw a bounding box
            XRect rect = new XRect(0.5, 0.5, page.Width - 1, page.Height - 1);
            this.pdfGfx.DrawRectangle(XBrushes.White, rect);
            // initialize cotation
            PicCotation._globalCotationProperties._arrowLength = XUnit.FromMillimeter(bbox.Height) / 50.0;
        }
        public override void DrawPoint(LT lineType, Vector2D pt)
        {
        }
        public override void DrawLine(LT lineType, Vector2D ptBeg, Vector2D ptEnd)
        {
            this.pdfGfx.DrawLine(new XPen(LineTypeToPdfColor(lineType))
                , DX(ptBeg.X), DY(ptBeg.Y)
                , DX(ptEnd.X), DY(ptEnd.Y));
        }
        public override void DrawArc(LT lineType, Vector2D ptCenter, double radius, double angleBeg, double angleEnd)
        {
            this.pdfGfx.DrawArc(new XPen(LineTypeToPdfColor(lineType))
                , DX(ptCenter.X - radius), DY(ptCenter.Y + radius)
                , DL(2 * radius), DL(2 * radius)
                , -angleBeg, -angleEnd + angleBeg);
        }
        public override void DrawText(string text, TextType font, Vector2D pt, HAlignment hAlignment, VAlignment vAlignment, float fAngle)
        {
            XStringFormat format = new XStringFormat();
            switch (hAlignment)
            {
                case HAlignment.HA_CENTER: format.Alignment = XStringAlignment.Center; break;
                case HAlignment.HA_LEFT: format.Alignment = XStringAlignment.Near; break;
                case HAlignment.HA_RIGHT: format.Alignment = XStringAlignment.Far; break;
                default: break;
            }
            switch (vAlignment)
            {
                case VAlignment.VA_BOTTOM: format.LineAlignment = XLineAlignment.Far; break;
                case VAlignment.VA_MIDDLE: format.LineAlignment = XLineAlignment.Center; break;
                case VAlignment.VA_TOP: format.LineAlignment = XLineAlignment.Near; break;
                default: break;
            }
            this.pdfGfx.DrawString(
                text
                , ToFont(font)
                , LineTypeToPdfBrush(PicGraphics.LT.LT_COTATION)
                , new XPoint(DX(pt.X), DY(pt.Y))
                , format);
        }

        public override void GetTextSize(string text, TextType font, out double width, out double height)
        {
            XSize size = this.pdfGfx.MeasureString(text, ToFont(font));

            width = DX_inv(size.Width);
            height = DY_inv(size.Height);
        }
        /// <summary>
        /// Finish
        /// </summary>
        public override void Finish()
        {
        }
        public override double DX_inv(double dX)
        {
            return dX * bbox.Width / XUnit.FromMillimeter(bbox.Width);
        }
        public override double DY_inv(double dY)
        {
            return dY * bbox.Height / XUnit.FromMillimeter(bbox.Height);
        }
        public override Box2D DrawingBox
        {
            get { return bbox; }
            set { bbox = value; }
        }
        #endregion

        #region Helpers
        private double DX(double x)
        {
            return XUnit.FromMillimeter((x - bbox.XMin));
        }
        private double DY(double y)
        {
            return XUnit.FromMillimeter((0.5 * bbox.Height + 0.5 * (bbox.YMin + bbox.YMax) - y));
        }
        private double DL(double length)
        {
            return XUnit.FromMillimeter(length);
        }
        private PdfSharp.Drawing.XColor LineTypeToPdfColor(PicGraphics.LT lType)
        {
            switch (lType)
            {
                case PicGraphics.LT.LT_CUT: return PdfSharp.Drawing.XColors.Red;
                case PicGraphics.LT.LT_PERFOCREASING: return PdfSharp.Drawing.XColors.Blue;
                case PicGraphics.LT.LT_CONSTRUCTION: return PdfSharp.Drawing.XColors.Black;
                case PicGraphics.LT.LT_PERFO: return PdfSharp.Drawing.XColors.Red;
                case PicGraphics.LT.LT_HALFCUT: return PdfSharp.Drawing.XColors.LightBlue;
                case PicGraphics.LT.LT_CREASING: return PdfSharp.Drawing.XColors.Blue;
                case PicGraphics.LT.LT_AXIS: return PdfSharp.Drawing.XColors.Pink;
                case PicGraphics.LT.LT_COTATION: return PdfSharp.Drawing.XColors.Green;
                default: return PdfSharp.Drawing.XColors.White;
            }
        }
        private PdfSharp.Drawing.XBrush LineTypeToPdfBrush(PicGraphics.LT lType)
        {
            switch (lType)
            {
                case PicGraphics.LT.LT_CUT: return PdfSharp.Drawing.XBrushes.Red;
                case PicGraphics.LT.LT_PERFOCREASING: return PdfSharp.Drawing.XBrushes.Blue;
                case PicGraphics.LT.LT_CONSTRUCTION: return PdfSharp.Drawing.XBrushes.Black;
                case PicGraphics.LT.LT_PERFO: return PdfSharp.Drawing.XBrushes.Red;
                case PicGraphics.LT.LT_HALFCUT: return PdfSharp.Drawing.XBrushes.LightBlue;
                case PicGraphics.LT.LT_CREASING: return PdfSharp.Drawing.XBrushes.Blue;
                case PicGraphics.LT.LT_AXIS: return PdfSharp.Drawing.XBrushes.Pink;
                case PicGraphics.LT.LT_COTATION: return PdfSharp.Drawing.XBrushes.Green;
                default: return PdfSharp.Drawing.XBrushes.White;
            }
        }
        private XFont ToFont(PicGraphics.TextType tType)
        {
            double fontHeight = pdfDocument.Pages[0].Height / 50.0;
            switch (tType)
            {
                case TextType.FT_COTATION: return new XFont("Arial", fontHeight);
                default: throw new Exception("Unknown text type");
            }
        }
        #endregion

        #region Public properties
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        #endregion

        #region Public method
        /// <summary>
        /// Access the result byte array
        /// </summary>
        /// <returns></returns>
        public byte[] GetResultByteArray()
        {
            MemoryStream stream = new MemoryStream();
            pdfDocument.Save(stream, false);
            return stream.ToArray();
        }
        #endregion
    }
}
#endif