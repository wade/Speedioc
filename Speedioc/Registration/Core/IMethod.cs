using System.Collections.Generic;

namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Implemented by types that specify a method to be called after a new instance of the registered type has been created.
	/// </summary>
	/// <seealso cref="IMember"/>
	/// <seealso cref="IConstructor"/>
	/// <seealso cref="IProperty"/>
	public interface IMethod : INamedMember
	{
		/// <summary>
		/// Gets the collection of method parameters.
		/// </summary>
		/// <value>
		/// The collection of method parameters.
		/// </value>
		/// <remarks>
		///		The method parameters represent are the arguments 
		///		that are passed to the method that is called after a 
		///		new instance of the registered type is created. 
		///		The number of parameters, their types and order must 
		///		match the method's signature exactly.
		/// </remarks>
		IList<IParameter> Parameters { get; }
	}
}