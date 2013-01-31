using System;
using Speedioc.Registration.Core;

namespace Speedioc.Registration.Builders
{
	/// <summary>
	/// Implemented by classes that build <see cref="IProperty"/> instances.
	/// </summary>
	/// <remarks>
	///		<see cref="IPropertyBuilder"/> is used to define how to set the 
	///		property's value or the actual property value. Once one of the 
	///		methods is called, the context must be returned back to the 
	///		original <see cref="IRegistrationBuilder"/> instance that created 
	///		the <see cref="IPropertyBuilder"/> instance.
	/// </remarks>
	/// <seealso cref="IRegistrationBuilder"/>
	/// <seealso cref="IProperty"/>
	/// <seealso cref="IResolvedProperty"/>
	/// <seealso cref="IValueProperty"/>
	public interface IPropertyBuilder
	{
		/// <summary>
		/// Specifies that the property should be set to the specified value.
		/// </summary>
		/// <param name="value">The value of which the property should be set.</param>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		///		This method ends the configuration of the property and returns the 
		///		context back to the original type registration.
		/// </returns>
		IRegistrationBuilder SetTo(object value);

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
		IRegistrationBuilder SetToValueFactory(Type type, Func<object> propertyValueFactory);

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
		IRegistrationBuilder SetToValueFactory<T>(Func<object> propertyValueFactory);

		/// <summary>
		/// Specifies that the property should be set to a value that is resolved 
		/// by the DrillBit's underlying resolver object (e.g. container).
		/// </summary>
		/// <returns>
		///		The property's value will be resolved by its type. 
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		///		This method ends the configuration of the property and returns the 
		///		context back to the original type registration.
		/// </returns>
		IRegistrationBuilder SetToResolvedValue();

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
		IRegistrationBuilder SetToResolvedValue(string dependencyName);
	}
}