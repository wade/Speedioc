namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Represents a property that is set with a specified value after 
	/// an instance of the registered type is created.
	/// </summary>
	/// <seealso cref="IValueProperty"/>
	/// <seealso cref="IProperty"/>
	/// <seealso cref="IResolvedProperty"/>
	/// <seealso cref="IValueFactoryProperty"/>
	public class ValueProperty : IValueProperty
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValueProperty" /> class.
		/// </summary>
		/// <param name="name">The name of the property to be set.</param>
		/// <param name="value">The value to be set.</param>
		public ValueProperty(string name, object value)
		{
			Name = name;
			Value = value;
		}

		/// <summary>
		/// Gets the name of the property to be set.
		/// </summary>
		/// <value>
		/// The name of the property to be set.
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