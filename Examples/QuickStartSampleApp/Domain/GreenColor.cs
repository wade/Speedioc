namespace QuickStartSampleApp.Domain
{
	public class GreenColor : IColor
	{
		public GreenColor()
		{
			Name = "Green";
		}

		public string Name { get; set; }
	}
}