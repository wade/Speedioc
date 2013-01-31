namespace Speedioc.Core
{
	/// <summary>
	/// Defines if a registration was specified implcitly or explicitly.
	/// </summary>
	public enum RegistrationType
	{
		/// <summary>
		/// The registration was specified explicitly such as in a registry class.
		/// </summary>
		Explicit,

		/// <summary>
		/// The registration is implicit and was created as a part of the generation of a container.
		/// </summary>
		Implicit
	}
}