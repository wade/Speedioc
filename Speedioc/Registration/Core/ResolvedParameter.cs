using System;
using Speedioc.Registration.Builders;

namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Represents a parameter parameter with a value that is a resolved dependency.
	/// </summary>
	/// <seealso cref="IResolvedParameter"/>
	/// <seealso cref="IParameter"/>
	/// <seealso cref="IConstructor"/>
	/// <seealso cref="IMethod"/>
	/// <seealso cref="IMemberSignatureBuilder"/>
	/// <seealso cref="ValueParameter"/>
	public class ResolvedParameter : IResolvedParameter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ResolvedParameter" /> class.
		/// </summary>
		/// <param name="dependencyType">The type of the dependency to be resolved.</param>
		public ResolvedParameter(Type dependencyType)
			: this (dependencyType, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ResolvedParameter" /> class.
		/// </summary>
		/// <param name="dependencyType">The type of the dependency to be resolved.</param>
		/// <param name="dependencyName">The registered name of the dependency to be resolved.</param>
		public ResolvedParameter(Type dependencyType, string dependencyName)
		{
			DependencyType = dependencyType;
			DependencyName = dependencyName;
		}

		/// <summary>
		/// Gets or sets the registered name of the dependency to be resolved.
		/// </summary>
		/// <value>
		/// The name of the dependency to be resolved.
		/// </value>
		/// <remarks>
		///		When resolving a dependency that does not have a registered name,
		///		this property must be null or be an empty string.
		///		This is not the name of the parameter.
		///		The name of the parameter is not used because the parameter
		///		is determined by its context (order and type).
		/// </remarks>
		public string DependencyName { get; set; }

		/// <summary>
		/// Gets or sets the type of the dependency to be resolved.
		/// </summary>
		/// <value>
		/// The type of the dependency to be resolved.
		/// </value>
		/// <remarks>
		///		The DependencyType property is the type to be resolved and must also be
		///		the correct type of the parameter via an implicit cast.
		///		If the instance of the dependency to be resolved is named, the <see cref="DependencyName" />
		///		property must also be specified, otherwise it must be null or an empty string.
		/// </remarks>
		public Type DependencyType { get; set; }

		public override string ToString()
		{
			string dependencyName = string.IsNullOrEmpty(DependencyName) ? "<Not_Specified>" : string.Format("\"{0}\"", DependencyName);
			return string.Format("{0}: DependencyType={1}, DependencyName={2}", GetType().FullName, DependencyType.FullName, dependencyName);
		}

		/// <summary>
		/// Gets the name of the member.
		/// </summary>
		/// <value>
		/// The name of the member.
		/// </value>
		public string Name { get; set; }
	}
}