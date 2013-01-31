using System.Collections.Generic;

namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Implemented by classes that are used to access the type registration 
	/// data built by an <see cref="IRegistrar"/> implementation.
	/// </summary>
	/// <remarks>
	///		<para>
	///			This interface is designed to be used by code that uses the object graph 
	///			produced by the implementation of the <see cref="IRegistrar"/> 
	///			interface to actually perform the type registration. Both interfaces 
	///			are typically implemented by the same class but offer two different 
	///			functional views of the same data: one to build it and one to use it.
	///		</para>
	/// </remarks>
	/// <seealso cref="IRegistrar"/>
	/// <seealso cref="IRegistry"/>
	public interface IRegistrationData
	{
		/// <summary>
		/// Gets the collection of registration objects.
		/// </summary>
		/// <value>
		/// The collection of registration objects.
		/// </value>
		/// <remarks>
		///		The collection of registration objects that were built by an 
		///		implementation of the <see cref="IRegistrar"/> interface.
		/// </remarks>
		IList<IRegistration> Registrations { get; } 
	}
}