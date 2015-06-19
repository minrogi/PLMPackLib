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
    #region ImpositionToolXY
    public class ImpositionToolXY : ImpositionTool
    {
        #region Data members
        private int _noColsExpected, _noRowsExpected;
        private Box2D _box;
        #endregion

        #region Constructor
        public ImpositionToolXY(IEntityContainer container, int noInX, int noInY)
            : base(container)
        {
            _noColsExpected = noInX;
            _noRowsExpected = noInY;
        }
        #endregion

        #region Public properties
        public int NoColsExpected { get { return _noColsExpected; } }
        public int NoRowsExpected { get { return _noRowsExpected; } }
        #endregion

        #region Override imposition tool
        internal override int NoPatternX(ImpositionPattern pattern)
        {
            return NoColsExpected / pattern.NoCols;
        }
        internal override int NoPatternY(ImpositionPattern pattern)
        {
            return NoRowsExpected / pattern.NoRows;
        }

        internal override ImpositionSolution GenerateSolution(ImpositionPattern pattern)
        {
            // instantiate solution
            ImpositionSolution solution = new ImpositionSolution(InitialEntities, pattern.Name, pattern.RequiresRotationInRows, pattern.RequiresRotationInColumns);
           // noRows / noCols
            solution.Rows = NoRowsExpected;
            solution.Cols = NoColsExpected;

            // number of each row / col of the pattern
            int[,] rowNumber = new int[pattern.NoRows, pattern.NoCols];
            int[,] colNumber = new int[pattern.NoRows, pattern.NoCols];

            int iCount = 0;
            for (int i = 0; i < pattern.NoRows; ++i)
                for (int j = 0; j < pattern.NoCols; ++j)
                {
                    colNumber[i ,j] = NoRowsExpected / pattern.NoRows + (NoRowsExpected % pattern.NoRows) /  (i+1);
                    rowNumber[i, j] = NoColsExpected / pattern.NoCols + (NoColsExpected % pattern.NoCols) / (j+1);

                    iCount += rowNumber[i, j] * colNumber[i, j];
                }
            // verify count
            System.Diagnostics.Debug.Assert(iCount == NoRowsExpected * NoColsExpected);

            // compute offsets
            Box2D boxGlobal = pattern.BBox;
            double xOffset = _margin.X - boxGlobal.XMin;
            double yOffset = _margin.Y - boxGlobal.YMin;

            _box = new Box2D();
            for (int i = 0; i < pattern.NoRows; ++i)
                for (int j = 0; j < pattern.NoCols; ++j)
                {
                    Box2D localBox = pattern._bboxes[i,j];
                    for (int k = 0; k < rowNumber[i, j]; ++k)
                        for (int l = 0; l < colNumber[i, j]; ++l)
                        {
                            // insert Box position
                            BPosition pos = pattern._relativePositions[i, j];
                            pos._pt.X = xOffset + k * pattern.PatternStep.X + pos._pt.X;
                            pos._pt.Y = yOffset + l * pattern.PatternStep.Y + pos._pt.Y;
                            solution.Add(pos);
                            // extend bounding box
                            Vector2D vOffset = new Vector2D(xOffset + k * pattern.PatternStep.X, yOffset + l * pattern.PatternStep.Y); 
                            _box.Extend(new Box2D(vOffset + localBox.PtMin, vOffset + localBox.PtMax));
                        }
                }

            // compute actual margin
            solution.CardboardPosition = new Vector2D(_margin.X, _margin.Y);
            solution.CardboardDimensions = new Vector2D(_box.XMax - _box.XMin + _margin.X + _minMargin.X, _box.YMax - _box.YMin + _margin.Y + _minMargin.Y);
            return solution;
        } 
        #endregion
    }
    #endregion
}
