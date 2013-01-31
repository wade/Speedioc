using QuickStartSampleApp.Domain;
using Speedioc;
using Speedioc.Registration;

namespace QuickStartSampleApp
{
	/// <summary>
	/// Registers dependencies in the Speedioc container.
	/// </summary>
	/// <remarks>
	/// Your application may have one or more registry classes. 
	/// Each registry class should derive from the Speedioc.Registration.Registry class (easy) 
	/// or implement the Speedioc.Registration.IRegistry interface (advanced).
	/// </remarks>
	public class DependencyRegistry : Registry
	{
		// You must override the RegisterTypes() method of the base Speedioc.Registration.Registry class.
		// This method is the entry-point of the registry implementation that will be called by the 
		// registration framework to create the registrations for the container.
		// You can put your registration code directly in the RegisterTypes() method or your can create 
		// additional methods to organize your registrations which are, in turn, called by the 
		// RegisterTypes() method.
		public override void RegisterTypes()
		{
			// The registry uses a fluent-style API to register types.

			// The most simple case:
			// Register a concrete type with no mappings or injection.
			Register<BoringCar>();

			// The registration above simply registers the BoringCar class with a 
			// transient lifetime (the default).


			// The next simple, but more useful case:
			// Register a concrete type that is mapped to an abstraction.
			Register<Car>().As<IVehicle>();

			// The registration above registers the Car class mapped to the IVehicle interface with a 
			// transient lifetime (the default). If you attempt to resolve IVehicle from the container, 
			// a new instance of Car will be returned.


			// Next, a named registration.
			// Register a concrete type that is mapped to an abstraction that is registered 
			// with a name (or key).
			Register<Car>().As<IVehicle>().WithName("MyCar");

			// The registration above registers the Car class mapped to the IVehicle interface with a 
			// transient lifetime (the default) and with a name. If you attempt to resolve IVehicle from 
			// the container with the name "MyCar", a new instance of Car will be returned. 
			// Not very useful, I know. We'll get there incrementally.


			// Next, specifying a lifetime.
			// Register a concrete type that is mapped to an abstraction that is registered 
			// with a name (or key) and an explicit transient lifetime.
			Register<Car>().As<IVehicle>().WithName("MyNextCar").WithLifetime(Lifetime.Transient);

			// The registration above registers the Car class mapped to the IVehicle interface with a 
			// transient lifetime (the default) and with a name. If you attempt to resolve IVehicle from 
			// the container with the name "MyNextCar", a new instance of Car will be returned. 
			// Not much new here, just showing you how to specify a lifetime.


			// Order of operations.
			// Register a concrete type that is mapped to an abstraction that is registered 
			// with a name (or key) and an explicit transient lifetime.
			Register<Car>().As<IVehicle>().WithLifetime(Lifetime.Transient).WithName("MyNextCar2");

			// The registration above is identical to the previous example with the exception of the name 
			// and the order in which the WithLifetime and WithName methods are chained. The order of 
			// most of the fluent builder methods is not important. When we get into injection, we will 
			// see that within the context of injection parameters that order will become more important.


			// Method chaining and code formatting.
			// Register a concrete type that is mapped to an abstraction that is registered 
			// with a name (or key) and an explicit transient lifetime.
			Register<Car>()
				.As<IVehicle>()
				.WithLifetime(Lifetime.Transient)
				.WithName("MyNextCar3");

			// The registration above is identical to the previous example with the exception of the name 
			// and the order in which the WithLifetime and WithName methods are chained. The formatting  
			// of the chained methods is no longer on a single line and is easier to read. Just so you know...


			//... more to come ...//

		}
	}
}