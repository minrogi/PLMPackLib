#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Pic.Factory2D;
using Sharp3D.Math.Core;
#endregion

namespace Pic.Plugin.SleeveBoxArchitecture
{
    public class Plugin : Pic.Plugin.IPlugin, Pic.Plugin.IPluginExt2
    {
        #region Constructor
        public Plugin()
        {
        }
        #endregion

        #region Private fields
        private string myName = "Sleeve-Box_Architecture";
        private string myDescription = "Sleeve box architecture";
        private string myAuthor = "treeDiM";
        private string myVersion = "2.0.0.0";
        private IPluginHost myHost = null;
        #endregion

        #region Public properties
        /// <summary>
        /// Description of the plugin's purpose
        /// </summary>
        public string Description { get { return myDescription; } }
        /// <summary>
        /// Author of the plugin
        /// </summary>
        public string Author { get { return myAuthor; } }
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
        public string Name { get { return myName; } }
        /// <summary>
        /// Plugin version
        /// </summary>
        public string Version { get { return myVersion; } }
        /// <summary>
        /// Has embedded thumbnail?
        /// </summary>
        public bool HasEmbeddedThumbnail { get { return false; } }
        /// <summary>
        /// Get embedded thumbnail image
        /// </summary>
        public Bitmap Thumbnail { get { return null; } }
        /// <summary>
        /// Get source code
        /// </summary>
        public string SourceCode { get { return ""; } }
        public Guid Guid { get { return new Guid("{2B88E9CC-72EC-46E7-B354-85D42F7A8F1A}"); } }
        public ParameterStack Parameters
        { get { throw new NotImplementedException("Plugin.Parameters not supported with IPluginExt2"); } }
        #endregion

