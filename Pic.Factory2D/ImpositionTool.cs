#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using Sharp3D.Math.Core;
using System.Diagnostics;
using System.Drawing;

using log4net;
using log4net.Config;

using TreeDim.UserControls;
#endregion

namespace Pic.Factory2D
{
    #region BPosition
    public struct BPosition
    {
        #region Data members
        public Vector2D _pt;
        public double _angle;
        #endregion

        #region Constructor
        public BPosition(Vector2D pt, double angle)
        {
            _pt = pt;
            _angle = angle;
        }
        #endregion

        #region Public methods
        public Transform2D Transformation
        {
            get { return Transform2D.Translation(_pt) * Transform2D.Rotation(_angle); }
        }
        #endregion
    }
    #endregion

    #region Imposition pattern abstract class
    internal abstract class ImpositionPattern
    {
        #region Data members
        protected BPosition[,] _relativePositions;
        protected Box2D[,] _bboxes;
        protected Vector2D _patternStep;
        protected static readonly ILog _log = LogManager.GetLogger(typeof(ImpositionPattern));
        #endregion

        #region Abstract methods
        abstract public void GeneratePattern(IEntityContainer container, Vector2D minDistance, Vector2D impositionOffset, bool ortho);
        #endregion

        #region Abstract properties
        abstract public bool RequiresRotationInRows { get; }
        abstract public bool RequiresRotationInColumns { get; }
        abstract public int NoRows { get; }
        abstract public int NoCols { get; }
        abstract public string Name { get; }
        #endregion

        #region Public methods
        public ImpositionSolution GenerateSolution(ImpositionTool impositionTool
            , IEntityContainer container, CardboardFormat format
            , Vector2D margin, Vector2D minMargin, Vector2D spaceBetween
            , Vector2D impositionOffset, bool ortho)
        {
            // generate pattern
            try
            {
                double minSpacing = 0.0001;
                Vector2D spacing = new Vector2D(
                    impositionTool.SpaceBetween.X > minSpacing ? impositionTool.SpaceBetween.X : minSpacing
                    , impositionTool.SpaceBetween.Y > minSpacing ? impositionTool.SpaceBetween.Y : minSpacing
                    );
                GeneratePattern(impositionTool.InitialEntities, spacing, impositionTool.ImpositionOffset, ortho);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return null;
            }
            // instantiate solution
            ImpositionSolution solution = new ImpositionSolution(impositionTool.InitialEntities, Name, RequiresRotationInRows, RequiresRotationInColumns);

            // pattern global box
            Box2D boxGlobal = new Box2D();
            for (int i = 0; i < NoRows; ++i)
                for (int j = 0; j < NoCols; ++j)
                    boxGlobal.Extend(_bboxes[i, j]);

            // compute max number of patterns
            // in X direction
            int noPatternX = (int)Math.Floor((impositionTool.UsableFormat.Width - boxGlobal.Width) / _patternStep.X) + 1;
            // in Y direction
            int noPatternY = (int)Math.Floor((impositionTool.UsableFormat.Height - boxGlobal.Height) / _patternStep.Y) + 1;

            int[,] rowNumber = new int[NoRows, NoCols];
            int[,] colNumber = new int[NoRows, NoCols];

            int iMax = -1, jMax = -1;
            for (int i = 0; i < NoRows; ++i)
                for (int j=0; j < NoCols; ++j)
                {
                    if (_bboxes[i, j].XMax + noPatternX * _patternStep.X - boxGlobal.XMin < impositionTool.UsableFormat.Width)
                    {
                        rowNumber[i, j] = noPatternX + 1;
                        iMax = i;
                    }
                    else
                        rowNumber[i, j] = noPatternX;

                    if (_bboxes[i, j].YMax + noPatternY * _patternStep.Y - boxGlobal.YMin < impositionTool.UsableFormat.Height)
                    {
                        colNumber[i, j] = noPatternY + 1;
                        jMax = j;
                    }
                    else
                        colNumber[i, j] = noPatternY;
                }
            
            
            // compute actual margin
            double xMax = double.MinValue;
            double yMax = double.MinValue;
            for (int i=0; i<NoRows; ++i)
                for (int j = 0; j < NoCols; ++j)
                {
                    xMax = Math.Max(xMax, _bboxes[i, j].XMax + (rowNumber[i, j] - 1) * _patternStep.X);
                    yMax = Math.Max(yMax, _bboxes[i, j].YMax + (colNumber[i, j] - 1) * _patternStep.Y);
                }

            double xMargin = 0.0;
            switch (impositionTool.HorizontalAlignment)
            {
                case ImpositionTool.HAlignment.HALIGN_LEFT: xMargin = impositionTool.UsableFormat.XMin; break;
                case ImpositionTool.HAlignment.HALIGN_RIGHT: xMargin = format.Width - impositionTool.Margin.X - xMax + boxGlobal.XMin; break;
                case ImpositionTool.HAlignment.HALIGN_CENTER: xMargin = (format.Width - xMax + boxGlobal.XMin) * 0.5; break;
                default: break;
            }

            double yMargin = 0.0;
            switch (impositionTool.VerticalAlignment)
            {
                case ImpositionTool.VAlignment.VALIGN_BOTTOM: yMargin = impositionTool.UsableFormat.YMin;   break;
                case ImpositionTool.VAlignment.VALIGN_TOP: yMargin = format.Height - impositionTool.Margin.Y - yMax + boxGlobal.YMin; break;
                case ImpositionTool.VAlignment.VALIGN_CENTER: yMargin = (format.Height - yMax + boxGlobal.YMin) * 0.5; break;
                default: break;
            }

            // compute offsets
            double xOffset = xMargin - boxGlobal.XMin;
            double yOffset = yMargin - boxGlobal.YMin;

            for (int i=0; i<NoRows; ++i)
                for (int j = 0; j < NoCols; ++j)
                {
                    for (int k=0; k<rowNumber[i, j]; ++k)
                        for (int l = 0; l < colNumber[i, j]; ++l)
                        {
                            BPosition pos = _relativePositions[i, j];
                            pos._pt.X = xOffset + k * _patternStep.X + pos._pt.X;
                            pos._pt.Y = yOffset + l * _patternStep.Y + pos._pt.Y;
                            solution.Add(pos);
                        }
                }
            // noRows / noCols
            solution.Rows = NoCols * noPatternX + iMax + 1;
            solution.Cols = NoRows * noPatternY + jMax + 1;
            // cardboard position
            solution.CardboardPosition = new Vector2D(xMargin, yMargin);
            return solution;
        }

