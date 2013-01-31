using System;

namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Represents a property with a value that is a resolved dependency.
	/// </summary>
	/// <seealso cref="IResolvedProperty"/>
	/// <seealso cref="IProperty"/>
	/// <seealso cref="IValueProperty"/>
	/// <seealso cref="IValueFactoryProperty"/>
	public class ResolvedProperty : IResolvedProperty
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ResolvedProperty" /> class.
		/// </summary>
		/// <param name="name">The name of the property to be set.</param>
		/// <param name="type">The type of the dependency to be resolved.</param>
		/// <param name="dependencyName">The registered name of the dependency to be resolved.</param>
		public ResolvedProperty(string name, Type type, string dependencyName = null)
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
		///		this property must be null or be an empty string.
		///		This is not the name of the property.
		/// </remarks>
		public string DependencyName { get; private set; }

		/// <summary>
		/// Gets the name of the property to be set.
		/// </summary>
		/// <value>
		/// The name of the property to be set.
		/// </value>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the type of the dependency to be resolved.
		/// </summary>
		/// <value>
		/// The type of the dependency to be resolved.
		/// </value>
		/// <remarks>
		///		The DependencyType property is the type to be resolved and must also be
		///		the correct type of the property via an implicit cast.
		///		If the instance of the dependency to be resolved is named, the <see cref="DependencyName" />
		///		property must also be specified, otherwise it must be null or an empty string.
		/// </remarks>
		public Type DependencyType { get; private set; }
	}
}