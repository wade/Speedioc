using System;
using Speedioc.Registration.Builders;
using Speedioc.Registration.Core;

namespace Speedioc.Registration
{
	/// <summary>
	/// Implemented by classes that are used to register types using the 
	/// fluent registration API.
	/// </summary>
	/// <remarks>
	///		<para>
	///			This interface is typically used along with the <see cref="IRegistry"/> 
	///			interface but may also be used independently.
	///		</para>
	///		<para>
	///			This interface is designed to be consumed by developers that are 
	///			registering types. The <see cref="IRegistrationData"/> interface 
	///			is designed to be used by code that processes the object graph 
	///			produced by the implementation of the <see cref="IRegistrar"/> 
	///			interface to actually perform the type registration. Both interfaces 
	///			are typically implemented by the same class but offer two different 
	///			functional views of the same data: one to build it and one to use it.
	///		</para>
	/// </remarks>
	/// <seealso cref="IRegistrationData"/>
	/// <seealso cref="IRegistry"/>
	public interface IRegistrar
	{
		/// <summary>
		/// Registers the specified type.
		/// </summary>
		/// <param name="type">The concrete type to be registered.</param>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that is 
		///		used to fluently build the complete type registration.
		/// </returns>
		IRegistrationBuilder Register(Type type);

		/// <summary>
		/// Registers the specified type.
		/// </summary>
		/// <typeparam name="T">The concrete type to be registered.</typeparam>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that is 
		///		used to fluently build the complete type registration.
		/// </returns>
		IRegistrationBuilder Register<T>();
	}
}