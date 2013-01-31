namespace Speedioc
{
	/// <summary>
	/// Implemented by classes that provide container settings.
	/// </summary>
	public interface IContainerSettings
	{
		/// <summary>
		/// Gets a value indicating whether the container should be forced to be recompiled.
		/// </summary>
		/// <value>
		///   <c>true</c> if the container should be forced to be recompiled during initialization even if it is already built; otherwise, <c>false</c>.
		/// </value>
		bool ForceCompile { get; }

		/// <summary>
		/// Gets the directory path where the generated container assembly will be located when compiled.
		/// </summary>
		/// <value>
		/// The directory path where the generated container assembly will be located when compiled.s
		/// </value>
		string GeneratedContainerAssemblyLocation { get; }

		/// <summary>
		/// Gets the file name of the generated container assembly.
		/// </summary>
		/// <value>
		/// The file name of the generated container assembly.
		/// </value>
		string GeneratedContainerAssemblyName { get; }

		/// <summary>
		/// Gets the full absolute path and file name of generated container assembly.
		/// </summary>
		/// <value>
		/// The full absolute path and file name of generated container assembly.
		/// </value>
		string GeneratedContainerAssemblyPath { get; }

		/// <summary>
		/// Gets the name of the generated container class.
		/// </summary>
		/// <value>
		/// The name of the generated container class.
		/// </value>
		string GeneratedContainerClassName { get; }

		/// <summary>
		/// Gets the full type name of the generated container class including the namespace and class name in dotted notation.
		/// </summary>
		/// <value>
		/// The full type name of the generated container class including the namespace and class name in dotted notation.
		/// </value>
		string GeneratedContainerFullTypeName { get; }

		/// <summary>
		/// Gets the unique identifier string of the generated container.
		/// </summary>
		/// <value>
		/// The unique identifier string of the generated container.
		/// </value>
		string GeneratedContainerId { get; }

		/// <summary>
		/// Gets the namespace of generated container class.
		/// </summary>
		/// <value>
		/// The namespace of generated container class.
		/// </value>
		string GeneratedContainerNamespace { get; }

		/// <summary>
		/// Gets the full absolute path and file name of generated container C-Sharp source code file.
		/// </summary>
		/// <value>
		/// The full absolute path and file name of generated container C-Sharp source code file.
		/// </value>
		string GeneratedContainerSourceCodeFilename { get; }

		/// <summary>
		/// Gets a value indicating whether pdb file should be generated when the container is compiled.
		/// </summary>
		/// <value>
		///   <c>true</c> if pdb file is generated when the container is compiled; otherwise, <c>false</c>.
		/// </value>
		bool IncludeDebugInfo { get; }

		/// <summary>
		/// Gets a value indicating whether the generated source code is saved to a file.
		/// </summary>
		/// <value>
		/// <c>true</c> if the generated source code is saved to a file; otherwise, <c>false</c>.
		/// </value>
		bool SaveGeneratedSourceCodeToFile { get; }

		/// <summary>
		/// Gets a value indicating whether descriptive method comments are generated in the container source code.
		/// </summary>
		/// <value>
		/// <c>true</c> if descriptive method comments are generated in the container source code; otherwise, <c>false</c>.
		/// </value>
		bool ShouldGenerateMethodComments { get; }
	}
}