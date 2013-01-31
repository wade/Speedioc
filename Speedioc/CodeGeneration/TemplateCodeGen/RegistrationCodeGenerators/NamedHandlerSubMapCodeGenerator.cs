using System;
using System.Collections.Generic;
using System.Text;
using Speedioc.Core.Utilities;

namespace Speedioc.CodeGeneration.TemplateCodeGen.RegistrationCodeGenerators
{
	public class NamedHandlerSubMapCodeGenerator : RegistrationCodeGeneratorBase
	{
		private readonly Dictionary<Type, List<string>> _namedHandlerSubMapEntries;
		private readonly Dictionary<Type, List<string>> _namedTypedHandlerSubMapEntries;
		private readonly StringBuilder _preCreateInstanceCodeBlockStringBuilder;
		private readonly StringBuilder _codeBlockStringBuilder;

		public NamedHandlerSubMapCodeGenerator(
			Dictionary<Type, List<string>> namedHandlerSubMapEntries, 
			Dictionary<Type, List<string>> namedTypedHandlerSubMapEntries, 
			StringBuilder preCreateInstanceCodeBlockStringBuilder,
			StringBuilder codeBlockStringBuilder)
			: base(null, null, null)
		{
			_namedHandlerSubMapEntries = namedHandlerSubMapEntries;
			_namedTypedHandlerSubMapEntries = namedTypedHandlerSubMapEntries;
			_preCreateInstanceCodeBlockStringBuilder = preCreateInstanceCodeBlockStringBuilder;
			_codeBlockStringBuilder = codeBlockStringBuilder;
		}

		public override void Generate()
		{
			GenerateNamedHandlerSubMapMembers();
			GenerateNamedTypedHandlerSubMapMembers();
		}

		private void GenerateNamedHandlerSubMapMembers()
		{
			const string prefix = "NamedHandlerSubMap";
			GenerateMembers(prefix, _namedHandlerSubMapEntries);
		}

		private void GenerateNamedTypedHandlerSubMapMembers()
		{
			const string prefix = "NamedTypedHandlerSubMap";
			GenerateMembers(prefix, _namedTypedHandlerSubMapEntries);
		}

		private void GenerateMembers(string prefix, Dictionary<Type, List<string>> entries)
		{
			if (null == entries || entries.Count < 1)
			{
				return;
			}
			
			const string declaredType = "Dictionary<string, object>";

			_codeBlockStringBuilder.AppendLine();
			_codeBlockStringBuilder.AppendLine();

			foreach (Type type in entries.Keys)
			{
				List<string> list = entries[type];

				string fieldName = GenerateFieldName(prefix, type);
				string methodName = string.Format("Create{0}", fieldName);

				// Field
				AppendStaticInstanceField(_codeBlockStringBuilder, fieldName, declaredType);

				// Method
				AppendMethod(
					_codeBlockStringBuilder,
					methodName,
					(Type)null,
					sb => GenerateMethodBody(sb, fieldName, declaredType, list));

				// Add a call to the method in PreCreateInstances
				AppendPreCreateInstanceMethodCall(methodName);
			}
		}

		private void AppendPreCreateInstanceMethodCall(string methodName)
		{
			_preCreateInstanceCodeBlockStringBuilder.Append(Indentations.PreCreateInstancesIndent);
			_preCreateInstanceCodeBlockStringBuilder.Append(methodName);
			_preCreateInstanceCodeBlockStringBuilder.Append("();");
			_preCreateInstanceCodeBlockStringBuilder.AppendLine();
		}

		private static string GenerateFieldName(string prefix, Type type)
		{
			string assemblyName = type.Assembly.GetName().Name;
			string fullTypeName = type.FullName;
			return IdentifierHelper.MakeSafeIdentifier(string.Format("{0}__{1}__{2}", prefix, assemblyName, fullTypeName));
		}

		private void GenerateMethodBody(StringBuilder sb, string fieldName, string declaredType, ICollection<string> list)
		{
			const string indent1 = Indentations.MemberBodyIndent;
			const string indent2 = indent1 + Indentations.Indent;
			const string indent3 = indent2 + Indentations.Indent;

			sb.Append(indent1);
			sb.Append(fieldName);
			sb.Append(" = ");
			sb.AppendLine();

			sb.Append(indent2);
			sb.Append("new ");
			sb.Append(declaredType);
			sb.AppendLine();

			sb.Append(indent3);
			sb.Append("{");
			sb.AppendLine();

			StringBuilder sb2 = new StringBuilder(list.Count * 400);
			foreach (string item in list)
			{
				sb2.Append(item);
			}
			sb.Append(sb2.ToString().TrimEnd());

			sb.AppendLine();
			sb.Append(indent3);
			sb.Append("};");
			sb.AppendLine();
		}
	}
}