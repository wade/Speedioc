namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Implemented by classes that represent a property that is set with a specified value.
	/// </summary>
	/// <seealso cref="IResolvedProperty"/>
	/// <seealso cref="IProperty"/>
	/// <seealso cref="IMember"/>
	public interface IValueFactoryProperty : IValueFactoryMember, IProperty
	{
	}
}