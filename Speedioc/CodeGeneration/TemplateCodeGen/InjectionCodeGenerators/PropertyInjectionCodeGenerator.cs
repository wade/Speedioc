using Speedioc.Registration.Core;

namespace Speedioc.CodeGeneration.TemplateCodeGen.InjectionCodeGenerators
{
	public class PropertyInjectionCodeGenerator
		: SettableMemberInjectionCodeGenerator<IProperty, IResolvedProperty, IValueFactoryProperty, IValueProperty>
	{
		public PropertyInjectionCodeGenerator(IProperty property, int index, TemplateRegistrationMetadata metadata, string instanceVariable, string indent)
			: base(property, index, metadata, instanceVariable, indent)
		{
		}

		protected override string MemberTypeWord
		{
			get { return "Property"; }
		}
	}
}