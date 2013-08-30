public ParameterStack Parameters
{
	get
	{
		ParameterStack stack = new ParameterStack();
		stack.AddDoubleParameter( "L", "L", 100, 0);
		stack.AddIntParameter("Sides", "S", 6, true, 3, false, 0);
		string[] valueList = {"Polygon", "Star"};
		stack.AddMultiParameter("Hole type", "Hole", valueList, valueList, 0);
		
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
	double L = stack.GetDoubleParameterValue("L");
	int S = stack.GetIntParameterValue("Sides");
	int iCombo = stack.GetMultiParameterValue("Hole type");

	// formulas

	SortedList<uint, PicEntity> entities = new SortedList<uint, PicEntity>();

	// segments
	double  x0 = 0.0, y0 = 0.0, x1 = 0.0, y1 = 0.0;

	// 14 : (-50, -50) <-> (50, -50)
	x0 = -0.5*L;
	y0 = -0.5*L;
	x1 = 0.5*L;
	y1 = -0.5*L;
	entities.Add(14, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1 ) );

	// 15 : (50, -50) <-> (50, 50)
	x0 = 0.5*L;
	y0 = -0.5*L;
	x1 = 0.5*L;
	y1 = 0.5*L;
	entities.Add(15, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1 ) );

	// 16 : (-50, -50) <-> (-50, 50)
	x0 = -0.5*L;
	y0 = -0.5*L;
	x1 = -0.5*L;
	y1 = 0.5*L;
	entities.Add(16, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1 ) );

	// 17 : (-50, 50) <-> (50, 50)
	x0 = -0.5*L;
	y0 = 0.5*L;
	x1 = 0.5*L;
	y1 = 0.5*L;
	entities.Add(17, fTemp.AddSegment(ltCut, 1, 1, x0, y0, x1, y1 ) );

	// arcs
	double  xc = 0.0, yc = 0.0, radius = 0.0;

	// cotations
	double offset = 0.0;

	if (iCombo == 0)
	{ // Regular Polygon

			IPlugin pluginIn = Host.GetPluginByGuid(new Guid("03b0819e-dbc6-4569-9f25-3fabd6e57a18"));
			ParameterStack stackIn = pluginIn.Parameters;
			stackIn.SetDoubleParameter("R",0.25 * L);		// R
			stackIn.SetIntParameter("Sides",S);		// Sides
			bool reflectionX = false, reflectionY = false;
			Transform2D transfReflect = (reflectionY ? Transform2D.ReflectionY : Transform2D.Identity) * (reflectionX ? Transform2D.ReflectionX : Transform2D.Identity);

			pluginIn.CreateFactoryEntities(fTemp, stackIn,
					 transform
					*Transform2D.Translation(new Vector2D(0.0, 0.0))
					*Transform2D.Rotation(0.0)
					*transfReflect);
	}

	else if (iCombo == 1)
	{ // Star

			IPlugin pluginIn = Host.GetPluginByGuid("b80378b7-9c9e-4b40-9d97-3d97711b3a69");
            double externalRadius = Host.GetParameterDefaultValueDouble(new Guid("b80378b7-9c9e-4b40-9d97-3d97711b3a69"), "External radius");
			double internalRadius = Host.GetParameterDefaultValueDouble(new Guid("b80378b7-9c9e-4b40-9d97-3d97711b3a69"), "Internal radius");
    		ParameterStack stackIn = pluginIn.Parameters;
			stackIn.SetDoubleParameter("External radius",externalRadius);		// R_ext
			stackIn.SetDoubleParameter("Internal radius",internalRadius);		// R_int
			stackIn.SetIntParameter("Branches",S);		// Branches
			bool reflectionX = false, reflectionY = false;
			Transform2D transfReflect = (reflectionY ? Transform2D.ReflectionY : Transform2D.Identity) * (reflectionX ? Transform2D.ReflectionX : Transform2D.Identity);
			pluginIn.CreateFactoryEntities(fTemp, stackIn,
				 transform
                *Transform2D.Translation(new Vector2D(0.0, 0.0))
                *Transform2D.Rotation(0.0)
                *transfReflect);

	}

	factory.AddEntities(fTemp, transform);
}

public double ImpositionOffsetX(ParameterStack stack) {	return 0.0; }
public double ImpositionOffsetY(ParameterStack stack) {	return 0.0; }