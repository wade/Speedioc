using System;
using Speedioc.Registration.Builders;

namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Represents a parameter with a value that is provided by the specified delegate.
	/// </summary>
	/// <seealso cref="IValueFactoryParameter"/>
	/// <seealso cref="IResolvedParameter"/>
	/// <seealso cref="IValueParameter"/>
	/// <seealso cref="IParameter"/>
	/// <seealso cref="IConstructor"/>
	/// <seealso cref="IMethod"/>
	/// <seealso cref="IMemberSignatureBuilder"/>
	/// <seealso cref="ResolvedParameter"/>
	/// <seealso cref="ValueParameter"/>
	public class ValueFactoryParameter : IValueFactoryParameter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValueParameter" /> class.
		/// </summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="type">The type of the parameter.</param>
		/// <param name="valueFactory">The delegate that is called to get the value of the parameter.</param>
		public ValueFactoryParameter(string name, Type type, Func<object> valueFactory)
		{
			Name = name;
			Type = type;
			ValueFactory = valueFactory;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValueParameter" /> class.
		/// </summary>
		/// <param name="type">The type of the parameter.</param>
		/// <param name="valueFactory">The delegate that is called to get the value of the parameter.</param>
		public ValueFactoryParameter(Type type, Func<object> valueFactory)
		{
			Type = type;
			ValueFactory = valueFactory;
		}

		/// <summary>
		/// Gets the name of the parameter.
		/// </summary>
		/// <value>
		/// The name of the parameter.
		/// </value>
		public string Name { get; private set; }

		/// <summary>
		/// Gets or sets the type of the parameter.
		/// </summary>
		/// <value>
		/// The type of the parameter.
		/// </value>
		public Type Type { get; private set; }

		/// <summary>
		/// Gets or sets the delegate that is called to get the value of the parameter.
		/// </summary>
		/// <value>
		/// The delegate that is called to get the value of the parameter.
		/// </value>
		/// <remarks>
		///		The type of the resulting value of the delegate must match the expected 
		///		parameter type and position in the order of parameters.
		/// </remarks>
		public Func<object> ValueFactory { get; private set; }

		public override string ToString()
		{
			string parameterName = string.IsNullOrEmpty(Name) ? "<Not_Specified>" : string.Format("\"{0}\"", Name);

			return string.Format("{0}: Type={1}, Parameter Name={2}", GetType().FullName, Type.FullName, parameterName);
		}
	}
}