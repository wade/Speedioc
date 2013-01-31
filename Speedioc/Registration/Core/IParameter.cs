using Speedioc.Registration.Builders;

namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Marker interface implemented by classes that represent a specific type of parameter.
	/// </summary>
	/// <seealso cref="IResolvedParameter"/>
	/// <seealso cref="IValueFactoryParameter"/>
	/// <seealso cref="IValueParameter"/>
	/// <seealso cref="IConstructor"/>
	/// <seealso cref="IMethod"/>
	/// <seealso cref="IMemberSignatureBuilder"/>
	public interface IParameter : INamedMember
	{
		// NOTE: This is a marker interface and therefore has no members.
	}
}