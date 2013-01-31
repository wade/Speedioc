using System;

namespace Speedioc.Core
{
	/// <summary>
	/// Represents a type registered type handler that is added to the handler map in a container.
	/// </summary>
	/// <typeparam name="T">The type of the registered object.</typeparam>
	public class Handler<T>
	{
		/// <summary>
		/// The singleton instance.
		/// </summary>
		public readonly T Instance;

		/// <summary>
		/// The delegate used to get the instance of the type.
		/// </summary>
		public readonly Func<T> Func;

		/// <summary>
		/// Initializes a new instance of the <see cref="Handler{T}" /> class.
		/// </summary>
		/// <param name="instance">The singleton instance or null.</param>
		/// <param name="func">The delegate used to get the instance of the type.</param>
		public Handler(T instance, Func<T> func)
		{
			Instance = instance;
			Func = func;
		}
	}
}