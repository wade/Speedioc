namespace Speedioc.CodeGeneration.TemplateCodeGen
{
	public class Indentations
	{
		public const string Indent = "\t"; // 1 tab
		public const string MemberIndent = Indent + Indent; // 2 tabs
		public const string MemberBodyIndent = MemberIndent + Indent;
		public const string PreCreateInstancesIndent = Indent + Indent + Indent; // 3 tabs
		public const string HandlerMapKeyValuePairsIndent = Indent + Indent + Indent + Indent + Indent + Indent; // 6 tabs 
	}
}