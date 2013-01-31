namespace Speedioc.TestDomain
{
	public interface IColorScheme
	{
		string Name { get; set; }
		IColor PrimaryColor { get; set; }
		IColor SecondaryColor { get; set; }
	}
}