using System;
using System.Collections.Generic;
using Speedioc.Core;

namespace Speedioc.CodeGeneration.TemplateCodeGen
{
	public class TemplateRegistrationMetadata : RegistrationMetadata
	{
		public TemplateRegistrationMetadata(
			IMethodNameGenerator methodNameGenerator,
			Dictionary<Type, GeneratedCodeItem> namedHandlerMapEntries,
			Dictionary<Type, GeneratedCodeItem> namedTypedHandlerMapEntries,
			Dictionary<Type, List<GeneratedCodeItem>> namedHandlerSubMapEntries,
			Dictionary<Type, List<GeneratedCodeItem>> namedTypedHandlerSubMapEntries)
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
		public string MembersCommentBlock { get; set; }
		public TemplateRegistrationMetadata OverriddenBy { get; set; }
		public string PreCreateInstanceCodeBlock { get; set; }

		public bool ShouldSkip { get; set; }
		public SkippedRegistrationReason SkippedReason { get; set; }
		public string SkippedReasonDescription { get; set; }

		public Type TypeKey { get; set; }

		public Dictionary<Type, GeneratedCodeItem> NamedHandlerMapEntries { get; set; }
		public Dictionary<Type, GeneratedCodeItem> NamedTypedHandlerMapEntries { get; set; }

		public Dictionary<Type, List<GeneratedCodeItem>> NamedHandlerSubMapEntries { get; set; }
		public Dictionary<Type, List<GeneratedCodeItem>> NamedTypedHandlerSubMapEntries { get; set; }
	}
}