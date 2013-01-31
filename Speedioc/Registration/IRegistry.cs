using Speedioc.Registration.Core;

namespace Speedioc.Registration
{
	/// <summary>
	/// Implemented by classes that are used to register types.
	/// </summary>
	/// <remarks>
	///		Registry classes are used to register types for a DrillBit's 
	///		underlying resolver object (e.g. container) using the 
	///		registration API. Classes that implement <see cref="IRegistry"/> 
	///		must also implement <see cref="IRegistrar"/> which provides the root 
	///		methods of the registration API.
	/// </remarks>
	/// <seealso cref="IRegistrar"/>
	/// <seealso cref="IRegistrationData"/>
	public interface IRegistry : IRegistrar
	{
		/// <summary>
		/// Registers types using the fluent <see cref="IRegistrar"/> interface.
		/// </summary>
		void RegisterTypes();
	}
}