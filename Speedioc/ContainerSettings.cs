using System.IO;

namespace Speedioc
{
	/// <summary>
	/// Represents the settings used when generating a container.
	/// </summary>
	public class ContainerSettings : IContainerSettings
	{
		/// <summary>
		/// Gets a value indicating whether the container should be forced to be recompiled.
		/// </summary>
		/// <value>
		/// <c>true</c> if the container should be forced to be recompiled during initialization even if it is already built; otherwise, <c>false</c>.
		/// </value>
		public bool ForceCompile { get; set; }

		/// <summary>
		/// Gets the directory path where the generated container assembly will be located when compiled.
		/// </summary>
		/// <value>
		/// The directory path where the generated container assembly will be located when compiled.s
		/// </value>
		public string GeneratedContainerAssemblyLocation { get; set; }

		/// <summary>
		/// Gets the file name of the generated container assembly.
		/// </summary>
		/// <value>
		/// The file name of the generated container assembly.
		/// </value>
		/// <remarks>
		/// This property's value is automatically generated unless it is explicitly set.
		/// </remarks>
		public string GeneratedContainerAssemblyName
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_generatedContainerAssemblyName))
				{
					return GeneratedContainerClassName;
				}
				return _generatedContainerAssemblyName;
			}
			set { _generatedContainerAssemblyName = value; }
		}

		private string _generatedContainerAssemblyName;

		/// <summary>
		/// Gets the full absolute path and file name of generated container assembly.
		/// </summary>
		/// <value>
		/// The full absolute path and file name of generated container assembly.
		/// </value>
		/// <remarks>
		/// This property's value is automatically generated unless it is explicitly set.
		/// </remarks>
		public string GeneratedContainerAssemblyPath
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_generatedContainerAssemblyPath))
				{
					return Path.Combine(GeneratedContainerAssemblyLocation ?? string.Empty, GeneratedContainerAssemblyName + ".dll");
				}
				return _generatedContainerAssemblyPath;
			}
			set { _generatedContainerAssemblyPath = value; }
		}

		private string _generatedContainerAssemblyPath;

		/// <summary>
		/// Gets the name of the generated container class.
		/// </summary>
		/// <value>
		/// The name of the generated container class.
		/// </value>
		public string GeneratedContainerClassName { get; set; }

		/// <summary>
		/// Gets the full type name of the generated container class including the namespace and class name in dotted notation.
		/// </summary>
		/// <value>
		/// The full type name of the generated container class including the namespace and class name in dotted notation.
		/// </value>
		/// <remarks>
		/// This property's value is automatically generated unless it is explicitly set.
		/// </remarks>
		public string GeneratedContainerFullTypeName
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_generatedContainerFullTypeName))
				{
					return string.Format("{0}.{1}", GeneratedContainerNamespace, GeneratedContainerClassName);
				}
				return _generatedContainerFullTypeName;
			}
			set { _generatedContainerFullTypeName = value; }
		}

		private string _generatedContainerFullTypeName;

		/// <summary>
		/// Gets the unique identifier string of the generated container.
		/// </summary>
		/// <value>
		/// The unique identifier string of the generated container.
		/// </value>
		public string GeneratedContainerId { get; set; }

		/// <summary>
		/// Gets the namespace of generated container class.
		/// </summary>
		/// <value>
		/// The namespace of generated container class.
		/// </value>
		public string GeneratedContainerNamespace { get; set; }

		/// <summary>
		/// Gets the full absolute path and file name of generated container C-Sharp source code file.
		/// </summary>
		/// <value>
		/// The full absolute path and file name of generated container C-Sharp source code file.
		/// </value>
		/// <remarks>
		/// This property's value is automatically generated unless it is explicitly set.
		/// </remarks>
		public string GeneratedContainerSourceCodeFilename
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_generatedContainerSourceCodeFilename))
				{
					return Path.Combine(GeneratedContainerAssemblyLocation ?? string.Empty, GeneratedContainerAssemblyName + ".cs");
				}
				return _generatedContainerSourceCodeFilename;
			}
			set { _generatedContainerSourceCodeFilename = value; }
		}

		private string _generatedContainerSourceCodeFilename;

		/// <summary>
		/// Gets a value indicating whether pdb file should be generated when the container is compiled.
		/// </summary>
		/// <value>
		/// <c>true</c> if pdb file is generated when the container is compiled; otherwise, <c>false</c>.
		/// </value>
		public bool IncludeDebugInfo { get; set; }

		/// <summary>
		/// Gets a value indicating whether the generated source code is saved to a file.
		/// </summary>
		/// <value>
		/// <c>true</c> if the generated source code is saved to a file; otherwise, <c>false</c>.
		/// </value>
		public bool SaveGeneratedSourceCodeToFile { get; set; }

		/// <summary>
		/// Gets a value indicating whether descriptive method comments are generated in the container source code.
		/// </summary>
		/// <value>
		/// <c>true</c> if descriptive method comments are generated in the container source code; otherwise, <c>false</c>.
		/// </value>
		public bool ShouldGenerateMethodComments { get; set; }
	}
}