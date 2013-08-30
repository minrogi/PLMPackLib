#region Using directives
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Sharp3D.Math.Core;

using log4net;
#endregion

namespace Pic
{
    namespace Factory2D
    {
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class PicFactory : IDisposable, IEntityContainer, IEnumerable<PicEntity>
        {
            #region Private fields
            protected static readonly ILog _log = LogManager.GetLogger(typeof(PicFactory));
            private uint _id;
            private List<PicEntity> _entities = new List<PicEntity>();
            private PicCardboardFormat _cardboardFormat;
            #endregion

            #region Constructors
            public PicFactory()
            {
                _id = 0;
            }

            public PicFactory(IEntityContainer container)
            {
                AddEntities(container, Transform2D.Identity);
            }

            public void Dispose()
            {
                _entities.Clear();
            }
            #endregion

            #region Entity creation methods
            /// <summary>
            /// Add new point
            /// </summary>
            /// <param name="pt"></param>
            public PicPoint AddPoint(PicGraphics.LT lType, Vector2D pt)
            {
                PicPoint point = PicPoint.CreateNewPoint(GetNewEntityId(), lType, pt);
                point.Group = 1;
                point.Layer = 1;
                _entities.Add(point);
                return point;
            }
            /// <summary>
            /// Add new segment
            /// </summary>
            /// <param name="lType">Line type</param>
            /// <param name="pt0">First extremity of segment</param>
            /// <param name="pt1">Second extremity of segment</param>
            /// <returns>Segment entity</returns>
            public PicSegment AddSegment(PicGraphics.LT lType, Vector2D pt0, Vector2D pt1)
            {
                PicSegment seg = PicSegment.CreateNewSegment(GetNewEntityId(), lType, pt0, pt1);
                seg.Group = 1;
                seg.Layer = 1;
                _entities.Add(seg);
                return seg;
            }
            /// <summary>
            /// Add new segment
            /// </summary>
            /// <param name="lType">Line type</param>
            /// <param name="grp">Group</param>
            /// <param name="layer">Layer</param>
            /// <param name="x0">First extremity abscissa</param>
            /// <param name="y0">First extremity ordinate</param>
            /// <param name="x1">Second extremity abscissa</param>
            /// <param name="y1">Second extremity ordinate</param>
            /// <returns>Segment entity</returns>
            public PicSegment AddSegment(PicGraphics.LT lType, short grp, short layer, double x0, double y0, double x1, double y1)
            {
                PicSegment seg = PicSegment.CreateNewSegment(GetNewEntityId(), lType, new Vector2D(x0, y0), new Vector2D(x1, y1));
                seg.Group = grp;
                seg.Layer = layer;
                _entities.Add(seg);
                return seg;
            }
            /// <summary>
            /// Add a new arc with center, radius, start angle and end angle
            /// </summary>
            /// <param name="lType">Line type</param>
            /// <param name="ptCenter">Center</param>
            /// <param name="radius">Radius of arc</param>
            /// <param name="angle0">Start angle</param>
            /// <param name="angle1">End angle</param>
            public PicArc AddArc(PicGraphics.LT lType, Vector2D ptCenter, double radius, double angle0, double angle1)
            {
                PicArc arc = PicArc.CreateNewArc(GetNewEntityId(), lType, ptCenter, radius, angle0, angle1);
                arc.Group = 1;
                arc.Layer = 1;
                _entities.Add(arc);
                return arc;
            }
            /// <summary>
            /// Add a new arc with center, radius, start angle and end angle
            /// </summary>
            /// <param name="lType">Line type</param>
            /// <param name="grp">Group</param>
            /// <param name="layer">Layer</param>
            /// <param name="ptCenter">Center</param>
            /// <param name="radius">Radius of arc</param>
            /// <param name="angle0">Start angle</param>
            /// <param name="angle1">End angle</param>
            public PicArc AddArc(PicGraphics.LT lType, short grp, short layer, Vector2D ptCenter, double radius, double angle0, double angle1)
            {
                PicArc arc = PicArc.CreateNewArc(GetNewEntityId(), lType, ptCenter, radius, angle0, angle1);
                arc.Group = grp;
                arc.Layer = layer;
                _entities.Add(arc);
                return arc;
            }
            /// <summary>
            /// Add a new arc with center, radius, start angle and end angle
            /// </summary>
            /// <param name="lType">Line type</param>
            /// <param name="grp">Group</param>
            /// <param name="layer">Layer</param>
            /// <param name="xc">abscissa of center</param>
            /// <param name="yc">ordinate of center</param>
            /// <param name="radius">Radius of arc</param>
            /// <param name="angle0">Start angle</param>
            /// <param name="angle1">End angle</param>
            public PicArc AddArc(PicGraphics.LT lType, short grp, short layer, double xc, double yc, double radius, double angle0, double angle1)
            {
                PicArc arc = PicArc.CreateNewArc(GetNewEntityId(), lType, new Vector2D(xc, yc), radius, angle0, angle1);
                arc.Group = grp;
                arc.Layer = layer;
                _entities.Add(arc);
                return arc;
            }
            /// <summary>
            /// Add a new arc with center, first point and second point
            /// </summary>
            /// <param name="lType">Line type</param>
            /// <param name="ptCenter">Center</param>
            /// <param name="radius">Radius of arc</param>
            /// <param name="pt0">Start point</param>
            /// <param name="pt1">End point</param>
            public PicArc AddArc(PicGraphics.LT lType, Vector2D ptCenter, Vector2D pt0, Vector2D pt1)
            {
                PicArc arc = PicArc.CreateNewArc(GetNewEntityId(), lType, ptCenter, pt0, pt1);
                arc.Group = 1;
                arc.Layer = 1;
                _entities.Add(arc);
                return arc;
            }
            /// <summary>
            /// Create a new nurb entity
            /// </summary>
            /// <param name="lType">Line type</param>
            public PicNurb AddNurb(PicGraphics.LT lType)
            {
                PicNurb nurb = PicNurb.CreateNewNurb(GetNewEntityId(), lType);
                nurb.LineType = lType;
                nurb.Group = 1;
                nurb.Layer = 1;
                _entities.Add(nurb);
                return nurb;
            }
            /// <summary>
            /// Create new horizontal cotation
            /// </summary>
            /// <param name="grp">Group</param>
            /// <param name="layer">Layer</param>
            /// <param name="pt0">First extremity</param>
            /// <param name="pt1">Second extremity</param>
            /// <param name="offset">Offset</param>
            /// <returns></returns>
            public PicCotation AddCotation(PicCotation.CotType cotationType, short grp, short layer, Vector2D pt0, Vector2D pt1, double offset, string text)
            {
                PicCotation cotation;
                switch (cotationType)
                {
                    case PicCotation.CotType.COT_DISTANCE:
                        cotation = PicCotationDistance.CreateNewCotation(GetNewEntityId(), pt0, pt1, offset);
                        break;
                    case PicCotation.CotType.COT_HORIZONTAL:
                        cotation = PicCotationHorizontal.CreateNewCotation(GetNewEntityId(), pt0, pt1, offset);
                        break;
                    case PicCotation.CotType.COT_VERTICAL:
                        cotation = PicCotationVertical.CreateNewCotation(GetNewEntityId(), pt0, pt1, offset);
                        break;
                    default:
                        throw new Exception("Invalid cotation type");
                }
                cotation.Group = grp;
                cotation.Layer = layer;
                cotation.Text = text;
                _entities.Add(cotation);
                return cotation;
            }
            public PicCotation AddCotation(PicCotation.CotType cotationType, short grp, short layer, double x0, double y0, double x1, double y1, double offset, string text)
            {
                return AddCotation(cotationType, grp, layer, new Vector2D(x0, y0), new Vector2D(x1, y1), offset, text);
            }
            /// <summary>
            /// Create a new block from an external container
            /// </summary>
            /// <param name="container">An object of a class that implements the <see cref="IEntityContainerInterface"/>. Either a <see cref="PicFactory"/> or a <see cref="PicBlock"/> instance.</param>
            /// <param name="transf">Transformation applied to all drawable entities</param>
            /// <returns></returns>
            public PicBlock AddBlock(IEntityContainer container, Transform2D transf)
            {
                PicBlock block = PicBlock.CreateNewBlock(GetNewEntityId(), container, transf);
                _entities.Add(block);
                return block;
            }
            public PicBlock AddBlock(IEntityContainer container)
            {
                return AddBlock(container, Transform2D.Identity);
            }
            /// <summary>
            /// Create a new block reference
            /// </summary>
            /// <param name="block">An instance of the <see cref="PicBlock2D"/> class</param>
            /// <param name="pt">A <see cref="Vector2D"/> point to be used as translation from the origin of block coordinates</param>
            /// <param name="angle">A double angle value</param>
            /// <returns>A <see cref="PicBlockRef"/> entity</returns>
            public PicBlockRef AddBlockRef(PicBlock block, Vector2D pt, double angle)
            {
                PicBlockRef blockRef = PicBlockRef.CreateNewBlockRef(GetNewEntityId(), block, pt, angle);
                _entities.Add(blockRef);
                return blockRef;
            }

            public PicBlockRef AddBlockRef(PicBlock block, Transform2D transf)
            {
                PicBlockRef blockRef = PicBlockRef.CreateNewBlockRef(GetNewEntityId(), block, transf);
                _entities.Add(blockRef);
                return blockRef;
            }
            #endregion

            #region Cardboard format
            public void InsertCardboardFormat(Vector2D position, Vector2D dimensions)
            {
                _cardboardFormat = new PicCardboardFormat(0, position, dimensions);
            }

            public void HideCardboardFormat()
            {
                _cardboardFormat = null;
            }

            public bool HasCardboardFormat
            {
                get { return _cardboardFormat != null; }
            }

            public PicCardboardFormat Format
            {
                get { return _cardboardFormat; }
            }
            #endregion

            #region Entities processing for drawing/visiting
            /// <summary>
            /// Drawing method used to draw factory content
            /// </summary>
            /// <param name="graphics">Graphics environment parameters</param>
            public void Draw(PicGraphics graphics)
            {
                Draw(graphics, new PicFilter());
            }

            /// <summary>
            /// Drawing method used to draw factory content
            /// </summary>
            /// <param name="graphics">Graphic environment parameters</param>
            public void Draw(PicGraphics graphics, PicFilter filter)
			{
				graphics.Initialize();

                // bounding box
                if (!graphics.DrawingBox.IsValid)
                {
                    // compute bounding box
                    using (PicVisitorBoundingBox visitor = new PicVisitorBoundingBox())
                    {
                        ProcessVisitor(visitor);
                        Box2D box = visitor.Box;
                        box.AddMarginRatio(0.01);
                        // set as drawing box
                        graphics.DrawingBox = box;
                    }
                }

                foreach (PicEntity entity in _entities)
                {
                    try
                    {
                        // cotation
                        PicCotation cotation = entity as PicCotation;
                        if (cotation != null && !cotation.Deleted && filter.Accept(cotation))
                            cotation.Draw(graphics);
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex.Message);
                    }                
                }

				foreach (Object o in _entities)
				{
					try
					{
                        // drawable entities
						PicDrawable drawable = o as PicDrawable;
                        if (drawable != null && !drawable.Deleted && !(drawable is PicCotation) && filter.Accept(drawable))
							drawable.Draw(graphics);
					}
					catch (Exception ex)
					{
                        _log.Error(ex.Message);
					}
				}
                // cardboard format
                if (null != _cardboardFormat)
                    _cardboardFormat.Draw(graphics);

				graphics.Finish();
			}

