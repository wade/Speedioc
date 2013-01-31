using System;

namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Represents a property that is set with a value that is provided by the 
	/// specified delegate after an instance of the registered type is created.
	/// </summary>
	/// <seealso cref="IValueFactoryProperty"/>
	/// <seealso cref="IProperty"/>
	/// <seealso cref="IResolvedProperty"/>
	/// <seealso cref="IValueProperty"/>
	public class ValueFactoryProperty : IValueFactoryProperty
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValueFactoryProperty" /> class.
		/// </summary>
		/// <param name="name">The name of the property to be set.</param>
		/// <param name="type">The type of the property to be set.</param>
		/// <param name="valueFactory">The delegate that is called to get the value of the property.</param>
		public ValueFactoryProperty(string name, Type type, Func<object> valueFactory)
		{
			Name = name;
			Type = type;
			ValueFactory = valueFactory;
		}

		/// <summary>
		/// Gets the name of the property to be set.
		/// </summary>
		/// <value>
		/// The name of the property to be set.
		/// </value>
		public string Name { get; private set; }

		/// <summary>
		/// Gets or sets the type of the property.
		/// </summary>
		/// <value>
		/// The type of the property.
		/// </value>
		public Type Type { get; private set; }

		/// <summary>
		/// Gets the delegate that is called to get the value that is set on the property.
		/// </summary>
		/// <value>
		/// The delegate that is called to get the value that is set on the property.
		/// </value>
		public Func<object> ValueFactory { get; private set; }
	}
}