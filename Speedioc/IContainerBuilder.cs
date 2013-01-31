using Speedioc.Registration;

namespace Speedioc
{
	/// <summary>
	/// Implemented by classes that are used to build a container.
	/// </summary>
	public interface IContainerBuilder
	{
		/// <summary>
		/// Adds the registry to the builder.
		/// </summary>
		/// <param name="registry">The registry to be added to the builder.</param>
		/// <remarks>
		/// Registry instances must be applied to a new container in the order that they are added for deterministic behavior.
		/// </remarks>
		void AddRegistry(IRegistry registry);

		/// <summary>
		/// Builds a new container instance.
		/// </summary>
		/// <returns>A new container instance.</returns>
		IContainer Build();
	}
}