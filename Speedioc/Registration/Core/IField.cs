namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Implemented by classes that represent a field to be 
	/// set after an instance of the registered type is created.
	/// </summary>
	/// <seealso cref="IResolvedField"/>
	/// <seealso cref="IValueFactoryField"/>
	/// <seealso cref="IValueField"/>
	/// <seealso cref="INamedMember"/>
	/// <seealso cref="IMember"/>
	public interface IField : INamedMember
	{
	}
}