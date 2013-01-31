using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Speedioc.CodeGeneration.TemplateCodeGen.InjectionCodeGenerators;
using Speedioc.Core.Utilities;
using Speedioc.Registration.Core;

namespace Speedioc.CodeGeneration.TemplateCodeGen.RegistrationCodeGenerators
{
	public abstract class RegistrationCodeGeneratorBase : IRegistrationCodeGenerator
	{
		protected const string InstanceFieldPrefix = "Instance";
		protected const string InstanceKeyFieldPrefix = "InstanceKey";
		protected const string InstanceVariable = "instance";

		protected RegistrationCodeGeneratorBase(IContainerSettings settings, TemplateRegistrationMetadata metadata, Type returnType)
		{
			Settings = settings;
			Metadata = metadata;
			ReturnType = returnType;
			InstanceField = null != metadata ? InstanceFieldPrefix + Metadata.Index : null;
		}

		public abstract void Generate();

		protected string InstanceField { get; private set; }
		protected TemplateRegistrationMetadata Metadata { get; private set; }
		protected IContainerSettings Settings { get; private set; }
		protected Type ReturnType { get; private set; }

		protected void AppendCommentBlock(StringBuilder sb, TemplateRegistrationMetadata metadata)
		{
			if (false == Settings.ShouldGenerateMethodComments)
			{
				return;
			}

			const string indent = Indentations.MemberIndent;
			const string notSpecified = "<Not_Specified>";

			sb.Append(indent);
			sb.Append("// ");
			sb.Append(metadata.RegistrationType);
			sb.Append(" Registration");
			sb.AppendLine();

			sb.Append(indent);
			sb.Append("// Registration Index ...........: ");
			sb.Append(metadata.Index);
			sb.AppendLine();

			sb.Append(indent);
			sb.Append("// Registration Key .............: ");
			sb.Append("\"");
			sb.Append(metadata.RegistrationKey);
			sb.Append("\"");
			sb.AppendLine();

			IRegistration registration = metadata.Registration;

			sb.Append(indent);
			sb.Append("// Concrete Type ................: ");
			sb.Append(registration.ConcreteType.AssemblyQualifiedName);
			sb.AppendLine();

			string mappedTypeFullName = (null == registration.MappedType) ? notSpecified : registration.MappedType.AssemblyQualifiedName;

			sb.Append(indent);
			sb.Append("// Mapped Type ..................: ");
			sb.Append(mappedTypeFullName);
			sb.AppendLine();

			string registeredName = string.IsNullOrEmpty(registration.Name) ? notSpecified : registration.Name;

			sb.Append(indent);
			sb.Append("// Registered Name ..............: ");
			sb.Append(registeredName);
			sb.AppendLine();

			sb.Append(indent);
			sb.Append("// Lifetime .....................: ");
			sb.Append(registration.Lifetime);
			sb.AppendLine();

			sb.Append(indent);
			sb.Append("// Operation ....................: ");
			sb.Append(registration.Operation);
			sb.AppendLine();

			sb.Append(indent);
			sb.Append("// Should Pre-Create Instance ...: ");
			sb.Append(registration.ShouldPreCreateInstance);
			sb.AppendLine();

			IConstructor constructor = registration.Constructor;
			bool isConstructorRegistered = (null != constructor);

			sb.Append(indent);
			sb.Append("// Is Constructor Registered ....: ");
			sb.Append(isConstructorRegistered);
			sb.AppendLine();

			if (isConstructorRegistered)
			{
				int constructorParameterCount = (null == constructor.Parameters) ? 0 : constructor.Parameters.Count;

				sb.Append(indent);
				sb.Append("// Constructor Parmeter Count ...: ");
				sb.Append(constructorParameterCount);
				sb.AppendLine();

				if (null != constructor.Parameters && constructorParameterCount > 0)
				{
					int index = 0;
					foreach (IParameter parameter in constructor.Parameters)
					{
						string dots = new string('.', 14 - index.ToString(CultureInfo.InvariantCulture).Length);
						string parameterDescription = string.Format("//     Parameter[{0}] {1}: ", index, dots);

						sb.Append(indent);
						sb.Append(parameterDescription);
						sb.Append(parameter);
						sb.AppendLine();
						index++;
					}
				}
			}
		}

