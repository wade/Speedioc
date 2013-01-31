namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Implemented by classes that represent a property to be 
	/// set after an instance of the registered type is created.
	/// </summary>
	/// <seealso cref="IResolvedProperty"/>
	/// <seealso cref="IValueFactoryProperty"/>
	/// <seealso cref="IValueProperty"/>
	/// <seealso cref="INamedMember"/>
	/// <seealso cref="IMember"/>
	public interface IProperty : INamedMember
	{
	}
}