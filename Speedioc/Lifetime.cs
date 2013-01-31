namespace Speedioc
{
	/// <summary>
	/// The lifetime of a type  that is registered with a container.
	/// </summary>
	public enum Lifetime
	{
		/// <summary>
		/// Types registered with the Transient lifetime have a new instance 
		/// created each time they are resolved.
		/// </summary>
		Transient = 0,

		/// <summary>
		/// Types registered with the AppDomain lifetime have a new instance 
		/// created once for an AppDomain (an AppDomain scoped singleton).
		/// </summary>
		AppDomain,

		/// <summary>
		/// Types registered with the Container lifetime have a new instance 
		/// created once for the container instance (a container instance scoped singleton).
		/// </summary>
		Container,

		/// <summary>
		/// Types registered with the Thread lifetime have a new instance 
		/// created once for each thread (one instance per thread).
		/// </summary>
		Thread,

		/// <summary>
		/// Specifies that a custom lifetime implementation is used.
		/// </summary>
		Custom
	}
}