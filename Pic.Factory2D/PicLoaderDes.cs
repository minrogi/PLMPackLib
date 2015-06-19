#region Using directives
using System;
using System.Collections.Generic;
using System.Text;

using Sharp3D.Math.Core;
using DesLib4NET;

using netDxf;
#endregion

namespace Pic.Factory2D
{
    public class PicLoaderDes : DES_CreationAdapter, IDisposable
    {
        #region Constructor
        public PicLoaderDes(PicFactory factory)
        {
            // initialize data member factory
            _factory = factory;
            // clear factory before loading data
            _factory.Clear();
        }
        #endregion

        #region IDisposable override
        public void Dispose()
        { 
        }
        #endregion

        #region DES_CreationAdapter override
        public void Set2D()
        { 
        }
        public void Set3D()
        { 
        }
        public void SetViewWindow(float xmin, float ymin, float xmax, float ymax)
        {
        }
        public void AddSuperBaseHeader(DES_SuperBaseHeader header)
        { 
        }
        public void AddPoint(DES_Point point)
        { 
        }
        /// <summary>
        /// insert new segment
        /// </summary>
        /// <param name="segment">segment to insert</param>
        public void AddSegment(DES_Segment segment)
        {
            PicSegment picSegment = _factory.AddSegment(
                DesPenToLineType(segment._pen), segment._grp, segment._layer,
                new Vector2D(segment.X1, segment.Y1),
                new Vector2D(segment.X2, segment.Y2));
        }
        /// <summary>
        /// insert new arc
        /// </summary>
        /// <param name="arc"></param>
        public void AddArc(DES_Arc arc)
        {
           	float angle0 = arc._dir
		          , angle1 = arc._dir + arc._angle;

	        if (arc._angle > 0.0f)
	        {
	        }
	        else
	        {
		        angle0 += arc._angle;
		        angle1 -= arc._angle;

		        while (angle0 < 0)
		        {
			        angle0 += 360.0f;
			        angle1 += 360.0f;
		        }
	        }

            PicArc picArc = _factory.AddArc(
                DesPenToLineType(arc._pen), arc._grp, arc._layer,
                new Vector2D(arc._x, arc._y),
                (double)arc._dim,
                (double)angle0,
                (double)angle1
                );
        }
        public void AddBezier(DES_Bezier bezier)
        { 
        }
        public void AddNurbs(DES_Nurbs nurbs)
        { 
        }
        public void AddText(DES_Text text)
        { 
        }
        public void AddPose(DES_Pose pose)
        { 
        }
        public void AddSurface(DES_Surface surface)
        { 
        }
        public void AddDimensionOuterRadius()
        { 
        }
        public void AddDimensionInnerRadius()
        { 
        }
        public void AddDimensionOuterDiameter()
        {        
        }
        public void AddDimensionInnerDiameter()
        { 
        }
        public void AddDimensionDistance(DES_CotationDistance dimension)
        {
            PicCotation.CotType type = PicCotation.CotType.COT_DISTANCE;
            if (Math.Abs(dimension._dir) < 1.0)
                type = PicCotation.CotType.COT_HORIZONTAL;
            else if (Math.Abs(dimension._dir - 90.0) < 1.0)
                type = PicCotation.CotType.COT_VERTICAL;
            else
                type = PicCotation.CotType.COT_DISTANCE;

            _factory.AddCotation(
                type
                , dimension._grp, dimension._layer
                , new Vector2D( dimension.X1, dimension.Y1)
                , new Vector2D( dimension.X2, dimension.Y2)
                , dimension._offset
                , dimension._text
                , dimension._noDecimals);
        }
        public void AddDimensionAngle()
        { 
        }
        public void AddDimensionArrow()
        { 
        }
        public void UpdateQuestionnaire(Dictionary<string, string> questions)
        {
            _factory.UpdateQuestions(questions);
        }
        #endregion

        #region Helpers
        PicGraphics.LT DesPenToLineType(byte pen)
        {
            switch (pen)
            {
                // perfos-rainant (2) , les perfos (4) et les mi-chair (5) se comportent comme des rainants (6)
                case 1: return PicGraphics.LT.LT_CUT;
                case 2: return PicGraphics.LT.LT_PERFOCREASING;
                case 3: return PicGraphics.LT.LT_CONSTRUCTION;
                case 4: return PicGraphics.LT.LT_PERFO;
                case 5: return PicGraphics.LT.LT_HALFCUT;
                case 6: return PicGraphics.LT.LT_CREASING;
                case 7: return PicGraphics.LT.LT_AXIS;
                case 8: return PicGraphics.LT.LT_COTATION;
                case 9: return PicGraphics.LT.LT_ORIGIN;
                case 10: return PicGraphics.LT.LT_GRID;
                case 11: return PicGraphics.LT.LT_BRIDGES;
                default: return PicGraphics.LT.LT_DEFAULT;
            }
        }
        #endregion

