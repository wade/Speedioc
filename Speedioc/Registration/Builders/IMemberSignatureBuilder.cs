using System;
using Speedioc.Registration.Core;

namespace Speedioc.Registration.Builders
{
	/// <summary>
	/// Implemented by classes that build a collection of <see cref="IParameter"/> instances 
	/// that are used with instances of <see cref="IConstructor"/> and <see cref="IMethod"/>.
	/// </summary>
	/// <remarks>
	///		<para>
	///			<see cref="IMemberSignatureBuilder"/> is used to define the parameters 
	///			(or arguments) of a constructor or method member. If the constructor or 
	///			method does not have any parameters, the <see cref="WithNoParameters"/> 
	///			method should be called. The other methods are called in a chain to 
	///			define the parameters of the member in the same order and with the same 
	///			types as defined by the member's signature. After the last parameter has 
	///			been defined, the <see cref="AsLastParameter"/> method must be called to 
	///			end the definition of the member's signature.
	///		</para>
	///		<para>
	///			Once either <see cref="WithNoParameters"/> or <see cref="AsLastParameter"/> 
	///			is called, the context must be switched back to the original <see cref="IRegistrationBuilder"/> 
	///			that created the <see cref="IMemberSignatureBuilder"/> instance to allow further 
	///			configuration of the type being registered.
	///		</para>
	/// </remarks>
	/// <seealso cref="IRegistrationBuilder"/>
	/// <seealso cref="IParameter"/>
	/// <seealso cref="IResolvedParameter"/>
	/// <seealso cref="IValueParameter"/>
	/// <seealso cref="IConstructor"/>
	/// <seealso cref="IMethod"/>
	public interface IMemberSignatureBuilder
	{
		/// <summary>
		/// Specifies a parameter that has its value provided by a delegate.
		/// </summary>
		///<param name="type">The type of the parameter.</param>
		///<param name="parameterValueFactory">The delegate that provides the parameter value.</param>
		/// <returns>
		///		An instance of <see cref="IMemberSignatureBuilder"/> that allows further method chaining 
		///		to configure the parameters of the member.
		/// </returns>
		IMemberSignatureBuilder WithValueFactoryParameter(Type type, Func<object> parameterValueFactory);

		/// <summary>
		/// Specifies a parameter that has its value provided by a delegate.
		/// </summary>
		/// <typeparam name="T">The type of the parameter.</typeparam>
		///<param name="parameterValueFactory">The delegate that provides the parameter value.</param>
		/// <returns>
		///		An instance of <see cref="IMemberSignatureBuilder"/> that allows further method chaining 
		///		to configure the parameters of the member.
		/// </returns>
		IMemberSignatureBuilder WithValueFactoryParameter<T>(Func<object> parameterValueFactory);

		/// <summary>
		/// Specifies a parameter that has the specified value and type.
		/// </summary>
		/// <param name="parameterValue">The parameter value.</param>
		/// <returns>
		///		An instance of <see cref="IMemberSignatureBuilder"/> that allows further method chaining 
		///		to configure the parameters of the member.
		/// </returns>
		IMemberSignatureBuilder WithValueParameter(object parameterValue);

		/// <summary>
		/// Specifies a parameter with a value that is resolved by the DrillBit's underlying 
		/// resolver object (e.g. container) by the specified type.
		/// </summary>
		/// <param name="type">The type of the resolved parameter value.</param>
		/// <returns>
		///		An instance of <see cref="IMemberSignatureBuilder"/> that allows further method chaining 
		///		to configure the parameters of the member.
		/// </returns>
		IMemberSignatureBuilder WithResolvedParameter(Type type);

		/// <summary>
		/// Specifies a parameter with a value that is resolved by the DrillBit's underlying 
		/// resolver object (e.g. container) by the specified type and name.
		/// </summary>
		/// <param name="type">The type of the resolved parameter value.</param>
		/// <param name="name">The name of the registered instance.</param>
		/// <returns>
		///		An instance of <see cref="IMemberSignatureBuilder"/> that allows further method chaining 
		///		to configure the parameters of the member.
		/// </returns>
		IMemberSignatureBuilder WithResolvedParameter(Type type, string name);

		/// <summary>
		/// Specifies a parameter with a value that is resolved by the DrillBit's underlying 
		/// resolver object (e.g. container) by the specified type.
		/// </summary>
		/// <typeparam name="T">The type of the resolved parameter value.</typeparam>
		/// <returns>
		///		An instance of <see cref="IMemberSignatureBuilder"/> that allows further method chaining 
		///		to configure the parameters of the member.
		/// </returns>
		IMemberSignatureBuilder WithResolvedParameter<T>();

		/// <summary>
		/// Specifies a parameter with a value that is resolved by the DrillBit's underlying 
		/// resolver object (e.g. container) by the specified type and name.
		/// </summary>
		/// <typeparam name="T">The type of the resolved parameter value.</typeparam>
		/// <param name="name">The name of the dependency to be resolved.</param>
		/// <returns>
		///		An instance of <see cref="IMemberSignatureBuilder"/> that allows further method chaining 
		///		to configure the parameters of the member.
		/// </returns>
		IMemberSignatureBuilder WithResolvedParameter<T>(string name);

		/// <summary>
		/// Specifies that the last parameter of the member's signature has been specified 
		/// and returns the context back to the original <see cref="IRegistrationBuilder"/>.
		/// </summary>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		/// </returns>
		/// <remarks>
		///		When called, this method finalizes the member's signature and the context 
		///		is returned to the original <see cref="IRegistrationBuilder"/> and no more 
		///		parameters may be added to the member. If this method is called and no parameters 
		///		were defined previously, the effect is essentially the same calling the 
		///		<see cref="WithNoParameters"/> method and the method will be defined with no parameters.
		/// </remarks>
		IRegistrationBuilder AsLastParameter();
		
		/// <summary>
		/// Specifies that the member's signature has no parameters 
		/// and returns the context back to the original <see cref="IRegistrationBuilder"/>.
		/// instance.
		/// </summary>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		/// </returns>
		/// <remarks>
		///		When called, any parameters that have been added previously for the member 
		///		are explicitly discarded and the member will have no defined parameters.
		/// </remarks>
		IRegistrationBuilder WithNoParameters();
	}
}