		protected void AppendConstructorInjection(IRegistration registration, int index, StringBuilder sb, bool declareVariable, string assignToVariable = InstanceVariable)
		{
			var constructorInjectionCodeGenerator =
				new ConstructorInjectionCodeGenerator(Metadata, assignToVariable, declareVariable, Indentations.MemberBodyIndent);

			string code = constructorInjectionCodeGenerator.GenerateCode();
			sb.Append(code);
		}

		protected void AppendStaticInstanceField(StringBuilder sb, int index, Type type, string indent = Indentations.MemberIndent)
		{
			string fieldName = InstanceFieldPrefix + index;
			AppendStaticInstanceField(sb, fieldName, type, indent);
		}

		protected void AppendStaticInstanceField(StringBuilder sb, string fieldName, Type type, string indent = Indentations.MemberIndent)
		{
			AppendStaticInstanceField(sb, fieldName, type.FullName, indent);
		}

		protected void AppendStaticInstanceField(StringBuilder sb, string fieldName, string declaredType, string indent = Indentations.MemberIndent)
		{
			sb.Append(indent);
			sb.Append("private");
			sb.Append(" ");
			sb.Append("static");
			sb.Append(" ");
			sb.Append(declaredType);
			sb.Append(" ");
			sb.Append(fieldName);
			sb.Append(";");
			sb.AppendLine();
			sb.AppendLine();
		}

		protected void AppendLazyInstanceFieldCheck(StringBuilder sb, string instanceField, string createInstanceMethodName, string indent = Indentations.MemberIndent)
		{
			sb.Append(indent);
			sb.Append("if (null == ");
			sb.Append(instanceField);
			sb.Append(")");
			sb.AppendLine();

			sb.Append(indent);
			sb.Append("{");
			sb.AppendLine();

			sb.Append(indent);
			sb.Append(Indentations.Indent); // Add an extra single-indent
			sb.Append(createInstanceMethodName);
			sb.Append("();");
			sb.AppendLine();

			sb.Append(indent);
			sb.Append("}");
			sb.AppendLine();
		}

		protected void AppendMembersInjection(IRegistration registration, int index, Type type, StringBuilder sb, string assignToVariable = InstanceVariable)
		{
			var memberInjectionCodeGenerator =
				new MemberInjectionCodeGenerator(Metadata, assignToVariable, Indentations.MemberBodyIndent);

			string code = memberInjectionCodeGenerator.GenerateCode();
			sb.Append(code);
		}

		protected void AppendMethod(StringBuilder sb, string methodName, Type returnType, Action<StringBuilder> appendMethodBody, bool isStatic = false)
		{
			string declaredReturnType = (null == returnType) ? "void" : (returnType.IsPrimitive ? "object" : returnType.FullName);
			AppendMethod(sb, methodName, declaredReturnType, appendMethodBody, isStatic);
		}

		protected void AppendMethod(StringBuilder sb, string methodName, string declaredReturnType, Action<StringBuilder> appendMethodBody, bool isStatic = false)
		{
			const string indent = Indentations.MemberIndent;

			// Method declaration line
			sb.Append(indent);
			sb.Append("private ");
			sb.Append(isStatic ? "static " : string.Empty);
			sb.Append(string.IsNullOrWhiteSpace(declaredReturnType) ? "void" : declaredReturnType);
			sb.Append(" ");
			sb.Append(methodName);
			sb.Append("()");
			sb.AppendLine();

			// Method opening brace line
			sb.Append(indent);
			sb.Append("{");
			sb.AppendLine();

			// Method body
			appendMethodBody(sb);

			// Method closing brace line
			sb.Append(indent);
			sb.Append("}");
			sb.AppendLine();
			sb.AppendLine();
		}

