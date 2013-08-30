#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Pic.Factory2D;
using Pic.Plugin;
#endregion

namespace Pic.Data
{
    public class ThumbnailGeneratorPlugin : IThumbnailGenerator
    {
        public Image getThumbnailImageFromDocument(string filePath, string fileType, Size size)
        {
            Image image;
            using (Tools pluginTools = new Pic.Plugin.Tools(filePath, new ComponentSearchMethodDB()))
            {
                pluginTools.ShowCotations = false;
                pluginTools.GenerateImage(size, out image);
            }
            return image;
        }
    }
}
