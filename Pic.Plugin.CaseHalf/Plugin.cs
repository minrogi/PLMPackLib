#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Pic.Factory2D;
using Sharp3D.Math.Core;
using System.Resources;
using Pic.Plugin;
#endregion

namespace Pic.Plugin.CaseHalf
{
    public class Plugin : IPlugin
    {
        #region Constructor
        public Plugin()
        {
        }
        #endregion

        #region Declarations of all our internal plugin variables
        string myName = "CaseHalf";
        string myDescription = "CaseHalf";
        string myAuthor = "treeDIM";
        string myVersion = "1.0.0";
        IPluginHost myHost = null;
        #endregion

        #region IPlugin overrides properties
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
        /// Access thumbnail
        /// </summary>
        public bool HasEmbeddedThumbnail
        {
            get { return false; }
        }
        public Bitmap Thumbnail
        {
            get
            {
                return null;
            }
        }
        public double ImpositionOffsetX(ParameterStack stack) { return 0.0; }
        public double ImpositionOffsetY(ParameterStack stack) { return 0.0; }

        /// <summary>
        /// Access plugin source code
        /// </summary>
        public string SourceCode
        {
            get { return ""; }
        }

        public Guid Guid
        {
            get { return new Guid("{17C92661-8FFD-4d4e-88CE-AFE8A2186057}"); }
        }

        /// <summary>
        /// get list of parameters
        /// </summary>
        public ParameterStack Parameters
        {
            get
            {
                ParameterStack stack = new ParameterStack();
                stack.AddDoubleParameter("ParamX1", "ParamX1", 0.0, true, 0.0, false, 0.0);
                stack.AddDoubleParameter("L1", "L1", 0.0, true, 0.0, false, 0.0);
                stack.AddDoubleParameter("B1", "B1", 0.0, true, 0.0, false, 0.0);
                stack.AddDoubleParameter("L2", "L2", 0.0, true, 0.0, false, 0.0);
                stack.AddDoubleParameter("B2", "B2", 0.0, true, 0.0, false, 0.0);
                stack.AddDoubleParameter("H1", "H1", 0.0, true, 0.0, false, 0.0);
                stack.AddDoubleParameter("RL", "RL", 0.0, true, 0.0, false, 0.0);
                stack.AddDoubleParameter("RB", "RB", 0.0, true, 0.0, false, 0.0);
                stack.AddDoubleParameter("Xo", "Xo", 0.0, true, 0.0, false, 0.0);
                stack.AddDoubleParameter("Yo", "Yo", 0.0, true, 0.0, false, 0.0);
                stack.AddDoubleParameter("Diro", "Diro", 0.0, true, 0.0, false, 0.0);
                stack.AddBoolParameter("Symy", "Symy", false);
                stack.AddDoubleParameter("m15", "m15", 0.0, true, 0.0, false, 0.0);
                stack.AddDoubleParameter("m16", "m16", 0.0, true, 0.0, false, 0.0);
                stack.AddDoubleParameter("decalenc", "decalenc", 0.0, true, 0.0, false, 0.0);
                stack.AddDoubleParameter("encoche", "encoche", 0.0, true, 0.0, false, 0.0);
                stack.AddBoolParameter("caisse_en_2", "caisse_en_2", false);
                stack.AddIntParameter("caisse_en_4", "caisse_en_4", 0, true, 0, false, 0);
                stack.AddDoubleParameter("lgPatte", "lgPatte", 0, true, 0, false, 0);
                return stack;
            }
        }
        #endregion

        #region IPlugin method implementation
        public void Initialize() { }
        public void Dispose() { }

        public void CalEncoche(ref double ec1, ref double ec2, double majo15, double majo16, double decalenc, double encoche)
        {
            if (majo15 != 0.0 && majo16 != 0.0)
            {
                ec1 = majo15;
                ec2 = majo16;
            }
            else if (decalenc <= encoche)
            {
                ec1 = encoche * 0.5 - decalenc;
                ec2 = encoche - ec1;
            }
            else
            {
                ec1 = encoche * 0.5;
                ec2 = encoche * 0.5;
            }
        }

