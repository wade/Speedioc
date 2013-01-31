using System.Collections.Generic;
using Speedioc.Registration.Core;

namespace Speedioc.Registration.Builders
{
	/// <summary>
	/// Builds the method configuration that specifies a method that 
	/// is called after an instance of the registered type is created.
	/// </summary>
	/// <seealso cref="MemberSignatureBuilder"/>
	/// <seealso cref="Method"/>
	/// <seealso cref="IMethod"/>
	/// <seealso cref="IRegistrationBuilder"/>
	public class MethodBuilder : MemberSignatureBuilder
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MethodBuilder" /> class.
		/// </summary>
		/// <param name="registrationBuilder">
		/// A reference to the registration builder that created this <see cref="MethodBuilder"/> instance.
		/// </param>
		/// <param name="name">The name of the method to be invoked.</param>
		public MethodBuilder(RegistrationBuilder registrationBuilder, string name) 
			: base(registrationBuilder)
		{
			Name = name;
		}

		/// <summary>
		/// Gets the name of the method to be invoked.
		/// </summary>
		/// <value>
		/// The name of the method to be invoked.
		/// </value>
		public string Name { get; private set; }

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
			return BuildMethod(Parameters);
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
			return BuildMethod(null);
		}

		/// <summary>
		/// Builds the method instance and sets the <see cref="IRegistrationBuilder"/> instance's 
		/// Members collection property.
		/// </summary>
		/// <param name="parameters">The method's parameters.</param>
		/// <returns>
		///		An instance of <see cref="IRegistrationBuilder"/> that allows further method chaining 
		///		of the fluent registration API.
		/// </returns>
		private IRegistrationBuilder BuildMethod(IEnumerable<IParameter> parameters)
		{
			RegistrationBuilder.Members.Add(new Method(Name, parameters));
			return RegistrationBuilder;
		}
	}
}