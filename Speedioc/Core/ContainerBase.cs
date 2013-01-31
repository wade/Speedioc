using System;
using System.Collections;
using System.Collections.Generic;
using Speedioc.Registration.Core;

namespace Speedioc.Core
{
	// ********************************************************************************************************
	// **
	// ** This class has been scrutinized very closely and heavily optimized for very fast performance with 
	// ** actual tests, profilers and experimentation of several different techniques for object resoution.
	// ** General principals such as DRY have been intentionally ignored to favor high performance 
	// ** over maintainability which doesn't mean much since this class is fairly simple and straight-forward.
	// ** Method calls and casts have been greatly reduced in order to resolve object instances very quickly 
	// ** with minimal latency and overhead.
	// **
	// ********************************************************************************************************

	/// <summary>
	/// The abstract base class from which generated container instances are derived.
	/// </summary>
	public abstract class ContainerBase : IContainer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerBase" /> class.
		/// </summary>
		/// <param name="registrations">The registrations.</param>
		/// <param name="containerId">The container identifier.</param>
		protected ContainerBase(ICollection<IRegistration> registrations, string containerId)
		{
			// Only allow the static Registrations to be initialized once. 
			if (null != Registrations)
			{
				return;
			}

			Registrations =
				(null == registrations || registrations.Count < 1)
					? new List<IRegistration>()
					: new List<IRegistration>(registrations);

			ContainerId = containerId;
			
			// The virtual method calls below are safe.
			// The derived type is generated and does not run any code in its constructor.
			// ReSharper disable DoNotCallOverridableMethodsInConstructor
			PreCreateInstances();
			HandlerMap = CreateHandlerMap();
			NamedHandlerMap = CreateNamedHandlerMap();
			// ReSharper restore DoNotCallOverridableMethodsInConstructor
		}

		protected readonly string ContainerId;

		protected readonly Dictionary<Type, object> HandlerMap;

		protected readonly Dictionary<Type, Dictionary<string, object>> NamedHandlerMap;

		protected IList<IRegistration> Registrations;

		protected abstract Dictionary<Type, object> CreateHandlerMap();

		protected abstract Dictionary<Type, Dictionary<string, object>> CreateNamedHandlerMap();

		protected abstract void PreCreateInstances();

		#region " IContainer Interface Implementation "

		/// <summary>
		/// Gets an instance of the type specified by <paramref name="targetType"/>.
		/// </summary>
		/// <param name="targetType">The type of object to be retrieved.</param>
		/// <returns>An instance of the requested type.</returns>
		public object GetInstance(Type targetType)
		{
			object obj;
			if (HandlerMap.TryGetValue(targetType, out obj))
			{
				// ReSharper disable PossibleNullReferenceException
				Handler<object> handler = obj as Handler<object>;
				return handler.Instance ?? handler.Func();
				// ReSharper restore PossibleNullReferenceException
			}
			return null;
		}

		/// <summary>
		/// Gets a named instance of the type specified by <paramref name="targetType"/>.
		/// </summary>
		/// <param name="targetType">The type of object to be retrieved.</param>
		/// <param name="name">The name of the specific object instance.</param>
		/// <returns>An instance of the requested named type.</returns>
		public object GetInstance(Type targetType, string name)
		{
			Dictionary<string, object> namedMap;
			if (NamedHandlerMap.TryGetValue(targetType, out namedMap))
			{
				object obj;
				if (namedMap.TryGetValue(name, out obj))
				{
					// ReSharper disable PossibleNullReferenceException
					Handler<object> handler = obj as Handler<object>;
					return handler.Instance ?? handler.Func();
					// ReSharper restore PossibleNullReferenceException
				}
			}
			return null;
		}

		/// <summary>
		/// Get all instances of the type specified by <paramref name="targetType"/>.
		/// </summary>
		/// <param name="targetType">The type of object to be retrieved.</param>
		/// <returns>
		/// An <see cref="IEnumerable"/> instance representing all 
		/// available instances of the requested type.
		/// </returns>
		public IEnumerable GetAllInstances(Type targetType)
		{
			object obj;
			if (HandlerMap.TryGetValue(targetType, out obj))
			{
				// ReSharper disable PossibleNullReferenceException
				Handler<object> handler = obj as Handler<object>;
				return (handler.Instance ?? handler.Func()) as IEnumerable;
				// ReSharper restore PossibleNullReferenceException
			}
			return null;
		}

		/// <summary>
		/// Gets an instance of the type specified by <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type of object to be retrieved.</typeparam>
		/// <returns>An instance of the requested type.</returns>
		public T GetInstance<T>()
		{
			object obj;
			if (HandlerMap.TryGetValue(typeof(Typed<T>), out obj))
			{
				// ReSharper disable PossibleNullReferenceException
				Handler<T> handler = obj as Handler<T>;
				return Equals(handler.Instance, default(T)) ? handler.Func() : handler.Instance;
				// ReSharper restore PossibleNullReferenceException
			}
			return default(T);
		}

		/// <summary>
		/// Gets a named instance of the type specified by <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type of object to be retrieved.</typeparam>
		/// <param name="name">Name the object was registered with.</param>
		/// <returns>An instance of the requested named type.</returns>
		public T GetInstance<T>(string name)
		{
			Dictionary<string, object> namedMap;
			if (NamedHandlerMap.TryGetValue(typeof(Typed<T>), out namedMap))
			{
				object obj;
				if (namedMap.TryGetValue(name, out obj))
				{
					// ReSharper disable PossibleNullReferenceException
					Handler<T> handler = obj as Handler<T>;
					return Equals(handler.Instance, default(T)) ? handler.Func() : handler.Instance;
					// ReSharper restore PossibleNullReferenceException
				}
			}
			return default(T);
		}

		/// <summary>
		/// Get all instances of the type specified by <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type of object to be retrieved.</typeparam>
		/// <returns>
		/// An <see cref="IEnumerable{TTargetType}"/> instance representing all 
		/// available instances of the requested type.
		/// </returns>
		public IEnumerable<T> GetAllInstances<T>()
		{
			return GetInstance<IEnumerable<T>>();
		}

		#endregion " IContainer Interface Implementation "
	}
}