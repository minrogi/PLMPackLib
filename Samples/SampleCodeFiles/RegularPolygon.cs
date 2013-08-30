public ParameterStack Parameters
{
	get
	{
		ParameterStack stack = new ParameterStack();
		stack.AddDoubleParameter( "R", "R", 100.0, 0);
		stack.AddIntParameter( "Sides", "Sides"
			, 6 // default value
			, true // has min value
			, 3	// min value
			, false // has no max value
			, 0 // dummy max value
			);
		return stack;
	}
}
public void CreateFactoryEntities(PicFactory factory, ParameterStack stack, Transform2D transform)
{
	PicFactory fTemp = new PicFactory();
	const PicGraphics.LT ltCut = PicGraphics.LT.LT_CUT;
	const PicGraphics.LT ltFold = PicGraphics.LT.LT_CREASING;
	const PicGraphics.LT ltCotation = PicGraphics.LT.LT_COTATION;

		// free variables
		double R = stack.GetDoubleParameterValue("R");
		int sides = stack.GetIntParameterValue("Sides");
	
		SortedList<uint, PicEntity> entities = new SortedList<uint, PicEntity>();

		for (int i=0; i<sides; ++i)
		{
			double  x0 = 0.0, y0 = 0.0, x1 = 0.0, y1 = 0.0;
			double angle0 = (2.0 * Math.PI * i) / sides;
			double angle1 = (2.0 * Math.PI * (i+1)) / sides;
			x0 = R * Math.Cos(angle0);
			y0 = R * Math.Sin(angle0);
			x1 = R * Math.Cos(angle1);
			y1 = R * Math.Sin(angle1);
			fTemp.AddSegment(ltCut, 0, 1, x0, y0, x1, y1 );
		}
		factory.AddEntities(fTemp, transform);
}
public double ImpositionOffsetX(ParameterStack stack) {	return 0.0; }
public double ImpositionOffsetY(ParameterStack stack) {	return 0.0; }