        /// <summary>
        /// Create factory entities
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="stack"></param>
        public void CreateFactoryEntities(PicFactory factory, ParameterStack stack, Transform2D transform)
        {

            // get parameter values
            double ParamX1 = stack.GetDoubleParameterValue("ParamX1");
            double L1 = stack.GetDoubleParameterValue("L1");
            double B1 = stack.GetDoubleParameterValue("B1");
            double L2 = stack.GetDoubleParameterValue("L2");
            double B2 = stack.GetDoubleParameterValue("B2");
            double H1 = stack.GetDoubleParameterValue("H1");
            double RL = stack.GetDoubleParameterValue("RL");
            double RB = stack.GetDoubleParameterValue("RB");
            double Xo = stack.GetDoubleParameterValue("Xo");
            double Yo = stack.GetDoubleParameterValue("Yo");
            double Diro = stack.GetDoubleParameterValue("Diro");
            bool Symy = stack.GetBoolParameterValue("Symy");

            double m15 = stack.GetDoubleParameterValue("m15");
            double m16 = stack.GetDoubleParameterValue("m16");
            double decalenc = stack.GetDoubleParameterValue("decalenc");
            double encoche = stack.GetDoubleParameterValue("encoche");

            int PoignsurTete = stack.GetIntParameterValue("PoignsurTete");

            bool caisse_en_2 = stack.GetBoolParameterValue("caisse_en_2");
            int caisse_en_4 = stack.GetIntParameterValue("caisse_en_4");

            double lgPatte = stack.GetDoubleParameterValue("lgPatte");

            IPlugin DemiPatteCol = stack.GetPluginParameterValue("DemiPatteCol");
            IPlugin Poignee = stack.GetPluginParameterValue("Poignee");


            double HY = 0.0;

            double ec1 = 0.0, ec2 = 0.0;
            CalEncoche(ref ec1, ref ec2, m15, m16, decalenc, encoche);

            if (caisse_en_2 || caisse_en_4 > 0)
            {
                B1 = B2;
                L2 = 0.0;
                B2 = 0.0;
            }
            if (caisse_en_4 == 1 || caisse_en_4 == 3)
            {
                B1 = 0.0;
                RB = RL;
                L2 = 0.0;
                B2 = 0.0;
            }
            if (caisse_en_4 == 2 || caisse_en_4 == 4)
            {
                L1 = B1;
                RL = RB;
                B1 = 0.0;
                L2 = 0.0;
                B2 = 0.0;
            }

            double PX1 = lgPatte;
            double PX2 = L1;
            double PX3 = B1;
            double PX4 = L2;
            double PX5 = B2;

            double RBY1 = 0.0, RBY2 = 0.0, RBY3 = 0.0, RBY4 = 0.0, RBY5 = 0.0;
            double RHY1 = 0.0, RHY2 = 0.0, RHY3 = 0.0, RHY4 = 0.0, RHY5 = 0.0;
            double patProlong = 0.0;
            if (Symy)
            {
                RBY1 = patProlong;
                RBY2 = RL;
                RBY3 = RB;
                RBY4 = RL;
                RBY5 = RB;
            }
            else
            {
                RHY1 = patProlong;
                RHY2 = RL;
                RHY3 = RB;
                RHY4 = RL;
                RHY5 = RB;
            }
            HY = HY + H1;

            if (caisse_en_2 || caisse_en_4 > 0)
            {
                if (PX5 != 0)
                    PX3 = PX5;
                PX4 = 0;
                PX5 = 0;
                RHY4 = 0;
                RHY5 = 0;
                RBY4 = 0;
                RBY5 = 0;
            }

            double V0 = 0.0;
            double v1 = V0 + lgPatte;
            double v2 = v1 + L1;
            double v3 = v2 + B1;
            double v4 = v3 + L2;
            double V5 = v4 + B2;
            double V6 = H1;
            double w0 = 0.0;
            double w1 = V6 + RL;
            double w2 = V6 + RB;
            double w3 = V6 + RL;
            double w4 = V6 + RB;

            SortedList<uint, PicEntity> entities = new SortedList<uint, PicEntity>();
            PicFactory fTemp = new PicFactory();

            // segments
            double x1 = 0.0, y1 = 0.0, x2 = 0.0, y2 = 0.0;
            uint index = 0;
            const PicGraphics.LT ltCut = PicGraphics.LT.LT_CUT;
            const PicGraphics.LT ltFold = PicGraphics.LT.LT_CREASING;

            if (3 == caisse_en_4)
            {
                x1 = v1;
                y1 = V6;
                x2 = v2;
                y2 = y1;
                entities.Add(++index, fTemp.AddSegment(ltFold, 1, 1, x1, y1, x2, y2));

                x1 = v1;
                y1 = w0;
                x2 = x1;
                y2 = w2;
                entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));

