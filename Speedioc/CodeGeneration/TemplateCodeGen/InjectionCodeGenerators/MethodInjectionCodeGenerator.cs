using System.Text;
using Speedioc.Registration.Core;

namespace Speedioc.CodeGeneration.TemplateCodeGen.InjectionCodeGenerators
{
	public class MethodInjectionCodeGenerator : IInjectionCodeGenerator
	{
		private const string MemberTypeWord = "method";

		private readonly string _instanceVariable;
		private readonly IMethod _method;
		private readonly string _indent;
		private readonly StringBuilder _stringBuilder;
		
		private readonly TemplateRegistrationMetadata _metadata;

		public MethodInjectionCodeGenerator(IMethod method, string instanceVariable, TemplateRegistrationMetadata metadata, string indent)
		{
			_method = method;
			_instanceVariable = instanceVariable;
			_metadata = metadata;
			_indent = indent;
			_stringBuilder = new StringBuilder(2000);
		}

		public string GenerateCode()
		{
			if (null == _method || null == _method.Parameters || _method.Parameters.Count < 1)
			{
				return string.Empty;
			}

			var parameterInjectionCodeGenerator =
				new ParameterInjectionCodeGenerator(_method.Parameters, _metadata, _indent, MemberTypeWord, false);

			var parameterCodeResult = parameterInjectionCodeGenerator.GenerateCode();

			if (false == string.IsNullOrWhiteSpace(parameterCodeResult.PreMemberCode))
			{
				_stringBuilder.Append(parameterCodeResult.PreMemberCode);
			}

			_stringBuilder.Append(_indent);

			_stringBuilder.Append(_instanceVariable);
			_stringBuilder.Append(".");
			_stringBuilder.Append(_method.Name);
			_stringBuilder.Append("(");

			int index = 0;
			foreach (string item in parameterCodeResult.ParameterCodeEntries)
			{
				if (index > 0)
				{
					_stringBuilder.Append(", ");
				}
				_stringBuilder.Append(item);
				index++;
			}

			_stringBuilder.AppendLine(");");
			return _stringBuilder.ToString();
		}
	}
}