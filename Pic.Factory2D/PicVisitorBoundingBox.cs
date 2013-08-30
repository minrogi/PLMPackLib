#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using log4net;
#endregion

namespace Pic.Factory2D
{
	public class PicVisitorBoundingBox : PicFactoryVisitor, IDisposable
	{
		#region Private fields
		private Box2D _box = new Box2D();
        protected static readonly ILog _log = LogManager.GetLogger(typeof(PicVisitorBoundingBox));
        protected bool _takePicBlocsIntoAccount = true;
		#endregion

		#region Public constructor
		public PicVisitorBoundingBox()
		{
		}
		#endregion

        #region Public properties
        public bool TakePicBlocksIntoAccount
        {
            get { return _takePicBlocsIntoAccount; }
            set { _takePicBlocsIntoAccount = value; }
        }
        #endregion

        #region PicFactoryVisitor overrides
        public override void Initialize(PicFactory factory)
		{
			_box.Reset();
            try
            {
                if (null != factory.Format)
                    _box.Extend(factory.Format.Box);
                else if (factory.IsEmpty)
                    _box.Extend(Sharp3D.Math.Core.Vector2D.Zero);
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
		}
		public override void ProcessEntity(PicEntity entity)
		{
			PicDrawable drawable = entity as PicDrawable;
            // non drawable not taken into account
            if (null == drawable)
                return;
            if (drawable is PicBlockRef)
            { 
                PicBlockRef blockRef = entity as PicBlockRef;
                _box.Extend(blockRef.Box);
            }
            else if ((drawable is PicBlock) )
            {
                if (_takePicBlocsIntoAccount)
                {
                    PicBlock block = entity as PicBlock;
                    _box.Extend(block.Box);
                }
            }
            else
            {
                PicTypedDrawable typedDrawable = drawable as PicTypedDrawable;
                if (null != typedDrawable && typedDrawable.LineType != PicGraphics.LT.LT_CONSTRUCTION)
                    _box.Extend(drawable.Box);
            }
		}
		public override void Finish()
		{
		}
        public void Dispose()
        {
        }
		#endregion

        #region Public properties
        public Box2D Box
		{
			get { return _box; }
		}
		#endregion
	}
}
