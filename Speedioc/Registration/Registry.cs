namespace Speedioc.Registration
{
	/// <summary>
	/// Default implementatyion of the <see cref="IRegistry"/> interface.
	/// </summary>
	public class Registry : Registrar, IRegistry
	{
		/// <summary>
		/// Registers types using the fluent <see cref="IRegistrar"/> interface.
		/// </summary>
		public virtual void RegisterTypes()
		{
			// Do nothing by default. Allow derived class to override and implement.
		}
	}
}