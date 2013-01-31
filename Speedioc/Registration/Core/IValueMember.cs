namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Implemented by classes that represent a member that is set with a specified value.
	/// </summary>
	/// <seealso cref="IResolvedProperty"/>
	/// <seealso cref="IProperty"/>
	/// <seealso cref="IMember"/>
	public interface IValueMember : IMember
	{
		/// <summary>
		/// Gets the value of the member.
		/// </summary>
		/// <value>
		/// The value of the member.
		/// </value>
		object Value { get; }
	}
}