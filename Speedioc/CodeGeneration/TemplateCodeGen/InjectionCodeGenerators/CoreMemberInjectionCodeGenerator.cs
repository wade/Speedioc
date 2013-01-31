using System;
using System.Text;

namespace Speedioc.CodeGeneration.TemplateCodeGen.InjectionCodeGenerators
{
	public abstract class CoreMemberInjectionCodeGenerator
	{
		protected string ProcessResolvedItem(
			Type dependencyType,
			string dependencyName,
			int memberIndex, 
			string indent, 
			StringBuilder preMemberCodeBulider, 
			string variableWord)
		{
			// For resolved parameters, insert a token placeholder like this:
			//		$ResolvedTypeMethod![RegistrationKey]!ResolvedTypeMethod$
			//
			// The generated code will have these tokens replaced with direct method names after all registrations have been processed.
			// Just to be clear, the token represents the method name only, not the invocation code of the method.
			// The method it represents has a signature like this: object MyMethod()
			// The code to invoke the method will be generated here, but the method name will be the token.
			// Example of the code generated:
			//		[FullTypeName] myVariable = $ResolvedTypeMethod!GetInstance|Speedioc_Tests|Speedioc_TestDomain_Car!ResolvedTypeMethod$();
			//
			// In the example above, a new variable is declared and is assigned the result of the method invocaation.
			// That line of code will go into the pre-constructor code block.
			// The variable will be passed as the parameter in the constructor.
			//
			string resolvedParameterRegistrationKey = dependencyType + dependencyName;

			string token = string.Format(TemplateCodeGenTokens.ResolvedTypeMethodTokenFormat, resolvedParameterRegistrationKey);

			bool isParameter = (variableWord == "param");

			// Line of code to execute the internal container method to resolve the value for this specific parameter at runtime.
			// Assigns the return value of the method to a variable which is passed to the constructor.
			// The token is used in place of the method name.
			string variable = string.Format("{0}{1}", variableWord, memberIndex);
			
			StringBuilder sb = new StringBuilder(400);

			if (isParameter)
			{
				sb.Append(indent);
				sb.Append(dependencyType.FullName);
				sb.Append(" ");
				sb.Append(variable);
				sb.Append(" = ");
			}
			
			// Add a cast if the type is primitive because the direct method 
			// will have a return type of object due to the requirement to box the primitive 
			// since the delegate uses type object (e.g. Func<object>).
			if (dependencyType.IsPrimitive)
			{
				sb.Append("(");
				sb.Append(dependencyType.FullName);
				sb.Append(")");
			}

			sb.Append(token);

			if (isParameter)
			{
				sb.AppendLine(";");

				// Add the line of code to the pre-member code string builder.
				preMemberCodeBulider.Append(sb);

				// Return just the variable name.
				return variable;
			}

			// Return the inline code.
			return sb.ToString();
		}

		protected string ProcessValueFactoryItem(
			string typeFullName, 
			TemplateRegistrationMetadata metadata, 
			int memberIndex,
			bool isConstructor,
			string indent, 
			StringBuilder preMemberCodeBulider,
			string variableWord,
			Type valueFactoryMemberType)
		{
			// The Pre-constructor code block used here wires up the value factory at runtime.
			// Line of code to get the registration instance.
			// Only generate this line once per constructor invocation.
			if (false == metadata.GeneratedRegistrationWireUpVariable)
			{
				preMemberCodeBulider.Append(indent);
				preMemberCodeBulider.Append("IRegistration registration = Registrations[");
				preMemberCodeBulider.Append(metadata.Index);
				preMemberCodeBulider.Append("];");
				preMemberCodeBulider.AppendLine();
				metadata.GeneratedRegistrationWireUpVariable = true;
			}

			// Line of code to get a reference to the value factory for this specific parameter at runtime.
			preMemberCodeBulider.Append(indent);
			preMemberCodeBulider.Append("var func");
			preMemberCodeBulider.Append(memberIndex);
			preMemberCodeBulider.Append(" = ((");
			preMemberCodeBulider.Append(valueFactoryMemberType.Name);
			preMemberCodeBulider.Append(")registration.");
			preMemberCodeBulider.Append(isConstructor ? "Constructor.Parameters" : "Members");
			preMemberCodeBulider.Append("[");
			preMemberCodeBulider.Append(memberIndex);
			preMemberCodeBulider.Append("]).ValueFactory;");
			preMemberCodeBulider.AppendLine();

			// Line of code that executes the value factory and assigns the result to a variable.
			string variable = string.Format("{0}{1}", variableWord, memberIndex);
			preMemberCodeBulider.Append(indent);
			preMemberCodeBulider.Append("var ");
			preMemberCodeBulider.Append(variable);
			preMemberCodeBulider.Append(" = (");
			preMemberCodeBulider.Append(typeFullName);
			preMemberCodeBulider.Append(")func");
			preMemberCodeBulider.Append(memberIndex);
			preMemberCodeBulider.Append("();");
			preMemberCodeBulider.AppendLine();
			preMemberCodeBulider.AppendLine();

			// Return just the variable name.
			return variable;
		}

		protected string ProcessValueItem(
			object value, 
			int memberIndex, 
			int registrationIndex, 
			StringBuilder preMemberCodeBulider, 
			string itemTypeWord, 
			string itemValueTypeFullName, 
			string itemValueFactoryTypeFullName, 
			string memberTypeWord)
		{
			string code;
			try
			{
				code = LiteralValueCodeGenerator.GenerateCodeForLiteralValue(value);
			}
			catch (InvalidOperationException ex)
			{
				if (false == ex.Message.Contains("unsupported type"))
				{
					throw;
				}

				string formattedMemberTypeWord =
					string.IsNullOrEmpty(memberTypeWord)
						? string.Empty
						: string.Format("{0} ", memberTypeWord);

				throw new InvalidOperationException(
					string.Format(
						"An unsupported type '{0}' was passed as an '{1}' {2} with an index of {3} " +
						"to the constructor of the registration with index {4}. " +
						"Please use the '{5}' {2} type to support the {6}{2}.",
						value.GetType().FullName,
						itemValueTypeFullName,
						itemTypeWord,
						memberIndex,
						registrationIndex,
						itemValueFactoryTypeFullName,
						formattedMemberTypeWord
						),
					ex
					);
			}
			return code;
		}
	}
}