        #region Data members
        PicFactory _factory;
        #endregion
    }

    public class PicLoaderDxf: IDisposable
    {
        #region Constructor
        public PicLoaderDxf(PicFactory factory)
        {
            // initialize data member factory
            _factory = factory;
            // clear factory before loading data
            _factory.Clear();
        }
        #endregion

        #region IDisposable override
        public void Dispose()
        {
        }
        #endregion

        #region Loading
        public void Load(string filePath)
        {
            _dxf = new DxfDocument();
            _dxf.Load(filePath);

            foreach (netDxf.Tables.LineType lineType in _dxf.LineTypes)
            {
                _lineTypeDictionary.Add(lineType, PicGraphics.LT.LT_CUT);
                _lineType2GrpDictionary.Add(lineType, 0);
            }
        }

        public void FillFactory()
        {
            // points
            foreach (netDxf.Entities.Point pt in _dxf.Points)
            {
                PicPoint picPoint = _factory.AddPoint(
                    DxfLineType2PicLT(pt.LineType), 0, 0
                    , new Vector2D(pt.Location.X, pt.Location.Y));
                picPoint.Group = DxfLineType2PicGrp(pt.LineType);
            }

            // lines
            foreach (netDxf.Entities.Line line in _dxf.Lines)
            {
                PicSegment picSegment = _factory.AddSegment(
                    DxfLineType2PicLT(line.LineType), 0, 0
                    , new Vector2D(line.StartPoint.X, line.StartPoint.Y)
                    , new Vector2D(line.EndPoint.X, line.EndPoint.Y)
                    );
                picSegment.Group = DxfLineType2PicGrp(line.LineType);
            }

            // arcs
            foreach (netDxf.Entities.Arc arc in _dxf.Arcs)
            {
                PicArc picArc = _factory.AddArc(
                    DxfLineType2PicLT(arc.LineType), DxfLineType2PicGrp(arc.LineType), 0
                    , new Vector2D(arc.Center.X, arc.Center.Y)
                    , arc.Radius
                    , 360.0 - arc.EndAngle, 360.0 - arc.StartAngle
                    );
            }

            foreach (netDxf.Entities.Circle circle in _dxf.Circles)
            {
                PicArc picArc = _factory.AddArc(
                    DxfLineType2PicLT(circle.LineType), 0, 0
                    , new Vector2D(circle.Center.X, circle.Center.Y)
                    , circle.Radius
                    , 0.0, 360.0);
            }

            // polylines
            foreach (netDxf.Entities.Polyline polyLine in _dxf.Polylines)
            {
                if (polyLine.Vertexes.Count < 2)
                    continue;

                int iVertexCount = 0;
                Vector2D startPoint = new Vector2D();
                foreach (netDxf.Entities.PolylineVertex vertex in polyLine.Vertexes)
                {
                    Vector2D endPoint = new Vector2D(vertex.Location.X, vertex.Location.Y);
                    if (iVertexCount > 0)
                    {
                        PicSegment picSegment = _factory.AddSegment(
                            DxfLineType2PicLT(polyLine.LineType), 0, 0
                            , startPoint
                            , endPoint);
                    }

                    startPoint = endPoint;
                    ++iVertexCount;
                }
            }
        }
        #endregion

        #region Helpers
        public PicGraphics.LT DxfLineType2PicLT(netDxf.Tables.LineType lineType)
        {
            if (_lineTypeDictionary.ContainsKey(lineType))
                return _lineTypeDictionary[lineType];
            else
                return PicGraphics.LT.LT_CUT;
        }
        short DxfLineType2PicGrp(netDxf.Tables.LineType lineType)
        {
            if (_lineType2GrpDictionary.ContainsKey(lineType))
                return _lineType2GrpDictionary[lineType];
            else
                return 0;
        }
        #endregion

        #region Properties
        public Dictionary<netDxf.Tables.LineType, Pic.Factory2D.PicGraphics.LT> LineTypeDictionary
        {
            get { return _lineTypeDictionary; }
            set { _lineTypeDictionary = value; }
        }
        public Dictionary<netDxf.Tables.LineType, short> LineType2GrpDictionary
        {
            get { return _lineType2GrpDictionary; }
            set { _lineType2GrpDictionary = value; }
        }
        #endregion

        #region Data members
        private PicFactory _factory;
        private Dictionary<netDxf.Tables.LineType, Pic.Factory2D.PicGraphics.LT> _lineTypeDictionary = new Dictionary<netDxf.Tables.LineType, Pic.Factory2D.PicGraphics.LT>();
        private Dictionary<netDxf.Tables.LineType, short> _lineType2GrpDictionary = new Dictionary<netDxf.Tables.LineType, short>();
        private DxfDocument _dxf = new DxfDocument();
        #endregion
    }
}
