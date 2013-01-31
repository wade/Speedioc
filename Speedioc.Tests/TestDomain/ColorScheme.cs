namespace Speedioc.TestDomain
{
	public class ColorScheme : IColorScheme
	{
		public ColorScheme(string name, IColor primaryColor, IColor secondaryColor)
		{
			Name = name;
			PrimaryColor = primaryColor;
			SecondaryColor = secondaryColor;
		}

		public string Name { get; set; }
		public IColor PrimaryColor { get; set; }
		public IColor SecondaryColor { get; set; }
	}
}