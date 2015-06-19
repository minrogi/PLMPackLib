#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

using Pic.Factory2D;
using DesLib4NET;
#endregion

namespace treeDiM.DrawingQuery
{
    public class DESQuerier
    {
        #region Data members
        private Pic.Factory2D.PicFactory _factory;
        #endregion

        #region Static methods
        public static DESQuerier LoadFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new DESQuerierFileNotFoundException(filePath);
            // instantiate factory
            PicFactory factory = new PicFactory();
            // instantiate loader
            PicLoaderDes picLoader = new PicLoaderDes(factory);
            // load file
            DesLib4NET.DES_FileReader reader = new DES_FileReader();
            reader.ReadFile(filePath, picLoader);
            // test for valid entities

            // return DESQuerier
            return new DESQuerier(factory);
        }
        #endregion

        #region Private Constructor
        // private constructor
        private DESQuerier(PicFactory factory)
        {
            _factory = factory;
        }
        #endregion

        #region Groups
        // acessing groups
        public int NoGroups
        {
            get { return 0; }
        }
        public Dictionary<short, string> Groups
        {
            get { return _factory.Groups; }
        }
        #endregion 

        #region Layers
        public int NoLayers
        {
            get { return 0; }
        }
        public Dictionary<short, string> Layers
        {
            get { return _factory.Layers; }
        }
        #endregion

        #region Questions
        public Dictionary<string, string> Questionnaire
        {
            get { return _factory.Questionnaire; }
        }
        public bool GetQuestionAnswer(string question, ref string answer)
        { 
            bool keyFound = _factory.Questionnaire.ContainsKey(question);
            if (keyFound)
                answer = _factory.Questionnaire[question];
            return keyFound;
        }
        #endregion

        #region Computed data
        public Box2D BoundingBox(bool withDimensions)
        { 
            // computing bounding box
            Box2D box = Box2D.Initial;
            using (PicVisitorBoundingBox visitor = new PicVisitorBoundingBox())
            {
                _factory.ProcessVisitor(visitor, withDimensions ? PicFilter.FilterNone : PicFilter.FilterCotation);
                box = visitor.Box;
            }
            return box;        
        }
        public Dictionary<string, double> DiecutLengthes
        {
            get
            {
                Dictionary<string, double> res = new Dictionary<string, double>();

                // collect diecut length
                PicVisitorDieCutLength visitor = new PicVisitorDieCutLength();
                _factory.ProcessVisitor(visitor);

                foreach (PicGraphics.LT lt in visitor.Lengths.Keys)
                {
                    double l = visitor.Lengths[lt];
                    if (l > 0)
                    {
                        switch (lt)
                        {
                            case PicGraphics.LT.LT_CUT:
                                res["CUT"] = l;
                                break;
                            case PicGraphics.LT.LT_CREASING:
                                res["CREASING"] = l;
                                break;
                            case PicGraphics.LT.LT_HALFCUT:
                                res["HALFCUT"] = l;
                                break;
                            case PicGraphics.LT.LT_PERFO:
                                res["PERFO"] = l;
                                break;
                            case PicGraphics.LT.LT_PERFOCREASING:
                                res["PERFOCREASING"] = l;
                                break;                        
                            default:
                                break;
                        }
                    }
                }

                return res;
            }
        }
        #endregion

        #region Image
        public Image GetImage(Size imageSize, bool showDimensions, List<short> groups, List<short> layers)
        {
            // computing bounding box
            Box2D box = BoundingBox(showDimensions);
            box.AddMarginRatio(0.05);
            // build filter
            PicFilter filter = new PicFilterListGroup(groups)
                & new PicFilterListLayer(layers)
                & (showDimensions ? PicFilter.FilterNone : PicFilter.FilterCotation);
            // cotations with long lines
            PicGlobalCotationProperties.ShowShortCotationLines = false;
            // drawing
            PicGraphicsImage picGraph = new PicGraphicsImage(imageSize, box);
            _factory.Draw(picGraph, filter);
            return picGraph.Bitmap;
        }
        public Image GetImage(int leadingDimension, bool showDimensions, List<short> groups, List<short> layers)
        { 
            // computing bounding box
            Box2D box = BoundingBox(showDimensions);
            box.AddMarginRatio(0.05);
            Size imageSize = box.GetSizeFromLeading(leadingDimension);
            // build filter
            PicFilter filter = new PicFilterListGroup(groups)
                & new PicFilterListLayer(layers)
                & (showDimensions ? PicFilter.FilterNone : PicFilter.FilterCotation);
            // cotations with long lines
            PicGlobalCotationProperties.ShowShortCotationLines = false;
            // drawing
            PicGraphicsImage picGraph = new PicGraphicsImage(imageSize, box);
            _factory.Draw(picGraph, showDimensions ? PicFilter.FilterNone : PicFilter.FilterCotation);
            return picGraph.Bitmap;
        }
        public void ImageToFile(string filePath, Size imageSize, bool showDimensions, List<short> groups, List<short> layers)
        {
            Image img = GetImage(imageSize, showDimensions, groups, layers);
            img.Save(filePath);
        }

        public void ImageToFile(string filePath, int leadingDimension, bool showDimensions, List<short> groups, List<short> layers)
        {
            Image img = GetImage(leadingDimension, showDimensions, groups, layers);
            img.Save(filePath);
        }
        #endregion
    }
}