        #endregion

        #region Public properties
        public Vector2D PatternStep { get { return _patternStep; } }
        #endregion
    }
    #endregion

    #region Pattern implementations

    #region Default pattern
    internal class PatternDefault : ImpositionPattern
    {
        public override bool RequiresRotationInRows { get { return false; } }
        public override bool RequiresRotationInColumns { get { return false; } }
        public override int NoRows { get { return 1; } }
        public override int NoCols { get { return 1; } }
        public override string Name { get { return "Default"; } }
        public override void GeneratePattern(IEntityContainer container, Vector2D minDistance, Vector2D impositionOffset, bool ortho)
        {
            using (PicFactory factory = new PicFactory())
            {
                // instantiate block and BlockRef
                PicBlock block = factory.AddBlock(container);
                PicBlockRef blockRef00 = factory.AddBlockRef(block, new Vector2D(0.0, 0.0), ortho ? 90.0 : 0.0);

                // compute bounding box
                PicVisitorBoundingBox visitor = new PicVisitorBoundingBox();
                visitor.TakePicBlocksIntoAccount = false;
                factory.ProcessVisitor(visitor, PicFilter.FilterCotation);
                Box2D boxEntities = visitor.Box;

                // compute default X step
                PicBlockRef blockRef01 = factory.AddBlockRef(block, new Vector2D(5.0 * boxEntities.Width + minDistance.X, 0.0), ortho ? 90.0 : 0.0);
                double horizontalDistance = 0.0;
                if (!PicBlockRef.Distance(blockRef00, blockRef01, PicBlockRef.DistDirection.HORIZONTAL_RIGHT, out horizontalDistance))
                    throw new Exception("Failed to compute distance between to block refs");
                _patternStep.X = 5.0 * boxEntities.Width - horizontalDistance + 2.0 * minDistance.X;

                // compute default Y step
                PicBlockRef blockRef10 = factory.AddBlockRef(block, new Vector2D(0.0, 5.0 * boxEntities.Height + minDistance.Y), ortho ? 90.0 : 0.0);
                double verticalDistance = 0.0;
                if (!PicBlockRef.Distance(blockRef00, blockRef10, PicBlockRef.DistDirection.VERTICAL_TOP, out verticalDistance))
                    throw new Exception("Failed to compute distance between to block refs");
                _patternStep.Y = 5.0 * boxEntities.Height - verticalDistance + 2.0 * minDistance.Y;

                // positions
                _relativePositions = new BPosition[1, 1];
                BPosition position = new BPosition(Vector2D.Zero, ortho ? 90.0 : 0.0);
                _relativePositions[0,0]= position;

                // bboxes
                _bboxes = new Box2D[1, 1];
                _bboxes[0, 0] = boxEntities;
            }
        }
    }
    #endregion

