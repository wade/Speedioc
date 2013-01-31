using System;
using System.CodeDom.Compiler;
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
		private readonly Dictionary<string, TemplateRegistrationMetadata> _metadataMap;
		private readonly List<OverriddenRegistration> _overriddenRegistrations;
		private readonly List<SkippedRegistration> _skippedRegistrations;
		private readonly Dictionary<string, Assembly> _referencedAssemblies;

		private readonly Dictionary<Type, string> _namedHandlerMapEntries;
		private readonly Dictionary<Type, string> _namedTypedHandlerMapEntries;

		private readonly Dictionary<Type, List<string>> _namedHandlerSubMapEntries;
		private readonly Dictionary<Type, List<string>> _namedTypedHandlerSubMapEntries;

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
			_metadataMap = new Dictionary<string, TemplateRegistrationMetadata>();
			_namedHandlerMapEntries = new Dictionary<Type, string>();
			_namedTypedHandlerMapEntries = new Dictionary<Type, string>();
			_namedHandlerSubMapEntries = new Dictionary<Type, List<string>>();
			_namedTypedHandlerSubMapEntries = new Dictionary<Type, List<string>>();
			_overriddenRegistrations = new List<OverriddenRegistration>();
			_skippedRegistrations = new List<SkippedRegistration>();
			_referencedAssemblies = InitializeReferencedAssemblies();
		}

		private Dictionary<string, Func<TemplateRegistrationMetadata, Type, IRegistrationCodeGenerator>> InitializeCodeGeneratorMethodMap()
		{
			return
				new Dictionary<string, Func<TemplateRegistrationMetadata, Type, IRegistrationCodeGenerator>>
					{
						{ "GetInstance|Transient", (m, t) => new TransientLifetimeRegistrationCodeGenerator(Settings, m, t) }, 
						{ "GetInstance|AppDomain", (m, t) => new AppDomainLifetimeRegistrationCodeGenerator(Settings, m, t) }, 
						{ "GetInstance|Container", (m, t) => new ContainerLifetimeRegistrationCodeGenerator(Settings, m, t) }, 
						{ "GetInstance|Thread", (m, t) => new ThreadLifetimeRegistrationCodeGenerator(Settings, m, t) }, 
					};
		}

		private Dictionary<string, Assembly> InitializeReferencedAssemblies()
		{
			Dictionary<string, Assembly> dictionary = new Dictionary<string, Assembly>();

			// mscorlib.dll
			Assembly assembly = typeof(object).Assembly;
			dictionary.Add(assembly.FullName, assembly);

			// System.dll
			assembly = typeof(Uri).Assembly;
			dictionary.Add(assembly.FullName, assembly);

			// System.Core.dll
			assembly = typeof(IQueryProvider).Assembly;
			dictionary.Add(assembly.FullName, assembly);

			// Speedioc.dll
			assembly = GetType().Assembly;
			dictionary.Add(assembly.FullName, assembly);

			return dictionary;
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

				var codeGenerationResult = GenerateGetInstanceCodeForRegistration(metadata);
				if (codeGenerationResult != RegistrationCodeGenerationResult.Successful)
				{
					continue;
				}

				UpdateMetadataMap(registrationKey, metadata);
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

			foreach (TemplateRegistrationMetadata metadata in _metadataMap.Values)
			{
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
			string assemblyFilename = Settings.GeneratedContainerAssemblyPath;
			if (File.Exists(assemblyFilename))
			{
				File.Delete(assemblyFilename);
			}

			CompilerParameters parameters = new CompilerParameters
				{
					CompilerOptions = "/optimize+",
					OutputAssembly = assemblyFilename,
					GenerateExecutable = false,
					IncludeDebugInformation = Settings.IncludeDebugInfo,
					TreatWarningsAsErrors = false
				};

			foreach (Assembly referencedAssembly in _referencedAssemblies.Values)
			{
				parameters.ReferencedAssemblies.Add(referencedAssembly.Location);
			}

			string filename = Settings.GeneratedContainerSourceCodeFilename;

			bool shouldCompileFromFile =
				Settings.SaveGeneratedSourceCodeToFile &&
				false == string.IsNullOrWhiteSpace(filename) &&
				File.Exists(filename);

			CodeDomProvider codeProvider = 
				CodeDomProvider.CreateProvider("CSharp", new Dictionary<string, string> { { "CompilerVersion", "v4.0" } });

			CompilerResults results = 
				shouldCompileFromFile
					? codeProvider.CompileAssemblyFromFile(parameters, filename)
					: codeProvider.CompileAssemblyFromSource(parameters, _template);

			if (results.Errors.HasErrors)
			{
				int count = results.Errors.Count;
				StringBuilder sb = new StringBuilder(1000 * count);

				sb.Append(count);
				sb.Append(" Compiler Error");
				sb.Append(count != 1 ? "s" : string.Empty);
				sb.Append(" Occurred:");
				sb.AppendLine();

				foreach (CompilerError error in results.Errors)
				{
					sb.Append("		");
					sb.AppendLine(error.ToString());
				}

				sb.AppendLine("Container Settings");
				sb.Append("    SaveGeneratedSourceCodeToFile ..........: "); sb.AppendLine(Settings.SaveGeneratedSourceCodeToFile.ToString());
				sb.Append("    GeneratedContainerSourceCodeFilename ...: "); sb.AppendLine(Settings.GeneratedContainerSourceCodeFilename);

				sb.AppendLine("Compiler Parameters");
				sb.Append("    OutputAssembly ............: "); sb.AppendLine(parameters.OutputAssembly);
				sb.Append("    IncludeDebugInformation ...: "); sb.AppendLine(parameters.IncludeDebugInformation.ToString());

				sb.AppendLine("    Referenced Assemblies");
				
				foreach (string item in parameters.ReferencedAssemblies)
				{
					sb.Append("        Referenced Assembly .....: ");
					sb.AppendLine(item);
				}

				// TODO: Fix this exception:
				throw new Exception(sb.ToString());
			}
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
				if (false == _referencedAssemblies.ContainsKey(key))
				{
					_referencedAssemblies.Add(key, assembly);
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
				// Could not find a code generator delegate.
				// Skip this registration.
				_skippedRegistrations.Add(
					new SkippedRegistration(
						SkippedRegistrationReason.NoCodeGenerator,
						string.Format(
							"The code generator delegate was not found for the CodeGeneratorMethodKey '{0}'."
							, metadata.CodeGeneratorMethodKey),
						metadata)
					);
				return RegistrationCodeGenerationResult.Skipped;
			}

			IRegistrationCodeGenerator codeGenerator = func(metadata, metadata.TypeKey);
			codeGenerator.Generate();
			return RegistrationCodeGenerationResult.Successful;
		}

		private void UpdateMetadataMap(string key, TemplateRegistrationMetadata metadata)
		{
			// Track occurrences where the current registration overrides (replaces) an 
			// existing registration. This is used for debugging, logging and informational purposes.
			if (_metadataMap.ContainsKey(key))
			{
				IRegistrationMetadata oldMetadata = _metadataMap[key];

				OverriddenRegistration overriddenRegistration =
					new OverriddenRegistration(oldMetadata, metadata);

				SkippedRegistration skippedRegistration =
					new SkippedRegistration(
						SkippedRegistrationReason.Overridden,
						"The registration was overridden by another registration.",
						oldMetadata,
						overriddenRegistration);

				_overriddenRegistrations.Add(overriddenRegistration);
				_skippedRegistrations.Add(skippedRegistration);
				_metadataMap[key] = metadata;
			}
			else
			{
				_metadataMap.Add(key, metadata);
			}
		}

		#endregion " Methods That Direct Code Generation "

		private void ValidateRegistration(IRegistration registration, int index)
		{
			// ConcreteType cannot be null.
			Type concreteType = registration.ConcreteType;
			if (null == concreteType)
			{
				// TODO: Fix this exception:
				throw new Exception(string.Format("Invalid registration at index {0}: ConcreteType cannot be null.", index));
			}

			// MappedType may be null.
			// MappedType must be a type that is implemented by ConcreteType.
			Type mappedType = registration.MappedType;
			if (null != mappedType)
			{
				if (false == mappedType.IsAssignableFrom(concreteType))
				{
					// TODO: Fix this exception:
					throw new Exception(string.Format("Invalid registration at index {0}: ConcreteType '{1}' is not assignable to MappedType '{2}'. This is not a valid mapping. Please ensure that ConcreteType impelments, inherits from or is otherwise assignable to MappedType.", index, concreteType.FullName, mappedType.FullName));
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
			Dictionary<Type, string>[] maps = new[] {_namedHandlerMapEntries, _namedTypedHandlerMapEntries};
			foreach (var map in maps)
			{
				foreach (Type type in map.Keys)
				{
					string item = map[type];
					_namedHandlerMapEntriesCodeBlockStringBuilder.Append(item);
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
	}
}