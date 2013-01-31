using System;
using Speedioc.Registration.Core;

namespace Speedioc.Registration.Builders
{
	/// <summary>
	/// Implemented by classes that build <see cref="IRegistration"/> instances.
	/// </summary>
	/// <remarks>
	///		The <see cref="IRegistrationBuilder"/> interface provides the core of the 
	///		fluent registration API. Instances of <see cref="IRegistrationBuilder"/> 
	///		are created by <see cref="IRegistrar"/> and then are built up and configured by 
	///		the implementation of <see cref="IRegistrationBuilder"/>, <see cref="IMemberSignatureBuilder"/> 
	///		for a constructor and methods and the <see cref="IPropertyBuilder"/> for properties.
	/// </remarks>
	/// <seealso cref="IRegistrar"/>
	public interface IRegistrationBuilder
	{
		/// <summary>
		/// Registers a mapping between the concrete type to be resolved as the specified mapped type.
		/// </summary>
		/// <param name="type">The mapped type used to resolve instances of the concrete type.</param>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		/// </returns>
		IRegistrationBuilder As(Type type);

		/// <summary>
		/// Registers a mapping between the concrete type to be resolved as the specified mapped type.
		/// </summary>
		/// <typeparam name="T">The mapped type used to resolve instances of the concrete type.</typeparam>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		/// </returns>
		IRegistrationBuilder As<T>();

		/// <summary>
		/// Specifies a method that is invoked after an instance of the type is created.
		/// </summary>
		/// <param name="name">The name of the method to be called.</param>
		/// <returns>
		///		An instance of <see cref="IMemberSignatureBuilder"/> that allows further method chaining 
		///		to configure the parameters of the method.
		/// </returns>
		IMemberSignatureBuilder CallingMethod(string name);

		/// <summary>
		/// Specifies that the singleton instance should be pre-created when the container is loaded.
		/// </summary>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		/// </returns>
		/// <remarks>
		///		PreCreateInstance only applies to registrations with singleton lifetimes which include 
		///		the buildt-in lifetimes, <see cref="Lifetime.AppDomain"/> and <see cref="Lifetime.Container"/>. 
		///		This method has no effect on registrations with other built-in lifetimes. 
		///		This method may affect custom lifetimes based on their implementation.
		/// </remarks>
		IRegistrationBuilder PreCreateInstance();

		/// <summary>
		/// Specifies the constructor that is used to create instances of the registered type.
		/// </summary>
		/// <returns>
		///		An instance of <see cref="IMemberSignatureBuilder"/> that allows further method chaining 
		///		to configure the parameters of the constructor.
		/// </returns>
		IMemberSignatureBuilder UsingConstructor();

		/// <summary>
		/// Specifies a field that is set after an instance of the type is created.
		/// </summary>
		/// <param name="name">The name of the field to be set.</param>
		/// <param name="type">The type of the field to be set.</param>
		/// <returns>
		///		An instance of <see cref="IFieldBuilder"/> that allows further method chaining 
		///		to configure the value that is set for the specified field.
		/// </returns>
		IFieldBuilder WithField(string name, Type type);

		/// <summary>
		/// Specifies a field that is set after an instance of the type is created.
		/// </summary>
		/// <param name="name">The name of the field to be set.</param>
		/// <typeparam name="T">The type of the field to be set.</typeparam>
		/// <returns>
		///		An instance of <see cref="IFieldBuilder"/> that allows further method chaining 
		///		to configure the value that is set for the specified field.
		/// </returns>
		IFieldBuilder WithField<T>(string name);

		/// <summary>
		/// Specifies the lifetime or scope of the registered type.
		/// </summary>
		/// <param name="lifetime">The lifetime or scope of the registered type.</param>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		/// </returns>
		IRegistrationBuilder WithLifetime(Lifetime lifetime);

		/// <summary>
		/// Specifies that the type registration should be a named type using the specified name.
		/// </summary>
		/// <param name="name">The name of the registered type.</param>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		/// </returns>
		IRegistrationBuilder WithName(string name);

		/// <summary>
		/// Specifies that the type registration is a primitive type that has the specified value.
		/// </summary>
		/// <param name="primitiveValue">The value of the primitive type.</param>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		/// </returns>
		IRegistrationBuilder WithPrimitiveValue(object primitiveValue);

		/// <summary>
		/// Specifies a property that is set after an instance of the type is created.
		/// </summary>
		/// <param name="name">The name of the property to be set.</param>
		/// <param name="type">The type of the property to be set.</param>
		/// <returns>
		///		An instance of <see cref="IPropertyBuilder"/> that allows further method chaining 
		///		to configure the value that is set for the specified property.
		/// </returns>
		IPropertyBuilder WithProperty(string name, Type type);

		/// <summary>
		/// Specifies a property that is set after an instance of the type is created.
		/// </summary>
		/// <param name="name">The name of the property to be set.</param>
		/// <typeparam name="T">The type of the property to be set.</typeparam>
		/// <returns>
		///		An instance of <see cref="IPropertyBuilder"/> that allows further method chaining 
		///		to configure the value that is set for the specified property.
		/// </returns>
		IPropertyBuilder WithProperty<T>(string name);
	}
}