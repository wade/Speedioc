using System;
using Speedioc.Registration.Core;

namespace Speedioc.Registration.Builders
{
	/// <summary>
	/// Builds the field configuration that specifies a field that 
	/// is set after an instance of the registered type is created.
	/// </summary>
	/// <seealso cref="IFieldBuilder"/>
	/// <seealso cref="IRegistrationBuilder"/>
	public class FieldBuilder : IFieldBuilder
	{
		private readonly string _name;
		private readonly Type _type;
		private readonly RegistrationBuilder _registrationBuilder;

		/// <summary>
		/// Initializes a new instance of the <see cref="FieldBuilder" /> class.
		/// </summary>
		/// <param name="registrationBuilder">A reference to the registration builder that created this <see cref="MethodBuilder" /> instance.</param>
		/// <param name="name">The name of the field to be set.</param>
		/// <param name="type">The type of the field to be set.</param>
		public FieldBuilder(RegistrationBuilder registrationBuilder, string name, Type type)
		{
			_registrationBuilder = registrationBuilder;
			_name = name;
			_type = type;
		}

		/// <summary>
		/// Specifies that the field should be set to the specified value.
		/// </summary>
		/// <param name="value">The value of which the field should be set.</param>
		/// <returns>
		/// An instance of <see cref="IRegistrationBuilder" /> that allows further method chaining
		/// of the fluent registration API.
		/// This method ends the configuration of the field and returns the
		/// context back to the original type registration.
		/// </returns>
		public IRegistrationBuilder SetTo(object value)
		{
			return BuildField(new ValueField(_name, value));
		}

		/// <summary>
		/// Specifies that the field should be set to a value provided by a delegate..
		/// </summary>
		/// <param name="type">The type of the field.</param>
		/// <param name="fieldValueFactory">The delegate that provides the field value.</param>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		///		This method ends the configuration of the field and returns the 
		///		context back to the original type registration.
		/// </returns>
		public IRegistrationBuilder SetToValueFactory(Type type, Func<object> fieldValueFactory)
		{
			return BuildField(new ValueFactoryField(_name, type, fieldValueFactory));
		}

		/// <summary>
		/// Specifies that the field should be set to a value provided by a delegate..
		/// </summary>
		/// <typeparam name="T">The type of the field.</typeparam>
		/// <param name="fieldValueFactory">The delegate that provides the field value.</param>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		///		This method ends the configuration of the field and returns the 
		///		context back to the original type registration.
		/// </returns>
		public IRegistrationBuilder SetToValueFactory<T>(Func<object> fieldValueFactory)
		{
			return SetToValueFactory(typeof(T), fieldValueFactory);
		}

		/// <summary>
		/// Specifies that the field should be set to a value that is resolved
		/// by the DrillBit's underlying resolver object (e.g. container).
		/// </summary>
		/// <returns>
		/// The field's value will be resolved by its type.
		/// An instance of <see cref="IRegistrationBuilder" /> that allows further method chaining
		/// of the fluent registration API.
		/// This method ends the configuration of the field and returns the
		/// context back to the original type registration.
		/// </returns>
		public IRegistrationBuilder SetToResolvedValue()
		{
			return BuildField(new ResolvedField(_name, _type));
		}

		/// <summary>
		/// Specifies that the field should be set to a value that is resolved
		/// by the DrillBit's underlying resolver object (e.g. container).
		/// </summary>
		/// <param name="dependencyName">The registered name of the dependency.</param>
		/// <returns>
		/// The field's value will be resolved by its type.
		/// An instance of <see cref="IRegistrationBuilder" /> that allows further method chaining
		/// of the fluent registration API.
		/// This method ends the configuration of the field and returns the
		/// context back to the original type registration.
		/// </returns>
		public IRegistrationBuilder SetToResolvedValue(string dependencyName)
		{
			return BuildField(new ResolvedField(_name, _type, dependencyName));
		}

		private IRegistrationBuilder BuildField(IField field)
		{
			_registrationBuilder.Members.Add(field);
			return _registrationBuilder;
		}
	}
}