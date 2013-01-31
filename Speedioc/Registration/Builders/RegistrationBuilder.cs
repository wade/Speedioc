using System;
using System.Collections.Generic;
using Speedioc.Core;
using Speedioc.Registration.Core;

namespace Speedioc.Registration.Builders
{
	/// <summary>
	/// Builds <see cref="IRegistration"/> instances.
	/// </summary>
	/// <seealso cref="IRegistrationBuilder"/>
	/// <seealso cref="IRegistration"/>
	/// <seealso cref="IRegistrar"/>
	public class RegistrationBuilder : IRegistrationBuilder, IRegistration
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RegistrationBuilder" /> class.
		/// </summary>
		/// <param name="concreteType">The concrete type to be registered.</param>
		/// <param name="operation">The registered operation.</param>
		/// <exception cref="System.ArgumentNullException">concreteType</exception>
		public RegistrationBuilder(Type concreteType, Operation operation)
		{
			if (null == concreteType)
			{
				throw new ArgumentNullException("concreteType");
			}
			ConcreteType = concreteType;
			Lifetime = Lifetime.Transient;
			Members = new List<IMember>();
			Operation = operation;
		}

		#region " IRegistration Interface Implementation "

		/// <summary>
		/// Gets the concrete type to be registered.
		/// </summary>
		/// <value>
		/// The concrete type to be registered.
		/// </value>
		/// <remarks>
		/// This member is required and may not be null.
		/// </remarks>
		public Type ConcreteType { get; private set; }

		/// <summary>
		/// Gets the abstract type or interface to be mapped to the concrete type.
		/// </summary>
		/// <value>
		/// The abstract type or interface to be mapped to the concrete type.
		/// </value>
		/// <remarks>
		/// This member is optional and may be null.
		/// </remarks>
		public Type MappedType { get; private set; }

		public Operation Operation { get; private set; }

		/// <summary>
		/// Gets the lifetime or scope of the object to be registered.
		/// </summary>
		/// <value>
		/// The lifetime.
		/// </value>
		public Lifetime Lifetime { get; private set; }

		/// <summary>
		/// Gets the value to be used as the named type's name.
		/// </summary>
		/// <value>
		/// The value to be used as the named type's name.
		/// </value>
		/// <remarks>
		/// The Name property is only used when the registration represents a named type.
		/// This member is optional and may be null.
		/// </remarks>
		public string Name { get; private set; }

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
		public object PrimitiveValue { get; private set; }

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
		public bool ShouldPreCreateInstance { get; private set; }

		/// <summary>
		/// Gets the constructor registration data.
		/// </summary>
		/// <value>
		/// The constructor registration data.
		/// </value>
		/// <remarks>
		///   <para>
		///		The Constructor property is used when specifying a specific
		///		constructor and parameters that should be used to create instances
		///		of the registered type spefified by the <see cref="ConcreteType" /> property.
		///   </para>
		///   <para>
		///		When this property is null or has an empty Parameters collection,
		///		the default, parameterless constructor will be used to create new
		///		instances of the registered type.
		///   </para>
		///   <para>
		///		This member is optional and may be null.
		///   </para>
		/// </remarks>
		public IConstructor Constructor { get; internal set; }

		/// <summary>
		/// Gets the list of members to be invoked after a new instance of the registered type is created.
		/// </summary>
		/// <value>
		/// The list of members to be invoked after a new instance of the registered type is created.
		/// </value>
		/// <remarks>
		/// The members collection may include <see cref="IProperty" /> and
		/// <see cref="IMethod" /> instances that are to be invoked in the order
		/// registered after a new instance of the registered type has been created.
		/// </remarks>
		public IList<IMember> Members { get; internal set; }

		#endregion " IRegistration Interface Implementation "

		#region " IRegistrationBuilder Interface Implementation "

		/// <summary>
		/// Registers a mapping between the concrete type to be resolved as the specified mapped type.
		/// </summary>
		/// <param name="type">The mapped type used to resolve instances of the concrete type.</param>
		/// <returns>
		/// An instance of <see cref="IRegistrationBuilder" /> that allows further method chaining
		/// of the fluent registration API.
		/// </returns>
		public IRegistrationBuilder As(Type type)
		{
			MappedType = type;
			return this;
		}

		/// <summary>
		/// Registers a mapping between the concrete type to be resolved as the specified mapped type.
		/// </summary>
		/// <typeparam name="T">The mapped type used to resolve instances of the concrete type.</typeparam>
		/// <returns>
		/// An instance of <see cref="IRegistrationBuilder" /> that allows further method chaining
		/// of the fluent registration API.
		/// </returns>
		public IRegistrationBuilder As<T>()
		{
			return As(typeof(T));
		}

