namespace QuickStartSampleApp.Domain
{
	public class Truck : IVehicle
	{
		public Truck()
		{
			ColorScheme = new ColorScheme("White", new WhiteColor(), new WhiteColor());
		}

		public Truck(string make, string model)
			: this()
		{
			Make = make;
			Model = model;
		}

		public Truck(string make, string model, IColorScheme colorScheme)
			: this()
		{
			Make = make;
			Model = model;
			ColorScheme = colorScheme;
		}

		public string Make { get; set; }
		public string Model { get; set; }
		public IColorScheme ColorScheme { get; set; }

		public int EngineSizeCubicInches;
	}
}