            public void ProcessVisitor(PicFactoryVisitor visitor)
            { 
                ProcessVisitor(visitor, new PicFilter());
            }

			public void ProcessVisitor(PicFactoryVisitor visitor, PicFilter filter)
			{
				visitor.Initialize(this);

                foreach (PicEntity entity in _entities)
				{
					try
					{
						if (entity != null && !entity.Deleted && filter.Accept(entity))
							visitor.ProcessEntity(entity);
					}
					catch (Exception ex)
					{
                        _log.Error(ex.Message);
					}
				}

				visitor.Finish();
			}

            public void ProcessTool(PicTool tool)
            {
                try
                {
                    // TODO : might need to implement Undo/Redo feature in this function
                    // process tool
                    tool.ProcessFactory(this);
                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message);
                }
            }

            public void ProcessToolAllEntities(PicTool tool)
            {
                foreach (PicEntity entity in _entities)
                {
                    try { tool.ProcessEntity(entity); } catch (Exception ex) { _log.Error(ex.Message); }
                }
            }
            /// <summary>
            /// Removes entities that match a specific criterion (filter)
            /// </summary>
            /// <param name="filter">Filter upon which entities are tested</param>
            public void Remove(PicFilter filter)
            {
                // build list of entities to remove
                List<PicEntity> entitiesToRemove = new List<PicEntity>();
                foreach (PicEntity entity in _entities)
                {
                    if (filter.Accept(entity))
                        entitiesToRemove.Add(entity);
                }
                // removes entities
                foreach (PicEntity entity in entitiesToRemove)
                    _entities.Remove(entity);
            }
            #endregion

