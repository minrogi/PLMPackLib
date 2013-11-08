#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using Sharp3D.Math.Core;

using Pic.Plugin;
using Pic.Factory2D;
#endregion

namespace Pic.Plugin
{
    public class Tools : IDisposable
    {
        #region Data members
        private Component _component;
        private bool _reflexionX = false, _reflexionY = false;
        private bool _showCotations = true;
        public static readonly int _fontSize = 10;
        public static readonly Color _fontColor = Color.White;
        public static readonly Color _backgroundColor = Color.Black;
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates the <see cref="Tools"/> class
        /// </summary>
        /// <param name="filepath">Component file path</param>
        public Tools(string filepath, IComponentSearchMethod searchMethod)
        {
            ComponentLoader loader = new ComponentLoader();
            loader.SearchMethod = searchMethod;
            _component = loader.LoadComponent(filepath);
        }
        /// <summary>
        /// Instantiates the <see cref="Tools"/> class
        /// </summary>
        /// <param name="component">An instance of the <see cref="Component"/> class.</param>
        public Tools(Component component, IComponentSearchMethod searchMethod)
        {
            _component = component;
        }
        #endregion

        #region Public properties
        internal Component Component
        {
            get { return _component; }
        }
        public bool ReflexionX
        {
            get { return _reflexionX; }
            set { _reflexionX = value; }
        }
        public bool ReflexionY
        {
            get { return _reflexionY; }
            set { _reflexionY = value; }
        }
        public bool ShowCotations
        {
            get { return _showCotations; }
            set { _showCotations = value; }
        }
        public string SourceCode
        {
            get
            {
                if (null == _component)
                    throw new Pic.Plugin.PluginException("No plugin loaded");
                return _component.SourceCode;
            }
        }
        public Guid Guid
        {
            get
            {
                if (null == _component)
                    throw new Pic.Plugin.PluginException("No plugin loaded");
                return _component.Guid;
            }
        }
        public string Name
        {
            get
            {
                if (null == _component)
                    throw new Pic.Plugin.PluginException("No plugin loaded");
                return _component.Name;
            }            
        }
        public string Description
        {
            get
            {
                if (null == _component)
                    throw new Pic.Plugin.PluginException("No plugin loaded");
                return _component.Description;
            }            
        }

        public string Version
        {
            get
            {
                if (null == Component)
                    throw new Pic.Plugin.PluginException("No plugin loaded");
                return _component.Version;
            }
        }
        #endregion

        #region Load method
        /// <summary>
        /// Loads a plugin from a given file path
        /// </summary>
        /// <param name="filepath">Plugin file path</param>
        private void LoadPluginFromFile(string filepath)
        {
            ComponentLoader loader = new ComponentLoader();            
            _component = loader.LoadComponent(filepath);
        }
        #endregion

