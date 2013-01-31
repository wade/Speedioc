using System;
using Speedioc.Registration.Core;

namespace Speedioc.Registration.Builders
{
	/// <summary>
	/// Builds the property configuration that specifies a property that 
	/// is set after an instance of the registered type is created.
	/// </summary>
	/// <seealso cref="IPropertyBuilder"/>
	/// <seealso cref="IRegistrationBuilder"/>
	public class PropertyBuilder : IPropertyBuilder
	{
		private readonly string _name;
		private readonly Type _type;
		private readonly RegistrationBuilder _registrationBuilder;

		/// <summary>
		/// Initializes a new instance of the <see cref="PropertyBuilder" /> class.
		/// </summary>
		/// <param name="registrationBuilder">A reference to the registration builder that created this <see cref="MethodBuilder" /> instance.</param>
		/// <param name="name">The name of the property to be set.</param>
		/// <param name="type">The type of the property to be set.</param>
		public PropertyBuilder(RegistrationBuilder registrationBuilder, string name, Type type)
		{
			_registrationBuilder = registrationBuilder;
			_name = name;
			_type = type;
		}

		/// <summary>
		/// Specifies that the property should be set to the specified value.
		/// </summary>
		/// <param name="value">The value of which the property should be set.</param>
		/// <returns>
		/// An instance of <see cref="IRegistrationBuilder" /> that allows further method chaining
		/// of the fluent registration API.
		/// This method ends the configuration of the property and returns the
		/// context back to the original type registration.
		/// </returns>
		public IRegistrationBuilder SetTo(object value)
		{
			return BuildProperty(new ValueProperty(_name, value));
		}

		/// <summary>
		/// Specifies that the property should be set to a value provided by a delegate..
		/// </summary>
		/// <param name="type">The type of the property.</param>
		/// <param name="propertyValueFactory">The delegate that provides the property value.</param>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		///		This method ends the configuration of the property and returns the 
		///		context back to the original type registration.
		/// </returns>
		public IRegistrationBuilder SetToValueFactory(Type type, Func<object> propertyValueFactory)
		{
			return BuildProperty(new ValueFactoryProperty(_name, type, propertyValueFactory));
		}

		/// <summary>
		/// Specifies that the property should be set to a value provided by a delegate..
		/// </summary>
		/// <typeparam name="T">The type of the property.</typeparam>
		/// <param name="propertyValueFactory">The delegate that provides the property value.</param>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		///		This method ends the configuration of the property and returns the 
		///		context back to the original type registration.
		/// </returns>
		public IRegistrationBuilder SetToValueFactory<T>(Func<object> propertyValueFactory)
		{
			return SetToValueFactory(typeof(T), propertyValueFactory);
		}

		/// <summary>
		/// Specifies that the property should be set to a value that is resolved
		/// by the DrillBit's underlying resolver object (e.g. container).
		/// </summary>
		/// <returns>
		/// The property's value will be resolved by its type.
		/// An instance of <see cref="IRegistrationBuilder" /> that allows further method chaining
		/// of the fluent registration API.
		/// This method ends the configuration of the property and returns the
		/// context back to the original type registration.
		/// </returns>
		public IRegistrationBuilder SetToResolvedValue()
		{
			return BuildProperty(new ResolvedProperty(_name, _type));
		}

		/// <summary>
		/// Specifies that the property should be set to a value that is resolved
		/// by the DrillBit's underlying resolver object (e.g. container).
		/// </summary>
		/// <param name="dependencyName">The registered name of the dependency.</param>
		/// <returns>
		/// The property's value will be resolved by its type.
		/// An instance of <see cref="IRegistrationBuilder" /> that allows further method chaining
		/// of the fluent registration API.
		/// This method ends the configuration of the property and returns the
		/// context back to the original type registration.
		/// </returns>
		public IRegistrationBuilder SetToResolvedValue(string dependencyName)
		{
			return BuildProperty(new ResolvedProperty(_name, _type, dependencyName));
		}

		private IRegistrationBuilder BuildProperty(IProperty property)
		{
			_registrationBuilder.Members.Add(property);
			return _registrationBuilder;
		}
	}
}