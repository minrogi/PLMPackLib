#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
#endregion

namespace Pic.DAL
{
    #region Format handlers
    public abstract class FormatHandler
    {
        public bool SupportExtension(string extension)
        {
            List<string> supportedExtension = new List<string>(SupportedExtensions);
            return supportedExtension.Exists(ext => ext == extension);
        }
        public string ThumbnailFile
        {
            get { return Path.Combine(ApplicationConfiguration.CustomSection.ThumbnailsPath, ThumbnailName); }
        }
        public abstract bool CanGenerateThumbnail { get; }
        public abstract string ThumbnailName { get; }
        public abstract string FilterString { get; }
        public abstract string[] SupportedExtensions { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string Application { get; }
        public virtual bool OpenInPLMPackLib { get { return false; } }
    }
    public class FormatDLL : FormatHandler
    {
        public override bool CanGenerateThumbnail { get { return true; } }
        public override string ThumbnailName { get { return ""; } }
        public override string FilterString { get { return "Component (*.dll)|*.dll|"; } }
        public override string[] SupportedExtensions { get { return new string[] { "dll" }; } }
        public override string Name { get { return "Parametric Component"; } }
        public override string Description { get { return "treeDim PLMPackLib component"; } }
        public override string Application { get { return "PLMPackLib"; } }
        public override bool OpenInPLMPackLib { get { return true; } }
    }
    public class FormatDES : FormatHandler
    {
        public override bool CanGenerateThumbnail { get { return true; } }
        public override string ThumbnailName { get { return "PicGEOM.bmp"; } }
        public override string FilterString { get { return "Picador (*.des)|*.des|"; } }
        public override string[] SupportedExtensions { get { return new string[] { "des" }; } }
        public override string Name { get { return "treeDim des"; } }
        public override string Description { get { return "treeDim des drawing"; } }
        public override string Application { get { return "Picador"; } }
        public override bool OpenInPLMPackLib { get { return true; } }
    }

    public class FormatDES3 : FormatHandler
    {
        public override bool CanGenerateThumbnail { get { return true; } }
        public override string ThumbnailName { get { return "Picador3D.bmp"; } }
        public override string FilterString { get { return "Picador (*.des3)|*.des3|"; } }
        public override string[] SupportedExtensions { get { return new string[] { "des3" }; } }
        public override string Name { get { return "treeDim des3"; } }
        public override string Description { get { return "treeDim des3 drawing"; } }
        public override string Application { get { return "Picador3D"; } }
        public override bool OpenInPLMPackLib { get { return true; } }
    }

