using System.Collections.Generic;
using Speedioc.Registration;
using Speedioc.Registration.Core;

namespace Speedioc.Core
{
	public interface IRegistrationAggregator
	{
		IList<IRegistration> AggregateRegistrations(IEnumerable<IRegistry> registries);
	}
}