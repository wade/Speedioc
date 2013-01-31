namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Represents a field that is set with a specified value after 
	/// an instance of the registered type is created.
	/// </summary>
	/// <seealso cref="IValueField"/>
	/// <seealso cref="IField"/>
	/// <seealso cref="ResolvedField"/>
	/// <seealso cref="ValueFactoryField"/>
	public class ValueField : IValueField
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValueField" /> class.
		/// </summary>
		/// <param name="name">The name of the field to be set.</param>
		/// <param name="value">The value to be set.</param>
		public ValueField(string name, object value)
		{
			Name = name;
			Value = value;
		}

		/// <summary>
		/// Gets the name of the field to be set.
		/// </summary>
		/// <value>
		/// The name of the field to be set.
		/// </value>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the value that should be set.
		/// </summary>
		/// <value>
		/// The value that should be set.
		/// </value>
		public object Value { get; private set; }
	}
}