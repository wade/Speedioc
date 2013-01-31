using System;
using System.Collections.Concurrent;

namespace Speedioc.Core
{
	/// <summary>
	/// Used to anchor App Domain-scoped singleton object instances to the current App Domain independently of the container instance.
	/// </summary>
	public static class AppDomainLifetimeAnchor
	{
		private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, object>> _containerMap;

		static AppDomainLifetimeAnchor()
		{
			_containerMap = new ConcurrentDictionary<string, ConcurrentDictionary<string, object>>();
		}

		public static object GetObject(string containerId, string key)
		{
			if (string.IsNullOrEmpty(containerId))
			{
				throw new ArgumentNullException("containerId");
			}

			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}

			if (_containerMap.ContainsKey(containerId))
			{
				ConcurrentDictionary<string, object> objectMap = _containerMap[containerId];
				object obj;
				if (objectMap.TryGetValue(key, out obj))
				{
					return obj;
				}
			}
			return null;
		}

		public static void SetObject(string containerId, string key, object obj)
		{
			if (string.IsNullOrEmpty(containerId))
			{
				throw new ArgumentNullException("containerId");
			}

			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}

			ConcurrentDictionary<string, object> objectMap =
				_containerMap.GetOrAdd(containerId, k => new ConcurrentDictionary<string, object>());

			objectMap.AddOrUpdate(key, obj, (k, v) => obj);
		}
	}
}