    #region With rotation in rows
    internal class PatternRotationInRow : ImpositionPattern
    {
        public override bool RequiresRotationInRows { get { return true; } }
        public override bool RequiresRotationInColumns { get { return false; } }
        public override int NoRows { get { return 1; } }
        public override int NoCols { get { return 2; } }
        public override string Name { get { return "Rotated in row (1*2)"; } }
        public override void GeneratePattern(IEntityContainer container, Vector2D minDistance, Vector2D impositionOffset, bool ortho)
        {
            //
            // 10 11 12
            // 00 01 02
            //
            using (PicFactory factory = new PicFactory())
            {
                // instantiate block and BlockRef
                PicBlock block = factory.AddBlock(container);
                PicBlockRef blockRef00 = factory.AddBlockRef(block
                    , Vector2D.Zero
                    , ortho ? 90.0 : 0.0);

                // compute bounding box
                PicVisitorBoundingBox visitor = new PicVisitorBoundingBox();
                visitor.TakePicBlocksIntoAccount = false;
                factory.ProcessVisitor(visitor, new PicFilter());
                Box2D boxEntities = visitor.Box;

                // compute second entity position on row 0
                PicBlockRef blockRef01 = factory.AddBlockRef(block
                    , new Vector2D(
                        boxEntities.XMax + boxEntities.XMin + 5.0 * boxEntities.Width + (ortho ? 1.0 : 0.0) * impositionOffset.X
                        , boxEntities.YMax + boxEntities.YMin + (ortho ? 0.0 : 1.0) * impositionOffset.Y)
                    , ortho ? 270.0 : 180.0);
                double horizontalDistance = 0.0;
                if (!PicBlockRef.Distance(blockRef00, blockRef01, PicBlockRef.DistDirection.HORIZONTAL_RIGHT, out horizontalDistance))
                    throw new Exception("Failed to compute distance between to block refs");
                Vector2D vecPosition = new Vector2D(
                    boxEntities.XMax + boxEntities.XMin + 5.0 * boxEntities.Width + minDistance.X - horizontalDistance + (ortho ? 1.0 : 0.0) * impositionOffset.X
                    , boxEntities.YMax + boxEntities.YMin + (ortho ? 0.0 : 1.0) * impositionOffset.Y);
                blockRef01.Position = vecPosition;

                // positions
                _relativePositions = new BPosition[1, 2];
                _relativePositions[0, 0] = new BPosition(Vector2D.Zero, ortho ? 90.0 : 0.0);
                _relativePositions[0, 1] = new BPosition(vecPosition, ortho ? 270.0 : 180.0);
                // bboxes
                _bboxes = new Box2D[1, 2];
                _bboxes[0, 0] = boxEntities;
                _bboxes[0, 1] = blockRef01.Box;

                // compute Y step (row1 / row0)
                // row0
                List<PicBlockRef> listRow0 = new List<PicBlockRef>();
                listRow0.Add(blockRef00);
                listRow0.Add(blockRef01);
                // row1
                List<PicBlockRef> listRow1 = new List<PicBlockRef>();
                PicBlockRef blockRef10 = factory.AddBlockRef(
                    block
                    , new Vector2D(0.0, 5.0 * boxEntities.Height + minDistance.Y)
                    , ortho ? 90.0 : 0.0);
                PicBlockRef blockRef11 = factory.AddBlockRef(
                    block
                    , new Vector2D(vecPosition.X, vecPosition.Y + 5.0 * boxEntities.Height + minDistance.Y)
                    , ortho ? 270.0 : 180.0);
                listRow1.Add(blockRef10);
                listRow1.Add(blockRef11);

                double verticalDistance = 0.0;
                if (!PicBlockRef.Distance(listRow0, listRow1, PicBlockRef.DistDirection.VERTICAL_TOP, out verticalDistance))
                    throw new Exception("Failed to compute distance between to block refs");
                _patternStep.Y = 5.0 * boxEntities.Height - verticalDistance + 2.0 * minDistance.Y;

                blockRef10.Position = new Vector2D(0.0, _patternStep.Y);
                blockRef11.Position = new Vector2D(vecPosition.X, vecPosition.Y + _patternStep.Y);

                // compute X step (col1 / col2)
                PicBlockRef blockRef02 = factory.AddBlockRef(
                    block
                    , new Vector2D( boxEntities.XMin + 5.0 * boxEntities.Width + minDistance.X
                        , 0.0)
                    , ortho ? 90.0 : 0.0);
                PicBlockRef blockRef12 = factory.AddBlockRef(
                    block
                    , new Vector2D(boxEntities.XMin + 5.0 * boxEntities.Width + minDistance.X
                        , _patternStep.Y)
                    , ortho ? 90.0 : 0.0);

                List<PicBlockRef> listCol1 = new List<PicBlockRef>();
                listCol1.Add(blockRef01);
                listCol1.Add(blockRef11);
                List<PicBlockRef> listCol2 = new List<PicBlockRef>();
                listCol2.Add(blockRef02);
                listCol2.Add(blockRef12);

                horizontalDistance = 0.0;
                if (!PicBlockRef.Distance(listCol1, listCol2, PicBlockRef.DistDirection.HORIZONTAL_RIGHT, out horizontalDistance))
                    throw new Exception("Failed to compute distance between to block refs");
                _patternStep.X = boxEntities.XMin + 5.0 * boxEntities.Width - horizontalDistance + 3.0 * minDistance.X;
            }
        }
    }
    #endregion