        #region Tool methods
        /// <summary>
        /// Generate an image from plugin with default parameters
        /// </summary>
        /// <param name="size"></param>
        /// <param name="filter"></param>
        /// <param name="bmp"></param>
        public void GenerateImage(Size size, out Image bmp)
        {
            // check if plugin was instantiated
            if (null == _component)
                throw new Exception("No valid plugin instance loaded!");

            // instantiate factory
            PicFactory factory = new PicFactory();
            // generate entities
            ParameterStack stack = _component.BuildParameterStack(null);
            _component.CreateFactoryEntities(factory, stack);
            // apply any needed transformation
            if (_reflexionX) factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionX));
            if (_reflexionY) factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionY));
            // get bounding box
            PicVisitorBoundingBox visitor = new PicVisitorBoundingBox();
            factory.ProcessVisitor(visitor);
            Box2D box = visitor.Box;
            box.AddMarginRatio(0.05);
            // draw image
            PicGraphicsImage picImage = new PicGraphicsImage(size, box);
            factory.Draw(picImage, _showCotations ? PicFilter.FilterNone : PicFilter.FilterCotation);

            bmp = picImage.Bitmap;
        }

        public void GenerateImage(Size size, ParameterStack stack, out Image bmp)
        {
            // check if plugin was instantiated
            if (null == _component)
                throw new Exception("No valid plugin instance loaded!");

            // instantiate factory
            PicFactory factory = new PicFactory();
            // generate entities
            _component.CreateFactoryEntities(factory, stack);
            // apply any needed transformation
            if (_reflexionX) factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionX));
            if (_reflexionY) factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionY));
            // get bounding box
            PicVisitorBoundingBox visitor = new PicVisitorBoundingBox();
            factory.ProcessVisitor(visitor);
            Pic.Factory2D.Box2D box = visitor.Box;
            box.AddMarginRatio(0.05);
            // draw image
            PicGraphicsImage picImage = new PicGraphicsImage(size, box);
            factory.Draw(picImage, _showCotations ? PicFilter.FilterNone : PicFilter.FilterCotation);

            bmp = picImage.Bitmap;
        }

        public byte[] GenerateImage(Size size, ParameterStack stack, ImageFormat format)
        {
            Image bmp;
            GenerateImage(size, stack, out bmp);
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, format);
            return ms.GetBuffer();
        }

        public byte[] GenerateExportFile(string fileFormat, ParameterStack stack)
        {
            // build factory
            Pic.Factory2D.PicFactory factory = new Pic.Factory2D.PicFactory();
            _component.CreateFactoryEntities(factory, stack);
            if (_reflexionX) factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionX));
            if (_reflexionY) factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionY));

            // instantiate filter
            PicFilter filter = (_showCotations ? PicFilter.FilterNone : PicFilter.FilterCotation) & PicFilter.FilterNoZeroEntities;

            // get bounding box
            Pic.Factory2D.PicVisitorBoundingBox visitorBoundingBox = new Pic.Factory2D.PicVisitorBoundingBox();
            factory.ProcessVisitor(visitorBoundingBox, filter);
            Pic.Factory2D.Box2D box = visitorBoundingBox.Box;
            // add margins : 5 % of the smallest value among height 
            box.AddMarginHorizontal(box.Width * 0.05);
            box.AddMarginVertical(box.Height * 0.05);
            // get file content
            if ("des" == fileFormat)
            {
                Pic.Factory2D.PicVisitorDesOutput visitor = new Pic.Factory2D.PicVisitorDesOutput();
                visitor.Author = "treeDiM";
                // process visitor
                factory.ProcessVisitor(visitor, filter);
                return visitor.GetResultByteArray();
            } 
            else if ("dxf" == fileFormat)
            {
                Pic.Factory2D.PicVisitorOutput visitor = new Pic.Factory2D.PicVisitorDxfOutput();
                visitor.Author = "treeDiM";
                // process visitor
                factory.ProcessVisitor(visitor, filter);
                return visitor.GetResultByteArray();

            }
            else if ("pdf" == fileFormat)
            {
                PicGraphicsPdf graphics = new PicGraphicsPdf(box);
                graphics.Author = "treeDiM";
                graphics.Title = "Pdf Export";
                // draw
                factory.Draw(graphics, filter);
                return graphics.GetResultByteArray();
            }
            else
                throw new Exception("Invalid file format : " + fileFormat);
        }

        public void Dimensions(ParameterStack stack, out double width, out double height, out double lengthCut, out double lengthFold)
        { 
            // build factory
            Pic.Factory2D.PicFactory factory = new Pic.Factory2D.PicFactory();
            _component.CreateFactoryEntities(factory, stack);
            if (_reflexionX) factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionX));
            if (_reflexionY) factory.ProcessVisitor(new PicVisitorTransform(Transform2D.ReflectionY));
            // get bounding box
            Pic.Factory2D.PicVisitorBoundingBox visitorBoundingBox = new Pic.Factory2D.PicVisitorBoundingBox();
            factory.ProcessVisitor(visitorBoundingBox, _showCotations ? PicFilter.FilterNone : PicFilter.FilterCotation);
            Pic.Factory2D.Box2D box = visitorBoundingBox.Box;
            width = box.Width;
            height = box.Height;
            // get length
            Pic.Factory2D.PicVisitorDieCutLength visitorLength = new Pic.Factory2D.PicVisitorDieCutLength();
            factory.ProcessVisitor(visitorLength, PicFilter.FilterNone);
            lengthCut = visitorLength.Lengths.ContainsKey(PicGraphics.LT.LT_CUT) ? visitorLength.Lengths[PicGraphics.LT.LT_CUT] : 0.0;
            lengthFold = visitorLength.Lengths.ContainsKey(PicGraphics.LT.LT_CREASING) ? visitorLength.Lengths[PicGraphics.LT.LT_CREASING] : 0.0;
        }

        public string GetInsertionCode()
        {
            Pic.Plugin.ParameterStack stackIn = _component.BuildParameterStack(null);
            string csCode = string.Empty;
            csCode += "\n";
            csCode += "\t\t{ // " + _component.Name + "\n";
            csCode += "\t\t\tIPlugin pluginIn = Host.GetPluginByGuid(\"" + _component.Guid.ToString()+ "\");\n";
            csCode += "\t\t\tParameterStack stackIn = Host.GetInitializedParameterStack(pluginIn);\n";
            foreach (Parameter param in stackIn)
            {
                // double
                ParameterDouble paramDouble = param as ParameterDouble;
                if (null != paramDouble)
                    csCode += "\t\t\tstackIn.SetDoubleParameter(\"" + paramDouble.Name + "\"," + paramDouble.ValueDefault.ToString() + ");\t\t// " + paramDouble.Description + "\n";

                // bool
                ParameterBool paramBool = param as ParameterBool;
                if (null != paramBool)
                    csCode += "\t\t\tstackIn.SetBoolParameter(\"" + paramBool.Name + "\"," + (paramBool.ValueDefault ? "true" : "false") + ");\t\t// " + paramBool.Description + "\n";

                // int
                ParameterInt paramInt = param as ParameterInt;
                if (null != paramInt)
                    csCode += "\t\t\tstackIn.SetIntParameter(\"" + paramInt.Name + "\"," + paramInt.ValueDefault.ToString() + ");\t\t// " + paramInt.Description + "\n";

                // multi
                ParameterMulti paramMulti = param as ParameterMulti;
                if (null != paramMulti)
                    csCode += "\t\t\tstackIn.SetMultiParameter(\"" + paramMulti.Name + "\"," + paramMulti.Value.ToString() + ");\t\t// " + paramMulti.Description + "\n";
            }
            csCode += "\t\t\tbool reflectionX = false, reflectionY = false;\n";
            csCode += "\t\t\tTransform2D transfReflect = (reflectionY ? Transform2D.ReflectionY : Transform2D.Identity) * (reflectionX ? Transform2D.ReflectionX : Transform2D.Identity);\n";
            csCode += "\t\t\tpluginIn.CreateFactoryEntities(fTemp, stackIn,\n";
			csCode += "\t\t\t\t Transform2D.Translation(new Vector2D(0.0, 0.0))\n";
			csCode += "\t\t\t\t *Transform2D.Rotation(0.0)\n";
			csCode += "\t\t\t\t *transfReflect);\n";
            csCode += "\t\t}\n";
            return csCode;
        }
        #endregion

        #region Static methods
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
        #endregion

        #region IDisposable implementation
        public void Dispose()
        {
        }
        #endregion
    }
}
