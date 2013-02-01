using System;
using QuickStartSampleApp.Domain;
using Speedioc;
using Speedioc.Core;
using Speedioc.Registration;

namespace QuickStartSampleApp
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			try
			{
				new Program().Run();
			}
			catch(Exception ex)
			{
				Console.WriteLine("An exception occurred:");
				Console.WriteLine(ex.ToString());
			}
#if DEBUG
			Console.WriteLine("The program has ended. Press any key to quit.");
			Console.ReadKey();
#endif
		}

		private void Run()
		{
			// To use Speedioc, we need to create a container.
			// There are two prerequisites for creating a container:
			// 1. Container settings
			// 2. One or more registries

			// Container Settings
			// Grab a new instance of the DefaultContainerSettings class specifying an identifier for the container.
			// Also, set the ForceCompile option to true. This is optional and it causes the container to be 
			// regenerated and rebuilt each time it is initialized. This may not be good for a production app, but 
			// it just depends on what you intend to accomplish. For now, turn the option on.
			ContainerSettings settings = new DefaultContainerSettings("SpeediocQuickStartSampleApp");
			settings.ForceCompile = true;

			// Registry
			// Please take the time now to look at the DependencyRegistry class in this project 
			// and read through all of the comments (lessons) to learn how the registration API works.
			IRegistry registry = new DependencyRegistry();
			
			// Get a container builder and bulid the container.
			IContainerBuilder containerBuilder = DefaultContainerBuilderFactory.GetInstance(settings, registry);
			IContainer container = containerBuilder.Build();

			// Now that the container has been built, there is no more opportunities to register types.
			// Don't panic. :-)
			// As you read through the code below which resolves instances from the container, 
			// please reference the DependencyRegistry class to observe the relationship between the 
			// registrations in that class and the resolutions below.

			// Resolve an instance of the BoringCar class.
			BoringCar boringCar = container.GetInstance<BoringCar>();
			Console.WriteLine("BoringCar was resolved {0}successfully.", null == boringCar ? "un" : string.Empty);

			// This sample app is incomplete.
			// First, take a look at the DependencyRegistry class.
			Console.WriteLine("This sample app is incomplete.");
			Console.WriteLine("Take a look at the DependencyRegistry class.");
			Console.WriteLine("More to come...\n");
			//... more to come ...//
		}
	}
}