    #region With rotation in colums
    internal class PatternRotationInColumn : ImpositionPattern
    {
        public override bool RequiresRotationInRows { get { return false; } }
        public override bool RequiresRotationInColumns { get { return true; } }
        public override int NoRows { get { return 2; } }
        public override int NoCols { get { return 1; } }
        public override string Name { get { return "Rotated in column"; } }
        public override void GeneratePattern(IEntityContainer container, Vector2D minDistance, Vector2D impositionOffset, bool ortho)
        {
            using (PicFactory factory = new PicFactory())
            {
                // 20 21
                // 10 11 12
                // 00 01 02
                //

                // instantiate block and BlockRef
                PicBlock block = factory.AddBlock(container);
                PicBlockRef blockRef00 = factory.AddBlockRef(block, new Vector2D(0.0, 0.0), ortho ? 90.0 : 0.0);

                // compute bounding box
                PicVisitorBoundingBox visitor = new PicVisitorBoundingBox();
                visitor.TakePicBlocksIntoAccount = false;
                factory.ProcessVisitor(visitor, new PicFilter());
                Box2D boxEntities = visitor.Box;

                // compute second entity position
                PicBlockRef blockRef10 = factory.AddBlockRef(block
                    , new Vector2D(
                    boxEntities.XMin + boxEntities.XMax + (ortho ? 0.0 : 1.0) * impositionOffset.X
                    , boxEntities.YMin + boxEntities.YMax + 5.0 * boxEntities.Height + minDistance.Y + (ortho ? 1.0 : 0.0) * impositionOffset.Y
                    ), ortho ? 270.0 : 180.0);
                double verticalDistance = 0.0;
                if (!PicBlockRef.Distance(blockRef00, blockRef10, PicBlockRef.DistDirection.VERTICAL_TOP, out verticalDistance))
                    throw new Exception("Failed to compute distance between to block refs");
                Vector2D vecPosition = new Vector2D(
                    boxEntities.XMin + boxEntities.XMax + (ortho ? 0.0 : 1.0) * impositionOffset.X
                    , boxEntities.YMin + boxEntities.YMax + 5.0 * boxEntities.Height + 2.0 * minDistance.Y - verticalDistance + (ortho ? 1.0 : 0.0) * impositionOffset.Y);
                blockRef10.Position = vecPosition;

                // positions
                _relativePositions = new BPosition[2, 1];
                _relativePositions[0, 0] = new BPosition(Vector2D.Zero, ortho ? 90.0 : 0.0);
                _relativePositions[1, 0] = new BPosition(vecPosition, ortho ? 270.0 : 180.0);
                // bboxes
                _bboxes = new Box2D[2, 1];
                _bboxes[0, 0] = boxEntities;
                _bboxes[1, 0] = blockRef10.Box;

                // compute X step (col1 / col0)
                // col0
                List<PicBlockRef> listCol0 = new List<PicBlockRef>();
                listCol0.Add(blockRef00);
                listCol0.Add(blockRef10);

                // col1
                PicBlockRef blockRef01 = factory.AddBlockRef(block
                    , new Vector2D(5.0 * boxEntities.Width + minDistance.X, 0.0)
                    , ortho ? 90.0 : 0.0);
                PicBlockRef blockRef11 = factory.AddBlockRef(block
                    , new Vector2D(5.0 * boxEntities.Width + minDistance.X + vecPosition.X, vecPosition.Y)
                    , ortho ? 270.0 : 180.0);
                List<PicBlockRef> listCol1 = new List<PicBlockRef>();
                listCol1.Add(blockRef01);
                listCol1.Add(blockRef11);
                double horizontalDistance = 0.0;
                if (!PicBlockRef.Distance(listCol0, listCol1, PicBlockRef.DistDirection.HORIZONTAL_RIGHT, out horizontalDistance))
                    throw new Exception("Failed to compute distance between to block refs");
                _patternStep.X = 5.0 * boxEntities.Width - horizontalDistance + 2.0 * minDistance.X;
                blockRef01.Position = vecPosition;
                blockRef11.Position = new Vector2D(vecPosition.X + _patternStep.X, vecPosition.Y);

                // compute Y step (row2 / row1)
                // row1
                List<PicBlockRef> listRow1 = new List<PicBlockRef>();
                listRow1.Add(blockRef10);
                listRow1.Add(blockRef11);

                PicBlockRef blockRef20 = factory.AddBlockRef(block
                    , new Vector2D(0.0, 5.0 * (boxEntities.Height + minDistance.Y))
                    , ortho ? 90.0 : 0.0);
                PicBlockRef blockRef21 = factory.AddBlockRef(block
                    , new Vector2D(_patternStep.X, 5.0 * (boxEntities.Height + minDistance.Y))
                    , ortho ? 90.0 : 0.0);

                List<PicBlockRef> listRow2 = new List<PicBlockRef>();
                listRow2.Add(blockRef20);
                listRow2.Add(blockRef21);

                verticalDistance = 0.0;
                if (!PicBlockRef.Distance(listRow1, listRow2, PicBlockRef.DistDirection.VERTICAL_TOP, out verticalDistance))
                    throw new Exception("Failed to compute distance between to block refs");
                _patternStep.Y = 5.0 * boxEntities.Height - verticalDistance + 6.0 * minDistance.Y;
            }
        }
    }
    #endregion

