namespace QuickStartSampleApp.Domain
{
	public class BlueColor : IColor
	{
		public BlueColor()
		{
			Name = "Blue";
		}

		public string Name { get; set; }
	}
}