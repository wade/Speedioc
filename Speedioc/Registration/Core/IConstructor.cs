using System.Collections.Generic;
using Speedioc.Registration.Builders;

namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Implemented by types that specify the constructor to be used to create instances of the registered type.
	/// </summary>
	/// <seealso cref="IMember"/>
	/// <seealso cref="IMethod"/>
	/// <seealso cref="IProperty"/>
	/// <seealso cref="IRegistration"/>
	/// <seealso cref="IRegistrationBuilder"/>
	public interface IConstructor : IMember
	{
		/// <summary>
		/// Gets the collection of constructor parameters.
		/// </summary>
		/// <value>
		/// The collection of constructor parameters.
		/// </value>
		/// <remarks>
		///		The constructor parameters represent are the arguments 
		///		that are passed to the constructor when a new instance 
		///		of the registered type is created. The number of parameters, 
		///		their types and order must match the constructor's signature 
		///		exactly. To use the default, parameterless constructor, the 
		///		Parameters collection must be empty.
		/// </remarks>
		IList<IParameter> Parameters { get; }
	}
}