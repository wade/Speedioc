using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Speedioc.CodeGeneration.TemplateCodeGen.RegistrationCodeGenerators;
using Speedioc.Core;
using Speedioc.Core.Utilities;
using Speedioc.Registration.Core;

namespace Speedioc.CodeGeneration.TemplateCodeGen
{
	public class TemplateContainerGenerator : ContainerGenerator
	{
		private const string TemplateResourceName = "Speedioc.CodeGeneration.TemplateCodeGen.Resources.GeneratedContainerClassTemplate.txt";

		private readonly Dictionary<string, Func<TemplateRegistrationMetadata, Type, IRegistrationCodeGenerator>> _codeGeneratorMethodMap;

		private readonly Dictionary<Type, GeneratedCodeItem> _namedHandlerMapEntries;
		private readonly Dictionary<Type, GeneratedCodeItem> _namedTypedHandlerMapEntries;

		private readonly Dictionary<Type, List<GeneratedCodeItem>> _namedHandlerSubMapEntries;
		private readonly Dictionary<Type, List<GeneratedCodeItem>> _namedTypedHandlerSubMapEntries;

		private List<TemplateRegistrationMetadata> _metadataList;
		private Dictionary<string, TemplateRegistrationMetadata> _metadataMap;

		private StringBuilder _handlerMapEntriesCodeBlockStringBuilder;
		private StringBuilder _handlerMapTypedEntriesCodeBlockStringBuilder;
		private StringBuilder _memebrsCodeBlockStringBuilder;
		private StringBuilder _namedHandlerMapEntriesCodeBlockStringBuilder;
		private StringBuilder _createNamedHandlerSubMapMembersCodeBlockStringBuilder;
		private StringBuilder _preCreateInstanceCodeBlockStringBuilder;
		private string _template;

		#region " Constructor and Initialization Methods "

		public TemplateContainerGenerator()
		{
			_codeGeneratorMethodMap = InitializeCodeGeneratorMethodMap();
			_namedHandlerMapEntries = new Dictionary<Type, GeneratedCodeItem>();
			_namedTypedHandlerMapEntries = new Dictionary<Type, GeneratedCodeItem>();
			_namedHandlerSubMapEntries = new Dictionary<Type, List<GeneratedCodeItem>>();
			_namedTypedHandlerSubMapEntries = new Dictionary<Type, List<GeneratedCodeItem>>();
		}

		private Dictionary<string, Func<TemplateRegistrationMetadata, Type, IRegistrationCodeGenerator>> InitializeCodeGeneratorMethodMap()
		{
			return
				new Dictionary<string, Func<TemplateRegistrationMetadata, Type, IRegistrationCodeGenerator>>
					{
						{ "GetInstance|Transient", (m, t) => new TransientLifetimeRegistrationCodeGenerator(Settings, m, t) }, 
						{ "GetInstance|AppDomain", (m, t) => new AppDomainLifetimeRegistrationCodeGenerator(Settings, m, t) }, 
						{ "GetInstance|Container", (m, t) => new ContainerLifetimeRegistrationCodeGenerator(Settings, m, t) }, 
						{ "GetInstance|Thread"   , (m, t) => new ThreadLifetimeRegistrationCodeGenerator   (Settings, m, t) }, 
					};
		}

		#endregion " Constructor and Initialization Methods "

		#region " Primary Operations Methods - Higher Level "

		protected override IContainer GenerateContainer()
		{
			ProcessRegistrations();
			ProcessCodeBlocks();
			ProcessTemplate();
			SaveSourceCodeToFile();
			CompileSourceCode();
			return CreateContainer();
		}

		private void ProcessRegistrations()
		{
			int registrationsCount = Registrations.Count;
			_metadataList = new List<TemplateRegistrationMetadata>(registrationsCount);
			_metadataMap = new Dictionary<string, TemplateRegistrationMetadata>(registrationsCount);

			int index = -1;
			foreach (IRegistration registration in Registrations)
			{
				index++;

				ValidateRegistration(registration, index);

				CheckRegistrationForAssembliesToBeReferenced(registration);

				MethodNameGenerator methodNameGenerator = new MethodNameGenerator(index, registration);

				string registrationKey = (registration.MappedType ?? registration.ConcreteType) + registration.Name;

				Type typeKey = registration.MappedType ?? registration.ConcreteType;

				TemplateRegistrationMetadata metadata =
					new TemplateRegistrationMetadata(
						methodNameGenerator, 
						_namedHandlerMapEntries, 
						_namedTypedHandlerMapEntries,
						_namedHandlerSubMapEntries, 
						_namedTypedHandlerSubMapEntries)
						{
							Index = index,
							InstanceFieldName = "Instance" + index,
							RegistrationKey = registrationKey,
							Registration = registration,
							RegistrationType = RegistrationType.Explicit,
							TypeKey = typeKey
						};

				_metadataList.Add(metadata);

				var codeGenerationResult = GenerateGetInstanceCodeForRegistration(metadata);
				if (codeGenerationResult != RegistrationCodeGenerationResult.Successful)
				{
					continue;
				}

				CheckForMetadataOverride(registrationKey, metadata);
				_metadataMap[registrationKey] = metadata;
			}
		}

