namespace Speedioc.Core
{
	public class OverriddenRegistration
	{
		public OverriddenRegistration(IRegistrationMetadata oldRegistration, IRegistrationMetadata newRegistration)
		{
			OldRegistration = oldRegistration;
			NewRegistration = newRegistration;
		}

		public IRegistrationMetadata OldRegistration { get; set; }
		public IRegistrationMetadata NewRegistration { get; set; }
	}
}