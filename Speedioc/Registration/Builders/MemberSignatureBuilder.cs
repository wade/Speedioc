using System;
using System.Collections.Generic;
using Speedioc.Registration.Core;

namespace Speedioc.Registration.Builders
{
	/// <summary>
	/// Builds a collection of <see cref="IParameter"/> instances that represent the 
	/// signature of <see cref="IConstructor"/> and <see cref="IMethod"/> instances.
	/// </summary>
	/// <seealso cref="ConstructorBuilder"/>
	/// <seealso cref="MethodBuilder"/>
	/// <seealso cref="IRegistrationBuilder"/>
	public abstract class MemberSignatureBuilder : IMemberSignatureBuilder
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MemberSignatureBuilder" /> class.
		/// </summary>
		/// <param name="registrationBuilder">
		/// A reference to the registration builder that created this <see cref="ConstructorBuilder"/> instance.
		/// </param>
		protected MemberSignatureBuilder(RegistrationBuilder registrationBuilder)
		{
			RegistrationBuilder = registrationBuilder;
			Parameters = new List<IParameter>();
		}

		/// <summary>
		/// Gets the parameters of the member signature built by the <see cref="MemberSignatureBuilder"/> instance.
		/// </summary>
		/// <value>
		/// The parameters of the member signature built by the <see cref="MemberSignatureBuilder"/> instance.
		/// </value>
		public List<IParameter> Parameters { get; private set; }

		/// <summary>
		/// Gets the reference to the <see cref="RegistrationBuilder"/> that created this <see cref="MemberSignatureBuilder"/> instance.
		/// </summary>
		/// <value>
		/// The <see cref="RegistrationBuilder"/> that created this <see cref="MemberSignatureBuilder"/> instance.
		/// </value>
		protected RegistrationBuilder RegistrationBuilder { get; private set; }

		/// <summary>
		/// Specifies a parameter that has its value provided by a delegate.
		/// </summary>
		/// <param name="type">The type of the parameter.</param>
		/// <param name="parameterValueFactory">The delegate that provides the parameter value.</param>
		/// <returns>
		/// An instance of <see cref="IMemberSignatureBuilder" /> that allows further method chaining
		/// to configure the parameters of the member.
		/// </returns>
		public IMemberSignatureBuilder WithValueFactoryParameter(Type type, Func<object> parameterValueFactory)
		{
			Parameters.Add(new ValueFactoryParameter(type, parameterValueFactory));
			return this;
		}

		/// <summary>
		/// Specifies a parameter that has its value provided by a delegate.
		/// </summary>
		/// <typeparam name="T">The type of the parameter.</typeparam>
		///<param name="parameterValueFactory">The delegate that provides the parameter value.</param>
		/// <returns>
		///		An instance of <see cref="IMemberSignatureBuilder"/> that allows further method chaining 
		///		to configure the parameters of the member.
		/// </returns>
		public IMemberSignatureBuilder WithValueFactoryParameter<T>(Func<object> parameterValueFactory)
		{
			return WithValueFactoryParameter(typeof(T), parameterValueFactory);
		}

		/// <summary>
		/// Specifies a parameter that has the specified value and type.
		/// </summary>
		/// <param name="parameterValue">The parameter value.</param>
		/// <returns>
		/// An instance of <see cref="IMemberSignatureBuilder" /> that allows further method chaining
		/// to configure the parameters of the member.
		/// </returns>
		public IMemberSignatureBuilder WithValueParameter(object parameterValue)
		{
			Parameters.Add(new ValueParameter(parameterValue));
			return this;
		}

		/// <summary>
		/// Specifies a parameter with a value that is resolved by the DrillBit's underlying
		/// resolver object (e.g. container) by the specified type.
		/// </summary>
		/// <param name="type">The type of the resolved parameter value.</param>
		/// <returns>
		/// An instance of <see cref="IMemberSignatureBuilder" /> that allows further method chaining
		/// to configure the parameters of the member.
		/// </returns>
		public IMemberSignatureBuilder WithResolvedParameter(Type type)
		{
			Parameters.Add(new ResolvedParameter(type));
			return this;
		}

		/// <summary>
		/// Specifies a parameter with a value that is resolved by the DrillBit's underlying
		/// resolver object (e.g. container) by the specified type and name.
		/// </summary>
		/// <param name="type">The type of the resolved parameter value.</param>
		/// <param name="name">The name of the registered instance.</param>
		/// <returns>
		/// An instance of <see cref="IMemberSignatureBuilder" /> that allows further method chaining
		/// to configure the parameters of the member.
		/// </returns>
		public IMemberSignatureBuilder WithResolvedParameter(Type type, string name)
		{
			Parameters.Add(new ResolvedParameter(type, name));
			return this;
		}

		/// <summary>
		/// Specifies a parameter with a value that is resolved by the DrillBit's underlying
		/// resolver object (e.g. container) by the specified type.
		/// </summary>
		/// <typeparam name="T">The type of the resolved parameter value.</typeparam>
		/// <returns>
		/// An instance of <see cref="IMemberSignatureBuilder" /> that allows further method chaining
		/// to configure the parameters of the member.
		/// </returns>
		public IMemberSignatureBuilder WithResolvedParameter<T>()
		{
			return WithResolvedParameter(typeof(T));
		}

		/// <summary>
		/// Specifies a parameter with a value that is resolved by the DrillBit's underlying
		/// resolver object (e.g. container) by the specified type and name.
		/// </summary>
		/// <typeparam name="T">The type of the resolved parameter value.</typeparam>
		/// <param name="name">The name of the registered instance.</param>
		/// <returns>
		/// An instance of <see cref="IMemberSignatureBuilder" /> that allows further method chaining
		/// to configure the parameters of the member.
		/// </returns>
		public IMemberSignatureBuilder WithResolvedParameter<T>(string name)
		{
			return WithResolvedParameter(typeof(T), name);
		}

		/// <summary>
		/// Specifies that the last parameter of the member's signature has been specified
		/// and returns the context back to the original <see cref="IRegistrationBuilder" />.
		/// </summary>
		/// <returns>
		/// An instance of <see cref="IRegistrationBuilder" /> that allows further method chaining
		/// of the fluent registration API.
		/// </returns>
		/// <remarks>
		/// When called, this method finalizes the member's signature and the context
		/// is returned to the original <see cref="IRegistrationBuilder" /> and no more
		/// parameters may be added to the member. If this method is called and no parameters
		/// were defined previously, the effect is essentially the same calling the
		/// <see cref="WithNoParameters" /> method and the method will be defined with no parameters.
		/// </remarks>
		public abstract IRegistrationBuilder AsLastParameter();

		/// <summary>
		/// Specifies that the member's signature has no parameters
		/// and returns the context back to the original <see cref="IRegistrationBuilder" />.
		/// instance.
		/// </summary>
		/// <returns>
		/// An instance of <see cref="IRegistrationBuilder" /> that allows further method chaining
		/// of the fluent registration API.
		/// </returns>
		/// <remarks>
		/// When called, any parameters that have been added previously for the member
		/// are explicitly discarded and the member will have no defined parameters.
		/// </remarks>
		public abstract IRegistrationBuilder WithNoParameters();
	}
}