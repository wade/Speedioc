using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

			Registrations = registrations;
			Settings = settings ?? new DefaultContainerSettings("Speedioc");
			AssemblyFilename = GetGeneratedAssemblyFilename();
			FullTypeName = GetFullTypeName();

			return
				ShouldGenerateContainer()
					? GenerateContainer()
					: LoadContainer();
		}

		protected abstract IContainer GenerateContainer();

		protected string AssemblyFilename { get; set; }

		protected string FullTypeName { get; set; }

		protected IContainerSettings Settings { get; set; }

		protected IList<IRegistration> Registrations { get; set; }

		private string GetFullTypeName()
		{
			string namespaceName = Settings.GeneratedContainerNamespace;
			if (string.IsNullOrWhiteSpace(namespaceName))
			{
				throw new InvalidOperationException(
					string.Format(
						"The {0}.GeneratedContainerNamespace property value is null, empty or whitespace."
					  , Settings.GetType().Name)
					);
			}

			string className = Settings.GeneratedContainerClassName;
			if (string.IsNullOrWhiteSpace(className))
			{
				throw new InvalidOperationException(
					string.Format(
						"The {0}.GeneratedContainerClassName property value is null, empty or whitespace."
					  , Settings.GetType().Name)
					);
			}

			return string.Format("{0}.{1}", namespaceName, className);
		}

		private string GetGeneratedAssemblyFilename()
		{
			string assemblyName = Settings.GeneratedContainerAssemblyName;
			if (string.IsNullOrWhiteSpace(assemblyName))
			{
				throw new InvalidOperationException(
					string.Format(
						"The {0}.GeneratedContainerAssemblyName property value is null, empty or whitespace."
					  , Settings.GetType().Name)
					);
			}

			string directory = Settings.GeneratedContainerAssemblyLocation;
			if (string.IsNullOrWhiteSpace(directory))
			{
				throw new InvalidOperationException(
					string.Format(
						"The {0}.GeneratedContainerAssemblyLocation property value is null, empty or whitespace."
					  , Settings.GetType().Name)
					);
			}
			if (false == Directory.Exists(directory))
			{
				throw new DirectoryNotFoundException(
					string.Format(
						"The directory specified by the {0}.GeneratedContainerAssemblyLocation property value was not found."
					  , Settings.GetType().Name)
					);
			}

			string assemblyFileName = string.Format("{0}.dll", assemblyName);
			return Path.Combine(directory, assemblyFileName);
		}

		private IContainer LoadContainer()
		{
			Assembly assembly = Assembly.LoadFrom(AssemblyFilename);
			if (null == assembly)
			{
				// TODO: Fix this exception.
				throw new Exception("Could not load the assembly.");
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
	}
}