		private void ProcessCodeBlocks()
		{
			_memebrsCodeBlockStringBuilder = new StringBuilder(1000 + (_metadataMap.Count * 2000));
			_preCreateInstanceCodeBlockStringBuilder = new StringBuilder(100 + (_metadataMap.Count * 200));
			_handlerMapEntriesCodeBlockStringBuilder = new StringBuilder(100 + (_metadataMap.Count * 1000));
			_handlerMapTypedEntriesCodeBlockStringBuilder = new StringBuilder(100 + (_metadataMap.Count * 500));
			_namedHandlerMapEntriesCodeBlockStringBuilder = new StringBuilder(100 + (_metadataMap.Count * 1000));
			_createNamedHandlerSubMapMembersCodeBlockStringBuilder = new StringBuilder(100 + (_metadataMap.Count * 500));

			GenerateAndAppendNamedHandlerMapCode();
			GenerateAndAppendNamedHandlerSubMapCode();

			foreach (TemplateRegistrationMetadata metadata in _metadataList)
			{
				string membersCommentBlock = metadata.MembersCommentBlock;
				if (false == string.IsNullOrWhiteSpace(membersCommentBlock))
				{
					_memebrsCodeBlockStringBuilder.Append(membersCommentBlock);
				}

				if (metadata.ShouldSkip)
				{
					continue;
				}

				string membersCodeBlock = metadata.MembersCodeBlock;
				if (false == string.IsNullOrWhiteSpace(membersCodeBlock))
				{
					_memebrsCodeBlockStringBuilder.Append(membersCodeBlock);
				}

				string preCreateInstanceCodeBlock = metadata.PreCreateInstanceCodeBlock;
				if (false == string.IsNullOrWhiteSpace(preCreateInstanceCodeBlock))
				{
					_preCreateInstanceCodeBlockStringBuilder.AppendLine(preCreateInstanceCodeBlock);
				}

				string handlerMapEntriesCodeBlock = metadata.HandlerMapEntriesCodeBlock;
				if (false == string.IsNullOrWhiteSpace(handlerMapEntriesCodeBlock))
				{
					_handlerMapEntriesCodeBlockStringBuilder.Append(handlerMapEntriesCodeBlock);
				}

				string handlerMapTypedEntriesCodeBlock = metadata.HandlerMapTypedEntriesCodeBlock;
				if (false == string.IsNullOrWhiteSpace(handlerMapTypedEntriesCodeBlock))
				{
					_handlerMapTypedEntriesCodeBlockStringBuilder.Append(handlerMapTypedEntriesCodeBlock);
				}
			}

			_handlerMapEntriesCodeBlockStringBuilder.Append(_handlerMapTypedEntriesCodeBlockStringBuilder);
			_memebrsCodeBlockStringBuilder.Append(_createNamedHandlerSubMapMembersCodeBlockStringBuilder);

			if (_preCreateInstanceCodeBlockStringBuilder.Length < 1)
			{
				_preCreateInstanceCodeBlockStringBuilder.Append(Indentations.MemberBodyIndent);
				_preCreateInstanceCodeBlockStringBuilder.Append("// No instances were configured to be pre-created.");
			}
		}

