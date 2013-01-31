using System;

namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Represents a field with a value that is a resolved dependency.
	/// </summary>
	/// <seealso cref="IResolvedField"/>
	/// <seealso cref="IField"/>
	/// <seealso cref="ValueField"/>
	/// <seealso cref="ValueFactoryField"/>
	public class ResolvedField : IResolvedField
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ResolvedField" /> class.
		/// </summary>
		/// <param name="name">The name of the field to be set.</param>
		/// <param name="type">The type of the dependency to be resolved.</param>
		/// <param name="dependencyName">The registered name of the dependency to be resolved.</param>
		public ResolvedField(string name, Type type, string dependencyName = null)
		{
			DependencyName = dependencyName;
			Name = name;
			DependencyType = type;
		}

		/// <summary>
		/// Gets the registered name of the dependency to be resolved.
		/// </summary>
		/// <value>
		/// The registered name of the dependency to be resolved.
		/// </value>
		/// <remarks>
		///		When resolving a dependency that does not have a registered name,
		///		this field must be null or be an empty string.
		///		This is not the name of the field.
		/// </remarks>
		public string DependencyName { get; private set; }

		/// <summary>
		/// Gets the name of the field to be set.
		/// </summary>
		/// <value>
		/// The name of the field to be set.
		/// </value>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the type of the dependency to be resolved.
		/// </summary>
		/// <value>
		/// The type of the dependency to be resolved.
		/// </value>
		/// <remarks>
		///		The DependencyType field is the type to be resolved and must also be
		///		the correct type of the field via an implicit cast.
		///		If the instance of the dependency to be resolved is named, the <see cref="DependencyName" />
		///		field must also be specified, otherwise it must be null or an empty string.
		/// </remarks>
		public Type DependencyType { get; private set; }
	}
}