using System;
using System.Collections.Generic;
using Speedioc.Core;
using Speedioc.Registration.Builders;

namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Implemented by classes that provide type registration data.
	/// </summary>
	/// <seealso cref="IRegistrationBuilder"/>
	/// <seealso cref="IRegistrationData"/>
	/// <seealso cref="IRegistrar"/>
	/// <seealso cref="IRegistry"/>
	public interface IRegistration
	{
		/// <summary>
		/// Gets the concrete type to be registered.
		/// </summary>
		/// <value>
		/// The concrete type to be registered.
		/// </value>
		/// <remarks>This member is required and may not be null.</remarks>
		Type ConcreteType { get; }

		/// <summary>
		/// Gets the abstract type or interface to be mapped to the concrete type.
		/// </summary>
		/// <value>
		/// The abstract type or interface to be mapped to the concrete type.
		/// </value>
		/// <remarks>This member is optional and may be null.</remarks>
		Type MappedType { get; }

		Operation Operation { get; }

		/// <summary>
		/// Gets the lifetime or scope of the object to be registered.
		/// </summary>
		/// <value>
		/// The lifetime.
		/// </value>
		Lifetime Lifetime { get; }

		/// <summary>
		/// Gets the value to be used as the dependency name.
		/// </summary>
		/// <value>
		/// The value to be used as the dependency name.
		/// </value>
		/// <remarks>
		///		The Name property is only used when the registration represents a named dependency. 
		///		This member is optional and may be null.
		/// </remarks>
		string Name { get; }

		/// <summary>
		/// Gets the primitive value.
		/// </summary>
		/// <value>
		/// The primitive value.
		/// </value>
		/// <remarks>
		///		The PrimitiveValue property is only used when the registration represents a primitive type. 
		///		This member is optional and may be null.
		/// </remarks>
		object PrimitiveValue { get; }

		/// <summary>
		/// Gets a value indicating whether the registered instance should be pre-created when the container is loaded.
		/// </summary>
		/// <value>
		///   <c>true</c> if the registered instance should be pre-created when the container is loaded; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		///		PreCreateInstance only applies to registrations with singleton lifetimes which include 
		///		the buildt-in lifetimes, Lifetime.AppDomain and Lifetime.Container. 
		///		This method has no effect on registrations with other built-in lifetimes. 
		///		This method may affect custom lifetimes based on their implementation.
		/// </remarks>
		bool ShouldPreCreateInstance { get; }

		/// <summary>
		/// Gets the constructor registration data.
		/// </summary>
		/// <value>
		/// The constructor registration data.
		/// </value>
		/// <remarks>
		///		<para>
		///			The Constructor property is used when specifying a specific 
		///			constructor and parameters that should be used to create instances 
		///			of the registered type spefified by the <see cref="ConcreteType"/> property. 
		///		</para>	
		///		<para>
		///			When this property is null or has an empty Parameters collection, 
		///			the default, parameterless constructor will be used to create new 
		///			instances of the registered type.
		///		</para>
		///		<para>
		///			This member is optional and may be null.
		///		</para>
		/// </remarks>
		IConstructor Constructor { get; }

		/// <summary>
		/// Gets the list of members to be invoked after a new instance of the registered type is created.
		/// </summary>
		/// <value>
		/// The list of members to be invoked after a new instance of the registered type is created.
		/// </value>
		/// <remarks>
		///		The members collection may include <see cref="IProperty"/> and 
		///		<see cref="IMethod"/> instances that are to be invoked in the order 
		///		registered after a new instance of the registered type has been created.
		/// </remarks>
		IList<IMember> Members { get; }
	}
}