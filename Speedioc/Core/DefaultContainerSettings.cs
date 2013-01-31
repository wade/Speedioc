using System;
using System.IO;

namespace Speedioc.Core
{
	/// <summary>
	/// Represents the settings used when generating a container populated with default values.
	/// </summary>
	public class DefaultContainerSettings : ContainerSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultContainerSettings" /> class.
		/// </summary>
		public DefaultContainerSettings()
			: this(GetDefaultContainerId())
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultContainerSettings" /> class with the specified container identifier.
		/// </summary>
		/// <param name="containerId">The container id.</param>
		/// <param name="className">The class name.</param>
		public DefaultContainerSettings(string containerId, string className = null)
		{
			string generatedClassName =
				string.Format("{0}_{1}", className ?? "DefaultGeneratedContainer", containerId);

			GeneratedContainerId = containerId;
			GeneratedContainerClassName = generatedClassName;
			GeneratedContainerNamespace = "Speedioc.GeneratedContainers";
			ForceCompile = false;
			GeneratedContainerAssemblyLocation = Path.GetDirectoryName(GetType().Assembly.Location);
			IncludeDebugInfo = true;
			SaveGeneratedSourceCodeToFile = true;
			ShouldGenerateMethodComments = true;
		}

		private static string GetDefaultContainerId()
		{
			return Guid.NewGuid().ToString("n");
		}
	}
}