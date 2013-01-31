namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Implemented by classes that represent a property with a value that is a resolved dependency.
	/// </summary>
	/// <seealso cref="IValueProperty"/>
	/// <seealso cref="IProperty"/>
	/// <seealso cref="IMember"/>
	public interface IResolvedProperty : IResolvedMember, IProperty
	{
	}
}