    #endregion // Pattern implementations

    #region ImpositionSolution
    public class ImpositionSolution : IDisposable
    {
        #region Data members
        private string _patternName;
        private bool _hasRotationInRows, _hasRotationInCols;
        private List<BPosition> _positions = new List<BPosition>();
        private IEntityContainer _container;
        private int _iRows, _iCols;
        private double _unitLengthCut, _unitLengthFold, _unitArea;
        internal Bitmap _thumbnail;
        private Box2D _box;
        private static int _thumbnailWidth = 75;
        private Vector2D _cardboardPosition;
        #endregion

        #region Constructor
        public ImpositionSolution(IEntityContainer container, string patternName, bool hasRotationInRows, bool hasRotationInCols)
        {
            _container = container;
            _patternName = patternName;
            _hasRotationInRows = hasRotationInRows;
            _hasRotationInCols = hasRotationInCols;
        }
        public void Dispose()
        {
            if (null != _thumbnail)
                _thumbnail.Dispose();
        }
        #endregion

        #region Private methods
        public void GenerateThumbnail(Vector2D dimensions)
        {
            using (PicFactory factory = new PicFactory())
            {
                CreateEntities(factory);
                // compute bounding box without format
                PicVisitorBoundingBox visitor0 = new PicVisitorBoundingBox();
                factory.ProcessVisitor(visitor0);
                _box = visitor0.Box;
                // insert format
                factory.InsertCardboardFormat(CardboardPosition, dimensions);
                // compute bounding box with format
                PicVisitorBoundingBox visitor1 = new PicVisitorBoundingBox();
                factory.ProcessVisitor(visitor1);
                // draw thumbnail
                PicGraphicsImage picImage = new PicGraphicsImage(new System.Drawing.Size(50, 50), visitor1.Box);
                factory.Draw(picImage);
                // save thumbnail
                _thumbnail = picImage.Bitmap;
            }
        }

        public Vector2D CardboardPosition
        {
            get { return _cardboardPosition; }
            set { _cardboardPosition = value; }
        }
        #endregion

        #region Public methods
        public void Add(BPosition pos)
        {
            _positions.Add(pos);
        }
        public void Translate(Vector2D vecTranslate)
        {
            List<BPosition> tempPositions = new List<BPosition>();
            foreach (BPosition position in _positions)
            {
                BPosition posTemp = position;
                posTemp._pt += vecTranslate;
                tempPositions.Add(posTemp);
            }
            _positions = tempPositions;
        }

