namespace Speedioc.Core
{
	/// <summary>
	/// Marker class for creating augmented dictionary keys based on <see cref="System.Type"/>.
	/// </summary>
	/// <typeparam name="T">The type of the Typed instance.</typeparam>
	public class Typed<T>
	{
		// This class intentionally has no implementation.
		// Its type is used as a wrapper type for certain dictionary keys based on System.Type.
		// Originally, the Tuple<T> class was used but was later replaced by this class as to 
		// reduce ambiguity of the weird usage of Tuple<T> for this purpose and this class 
		// was then created. Its usage appears in the ContainerBase class and in the 
		// generated container class.
	}
}