using System.Collections.Generic;

namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Specifies a method to be called after a new instance of the registered type has been created.
	/// </summary>
	/// <seealso cref="IMethod"/>
	/// <seealso cref="IRegistration"/>
	public class Method : IMethod
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Method" /> class.
		/// </summary>
		/// <param name="name">The name of the method to be invoked.</param>
		/// <param name="parameters">
		/// The method parameters that match the specific method signature to be used.
		/// </param>
		public Method(string name, IEnumerable<IParameter> parameters)
		{
			List<IParameter> list = new List<IParameter>();
			if (null != parameters)
			{
				list.AddRange(parameters);
			}
			Parameters = list;
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
		/// Gets the collection of method parameters.
		/// </summary>
		/// <value>
		/// The collection of method parameters.
		/// </value>
		/// <remarks>
		/// The method parameters represent are the arguments
		/// that are passed to the method that is called after a
		/// new instance of the registered type is created.
		/// The number of parameters, their types and order must
		/// match the method's signature exactly.
		/// </remarks>
		public IList<IParameter> Parameters { get; private set; }
	}
}