        public void CreateEntities(PicFactory factory)
        {
            // sanity check
            if (_positions.Count == 0)
                return; // solution has no position -> exit
            // get first position
            BPosition pos0 = _positions[0];
            // block
            PicBlock block = factory.AddBlock(_container, pos0.Transformation);
            // blockrefs
            for (int i = 1; i < _positions.Count; ++i ) // do not insert first position as the block is now displayed
            {
                BPosition pos = _positions[i];
                factory.AddBlockRef(block, pos.Transformation * pos0.Transformation.Inverse());
            }
        }
        #endregion

        #region Public properties
        public bool IsValid
        {
            get { return _iRows * _iCols > 0; }
        }
        public int PositionCount
        {
            get { return _positions.Count; }
        }
        public bool HasRotationInRows
        {
            get { return _hasRotationInRows; }
        }
        public bool HasRotationInCols
        {
            get { return _hasRotationInCols; }
        }
        public int Rows
        {
            get { return _iRows;    }
            set { _iRows = value;   }
        }
        public int Cols
        {
            get { return _iCols;    }
            set { _iCols = value;   }
        }
        public double UnitLengthCut { set { _unitLengthCut = value; } }
        public double UnitLengthFold { set { _unitLengthFold = value; } }
        public double UnitArea { set { _unitArea = value; } }
        public double LengthCut { get { return _unitLengthCut * PositionCount; } }
        public double LengthFold { get {return _unitLengthFold * PositionCount; } }
        public double Area { get { return _unitArea * PositionCount; } }

        public Bitmap Thumbnail
        {
            get
            {
                return _thumbnail;
            }
        }

        public static int ThumbnailWidth
        {
            get { return _thumbnailWidth; }
            set { _thumbnailWidth = value; }
        }

        public double Width  { get { return _box.Width; } }
        public double Height { get { return _box.Height; } }
        public double BBoxArea { get { return _box.Width * _box.Height; } }
        #endregion

        #region System.Object override
        public override string ToString()
        {
            return string.Format("{0}\n{1} ({2}*{3})", _patternName, PositionCount, Rows, Cols);
        }
        #endregion
    }

    /// <summary>
    /// Implementation of <see cref="System.Collections.Generic.IComparer"/> interface used to sort the list of <see cref="ImpositionSolution"/> solutions.
    /// </summary>
    internal class SolutionComparer : IComparer<ImpositionSolution>
    {
        #region IComparer<ImpositionSolution> Members
        /// <summary>
        /// Compares two <see cref="ImpositionSolution"/> solutions.
        /// </summary>
        /// <param name="solution1">First instance of <see cref="ImpositionSolution"/>.</param>
        /// <param name="solution2">>First instance of <see cref="ImpositionSolution"/>.</param>
        /// <returns></returns>
        public int Compare(ImpositionSolution solution1, ImpositionSolution solution2)
        {
            int returnValue = 1;
            if (solution1 is ImpositionSolution && solution2 is ImpositionSolution)
            {
                // order by Position count
                if (solution1.PositionCount > solution2.PositionCount)
                    returnValue = -1;
                else if (solution1.PositionCount < solution2.PositionCount)
                    returnValue = 1;
                else
                {
                    if (solution1.BBoxArea < solution2.BBoxArea)
                        return -1;
                    else if (solution1.BBoxArea > solution2.BBoxArea)
                        return 1;
                    else
                    {
                        // rules:
                        // prefer default configuration than with rotation in rows
                        // prefer rotation in rows than rotation in cols
                        // prefer rotation in cols than rotation in rows + in cols
                        int score1 = 0, score2 = 0;
                        score1 += (solution1.HasRotationInRows ? 0 : 1);
                        score1 += (solution1.HasRotationInCols ? 0 : 2);
                        score2 += (solution2.HasRotationInRows ? 0 : 1);
                        score2 += (solution2.HasRotationInCols ? 0 : 2);
                        if (score1 > score2)
                            returnValue = -1;
                        else if (score1 < score2)
                            returnValue = 1;
                        else
                            returnValue = 0;
                    }
                }
            }
            return returnValue;
        }
        #endregion
    }
    #endregion

    #region Imposition tool
    public class ImpositionTool
    {
        #region Alignment enums
        public enum VAlignment
        {
            VALIGN_BOTTOM
            , VALIGN_TOP
            , VALIGN_CENTER
        };
        public enum HAlignment
        {
            HALIGN_LEFT
            , HALIGN_RIGHT
            , HALIGN_CENTER
        };
        #endregion

