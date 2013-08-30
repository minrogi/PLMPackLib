#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using DesLib4NET;

using log4net;
#endregion

namespace Pic.Factory2D
{
    public class ThumbnailGenerator
    {
        #region Static methods
        public static void GenerateImage(Size size, PicFactory factory, bool showCotations, out Image bmp)
        {
            // get bounding box
            PicVisitorBoundingBox visitor = new PicVisitorBoundingBox();
            factory.ProcessVisitor(visitor);
            Box2D box = visitor.Box;
            box.AddMarginRatio(0.05);
            // draw image
            PicGraphicsImage picImage = new PicGraphicsImage(size, box);
            factory.Draw(picImage, showCotations ? PicFilter.FilterNone : PicFilter.FilterCotation);

            bmp = picImage.Bitmap;            
        }

        public static void GenerateImage(Size size, string filePath, string thumbnailFilePath)
        {
            // load file
            PicFactory factory = new PicFactory();
            string fileExt = System.IO.Path.GetExtension(filePath);
            if (string.Equals(".des", fileExt, StringComparison.CurrentCultureIgnoreCase))
            {
                PicLoaderDes picLoaderDes = new PicLoaderDes(factory);
                using (DES_FileReader fileReader = new DES_FileReader())
                    fileReader.ReadFile(filePath, picLoaderDes);
            }
            else if (string.Equals(".dxf", fileExt, StringComparison.CurrentCultureIgnoreCase))
            {
                using (PicLoaderDxf picLoaderDxf = new PicLoaderDxf(factory))
                {
                    picLoaderDxf.Load(filePath);
                    picLoaderDxf.FillFactory();
                }                
            }
            // draw image
            Image bmp;
            ThumbnailGenerator.GenerateImage(size, factory, false, out bmp);
            bmp.Save(thumbnailFilePath);
        }
        #endregion
    }
}
