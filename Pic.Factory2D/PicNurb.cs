#region Using directives
using System;
using System.Collections.Generic;
using System.Text;

using Sharp3D.Math.Core;
using Sharp3D.Math.Geometry2D;
#endregion

namespace Pic.Factory2D
{
    public class PicNurb : PicTypedDrawable 
    {
        #region Private fields
        #endregion

        #region Protected Constructors
        protected PicNurb(uint id, PicGraphics.LT lType)
            :base(id, lType)
        {
        }
        #endregion

        #region Pic.Factory.PicEntity Overrides
        protected override PicEntity.eCode  GetCode()
        {
            return PicEntity.eCode.PE_NURBS;
        }
        #endregion

        #region Public Methods
        public static PicNurb CreateNewNurb(uint id, PicGraphics.LT lType)
        {
            PicNurb nurb = new PicNurb(id, lType);
            return nurb;
        }
		#endregion

		#region PicTypedDrawable overrides
		protected override void DrawSpecific(PicGraphics graphics)
		{
		}
        protected override void DrawSpecific(PicGraphics graphics, Transform2D transform)
        {
        }
		protected override bool Evaluate()
		{
			return true;
		}
        public override void Transform(Sharp3D.Math.Core.Transform2D transform)
        {
        }
        public override PicEntity Clone(IEntityContainer factory)
        {
            PicNurb nurb = new PicNurb(factory.GetNewEntityId(), LineType);
            return nurb;
        }
        public override Box2D ComputeBox(Transform2D transform)
        {
            Box2D box = Box2D.Initial;
            return box;
        }
        public override double Length
        {
            get { return 0.0; }
        }
        public override Segment[] Segments
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

        #region Public properties
        #endregion

        #region System.Object Overrides
        public override string ToString()
        {
            return string.Format("Nurb id = {0}\n", this.Id);
        }
        #endregion
    }
}