        #region Data members
        IEntityContainer _initialEntities;
        private Vector2D _margin = Vector2D.Zero;
        private Vector2D _minMargin = Vector2D.Zero;
        private Vector2D _spaceBetween = Vector2D.Zero;
        private Vector2D _impositionOffset = Vector2D.Zero;
        private bool _allowRotationInColumns=true, _allowRotationInRows=true, _allowOrthogonalImposition=true;
        private double _unitLengthCut, _unitLengthFold, _unitArea;
        private CardboardFormat _cardboardFormat;
        private VAlignment _vAlignment = VAlignment.VALIGN_CENTER;
        private HAlignment _hAlignment = HAlignment.HALIGN_CENTER;
        #endregion

        #region Constructor
        public ImpositionTool(IEntityContainer container)
        {
            _initialEntities = container;

            using (PicFactory factory = new PicFactory(container))
            { 
                // compute lengthes
                PicVisitorDieCutLength visitor = new PicVisitorDieCutLength();
                factory.ProcessVisitor(visitor);
                Dictionary<PicGraphics.LT, double> lengthes = visitor.Lengths;
                _unitLengthCut = lengthes.ContainsKey(PicGraphics.LT.LT_CUT) ? visitor.Lengths[PicGraphics.LT.LT_CUT] : 0.0;
                _unitLengthFold = lengthes.ContainsKey(PicGraphics.LT.LT_CREASING) ? visitor.Lengths[PicGraphics.LT.LT_CREASING] : 0.0;
                // compute area
                try
                {
                    PicToolArea picToolArea = new PicToolArea();
                    factory.ProcessTool(picToolArea);
                    _unitArea = picToolArea.Area;
                }
                catch (PicToolTooLongException /*ex*/)
                {
                    _unitArea = 0;
                }
            }
        }
        #endregion

        #region Private method to get list of patterns
        private List<ImpositionPattern> GetPatternList()
        {
            List<ImpositionPattern> list = new List<ImpositionPattern>();
            ImpositionPattern[] allPaterns = {
                new PatternDefault()
                , new PatternRotationInRow()
                , new PatternRotationInColumn()
            };
            foreach (ImpositionPattern pattern in allPaterns)
            {
                if (pattern.RequiresRotationInRows && !_allowRotationInRows)
                    continue;
                if (pattern.RequiresRotationInColumns && !_allowRotationInColumns)
                    continue;
                list.Add(pattern);
            }
            return list;
        }
        #endregion

        #region Public methods
        public bool GenerateSortedSolutionList(IProgressCallback callback, out List<ImpositionSolution> solutions)
        {
            // get applicable pattern list
            List<ImpositionPattern> patternList = GetPatternList();
            // need to compute orthogonal positions ?
            bool processOrthogonalImposition = _allowOrthogonalImposition && (_cardboardFormat.Width != _cardboardFormat.Height);
            // compute number of expected solutions
            if (null != callback)
                callback.Begin(0, patternList.Count * (processOrthogonalImposition ? 4 : 2));
            // instantiate solution list
            solutions = new List<ImpositionSolution>();
            // process pattern list
            foreach (ImpositionPattern pattern in patternList)
            {
                // default orientation
                ImpositionSolution solution = pattern.GenerateSolution(this
                    , _initialEntities, _cardboardFormat
                    , _margin, _minMargin, _spaceBetween
                    , _impositionOffset, false);
                if (null != solution && solution.IsValid)
                {
                    solution.UnitLengthCut = _unitLengthCut;
                    solution.UnitLengthFold = _unitLengthFold;
                    solution.UnitArea = _unitArea;
                    solutions.Add(solution);
                }
                // increment progress dialog?
                if (null != callback)
                    callback.Increment(solutions.Count);

                // orthogonal direction
                if (processOrthogonalImposition)
                {
                    solution = pattern.GenerateSolution(this
                        , _initialEntities, _cardboardFormat
                        , _margin, _minMargin, _spaceBetween
                        , _impositionOffset, true);
                    if (null != solution && solution.IsValid)
                    {
                        solution.UnitLengthCut = _unitLengthCut;
                        solution.UnitLengthFold = _unitLengthFold;
                        solution.UnitArea = _unitArea;
                        solutions.Add(solution);
                    }
                }
                // increment progress dialog?
                if (null != callback)
                    callback.Increment(solutions.Count);
            }

            // generate thumbnails
            foreach (ImpositionSolution sol in solutions)
            {
                sol.GenerateThumbnail(_cardboardFormat.Dimensions);
                // increment progress dialog?
                if (null != callback)
                    callback.Increment(solutions.Count);
            }

            // sort solution list
            solutions.Sort(new SolutionComparer());

            if (null != callback)
                callback.End();

            return solutions.Count > 0;
        }
        #endregion

