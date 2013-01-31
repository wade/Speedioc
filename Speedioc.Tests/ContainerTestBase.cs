using System.IO;
using System.Threading;
using NUnit.Framework;
using Speedioc.Core;
using Speedioc.Registration;

namespace Speedioc
{
	[TestFixture]
	public abstract partial class ContainerTestBase
	{
		private string _generatedFilesDirectory;

		protected abstract IContainer CreateContainer(IContainerSettings settings, IRegistry registry);
		protected abstract string GeneratedFilesDirectoryName { get; }

		[TestFixtureSetUp]
		public void TestFixtureSetup()
		{
			string dir = Path.GetDirectoryName(new DefaultContainerSettings().GeneratedContainerAssemblyPath);
			if (null == dir) Assert.Fail("Could not get generated container assembly path.");
			_generatedFilesDirectory = Path.Combine(dir, GeneratedFilesDirectoryName);

			if (Directory.Exists(_generatedFilesDirectory))
			{
				Directory.Delete(_generatedFilesDirectory, true);
			}

			// There seems to be a race condition with the delete dir above and the create dir below.
			// This sleep statement seems to alleviate the race condition on a Core i7.
			Thread.Sleep(100);

			Directory.CreateDirectory(_generatedFilesDirectory);
		}

		public ContainerSettings RegisterSettings(object id)
		{
			ContainerSettings settings = new DefaultContainerSettings(id.ToString(), GetType().Name);

			string filename = Path.GetFileName(settings.GeneratedContainerAssemblyPath);
			if (null == filename) Assert.Fail("Could not get generated container assembly path filename.");
			settings.GeneratedContainerAssemblyPath =
				Path.Combine(_generatedFilesDirectory, filename);

			filename = Path.GetFileName(settings.GeneratedContainerSourceCodeFilename);
			if (null == filename) Assert.Fail("Could not get generated container source code filename.");
			settings.GeneratedContainerSourceCodeFilename =
				Path.Combine(_generatedFilesDirectory, filename);

			return settings;
		}
	}
}