#region Using directives
using System;
using System.Collections.Generic;
using System.Text;

using Sharp3D.Math;
using Sharp3D.Math.Geometry2D;

using log4net;
#endregion

namespace Pic.Factory2D
{
    public class AreaData
    {
        public double _xMin = double.MaxValue, _xMax = double.MinValue, _yMin = double.MaxValue, _yMax = double.MinValue;
    }

    public class PicToolArea : PicTool
    {
        #region Override PicTool
        public override void ProcessFactory(PicFactory factory)
        {
            // initialization
            _segList.Clear();
            _box = new Box2D();
            _area = 0.0;

            // build list of segments with entities that belongs to group
            factory.ProcessToolAllEntities(this);
            _box.AddMarginRatio(0.2);

            // exit if no of segments exceeds MAXSEG
            int maxSeg = Pic.Factory2D.Properties.Settings.Default.AreaMaxNoSegments;
            if (_segList.Count > maxSeg)
                throw new PicToolTooLongException(string.Format("No of segments exceeds {0}", maxSeg));

            // compute actual step size
            uint iNoStep = (uint)(_box.Width / _stepSize);
            iNoStep = Math.Min(iNoStep, _iNoStepMax);
            double stepDefault = _box.Width / iNoStep;

            // compute area
            for (int i = 0; i < iNoStep; ++i)
            {
                double xh = _box.XMin + i * stepDefault;
                int nby = 1;
                double stepCurrent = stepDefault;
                List<double> tabyh = new List<double>();

                tabyh.Add(double.MaxValue);

                bool bAgain = false;
                do
                {
                    bAgain = false;
                    stepCurrent = stepDefault;

                    foreach (Segment segTemp in _segList)
                    {
                        Segment seg;
                        if (segTemp.P0.X <= segTemp.P1.X)
                            seg = segTemp;
                        else
                            seg = new Segment(segTemp.P1, segTemp.P0);

                        if (xh >= seg.P0.X && xh <= seg.P1.X)
                        {
                            // cas intersections confondues
                            if ((Math.Abs(xh - seg.P0.X) < 0.001) || (Math.Abs(seg.P1.X - xh) < 0.001))
                            {
                                // restart loop from the begining
                                nby = 1;
                                tabyh[0] = double.MaxValue;
                                stepCurrent = 0.9 * stepDefault;
                                xh -= stepDefault * 0.1;
                                bAgain = true;
                                break;
                            }
                            else
                            {
                                double yh = seg.P0.Y + (xh - seg.P0.X) * ((seg.P1.Y - seg.P0.Y) / (seg.P1.X - seg.P0.X));
                                tabyh.Add(yh);
                            }
                        }
                    }
                }
                while (bAgain);

                tabyh.Sort();
                nby = tabyh.Count;

                // increment area
                if (nby > 1 && (nby % 2 != 0))
                {
                    for (int l = 0; l < nby - 1; l += 2)
                    {
                        _area = _area + stepCurrent * (tabyh[l + 1] - tabyh[l]);
                    }
                }
            }
        }
        public override void ProcessEntity(PicEntity entity)
        {
            PicDrawable picDrawable = entity as PicDrawable;
            if (null != picDrawable)
            {
                _box.Extend(picDrawable.Box);
                _segList.AddRange(picDrawable.Segments);
            }
        }
        #endregion

        #region Public properties
        public double StepSize
        {
            get { return _stepSize; }
            set
            {
                if (value < 0.0)
                    throw new Exception("Invalid step size value");
                _stepSize = value;
            }
        }

        public double Area
        {
            get { return _area; }
        }
        #endregion

        #region Data members
        public double _stepSize = 0.1;
        public short _group = -1;
        private List<Segment> _segList = new List<Segment>();
        private Box2D _box;
        private const uint _iNoStepMax = 10000;
        private double _area = 0.0;
        protected static readonly ILog _log = LogManager.GetLogger(typeof(PicToolArea));
        #endregion
    }
}
