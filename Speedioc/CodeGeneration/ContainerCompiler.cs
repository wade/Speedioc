using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Speedioc.CodeGeneration
{
	public class ContainerCompiler
	{
		public ContainerCompiler(IContainerSettings settings, Dictionary<string, Assembly> referencedAssemblies, string sourceCode)
		{
			if (null == settings)
			{
				throw new ArgumentNullException("settings");
			}

			if (string.IsNullOrWhiteSpace(sourceCode))
			{
				throw new ArgumentNullException("sourceCode");
			}
			
			Settings = settings;
			SourceCode = sourceCode;
			ReferencedAssemblies = referencedAssemblies;
			OutputAssemblyFilename = Settings.GeneratedContainerAssemblyPath;
			SourceCodeFilename = Settings.GeneratedContainerSourceCodeFilename;
			Parameters = InitializeCompilerParameters();
		}

		protected string OutputAssemblyFilename { get; set; }
		protected CompilerParameters Parameters { get; set; }
		protected CompilerResults Results { get; set; }
		protected Dictionary<string, Assembly> ReferencedAssemblies { get; set; }
		protected IContainerSettings Settings { get; set; }
		protected string SourceCode { get; set; }
		protected string SourceCodeFilename { get; set; }

		public void Compile()
		{
			// Clean up the previous output assembly, if necessary.
			if (File.Exists(OutputAssemblyFilename))
			{
				File.Delete(OutputAssemblyFilename);
			}

			bool shouldCompileFromFile =
				Settings.SaveGeneratedSourceCodeToFile &&
				false == string.IsNullOrWhiteSpace(SourceCodeFilename) &&
				File.Exists(SourceCodeFilename);

			CodeDomProvider codeProvider =
				CodeDomProvider.CreateProvider("CSharp", new Dictionary<string, string> { { "CompilerVersion", "v4.0" } });

			Results =
				shouldCompileFromFile
					? codeProvider.CompileAssemblyFromFile(Parameters, SourceCodeFilename)
					: codeProvider.CompileAssemblyFromSource(Parameters, SourceCode);

			if (Results.Errors.HasErrors)
			{
				HandleCompilerErrors();
			}
		}

		private void HandleCompilerErrors()
		{
			int count = Results.Errors.Count;
			StringBuilder sb = new StringBuilder(1000 * count);

			sb.Append(count);
			sb.Append(" Compiler Error");
			sb.Append(count != 1 ? "s" : string.Empty);
			sb.Append(" Occurred:");
			sb.AppendLine();

			foreach (CompilerError error in Results.Errors)
			{
				sb.Append("		");
				sb.AppendLine(error.ToString());
			}

			sb.AppendLine("Container Settings");
			sb.Append("    SaveGeneratedSourceCodeToFile ..........: "); sb.AppendLine(Settings.SaveGeneratedSourceCodeToFile.ToString());
			sb.Append("    GeneratedContainerSourceCodeFilename ...: "); sb.AppendLine(Settings.GeneratedContainerSourceCodeFilename);

			sb.AppendLine("Compiler Parameters");
			sb.Append("    OutputAssembly ............: "); sb.AppendLine(Parameters.OutputAssembly);
			sb.Append("    IncludeDebugInformation ...: "); sb.AppendLine(Parameters.IncludeDebugInformation.ToString());

			sb.AppendLine("    Referenced Assemblies");

			foreach (string item in Parameters.ReferencedAssemblies)
			{
				sb.Append("        Referenced Assembly .....: ");
				sb.AppendLine(item);
			}

			throw new ContainerGenerationException(sb.ToString());
		}

		private CompilerParameters InitializeCompilerParameters()
		{
			CompilerParameters parameters = new CompilerParameters
			{
				CompilerOptions = "/optimize+",
				OutputAssembly = OutputAssemblyFilename,
				GenerateExecutable = false,
				IncludeDebugInformation = Settings.IncludeDebugInfo,
				TreatWarningsAsErrors = false
			};

			foreach (Assembly referencedAssembly in ReferencedAssemblies.Values)
			{
				parameters.ReferencedAssemblies.Add(referencedAssembly.Location);
			}

			return parameters;
		}
	}
}