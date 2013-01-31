using Speedioc.Registration.Core;

namespace Speedioc.CodeGeneration.TemplateCodeGen.InjectionCodeGenerators
{
	public class FieldInjectionCodeGenerator
		: SettableMemberInjectionCodeGenerator<IField, IResolvedField, IValueFactoryField, IValueField>
	{
		public FieldInjectionCodeGenerator(IField field, int index, TemplateRegistrationMetadata metadata, string instanceVariable, string indent)
			: base(field, index, metadata, instanceVariable, indent)
		{
		}

		protected override string MemberTypeWord
		{
			get { return "Field"; }
		}
	}
}