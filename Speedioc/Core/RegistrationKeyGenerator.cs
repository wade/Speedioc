//using System;
//using System.Text;

//namespace Speedioc.Core
//{
//	public static class RegistrationKeyGenerator
//	{
//		public static string GenerateKey(Operation operation, Type type, string name = null)
//		{
//			//if (null == type)
//			//{
//			//	throw new ArgumentNullException("type");
//			//}

//			//const string delimiter = "|";

//			return type + name;

//			//StringBuilder sb = new StringBuilder(500);

//			//// Operation
//			//sb.Append(GetOperationString(operation));
//			//sb.Append(delimiter);
//			//sb.Append(type);
//			//sb.Append(delimiter);
//			//sb.Append(name);

//			//// Assembly Name
//			//string assemblyName = type.Assembly.GetName().Name;
//			//return sb.ToString();
//		}

//		private static string GetOperationString(Operation operation)
//		{
//			const string getAllInstances = "GetInstance";
//			const string getInstance = "GetInstance";

//			switch (operation)
//			{
//				case Operation.GetAllInstances:
//					return getAllInstances;

//				//case Operation.GetInstance:
//				default:
//					return getInstance;
//			}
//		}
//	}
//}