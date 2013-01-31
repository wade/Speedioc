using System;

namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Implemented by classes that represent a member with a value that is a resolved dependency.
	/// </summary>
	/// <seealso cref="IMember"/>
	public interface IResolvedMember : IMember
	{
		/// <summary>
		/// Gets the registered name of the dependency to be resolved.
		/// </summary>
		/// <value>
		/// The registered name of the dependency to be resolved.
		/// </value>
		/// <remarks>
		///		When resolving a dependency that does not have a registered name, 
		///		this member must be null or be an empty string. 
		///		This is not the name of the member.
		/// </remarks>
		string DependencyName { get; }

		/// <summary>
		/// Gets the type of the dependency to be resolved.
		/// </summary>
		/// <value>
		/// The type of the dependency to be resolved.
		/// </value>
		/// <remarks>
		///		The DependencyType member is the type to be resolved and must also be 
		///		the correct type of the member via an implicit cast. 
		///		If the instance of the dependency to be resolved is named, the <see cref="DependencyName"/> 
		///		member must also be specified, otherwise it must be null or an empty string.
		/// </remarks>
		Type DependencyType { get; }
	}
}