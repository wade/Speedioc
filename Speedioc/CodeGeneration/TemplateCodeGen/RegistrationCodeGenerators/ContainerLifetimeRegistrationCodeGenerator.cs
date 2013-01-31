using System;
using System.Text;

namespace Speedioc.CodeGeneration.TemplateCodeGen.RegistrationCodeGenerators
{
	public class ContainerLifetimeRegistrationCodeGenerator : RegistrationCodeGeneratorBase
	{
		public ContainerLifetimeRegistrationCodeGenerator(IContainerSettings settings, TemplateRegistrationMetadata metadata, Type returnType)
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
			AppendStaticInstanceField(membersStringBuilder, index, ReturnType);

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
			// Invoke constructor and assign the returned reference to the instance field.
			AppendConstructorInjection(Metadata.Registration, Metadata.Index, sb, false, InstanceField);

			// Invoke members (properties and methods), if any are defined
			AppendMembersInjection(Metadata.Registration, Metadata.Index, ReturnType, sb, InstanceField);
		}

		private void GenerateOperationMethodBody(StringBuilder sb)
		{
			// If the instance is pre-created, simply return the instance field.
			// Otherwise, check the instance field for null and initialize it if null.
			if (false == Metadata.Registration.ShouldPreCreateInstance)
			{
				AppendLazyInstanceFieldCheck(sb, InstanceField, Metadata.CreateInstanceMethodName, Indentations.MemberBodyIndent);
			}

			AppendReturnInstance(sb, InstanceField);
		}
	}
}