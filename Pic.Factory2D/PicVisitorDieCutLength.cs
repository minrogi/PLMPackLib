#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Pic.Factory2D
{
    public class PicVisitorDieCutLength : PicFactoryVisitor, IDisposable
    {
        #region Private fields
        Dictionary<PicGraphics.LT, double> _dictionnaryLength = new Dictionary<PicGraphics.LT, double>();
        #endregion

        #region Public constructor
        public PicVisitorDieCutLength()
        {
        }
        #endregion

		#region PicFactoryVisitor overrides
		public override void Initialize(PicFactory factory)
		{
            _dictionnaryLength.Clear();
		}
		public override void ProcessEntity(PicEntity entity)
		{
            if (entity.Code != PicEntity.eCode.PE_BLOCKREF)
            {
                PicTypedDrawable drawable = entity as PicTypedDrawable;
                if (null == drawable)
                    return;
                double totalLength = drawable.Length;
                if (_dictionnaryLength.ContainsKey(drawable.LineType))
                    totalLength += _dictionnaryLength[drawable.LineType];
                _dictionnaryLength[drawable.LineType] = totalLength;
            }
            else
            {
                PicBlockRef blockRef = entity as PicBlockRef;
                PicBlock block = blockRef.Block;

                foreach (PicEntity blockEntity in block)
                {
                    PicTypedDrawable innerDrawable = blockEntity as PicTypedDrawable;
                    if (null == innerDrawable)
                        continue;

                    double totalLength = innerDrawable.Length;
                    if (_dictionnaryLength.ContainsKey(innerDrawable.LineType))
                        totalLength += _dictionnaryLength[innerDrawable.LineType];
                    _dictionnaryLength[innerDrawable.LineType] = totalLength;
                }
            }
		}
		public override void Finish()
		{
		}
        public void Dispose()
        {
            _dictionnaryLength.Clear();
        }
		#endregion

        #region Public properties
        public Dictionary<PicGraphics.LT, double> Lengths
        { 
            get
            {
                return _dictionnaryLength;
            }
        }
        #endregion
    }
}
