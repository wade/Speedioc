using System;
using System.Collections;
using System.Collections.Generic;

namespace Speedioc
{
	/// <summary>
	/// Implemented by classes that provide IoC container fucntionality.
	/// </summary>
	public interface IContainer
	{
		/// <summary>
		/// Gets an instance of the type specified by <paramref name="targetType"/>.
		/// </summary>
		/// <param name="targetType">The type of object to be retrieved.</param>
		/// <returns>An instance of the requested type.</returns>
		object GetInstance(Type targetType);

		/// <summary>
		/// Gets a named instance of the type specified by <paramref name="targetType"/>.
		/// </summary>
		/// <param name="targetType">The type of object to be retrieved.</param>
		/// <param name="name">The name of the specific object instance.</param>
		/// <returns>An instance of the requested named type.</returns>
		object GetInstance(Type targetType, string name);

		/// <summary>
		/// Get all instances of the type specified by <paramref name="targetType"/>.
		/// </summary>
		/// <param name="targetType">The type of object to be retrieved.</param>
		/// <returns>
		/// An <see cref="IEnumerable"/> instance representing all 
		/// available instances of the requested type.
		/// </returns>
		IEnumerable GetAllInstances(Type targetType);

		/// <summary>
		/// Gets an instance of the type specified by <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type of object to be retrieved.</typeparam>
		/// <returns>An instance of the requested type.</returns>
		T GetInstance<T>();

		/// <summary>
		/// Gets a named instance of the type specified by <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type of object to be retrieved.</typeparam>
		/// <param name="name">Name the object was registered with.</param>
		/// <returns>An instance of the requested named type.</returns>
		T GetInstance<T>(string name);

		/// <summary>
		/// Get all instances of the type specified by <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type of object to be retrieved.</typeparam>
		/// <returns>
		/// An <see cref="IEnumerable{TTargetType}"/> instance representing all 
		/// available instances of the requested type.
		/// </returns>
		IEnumerable<T> GetAllInstances<T>(); 
	}
}