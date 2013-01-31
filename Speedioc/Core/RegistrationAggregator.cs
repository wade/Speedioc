using System.Collections.Generic;
using Speedioc.Registration;
using Speedioc.Registration.Core;

namespace Speedioc.Core
{
	public class RegistrationAggregator : IRegistrationAggregator
	{
		public IList<IRegistration> AggregateRegistrations(IEnumerable<IRegistry> registries)
		{
			List<IRegistration> list = new List<IRegistration>();
			foreach (IRegistry registry in registries)
			{
				if (null == registry)
				{
					continue;
				}
				registry.RegisterTypes();
				IRegistrationData registrationData = (IRegistrationData)registry;
				list.AddRange(registrationData.Registrations);
			}
			return list;
		}
	}
}