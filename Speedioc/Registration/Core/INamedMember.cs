namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Implemented by classes that represent a member with a Name property to be 
	/// invoked or set after an instance of the registered type is created.
	/// </summary>
	/// <seealso cref="IMethod"/>
	/// <seealso cref="IParameter"/>
	/// <seealso cref="IProperty"/>
	public interface INamedMember : IMember
	{
		/// <summary>
		/// Gets the name of the member.
		/// </summary>
		/// <value>
		/// The name of the member.
		/// </value>
		string Name { get; }
	}
}