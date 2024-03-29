public ParameterStack BuildParameterStack(ParameterStack stackIn)
{
    ParameterStackUpdater paramUpdater = new ParameterStackUpdater(stackIn);
	paramUpdater.CreateDoubleParameter( "B", "B", 400, 0);
	paramUpdater.CreateDoubleParameter( "H", "H", 300, 0);
	paramUpdater.CreateDoubleParameter( "L", "L", 500, 0);
	paramUpdater.CreateDoubleParameter( "ep", "ep", 5, 0);
	paramUpdater.CreateDoubleParameter( "hpr", "hpr", 50, 0);
	paramUpdater.CreateDoubleParameter( "hr", "hr", 50, 0);
	paramUpdater.CreateDoubleParameter( "lp", "lp", 50, 0);
	return paramUpdater.UpdatedStack;
}

public void CreateFactoryEntities(PicFactory factory, ParameterStack stack, Transform2D transform)
{
	PicFactory fTemp = new PicFactory();
	const PicGraphics.LT ltCut = PicGraphics.LT.LT_CUT;
	const PicGraphics.LT ltFold = PicGraphics.LT.LT_CREASING;
	const PicGraphics.LT ltCotation = PicGraphics.LT.LT_COTATION;

		// free variables
		double B = stack.GetDoubleParameterValue("B");
		double H = stack.GetDoubleParameterValue("H");
		double L = stack.GetDoubleParameterValue("L");
		double ep = stack.GetDoubleParameterValue("ep");
		double hpr = stack.GetDoubleParameterValue("hpr");
		double hr = stack.GetDoubleParameterValue("hr");
		double lp = stack.GetDoubleParameterValue("lp");

		// formulas
		double b1=B/2;
		double b2=L/4;
		double l2=L/2;
		double Rp=hpr-1;
		double Re=(hr/2)-3;

		SortedList<uint, PicEntity> entities = new SortedList<uint, PicEntity>();

		// segments
		double  x0 = 0.0, y0 = 0.0, x1 = 0.0, y1 = 0.0;

		// 3 : (478.344, 543.631) <-> (465.844, 556.131)
		x0 = 425.844+lp+12.5;
		y0 = 296.131+hr+b1-12.5;
		x1 = 425.844+lp;
		y1 = 296.131+hr+b1;
		entities.Add(3, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 4 : (478.344, 543.631) <-> (478.344, 296.131)
		x0 = 425.844+lp+12.5;
		y0 = 296.131+hr+b1-12.5;
		x1 = 425.844+lp+12.5;
		y1 = 296.131;
		entities.Add(4, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 5 : (478.344, 296.131) <-> (615.844, 296.131)
		x0 = 425.844+lp+12.5;
		y0 = 296.131;
		x1 = 425.844+lp+b2;
		y1 = 296.131;
		entities.Add(5, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 6 : (615.844, 296.131) <-> (615.844, 356.131)
		x0 = 425.844+lp+b2;
		y0 = 296.131;
		x1 = 425.844+lp+b2;
		y1 = 296.131+hr;
		entities.Add(6, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 7 : (615.844, 356.131) <-> (915.844, 356.131)
		x0 = 425.844+lp+b2;
		y0 = 296.131+hr;
		x1 = 425.844+lp+b2+l2;
		y1 = 296.131+hr;
		entities.Add(7, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 8 : (915.844, 356.131) <-> (915.844, 296.131)
		x0 = 425.844+lp+b2+l2;
		y0 = 296.131+hr;
		x1 = 425.844+lp+b2+l2;
		y1 = 296.131;
		entities.Add(8, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 9 : (915.844, 296.131) <-> (1053.34, 296.131)
		x0 = 425.844+lp+b2+l2;
		y0 = 296.131;
		x1 = 425.844+lp+L-12.5;
		y1 = 296.131;
		entities.Add(9, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 10 : (1053.34, 296.131) <-> (1053.34, 543.631)
		x0 = 425.844+lp+L-12.5;
		y0 = 296.131;
		x1 = 425.844+lp+L-12.5;
		y1 = 296.131+hr+b1-12.5;
		entities.Add(10, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 11 : (1053.34, 543.631) <-> (1065.84, 556.131)
		x0 = 425.844+lp+L-12.5;
		y0 = 296.131+hr+b1-12.5;
		x1 = 425.844+lp+L;
		y1 = 296.131+hr+b1;
		entities.Add(11, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 12 : (1065.84, 556.131) <-> (1265.84, 356.131)
		x0 = 425.844+lp+L;
		y0 = 296.131+hr+b1;
		x1 = 425.844+lp+L+b1;
		y1 = 296.131+hr;
		entities.Add(12, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 13 : (1265.84, 356.131) <-> (1244.5, 328.303)
		x0 = 425.844+lp+L+b1;
		y0 = 296.131+hr;
		x1 = 425.844+lp+L+b1-21.3455;
		y1 = 296.131+hr-27.8274;
		entities.Add(13, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 14 : (1260.37, 296.131) <-> (1453.34, 296.131)
		x0 = 425.844+lp+L+b1-5.47632;
		y0 = 296.131;
		x1 = 425.844+lp+L+B-12.5007;
		y1 = 296.131;
		entities.Add(14, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 15 : (1453.34, 296.131) <-> (1453.34, 543.631)
		x0 = 425.844+lp+L+B-12.5007;
		y0 = 296.131;
		x1 = 425.844+lp+L+B-12.5007;
		y1 = 296.131+hr+b1-12.5;
		entities.Add(15, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 16 : (1453.34, 543.631) <-> (1465.84, 556.131)
		x0 = 425.844+lp+L+B-12.5007;
		y0 = 296.131+hr+b1-12.5;
		x1 = 425.844+lp+L+B;
		y1 = 296.131+hr+b1;
		entities.Add(16, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 17 : (1465.84, 556.131) <-> (1615.84, 356.131)
		x0 = 425.844+lp+L+B;
		y0 = 296.131+hr+b1;
		x1 = 425.844+lp+L+B+b2;
		y1 = 296.131+hr;
		entities.Add(17, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 18 : (1615.84, 356.131) <-> (1615.84, 296.131)
		x0 = 425.844+lp+L+B+b2;
		y0 = 296.131+hr;
		x1 = 425.844+lp+L+B+b2;
		y1 = 296.131;
		entities.Add(18, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 19 : (1615.84, 296.131) <-> (1915.84, 296.131)
		x0 = 425.844+lp+L+B+b2;
		y0 = 296.131;
		x1 = 425.844+lp+L+B+b2+l2;
		y1 = 296.131;
		entities.Add(19, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 20 : (1915.84, 296.131) <-> (1915.84, 356.131)
		x0 = 425.844+lp+L+B+b2+l2;
		y0 = 296.131;
		x1 = 425.844+lp+L+B+b2+l2;
		y1 = 296.131+hr;
		entities.Add(20, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 21 : (1915.84, 356.131) <-> (2065.84, 556.131)
		x0 = 425.844+lp+L+B+b2+l2;
		y0 = 296.131+hr;
		x1 = 425.844+lp+L+B+L;
		y1 = 296.131+hr+b1;
		entities.Add(21, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 22 : (2065.85, 556.131) <-> (2078.35, 543.631)
		x0 = 425.844+lp+L+B+L;
		y0 = 296.131+hr+b1;
		x1 = 425.844+lp+L+B+L+12.5024;
		y1 = 296.131+hr+b1-12.5;
		entities.Add(22, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 23 : (2078.35, 543.631) <-> (2078.35, 296.131)
		x0 = 425.844+lp+L+B+L+12.5024;
		y0 = 296.131+hr+b1-12.5;
		x1 = 425.844+lp+L+B+L+12.5024;
		y1 = 296.131;
		entities.Add(23, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 24 : (2078.35, 296.131) <-> (2271.32, 296.131)
		x0 = 425.844+lp+L+B+L+12.5022;
		y0 = 296.131;
		x1 = 425.844+lp+L+B+L+b1+5.47681;
		y1 = 296.131;
		entities.Add(24, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 25 : (2287.19, 328.304) <-> (2265.84, 356.131)
		x0 = 425.844+lp+L+B+L+b1+21.3457;
		y0 = 296.131+hr-27.8272;
		x1 = 425.844+lp+L+B+L+b1;
		y1 = 296.131+hr;
		entities.Add(25, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 26 : (2265.84, 356.131) <-> (2465.84, 556.131)
		x0 = 425.844+lp+L+B+L+b1;
		y0 = 296.131+hr;
		x1 = 425.844+lp+L+B+L+B;
		y1 = 296.131+hr+b1;
		entities.Add(26, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 27 : (1065.84, 846.131) <-> (1465.84, 846.131)
		x0 = 425.844+lp+L;
		y0 = 296.131+hr+b1+H-ep;
		x1 = 425.844+lp+L+B;
		y1 = 296.131+hr+b1+H-ep;
		entities.Add(27, fTemp.AddSegment(ltFold, 0, 1,
			x0, y0, x1, y1 ) );

		// 28 : (2065.84, 846.131) <-> (2465.84, 846.131)
		x0 = 425.844+lp+L+B+L;
		y0 = 296.131+hr+b1+H-ep;
		x1 = 425.844+lp+L+B+L+B;
		y1 = 296.131+hr+b1+H-ep;
		entities.Add(28, fTemp.AddSegment(ltFold, 0, 1,
			x0, y0, x1, y1 ) );

		// 29 : (1065.84, 846.131) <-> (1078.34, 856.131)
		x0 = 425.844+lp+L;
		y0 = 296.131+hr+b1+H-ep;
		x1 = 425.844+lp+L+12.5;
		y1 = 296.131+hr+b1+H;
		entities.Add(29, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 30 : (1065.84, 556.131) <-> (1065.84, 846.131)
		x0 = 425.844+lp+L;
		y0 = 296.131+hr+b1;
		x1 = 425.844+lp+L;
		y1 = 296.131+hr+b1+H-ep;
		entities.Add(30, fTemp.AddSegment(ltFold, 0, 1,
			x0, y0, x1, y1 ) );

		// 31 : (465.844, 556.131) <-> (465.844, 856.131)
		x0 = 425.844+lp;
		y0 = 296.131+hr+b1;
		x1 = 425.844+lp;
		y1 = 296.131+hr+b1+H;
		entities.Add(31, fTemp.AddSegment(ltFold, 0, 1,
			x0, y0, x1, y1 ) );

		// 32 : (465.844, 856.131) <-> (1078.34, 856.131)
		x0 = 425.844+lp;
		y0 = 296.131+hr+b1+H;
		x1 = 425.844+lp+L+12.5;
		y1 = 296.131+hr+b1+H;
		entities.Add(32, fTemp.AddSegment(ltFold, 0, 1,
			x0, y0, x1, y1 ) );

		// 33 : (1465.84, 846.131) <-> (1453.34, 856.131)
		x0 = 425.844+lp+L+B;
		y0 = 296.131+hr+b1+H-ep;
		x1 = 425.844+lp+L+B-12.5007;
		y1 = 296.131+hr+b1+H;
		entities.Add(33, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 34 : (2065.84, 846.131) <-> (2078.34, 856.131)
		x0 = 425.844+lp+L+B+L;
		y0 = 296.131+hr+b1+H-ep;
		x1 = 425.844+lp+L+B+L+12.5;
		y1 = 296.131+hr+b1+H;
		entities.Add(34, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 35 : (1465.84, 556.131) <-> (1465.84, 846.131)
		x0 = 425.844+lp+L+B;
		y0 = 296.131+hr+b1;
		x1 = 425.844+lp+L+B;
		y1 = 296.131+hr+b1+H-ep;
		entities.Add(35, fTemp.AddSegment(ltFold, 0, 1,
			x0, y0, x1, y1 ) );

		// 36 : (2065.84, 556.131) <-> (2065.84, 846.131)
		x0 = 425.844+lp+L+B+L;
		y0 = 296.131+hr+b1;
		x1 = 425.844+lp+L+B+L;
		y1 = 296.131+hr+b1+H-ep;
		entities.Add(36, fTemp.AddSegment(ltFold, 0, 1,
			x0, y0, x1, y1 ) );

		// 37 : (465.844, 856.131) <-> (453.344, 868.631)
		x0 = 425.844+lp;
		y0 = 296.131+hr+b1+H;
		x1 = 425.844+lp-12.5;
		y1 = 296.131+hr+b1+H+12.5;
		entities.Add(37, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 38 : (465.844, 556.131) <-> (425.844, 556.131)
		x0 = 425.844+lp;
		y0 = 296.131+hr+b1;
		x1 = 425.844;
		y1 = 296.131+hr+b1;
		entities.Add(38, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 39 : (425.844, 556.131) <-> (425.844, 856.131)
		x0 = 425.844;
		y0 = 296.131+hr+b1;
		x1 = 425.844;
		y1 = 296.131+hr+b1+H;
		entities.Add(39, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 40 : (425.844, 856.131) <-> (465.844, 856.131)
		x0 = 425.844;
		y0 = 296.131+hr+b1+H;
		x1 = 425.844+lp;
		y1 = 296.131+hr+b1+H;
		entities.Add(40, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 41 : (2465.84, 556.131) <-> (2465.84, 846.131)
		x0 = 425.844+lp+L+B+L+B;
		y0 = 296.131+hr+b1;
		x1 = 425.844+lp+L+B+L+B;
		y1 = 296.131+hr+b1+H-ep;
		entities.Add(41, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 42 : (2465.84, 846.131) <-> (2453.34, 856.131)
		x0 = 425.844+lp+L+B+L+B;
		y0 = 296.131+hr+b1+H-ep;
		x1 = 425.844+lp+L+B+L+B-12.5024;
		y1 = 296.131+hr+b1+H;
		entities.Add(42, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 43 : (453.344, 868.631) <-> (453.344, 1256.13)
		x0 = 425.844+lp-12.5;
		y0 = 296.131+hr+b1+H+12.5;
		x1 = 425.844+lp-12.5;
		y1 = 296.131+hr+b1+H+B;
		entities.Add(43, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 44 : (453.344, 1256.13) <-> (465.844, 1256.13)
		x0 = 425.844+lp-12.5;
		y0 = 296.131+hr+b1+H+B;
		x1 = 425.844+lp;
		y1 = 296.131+hr+b1+H+B;
		entities.Add(44, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 45 : (1465.84, 846.131) <-> (1478.34, 856.131)
		x0 = 425.844+lp+L+B;
		y0 = 296.131+hr+b1+H-ep;
		x1 = 425.844+lp+L+B+12.4993;
		y1 = 296.131+hr+b1+H;
		entities.Add(45, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 46 : (2065.84, 846.131) <-> (2053.34, 856.131)
		x0 = 425.844+lp+L+B+L;
		y0 = 296.131+hr+b1+H-ep;
		x1 = 425.844+lp+L+B+L-12.5;
		y1 = 296.131+hr+b1+H;
		entities.Add(46, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 47 : (1478.34, 856.131) <-> (2053.34, 856.131)
		x0 = 425.844+lp+L+B+12.4995;
		y0 = 296.131+hr+b1+H;
		x1 = 425.844+lp+L+B+L-12.5;
		y1 = 296.131+hr+b1+H;
		entities.Add(47, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 48 : (2453.34, 856.131) <-> (2453.34, 1046.13)
		x0 = 425.844+lp+L+B+L+B-12.5024;
		y0 = 296.131+hr+b1+H;
		x1 = 425.844+lp+L+B+L+B-12.5024;
		y1 = 296.131+hr+b1+H-ep+b1;
		entities.Add(48, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 49 : (2078.34, 856.131) <-> (2078.34, 1046.13)
		x0 = 425.844+lp+L+B+L+12.5;
		y0 = 296.131+hr+b1+H;
		x1 = 425.844+lp+L+B+L+12.5;
		y1 = 296.131+hr+b1+H-ep+b1;
		entities.Add(49, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 50 : (2078.34, 1046.13) <-> (2453.34, 1046.13)
		x0 = 425.844+lp+L+B+L+12.5;
		y0 = 296.131+hr+b1+H-ep+b1;
		x1 = 425.844+lp+L+B+L+B-12.5024;
		y1 = 296.131+hr+b1+H-ep+b1;
		entities.Add(50, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 51 : (1453.34, 856.131) <-> (1453.34, 1046.13)
		x0 = 425.844+lp+L+B-12.5007;
		y0 = 296.131+hr+b1+H;
		x1 = 425.844+lp+L+B-12.5007;
		y1 = 296.131+hr+b1+H-ep+b1;
		entities.Add(51, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 52 : (1453.34, 1046.13) <-> (1078.34, 1046.13)
		x0 = 425.844+lp+L+B-12.5007;
		y0 = 296.131+hr+b1+H-ep+b1;
		x1 = 425.844+lp+L+12.5;
		y1 = 296.131+hr+b1+H-ep+b1;
		entities.Add(52, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 53 : (465.844, 1256.13) <-> (465.844, 1276.13)
		x0 = 425.844+lp;
		y0 = 296.131+hr+b1+H+B;
		x1 = 425.844+lp;
		y1 = 296.131+hr+b1+H+B+20;
		entities.Add(53, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 54 : (505.844, 1316.13) <-> (1025.84, 1316.13)
		x0 = 425.844+lp+40;
		y0 = 296.131+hr+b1+H+B+hpr;
		x1 = 425.844+lp+L-40;
		y1 = 296.131+hr+b1+H+B+hpr;
		entities.Add(54, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 55 : (1065.84, 1276.13) <-> (1065.84, 1256.13)
		x0 = 425.844+lp+L;
		y0 = 296.131+hr+b1+H+B+20;
		x1 = 425.844+lp+L;
		y1 = 296.131+hr+b1+H+B;
		entities.Add(55, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 56 : (1065.84, 1256.13) <-> (1078.34, 1256.13)
		x0 = 425.844+lp+L;
		y0 = 296.131+hr+b1+H+B;
		x1 = 425.844+lp+L+12.5;
		y1 = 296.131+hr+b1+H+B;
		entities.Add(56, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 57 : (1065.84, 1256.13) <-> (465.844, 1256.13)
		x0 = 425.844+lp+L;
		y0 = 296.131+hr+b1+H+B;
		x1 = 425.844+lp;
		y1 = 296.131+hr+b1+H+B;
		entities.Add(57, fTemp.AddSegment(ltFold, 0, 1,
			x0, y0, x1, y1 ) );

		// 79 : (465.844, 556.131) <-> (1065.84, 556.131)
		x0 = 425.844+lp;
		y0 = 296.131+hr+b1;
		x1 = 425.844+lp+L;
		y1 = 296.131+hr+b1;
		entities.Add(79, fTemp.AddSegment(ltFold, 0, 1,
			x0, y0, x1, y1 ) );

		// 80 : (1065.84, 556.131) <-> (1265.84, 556.131)
		x0 = 425.844+lp+L;
		y0 = 296.131+hr+b1;
		x1 = 425.844+lp+L+b1;
		y1 = 296.131+hr+b1;
		entities.Add(80, fTemp.AddSegment(ltFold, 0, 1,
			x0, y0, x1, y1 ) );

		// 81 : (1265.84, 556.131) <-> (1465.84, 556.131)
		x0 = 425.844+lp+L+b1;
		y0 = 296.131+hr+b1;
		x1 = 425.844+lp+L+B;
		y1 = 296.131+hr+b1;
		entities.Add(81, fTemp.AddSegment(ltFold, 0, 1,
			x0, y0, x1, y1 ) );

		// 82 : (1465.84, 556.131) <-> (2065.84, 556.131)
		x0 = 425.844+lp+L+B;
		y0 = 296.131+hr+b1;
		x1 = 425.844+lp+L+B+L;
		y1 = 296.131+hr+b1;
		entities.Add(82, fTemp.AddSegment(ltFold, 0, 1,
			x0, y0, x1, y1 ) );

		// 83 : (2065.85, 556.131) <-> (2265.84, 556.131)
		x0 = 425.844+lp+L+B+L;
		y0 = 296.131+hr+b1;
		x1 = 425.844+lp+L+B+L+b1;
		y1 = 296.131+hr+b1;
		entities.Add(83, fTemp.AddSegment(ltFold, 0, 1,
			x0, y0, x1, y1 ) );

		// 84 : (2265.84, 556.131) <-> (2465.84, 556.131)
		x0 = 425.844+lp+L+B+L+b1;
		y0 = 296.131+hr+b1;
		x1 = 425.844+lp+L+B+L+B;
		y1 = 296.131+hr+b1;
		entities.Add(84, fTemp.AddSegment(ltFold, 0, 1,
			x0, y0, x1, y1 ) );

		// 85 : (1078.34, 1046.13) <-> (1078.34, 856.131)
		x0 = 425.844+lp+L+12.5;
		y0 = 296.131+hr+b1+H-ep+b1;
		x1 = 425.844+lp+L+12.5;
		y1 = 296.131+hr+b1+H;
		entities.Add(85, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// 86 : (1078.34, 1256.13) <-> (1078.34, 1046.13)
		x0 = 425.844+lp+L+12.5;
		y0 = 296.131+hr+b1+H+B;
		x1 = 425.844+lp+L+12.5;
		y1 = 296.131+hr+b1+H-ep+b1;
		entities.Add(86, fTemp.AddSegment(ltCut, 0, 1,
			x0, y0, x1, y1 ) );

		// arcs
		double  xc = 0.0, yc = 0.0, radius = 0.0;

		// 58 : radius = 40  s0 = 54  s1 = 55
		fTemp.ProcessTool( new PicToolRound(
			  entities[54]
			, entities[55]
			, Rp						// radius
			));
		// 59 : radius = 40  s0 = 53  s1 = 54
		fTemp.ProcessTool( new PicToolRound(
			  entities[53]
			, entities[54]
			, Rp						// radius
			));
		// 89 : radius = 20  s0 = 13  s1 = 14
		fTemp.ProcessTool( new PicToolRound(
			  entities[13]
			, entities[14]
			, Re						// radius
			));
		// 90 : radius = 20  s0 = 24  s1 = 25
		fTemp.ProcessTool( new PicToolRound(
			  entities[24]
			, entities[25]
			, Re						// radius
			));
		// cotations
		double offset = 0.0;

		// 60: Pt0 = ( 2257.61, 846.131) Pt1 = ( 2257.61, 1046.13) offset = -8.7301
		x0 = 425.844+lp+L+B+L+b1-8.23413;
		y0 = 296.131+hr+b1+H-ep;
		x1 = 425.844+lp+L+B+L+b1-8.23413;
		y1 = 296.131+hr+b1+H-ep+b1;
		offset = -8.7301;
		entities.Add(60, fTemp.AddCotation(PicCotation.CotType.COT_DISTANCE,
			5, 1, x0, y0, x1, y1, offset, ""));

		// 61: Pt0 = ( 856.89, 556.131) Pt1 = ( 856.89, 356.131) offset = -21.0876
		x0 = 425.844+lp+b2+l2-58.9536;
		y0 = 296.131+hr+b1;
		x1 = 425.844+lp+b2+l2-58.9536;
		y1 = 296.131+hr;
		offset = -21.0876;
		entities.Add(61, fTemp.AddCotation(PicCotation.CotType.COT_DISTANCE,
			1, 1, x0, y0, x1, y1, offset, ""));

		// 62: Pt0 = ( 669.711, 1256.13) Pt1 = ( 669.711, 856.131) offset = -48.2816
		x0 = 425.844+lp+b2+53.8672;
		y0 = 296.131+hr+b1+H+B;
		x1 = 425.844+lp+b2+53.8672;
		y1 = 296.131+hr+b1+H;
		offset = -48.2816;
		entities.Add(62, fTemp.AddCotation(PicCotation.CotType.COT_DISTANCE,
			1, 1, x0, y0, x1, y1, offset, ""));

		// 93: Pt0 = ( 465.844, 593.295) Pt1 = ( 1065.84, 593.295) offset = 2.71844
		x0 = 425.844+lp;
		y0 = 296.131+hr+b1+37.1644;
		x1 = 425.844+lp+L;
		y1 = 296.131+hr+b1+37.1644;
		offset = 2.71844;
		entities.Add(93, fTemp.AddCotation(PicCotation.CotType.COT_DISTANCE,
			1, 1, x0, y0, x1, y1, offset, ""));

		// 94: Pt0 = ( 1065.84, 598.732) Pt1 = ( 1465.84, 598.732) offset = -2.71851
		x0 = 425.844+lp+L;
		y0 = 296.131+hr+b1+42.6013;
		x1 = 425.844+lp+L+B;
		y1 = 296.131+hr+b1+42.6013;
		offset = -2.71851;
		entities.Add(94, fTemp.AddCotation(PicCotation.CotType.COT_DISTANCE,
			1, 1, x0, y0, x1, y1, offset, ""));

		// 95: Pt0 = ( 1465.84, 590.577) Pt1 = ( 2065.84, 590.577) offset = 2.71851
		x0 = 425.844+lp+L+B;
		y0 = 296.131+hr+b1+34.4459;
		x1 = 425.844+lp+L+B+L;
		y1 = 296.131+hr+b1+34.4459;
		offset = 2.71851;
		entities.Add(95, fTemp.AddCotation(PicCotation.CotType.COT_DISTANCE,
			1, 1, x0, y0, x1, y1, offset, ""));

		// 96: Pt0 = ( 2065.84, 596.014) Pt1 = ( 2465.84, 596.014) offset = 0
		x0 = 425.844+lp+L+B+L;
		y0 = 296.131+hr+b1+39.8828;
		x1 = 425.844+lp+L+B+L+B;
		y1 = 296.131+hr+b1+39.8828;
		offset = 0;
		entities.Add(96, fTemp.AddCotation(PicCotation.CotType.COT_DISTANCE,
			1, 1, x0, y0, x1, y1, offset, ""));

		// 97: Pt0 = ( 628.84, 856.131) Pt1 = ( 628.84, 556.131) offset = -3.36783
		x0 = 425.844+lp+b2+12.996;
		y0 = 296.131+hr+b1+H;
		x1 = 425.844+lp+b2+12.996;
		y1 = 296.131+hr+b1;
		offset = -3.36783;
		entities.Add(97, fTemp.AddCotation(PicCotation.CotType.COT_DISTANCE,
			1, 1, x0, y0, x1, y1, offset, ""));

	factory.AddEntities(fTemp, transform);
}

public double ImpositionOffsetX(ParameterStack stack) {	return 0.0; }
public double ImpositionOffsetY(ParameterStack stack) {	return 0.0; }
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