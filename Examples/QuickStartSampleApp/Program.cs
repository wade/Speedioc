using System;

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
			// This sample app is incomplete.
			// First, take a look at the DependencyRegistry class.
			Console.WriteLine("This sample app is incomplete.");
			Console.WriteLine("Take a look at the DependencyRegistry class.");
			Console.WriteLine("More to come...\n");
			//... more to come ...//
		}
	}
}
