namespace QuickStartSampleApp.Domain
{
	public class WhiteColor : IColor
	{
		public WhiteColor()
		{
			Name = "White";
		}

		public string Name { get; set; }
	}
}