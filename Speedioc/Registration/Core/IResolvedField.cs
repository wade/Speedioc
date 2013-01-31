namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Implemented by classes that represent a field with a value that is a resolved dependency.
	/// </summary>
	/// <seealso cref="IValueField"/>
	/// <seealso cref="IValueFactoryField"/>
	/// <seealso cref="IField"/>
	/// <seealso cref="IMember"/>
	public interface IResolvedField : IResolvedMember, IField
	{
	}
}