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
            private Dictionary<short, string> _groups = new Dictionary<short,string>();
            private Dictionary<short, string> _layers = new Dictionary<short,string>();
            private Dictionary<string, string> _questions = new Dictionary<string, string>();
            #endregion

            #region Delegates
            public delegate void onEntityAdded(PicEntity entity);
            public delegate void onGroupAdded(short grp, string grpName);
            public delegate void onLayerAdded(short layer, string layerName);
            #endregion

            #region Events
            public event onEntityAdded EntityAdded;
            public event onGroupAdded GroupAdded;
            public event onLayerAdded LayerAdded;
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

            #region Public properties
            public Dictionary<string, string> Questionnaire { get { return _questions; } }
            #endregion

            #region Groups & Layers
            /// <summary>
            /// Get groups (key is group id)
            /// </summary>
            public Dictionary<short, string> Groups { get { return _groups; } }
            /// <summary>
            /// Get layers (key is layer id)
            /// </summary>
            public Dictionary<short, string> Layers { get { return _layers; } }
            /// <summary>
            /// create a new group with id if it does not already exists
            /// </summary>
            private void TryAddGroup(short grpId)
            {
                if (!_groups.ContainsKey(grpId))
                {
                    string grpName = string.Format("Group {0}", grpId);
                    _groups.Add(grpId, grpName);
                    if (null != GroupAdded) GroupAdded(grpId, grpName);
                }
            }
            /// <summary>
            /// create a new layer with id if it does not already exists
            /// </summary>
            /// <param name="layerId">layer id</param>
            private void TryAddLayer(short layerId)
            {
                if (!_layers.ContainsKey(layerId))
                {
                    string layerName = string.Format("Layer {0}", layerId);
                    _layers.Add(layerId, layerName);
                    if (null != LayerAdded) LayerAdded(layerId, layerName);
                }
            }
            #endregion

            #region Entity creation methods
            /// <summary>
            /// Adds any entity in the list of entities and updates groups, layers...
            /// </summary>
            /// <param name="entity">Entity being added</param>
            protected void AddEntity(PicEntity entity)
            {
                _entities.Add(entity);
                if (null != EntityAdded)
                    EntityAdded(entity);
                if (entity is PicTypedDrawable)
                {
                    PicTypedDrawable drawable = entity as PicTypedDrawable;
                    TryAddGroup(drawable.Group);
                    TryAddLayer(drawable.Layer);
                }
            }

            /// <summary>
            /// Add new point
            /// </summary>
            /// <param name="pt"></param>
            public PicPoint AddPoint(
                PicGraphics.LT lType, short grp, short layer
                , Vector2D pt)
            {
                PicPoint point = PicPoint.CreateNewPoint(GetNewEntityId(), lType, pt);
                point.Group = grp;
                point.Layer = layer;
                AddEntity(point);
                return point;
            }
            /// <summary>
            /// Add new segment
            /// </summary>
            /// <param name="lType">Line type</param>
            /// <param name="pt0">First extremity of segment</param>
            /// <param name="pt1">Second extremity of segment</param>
            /// <returns>Segment entity</returns>
            public PicSegment AddSegment(
                PicGraphics.LT lType, short grp, short layer
                , Vector2D pt0, Vector2D pt1)
            {
                PicSegment seg = PicSegment.CreateNewSegment(GetNewEntityId(), lType, pt0, pt1);
                seg.Group = grp;
                seg.Layer = layer;
                AddEntity(seg);
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
            public PicSegment AddSegment(
                PicGraphics.LT lType, short grp, short layer
                , double x0, double y0, double x1, double y1)
            {
                PicSegment seg = PicSegment.CreateNewSegment(GetNewEntityId(), lType, new Vector2D(x0, y0), new Vector2D(x1, y1));
                seg.Group = grp;
                seg.Layer = layer;
                AddEntity(seg);
                return seg;
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
            public PicArc AddArc(PicGraphics.LT lType, short grp, short layer
                , Vector2D ptCenter, double radius, double angle0, double angle1)
            {
                PicArc arc = PicArc.CreateNewArc(GetNewEntityId(), lType, ptCenter, radius, angle0, angle1);
                arc.Group = grp;
                arc.Layer = layer;
                AddEntity(arc);
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
            public PicArc AddArc(PicGraphics.LT lType, short grp, short layer
                , double xc, double yc, double radius, double angle0, double angle1)
            {
                PicArc arc = PicArc.CreateNewArc(GetNewEntityId(), lType, new Vector2D(xc, yc), radius, angle0, angle1);
                arc.Group = grp;
                arc.Layer = layer;
                AddEntity(arc);
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
            public PicArc AddArc(PicGraphics.LT lType, short grp, short layer
                , Vector2D ptCenter, Vector2D pt0, Vector2D pt1)
            {
                PicArc arc = PicArc.CreateNewArc(GetNewEntityId(), lType, ptCenter, pt0, pt1);
                arc.Group = grp;
                arc.Layer = layer;
                AddEntity(arc);
                return arc;
            }
            /// <summary>
            /// Create a new nurb entity
            /// </summary>
            /// <param name="lType">Line type</param>
            public PicNurb AddNurb(PicGraphics.LT lType, short grp, short layer)
            {
                PicNurb nurb = PicNurb.CreateNewNurb(GetNewEntityId(), lType);
                nurb.LineType = lType;
                nurb.Group = grp;
                nurb.Layer = layer;
                AddEntity(nurb);
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
            public PicCotation AddCotation(PicCotation.CotType cotationType, short grp, short layer, Vector2D pt0, Vector2D pt1, double offset, string text, short noDecimals)
            {
                PicCotation cotation;
                switch (cotationType)
                {
                    case PicCotation.CotType.COT_DISTANCE:
                        cotation = PicCotationDistance.CreateNewCotation(GetNewEntityId(), pt0, pt1, offset, noDecimals);
                        break;
                    case PicCotation.CotType.COT_HORIZONTAL:
                        cotation = PicCotationHorizontal.CreateNewCotation(GetNewEntityId(), pt0, pt1, offset, noDecimals);
                        break;
                    case PicCotation.CotType.COT_VERTICAL:
                        cotation = PicCotationVertical.CreateNewCotation(GetNewEntityId(), pt0, pt1, offset, noDecimals);
                        break;
                    default:
                        throw new Exception("Invalid cotation type");
                }
                cotation.Group = grp;
                cotation.Layer = layer;
                cotation.Text = text;
                AddEntity(cotation);
                return cotation;
            }
            public PicCotation AddCotation(PicCotation.CotType cotationType, short grp, short layer, double x0, double y0, double x1, double y1, double offset, string text, short noDecimals)
            {
                return AddCotation(cotationType, grp, layer, new Vector2D(x0, y0), new Vector2D(x1, y1), offset, text, noDecimals);
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
                AddEntity(block);
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
                AddEntity(blockRef);
                return blockRef;
            }

            public PicBlockRef AddBlockRef(PicBlock block, Transform2D transf)
            {
                PicBlockRef blockRef = PicBlockRef.CreateNewBlockRef(GetNewEntityId(), block, transf);
                AddEntity(blockRef);
                return blockRef;
            }

            public void UpdateQuestions(Dictionary<string, string> questions)
            {
                foreach (string sKey in questions.Keys)
                {
                    if (_questions.ContainsKey(sKey))
                        _questions[sKey] = questions[sKey];
                    else
                        _questions.Add(sKey, questions[sKey]);
                }
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
