using System.Collections.Generic;
using System.Text;
using Speedioc.Registration.Core;

namespace Speedioc.CodeGeneration.TemplateCodeGen.InjectionCodeGenerators
{
	public class ConstructorInjectionCodeGenerator : IInjectionCodeGenerator
	{
		private const string MemberTypeWord = "constructor";

		private readonly string _assignToVariable;
		private readonly bool _declareVariable;
		private readonly string _indent;
		private readonly IRegistration _registration;
		private readonly StringBuilder _stringBuilder;
		
		private readonly TemplateRegistrationMetadata _metadata;

		public ConstructorInjectionCodeGenerator(TemplateRegistrationMetadata metadata, string assignToVariable, bool declareVariable, string indent)
		{
			_metadata = metadata;
			_registration = metadata.Registration;
			_assignToVariable = assignToVariable;
			_declareVariable = declareVariable;
			_indent = indent;
			_stringBuilder = new StringBuilder(2000);
		}

		public string GenerateCode()
		{
			List<string> parameterCodeEntries;

			IConstructor constructor = _registration.Constructor;
			if (null != constructor && null != constructor.Parameters && constructor.Parameters.Count > 0)
			{
				var parameterInjectionCodeGenerator =
					new ParameterInjectionCodeGenerator(constructor.Parameters, _metadata, _indent, MemberTypeWord, true);

				var parameterCodeResult = parameterInjectionCodeGenerator.GenerateCode();

				if (false == string.IsNullOrWhiteSpace(parameterCodeResult.PreMemberCode))
				{
					_stringBuilder.Append(parameterCodeResult.PreMemberCode);
				}

				parameterCodeEntries = parameterCodeResult.ParameterCodeEntries;
			}
			else
			{
				parameterCodeEntries = new List<string>();
			}

			string concreteTypeFullName = _registration.ConcreteType.FullName;

			_stringBuilder.Append(_indent);

			if (_declareVariable)
			{
				_stringBuilder.Append(concreteTypeFullName);
				_stringBuilder.Append(" ");
			}
			
			_stringBuilder.Append(_assignToVariable);
			_stringBuilder.Append(" = ");

			if (_registration.ConcreteType.IsPrimitive)
			{
				GenerateValueTypeCode(concreteTypeFullName);
			}
			else
			{
				GenerateReferenceOrComplexValueTypeCode(concreteTypeFullName, parameterCodeEntries);
			}
			_stringBuilder.AppendLine(";");
			
			return _stringBuilder.ToString();
		}

		private void GenerateReferenceOrComplexValueTypeCode(string concreteTypeFullName, IEnumerable<string> parameterCodeEntries)
		{
			_stringBuilder.Append("new ");
			_stringBuilder.Append(concreteTypeFullName);
			_stringBuilder.Append("(");

			int index = 0;
			foreach (string item in parameterCodeEntries)
			{
				if (index > 0)
				{
					_stringBuilder.Append(", ");
				}
				_stringBuilder.Append(item);
				index++;
			}

			_stringBuilder.Append(")");
		}

		private void GenerateValueTypeCode(string concreteTypeFullName)
		{
			string literalValue = LiteralValueCodeGenerator.GenerateCodeForLiteralValue(_registration.PrimitiveValue);
			if (string.IsNullOrEmpty(literalValue))
			{
				GenerateReferenceOrComplexValueTypeCode(concreteTypeFullName, new List<string>());
			}
			else
			{
				_stringBuilder.Append(literalValue);
			}
		}
	}
}