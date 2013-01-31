namespace QuickStartSampleApp.Domain
{
	public class RedColor : IColor
	{
		public RedColor()
		{
			Name = "Red";
		}

		public string Name { get; set; }
	}
}