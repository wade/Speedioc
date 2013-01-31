using System;
using System.Text;
using Speedioc.Core.Utilities;
using Speedioc.Registration.Core;

namespace Speedioc.Core
{
	public class MethodNameGenerator : IMethodNameGenerator
	{
		public MethodNameGenerator(int index, IRegistration registration)
		{
			Index = index;
			Registration = registration;
			CoreMethodName = GenerateCoreMethodName();
		}

		public string CoreMethodName { get; set; }

		private int Index { get; set; }

		private IRegistration Registration { get; set; }

		public string GenerateCreateInstanceMethodName()
		{
			return string.Format("CreateInstance{0}", CoreMethodName);
		}

		public string GenerateCreateThreadLocalInstanceMethodName()
		{
			return string.Format("CreateThreadLocalInstance{0}", CoreMethodName);
		}

		public string GenerateOperationMethodName()
		{
			return string.Format("{0}__{1}", Registration.Operation, CoreMethodName);
		}

		private string GenerateCoreMethodName()
		{
			StringBuilder sb = new StringBuilder(400);

			Type concreteType = Registration.ConcreteType;
			if (null != concreteType)
			{
				sb.Append(IdentifierHelper.MakeSafeIdentifier(concreteType.Assembly.GetName().Name));
				sb.Append("__");
				sb.Append(IdentifierHelper.MakeSafeIdentifier(concreteType.FullName));
			}

			Type mappedType = Registration.MappedType;
			if (null != mappedType)
			{
				if (null != concreteType)
				{
					sb.Append("__As__");
				}
				sb.Append(IdentifierHelper.MakeSafeIdentifier(mappedType.Assembly.GetName().Name));
				sb.Append("__");
				sb.Append(IdentifierHelper.MakeSafeIdentifier(mappedType.FullName));
			}

			string name = Registration.Name;
			if (false == string.IsNullOrWhiteSpace(name))
			{
				sb.Append("__WithName__");
				sb.Append(IdentifierHelper.MakeSafeIdentifier(name));
			}

			sb.Append("__RegistrationIndex__");
			sb.Append(Index);
			return sb.ToString();
		}
	}
}