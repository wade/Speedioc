using System;
using System.Text;

namespace Speedioc.CodeGeneration.TemplateCodeGen.RegistrationCodeGenerators
{
	public class ThreadLifetimeRegistrationCodeGenerator : RegistrationCodeGeneratorBase
	{
		public ThreadLifetimeRegistrationCodeGenerator(IContainerSettings settings, TemplateRegistrationMetadata metadata, Type returnType)
			: base(settings, metadata, returnType)
		{
		}

		public override void Generate()
		{
			StringBuilder commentsStringBuilder = new StringBuilder(1000);
			StringBuilder membersStringBuilder = new StringBuilder(2000);
			int index = Metadata.Index;

			AppendCommentBlock(commentsStringBuilder, Metadata);
			Metadata.MembersCommentBlock = commentsStringBuilder.ToString();

			// Operation Method
			AppendMethod(
				membersStringBuilder,
				Metadata.OperationMethodName,
				ReturnType,
				GenerateOperationMethodBody);

			// Create Thread Local Instance Method
			AppendMethod(
				membersStringBuilder,
				Metadata.CreateThreadLocalInstanceMethodName,
				(Type)null,
				GenerateCreateThreadLocalInstanceMethodBody);

			// Create Instance Method
			AppendMethod(
				membersStringBuilder,
				Metadata.CreateInstanceMethodName,
				ReturnType,
				GenerateCreateInstanceMethodBody);

			// Field
			AppendThreadLocalInstanceField(membersStringBuilder, index, ReturnType);

			// Done with members code block
			Metadata.MembersCodeBlock = membersStringBuilder.ToString();

			// HandlerMap entry code block
			if (string.IsNullOrWhiteSpace(Metadata.Registration.Name))
			{
				Metadata.HandlerMapEntriesCodeBlock =
					GenerateCodeForHandlerMapEntry(Metadata.TypeKey, Metadata.OperationMethodName);

				Metadata.HandlerMapTypedEntriesCodeBlock =
					GenerateCodeForHandlerMapEntry(Metadata.TypeKey, Metadata.OperationMethodName, true);
			}
			else
			{
				GenerateCodeForNamedHandlerMapEntry(Metadata.TypeKey, Metadata.Registration.Name, Metadata.OperationMethodName);
				GenerateCodeForNamedHandlerMapEntry(Metadata.TypeKey, Metadata.Registration.Name, Metadata.OperationMethodName, true);
			}

			// Pre-Created Instance code block
			Metadata.PreCreateInstanceCodeBlock =
				GenerareCodeForPreCreateInstanceCodeBlock(Metadata.Registration.ShouldPreCreateInstance, Metadata.CreateThreadLocalInstanceMethodName);
		}

		private void GenerateCreateInstanceMethodBody(StringBuilder sb)
		{
			// Invoke constructor and assign the returnede reference to the instance field.
			AppendConstructorInjection(Metadata.Registration, Metadata.Index, sb, true);

			// Invoke members (properties and methods), if any are defined
			AppendMembersInjection(Metadata.Registration, Metadata.Index, ReturnType, sb);

			// Return the reference.
			AppendReturnInstance(sb, InstanceVariable);
		}

		private void GenerateCreateThreadLocalInstanceMethodBody(StringBuilder sb)
		{
			// Create a new instance of ThreadLocal<T> passing the create instance method.
			AppendThreadLocalInstanceCreation(sb, InstanceField, ReturnType, Metadata.CreateInstanceMethodName);
		}

		private void GenerateOperationMethodBody(StringBuilder sb)
		{
			// If the instance is pre-created, simply return the instance field.
			// Otherwise, check the instance field for null and initialize it if null.
			if (false == Metadata.Registration.ShouldPreCreateInstance)
			{
				AppendLazyInstanceFieldCheck(sb, InstanceField, Metadata.CreateThreadLocalInstanceMethodName, Indentations.MemberBodyIndent);
			}

			string instanceFieldValue = string.Format("{0}.Value", InstanceField);

			AppendReturnInstance(sb, instanceFieldValue);
		}

		private void AppendThreadLocalInstanceField(StringBuilder sb, int index, Type type, string indent = Indentations.MemberIndent)
		{
			sb.Append(indent);
			sb.Append("private ThreadLocal<");
			sb.Append(type.FullName);
			sb.Append("> ");
			sb.Append(InstanceFieldPrefix);
			sb.Append(index);
			sb.Append(";");
			sb.AppendLine();
			sb.AppendLine();
		}

		private void AppendThreadLocalInstanceCreation(StringBuilder sb, string instanceField, Type type, string createInstanceMethodName, string indent = Indentations.MemberBodyIndent)
		{
			sb.Append(indent);
			sb.Append(instanceField);
			sb.Append(" = new ThreadLocal<");
			sb.Append(type.FullName);
			sb.Append(">(");
			sb.Append(createInstanceMethodName);
			sb.Append(");");
			sb.AppendLine();
		}
	}
}