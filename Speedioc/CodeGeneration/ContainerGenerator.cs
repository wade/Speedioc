using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Speedioc.Core;
using Speedioc.Registration.Core;

namespace Speedioc.CodeGeneration
{
	public abstract class ContainerGenerator : IContainerGenerator
	{
		public IContainer GenerateContainer(IList<IRegistration> registrations, IContainerSettings settings = null)
		{
			if (null == registrations)
			{
				throw new ArgumentNullException("registrations");
			}
			if (registrations.Count < 1)
			{
				throw new ArgumentException("The registrations argument cannot be empty.", "registrations");
			}

			ReferencedAssemblies = InitializeReferencedAssemblies();
			Registrations = registrations;
			Settings = settings ?? new DefaultContainerSettings("Speedioc");
			ValidateContainerSettings(Settings);  // Validate the settings before attempting to use them.
			AssemblyFilename = Path.Combine(Settings.GeneratedContainerAssemblyLocation, Settings.GeneratedContainerAssemblyName + ".dll");
			FullTypeName = string.Format("{0}.{1}", Settings.GeneratedContainerNamespace, Settings.GeneratedContainerClassName);

			return
				ShouldGenerateContainer()
					? GenerateContainer()
					: LoadContainer();
		}

		protected abstract IContainer GenerateContainer();

		protected string AssemblyFilename { get; set; }

		protected string FullTypeName { get; set; }

		protected IContainerSettings Settings { get; set; }

		protected Dictionary<string, Assembly> ReferencedAssemblies { get; set; }

		protected IList<IRegistration> Registrations { get; set; }

		private IContainer LoadContainer()
		{
			Assembly assembly;
			try
			{
				assembly = Assembly.LoadFrom(AssemblyFilename);
			}
			catch (Exception ex)
			{
				throw new ContainerLoadException(ex, "The generated container assembly '{0}' could not be loaded. See the inner exception for details.", AssemblyFilename);
			}

			if (null == assembly)
			{
				throw new ContainerLoadException("The generated container assembly '{0}' could not be loaded.", AssemblyFilename);
			}

			return
				(IContainer)assembly
					.CreateInstance(
						FullTypeName
					  , false
					  , BindingFlags.Default
					  , null
					  , new object[] { Registrations }
					  , CultureInfo.CurrentCulture
					  , null
						);
		}

		private bool ShouldGenerateContainer()
		{
			return Settings.ForceCompile || false == File.Exists(AssemblyFilename);
		}

		protected void ValidateContainerSettings(IContainerSettings settings)
		{
			if (string.IsNullOrWhiteSpace(Settings.GeneratedContainerNamespace))
			{
				throw new ContainerSettingsValidationException(GetIsNullEmptyOrWhitespaceMessage("GeneratedContainerNamespace"));
			}

			if (string.IsNullOrWhiteSpace(Settings.GeneratedContainerClassName))
			{
				throw new ContainerSettingsValidationException(GetIsNullEmptyOrWhitespaceMessage("GeneratedContainerClassName"));
			}

			if (string.IsNullOrWhiteSpace(Settings.GeneratedContainerAssemblyName))
			{
				throw new ContainerSettingsValidationException(GetIsNullEmptyOrWhitespaceMessage("GeneratedContainerAssemblyName"));
			}

			if (string.IsNullOrWhiteSpace(Settings.GeneratedContainerAssemblyLocation))
			{
				throw new ContainerSettingsValidationException(GetIsNullEmptyOrWhitespaceMessage("GeneratedContainerAssemblyLocation"));
			}

			if (false == Directory.Exists(Settings.GeneratedContainerAssemblyLocation))
			{
				throw new DirectoryNotFoundException(
					"The directory specified by IContainerSettings.GeneratedContainerAssemblyLocation property value was not found."
					);
			}
		}

		private static string GetIsNullEmptyOrWhitespaceMessage(string propertyName)
		{
			return string.Format("The IContainerSettings.{0} property value is null, empty or whitespace.", propertyName);
		}

		private static Dictionary<string, Assembly> InitializeReferencedAssemblies()
		{
			Dictionary<string, Assembly> dictionary = new Dictionary<string, Assembly>();

			// mscorlib.dll
			Assembly assembly = typeof(object).Assembly;	// The System.Object type is used to get a reference to the mscorlib assembly.
			dictionary.Add(assembly.FullName, assembly);

			// System.dll
			assembly = typeof(Uri).Assembly;				// The System.Uri type is used to get a reference to the System assembly.
			dictionary.Add(assembly.FullName, assembly);

			// System.Core.dll
			assembly = typeof(IQueryProvider).Assembly;		// The System.Linq.IQueryProvider type is used to get a reference to the System.Core assembly.
			dictionary.Add(assembly.FullName, assembly);

			// Speedioc.dll
			assembly = Assembly.GetExecutingAssembly();
			dictionary.Add(assembly.FullName, assembly);

			return dictionary;
		}
	}
}