        #region IPlugin methods
        public void Initialize() { }
        public void Dispose() { }
        public void CreateFactoryEntities(PicFactory factory, ParameterStack stack, Transform2D transform)
        {
            PicFactory fTemp = new PicFactory();

            // free variables
            double a = stack.GetDoubleParameterValue("a");
            double b = stack.GetDoubleParameterValue("b");
            double h = stack.GetDoubleParameterValue("h");
            double e = stack.GetDoubleParameterValue("e");
            double g = stack.GetDoubleParameterValue("g");

            int iTop = stack.GetMultiParameterValue("TOP");
            int iBot = stack.GetMultiParameterValue("BOTTOM");

            double gg = 15.0;

            // formulas
            SortedList<uint, PicEntity> entities = new SortedList<uint, PicEntity>();

            if (g < 5)
            { // Glue_flap
                IPlugin pluginIn = Host.GetPluginByGuid("729625f4-921d-4f72-af43-4248835a59f3");
                ParameterStack stackIn = Host.GetInitializedParameterStack(pluginIn);
                gg = stackIn.GetDoubleParameterValue("g");
            }
            else
                gg = g;
            //---------- TOP Architecture ---------------------
            if (iTop == 0)
            { // Sleeve
                IPlugin pluginIn = Host.GetPluginByGuid("da290efa-83a5-4ccd-808c-9a5eec81f36b");
                ParameterStack stackIn = Host.GetInitializedParameterStack(pluginIn);
                stackIn.SetDoubleParameter("A", a);		// A
                stackIn.SetDoubleParameter("B", b);		// B
                stackIn.SetDoubleParameter("e", e);		// e
                stackIn.SetDoubleParameter("H", h / 2);		// H
                stackIn.SetDoubleParameter("g", gg);		// g
                bool reflectionX = false, reflectionY = false;
                Transform2D transfReflect = (reflectionY ? Transform2D.ReflectionY : Transform2D.Identity) * (reflectionX ? Transform2D.ReflectionX : Transform2D.Identity);
                pluginIn.CreateFactoryEntities(fTemp, stackIn,
                     Transform2D.Translation(new Vector2D(gg, 0.0))
                     * Transform2D.Rotation(0.0)
                     * transfReflect);
            }
            else if (iTop == 1)
            { // Tuck_end
                int iTuck = stack.GetMultiParameterValue("TUCK");

                IPlugin pluginIn = Host.GetPluginByGuid("818567a3-ce01-45f5-b328-04031713c12c");
                ParameterStack stackIn = Host.GetInitializedParameterStack(pluginIn);
                stackIn.SetDoubleParameter("a", a);		// A
                stackIn.SetDoubleParameter("b", b);		// B
                stackIn.SetDoubleParameter("h", h / 2);		// H
                stackIn.SetDoubleParameter("e", e);		// t
                stackIn.SetDoubleParameter("g", g);		// g
                if (2 == iTuck)
                {
                    int iHole = stack.GetMultiParameterValue("HOLE");
                    stackIn.SetMultiParameter("HOLE", iHole);		// Hanging Hole			
                }
                stackIn.SetDoubleParameter("bp", iTuck);
                bool reflectionX = false, reflectionY = false;
                Transform2D transfReflect = (reflectionY ? Transform2D.ReflectionY : Transform2D.Identity) * (reflectionX ? Transform2D.ReflectionX : Transform2D.Identity);
                pluginIn.CreateFactoryEntities(fTemp, stackIn,
                     Transform2D.Translation(new Vector2D(0.0, 0.0))
                     * Transform2D.Rotation(0.0)
                     * transfReflect);
            }
            else if (iTop == 2)
            { // Inverted_Tuck_end
                int iTuck = stack.GetMultiParameterValue("TUCK");

                IPlugin pluginIn = Host.GetPluginByGuid("66e5437d-a8a8-404d-951c-e7bf944d2342");
                ParameterStack stackIn = Host.GetInitializedParameterStack(pluginIn);
                stackIn.SetDoubleParameter("a", a);		// A
                stackIn.SetDoubleParameter("b", b);		// B
                stackIn.SetDoubleParameter("h", h / 2);		// H
                stackIn.SetDoubleParameter("e", e);		// t
                stackIn.SetDoubleParameter("g", g);		// g
                stackIn.SetDoubleParameter("bp", iTuck);
                bool reflectionX = false, reflectionY = false;
                Transform2D transfReflect = (reflectionY ? Transform2D.ReflectionY : Transform2D.Identity) * (reflectionX ? Transform2D.ReflectionX : Transform2D.Identity);
                pluginIn.CreateFactoryEntities(fTemp, stackIn,
                     Transform2D.Translation(new Vector2D(0.0, 0.0))
                     * Transform2D.Rotation(0.0)
                     * transfReflect);
            }
            else if (iTop == 3)
            { // Edge_Lock
                int iEdge = stack.GetMultiParameterValue("Edge");

                IPlugin pluginIn = Host.GetPluginByGuid("827b4625-ccad-41f8-823a-c165852ca8f4");
                ParameterStack stackIn = Host.GetInitializedParameterStack(pluginIn);
                stackIn.SetDoubleParameter("A", a);		// A
                stackIn.SetDoubleParameter("B", b);		// B
                stackIn.SetDoubleParameter("h", h / 2);		// h
                stackIn.SetDoubleParameter("e", e);		// e
                stackIn.SetDoubleParameter("g", gg);		// g
                stackIn.SetMultiParameter("Edge", iEdge);		// Edge Lock
                stackIn.SetDoubleParameter("A1", b);			// A1
                bool reflectionX = false, reflectionY = false;
                Transform2D transfReflect = (reflectionY ? Transform2D.ReflectionY : Transform2D.Identity) * (reflectionX ? Transform2D.ReflectionX : Transform2D.Identity);
                pluginIn.CreateFactoryEntities(fTemp, stackIn,
                     Transform2D.Translation(new Vector2D(gg, 0.0))
                     * Transform2D.Rotation(0.0)
                     * transfReflect);
            }
            else if (iTop == 4)
            { // Seal_End
                int iSeal = stack.GetMultiParameterValue("Seal");

                IPlugin pluginIn = Host.GetPluginByGuid("af7fb901-90de-4034-9a27-c21d51f826d2");
                ParameterStack stackIn = Host.GetInitializedParameterStack(pluginIn);
                stackIn.SetDoubleParameter("A", a);		// A
                stackIn.SetDoubleParameter("B", b);		// B
                stackIn.SetDoubleParameter("h", h / 2);		// h
                stackIn.SetDoubleParameter("g", gg);		// g
                stackIn.SetMultiParameter("Seal", iSeal);		// Seal End
                stackIn.SetDoubleParameter("e", e);		// e
                bool reflectionX = false, reflectionY = false;
                Transform2D transfReflect = (reflectionY ? Transform2D.ReflectionY : Transform2D.Identity) * (reflectionX ? Transform2D.ReflectionX : Transform2D.Identity);
                pluginIn.CreateFactoryEntities(fTemp, stackIn,
                     Transform2D.Translation(new Vector2D(gg, 0.0))
                     * Transform2D.Rotation(0.0)
                     * transfReflect);
            }


            //---------- BOTTOM Architecture ---------------------
            if (iBot == 0)
            { // Sleeve
                IPlugin pluginIn = Host.GetPluginByGuid("da290efa-83a5-4ccd-808c-9a5eec81f36b");
                ParameterStack stackIn = Host.GetInitializedParameterStack(pluginIn);
                stackIn.SetDoubleParameter("A", a);		// A
                stackIn.SetDoubleParameter("B", b);		// B
                stackIn.SetDoubleParameter("e", e);		// e
                stackIn.SetDoubleParameter("H", h / 2);		// H
                stackIn.SetDoubleParameter("g", gg);		// g
                bool reflectionX = true, reflectionY = false;
                Transform2D transfReflect = (reflectionY ? Transform2D.ReflectionY : Transform2D.Identity) * (reflectionX ? Transform2D.ReflectionX : Transform2D.Identity);
                pluginIn.CreateFactoryEntities(fTemp, stackIn,
                     Transform2D.Translation(new Vector2D(gg, 0.0))
                     * Transform2D.Rotation(0.0)
                     * transfReflect);
            }
            else if (iBot == 1)
            { // Tuck_end
                int iBTuck = stack.GetMultiParameterValue("BTUCK");
                IPlugin pluginIn = Host.GetPluginByGuid("818567a3-ce01-45f5-b328-04031713c12c");
                ParameterStack stackIn = Host.GetInitializedParameterStack(pluginIn);
                stackIn.SetDoubleParameter("a", a);		// A
                stackIn.SetDoubleParameter("b", b);		// B
                stackIn.SetDoubleParameter("h", h / 2);		// H
                stackIn.SetDoubleParameter("e", e);		// t
                stackIn.SetDoubleParameter("g", g);		// g
                stackIn.SetDoubleParameter("bp", 0.0);
                if (iBTuck == 2)
                    stackIn.SetDoubleParameter("bp", 0);
                else
                    stackIn.SetDoubleParameter("bp", iBTuck);
                bool reflectionX = true, reflectionY = false;
                Transform2D transfReflect = (reflectionY ? Transform2D.ReflectionY : Transform2D.Identity) * (reflectionX ? Transform2D.ReflectionX : Transform2D.Identity);
                pluginIn.CreateFactoryEntities(fTemp, stackIn,
                     Transform2D.Translation(new Vector2D(0.0, 0.0))
                     * Transform2D.Rotation(0.0)
                     * transfReflect);
            }
            else if (iBot == 2)
            { // Inverted_Tuck_end
                int iBTuck = stack.GetMultiParameterValue("BTUCK");

                IPlugin pluginIn = Host.GetPluginByGuid("66e5437d-a8a8-404d-951c-e7bf944d2342");
                ParameterStack stackIn = Host.GetInitializedParameterStack(pluginIn);
                stackIn.SetDoubleParameter("a", a);		// A
                stackIn.SetDoubleParameter("b", b);		// B
                stackIn.SetDoubleParameter("h", h / 2);		// H
                stackIn.SetDoubleParameter("e", e);		// t
                stackIn.SetDoubleParameter("g", g);		// g
                stackIn.SetDoubleParameter("bp", 0.0);
                if (iBTuck == 2)
                    stackIn.SetDoubleParameter("bp", 0);
                else
                    stackIn.SetDoubleParameter("bp", iBTuck);
                bool reflectionX = true, reflectionY = false;
                Transform2D transfReflect = (reflectionY ? Transform2D.ReflectionY : Transform2D.Identity) * (reflectionX ? Transform2D.ReflectionX : Transform2D.Identity);
                pluginIn.CreateFactoryEntities(fTemp, stackIn,
                     Transform2D.Translation(new Vector2D(0.0, 0.0))
                     * Transform2D.Rotation(0.0)
                     * transfReflect);
            }
            else if (iBot == 3)
            { // Snap_lock_base
                IPlugin pluginIn = Host.GetPluginByGuid("2c366e1f-35d1-4e72-ba2b-7786e699f94c");
                ParameterStack stackIn = Host.GetInitializedParameterStack(pluginIn);
                stackIn.SetDoubleParameter("a", a);		// a
                stackIn.SetDoubleParameter("b", b);		// b
                stackIn.SetDoubleParameter("h", h / 2);		// h
                stackIn.SetDoubleParameter("e", e);		// e
                stackIn.SetDoubleParameter("g", g);		// g
                bool reflectionX = false, reflectionY = false;
                Transform2D transfReflect = (reflectionY ? Transform2D.ReflectionY : Transform2D.Identity) * (reflectionX ? Transform2D.ReflectionX : Transform2D.Identity);
                pluginIn.CreateFactoryEntities(fTemp, stackIn,
                     Transform2D.Translation(new Vector2D(0.0, 0.0))
                     * Transform2D.Rotation(0.0)
                     * transfReflect);
            }
            else if (iBot == 4)
            { // Crash_lock_base
                IPlugin pluginIn = Host.GetPluginByGuid("2015adce-a857-49c8-b051-b6891b90b941");
                ParameterStack stackIn = Host.GetInitializedParameterStack(pluginIn);
                stackIn.SetDoubleParameter("a", a);		// a
                stackIn.SetDoubleParameter("b", b);		// b
                stackIn.SetDoubleParameter("h", h / 2);		// h
                stackIn.SetDoubleParameter("e", e);		// e
                stackIn.SetDoubleParameter("d", e);		// d
                stackIn.SetDoubleParameter("g", g);		// g
                bool reflectionX = false, reflectionY = false;
                Transform2D transfReflect = (reflectionY ? Transform2D.ReflectionY : Transform2D.Identity) * (reflectionX ? Transform2D.ReflectionX : Transform2D.Identity);
                pluginIn.CreateFactoryEntities(fTemp, stackIn,
                     Transform2D.Translation(new Vector2D(0.0, -h / 2))
                     * Transform2D.Rotation(0.0)
                     * transfReflect);
            }
            else if (iBot == 5)
            { // Edge_Lock
                int iBEdge = stack.GetMultiParameterValue("BEdge");

                IPlugin pluginIn = Host.GetPluginByGuid("827b4625-ccad-41f8-823a-c165852ca8f4");
                ParameterStack stackIn = Host.GetInitializedParameterStack(pluginIn);
                stackIn.SetDoubleParameter("A", a);		// A
                stackIn.SetDoubleParameter("B", b);		// B
                stackIn.SetDoubleParameter("h", h / 2);		// h
                stackIn.SetDoubleParameter("e", e);		// e
                stackIn.SetDoubleParameter("g", gg);		// g
                stackIn.SetMultiParameter("Edge", iBEdge);		// Edge Lock
                stackIn.SetDoubleParameter("A1", b);			// A1
                bool reflectionX = true, reflectionY = false;
                Transform2D transfReflect = (reflectionY ? Transform2D.ReflectionY : Transform2D.Identity) * (reflectionX ? Transform2D.ReflectionX : Transform2D.Identity);
                pluginIn.CreateFactoryEntities(fTemp, stackIn,
                     Transform2D.Translation(new Vector2D(gg, 0.0))
                     * Transform2D.Rotation(0.0)
                     * transfReflect);
            }
            else if (iBot == 6)
            { // Seal_End
                int iBSeal = stack.GetMultiParameterValue("BSeal");

                IPlugin pluginIn = Host.GetPluginByGuid("af7fb901-90de-4034-9a27-c21d51f826d2");
                ParameterStack stackIn = Host.GetInitializedParameterStack(pluginIn);
                stackIn.SetDoubleParameter("A", a);		// A
                stackIn.SetDoubleParameter("B", b);		// B
                stackIn.SetDoubleParameter("h", h / 2);		// h
                stackIn.SetDoubleParameter("g", gg);		// g
                stackIn.SetMultiParameter("Seal", iBSeal);		// Seal End
                stackIn.SetDoubleParameter("e", e);		// e
                bool reflectionX = true, reflectionY = false;
                Transform2D transfReflect = (reflectionY ? Transform2D.ReflectionY : Transform2D.Identity) * (reflectionX ? Transform2D.ReflectionX : Transform2D.Identity);
                pluginIn.CreateFactoryEntities(fTemp, stackIn,
                     Transform2D.Translation(new Vector2D(gg, 0.0))
                     * Transform2D.Rotation(0.0)
                     * transfReflect);
            }

            factory.AddEntities(fTemp, transform);
        }
        #endregion

