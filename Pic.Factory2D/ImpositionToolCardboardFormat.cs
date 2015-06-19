#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sharp3D.Math.Core;
using log4net;

//using TreeDim.UserControls;
#endregion

namespace Pic.Factory2D
{
    #region ImpositionToolCardboardFormat
    public class ImpositionToolCardboardFormat : ImpositionTool
    {
        #region Data members
        private CardboardFormat _cardboardFormat;
        #endregion

        #region Constructor
        public ImpositionToolCardboardFormat(IEntityContainer container, CardboardFormat cardboardFormat)
            : base(container)
        {
            _cardboardFormat = cardboardFormat;
        }
        #endregion

        #region Public format
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

        #region Override imposition tool
        internal override int NoPatternX(ImpositionPattern pattern)
        {
            return (int)Math.Floor((UsableFormat.Width - pattern.BBox.Width) / pattern.PatternStep.X) + 1;
        }
        internal override int NoPatternY(ImpositionPattern pattern)
        {
            return (int)Math.Floor((UsableFormat.Height - pattern.BBox.Height) / pattern.PatternStep.Y) + 1;
        }
        internal override ImpositionSolution GenerateSolution(ImpositionPattern pattern)
        {
            // instantiate solution
            ImpositionSolution solution = new ImpositionSolution(InitialEntities, pattern.Name, pattern.RequiresRotationInRows, pattern.RequiresRotationInColumns);

            // pattern global box
            Box2D boxGlobal = pattern.BBox;

            // compute max number of patterns
            int noPatternX = NoPatternX(pattern);
            int noPatternY = NoPatternY(pattern);

            int[,] rowNumber = new int[pattern.NoRows, pattern.NoCols];
            int[,] colNumber = new int[pattern.NoRows, pattern.NoCols];

            int iMax = -1, jMax = -1;
            for (int i = 0; i < pattern.NoRows; ++i)
                for (int j = 0; j < pattern.NoCols; ++j)
                {
                    if (pattern._bboxes[i, j].XMax + noPatternX * pattern.PatternStep.X - boxGlobal.XMin < UsableFormat.Width)
                    {
                        rowNumber[i, j] = noPatternX + 1;
                        iMax = i;
                    }
                    else
                        rowNumber[i, j] = noPatternX;

                    if (pattern._bboxes[i, j].YMax + noPatternY * pattern.PatternStep.Y - boxGlobal.YMin < UsableFormat.Height)
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
            for (int i = 0; i < pattern.NoRows; ++i)
                for (int j = 0; j < pattern.NoCols; ++j)
                {
                    xMax = Math.Max(xMax, pattern._bboxes[i, j].XMax + (rowNumber[i, j] - 1) * pattern.PatternStep.X);
                    yMax = Math.Max(yMax, pattern._bboxes[i, j].YMax + (colNumber[i, j] - 1) * pattern.PatternStep.Y);
                }

            double xMargin = 0.0;
            switch (HorizontalAlignment)
            {
                case ImpositionTool.HAlignment.HALIGN_LEFT: xMargin = UsableFormat.XMin; break;
                case ImpositionTool.HAlignment.HALIGN_RIGHT: xMargin = CardboardFormat.Width - Margin.X - xMax + boxGlobal.XMin; break;
                case ImpositionTool.HAlignment.HALIGN_CENTER: xMargin = (CardboardFormat.Width - xMax + boxGlobal.XMin) * 0.5; break;
                default: break;
            }

            double yMargin = 0.0;
            switch (VerticalAlignment)
            {
                case ImpositionTool.VAlignment.VALIGN_BOTTOM: yMargin = UsableFormat.YMin; break;
                case ImpositionTool.VAlignment.VALIGN_TOP: yMargin = CardboardFormat.Height - Margin.Y - yMax + boxGlobal.YMin; break;
                case ImpositionTool.VAlignment.VALIGN_CENTER: yMargin = (CardboardFormat.Height - yMax + boxGlobal.YMin) * 0.5; break;
                default: break;
            }

            // compute offsets
            double xOffset = xMargin - boxGlobal.XMin;
            double yOffset = yMargin - boxGlobal.YMin;

            for (int i = 0; i < pattern.NoRows; ++i)
                for (int j = 0; j < pattern.NoCols; ++j)
                {
                    for (int k = 0; k < rowNumber[i, j]; ++k)
                        for (int l = 0; l < colNumber[i, j]; ++l)
                        {
                            BPosition pos = pattern._relativePositions[i, j];
                            pos._pt.X = xOffset + k * pattern.PatternStep.X + pos._pt.X;
                            pos._pt.Y = yOffset + l * pattern.PatternStep.Y + pos._pt.Y;
                            solution.Add(pos);
                        }
                }
            // noRows / noCols
            solution.Rows = pattern.NoCols * noPatternX + iMax + 1;
            solution.Cols = pattern.NoRows * noPatternY + jMax + 1;
            // cardboard position
            solution.CardboardPosition = new Vector2D(xMargin, yMargin);
            solution.CardboardDimensions = _cardboardFormat.Dimensions;
            return solution;
        }

        internal override bool AllowOrthogonalImposition
        {
            get
            {
                return _allowOrthogonalImposition && (_cardboardFormat.Width != _cardboardFormat.Height);
            }
        }
        #endregion
    }
    #endregion
}
