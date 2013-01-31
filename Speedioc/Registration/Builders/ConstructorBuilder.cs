using System.Collections.Generic;
using Speedioc.Registration.Core;

namespace Speedioc.Registration.Builders
{
	/// <summary>
	/// Builds the constructor configuration that specifies the constructor 
	/// to be used to create instances of the registered type.
	/// </summary>
	/// <seealso cref="MemberSignatureBuilder"/>
	/// <seealso cref="Constructor"/>
	/// <seealso cref="IConstructor"/>
	/// <seealso cref="IRegistrationBuilder"/>
	public class ConstructorBuilder : MemberSignatureBuilder
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConstructorBuilder" /> class.
		/// </summary>
		/// <param name="registrationBuilder">
		/// A reference to the <see cref="RegistrationBuilder"/> that created this <see cref="ConstructorBuilder"/> instance.
		/// </param>
		public ConstructorBuilder(RegistrationBuilder registrationBuilder) 
			: base(registrationBuilder)
		{
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
		public override IRegistrationBuilder AsLastParameter()
		{
			return BuildConstructor(Parameters);
		}

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
		public override IRegistrationBuilder WithNoParameters()
		{
			return BuildConstructor(null);
		}

		/// <summary>
		/// Builds the constructor instance and sets the <see cref="IRegistrationBuilder"/> instance's 
		/// Constructor property.
		/// </summary>
		/// <param name="parameters">The constructor's parameters.</param>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		/// </returns>
		private IRegistrationBuilder BuildConstructor(IEnumerable<IParameter> parameters)
		{
			RegistrationBuilder.Constructor = new Constructor(parameters);
			return RegistrationBuilder;
		}
	}
}