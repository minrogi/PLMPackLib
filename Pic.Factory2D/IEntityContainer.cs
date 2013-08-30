using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Sharp3D.Math.Core;

namespace Pic.Factory2D
{
    public interface IEntityContainer
    {
        #region Entity numbering
        uint GetNewEntityId();
        #endregion

        #region Entity accessing / removing
        PicEntity GetEntityById(uint id);
        void RemoveEntityById(uint id);
        void Clear();
        #endregion

        #region Entity adding
        void AddEntities(IEntityContainer container, Transform2D transform);
        #endregion

        #region Implementation of the IEnumerable interface needed
        IEnumerator<PicEntity> GetEnumerator();
        #endregion
    }

    #region PicEntityEnumarator implements IEnumerator
    /// <summary>
    /// Implements interface <see cref="IEnumerator"/> and is used by classes that implement <see cref="IEntityContainer"/>
    /// </summary>
    /// <typeparam name="PicEntity"><see cref="PicEntity"/> class</typeparam>
    public class PicEntityEnumerator: IEnumerator<PicEntity>
    {
        #region Data members
        int _position = -1;
        private List<PicEntity> _entities;
        #endregion

        #region IEnumerator<PicEntity> implementation
        public PicEntityEnumerator(List<PicEntity> entities)
        {
            _entities = entities;
        }

        public PicEntity Current
        {
            get
            {
                return _entities[_position];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return _entities[_position];
            }
        }

        public bool MoveNext()
        {
            ++_position;
            return _position < _entities.Count;
        }
        public void Reset()
        {
            _position = -1;
        }
        #endregion

        #region IDisposable implementation
        public void Dispose()
        {
        }
        #endregion
    }
    #endregion
}
