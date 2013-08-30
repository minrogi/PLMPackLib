public ParameterStack Parameters
{
	get
	{
		ParameterStack stack = new ParameterStack();
		stack.AddDoubleParameter( "B", "B", 70.711, 0);
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
	double B = stack.GetDoubleParameterValue("B");

	// formulas

	SortedList<uint, PicEntity> entities = new SortedList<uint, PicEntity>();

	// segments
	double  x0 = 0.0, y0 = 0.0, x1 = 0.0, y1 = 0.0;

	x0 = 0.5*B;
	y0 = 0.0;
	x1 = 0.0;
	y1 = 0.5*B;
	entities.Add(21, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1 ) );

	x0 = -0.5*B;
	y0 = 0.0;
	x1 = 0.0;
	y1 = 0.5*B;
	entities.Add(22, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1 ) );

	x0 = 0.0;
	y0 = -0.5*B;
	x1 = -0.5*B;
	y1 = 0.0;
	entities.Add(23, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1 ) );

	x0 = 0.0;
	y0 = -0.5*B;
	x1 = 0.5*B;
	y1 = 0.0;
	entities.Add(24, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1 ) );

	// arcs
	double  xc = 0.0, yc = 0.0, radius = 0.0;

	// cotations
	double offset = 0.0;

	factory.AddEntities(fTemp, transform);
}
public double ImpositionOffsetX(ParameterStack stack) {	return 0.0; }
public double ImpositionOffsetY(ParameterStack stack) {	return 0.0; }