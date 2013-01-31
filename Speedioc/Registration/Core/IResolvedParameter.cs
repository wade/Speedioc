using Speedioc.Registration.Builders;

namespace Speedioc.Registration.Core
{
	/// <summary>
	/// Implemented by classes that represent a parameter with a value that is a resolved dependency.
	/// </summary>
	/// <seealso cref="IValueParameter"/>
	/// <seealso cref="IParameter"/>
	/// <seealso cref="IConstructor"/>
	/// <seealso cref="IMethod"/>
	/// <seealso cref="IMemberSignatureBuilder"/>
	public interface IResolvedParameter : IResolvedMember, IParameter
	{
	}
}