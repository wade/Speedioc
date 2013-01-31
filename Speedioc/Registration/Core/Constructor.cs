using System.Collections.Generic;

namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Specifies the constructor to be used to create instances of the registered type.
	/// </summary>
	/// <seealso cref="IConstructor"/>
	/// <seealso cref="IRegistration"/>
	public class Constructor : IConstructor
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Constructor" /> class.
		/// </summary>
		/// <param name="parameters">
		/// The constructor parameters that match the specific constructor signature to be used.
		/// </param>
		public Constructor(IEnumerable<IParameter> parameters)
		{
			List<IParameter> list = new List<IParameter>();
			if (null != parameters)
			{
				list.AddRange(parameters);
			}
			Parameters = list;
		}

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
		public IList<IParameter> Parameters { get; private set; }

		/// <summary>
		/// Gets the name of the member.
		/// </summary>
		/// <value>
		/// The name of the member.
		/// </value>
		/// <remarks>
		///		This property always returns null as constructors do not have a name.
		/// </remarks>
		public string Name { get { return null; } }
	}
}