using System;
using Speedioc.Registration.Core;

namespace Speedioc.Registration.Builders
{
	/// <summary>
	/// Implemented by classes that build <see cref="IField"/> instances.
	/// </summary>
	/// <remarks>
	///		<see cref="IFieldBuilder"/> is used to define how to set the 
	///		field's value or the actual field value. Once one of the 
	///		methods is called, the context must be returned back to the 
	///		original <see cref="IRegistrationBuilder"/> instance that created 
	///		the <see cref="IFieldBuilder"/> instance.
	/// </remarks>
	/// <seealso cref="IRegistrationBuilder"/>
	/// <seealso cref="IField"/>
	/// <seealso cref="IResolvedField"/>
	/// <seealso cref="IValueFactoryField"/>
	/// <seealso cref="IValueField"/>
	public interface IFieldBuilder
	{
		/// <summary>
		/// Specifies that the field should be set to the specified value.
		/// </summary>
		/// <param name="value">The value of which the field should be set.</param>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		///		This method ends the configuration of the field and returns the 
		///		context back to the original type registration.
		/// </returns>
		IRegistrationBuilder SetTo(object value);

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
		IRegistrationBuilder SetToValueFactory(Type type, Func<object> fieldValueFactory);

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
		IRegistrationBuilder SetToValueFactory<T>(Func<object> fieldValueFactory);

		/// <summary>
		/// Specifies that the field should be set to a value that is resolved 
		/// by the DrillBit's underlying resolver object (e.g. container).
		/// </summary>
		/// <returns>
		///		The field's value will be resolved by its type. 
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		///		This method ends the configuration of the field and returns the 
		///		context back to the original type registration.
		/// </returns>
		IRegistrationBuilder SetToResolvedValue();

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
		IRegistrationBuilder SetToResolvedValue(string dependencyName);
	}
}