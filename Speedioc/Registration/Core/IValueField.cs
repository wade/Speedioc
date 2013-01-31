namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Implemented by classes that represent a field that is set with a specified value.
	/// </summary>
	/// <seealso cref="IResolvedField"/>
	/// <seealso cref="IValueFactoryField"/>
	/// <seealso cref="IField"/>
	/// <seealso cref="IMember"/>
	public interface IValueField : IValueMember, IField
	{
	}
}