using System;
using System.Collections.Generic;
using Speedioc.Core;

namespace Speedioc.CodeGeneration.TemplateCodeGen
{
	public class TemplateRegistrationMetadata : RegistrationMetadata
	{
		public TemplateRegistrationMetadata(
			IMethodNameGenerator methodNameGenerator,
			Dictionary<Type, string> namedHandlerMapEntries, 
			Dictionary<Type, string> namedTypedHandlerMapEntries,
			Dictionary<Type, List<string>> namedHandlerSubMapEntries, 
			Dictionary<Type, List<string>> namedTypedHandlerSubMapEntries)
			: base(methodNameGenerator)
		{
			NamedHandlerMapEntries = namedHandlerMapEntries;
			NamedTypedHandlerMapEntries = namedTypedHandlerMapEntries;

			NamedHandlerSubMapEntries = namedHandlerSubMapEntries;
			NamedTypedHandlerSubMapEntries = namedTypedHandlerSubMapEntries;
		}

		public string CodeGeneratorMethodKey { get; set; }
		public bool GeneratedRegistrationWireUpVariable { get; set; }
		public string HandlerMapEntriesCodeBlock { get; set; }
		public string HandlerMapTypedEntriesCodeBlock { get; set; }
		public string InstanceFieldName { get; set; }
		public string MembersCodeBlock { get; set; }
		public string PreCreateInstanceCodeBlock { get; set; }
		public Type TypeKey { get; set; }

		public Dictionary<Type, string> NamedHandlerMapEntries { get; set; }
		public Dictionary<Type, string> NamedTypedHandlerMapEntries { get; set; }

		public Dictionary<Type, List<string>> NamedHandlerSubMapEntries { get; set; }
		public Dictionary<Type, List<string>> NamedTypedHandlerSubMapEntries { get; set; }
	}
}