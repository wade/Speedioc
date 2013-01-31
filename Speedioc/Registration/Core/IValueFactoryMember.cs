using System;

namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Implemented by classes that represent a member with a specified value.
	/// </summary>
	/// <seealso cref="IMember"/>
	public interface IValueFactoryMember : IMember
	{
		/// <summary>
		/// Gets or sets the type of the member.
		/// </summary>
		/// <value>
		/// The type of the member.
		/// </value>
		Type Type { get; }

		/// <summary>
		/// Gets or sets the delegate that is called to get the value of the member.
		/// </summary>
		/// <value>
		/// The delegate that is called to get the value of the member.
		/// </value>
		/// <remarks>
		///		The type of the resulting value of the delegate must match the expected member type.
		/// </remarks>
		Func<object> ValueFactory { get; }
	}
}