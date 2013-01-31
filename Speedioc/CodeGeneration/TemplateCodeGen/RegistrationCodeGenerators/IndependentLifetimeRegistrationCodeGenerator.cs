using System;
using System.Text;

namespace Speedioc.CodeGeneration.TemplateCodeGen.RegistrationCodeGenerators
{
	public class TransientLifetimeRegistrationCodeGenerator : RegistrationCodeGeneratorBase
	{
		public TransientLifetimeRegistrationCodeGenerator(IContainerSettings settings, TemplateRegistrationMetadata metadata, Type returnType)
			: base(settings, metadata, returnType)
		{
		}

		public override void Generate()
		{
			StringBuilder membersStringBuilder = new StringBuilder(2000);

			AppendCommentBlock(membersStringBuilder, Metadata);

			AppendMethod(
				membersStringBuilder,
				Metadata.OperationMethodName,
				ReturnType,
				GenerateOperationMethodBody);

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
		}

		private void GenerateOperationMethodBody(StringBuilder sb)
		{
			// Invoke constructor
			AppendConstructorInjection(Metadata.Registration, Metadata.Index, sb, true);

			// Invoke members (properties and methods), if any are defined
			AppendMembersInjection(Metadata.Registration, Metadata.Index, ReturnType, sb);

			// Return instance
			AppendReturnInstance(sb, InstanceVariable);
		}
	}
}