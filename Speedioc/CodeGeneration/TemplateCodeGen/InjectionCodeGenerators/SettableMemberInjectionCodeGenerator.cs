using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Speedioc.Registration.Core;

namespace Speedioc.CodeGeneration.TemplateCodeGen.InjectionCodeGenerators
{
	public abstract class SettableMemberInjectionCodeGenerator<TMember, TResolvedMember, TValueFactoryMember, TValueMember>
		: CoreMemberInjectionCodeGenerator, IInjectionCodeGenerator
		where TMember : class, INamedMember
		where TResolvedMember : class, IResolvedMember
		where TValueFactoryMember : class, IValueFactoryMember
		where TValueMember : class, IValueMember
	{
		private readonly int _index;
		private readonly TMember _member;
		private readonly Dictionary<Type, Func<TMember, int, StringBuilder, string>> _codeGeneratorMethodMap;

		protected SettableMemberInjectionCodeGenerator(TMember member, int index, TemplateRegistrationMetadata metadata, string instanceVariable, string indent)
		{
			_member = member;
			_index = index;
			Metadata = metadata;
			InstanceVariable = instanceVariable;
			Indent = indent;
			_codeGeneratorMethodMap = InitializeCodeGeneratorMethodMap();
		}

		protected abstract string MemberTypeWord { get; }

		protected string Indent { get; private set; }
		protected string InstanceVariable { get; private set; }
		protected TemplateRegistrationMetadata Metadata { get; private set; }

		public string GenerateCode()
		{
			Type key = _member.GetType().GetInterfaces().First(t => t.Namespace == typeof(IMember).Namespace && t.Name != string.Format("I{0}", MemberTypeWord)  && t.Name.StartsWith("I") && t.Name.EndsWith(MemberTypeWord));

			StringBuilder preMemberCodeBulider = new StringBuilder(1000);

			Func<TMember, int, StringBuilder, string> func = _codeGeneratorMethodMap[key];

			string valueCode = func(_member, _index, preMemberCodeBulider);

			StringBuilder sb = new StringBuilder(2000);

			sb.Append(preMemberCodeBulider);

			sb.Append(Indent);
			sb.Append(InstanceVariable);
			sb.Append(".");
			sb.Append(_member.Name.Trim());
			sb.Append(" = ");
			sb.Append(valueCode);
			sb.Append(";");
			sb.AppendLine();

			return sb.ToString();
		}

		private Dictionary<Type, Func<TMember, int, StringBuilder, string>> InitializeCodeGeneratorMethodMap()
		{
			return
				new Dictionary<Type, Func<TMember, int, StringBuilder, string>>
					{
						{ typeof(TResolvedMember), ProcessResolvedMember }, 
						{ typeof(TValueFactoryMember), ProcessValueFactoryMember }, 
						{ typeof(TValueMember), ProcessValueMember }
					};
		}

		private string ProcessResolvedMember(TMember member, int index, StringBuilder preMemberCodeBulider)
		{
			TResolvedMember resolvedMember = member as TResolvedMember;

			if (null == resolvedMember)
			{
				// TODO: Fix this exception:
				throw new Exception("Cannot cast member as TResolvedMember.");
			}

			string code =
				ProcessResolvedItem(
					resolvedMember.DependencyType,
					resolvedMember.DependencyName,
					index,
					Indent,
					preMemberCodeBulider,
					MemberTypeWord.ToLowerInvariant()
					);

			return code;
		}

		private string ProcessValueFactoryMember(TMember member, int index, StringBuilder preMemberCodeBulider)
		{
			TValueFactoryMember valueFactoryMember = member as TValueFactoryMember;

			if (null == valueFactoryMember)
			{
				// TODO: Fix this exception:
				throw new Exception("Cannot cast member as TValueFactoryMember.");
			}

			string code =
				ProcessValueFactoryItem(
					valueFactoryMember.Type.FullName,
					Metadata,
					index,
					false,
					Indent,
					preMemberCodeBulider,
					MemberTypeWord.ToLowerInvariant(),
					typeof(TValueFactoryMember)
					);

			return code;
		}

		private string ProcessValueMember(TMember member, int index, StringBuilder preMemberCodeBulider)
		{
			TValueMember valueMember = member as TValueMember;

			if (null == valueMember)
			{
				// TODO: Fix this exception:
				throw new Exception("Cannot cast member as TValueMember.");
			}

			string code =
				ProcessValueItem(
					valueMember.Value, 
					index,
					Metadata.Index,
					preMemberCodeBulider,
					MemberTypeWord.ToLowerInvariant(),
					typeof(IValueParameter).FullName,
					typeof(IValueFactoryParameter).FullName,
					string.Empty
					);

			return code;
		}
	}
}