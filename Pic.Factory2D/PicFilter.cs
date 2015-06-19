using System;
using System.Collections.Generic;
using System.Text;

namespace Pic.Factory2D
{
    public class PicFilter
    {
        #region Constructor
        public PicFilter()
        { 
        }
        #endregion

        #region Public method
        public virtual bool Accept(PicEntity entity)
        {
            return true;
        }
        #endregion

        #region Unary operators
        public static PicFilter operator!(PicFilter filter)
        {
            return new PicFilterNot(filter);
        }
        #endregion

        #region Binary operators
        public static PicFilter operator|(PicFilter filter1, PicFilter filter2)
        {
            return new PicFilterOr(filter1, filter2);
        }
        public static PicFilter operator&(PicFilter filter1, PicFilter filter2)
        {
            return new PicFilterAnd(filter1, filter2);
        }
        #endregion

        #region Constants
        public static readonly PicFilter FilterNone = new PicFilter();
        public static readonly PicFilter FilterCotation = new PicFilterNot(
                (new PicFilterCode(PicEntity.eCode.PE_COTATIONDISTANCE))
                | (new PicFilterCode(PicEntity.eCode.PE_COTATIONHORIZONTAL))
                | (new PicFilterCode(PicEntity.eCode.PE_COTATIONVERTICAL))
                );
        public static readonly PicFilter FilterNoZeroEntities = new PicFilterOutZeroEntities();
        #endregion
    }

    /// <summary>
    /// "Not" condition filter
    /// </summary>
    public class PicFilterNot :
        PicFilter
    {
        public PicFilterNot(PicFilter filter)
        {
            _filter = filter;
        }
        public override bool Accept(PicEntity entity)
        {
            return !_filter.Accept(entity);
        }
        private PicFilter _filter;
    }

    /// <summary>
    /// "Or" condition filter
    /// </summary>
    public class PicFilterOr
        : PicFilter
    {
        public PicFilterOr(PicFilter filter1, PicFilter filter2)
        {
            _filter1 = filter1;
            _filter2 = filter2;
        }
        public override bool Accept(PicEntity entity)
        {
            return _filter1.Accept(entity) || _filter2.Accept(entity);
        }
        private PicFilter _filter1, _filter2;
    }

    /// <summary>
    /// "And" condition filter
    /// </summary>
    public class PicFilterAnd
        : PicFilter
    {
        public PicFilterAnd(PicFilter filter1, PicFilter filter2)
        {
            _filter1 = filter1;
            _filter2 = filter2;
        }
        public override bool Accept(PicEntity entity)
        {
            return _filter1.Accept(entity) && _filter2.Accept(entity);
        }
        private PicFilter _filter1, _filter2;
    }

    /// <summary>
    /// Filter entity by code
    /// </summary>
    public class PicFilterCode
        : PicFilter
    {
        public PicFilterCode(PicEntity.eCode code)
        {
            _code = code;
        }
        public override bool Accept(PicEntity entity)
        {
            if (null != entity)
                return entity.Code == _code;
            else
                return false;
        }
        private PicEntity.eCode _code;
    }

    /// <summary>
    /// Filter entity by group
    /// </summary>
    public class PicFilterGroup
        : PicFilter
    {
        public PicFilterGroup(short grp)
        {
            _grp = grp;
        }
        public override bool Accept(PicEntity entity)
        {
            PicTypedDrawable typedDrawable = entity as PicTypedDrawable;
            if (null == typedDrawable) return false;
            return typedDrawable.Group == _grp;            
        }
        private short _grp;
    }

    public class PicFilterListGroup
        : PicFilter
    {
        public PicFilterListGroup(List<short> grps)
        {
            _grps = grps;
        }
        public override bool Accept(PicEntity entity)
        {
            if (null == _grps) return true;
            PicTypedDrawable typedDrawable = entity as PicTypedDrawable;
            if (null == typedDrawable) return false;
            return _grps.Contains(typedDrawable.Group);
        }
        private List<short> _grps;
    }

    public class PicFilterListLayer
        : PicFilter
    {
        public PicFilterListLayer(List<short> layers)
        {
            _layers = layers;
        }
        public override bool Accept(PicEntity entity)
        {
            if (null == _layers) return true;
            PicTypedDrawable typedDrawable = entity as PicTypedDrawable;
            if (null == typedDrawable) return false;
            return _layers.Contains(typedDrawable.Layer);
        }
        private List<short> _layers;
    }

    /// <summary>
    /// Filter out very zero length entities
    /// </summary>
    public class PicFilterOutZeroEntities
        : PicFilter
    {
        public PicFilterOutZeroEntities()
        {        
        }
        public override bool Accept(PicEntity entity)
        {
            PicTypedDrawable typedDrawable = entity as PicTypedDrawable;
            if (null == typedDrawable) return true;
            return (typedDrawable.Length > _epsilon);
        }
        private static double _epsilon = 0.1;
    }
}