                x1 = x2;
                y1 = y2;
                x2 = v2;
                y2 = y1;
                entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));

                x1 = x2;
                y1 = y2;
                x2 = x1;
                y2 = w0;
                entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));
            }
            else
            {
                x1 = v1;
                y1 = w0;
                x2 = x1;
                y2 = V6;
                entities.Add(++index, fTemp.AddSegment(ltFold, 1, 1, x1, y1, x2, y2));

                x1 = v2;
                y1 = w0;
                x2 = x1;
                y2 = V6;

                if (!caisse_en_2 && 0 == caisse_en_4)
                {
                    entities.Add(++index, fTemp.AddSegment(ltFold, 1, 1, x1, y1, x2, y2));

                    x1 = v3;
                    y1 = w0;
                    x2 = x1;
                    y2 = V6;
                    entities.Add(++index, fTemp.AddSegment(ltFold, 1, 1, x1, y1, x2, y2));

                    x1 = v4;
                    y1 = w0;
                    x2 = x1;
                    y2 = V6;
                }
                if (0 == caisse_en_4)
                {
                    entities.Add(++index, fTemp.AddSegment(ltFold, 1, 1, x1, y1, x2, y2));
                    x1 = v1 + ec2;
                    y1 = V6;
                    x2 = v2 - ec2;
                }
                else
                {
                    x1 = v1 + ec2;
                    y1 = V6;
                    if (caisse_en_4 == 4)
                        x2 = v2 - ec2;
                    else
                        x2 = v2;
                }
                y2 = y1;
                entities.Add(++index, fTemp.AddSegment(ltFold, 1, 1, x1, y1, x2, y2));

                if (caisse_en_4 == 4)
                {
                    x1 = x2;
                    y1 = y2;
                    x2 = x1 + ec1;
                    y2 = y1;
                    entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));
                }
                x1 = v2 + ec1;
                y1 = V6;
                if (!caisse_en_2 && caisse_en_4 == 0)
                    x2 = v3 - ec1;
                else
                    x2 = v3;
                y2 = y1;
                if (caisse_en_4 == 0)
                    entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));

                if (!caisse_en_2 && caisse_en_4 == 0)
                {
                    // 9
                    x1 = v3 + ec2;
                    y1 = V6;
                    x2 = v4 - ec2;
                    y2 = y1;
                    entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));

                    // 10
                    x1 = v4 + ec1;
                    y1 = V6;
                    x2 = V5;
                    y2 = y1;
                    entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));
                }


                // 11
                if (!caisse_en_2 && caisse_en_4 == 0)
                    x1 = V5;
                else
                {
                    if (caisse_en_4 == 0)
                        x1 = v3;
                    else
                        x1 = v2;
                }
                y1 = w0;
                x2 = x1;
                if (caisse_en_4 == 4)
                    y2 = V6;
                else
                    y2 = w4;
                entities.Add(++index, fTemp.AddSegment(caisse_en_4 == 4 ? ltFold : ltCut, 1, 1, x1, y1, x2, y2));

                if (caisse_en_4 == 4)
                {
                    x1 = x1 - ec1;
                    x2 = x1;
                    y1 = y2;
                    y2 = w4;
                    entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));
                }

                if (!caisse_en_2 && caisse_en_4 == 0)
                {
                    // 12
                    x1 = x2;
                    y1 = y2;
                    x2 = v4 + ec1;
                    y2 = y1;
                    entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));

                    // 13
                    x1 = x2;
                    y1 = y2;
                    x2 = x1;
                    y2 = V6;
                    entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));

                    // 14
                    x1 = x2;
                    y1 = y2;
                    x2 = v4 - ec2;
                    y2 = y1;
                    entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));

                    // 15
                    x1 = x2;
                    y1 = y2;
                    x2 = x1;
                    y2 = w3;
                    entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));

                    // 16
                    x1 = x2;
                    y1 = y2;
                    x2 = v3 + ec2;
                    y2 = y1;
                    entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));

                    // 17
                    x1 = x2;
                    y1 = y2;
                    x2 = x1;
                    y2 = V6;
                    entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));

                    // 18
                    x1 = x2;
                    y1 = y2;
                    x2 = v3 - ec1;
                    y2 = y1;
                    entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));

                    // 19
                    x1 = x2;
                    y1 = y2;
                    x2 = x1;
                    y2 = w2;
                    entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));
                }

                // 20
                x1 = x2;
                y1 = y2;
                y2 = y1;
                if (caisse_en_4 == 0)
                {
                    x2 = v2 + ec1;
                    entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));
                    // 21
                    x1 = x2;
                    y1 = y2;
                    x2 = x1;
                    y2 = V6;
                    entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));
                    // 22
                    x1 = x2;
                    y1 = y2;
                    x2 = v2 - ec2;
                    y2 = y1;
                    entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));
                    // 23
                    x1 = x2;
                    y1 = y2;
                    x2 = x1;
                    y2 = w1;
                    entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));
                    // 24
                }
                else
                {
                    if (caisse_en_4 != 4)
                        x2 = v2;
                }

                x1 = x2;
                y1 = y2;
                x2 = v1 + ec2;
                y2 = y1;
                entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));

                // 25
                x1 = x2;
                y1 = y2;
                x2 = x1;
                y2 = V6;
                entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));

                // 26
                x1 = x2;
                y1 = y2;
                x2 = v1;
                y2 = y1;
                entities.Add(++index, fTemp.AddSegment(ltCut, 1, 1, x1, y1, x2, y2));

                // Patte de Collage
                ParameterStack stackDemiPatteCol = DemiPatteCol.Parameters;
                if (caisse_en_4 != 3)
                {

                    //Call DemiPatteCol(ParamX1, lgPatte, H1, ec1, anglPc, patProlong, 0#, 0#, 0#, False)
                    DemiPatteCol.CreateFactoryEntities(fTemp, stackDemiPatteCol, transform);
                }
                if (caisse_en_4 == 4)
                {
                    // Call DemiPatteCol(ParamX1, lgPatte, H1, ec1, anglPc, patProlong, L1 + 2 * lgPatte, 0#, 180, True)
                    DemiPatteCol.CreateFactoryEntities(fTemp, stackDemiPatteCol, transform);
                }
            }

            if ((!Symy && PoignsurTete > 0) && caisse_en_4 == 0)
            {
                if (L1 < B1)
                {
                    v1 = lgPatte + L1 / 2.0;
                    v2 = lgPatte + L1 + B1 + L2 / 2.0;
                }
                else
                {
                    v1 = lgPatte + L1 + B1 / 2.0;
                    v2 = lgPatte + L1 + B1 + L2 + B2 / 2.0;
                }
                // ParamX1, v1, H1, 0, False
                ParameterStack stackPoignee = Poignee.Parameters;
                stackPoignee.SetDoubleParameter("", v1);
                stackPoignee.SetDoubleParameter("", H1);
                stackPoignee.SetDoubleParameter("", 0);
                stackPoignee.SetBoolParameter("", false);
                Poignee.CreateFactoryEntities(fTemp, stackPoignee, transform);

                //(ParamX1, v2, H1, 0, False)
                if (!caisse_en_2)
                    Poignee.CreateFactoryEntities(fTemp, stackPoignee, transform);
            }

            factory.AddEntities(fTemp, transform);
/*
            int hom = 0;
            if (Symy)
                hom = -1;
            else
                hom = 1;
            //ParamX1.Tranf2d Index(index1), Index(idxcour), Xo, Yo, Diro, hom
*/ 
        }
        #endregion
    }
}