		private void ProcessTemplate()
		{
			// Load the template from the embedded resource into a string field.
			_template = ResourceUtility.GetManifestResourceAsString(TemplateResourceName);

			// Replace all of the tokens in the template with actual values which may include code blocks.
			_template =
				_template
					.Replace(TemplateCodeGenTokens.ClassNameToken, Settings.GeneratedContainerClassName)
					.Replace(TemplateCodeGenTokens.ContainerIdToken, Settings.GeneratedContainerId)
					.Replace(TemplateCodeGenTokens.HandlerMapEntries, CleanCodeBlock(_handlerMapEntriesCodeBlockStringBuilder.ToString()))
					.Replace(TemplateCodeGenTokens.MembersToken, CleanCodeBlock(_memebrsCodeBlockStringBuilder.ToString()))
					.Replace(TemplateCodeGenTokens.NamedHandlerMapEntries, CleanCodeBlock(_namedHandlerMapEntriesCodeBlockStringBuilder.ToString()))
					.Replace(TemplateCodeGenTokens.NamespaceToken, Settings.GeneratedContainerNamespace)
					.Replace(TemplateCodeGenTokens.PreCreateInstancesToken, CleanCodeBlock(_preCreateInstanceCodeBlockStringBuilder.ToString()));
			
			// Replace resolved type method tokens with the actual method names.
			ProcessResolvedTypeMethodTokens();
		}

		private void ProcessResolvedTypeMethodTokens()
		{
			// Replace each Resolved Type Token with the actual method name.
			// This allows resolved types within the container to make a direct method call 
			// to get the instance of the resolved type. It does not rely on a lookup at runtime.
			foreach (string metadataKey in _metadataMap.Keys)
			{
				TemplateRegistrationMetadata metadata = _metadataMap[metadataKey];
				Lifetime lifetime = metadata.Registration.Lifetime;
				bool useInstanceField = (lifetime == Lifetime.Container || lifetime == Lifetime.AppDomain) && metadata.Registration.ShouldPreCreateInstance;
				string memberName = useInstanceField ? metadata.InstanceFieldName : metadata.OperationMethodName + "()";
				string token = string.Format(TemplateCodeGenTokens.ResolvedTypeMethodTokenFormat, metadataKey);
				_template = _template.Replace(token, memberName);
			}
		}

		private void SaveSourceCodeToFile()
		{
			if (false == Settings.SaveGeneratedSourceCodeToFile)
			{
				return;
			}

			string filename = Settings.GeneratedContainerSourceCodeFilename;
			if (File.Exists(filename))
			{
				File.Delete(filename);
			}
			File.WriteAllText(filename, _template);
		}

		private void CompileSourceCode()
		{
			var compiler = new ContainerCompiler(Settings, ReferencedAssemblies, _template);
			compiler.Compile();
		}

		private IContainer CreateContainer()
		{
			Assembly assembly = Assembly.LoadFrom(Settings.GeneratedContainerAssemblyPath);
			return
				(IContainer)assembly
					.CreateInstance(
						Settings.GeneratedContainerFullTypeName, 
						false, 
						BindingFlags.Default, 
						null,
						new object[] { Registrations },
						CultureInfo.CurrentCulture,
						null
						);
		}

		#endregion " Primary Operations Methods - Higher Level "

		#region " Methods That Direct Code Generation "

		private void CheckRegistrationForAssembliesToBeReferenced(IRegistration registration)
		{
			List<Type> typesToCheck = new List<Type>{ registration.ConcreteType, registration.MappedType };
			foreach (Type type in typesToCheck.Where(t => null != t))
			{
				Assembly assembly = type.Assembly;
				string key = assembly.FullName;
				if (false == ReferencedAssemblies.ContainsKey(key))
				{
					ReferencedAssemblies.Add(key, assembly);
				}
			}
		}

		private string GenerateGetInstanceCodeGeneratorMethodKey(TemplateRegistrationMetadata metadata)
		{
			return string.Format("GetInstance|{0}", metadata.Registration.Lifetime);
		}

		private RegistrationCodeGenerationResult GenerateGetInstanceCodeForRegistration(TemplateRegistrationMetadata metadata)
		{
			metadata.CodeGeneratorMethodKey = GenerateGetInstanceCodeGeneratorMethodKey(metadata);
			Func<TemplateRegistrationMetadata, Type, IRegistrationCodeGenerator> func;

			if (false == _codeGeneratorMethodMap.TryGetValue(metadata.CodeGeneratorMethodKey, out func))
			{
				metadata.ShouldSkip = true;
				metadata.SkippedReason = SkippedRegistrationReason.NoCodeGenerator;
				metadata.SkippedReasonDescription =
					string.Format(
						"The code generator delegate was not found for the CodeGeneratorMethodKey '{0}'."
						, metadata.CodeGeneratorMethodKey);
				UpdateSkippedRegistrationComments(metadata);
				return RegistrationCodeGenerationResult.Skipped;
			}

			IRegistrationCodeGenerator codeGenerator = func(metadata, metadata.TypeKey);
			codeGenerator.Generate();
			return RegistrationCodeGenerationResult.Successful;
		}

