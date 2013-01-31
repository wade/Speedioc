using System;
using System.Text;
using Speedioc.Registration.Core;

namespace Speedioc.CodeGeneration.TemplateCodeGen.InjectionCodeGenerators
{
	public class MemberInjectionCodeGenerator : IInjectionCodeGenerator
	{
		private readonly string _indent;
		private readonly string _instanceVariable;
		private readonly TemplateRegistrationMetadata _metadata;

		public MemberInjectionCodeGenerator(TemplateRegistrationMetadata metadata, string instanceVariable, string indent)
		{
			_metadata = metadata;
			_instanceVariable = instanceVariable;
			_indent = indent;
		}

		public string GenerateCode()
		{
			if (null == _metadata.Registration.Members || _metadata.Registration.Members.Count < 1)
			{
				return string.Empty;
			}

			int count = _metadata.Registration.Members.Count;
			
			StringBuilder sb = new StringBuilder(count * 2000);

			int index = -1;
			foreach (IMember member in _metadata.Registration.Members)
			{
				if (null == member)
				{
					throw new InvalidOperationException(
						string.Format(
						"The member injection code could not be generated because the registration with key '{0}' has a null member."
					  , _metadata.RegistrationKey)
						);
				}

				index++;

				IField field = member as IField;
				if (null != field)
				{
					GenerateAndAppendFieldInjectionCode(sb, field, index);
					continue;
				}

				IProperty property = member as IProperty;
				if (null != property)
				{
					GenerateAndAppendPropertyInjectionCode(sb, property, index);
					continue;
				}

				IMethod method = member as IMethod;
				if (null != method)
				{
					GenerateAndAppendMethodInjectionCode(sb, method);
					continue;
				}

				throw new InvalidOperationException(
					string.Format(
					"The member injection code could not be generated for the invalid or unsupported member type, {0}, for registration with key {1}."
				  , member.GetType().FullName
				  , _metadata.RegistrationKey
					));
			}

			return sb.ToString();
		}

		private void GenerateAndAppendFieldInjectionCode(StringBuilder sb, IField field, int index)
		{
			var codeGenerator = new FieldInjectionCodeGenerator(field, index, _metadata, _instanceVariable, _indent);
			GenerateAndAppendInjectionCode(sb, codeGenerator);
		}

		private void GenerateAndAppendPropertyInjectionCode(StringBuilder sb, IProperty property, int index)
		{
			var codeGenerator = new PropertyInjectionCodeGenerator(property, index, _metadata, _instanceVariable, _indent);
			GenerateAndAppendInjectionCode(sb, codeGenerator);
		}

		private void GenerateAndAppendMethodInjectionCode(StringBuilder sb, IMethod method)
		{
			var codeGenerator = new MethodInjectionCodeGenerator(method, _instanceVariable, _metadata, _indent);
			GenerateAndAppendInjectionCode(sb, codeGenerator);
		}

		private static void GenerateAndAppendInjectionCode(StringBuilder sb, IInjectionCodeGenerator codeGenerator)
		{
			string generatedCode = codeGenerator.GenerateCode();
			sb.Append(generatedCode);
		}
	}
}