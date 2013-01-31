namespace Speedioc.Core
{
	/// <summary>
	/// Represents a type registration that was skipped during the generation of a container.
	/// </summary>
	public class SkippedRegistration
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SkippedRegistration" /> class.
		/// </summary>
		/// <param name="reason">The reason.</param>
		/// <param name="description">The description.</param>
		/// <param name="registration">The registration.</param>
		/// <param name="overriddenRegistration">The overridden registration.</param>
		public SkippedRegistration(SkippedRegistrationReason reason, string description, IRegistrationMetadata registration, OverriddenRegistration overriddenRegistration = null)
		{
			Reason = reason;
			Description = description;
			Registration = registration;
			OverriddenRegistration = overriddenRegistration;
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the reason.
		/// </summary>
		/// <value>
		/// The reason.
		/// </value>
		public SkippedRegistrationReason Reason { get; set; }

		/// <summary>
		/// Gets or sets the registration.
		/// </summary>
		/// <value>
		/// The registration.
		/// </value>
		public IRegistrationMetadata Registration { get; set; }

		/// <summary>
		/// Gets or sets the overridden registration.
		/// </summary>
		/// <value>
		/// The overridden registration.
		/// </value>
		public OverriddenRegistration OverriddenRegistration { get; set; }
	}
}