    public class FormatDXF : FormatHandler
    {
        public override bool CanGenerateThumbnail { get { return true; } }
        public override string ThumbnailName { get { return "dxf.bmp"; } }
        public override string FilterString { get { return "Autocad (*.dxf)|*.dxf|"; } }
        public override string[] SupportedExtensions { get { return new string[] { "dxf" }; } }
        public override string Name { get { return "autodesk dxf"; } }
        public override string Description { get { return "Autodesk data exchange format"; } }
        public override string Application { get { return "AutoCAD"; } }
        public override bool OpenInPLMPackLib { get { return true; } }
    }
    public class FormatCF2 : FormatHandler
    {
        public override bool CanGenerateThumbnail { get { return true; } }
        public override string ThumbnailName { get { return "cf2.bmp"; } }
        public override string FilterString { get { return "Common File Format 2 (*.cf2)|*.cf2|"; } }
        public override string[] SupportedExtensions { get { return new string[] { "cf2" }; } }
        public override string Name { get { return "CFF2"; } }
        public override string Description { get { return "Common File Format 2"; } }
        public override string Application { get { return "Picador"; } }
    }
    public class FormatAI : FormatHandler
    {
        public override bool CanGenerateThumbnail { get { return false; } }
        public override string ThumbnailName { get { return "ai.bmp"; } }
        public override string FilterString { get { return "Adobe Illustrator (*.ai)|*.ai|"; } }
        public override string[] SupportedExtensions { get { return new string[] { "ai" }; } }
        public override string Name { get { return "Adobe Illustrator"; } }
        public override string Description { get { return "Adobe Illustrator"; } }
        public override string Application { get { return "Illustrator"; } }
    }
    public class FormatEPS : FormatHandler
    {
        public override bool CanGenerateThumbnail { get { return false; } }
        public override string ThumbnailName { get { return "eps.bmp"; } }
        public override string FilterString { get { return "Encapsulated Postscripts (*.eps)|*.eps|"; } }
        public override string[] SupportedExtensions { get { return new string[] { "eps" }; } }
        public override string Name { get { return "Encapsulated Postscripts"; } }
        public override string Description { get { return "Encapsulated Postscripts"; } }
        public override string Application { get { return "Postscript"; } }
    }
    public class FormatImage : FormatHandler
    {
        public override bool CanGenerateThumbnail { get { return true; } }
        public override string ThumbnailName { get { return ""; } }
        public override string FilterString { get { return "Image files (*.bmp;*.gif;*.jpg;*.png)|*.bmp;*.gif;*.jpg;*.jpeg;*.png|"; } }
        public override string[] SupportedExtensions { get { return new string[] { "bmp", "gif", "jpg", "jpeg", "png" }; } }
        public override string Name { get { return "raster image"; } }
        public override string Description { get { return "Raster image"; } }
        public override string Application { get { return "Picture viewer"; } }
        public override bool OpenInPLMPackLib { get { return true; } }
    }
    public class FormatPDF : FormatHandler
    {
        public override bool CanGenerateThumbnail { get { return false; } }
        public override string ThumbnailName { get { return "pdf.bmp"; } }
        public override string FilterString { get { return "Adobe Acrobat Reader (*.pdf)|*.pdf|"; } }
        public override string[] SupportedExtensions { get { return new string[] { "pdf" }; } }
        public override string Name { get { return "Adobe Acrobat"; } }
        public override string Description { get { return "Adobe Portable Document Format"; } }
        public override string Application { get { return "Adobe acrobat reader"; } }
        public override bool OpenInPLMPackLib { get { return true; } }
    }
    public class FormatWORD : FormatHandler
    {
        public override bool CanGenerateThumbnail { get { return false; } }
        public override string ThumbnailName { get { return "Word.bmp"; } }
        public override string FilterString { get { return "Microsoft Word Document (*.doc;*.docx)|*.doc;*.docx|"; } }
        public override string[] SupportedExtensions { get { return new string[] { "doc", "docx" }; } }
        public override string Name { get { return "MS Word"; } }
        public override string Description { get { return "Microsoft Office Word"; } }
        public override string Application { get { return "MS Word"; } }
    }
    public class FormatEXCEL : FormatHandler
    {
        public override bool CanGenerateThumbnail { get { return false; } }
        public override string ThumbnailName { get { return "Excel.bmp"; } }
        public override string FilterString { get { return "Microsoft Excel Sheet (*.xls;*.xlsx)|*.xls;*.xlsx|"; } }
        public override string[] SupportedExtensions { get { return new string[] { "xls", "xlsx" }; } }
        public override string Name { get { return "MS Excel"; } }
        public override string Description { get { return "Microsoft Office Excel"; } }
        public override string Application { get { return "MS Excel"; } }
    }
    public class FormatPPT : FormatHandler
    {
        public override bool CanGenerateThumbnail { get { return false; } }
        public override string ThumbnailName { get { return "Powerpoint.bmp"; } }
        public override string FilterString { get { return "Microsoft Powerpoint (*.ppt)|*.ppt|"; } }
        public override string[] SupportedExtensions { get { return new string[] { "ppt" }; } }
        public override string Name { get { return "MS Powerpoint"; } }
        public override string Description { get { return "Microsoft Powerpoint"; } }
        public override string Application { get { return "MS Powerpoint"; } }
    }
    public class FormatWRITE : FormatHandler
    {
        public override bool CanGenerateThumbnail { get { return false; } }
        public override string ThumbnailName { get { return "Writer.bmp"; } }
        public override string FilterString { get { return "Open Office Write (*.sxw;*.stw)|*.sxw;*.stw|"; } }
        public override string[] SupportedExtensions { get { return new string[] { "sxw", "stw" }; } }
        public override string Name { get { return "Open Office Write"; } }
        public override string Description { get { return "Open Office Write"; } }
        public override string Application { get { return "OO Write"; } }
    }
    public class FormatCALC : FormatHandler
    {
        public override bool CanGenerateThumbnail { get { return false; } }
        public override string ThumbnailName { get { return "Calc.bmp"; } }
        public override string FilterString { get { return "Open Office Calc (*.sxc;*.stc)|*.sxc;*.stc|"; } }
        public override string[] SupportedExtensions { get { return new string[] { "sxc", "stc" }; } }
        public override string Name { get { return "Open Office Calc"; } }
        public override string Description { get { return "Open Office Calc"; } }
        public override string Application { get { return "OO Calc"; } }
    }
    public class FormatARD : FormatHandler
    {
        public override bool CanGenerateThumbnail { get { return false; } }
        public override string ThumbnailName { get { return "ard.bmp"; } }
        public override string FilterString { get { return "ArtiosCAD (*.ard)|*.ard"; } }
        public override string[] SupportedExtensions { get { return new string[] { "ard" }; } }
        public override string Name { get { return "ArtiosCAD"; } }
        public override string Description { get { return "ArtiosCAD"; } }
        public override string Application { get { return "ArtiosCAD"; } }
    }
    #endregion

    public class FFormatManager
    {
        #region Public methods
        public static FormatHandler GetFormatHandlerFromFileExt(string fileExt)
        {
            foreach (FormatHandler fHandler in _formatHandlers)
            {
                if (fHandler.SupportExtension(fileExt))
                    return fHandler;
            }
            return null;    
        }
        public static FormatHandler GetFormatHandlerFromFilePath(string filePath)
        {
            string fileExt = System.IO.Path.GetExtension(filePath);
            fileExt = fileExt.Substring(1).ToLower();
            return GetFormatHandlerFromFileExt(fileExt);
        }
        #endregion

        #region Filter
        public static string Filters
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Supported formats (");
                sb.Append(ExtensionString);
                sb.Append(")");
                sb.Append("|");
                sb.Append(ExtensionString);
                sb.Append("|");

                foreach (FormatHandler fHandler in _formatHandlers)
                    sb.Append(fHandler.FilterString);

                sb.Append("All Files (*.*)|*.*|");

                return sb.ToString();
            }
        }
        public static string ExtensionString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                bool first = true;
                foreach (FormatHandler fHandler in _formatHandlers)
                {
                    foreach (string ext in fHandler.SupportedExtensions)
                    {
                        if (!first) { sb.Append(";");}
                        first = false; 
                        sb.Append("*.");
                        sb.Append(ext);
                    }
                }
                return sb.ToString();
            }
        }
        #endregion

        #region List of format handlers
        private static FormatHandler[] _formatHandlers = new FormatHandler[]
        {
            new FormatDLL()
            , new FormatDES()
            , new FormatDES3()
            , new FormatDXF()
            , new FormatCF2()
            , new FormatAI()
            , new FormatEPS()
            , new FormatPDF()
            , new FormatImage()
            , new FormatWORD()
            , new FormatEXCEL()
            , new FormatWRITE()
            , new FormatCALC()
            , new FormatARD()
        };
        #endregion
    }
}
