namespace Speedioc.Core
{
	/// <summary>
	/// Reasons why a registration was skipped during container generation.
	/// </summary>
	public enum SkippedRegistrationReason
	{
		Overridden,
		NoCodeGenerator
	}
}