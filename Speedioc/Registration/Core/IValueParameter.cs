using Speedioc.Registration.Builders;

namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Implemented by classes that represent a parameter with a specified value.
	/// </summary>
	/// <seealso cref="IResolvedParameter"/>
	/// <seealso cref="IParameter"/>
	/// <seealso cref="IConstructor"/>
	/// <seealso cref="IMethod"/>
	/// <seealso cref="IMemberSignatureBuilder"/>
	public interface IValueParameter : IValueMember, IParameter
	{
	}
}