        #region Public properties
        public IEntityContainer InitialEntities
        {
            get { return _initialEntities; }
        }
        public HAlignment HorizontalAlignment
        {
            get { return _hAlignment; }
            set { _hAlignment = value; }
        }
        public VAlignment VerticalAlignment
        {
            get { return _vAlignment; }
            set { _vAlignment = value; }
        }
        public Vector2D Margin
        {
            get { return _margin; }
            set { _margin = value; }
        }
        public Vector2D MinMargin
        {
            get { return _minMargin; }
            set { _minMargin = value; }            
        }
        public Vector2D SpaceBetween
        {
            get { return _spaceBetween; }
            set { _spaceBetween = value; }
        }
        public bool AllowRotationInRowDirection
        {
            get { return _allowRotationInRows; }
            set { _allowRotationInRows = value; }
        }
        public bool AllowRotationInColumnDirection
        {
            get { return _allowRotationInColumns; }
            set { _allowRotationInColumns = value; }
        }
        public Vector2D ImpositionOffset
        {
            get { return _impositionOffset; }
            set { _impositionOffset = value; }
        }
        public CardboardFormat CardboardFormat
        {
            set { _cardboardFormat = value; }
            get { return _cardboardFormat; }
        }

        public Box2D UsableFormat
        {
            get
            {
                Box2D box = new Box2D();
                switch (_hAlignment)
                {
                    case ImpositionTool.HAlignment.HALIGN_LEFT:
                        box.XMin = _margin.X;
                        box.XMax = _cardboardFormat.Width - _minMargin.X;
                        break;
                    case ImpositionTool.HAlignment.HALIGN_RIGHT:
                        box.XMin = _minMargin.X;
                        box.XMax = _cardboardFormat.Width - _margin.X;
                        break;
                    case ImpositionTool.HAlignment.HALIGN_CENTER:
                        box.XMin = _margin.X;
                        box.XMax = _cardboardFormat.Width - _margin.X;
                        break;
                    default:
                        break;
                }

                switch (_vAlignment)
                {
                    case ImpositionTool.VAlignment.VALIGN_BOTTOM:
                        box.YMin = _margin.Y;
                        box.YMax = _cardboardFormat.Height - _minMargin.Y;
                        break;
                    case ImpositionTool.VAlignment.VALIGN_TOP:
                        box.YMin = _minMargin.Y;
                        box.YMax = _cardboardFormat.Height - _margin.Y;
                        break;
                    case ImpositionTool.VAlignment.VALIGN_CENTER:
                        box.YMin = _margin.Y;
                        box.YMax = _cardboardFormat.Height - _margin.Y;
                        break;
                    default:
                        break;
                }
                return box;
            }
        }
        #endregion
    }
    #endregion

    #region Cardboard format and loader
    public class CardboardFormat
    {
        #region Data members
        private int _id;
        private string _name, _description;
        private double _width, _height;
        #endregion
        #region Constructor
        public CardboardFormat(int id, string name, string description, double width, double height)
        {
            _id = id;
            _name = name;
            _description = description;
            _width = width;
            _height = height;
        }
        #endregion
        #region Public properties
        public Vector2D Dimensions { get { return new Vector2D(_width, _height); } }
        public double Width { get { return _width; } }
        public double Height { get { return _height; } }
        public double Area { get { return _width * _height; } }
        #endregion
        #region Object overrides
        public override string ToString()
        {
            return string.Format("{0} ({1} * {2})", _name, _width, _height);
        }
        #endregion
    }

    public abstract class CardboardFormatLoader
    {
        #region Abstract methods
        public abstract CardboardFormat[] LoadCardboardFormats();
        public abstract void EditCardboardFormats();
        #endregion
    }

    public class CardboardFormatLoaderDefault : CardboardFormatLoader
    {
        #region Constructor
        public CardboardFormatLoaderDefault()
        {
        }
        #endregion

        #region CardboardFormatLoader implementation
        public override CardboardFormat[] LoadCardboardFormats()
        {
            List<CardboardFormat> listCardboardFormat = new List<CardboardFormat>();
            int id = 0;
            for (int i = 0; i < 5; ++i)
                for (int j = i; j < 5; ++j)
                {
                    string name = string.Format("F_{0}_{1}", i+1, j+1);
                    listCardboardFormat.Add(new CardboardFormat(id, name, name, (i+1) * 1000.0, (j+1) * 1000.0));
                    ++id;
                }
            return listCardboardFormat.ToArray();
        }

        public override void EditCardboardFormats()
        {
            throw new Exception("This method is not implemented in CardboardFormatLoaderDefault");
        }
        #endregion
    }
    #endregion
}