            #region IEntityContainer implementation
            public PicEntity GetEntityById(uint id)
            {
                foreach (PicEntity entity in _entities)
                {
                    if (null != entity && id == entity.Id)
                        return entity;
                }
                throw new Exception( "There is no such thing as entity #" + id );
            }

            public void RemoveEntityById(uint id)
            {
                GetEntityById(id).Deleted = true;
            }

            public void Clear()
            {
                _entities.Clear();
                _id = 0;
            }

            public uint GetNewEntityId()
            {
                return ++_id;
            }

            public void AddEntities(IEntityContainer container, Transform2D transform)
            {
                foreach (PicEntity entityIn in container)
                {
                    if (null == entityIn) continue;
                    PicTypedDrawable entityOut = (entityIn.Clone(this)) as PicTypedDrawable;
                    entityOut.Transform(transform);
                    _entities.Add(entityOut);
                }
            }

            public bool IsEmpty
            {
                get { return _entities.Count == 0; }
            }
            #endregion

            #region IEnumerable implementation
            public IEnumerator<PicEntity> GetEnumerator()
            {
                return new PicEntityEnumerator(_entities);
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return new PicEntityEnumerator(_entities);
            }
            #endregion

            #region System.Object Overrides
            public override string ToString()
            {
                string factoryString = string.Empty;

                foreach (Object o in _entities)
                    factoryString += o.ToString();

                return factoryString;
            }
            #endregion
        }
    }
}
