using Speedioc.Registration.Builders;

namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Represents a parameter with a specified value.
	/// </summary>
	/// <seealso cref="IValueParameter"/>
	/// <seealso cref="IResolvedParameter"/>
	/// <seealso cref="IParameter"/>
	/// <seealso cref="IConstructor"/>
	/// <seealso cref="IMethod"/>
	/// <seealso cref="IMemberSignatureBuilder"/>
	/// <seealso cref="ResolvedParameter"/>
	public class ValueParameter : IValueParameter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValueParameter" /> class.
		/// </summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="value">The parameter value.</param>
		public ValueParameter(string name, object value)
		{
			Name = name;
			Value = value;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValueParameter" /> class.
		/// </summary>
		/// <param name="value">The parameter value.</param>
		public ValueParameter(object value)
		{
			Value = value;
		}

		/// <summary>
		/// Gets the name of the parameter.
		/// </summary>
		/// <value>
		/// The name of the parameter.
		/// </value>
		public string Name { get; private set; }

		/// <summary>
		/// Gets or sets the value of the parameter.
		/// </summary>
		/// <value>
		/// The value of the parameter.
		/// </value>
		/// <remarks>
		/// The type of the value must match the expected parameter
		/// type and position in the order of parameters.
		/// </remarks>
		public object Value { get; set; }

		public override string ToString()
		{
			string parameterName = string.IsNullOrEmpty(Name) ? "<Not_Specified>" : string.Format("\"{0}\"", Name);
			string valueTypeName = (null == Value) ? "{null}" : Value.GetType().FullName;
			string valueToString = (null == Value) ? "{null}" : string.Format("\"{0}\"", Value);
			return string.Format("{0}: Name={1}, Value.GetType()={2}, Value.ToString()={3}", GetType().FullName, parameterName, valueTypeName, valueToString);
		}
	}
}