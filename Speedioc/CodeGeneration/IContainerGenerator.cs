using System.Collections.Generic;
using Speedioc.Registration.Core;

namespace Speedioc.CodeGeneration
{
	/// <summary>
	/// Implemented by classes that create a container instance by generating and compiling source code.
	/// </summary>
	public interface IContainerGenerator
	{
		/// <summary>
		/// Generates the container using the specified registry instance and container settings.
		/// </summary>
		/// <param name="registrations">The registrations.</param>
		/// <param name="settings">The settings.</param>
		/// <returns>An instance of the generated conatiner.</returns>
		IContainer GenerateContainer(IList<IRegistration> registrations, IContainerSettings settings = null);
	}
}