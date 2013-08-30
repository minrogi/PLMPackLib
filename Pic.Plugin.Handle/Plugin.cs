#region Using directives
using System;
using System.Collections.Generic;
using System.Text;

using Pic.Factory2D;
using Sharp3D.Math.Core;
using System.Drawing;
#endregion

namespace Pic.Plugin.Handle
{
    public class Plugin : Pic.Plugin.IPlugin, Pic.Plugin.IPluginExt1
    {
        #region Constructor
        public Plugin()
        {
        }
        #endregion

        #region Private fields
        string myName = "Handle";
        string myDescription = "Handle";
        string myAuthor = "treeDiM";
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
        /// <summary>
        /// Plugin name
        /// </summary>
        public string Name
        {
            get { return myName; }
        }
        /// <summary>
        /// Plugin version
        /// </summary>
        public string Version
        {
            get { return myVersion; }
        }
        /// <summary>
        /// Has embedded thumbnail?
        /// </summary>
        public bool HasEmbeddedThumbnail
        {
            get { return false; }
        }
        /// <summary>
        /// Get embedded thumbnail image
        /// </summary>
        public Bitmap Thumbnail
        {
            get { return null; }
        }
        /// <summary>
        /// Get source code
        /// </summary>
        public string SourceCode
        {
            get { return ""; }
        }
        public Guid Guid
        {
            get { return new Guid("{999D1D40-C934-46b1-9C19-6BE737501913}"); }
        }
        /// <summary>
        /// Access parameters
        /// </summary>
        public ParameterStack Parameters
        {
            get
            {
                ParameterStack stack = new ParameterStack();
                stack.AddDoubleParameter("L", "Length", 100.0, true, 0.0, false, 0.0);
                stack.AddDoubleParameter("H", "Height", 30.0, true, 0.0, false, 0.0);
                stack.AddDoubleParameter("E", "E", 0.0, true, 0.0, false, 0.0);
                string[] valueList = {"0", "1", "2", "3"};
                stack.AddMultiParameter("PoignsurTete", "PoignsurTete", valueList, valueList, 0);
                return stack;
            }
        }
        #endregion

        #region IPlugin methods
        public void Initialize() { }
        public void Dispose() { }
        public void CreateFactoryEntities(PicFactory factory, ParameterStack stack, Transform2D transform)
        {
            PicFactory fTemp = new PicFactory();
            const PicGraphics.LT ltCut = PicGraphics.LT.LT_CUT;
            const PicGraphics.LT ltFold = PicGraphics.LT.LT_CREASING;

            // get parameter values
            double L = stack.GetDoubleParameterValue("L");
            double H = stack.GetDoubleParameterValue("H");
            double E = stack.GetDoubleParameterValue("E");
            int PoignsurTete = stack.GetMultiParameterValue("PoignsurTete");

            short layer = 1;
            short grp = 1;
            PicGraphics.LT lt = ltCut;
            if (PoignsurTete >= 2)
            {
                lt = ltFold;
                layer = 2;
            }
            double x1 = (L-H)/2.0;
            double y1 = 0.0;
            double x2 = x1 - (L - H);
            double y2 = y1;
            fTemp.AddSegment(lt, grp, layer, x1, y1, x2, y2);

            lt = ltCut;
            double ry = H / 2.0;
            double xc = x2;
            double yc = - H / 2.0;
            fTemp.AddArc(lt, grp, layer, xc, yc, ry, 90.0, 270.0);

            x1 = xc;
            y1 = -H;
            x2 = x1 + (L - H);
            y2 = y1;
            fTemp.AddSegment(lt, grp, layer, x1, y1, x2, y2);

            ry = H / 2.0;
            xc = x2;
            yc = -H / 2.0;
            fTemp.AddArc(lt, grp, layer, xc, yc, ry, -90.0, 90.0);

            if (PoignsurTete == 3)
            {
                lt = ltCut;
                x1 = (L - H) / 2.0;
                y1 = 0.0;
                x2 = x1;
                y2 = y1 + E;
                fTemp.AddSegment(lt, grp, layer, x1, y1, x2, y2);

                x1 = x2;
                y1 = y2;
                x2 = x1 + E;
                y2 = y1;
                fTemp.AddSegment(lt, grp, layer, x1, y1, x2, y2);

                x1 = -(L - H) / 2.0;
                y1 = 0.0;
                x2 = x1;
                y2 = y1 + E;
                fTemp.AddSegment(lt, grp, layer, x1, y1, x2, y2);

                x1 = x2;
                y1 = y2;
                x2 = x1 - E;
                y2 = y1;
                fTemp.AddSegment(lt, grp, layer, x1, y1, x2, y2);

                lt = ltFold;
                x2 = x1 + (L - H);
                y2 = y1;
                fTemp.AddSegment(lt, grp, layer, x1, y1, x2, y2);
            }

            factory.AddEntities(fTemp, transform);
        }
        #endregion

        #region IpluginExt1 implementation
        public double ImpositionOffsetX(ParameterStack stack) { return 0.0; }
        public double ImpositionOffsetY(ParameterStack stack) { return 0.0; }
        #endregion
    }
}
