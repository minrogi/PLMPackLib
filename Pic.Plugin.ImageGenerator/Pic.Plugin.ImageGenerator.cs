#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using Pic.Plugin;
using Pic.Factory2D;
#endregion

namespace Pic.Plugin
{
    #region Static methods
    public class ImageGenerator
    {
        public static void Generate(string pluginPath, out Image bmp, Size size, PicFilter filter)
        {
            // instantiate factory
            PicFactory factory = new PicFactory();
            factory.Clear();

            // instantiate plugin
            Pic.Plugin.PluginServices pluginHost = new Pic.Plugin.PluginServices(pluginPath);
            IPlugin plugin = pluginHost.GetFirstPlugin();

            // generate entities
            ParameterStack stack = plugin.Parameters;
            plugin.CreateFactoryEntities(factory, stack);

            // get bounding box
            PicVisitorBoundingBox visitor = new PicVisitorBoundingBox();
            factory.ProcessVisitor(visitor);

            // draw image
            PicGraphicsImage picImage = new PicGraphicsImage();
            picImage.ImageSize = size;
            PicBox2D box = visitor.Box;
            box.AddMarginRatio(0.05);
            picImage.DrawingBox = box;
            factory.Draw(picImage, filter);

            bmp = picImage.Bitmap;
        }

        /// <summary>
        /// Annotates an image with specified string
        /// </summary>
        /// <param name="image">Image that will be annotated</param>
        /// <param name="annotation">Annotation</param>
        public static void Annotate(Image image, string annotation)
        { 
            // graphics
            Graphics grph = Graphics.FromImage(image);
            Font tfont = new Font("Arial", _fontSize);
            Brush tbrush = new SolidBrush(_fontColor);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;
            sf.LineAlignment = StringAlignment.Far;
            Size txtSize = grph.MeasureString(annotation, tfont).ToSize();
            grph.FillRectangle(new SolidBrush(_backgroundColor), new Rectangle(image.Width - txtSize.Width - 2, image.Height - txtSize.Height - 2, txtSize.Width + 2, txtSize.Height + 2));
            grph.DrawString(annotation, tfont, tbrush, new PointF(image.Width - 2, image.Height - 2), sf);
        }

        public static int _fontSize = 10;
        public static Color _fontColor = Color.White;
        public static Color _backgroundColor = Color.Black;
    }
    #endregion
}
