using System;
using System.Collections.Generic;
using System.Text;

namespace Pic
{
    namespace Factory2D
    {
        public abstract class PicFactoryVisitor
        {
			public abstract void Initialize(PicFactory factory);
			public abstract void ProcessEntity(PicEntity entity);
			public abstract void Finish();
        }
    }
}
