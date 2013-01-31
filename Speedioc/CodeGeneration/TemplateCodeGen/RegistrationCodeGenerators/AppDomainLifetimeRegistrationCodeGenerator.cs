using System;
using System.Text;

namespace Speedioc.CodeGeneration.TemplateCodeGen.RegistrationCodeGenerators
{
	public class AppDomainLifetimeRegistrationCodeGenerator : RegistrationCodeGeneratorBase
	{
		public AppDomainLifetimeRegistrationCodeGenerator(IContainerSettings settings, TemplateRegistrationMetadata metadata, Type returnType)
			: base(settings, metadata, returnType)
		{
		}

		public override void Generate()
		{
			StringBuilder membersStringBuilder = new StringBuilder(2000);
			int index = Metadata.Index;

			AppendCommentBlock(membersStringBuilder, Metadata);

			// Operation Method
			AppendMethod(
				membersStringBuilder,
				Metadata.OperationMethodName,
				ReturnType,
				GenerateOperationMethodBody);

			// Create Instance Method
			AppendMethod(
				membersStringBuilder,
				Metadata.CreateInstanceMethodName,
				(Type)null,
				GenerateCreateInstanceMethodBody);

			// Field
			AppendInstanceKeyField(membersStringBuilder, index, Metadata.RegistrationKey);

			// Done with members code block
			Metadata.MembersCodeBlock = membersStringBuilder.ToString();

			// HandlerMap entry code block
			if (string.IsNullOrWhiteSpace(Metadata.Registration.Name))
			{
				Metadata.HandlerMapEntriesCodeBlock =
					GenerateCodeForHandlerMapEntry(Metadata.TypeKey, Metadata.OperationMethodName, false, Metadata.InstanceFieldName);

				Metadata.HandlerMapTypedEntriesCodeBlock =
					GenerateCodeForHandlerMapEntry(Metadata.TypeKey, Metadata.OperationMethodName, true, Metadata.InstanceFieldName);
			}
			else
			{
				GenerateCodeForNamedHandlerMapEntry(Metadata.TypeKey, Metadata.Registration.Name, Metadata.OperationMethodName);
				GenerateCodeForNamedHandlerMapEntry(Metadata.TypeKey, Metadata.Registration.Name, Metadata.OperationMethodName, true);
			}

			// Pre-Created Instance code block
			Metadata.PreCreateInstanceCodeBlock =
				GenerareCodeForPreCreateInstanceCodeBlock(Metadata.Registration.ShouldPreCreateInstance, Metadata.CreateInstanceMethodName);
		}

		private void GenerateCreateInstanceMethodBody(StringBuilder sb)
		{
			// Invoke constructor
			AppendConstructorInjection(Metadata.Registration, Metadata.Index, sb, true);

			// Invoke members (properties and methods), if any are defined
			AppendMembersInjection(Metadata.Registration, Metadata.Index, ReturnType, sb);

			// AppDomainLifetimeAnchor.SetObject
			AppendAppDomainLifetimeContainerSetObject(sb, Metadata.Index);
		}

		private void GenerateOperationMethodBody(StringBuilder sb)
		{
			// Return instance from AppDomainLifetimeAnchor.GetObject
			AppendAppDomainLifetimeContainerGetObject(sb, Metadata.Index, ReturnType);
		}

		private void AppendAppDomainLifetimeContainerGetObject(StringBuilder sb, int index, Type type, bool shouldReturn = true, string indent = Indentations.MemberBodyIndent)
		{
			sb.Append(indent);

			if (shouldReturn)
			{
				sb.Append("return ");
			}
			sb.Append("(");
			sb.Append(type.FullName);
			sb.Append(")AppDomainLifetimeAnchor.GetObject(ContainerId, InstanceKey");
			sb.Append(index);
			sb.Append(");");
			sb.AppendLine();
		}

		private void AppendAppDomainLifetimeContainerSetObject(StringBuilder sb, int index, string indent = Indentations.MemberBodyIndent)
		{
			sb.Append(indent);
			sb.Append("AppDomainLifetimeAnchor.SetObject(ContainerId, InstanceKey");
			sb.Append(index);
			sb.Append(", ");
			sb.Append(InstanceVariable);
			sb.Append(");");
			sb.AppendLine();
		}

		private void AppendInstanceKeyField(StringBuilder sb, int index, string key, string indent = Indentations.MemberIndent)
		{
			sb.Append(indent);
			sb.Append("private const string ");
			sb.Append(InstanceKeyFieldPrefix);
			sb.Append(index);
			sb.Append(" = \"");
			sb.Append(key);
			sb.Append("\"");
			sb.Append(";");
			sb.AppendLine();
			sb.AppendLine();
		}
	}
}