		/// <summary>
		/// Specifies a method that is invoked after an instance of the type is created.
		/// </summary>
		/// <param name="name">The name of the method to be called.</param>
		/// <returns>
		/// An instance of <see cref="IMemberSignatureBuilder" /> that allows further method chaining
		/// to configure the parameters of the method.
		/// </returns>
		public IMemberSignatureBuilder CallingMethod(string name)
		{
			return new MethodBuilder(this, name);
		}

		/// <summary>
		/// Specifies that the singleton instance should be pre-created when the container is loaded.
		/// </summary>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		/// </returns>
		/// <remarks>
		///		PreCreateInstance only applies to registrations with singleton lifetimes which include 
		///		the built-in lifetimes <see cref="Speedioc.Lifetime.AppDomain"/> and <see cref="Speedioc.Lifetime.Container"/>. 
		///		This method has no effect on registrations with other built-in lifetimes. 
		///		This method may affect custom lifetimes based on their implementation.
		/// </remarks>
		public IRegistrationBuilder PreCreateInstance()
		{
			ShouldPreCreateInstance = true;
			return this;
		}

		/// <summary>
		/// Specifies that the type registration should be a named type using the specified name.
		/// </summary>
		/// <param name="name">The name of the registered type.</param>
		/// <returns>
		/// An instance of <see cref="IRegistrationBuilder" /> that allows further method chaining
		/// of the fluent registration API.
		/// </returns>
		public IRegistrationBuilder WithName(string name)
		{
			Name = name;
			return this;
		}

		/// <summary>
		/// Specifies that the type registration is a primitive type that has the specified value.
		/// </summary>
		/// <param name="primitiveValue">The value of the primitive type.</param>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		/// </returns>
		public IRegistrationBuilder WithPrimitiveValue(object primitiveValue)
		{
			PrimitiveValue = primitiveValue;
			return this;
		}

		/// <summary>
		/// Specifies a property that is set after an instance of the type is created.
		/// </summary>
		/// <param name="name">The name of the property to be set.</param>
		/// <param name="type">The type of the property to be set.</param>
		/// <returns>
		/// An instance of <see cref="IPropertyBuilder" /> that allows further method chaining
		/// to configure the value that is set for the specified property.
		/// </returns>
		public IPropertyBuilder WithProperty(string name, Type type)
		{
			return new PropertyBuilder(this, name, type);
		}

		/// <summary>
		/// Specifies a property that is set after an instance of the type is created.
		/// </summary>
		/// <param name="name">The name of the property to be set.</param>
		/// <typeparam name="T">The type of the property to be set.</typeparam>
		/// <returns>
		///		An instance of <see cref="IPropertyBuilder"/> that allows further method chaining 
		///		to configure the value that is set for the specified property.
		/// </returns>
		public IPropertyBuilder WithProperty<T>(string name)
		{
			return WithProperty(name, typeof (T));
		}

		/// <summary>
		/// Specifies a field that is set after an instance of the type is created.
		/// </summary>
		/// <param name="name">The name of the field to be set.</param>
		/// <param name="type">The type of the field to be set.</param>
		/// <returns>
		///		An instance of <see cref="IFieldBuilder"/> that allows further method chaining 
		///		to configure the value that is set for the specified field.
		/// </returns>
		public IFieldBuilder WithField(string name, Type type)
		{
			return new FieldBuilder(this, name, type);
		}

		/// <summary>
		/// Specifies a field that is set after an instance of the type is created.
		/// </summary>
		/// <param name="name">The name of the field to be set.</param>
		/// <typeparam name="T">The type of the field to be set.</typeparam>
		/// <returns>
		///		An instance of <see cref="IFieldBuilder"/> that allows further method chaining 
		///		to configure the value that is set for the specified field.
		/// </returns>
		public IFieldBuilder WithField<T>(string name)
		{
			return WithField(name, typeof(T));
		}

		/// <summary>
		/// Specifies the lifetime or scope of the registered type.
		/// </summary>
		/// <param name="lifetime">The lifetime or scope of the registered type.</param>
		/// <returns>
		/// An instance of <see cref="IRegistrationBuilder" /> that allows further method chaining
		/// of the fluent registration API.
		/// </returns>
		public IRegistrationBuilder WithLifetime(Lifetime lifetime)
		{
			Lifetime = lifetime;
			return this;
		}

		/// <summary>
		/// Specifies the constructor that is used to create instances of the registered type.
		/// </summary>
		/// <returns>
		/// An instance of <see cref="IMemberSignatureBuilder" /> that allows further method chaining
		/// to configure the parameters of the constructor.
		/// </returns>
		public IMemberSignatureBuilder UsingConstructor()
		{
			return new ConstructorBuilder(this);
		}

		#endregion " IRegistrationBuilder Interface Implementation "
	}
}