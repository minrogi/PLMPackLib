#region Using directives
using System;
using System.Collections.Generic;
using System.Text;

using Pic.Factory2D;
using Sharp3D.Math.Core;
using System.Drawing;
#endregion

namespace Pic.Plugin.SimpleRectangle
{
	/// <summary>
	/// SimpleRectangle
	/// </summary>
    public class Plugin : Pic.Plugin.IPlugin, Pic.Plugin.IPluginExt1
    {
        public Plugin()
        {
        }

        // Declarations of all our internal plugin variables
        string myName = "Simple rectangle";
        string myDescription = "A Simple rectangle. Could not be simpler!";
        string myAuthor = "François Gasnier";
        string myVersion = "1.0.0";
        IPluginHost myHost = null;

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

        public bool HasEmbeddedThumbnail
        {
            get { return false; }
        }

        public Bitmap Thumbnail
        {
            get { return null; }
        }

        public string SourceCode
        {
            get { return ""; }
        }
        public Guid Guid
        {
            get { return new Guid("{999D1D40-C934-46b1-9C19-6BE737501913}"); }
        }
        public void Initialize()
        {
            //This is the first Function called by the host...
            //Put anything needed to start with here first
        }

        public void Dispose()
        {
            //Put any cleanup code in here for when the program is stopped
        }
 
        public ParameterStack Parameters
        {
            get
            {
                ParameterStack stack = new ParameterStack();
                stack.AddDoubleParameter( "L", "Length", 200.0, 0.0);
                stack.AddDoubleParameter( "H", "Height", 100.0, 0.0);
                stack.AddBoolParameter("Rounding0", "Rounding0", true);
                stack.AddBoolParameter("Rounding1", "Rounding1", true);
                stack.AddBoolParameter("Rounding2", "Rounding2", true);
                stack.AddBoolParameter("Rounding3", "Rounding3", true);
                stack.AddDoubleParameter("R", "Radius", 10.0, 0.0);
                stack.AddDoubleParameter( "Angle", "Angle (deg)", 0.0, -360.0, 360.0); 
                return stack;
            }
        }

        public void CreateFactoryEntities(PicFactory factory, ParameterStack stack, Transform2D transform)
        {
            double L = stack.GetDoubleParameterValue("L");
            double H = stack.GetDoubleParameterValue("H");
            double R = stack.GetDoubleParameterValue("R");
            bool Rounding0 = stack.GetBoolParameterValue("Rounding0");
            bool Rounding1 = stack.GetBoolParameterValue("Rounding1");
            bool Rounding2 = stack.GetBoolParameterValue("Rounding2");
            bool Rounding3 = stack.GetBoolParameterValue("Rounding3");
            double Ang = stack.GetDoubleParameterValue("Angle") * Math.PI / 180.0;

            // segments
            PicFactory fTemp = new PicFactory();
            List<PicEntity> entityList = new List<PicEntity>();
            entityList.Add(fTemp.AddSegment(
                PicGraphics.LT.LT_CUT
                , new Vector2D(0.0, 0.0)
                , new Vector2D(L * Math.Cos(Ang), L * Math.Sin(Ang))));
            entityList.Add(fTemp.AddSegment(
                PicGraphics.LT.LT_CUT
                , new Vector2D(L * Math.Cos(Ang), L * Math.Sin(Ang))
                , new Vector2D(L * Math.Cos(Ang) - H * Math.Sin(Ang), L * Math.Sin(Ang) + H * Math.Cos(Ang))));
            entityList.Add(fTemp.AddSegment(
                PicGraphics.LT.LT_CUT
                , new Vector2D(L * Math.Cos(Ang) - H * Math.Sin(Ang), L * Math.Sin(Ang) + H * Math.Cos(Ang))
                , new Vector2D(-H * Math.Sin(Ang), H * Math.Cos(Ang))));
            entityList.Add(fTemp.AddSegment(
                PicGraphics.LT.LT_CUT
                , new Vector2D(-H * Math.Sin(Ang), H * Math.Cos(Ang))
                , new Vector2D(0.0, 0.0)));

            if (Rounding0) fTemp.ProcessTool(new PicToolRound(entityList[0], entityList[1], R));
            if (Rounding1) fTemp.ProcessTool(new PicToolRound(entityList[1], entityList[2], R));
            if (Rounding2) fTemp.ProcessTool(new PicToolRound(entityList[2], entityList[3], R));
            if (Rounding3) fTemp.ProcessTool(new PicToolRound(entityList[3], entityList[0], R));

            factory.AddEntities(fTemp, transform);
        }

        #region IpluginExt1 implementation
        public double ImpositionOffsetX(ParameterStack stack) { return 0.0; }
        public double ImpositionOffsetY(ParameterStack stack) { return 0.0; }
        #endregion
    }
}
