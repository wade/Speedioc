using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Speedioc.Registration.Core;

namespace Speedioc.CodeGeneration.TemplateCodeGen.InjectionCodeGenerators
{
	public class ParameterInjectionCodeGenerator : CoreMemberInjectionCodeGenerator
	{
		private const string VariableWord = "param";

		private readonly string _indent;
		private readonly bool _isConstructor;
		private readonly string _memberTypeWord;
		private readonly TemplateRegistrationMetadata _metadata;
		private readonly List<string> _parameterCode;
		private readonly IList<IParameter> _parameters;
		private readonly Dictionary<Type, Action<IParameter, int, StringBuilder>> _parameterCodeGeneratorMethodMap;

		private string _preMemberCode;

		public ParameterInjectionCodeGenerator(IList<IParameter> parameters, TemplateRegistrationMetadata metadata, string indent, string memberTypeWord, bool isConstructor)
		{
			_parameters = parameters;
			_metadata = metadata;
			_indent = indent;
			_isConstructor = isConstructor;
			_memberTypeWord = memberTypeWord;
			_parameterCodeGeneratorMethodMap = InitializeParameterCodeGeneratorMethodMap();
			_parameterCode = new List<string>();
		}

		public ParameterInjectionCodeGeneratorResult GenerateCode()
		{
			if (null == _parameters || _parameters.Count < 1)
			{
				return new ParameterInjectionCodeGeneratorResult();
			}

			int index = 0;
			StringBuilder preMemberCodeBulider = new StringBuilder(1000);
			_parameters.ToList().ForEach(p => { ProcessParameter(p, index, preMemberCodeBulider); index++; });
			_preMemberCode = preMemberCodeBulider.ToString();

			return new ParameterInjectionCodeGeneratorResult(_parameterCode, _preMemberCode);
		}

		private Dictionary<Type, Action<IParameter, int, StringBuilder>> InitializeParameterCodeGeneratorMethodMap()
		{
			return
				new Dictionary<Type, Action<IParameter, int, StringBuilder>>
					{
						{ typeof(IResolvedParameter), ProcessResolvedParameter }, 
						{ typeof(IValueFactoryParameter), ProcessValueFactoryParameter }, 
						{ typeof(IValueParameter), ProcessValueParameter }
					};
		}

		private void ProcessParameter(IParameter parameter, int index, StringBuilder preMemberCodeBulider)
		{
			// Dispatch to the typed parameter handler method.
			Type key = parameter.GetType().GetInterfaces().First(t => t.Namespace == typeof(IParameter).Namespace && t.Name.StartsWith("I") && t.Name.EndsWith("Parameter"));
			Action<IParameter, int, StringBuilder> method = _parameterCodeGeneratorMethodMap[key];
			method(parameter, index, preMemberCodeBulider);
		}

		private void ProcessResolvedParameter(IParameter parameter, int index, StringBuilder preMemberCodeBulider)
		{
			IResolvedParameter resolvedParameter = (IResolvedParameter)parameter;
			
			string code = 
				ProcessResolvedItem(
					resolvedParameter.DependencyType,
					resolvedParameter.DependencyName,
					index,
					_indent,
					preMemberCodeBulider,
					VariableWord
					);

			_parameterCode.Add(code);
		}

		private void ProcessValueFactoryParameter(IParameter parameter, int index, StringBuilder preMemberCodeBulider)
		{
			IValueFactoryParameter valueFactoryParameter = (IValueFactoryParameter)parameter;

			string code =
				ProcessValueFactoryItem(
					valueFactoryParameter.Type.FullName,
					_metadata,
					index,
					_isConstructor,
					_indent,
					preMemberCodeBulider,
					VariableWord,
					typeof(IValueFactoryParameter)
					);

			_parameterCode.Add(code);
		}

		private void ProcessValueParameter(IParameter parameter, int index, StringBuilder preMemberCodeBulider)
		{
			IValueParameter valueParameter = (IValueParameter)parameter;

			string code =
				ProcessValueItem(
					valueParameter.Value, 
					index,
					_metadata.Index,
					preMemberCodeBulider,
					"parameter",
					typeof(IValueParameter).FullName,
					typeof(IValueFactoryParameter).FullName,
					_memberTypeWord
					);

			_parameterCode.Add(code);
		}
	}
}