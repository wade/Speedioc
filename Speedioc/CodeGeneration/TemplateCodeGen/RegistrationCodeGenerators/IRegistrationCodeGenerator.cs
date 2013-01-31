using Speedioc.Registration.Core;

namespace Speedioc.CodeGeneration.TemplateCodeGen.RegistrationCodeGenerators
{
	/// <summary>
	/// Implemented by classes that perform code generation for an <see cref="IRegistration"/> instance.
	/// </summary>
	/// <remarks>
	///		The implementation must provide a mechanism to obtain the <see cref="TemplateRegistrationMetadata"/> 
	///		instance before the Generate method is invoked. The <see cref="TemplateRegistrationMetadata"/> 
	///		instance is usually passed in through the constructor and contains a reference to the 
	///		<see cref="IRegistration"/> instance that contains the source information for the code to be 
	///		generated. The generated code is not returned but is placed into the appropriate properties of 
	///		the <see cref="TemplateRegistrationMetadata"/> instance and is further processed by the core 
	///		code generator, an instance of <see cref="TemplateContainerGenerator"/>.
	/// </remarks>
	/// <seealso cref="IRegistration"/>
	/// <seealso cref="TemplateRegistrationMetadata"/>
	public interface IRegistrationCodeGenerator
	{
		/// <summary>
		/// Generates the code.
		/// </summary>
		void Generate();
	}
}