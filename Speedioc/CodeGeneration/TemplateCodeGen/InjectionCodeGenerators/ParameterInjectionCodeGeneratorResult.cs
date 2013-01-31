using System.Collections.Generic;

namespace Speedioc.CodeGeneration.TemplateCodeGen.InjectionCodeGenerators
{
	public class ParameterInjectionCodeGeneratorResult
	{
		public ParameterInjectionCodeGeneratorResult()
		{
			PreMemberCode = null;
			ParameterCodeEntries = new List<string>();
		}

		public ParameterInjectionCodeGeneratorResult(List<string> parameterCodeEntries, string preMemberCode)
		{
			ParameterCodeEntries = parameterCodeEntries;
			PreMemberCode = preMemberCode;
		}

		public string PreMemberCode { get; private set; }
		public List<string> ParameterCodeEntries { get; private set; }
	}
}