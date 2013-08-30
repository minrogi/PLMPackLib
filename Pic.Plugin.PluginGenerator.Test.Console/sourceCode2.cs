public ParameterStack BuildParameterStack(ParameterStack stackIn)
{
    ParameterStackUpdater paramUpdater = new ParameterStackUpdater(stackIn);
	paramUpdater.CreateDoubleParameter( "B", "Largeur intérieure", 300, 0);
	paramUpdater.CreateDoubleParameter( "Ep", "Epaisseur", 3, 0);
	paramUpdater.CreateDoubleParameter( "H", "Hauteur intérieure", 100, 0);
	paramUpdater.CreateDoubleParameter( "L", "Longueur intérieure", 400, 0);
	paramUpdater.CreateDoubleParameter( "M2", "Tenon", 25, 0);
	paramUpdater.CreateDoubleParameter( "P", "Patte Rentrante", 30, 0);
	paramUpdater.CreateDoubleParameter( "Rc", "Rc", 10, 0);

    return paramUpdater.UpdatedStack;
}            

public void CreateFactoryEntities(PicFactory factory, ParameterStack stack, Transform2D transform)
{
	if (factory != null)
	{

		// free variables
		double B = stack.GetDoubleParameterValue("B");
		double Ep = stack.GetDoubleParameterValue("Ep");
		double H = stack.GetDoubleParameterValue("H");
		double L = stack.GetDoubleParameterValue("L");
		double M2 = stack.GetDoubleParameterValue("M2");
		double P = stack.GetDoubleParameterValue("P");
		double Rc = stack.GetDoubleParameterValue("Rc");

		// formulas
		double Ec=0.5;
		double PP=2*Ep/3;
		double GE=Ep-PP;
		double E1=4*PP-2*Ec;
		double E2=2*Ep;
		double L1=L+2*PP+2*Ep;
		double L2=L+2*(2*Ep+PP);
		double L3=L2+2*GE;
		double L4=L;
		double H1=H+PP;
		double H2=H+2*PP+Ep;
		double H3=H-GE;
		double H4=H+PP-GE;
		double H5=H-Ep-2*Ec;
		double v3=(H1-Ep+Ec-H5);
		double B1=B+2*PP+Ep;
		double B2=B+2*PP;
		double B3=B1+2*v3;
		double B4=B;
		double D1=(B+2*PP+Ep)/2;
		double M1=2*Ep+PP;
		double T1=Ep+1;
		double R1=Ep-Ec;
		double v1=(L2-L1)/2;
		double v2=(L3-L4)/2;
		double v4=(B3-B4)/2;
		double v5=(B3-2*M2)/4;
		double v6=(L3-L1)/2;
		double R2=v4;

		SortedList<uint, PicEntity> entities = new SortedList<uint, PicEntity>();

		// segments
		double  x0 = 0.0, y0 = 0.0, x1 = 0.0, y1 = 0.0;

		// 3 : (90.7923, -352) <-> (3, -352)
		x0 = R1+H5-v4;
		y0 = -422+T1+H3+E2+H4+v1-D1;
		x1 = R1;
		y1 = -422+T1+H3+E2+H4+v1-D1;
		entities.Add(3, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 4 : (96.25, -314) <-> (92.7923, -314)
		x0 = R1+H5;
		y0 = -422+T1+H3+E2;
		x1 = R1+H5-v4+2;
		y1 = -422+T1+H3+E2;
		entities.Add(4, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 5 : (98, -206) <-> (96.25, -210)
		x0 = H1;
		y0 = -422+T1+H3+E2+H4+v1;
		x1 = R1+H5;
		y1 = -422+T1+H3+E2+H4;
		entities.Add(5, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 2,
			x0, y0, x1, y1 ) );

		// 6 : (402, -206) <-> (500, -206)
		x0 = H1+B1;
		y0 = -422+T1+H3+E2+H4+v1;
		x1 = R1+H5+B3+H5;
		y1 = -422+T1+H3+E2+H4+v1;
		entities.Add(6, factory.AddSegment(PicGraphics.LT.LT_CREASING, 1, 1,
			x0, y0, x1, y1 ) );

		// 7 : (402, -206) <-> (403.75, -210)
		x0 = H1+B1;
		y0 = -422+T1+H3+E2+H4+v1;
		x1 = R1+H5+B3;
		y1 = -422+T1+H3+E2+H4;
		entities.Add(7, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 2,
			x0, y0, x1, y1 ) );

		// 8 : (500, -206) <-> (500, -352)
		x0 = R1+H5+B3+H5;
		y0 = -422+T1+H3+E2+H4+v1;
		x1 = R1+H5+B3+H5;
		y1 = -422+T1+H3+E2+H4+v1-D1;
		entities.Add(8, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 9 : (409.208, -352) <-> (500, -352)
		x0 = R1+H5+B3+v4;
		y0 = -422+T1+H3+E2+H4+v1-D1;
		x1 = R1+H5+B3+H5;
		y1 = -422+T1+H3+E2+H4+v1-D1;
		entities.Add(9, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 10 : (403.75, -314) <-> (407.208, -314)
		x0 = R1+H5+B3;
		y0 = -422+T1+H3+E2;
		x1 = R1+H5+B3+v4-2;
		y1 = -422+T1+H3+E2;
		entities.Add(10, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 11 : (506, -212) <-> (500, -206)
		x0 = H1+B1+H2;
		y0 = -422+T1+H3+E2+H4+v1-v6;
		x1 = R1+H5+B3+H5;
		y1 = -422+T1+H3+E2+H4+v1;
		entities.Add(11, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 12 : (96.25, -210) <-> (96.25, -314)
		x0 = R1+H5;
		y0 = -422+T1+H3+E2+H4;
		x1 = R1+H5;
		y1 = -422+T1+H3+E2;
		entities.Add(12, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 13 : (100.5, -320) <-> (96.25, -314)
		x0 = R1+H5+v4;
		y0 = -422+T1+H3;
		x1 = R1+H5;
		y1 = -422+T1+H3+E2;
		entities.Add(13, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 14 : (100.5, -417) <-> (100.5, -320)
		x0 = R1+H5+v4;
		y0 = -422+T1;
		x1 = R1+H5+v4;
		y1 = -422+T1+H3;
		entities.Add(14, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 15 : (403.75, -210) <-> (403.75, -314)
		x0 = R1+H5+B3;
		y0 = -422+T1+H3+E2+H4;
		x1 = R1+H5+B3;
		y1 = -422+T1+H3+E2;
		entities.Add(15, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 16 : (399.5, -320) <-> (403.75, -314)
		x0 = R1+H5+v4+B4;
		y0 = -422+T1+H3;
		x1 = R1+H5+B3;
		y1 = -422+T1+H3+E2;
		entities.Add(16, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 17 : (399.5, -417) <-> (399.5, -320)
		x0 = R1+H5+v4+B4;
		y0 = -422+T1;
		x1 = R1+H5+v4+B4;
		y1 = -422+T1+H3;
		entities.Add(17, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 18 : (403.75, -314) <-> (96.25, -314)
		x0 = R1+H5+B3;
		y0 = -422+T1+H3+E2;
		x1 = R1+H5;
		y1 = -422+T1+H3+E2;
		entities.Add(18, factory.AddSegment(PicGraphics.LT.LT_CREASING, 1, 1,
			x0, y0, x1, y1 ) );

		// 19 : (399.5, -320) <-> (100.5, -320)
		x0 = R1+H5+v4+B4;
		y0 = -422+T1+H3;
		x1 = R1+H5+v4;
		y1 = -422+T1+H3;
		entities.Add(19, factory.AddSegment(PicGraphics.LT.LT_CREASING, 1, 1,
			x0, y0, x1, y1 ) );

		// 20 : (177.75, -417) <-> (176.41, -422)
		x0 = R1+H5+v5+M2-1.50002;
		y0 = -422+T1;
		x1 = R1+H5+v5+M2-2.83977;
		y1 = -422;
		entities.Add(20, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 21 : (149.09, -422) <-> (176.41, -422)
		x0 = R1+H5+v5+2.83975;
		y0 = -422;
		x1 = R1+H5+v5+M2-2.83975;
		y1 = -422;
		entities.Add(21, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 22 : (147.75, -417) <-> (149.09, -422)
		x0 = R1+H5+v5+1.5;
		y0 = -422+T1;
		x1 = R1+H5+v5+2.83975;
		y1 = -422;
		entities.Add(22, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 23 : (146.25, -210) <-> (179.25, -210)
		x0 = R1+H5+v5;
		y0 = -422+T1+H3+E2+H4;
		x1 = R1+H5+v5+M2;
		y1 = -422+T1+H3+E2+H4;
		entities.Add(23, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 24 : (179.25, -210) <-> (179.25, -204)
		x0 = R1+H5+v5+M2;
		y0 = -422+T1+H3+E2+H4;
		x1 = R1+H5+v5+M2;
		y1 = -422+T1+H3+E2+H4+M1;
		entities.Add(24, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 25 : (146.25, -204) <-> (179.25, -204)
		x0 = R1+H5+v5;
		y0 = -422+T1+H3+E2+H4+M1;
		x1 = R1+H5+v5+M2;
		y1 = -422+T1+H3+E2+H4+M1;
		entities.Add(25, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 26 : (146.25, -210) <-> (146.25, -204)
		x0 = R1+H5+v5;
		y0 = -422+T1+H3+E2+H4;
		x1 = R1+H5+v5;
		y1 = -422+T1+H3+E2+H4+M1;
		entities.Add(26, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 27 : (147.75, -417) <-> (100.5, -417)
		x0 = R1+H5+v5+1.5;
		y0 = -422+T1;
		x1 = R1+H5+v4;
		y1 = -422+T1;
		entities.Add(27, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 28 : (146.25, -210) <-> (96.25, -210)
		x0 = R1+H5+v5;
		y0 = -422+T1+H3+E2+H4;
		x1 = R1+H5;
		y1 = -422+T1+H3+E2+H4;
		entities.Add(28, factory.AddSegment(PicGraphics.LT.LT_CREASING, 1, 1,
			x0, y0, x1, y1 ) );

		// 29 : (322.25, -417) <-> (323.59, -422)
		x0 = R1+H5+v5+M2+2*v5+1.5;
		y0 = -422+T1;
		x1 = R1+H5+v5+M2+2*v5+2.83972;
		y1 = -422;
		entities.Add(29, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 30 : (350.91, -422) <-> (323.59, -422)
		x0 = R1+H5+v5+M2+2*v5+M2-2.83972;
		y0 = -422;
		x1 = R1+H5+v5+M2+2*v5+2.83972;
		y1 = -422;
		entities.Add(30, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 31 : (352.25, -417) <-> (350.91, -422)
		x0 = R1+H5+v5+M2+2*v5+M2-1.5;
		y0 = -422+T1;
		x1 = R1+H5+v5+M2+2*v5+M2-2.83972;
		y1 = -422;
		entities.Add(31, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 32 : (353.75, -210) <-> (320.75, -210)
		x0 = R1+H5+v5+M2+2*v5+M2;
		y0 = -422+T1+H3+E2+H4;
		x1 = R1+H5+v5+M2+2*v5;
		y1 = -422+T1+H3+E2+H4;
		entities.Add(32, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 33 : (320.75, -210) <-> (320.75, -204)
		x0 = R1+H5+v5+M2+2*v5;
		y0 = -422+T1+H3+E2+H4;
		x1 = R1+H5+v5+M2+2*v5;
		y1 = -422+T1+H3+E2+H4+M1;
		entities.Add(33, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 34 : (353.75, -204) <-> (320.75, -204)
		x0 = R1+H5+v5+M2+2*v5+M2;
		y0 = -422+T1+H3+E2+H4+M1;
		x1 = R1+H5+v5+M2+2*v5;
		y1 = -422+T1+H3+E2+H4+M1;
		entities.Add(34, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 35 : (353.75, -210) <-> (353.75, -204)
		x0 = R1+H5+v5+M2+2*v5+M2;
		y0 = -422+T1+H3+E2+H4;
		x1 = R1+H5+v5+M2+2*v5+M2;
		y1 = -422+T1+H3+E2+H4+M1;
		entities.Add(35, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 36 : (352.25, -417) <-> (399.5, -417)
		x0 = R1+H5+v5+M2+2*v5+M2-1.5;
		y0 = -422+T1;
		x1 = R1+H5+v4+B4;
		y1 = -422+T1;
		entities.Add(36, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 37 : (353.75, -210) <-> (403.75, -210)
		x0 = R1+H5+v5+M2+2*v5+M2;
		y0 = -422+T1+H3+E2+H4;
		x1 = R1+H5+B3;
		y1 = -422+T1+H3+E2+H4;
		entities.Add(37, factory.AddSegment(PicGraphics.LT.LT_CREASING, 1, 1,
			x0, y0, x1, y1 ) );

		// 38 : (322.25, -417) <-> (177.75, -417)
		x0 = R1+H5+v5+M2+2*v5+1.5;
		y0 = -422+T1;
		x1 = R1+H5+v5+M2-1.5;
		y1 = -422+T1;
		entities.Add(38, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 39 : (320.75, -210) <-> (179.25, -210)
		x0 = R1+H5+v5+M2+2*v5;
		y0 = -422+T1+H3+E2+H4;
		x1 = R1+H5+v5+M2;
		y1 = -422+T1+H3+E2+H4;
		entities.Add(39, factory.AddSegment(PicGraphics.LT.LT_CREASING, 1, 1,
			x0, y0, x1, y1 ) );

		// 40 : (98, 206) <-> (98, -206)
		x0 = H1;
		y0 = -422+T1+H3+E2+H4+v1+L1;
		x1 = H1;
		y1 = -422+T1+H3+E2+H4+v1;
		entities.Add(40, factory.AddSegment(PicGraphics.LT.LT_CREASING, 1, 1,
			x0, y0, x1, y1 ) );

		// 41 : (402, 206) <-> (402, -206)
		x0 = H1+B1;
		y0 = -422+T1+H3+E2+H4+v1+L1;
		x1 = H1+B1;
		y1 = -422+T1+H3+E2+H4+v1;
		entities.Add(41, factory.AddSegment(PicGraphics.LT.LT_CREASING, 1, 1,
			x0, y0, x1, y1 ) );

		// 42 : (506, 212) <-> (506, -212)
		x0 = H1+B1+H2;
		y0 = -422+T1+H3+E2+H4+v1-v6+L3;
		x1 = H1+B1+H2;
		y1 = -422+T1+H3+E2+H4+v1-v6;
		entities.Add(42, factory.AddSegment(PicGraphics.LT.LT_CREASING, 1, 1,
			x0, y0, x1, y1 ) );

		// 43 : (806, -212) <-> (506, -212)
		x0 = H1+B1+H2+B2;
		y0 = -422+T1+H3+E2+H4+v1-v6;
		x1 = H1+B1+H2;
		y1 = -422+T1+H3+E2+H4+v1-v6;
		entities.Add(43, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 44 : (806, -203) <-> (806, -212)
		x0 = H1+B1+H2+B2;
		y0 = -422+T1+H3+E2+H4+v1-v6+v2;
		x1 = H1+B1+H2+B2;
		y1 = -422+T1+H3+E2+H4+v1-v6;
		entities.Add(44, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 45 : (806, -203) <-> (826, -203)
		x0 = H1+B1+H2+B2;
		y0 = -422+T1+H3+E2+H4+v1-v6+v2;
		x1 = H1+B1+H2+B2+P-10;
		y1 = -422+T1+H3+E2+H4+v1-v6+v2;
		entities.Add(45, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 46 : (836, 193) <-> (836, -193)
		x0 = H1+B1+H2+B2+P;
		y0 = -422+T1+H3+E2+H4+v1-v6+v2+L4-10;
		x1 = H1+B1+H2+B2+P;
		y1 = -422+T1+H3+E2+H4+v1-v6+v2+10;
		entities.Add(46, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 48 : (806, -203) <-> (806, 203)
		x0 = H1+B1+H2+B2;
		y0 = -422+T1+H3+E2+H4+v1-v6+v2;
		x1 = H1+B1+H2+B2;
		y1 = -422+T1+H3+E2+H4+v1-v6+v2+L4;
		entities.Add(48, factory.AddSegment(PicGraphics.LT.LT_CREASING, 1, 1,
			x0, y0, x1, y1 ) );

		// 49 : (90.7923, 352) <-> (3, 352)
		x0 = R1+H5-v4;
		y0 = -422+T1+H3+E2+H4+v1+L1+D1;
		x1 = R1;
		y1 = -422+T1+H3+E2+H4+v1+L1+D1;
		entities.Add(49, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 50 : (96.25, 314) <-> (92.7923, 314)
		x0 = R1+H5;
		y0 = -422+T1+H3+E2+H4+L2+H4;
		x1 = R1+H5-v4+2;
		y1 = -422+T1+H3+E2+H4+L2+H4;
		entities.Add(50, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 51 : (96.25, 210) <-> (98, 206)
		x0 = R1+H5;
		y0 = -422+T1+H3+E2+H4+L2;
		x1 = H1;
		y1 = -422+T1+H3+E2+H4+v1+L1;
		entities.Add(51, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 2,
			x0, y0, x1, y1 ) );

		// 52 : (500, 206) <-> (402, 206)
		x0 = R1+H5+B3+H5;
		y0 = -422+T1+H3+E2+H4+v1+L1;
		x1 = H1+B1;
		y1 = -422+T1+H3+E2+H4+v1+L1;
		entities.Add(52, factory.AddSegment(PicGraphics.LT.LT_CREASING, 1, 1,
			x0, y0, x1, y1 ) );

		// 53 : (403.75, 210) <-> (402, 206)
		x0 = R1+H5+B3;
		y0 = -422+T1+H3+E2+H4+L2;
		x1 = H1+B1;
		y1 = -422+T1+H3+E2+H4+v1+L1;
		entities.Add(53, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 2,
			x0, y0, x1, y1 ) );

		// 54 : (500, 206) <-> (500, 352)
		x0 = R1+H5+B3+H5;
		y0 = -422+T1+H3+E2+H4+v1+L1;
		x1 = R1+H5+B3+H5;
		y1 = -422+T1+H3+E2+H4+v1+L1+D1;
		entities.Add(54, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 55 : (409.208, 352) <-> (500, 352)
		x0 = R1+H5+B3+v4;
		y0 = -422+T1+H3+E2+H4+v1+L1+D1;
		x1 = R1+H5+B3+H5;
		y1 = -422+T1+H3+E2+H4+v1+L1+D1;
		entities.Add(55, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 56 : (403.75, 314) <-> (407.208, 314)
		x0 = R1+H5+B3;
		y0 = -422+T1+H3+E2+H4+L2+H4;
		x1 = R1+H5+B3+v4-2;
		y1 = -422+T1+H3+E2+H4+L2+H4;
		entities.Add(56, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 57 : (506, 212) <-> (500, 206)
		x0 = H1+B1+H2;
		y0 = -422+T1+H3+E2+H4+v1-v6+L3;
		x1 = R1+H5+B3+H5;
		y1 = -422+T1+H3+E2+H4+v1+L1;
		entities.Add(57, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 58 : (96.25, 210) <-> (96.25, 314)
		x0 = R1+H5;
		y0 = -422+T1+H3+E2+H4+L2;
		x1 = R1+H5;
		y1 = -422+T1+H3+E2+H4+L2+H4;
		entities.Add(58, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 59 : (100.5, 320) <-> (96.25, 314)
		x0 = R1+H5+v4;
		y0 = -422+T1+H3+E2+H4+L2+H4+E2;
		x1 = R1+H5;
		y1 = -422+T1+H3+E2+H4+L2+H4;
		entities.Add(59, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 60 : (100.5, 417) <-> (100.5, 320)
		x0 = R1+H5+v4;
		y0 = -422+T1+H3+E2+H4+L2+H4+E2+H3;
		x1 = R1+H5+v4;
		y1 = -422+T1+H3+E2+H4+L2+H4+E2;
		entities.Add(60, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 61 : (403.75, 210) <-> (403.75, 314)
		x0 = R1+H5+B3;
		y0 = -422+T1+H3+E2+H4+L2;
		x1 = R1+H5+B3;
		y1 = -422+T1+H3+E2+H4+L2+H4;
		entities.Add(61, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 62 : (399.5, 320) <-> (403.75, 314)
		x0 = R1+H5+v4+B4;
		y0 = -422+T1+H3+E2+H4+L2+H4+E2;
		x1 = R1+H5+B3;
		y1 = -422+T1+H3+E2+H4+L2+H4;
		entities.Add(62, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 63 : (399.5, 417) <-> (399.5, 320)
		x0 = R1+H5+v4+B4;
		y0 = -422+T1+H3+E2+H4+L2+H4+E2+H3;
		x1 = R1+H5+v4+B4;
		y1 = -422+T1+H3+E2+H4+L2+H4+E2;
		entities.Add(63, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 64 : (403.75, 314) <-> (96.25, 314)
		x0 = R1+H5+B3;
		y0 = -422+T1+H3+E2+H4+L2+H4;
		x1 = R1+H5;
		y1 = -422+T1+H3+E2+H4+L2+H4;
		entities.Add(64, factory.AddSegment(PicGraphics.LT.LT_CREASING, 1, 1,
			x0, y0, x1, y1 ) );

		// 65 : (399.5, 320) <-> (100.5, 320)
		x0 = R1+H5+v4+B4;
		y0 = -422+T1+H3+E2+H4+L2+H4+E2;
		x1 = R1+H5+v4;
		y1 = -422+T1+H3+E2+H4+L2+H4+E2;
		entities.Add(65, factory.AddSegment(PicGraphics.LT.LT_CREASING, 1, 1,
			x0, y0, x1, y1 ) );

		// 66 : (177.75, 417) <-> (176.41, 422)
		x0 = R1+H5+v5+M2-1.50002;
		y0 = -422+T1+H3+E2+H4+L2+H4+E2+H3;
		x1 = R1+H5+v5+M2-2.83977;
		y1 = -422+T1+H3+E2+H4+L2+H4+E2+H3+T1;
		entities.Add(66, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 67 : (149.09, 422) <-> (176.41, 422)
		x0 = R1+H5+v5+2.83975;
		y0 = -422+T1+H3+E2+H4+L2+H4+E2+H3+T1;
		x1 = R1+H5+v5+M2-2.83975;
		y1 = -422+T1+H3+E2+H4+L2+H4+E2+H3+T1;
		entities.Add(67, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 68 : (147.75, 417) <-> (149.09, 422)
		x0 = R1+H5+v5+1.5;
		y0 = -422+T1+H3+E2+H4+L2+H4+E2+H3;
		x1 = R1+H5+v5+2.83975;
		y1 = -422+T1+H3+E2+H4+L2+H4+E2+H3+T1;
		entities.Add(68, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 69 : (146.25, 210) <-> (179.25, 210)
		x0 = R1+H5+v5;
		y0 = -422+T1+H3+E2+H4+L2;
		x1 = R1+H5+v5+M2;
		y1 = -422+T1+H3+E2+H4+L2;
		entities.Add(69, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 70 : (179.25, 210) <-> (179.25, 204)
		x0 = R1+H5+v5+M2;
		y0 = -422+T1+H3+E2+H4+L2;
		x1 = R1+H5+v5+M2;
		y1 = -422+T1+H3+E2+H4+L2-M1;
		entities.Add(70, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 71 : (146.25, 204) <-> (179.25, 204)
		x0 = R1+H5+v5;
		y0 = -422+T1+H3+E2+H4+L2-M1;
		x1 = R1+H5+v5+M2;
		y1 = -422+T1+H3+E2+H4+L2-M1;
		entities.Add(71, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 72 : (146.25, 210) <-> (146.25, 204)
		x0 = R1+H5+v5;
		y0 = -422+T1+H3+E2+H4+L2;
		x1 = R1+H5+v5;
		y1 = -422+T1+H3+E2+H4+L2-M1;
		entities.Add(72, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 73 : (147.75, 417) <-> (100.5, 417)
		x0 = R1+H5+v5+1.5;
		y0 = -422+T1+H3+E2+H4+L2+H4+E2+H3;
		x1 = R1+H5+v4;
		y1 = -422+T1+H3+E2+H4+L2+H4+E2+H3;
		entities.Add(73, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 74 : (146.25, 210) <-> (96.25, 210)
		x0 = R1+H5+v5;
		y0 = -422+T1+H3+E2+H4+L2;
		x1 = R1+H5;
		y1 = -422+T1+H3+E2+H4+L2;
		entities.Add(74, factory.AddSegment(PicGraphics.LT.LT_CREASING, 1, 1,
			x0, y0, x1, y1 ) );

		// 75 : (322.25, 417) <-> (323.59, 422)
		x0 = R1+H5+v5+M2+2*v5+1.5;
		y0 = -422+T1+H3+E2+H4+L2+H4+E2+H3;
		x1 = R1+H5+v5+M2+2*v5+2.83972;
		y1 = -422+T1+H3+E2+H4+L2+H4+E2+H3+T1;
		entities.Add(75, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 76 : (350.91, 422) <-> (323.59, 422)
		x0 = R1+H5+v5+M2+2*v5+M2-2.83972;
		y0 = -422+T1+H3+E2+H4+L2+H4+E2+H3+T1;
		x1 = R1+H5+v5+M2+2*v5+2.83972;
		y1 = -422+T1+H3+E2+H4+L2+H4+E2+H3+T1;
		entities.Add(76, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 77 : (352.25, 417) <-> (350.91, 422)
		x0 = R1+H5+v5+M2+2*v5+M2-1.5;
		y0 = -422+T1+H3+E2+H4+L2+H4+E2+H3;
		x1 = R1+H5+v5+M2+2*v5+M2-2.83972;
		y1 = -422+T1+H3+E2+H4+L2+H4+E2+H3+T1;
		entities.Add(77, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 78 : (353.75, 210) <-> (320.75, 210)
		x0 = R1+H5+v5+M2+2*v5+M2;
		y0 = -422+T1+H3+E2+H4+L2;
		x1 = R1+H5+v5+M2+2*v5;
		y1 = -422+T1+H3+E2+H4+L2;
		entities.Add(78, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 79 : (320.75, 210) <-> (320.75, 204)
		x0 = R1+H5+v5+M2+2*v5;
		y0 = -422+T1+H3+E2+H4+L2;
		x1 = R1+H5+v5+M2+2*v5;
		y1 = -422+T1+H3+E2+H4+L2-M1;
		entities.Add(79, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 80 : (353.75, 204) <-> (320.75, 204)
		x0 = R1+H5+v5+M2+2*v5+M2;
		y0 = -422+T1+H3+E2+H4+L2-M1;
		x1 = R1+H5+v5+M2+2*v5;
		y1 = -422+T1+H3+E2+H4+L2-M1;
		entities.Add(80, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 81 : (353.75, 210) <-> (353.75, 204)
		x0 = R1+H5+v5+M2+2*v5+M2;
		y0 = -422+T1+H3+E2+H4+L2;
		x1 = R1+H5+v5+M2+2*v5+M2;
		y1 = -422+T1+H3+E2+H4+L2-M1;
		entities.Add(81, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 82 : (352.25, 417) <-> (399.5, 417)
		x0 = R1+H5+v5+M2+2*v5+M2-1.5;
		y0 = -422+T1+H3+E2+H4+L2+H4+E2+H3;
		x1 = R1+H5+v4+B4;
		y1 = -422+T1+H3+E2+H4+L2+H4+E2+H3;
		entities.Add(82, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 83 : (353.75, 210) <-> (403.75, 210)
		x0 = R1+H5+v5+M2+2*v5+M2;
		y0 = -422+T1+H3+E2+H4+L2;
		x1 = R1+H5+B3;
		y1 = -422+T1+H3+E2+H4+L2;
		entities.Add(83, factory.AddSegment(PicGraphics.LT.LT_CREASING, 1, 1,
			x0, y0, x1, y1 ) );

		// 84 : (322.25, 417) <-> (177.75, 417)
		x0 = R1+H5+v5+M2+2*v5+1.5;
		y0 = -422+T1+H3+E2+H4+L2+H4+E2+H3;
		x1 = R1+H5+v5+M2-1.5;
		y1 = -422+T1+H3+E2+H4+L2+H4+E2+H3;
		entities.Add(84, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 85 : (320.75, 210) <-> (179.25, 210)
		x0 = R1+H5+v5+M2+2*v5;
		y0 = -422+T1+H3+E2+H4+L2;
		x1 = R1+H5+v5+M2;
		y1 = -422+T1+H3+E2+H4+L2;
		entities.Add(85, factory.AddSegment(PicGraphics.LT.LT_CREASING, 1, 1,
			x0, y0, x1, y1 ) );

		// 86 : (806, 212) <-> (506, 212)
		x0 = H1+B1+H2+B2;
		y0 = -422+T1+H3+E2+H4+v1-v6+L3;
		x1 = H1+B1+H2;
		y1 = -422+T1+H3+E2+H4+v1-v6+L3;
		entities.Add(86, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 87 : (806, 203) <-> (806, 212)
		x0 = H1+B1+H2+B2;
		y0 = -422+T1+H3+E2+H4+v1-v6+v2+L4;
		x1 = H1+B1+H2+B2;
		y1 = -422+T1+H3+E2+H4+v1-v6+L3;
		entities.Add(87, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 88 : (826, 203) <-> (806, 203)
		x0 = H1+B1+H2+B2+P-10;
		y0 = -422+T1+H3+E2+H4+v1-v6+v2+L4;
		x1 = H1+B1+H2+B2;
		y1 = -422+T1+H3+E2+H4+v1-v6+v2+L4;
		entities.Add(88, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 114 : (90.7923, -352) <-> (90.7923, -316)
		x0 = R1+H5-v4;
		y0 = -422+T1+H3+E2+H4+v1-D1;
		x1 = R1+H5-v4;
		y1 = -422+T1+H3+E2-2;
		entities.Add(114, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 115 : (409.208, -352) <-> (409.208, -316)
		x0 = R1+H5+B3+v4;
		y0 = -422+T1+H3+E2+H4+v1-D1;
		x1 = R1+H5+B3+v4;
		y1 = -422+T1+H3+E2-2;
		entities.Add(115, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 120 : (90.7923, 352) <-> (90.7923, 316)
		x0 = R1+H5-v4;
		y0 = -422+T1+H3+E2+H4+v1+L1+D1;
		x1 = R1+H5-v4;
		y1 = -422+T1+H3+E2+H4+L2+H4+2;
		entities.Add(120, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 121 : (409.208, 352) <-> (409.208, 316)
		x0 = R1+H5+B3+v4;
		y0 = -422+T1+H3+E2+H4+v1+L1+D1;
		x1 = R1+H5+B3+v4;
		y1 = -422+T1+H3+E2+H4+L2+H4+2;
		entities.Add(121, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 134 : (9.28455e-008, 10) <-> (2.68245e-007, 206)
		x0 = 0.0;
		y0 = -422+T1+H3+E2+H4+v1+L1/2+Rc;
		x1 = 0.0;
		y1 = -422+T1+H3+E2+H4+v1+L1;
		entities.Add(134, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 135 : (3, 352) <-> (3, 206)
		x0 = R1;
		y0 = -422+T1+H3+E2+H4+v1+L1+D1;
		x1 = R1;
		y1 = -422+T1+H3+E2+H4+v1+L1;
		entities.Add(135, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 136 : (3, 206) <-> (0, 206)
		x0 = R1;
		y0 = -422+T1+H3+E2+H4+v1+L1;
		x1 = 0.0;
		y1 = -422+T1+H3+E2+H4+v1+L1;
		entities.Add(136, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 137 : (98, 206) <-> (3, 206)
		x0 = H1;
		y0 = -422+T1+H3+E2+H4+v1+L1;
		x1 = R1;
		y1 = -422+T1+H3+E2+H4+v1+L1;
		entities.Add(137, factory.AddSegment(PicGraphics.LT.LT_CREASING, 1, 1,
			x0, y0, x1, y1 ) );

		// 138 : (3, -352) <-> (3, -206)
		x0 = R1;
		y0 = -422+T1+H3+E2+H4+v1-D1;
		x1 = R1;
		y1 = -422+T1+H3+E2+H4+v1;
		entities.Add(138, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 139 : (2.28199e-007, -206) <-> (4.03598e-007, -10)
		x0 = 0.0;
		y0 = -422+T1+H3+E2+H4+v1;
		x1 = 0.0;
		y1 = -422+T1+H3+E2+H4+v1+L1/2-Rc;
		entities.Add(139, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 140 : (3, -206) <-> (0, -206)
		x0 = R1;
		y0 = -422+T1+H3+E2+H4+v1;
		x1 = 0.0;
		y1 = -422+T1+H3+E2+H4+v1;
		entities.Add(140, factory.AddSegment(PicGraphics.LT.LT_CUT, 1, 1,
			x0, y0, x1, y1 ) );

		// 141 : (98, -206) <-> (3, -206)
		x0 = H1;
		y0 = -422+T1+H3+E2+H4+v1;
		x1 = R1;
		y1 = -422+T1+H3+E2+H4+v1;
		entities.Add(141, factory.AddSegment(PicGraphics.LT.LT_CREASING, 1, 1,
			x0, y0, x1, y1 ) );

		// arcs
		double radius = 0.0;

		// 47 : radius = 10  s0 = 45  s1 = 46
		factory.ProcessTool( new PicToolRound(
			  entities[45]
			, entities[46]
			, 10						// radius
			));
		// 89 : radius = 10  s0 = 46  s1 = 88
		factory.ProcessTool( new PicToolRound(
			  entities[46]
			, entities[88]
			, 10						// radius
			));
		// 90: Pt0 = ( -2.68469e-008, -10) Pt1 = ( 4.47448e-008, 10)
		entities.Add(90, factory.AddArc(PicGraphics.LT.LT_CUT, 1, 1
			, 0.0						// xc
			, -422+T1+H3+E2+H4+v1+L1/2			// yc
			, 0.5 * Math.Sqrt( ( (0.0) - (0.0) ) * ( (0.0) - (0.0) ) + ( (-422+T1+H3+E2+H4+v1+L1/2+Rc) - (-422+T1+H3+E2+H4+v1+L1/2-Rc) ) * ( (-422+T1+H3+E2+H4+v1+L1/2+Rc) - (-422+T1+H3+E2+H4+v1+L1/2-Rc) ) )		// radius
			, 270
			, 450
			));
		// 149 : radius = 2  s0 = 56  s1 = 121
		factory.ProcessTool( new PicToolRound(
			  entities[56]
			, entities[121]
			, R2						// radius
			));
		// 150 : radius = 2  s0 = 50  s1 = 120
		factory.ProcessTool( new PicToolRound(
			  entities[50]
			, entities[120]
			, R2						// radius
			));
		// 151 : radius = 2  s0 = 4  s1 = 114
		factory.ProcessTool( new PicToolRound(
			  entities[4]
			, entities[114]
			, R2						// radius
			));
		// 152 : radius = 2  s0 = 10  s1 = 115
		factory.ProcessTool( new PicToolRound(
			  entities[10]
			, entities[115]
			, R2						// radius
			));
		// cotations
		double offset = 0.0;

		// 102: Pt0 = ( 806, -71.4252) Pt1 = ( 836, -71.4252) offset = 0
		x0 = H1+B1+H2+B2;
		y0 = -422+T1+H3+E2+H4+v1+L1/2-Rc-61.4252;
		x1 = H1+B1+H2+B2+P;
		y1 = -422+T1+H3+E2+H4+v1+L1/2-Rc-61.4252;
		offset = 0;
		entities.Add(102, factory.AddCotation(PicCotation.CotType.COT_HORIZONTAL,
			1, 1, x0, y0, x1, y1, offset, "P"));

		// 126: Pt0 = ( 146.25, 207.263) Pt1 = ( 179.25, 207.263) offset = -17.1615
		x0 = R1+H5+v5;
		y0 = -422+T1+H3+E2+H4+v1+L1+1.26273;
		x1 = R1+H5+v5+M2;
		y1 = -422+T1+H3+E2+H4+v1+L1+1.26273;
		offset = -17.1615;
		entities.Add(126, factory.AddCotation(PicCotation.CotType.COT_HORIZONTAL,
			1, 1, x0, y0, x1, y1, offset, "M2"));

		// 127: Pt0 = ( 320.75, 206.394) Pt1 = ( 353.75, 206.394) offset = -12.9171
		x0 = R1+H5+v5+M2+2*v5;
		y0 = -422+T1+H3+E2+H4+v1+L1;
		x1 = R1+H5+v5+M2+2*v5+M2;
		y1 = -422+T1+H3+E2+H4+v1+L1;
		offset = -12.9171;
		entities.Add(127, factory.AddCotation(PicCotation.CotType.COT_HORIZONTAL,
			1, 1, x0, y0, x1, y1, offset, "M2"));

		// 132: Pt0 = ( 2.0963e-007, 0) Pt1 = ( 1.82783e-007, -10) offset = -51.7976
		x0 = 0.0;
		y0 = -422+T1+H3+E2+H4+v1+L1/2;
		x1 = 0.0;
		y1 = -422+T1+H3+E2+H4+v1+L1/2-Rc;
		offset = -51.7976;
		entities.Add(132, factory.AddCotation(PicCotation.CotType.COT_VERTICAL,
			1, 1, x0, y0, x1, y1, offset, "Rc"));

		// 133: Pt0 = ( 4.75414e-008, 0) Pt1 = ( 5.64904e-008, 10) offset = -52.1767
		x0 = 0.0;
		y0 = -422+T1+H3+E2+H4+v1+L1/2;
		x1 = 0.0;
		y1 = -422+T1+H3+E2+H4+v1+L1/2+Rc;
		offset = -52.1767;
		entities.Add(133, factory.AddCotation(PicCotation.CotType.COT_VERTICAL,
			1, 1, x0, y0, x1, y1, offset, "Rc"));
	}
}
public double ImpositionOffsetX(ParameterStack stack) { return 0.0; }
public double ImpositionOffsetY(ParameterStack stack) { return 0.0; }

/// <summary>
/// Is supporting palletization ?
/// </summary>
public bool IsSupportingPalletization { get { return true; } }
/// <summary>
/// Outer dimensions
/// Method should only be called if component supports palletization
/// </summary>
public void OuterDimensions(ParameterStack stack, out double[] dimensions)
{
    dimensions = new double[3];
    double Ep = stack.GetDoubleParameterValue("Ep");
    dimensions[0] = stack.GetDoubleParameterValue("L") + 2.0 * Ep;
    dimensions[1] = stack.GetDoubleParameterValue("B") + 2.0 * Ep;
    dimensions[2] = stack.GetDoubleParameterValue("C") + 2.0 * Ep;
}
/// <summary>
/// Returns case type to be used for ECT computation 
/// </summary>
public int CaseType { get { return 0; } }
/// <summary>
/// Is supporting automatic folding
/// </summary>
public bool IsSupportingAutomaticFolding { get { return false; } }
/// <summary>
/// Reference point that defines anchored face
/// </summary>
public List<Vector2D> ReferencePoints(ParameterStack stack)
{
    List<Vector2D> ltPoints = new List<Vector2D>();
    return ltPoints;
}