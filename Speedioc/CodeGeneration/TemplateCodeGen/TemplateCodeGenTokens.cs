namespace Speedioc.CodeGeneration.TemplateCodeGen
{
	public static class TemplateCodeGenTokens
	{
		public const string ClassNameToken = "$ClassName$";
		public const string ContainerIdToken = "$ContainerId$";
		public const string HandlerMapEntries = "$HandlerMapEntries$";
		public const string MembersToken = "$Members$";
		public const string NamedHandlerMapEntries = "$NamedHandlerMapEntries$";
		public const string NamespaceToken = "$Namespace$";
		public const string PreCreateInstancesToken = "$PreCreateInstances$";
		private const string ResolvedTypeMethodBeginToken = "$ResolvedTypeMethod!";
		private const string ResolvedTypeMethodEndToken = "!ResolvedTypeMethod$";
		public const string ResolvedTypeMethodTokenFormat = ResolvedTypeMethodBeginToken + "{0}" + ResolvedTypeMethodEndToken;
	}
}