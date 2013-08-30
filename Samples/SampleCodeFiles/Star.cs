public ParameterStack Parameters
{
	get
	{
		ParameterStack stack = new ParameterStack();
		stack.AddDoubleParameter( "External radius", "R_ext", 100.0, 0.0);
		stack.AddDoubleParameter( "Internal radius", "R_int", 50.0, 0.0);
		stack.AddIntParameter(
			"Branches"
			, "Branches"
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
	double R_ext = stack.GetDoubleParameterValue("External radius");
	double R_int = stack.GetDoubleParameterValue("Internal radius");
	int branches = stack.GetIntParameterValue("Branches");

	SortedList<uint, PicEntity> entities = new SortedList<uint, PicEntity>();

	for (int i=0; i<branches; ++i)
	{
		double angle0 = (2.0 * Math.PI * i) / branches;
		double angle2 = (2.0 * Math.PI * (i+1)) / branches;
		double angle1 = 0.5*(angle0 + angle2);
		double x0 = R_int * Math.Cos(angle0);
		double y0 = R_int * Math.Sin(angle0);
		double x1 = R_ext * Math.Cos(angle1);
		double y1 = R_ext * Math.Sin(angle1);
		double x2 = R_int * Math.Cos(angle2);
		double y2 = R_int * Math.Sin(angle2);
		fTemp.AddSegment(ltCut, 0, 1, x0, y0, x1, y1 );
		fTemp.AddSegment(ltCut, 0, 1, x1, y1, x2, y2 );
	}
	factory.AddEntities(fTemp, transform);
}
public double ImpositionOffsetX(ParameterStack stack) {	return 0.0; }
public double ImpositionOffsetY(ParameterStack stack) {	return 0.0; }
