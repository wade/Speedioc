namespace Speedioc.CodeGeneration.TemplateCodeGen
{
	public class GeneratedCodeItem
	{
		public GeneratedCodeItem()
		{
		}

		public GeneratedCodeItem(TemplateRegistrationMetadata metadata, string code)
		{
			Metadata = metadata;
			Code = code;
		}

		public string Code { get; set; }
		public TemplateRegistrationMetadata Metadata { get; set; }
	}
}