        #region IPluginExt2 implementation
        public double ImpositionOffsetX(ParameterStack stack) { return 0.0; }
        public double ImpositionOffsetY(ParameterStack stack) { return 0.0; }

        /// <summary>
        /// Build / rebuild parameter stack 
        /// </summary>
        public ParameterStack BuildParameterStack(ParameterStack stackIn)
        {
            ParameterStackUpdater paramUpdater = new ParameterStackUpdater(stackIn);

            paramUpdater.CreateDoubleParameter("a", "A  [ length ]", 100, 0);
            paramUpdater.CreateDoubleParameter("b", "B  [ width ]", 60, 0);
            paramUpdater.CreateDoubleParameter("h", "H  [ height ]", 120, 0);
            paramUpdater.CreateDoubleParameter("e", "thickness", 0.5, 0);
            paramUpdater.CreateDoubleParameter("g", "Glue Flap [ 0=default ]", 0, 0);

            string[] vListTop = { "Sleeve", "Tuck End", "Inversed Tuck End", "Edge Lock", "Seal End" }; //,"Snap Lock","Crash Lock"};
            Parameter paramTopStyle = paramUpdater.CreateMultiParameter("TOP", "TOP Style", vListTop, vListTop, 0);

            if (1 == paramUpdater.GetMultiParameterValue("TOP") || 2 == paramUpdater.GetMultiParameterValue("TOP"))
            {
                string[] vListTuck = { "Simple", "Thumb Hole", "Hanging Tab", "Lock Tongue", "Crash Lock Tongue", "Mailer Lock" };
                Parameter paramTopTuckEnd = paramUpdater.CreateMultiParameter("TUCK", "-- Tuck End", vListTuck, vListTuck, 0);
                paramTopTuckEnd.Parent = paramTopStyle;

                if (2 == paramUpdater.GetMultiParameterValue("TUCK"))
                {
                    string[] vListHole = { "Euro", "Euro Std", "Delta", "Hardware", "Round" };
                    Parameter paramTopHole = paramUpdater.CreateMultiParameter("HOLE", "* Hanging Hole", vListHole, vListHole, 0);
                    paramTopHole.Parent = paramTopTuckEnd;
                }
            }
            else if (3 == paramUpdater.GetMultiParameterValue("TOP"))
            {
                string[] vListEdge = { "Simple Edge Lock", "Crash Edge lock" };
                Parameter paramTopEdgeLock = paramUpdater.CreateMultiParameter("Edge", "-- Edge Lock", vListEdge, vListEdge, 0);
                paramTopEdgeLock.Parent = paramTopStyle;
            }
            else if (4 == paramUpdater.GetMultiParameterValue("TOP"))
            {
                string[] vListSeal = { "Economy", "Full Overlapping", "Reduced Flaps", "Lock Tab" };
                Parameter paramTopSealEnd = paramUpdater.CreateMultiParameter("Seal", "-- Seal End", vListSeal, vListSeal, 0);
                paramTopSealEnd.Parent = paramTopStyle;
            }

            string[] vListBot = { "Sleeve", "Tuck End", "Inversed Tuck End", "Crash-Lock Automatic", "Snap-Lock Semi-Auto", "Edge Lock", "Seal End" };
            Parameter paramBottom = paramUpdater.CreateMultiParameter("BOTTOM", "BOTTOM Style", vListBot, vListBot, 0);

            if (1 == paramUpdater.GetMultiParameterValue("BOTTOM") || 2 == paramUpdater.GetMultiParameterValue("BOTTOM"))
            {
                string[] vListBTuck = { "Simple", "Thumb Hole", "---", "Lock Tongue", "Crash Lock Tongue", "Mailer Lock" };
                Parameter paramBottomTuckEnd = paramUpdater.CreateMultiParameter("BTUCK", "-- Tuck End", vListBTuck, vListBTuck, 0);
                paramBottomTuckEnd.Parent = paramBottom;
            }
            else if (5 == paramUpdater.GetMultiParameterValue("BOTTOM"))
            {
                string[] vListBEdge = { "Simple Edge Lock", "Crash Edge lock" };
                Parameter paramBottomEdgeLock = paramUpdater.CreateMultiParameter("BEdge", "-- Edge Lock", vListBEdge, vListBEdge, 0);
                paramBottomEdgeLock.Parent = paramBottom;
            }
            else if (6 == paramUpdater.GetMultiParameterValue("BOTTOM"))
            {
                string[] vListBSeal = { "Economy", "Full Overlapping", "Reduced Flaps", "Lock Tab" };
                Parameter paramBottomSealEnd = paramUpdater.CreateMultiParameter("BSeal", "-- Seal End", vListBSeal, vListBSeal, 0);
                paramBottomSealEnd.Parent = paramBottom;
            }
            return paramUpdater.UpdatedStack;
        }
        /// <summary>
        /// Is supporting palletization ?
        /// </summary>
        public bool IsSupportingPalletization { get { return false; } }
        /// <summary>
        /// Outer dimensions
        /// Method should only be called if component supports palletization
        /// </summary>
        public void OuterDimensions(ParameterStack stack, out double[] dimensions)
        {
            dimensions = new double[3];
        }
        /// <summary>
        /// Returns case type to be used for ECT computation 
        /// </summary>
        public int CaseType { get { return 0; } }
        /// <summary>
        /// Is supporting automatic folding
        /// </summary>
        public bool IsSupportingAutomaticFolding { get { return true; } }
        /// <summary>
        /// Reference point that defines anchored face
        /// </summary>
        public List<Vector2D> ReferencePoints(ParameterStack stack)
        {
            List<Vector2D> ltPoints = new List<Vector2D>();
            return ltPoints;
        }
        #endregion
    }
}
