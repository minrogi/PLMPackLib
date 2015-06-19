#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Sharp3D.Math.Core;
#endregion

namespace Pic
{
    namespace Factory2D
    {
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public abstract class PicGraphics : IDisposable
		{
			#region Public enums
            /// <summary>
            /// Line types
            /// see PicGeom \ DRAW2D.CPP \InitStylosPic
            /// 
            /// 
            /// 
            /// 
            ///		1 Coupant
            ///		2 Perfo. Rainant
            ///		3 Construction
            ///		4 Perfo
            ///		5 Mi-chair
            ///		6 Rainant
            ///		7 Axe
            ///		8 Cotation
            ///		9 Origine
            ///		10 Grille
            ///		11 Ponts
            ///		default Defaut
            /// </summary>
			public enum LT
			{
				LT_CUT                  // 1 (coupant)
                , LT_PERFOCREASING      // 2 (perfo-rainant)
				, LT_CONSTRUCTION       // 3 (construction)
                , LT_PERFO              // 4 (perfo)
				, LT_HALFCUT            // 5 (mi-chair)   
				, LT_CREASING           // 6 (rainant)
                , LT_AXIS               // 7 (axe)
                , LT_COTATION           // 8 (cotation)
                , LT_ORIGIN             // 9 (origine)
                , LT_GRID               // 10 (grid)
                , LT_BRIDGES            // 11 (ponts)
                , LT_DEFAULT            // (defaut)
			}
            /// <summary>
            /// Text types
            /// </summary>
            public enum TextType
            {
                FT_COTATION
            }
            /// <summary>
            /// Horizontal alignment
            /// </summary>
            public enum HAlignment
            {
                HA_RIGHT,
                HA_CENTER,
                HA_LEFT
            }
            /// <summary>
            /// Vertical alignment
            /// </summary>
            public enum VAlignment
            { 
                VA_TOP,
                VA_MIDDLE,
                VA_BOTTOM
            }
			#endregion

			#region Private fields
            protected Box2D _box;
            #endregion

            #region Protected Constructors
			protected PicGraphics()
			{
                _box = new Box2D();
			}
            #endregion

            #region Public methods
			public abstract void Initialize();
			public abstract void DrawPoint(LT lineType, Vector2D pt);
			public abstract void DrawLine(LT lineType, Vector2D ptBeg, Vector2D ptEnd);
			public abstract void DrawArc(LT lineType, Vector2D ptCenter, double radius, double angleBeg, double angleEnd);
            public abstract void DrawText(string text, TextType font, Vector2D pt, HAlignment hAlignment, VAlignment vAlignment, float fAngle);
            public abstract void GetTextSize(string text, TextType font, out double width, out double height);
			public abstract void Finish();
            public virtual void ShowMessage(string message) { }

            public abstract double DX_inv(double dX);
            public abstract double DY_inv(double dY);

            public static string LTypeToString(LT lType)
            {
                switch (lType)
                {
                    case LT.LT_CUT: return "Cut";
                    case LT.LT_CREASING: return "Creasing";
                    case LT.LT_CONSTRUCTION: return "Construction";
                    case LT.LT_AXIS: return "Axis";
                    case LT.LT_COTATION: return "Cotation";
                    case LT.LT_PERFO: return "Perfo";
                    default: return "Unknown";
                }
            }
            #endregion

            #region Implement IDisposable
            public virtual void Dispose()
            { 
            }
            #endregion

            #region Public properties
			public double Width
			{
				get { return _box.Width; }
			}
			public double Height
			{
				get { return _box.Height; }
			}

            public abstract Box2D DrawingBox { get; set; }
            #endregion

            #region System.Object overrides
			public override string ToString()
			{
				string s = string.Empty;
				s += "Box (World coordinates) = " + _box.ToString() + "\n";
				return s;
			}
            #endregion
        }
    }
}