		protected void AppendReturnInstance(StringBuilder sb, string referenceToReturn, string indent = Indentations.MemberBodyIndent)
		{
			sb.Append(indent);
			sb.Append("return ");
			sb.Append(referenceToReturn);
			sb.Append(";");
			sb.AppendLine();
		}

		protected string GenerateCodeForHandlerMapEntry(Type dependencyType, string methodName, bool typed = false, string instanceFieldName = null)
		{
			string keyType = typed ? string.Format("Typed<{0}>", dependencyType.FullName) : dependencyType.FullName;
			string funcType = (typed && false == dependencyType.IsPrimitive) ? dependencyType.FullName : "object";

			return string.Format("{0}{{ typeof({1}), new Handler<{2}>({3}, new Func<{2}>({4})) }},{5}",
				Indentations.HandlerMapKeyValuePairsIndent, keyType, funcType, instanceFieldName ?? "null", methodName, Environment.NewLine);
		}

		protected void GenerateCodeForNamedHandlerMapEntry(Type dependencyType, string dependencyName, string methodName, bool typed = false, string instanceFieldName = null)
		{
			Type key = dependencyType;
			Dictionary<Type, string> map = typed ? Metadata.NamedTypedHandlerMapEntries : Metadata.NamedHandlerMapEntries;
			if (false == map.ContainsKey(key))
			{
				string keyType = typed ? string.Format("Typed<{0}>", dependencyType.FullName) : dependencyType.FullName;
				string prefix = string.Format("Named{0}HandlerSubMap", typed ? "Typed" : string.Empty);
				string assemblyName = dependencyType.Assembly.GetName().Name;
				string fullTypeName = dependencyType.FullName;
				string fieldName = IdentifierHelper.MakeSafeIdentifier(string.Format("{0}__{1}__{2}", prefix, assemblyName, fullTypeName));

				string namedHandlerMapEntry = string.Format("{0}map.Add(typeof({1}), {2});{3}",
					Indentations.MemberBodyIndent, keyType, fieldName, Environment.NewLine);

				map.Add(key, namedHandlerMapEntry);
			}

			// Sub map entry
			GenerateCodeForNamedHandlerSubMapEntry(dependencyType, dependencyName, methodName, typed, instanceFieldName);
		}

		private void GenerateCodeForNamedHandlerSubMapEntry(Type dependencyType, string dependencyName, string methodName, bool typed = false, string instanceFieldName = null)
		{
			string funcType = (typed && false == dependencyType.IsPrimitive) ? dependencyType.FullName : "object";

			string namedHandlerSubMapEntry = string.Format("{0}{{ \"{1}\", new Handler<{2}>({3}, new Func<{2}>({4})) }},{5}",
				Indentations.HandlerMapKeyValuePairsIndent, dependencyName, funcType, instanceFieldName ?? "null", methodName, Environment.NewLine);

			Dictionary<Type, List<string>> map = typed ? Metadata.NamedTypedHandlerSubMapEntries : Metadata.NamedHandlerSubMapEntries;
			AddValueToMap(map, dependencyType, namedHandlerSubMapEntry);
		}

		private void AddValueToMap(Dictionary<Type, List<string>> map, Type key, string value)
		{
			List<string> list;
			if (false == map.TryGetValue(key, out list))
			{
				list = new List<string>();
				map.Add(key, list);
			}
			list.Add(value);
		}

		protected string GenerareCodeForPreCreateInstanceCodeBlock(bool shouldPreCreateInstance, string createInstanceMethodName)
		{
			return shouldPreCreateInstance 
				? string.Format("{0}{1}();", Indentations.PreCreateInstancesIndent, createInstanceMethodName) 
				: null;
		}
	}
}