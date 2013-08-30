#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
#endregion

namespace Pic
{
    namespace Factory2D
    {
        /// <summary>
        /// Represents 2D entity stored in PicFactory2D
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public abstract class PicEntity
        {
            #region Private fields
            private uint _id;
            private bool _modified = true;
            private bool _deleted = false;
            #endregion

            #region Enums
            public enum eCode
            {
                PE_POINT,
                PE_SEGMENT,
                PE_ARC,
                PE_ELLIPSE,
                PE_NURBS,
                PE_COTATIONDISTANCE,
                PE_COTATIONHORIZONTAL,
                PE_COTATIONVERTICAL,
                PE_COTATIONRADIUSINT,
                PE_COTATIONRADIUSEXT,
                PE_BLOCK,
                PE_BLOCKREF,
                PE_CARDBOARDFORMAT
            };
            #endregion

            #region Constructors
            protected PicEntity(uint id)
            {
                _id = id;
            }
            private PicEntity(PicEntity entity)
            {
            }
            #endregion

            #region Constants
            #endregion

            #region Protected properties
            internal bool Deleted
            {
                get { return _deleted; }
                set { _deleted = value; }
            }
            #endregion

            #region Public Static Parse Methods
            #endregion

            #region Public Abstract Method
            /// <summary>
            /// Enables to copy an entity in a new factory
            /// </summary>
            /// <returns>An entity to be saved in a new factory</returns>
            public abstract PicEntity Clone(IEntityContainer factory);

            /// <summary>
            /// Implement this method to return entity specific code
            /// </summary>
            /// <returns>A value of enum eCode</returns>
            protected abstract eCode GetCode();
            /// <summary>
            /// Use this property to access entity code
            /// </summary>
            public eCode Code
            {
                get { return GetCode(); }
            }
            #endregion

            #region Protected methods
            /// <summary>
            /// Implement this method to compute drawing points from data
            /// </summary>
            /// <returns>True if successful (entity should be drawn), false if entity can not be drawn</returns>
            protected virtual bool Evaluate()
            {
                return true;
            }
            /// <summary>
            /// Call this method each time drawing points needs to be accessed
            /// </summary>
            protected void Compute()
            {
                if (_modified)
                {
                    if (!Evaluate())
                        throw new Exception(String.Format("Failed to evaluate entity {0}", _id));
                    _modified = false;
                }
            }
            protected void SetModified()
            {
                _modified = true;
            }
            #endregion

            #region Public Methods
            #endregion

            #region System.Object Overrides
            /// <summary>
            /// Returns the hashcode for this instance.
            /// </summary>
            /// <returns>A 32-bit signed integer hash code.</returns>
            public override int GetHashCode()
            {
                return _id.GetHashCode();
            }
            /// <summary>
            /// Returns a value indicating whether this instance is equal to
            /// the specified object.
            /// </summary>
            /// <param name="obj">An object to compare to this instance.</param>
            /// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="PicEntity"/> and has the same values as this instance; otherwise, <see langword="false"/>.</returns>
            public override bool Equals(object obj)
            {
                if (obj is PicEntity)
                {
                    PicEntity e = (PicEntity)obj;
                    return (_id == e._id);
                }
                return false;
            }
            /// <summary>
            /// Returns a string representation of this object.
            /// </summary>
            /// <returns>A string representation of this object.</returns>
            public override string ToString()
            {
                return string.Format("(id = {0}\n)", _id);
            }

            /// <summary>
            /// Read only access to integer id
            /// </summary>
            public uint Id
            {
                get { return _id; }
            }
            #endregion
        }
    }
}
