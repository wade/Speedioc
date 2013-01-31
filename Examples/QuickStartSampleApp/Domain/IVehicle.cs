namespace QuickStartSampleApp.Domain
{
	public interface IVehicle
	{
		string Make { get; }
		string Model { get; }
		IColorScheme ColorScheme { get; }
	}
}