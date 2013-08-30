#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Pic.Factory2D;
using Sharp3D.Math.Core;
using System.Resources;
#endregion

namespace Pic.Plugin.Arc
{
    public class Plugin : Pic.Plugin.IPlugin, Pic.Plugin.IPluginExt1
    {
        #region Constructor
        public Plugin()
        {
        }
        #endregion

        #region Declarations of all our internal plugin variables
        string myName = "Test arc";
        string myDescription = "Plugin used to test arc with various start angle and end angle";
        string myAuthor = "treeDIM";
        string myVersion = "1.0.0";
        IPluginHost myHost = null;
        #endregion

        #region Public properties
        /// <summary>
        /// Description of the Plugin's purpose
        /// </summary>
        public string Description
        {
            get { return myDescription; }
        }
        /// <summary>
        /// Author of the plugin
        /// </summary>
        public string Author
        {
            get { return myAuthor; }
        }
        /// <summary>
        /// Host of the plugin.
        /// </summary>
        public IPluginHost Host
        {
            get { return myHost; }
            set { myHost = value; }
        }

        public string Name
        {
            get { return myName; }
        }

        public string Version
        {
            get { return myVersion; }
        }

        public void Initialize()
        {
            //This is the first Function called by the host...
            //Put anything needed to start with here first
        }
        #endregion

        #region IPluginExt1 implementation
        public bool HasEmbeddedThumbnail
        {
            get { return true; }
        }

        public Bitmap Thumbnail
        {
            get
            {
                return Resource.Thumbnail;
            }
        }

        public string SourceCode
        {
            get { return Resource.SourceCode; }
        }

        public void Dispose()
        {
        }

        public double ImpositionOffsetX(ParameterStack stack) { return 0.0; }
        public double ImpositionOffsetY(ParameterStack stack) { return 0.0; }
        #endregion

        #region IPugin implementation
        public Guid Guid
        {
            get { return new Guid("{1E4D48E9-3EEC-4f30-A311-27673C930CF2}"); }
        }

        public ParameterStack Parameters
        {
            get
            {
                ParameterStack stack = new ParameterStack();
                stack.AddDoubleParameter("a0", "Start angle", 0.0, -720.0, 720.0);
                stack.AddDoubleParameter("a1", "End angle", 90.0, -720.0, 720.0);
                stack.AddBoolParameter("Arc default", "Arc default", true);
                stack.AddBoolParameter("Arc Refl X", "Arc Refl X", true);
                stack.AddBoolParameter("Arc Refl Y", "Arc Refl Y", true);
                stack.AddBoolParameter("Arc Refl XY", "Arc Ref XY", true);
                stack.AddBoolParameter("bbox", "Show bounding box", false);
                stack.AddBoolParameter("Complement", "Complement", false);
                return stack;
            }
        }

        public void CreateFactoryEntities(PicFactory factory, ParameterStack stack, Transform2D transform)
        {
            PicFactory fTemp = new PicFactory();
            // free variables
            double a0 = stack.GetDoubleParameterValue("a0");
            double a1 = stack.GetDoubleParameterValue("a1");
            bool bbox = stack.GetBoolParameterValue("bbox");
            bool arcDefault = stack.GetBoolParameterValue("Arc default");
            bool arcReflX = stack.GetBoolParameterValue("Arc Refl X");
            bool arcReflY = stack.GetBoolParameterValue("Arc Refl Y");
            bool arcReflXY = stack.GetBoolParameterValue("Arc Refl XY");
            bool arcComplement = stack.GetBoolParameterValue("Complement");

            if (arcDefault)
            {
                PicArc arc0 = fTemp.AddArc(PicGraphics.LT.LT_CUT, new Vector2D(0.0, 0.0), 100.0, a0, a1);
                arc0.Transform(Transform2D.Identity);

                if (bbox)
                {
                    Box2D box = arc0.Box;
                    fTemp.AddSegment(PicGraphics.LT.LT_CUT, 0, 0
                        , box.PtMin.X	// x0
                        , box.PtMin.Y	// y0
                        , box.PtMax.X	// x1
                        , box.PtMin.Y	// y1
                        );
                    fTemp.AddSegment(PicGraphics.LT.LT_CUT, 0, 0
                        , box.PtMax.X	// x0
                        , box.PtMin.Y	// y0
                        , box.PtMax.X	// x1
                        , box.PtMax.Y	// y1
                        );
                    fTemp.AddSegment(PicGraphics.LT.LT_CUT, 0, 0
                        , box.PtMin.X	// x0
                        , box.PtMax.Y	// y0
                        , box.PtMax.X	// x1
                        , box.PtMax.Y	// y1
                        );
                    fTemp.AddSegment(PicGraphics.LT.LT_CUT, 0, 0
                        , box.PtMin.X	// x0
                        , box.PtMin.Y	// y0
                        , box.PtMin.X	// x1
                        , box.PtMax.Y	// y1
                        );
                }
            }
            if (arcReflY)
            {
                PicArc arc1 = fTemp.AddArc(PicGraphics.LT.LT_CUT, new Vector2D(0.0, 0.0), 100.0, a0, a1);
                arc1.Transform(Transform2D.ReflectionY);
            }
            if (arcReflX)
            {
                PicArc arc2 = fTemp.AddArc(PicGraphics.LT.LT_CUT, new Vector2D(0.0, 0.0), 100.0, a0, a1);
                arc2.Transform(Transform2D.ReflectionX);
            }
            if (arcReflXY)
            {
                PicArc arc3 = fTemp.AddArc(PicGraphics.LT.LT_CUT, new Vector2D(0.0, 0.0), 100.0, a0, a1);
                arc3.Transform(Transform2D.ReflectionX * Transform2D.ReflectionY);
            }
            if (arcComplement)
            {
                PicArc arc4 = fTemp.AddArc(PicGraphics.LT.LT_COTATION, new Vector2D(0.0, 0.0), 100.0, a0, a1);
                arc4.Complement();
            }
            
            // end
            factory.AddEntities(fTemp, transform);
        }
        #endregion
    }
}