		private void CheckForMetadataOverride(string key, TemplateRegistrationMetadata metadata)
		{
			if (false == _metadataMap.ContainsKey(key))
			{
				return;
			}

			TemplateRegistrationMetadata oldMetadata = _metadataMap[key];
			oldMetadata.OverriddenBy = metadata;
			oldMetadata.ShouldSkip = true;
			oldMetadata.SkippedReason = SkippedRegistrationReason.Overridden;
			oldMetadata.SkippedReasonDescription = "The registration was overridden by another registration.";
			UpdateSkippedRegistrationComments(oldMetadata);
		}

		#endregion " Methods That Direct Code Generation "

		private void ValidateRegistration(IRegistration registration, int index)
		{
			// ConcreteType cannot be null.
			Type concreteType = registration.ConcreteType;
			if (null == concreteType)
			{
				throw new RegistrationValidationException("Invalid registration at index {0}: ConcreteType cannot be null.", index);
			}

			// MappedType may be null.
			// MappedType must be a type that is implemented by ConcreteType.
			Type mappedType = registration.MappedType;
			if (null != mappedType)
			{
				if (false == mappedType.IsAssignableFrom(concreteType))
				{
					throw new RegistrationValidationException("Invalid registration at index {0}: ConcreteType '{1}' is not assignable to MappedType '{2}'. This is not a valid mapping. Please ensure that ConcreteType impelments, inherits from or is otherwise assignable to MappedType.", index, concreteType.FullName, mappedType.FullName);
				}
			}
		}

		private static string CleanCodeBlock(string codeBlock)
		{
			if (string.IsNullOrWhiteSpace(codeBlock))
			{
				return string.Empty;
			}

			string cleanCodeBlock = codeBlock.TrimEnd();

			if (string.IsNullOrWhiteSpace(cleanCodeBlock))
			{
				cleanCodeBlock = string.Empty;
			}
			return cleanCodeBlock;
		}

		private void GenerateAndAppendNamedHandlerMapCode()
		{
			Dictionary<Type, GeneratedCodeItem>[] maps = new[] { _namedHandlerMapEntries, _namedTypedHandlerMapEntries };
			foreach (var map in maps)
			{
				foreach (Type type in map.Keys)
				{
					GeneratedCodeItem item = map[type];
					if (false == item.Metadata.ShouldSkip)
					{
						_namedHandlerMapEntriesCodeBlockStringBuilder.Append(item.Code);
					}
				}
			}
		}

		private void GenerateAndAppendNamedHandlerSubMapCode()
		{
			var codeGenerator = 
				new NamedHandlerSubMapCodeGenerator(
					_namedHandlerSubMapEntries, 
					_namedTypedHandlerSubMapEntries,
					_preCreateInstanceCodeBlockStringBuilder,
					_createNamedHandlerSubMapMembersCodeBlockStringBuilder);
			codeGenerator.Generate();
		}

		private void UpdateSkippedRegistrationComments(TemplateRegistrationMetadata metadata)
		{
			const string indent = Indentations.MemberIndent;
			StringBuilder sb = new StringBuilder(metadata.MembersCommentBlock, metadata.MembersCommentBlock.Length + 500);

			sb.Append(indent);
			sb.Append("// Skip This Registration .............: ");
			sb.Append(metadata.ShouldSkip);
			sb.AppendLine();

			sb.Append(indent);
			sb.Append("// Skipped Reason .....................: ");
			sb.Append(metadata.SkippedReason);
			sb.AppendLine();

			sb.Append(indent);
			sb.Append("// Skipped Reason Description .........: ");
			sb.Append(metadata.SkippedReasonDescription);
			sb.AppendLine();

			if (null != metadata.OverriddenBy)
			{
				sb.Append(indent);
				sb.Append("// Overridden By Registration Index ...: ");
				sb.Append(metadata.OverriddenBy.Index);
				sb.AppendLine();
			}

			sb.Append(indent);
			sb.Append("// ");
			sb.Append(new string('-', 80));
			sb.AppendLine();

			sb.Append(indent);
			sb.Append("// NOTE: There is no code generated for this skipped registration.");
			sb.AppendLine();

			sb.Append(indent);
			sb.Append("// ");
			sb.Append(new string('-', 80));
			sb.AppendLine();

			sb.AppendLine(); // Blank line to separate from next member

			metadata.MembersCommentBlock = sb.ToString();
		}
	}
}