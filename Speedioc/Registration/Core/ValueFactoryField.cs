using System;

namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Represents a field that is set with a value that is provided by the 
	/// specified delegate after an instance of the registered type is created.
	/// </summary>
	/// <seealso cref="IValueFactoryField"/>
	/// <seealso cref="IField"/>
	/// <seealso cref="ResolvedField"/>
	/// <seealso cref="ValueField"/>
	public class ValueFactoryField : IValueFactoryField
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValueFactoryField" /> class.
		/// </summary>
		/// <param name="name">The name of the field to be set.</param>
		/// <param name="type">The type of the field to be set.</param>
		/// <param name="valueFactory">The delegate that is called to get the value of the field.</param>
		public ValueFactoryField(string name, Type type, Func<object> valueFactory)
		{
			Name = name;
			Type = type;
			ValueFactory = valueFactory;
		}

		/// <summary>
		/// Gets the name of the field to be set.
		/// </summary>
		/// <value>
		/// The name of the field to be set.
		/// </value>
		public string Name { get; private set; }

		/// <summary>
		/// Gets or sets the type of the field.
		/// </summary>
		/// <value>
		/// The type of the field.
		/// </value>
		public Type Type { get; private set; }

		/// <summary>
		/// Gets the delegate that is called to get the value that is set on the field.
		/// </summary>
		/// <value>
		/// The delegate that is called to get the value that is set on the field.
		/// </value>
		public Func<object> ValueFactory { get; private set; }
	}
}