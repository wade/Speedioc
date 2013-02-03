using Speedioc.Registration;
using Speedioc.TestDomain;

namespace Speedioc.QuickStartLessons
{
	/// <summary>
	/// Registers dependencies in the Speedioc container.
	/// </summary>
	/// <remarks>
	/// Your application may have one or more registry classes. 
	/// Each registry class should derive from the Speedioc.Registration.Registry class (easy) 
	/// or implement the Speedioc.Registration.IRegistry interface (advanced).
	/// </remarks>
	public class QuickStartRegistry : Registry
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

			// See Lessons 01 and 02.
			// The most simple case:
			// Register a concrete type with no mappings or injection.
			Register<BoringCar>();

			// The registration above simply registers the BoringCar class with a 
			// transient lifetime (the default).
			//
			//---------------------------------------------------------------------------------------------------


			// See Lessons 03 and 04.
			// The next simple, but more useful case:
			// Register a concrete type that is mapped to an abstraction.
			Register<Car>().As<IVehicle>();

			// The registration above registers the Car class mapped to the IVehicle interface with a 
			// transient lifetime (the default). If you attempt to resolve IVehicle from the container, 
			// a new instance of Car will be returned.
			//
			//---------------------------------------------------------------------------------------------------


			// See Lessons 05 and 06.
			// Next, a named registration.
			// Register a concrete type that is mapped to an abstraction that is registered 
			// with a name (or key).
			Register<Car>().As<IVehicle>().WithName("MyCar");

			// The registration above registers the Car class mapped to the IVehicle interface with a 
			// transient lifetime (the default) and with a name. If you attempt to resolve IVehicle from 
			// the container with the name "MyCar", a new instance of Car will be returned. 
			// Not very useful, I know. We'll get there incrementally.
			//
			//---------------------------------------------------------------------------------------------------


			// See Lessons 07 and 08.
			// Next, specifying a lifetime.
			// Register a concrete type that is mapped to an abstraction that is registered 
			// with a name (or key) and an explicit transient lifetime.
			Register<Car>().As<IVehicle>().WithName("MyNextCar").WithLifetime(Lifetime.Transient);

			// The registration above registers the Car class mapped to the IVehicle interface with a 
			// transient lifetime (the default) and with a name. If you attempt to resolve IVehicle from 
			// the container with the name "MyNextCar", a new instance of Car will be returned. 
			// Not much new here, just showing you how to specify a lifetime.
			//
			//---------------------------------------------------------------------------------------------------


			// See Lessons 09 and 10.
			// Order of operations.
			// Register a concrete type that is mapped to an abstraction that is registered 
			// with a name (or key) and an explicit transient lifetime.
			Register<Car>().As<IVehicle>().WithLifetime(Lifetime.Transient).WithName("MyNextCar2");

			// The registration above is identical to the previous example with the exception of the name 
			// and the order in which the WithLifetime and WithName methods are chained. The order of 
			// most of the fluent builder methods is not important. When we get into injection, we will 
			// see that within the context of injection parameters that order will become more important.
			//
			//---------------------------------------------------------------------------------------------------


			// See Lessons 11 and 12.
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
			//
			//---------------------------------------------------------------------------------------------------


			// See Lessons 13 and 14.
			// Container lifetime
			// Register a concrete type that is mapped to an abstraction that is explicitly 
			// set with a container lifetime, which is the main type of singleton, one 
			// single instance per container instance.
			Register<RedColor>().As<IColor>().WithLifetime(Lifetime.Container);

			// The registration above registers the RedColor class mapped to the IColor interface 
			// with a container lifetime (singleton). The first time that an instance of IColor is 
			// resolved, a new instance of the RedColor class is created and returned. All further 
			// resolution requests for IColor return the same instance of RedColor.
			//
			//---------------------------------------------------------------------------------------------------


			// See Lessons 13 and 14. (Yes, it's true: lessons 13 and 14 again)
			// Container lifetime with a precreated instance
			// Register a concrete type that is mapped to an abstraction that is explicitly 
			// set with a pre-created container lifetime, which is the main type of singleton, one 
			// single instance per container instance.
			Register<RedColor>().As<IColor>().WithLifetime(Lifetime.Container).PreCreateInstance();

			// The registration above is identical to the previous example except that the instance 
			// of the RedColor class is created when the container is initialized and that single 
			// instance will be returned on every resolution request, including the first, without 
			// incurring the overhead of object creation on the first request. Precreated instances 
			// only apply to singleton lifetimes (e.g. Container, AppDomain) and if specified for 
			// other lifetimes, it is silently ignored (no error will occur) and will have no effect.
			// Percreated instances incur the performance hit for instance creation during 
			// container initialization which has the side-effect of causing memory usage earlier in 
			// the lifecycle of the application. There is risk of memory waste if the instance is never 
			// used by the application versus using the default lazy creation. If you know that your 
			// application will be using the instance, it may be worth it to use a precreated instance.
			//
			// BTW, notice that this registration collides with the previous registration. When this 
			// type of collision occurs, the last registration takes precedence and overrides all 
			// previous colliding registrations.
			//
			//---------------------------------------------------------------------------------------------------


			// See Lessons 15 and 16.
			// Introduction to constructor Injection
			Register<Color>()
				.As<IColor>()
				.WithName("LightBlue")
				.UsingConstructor()
					.WithValueParameter("LightBlue")
					.AsLastParameter();

			// Let's see what's going on with the registration above.
			// The registration above registers the Color class mapped to the IColor interface as a 
			// transient instance with a name (or key) of "LightBlue". You've seen all of that before (above). 
			// The new piece is the UsingConstructor() clause. Calling UsingConstructor() starts the 
			// definitiion of a constructor of the concrete type that should be used to create the 
			// instance. Once UsingConstructor is called, the context of subsequent members changes 
			// to allow additional details of the constructor to be specified. The additional details 
			// of a constructor are the constructor's parameters, which must be specified in order.
			// To complete the definition of the constructor, one of two methods must be called.
			// If the constructor has no parameters, calling the WithNoParameters() method completes 
			// the constructor definition (and in this case is the same as if the constructor definition 
			// had been omitted - kind of useless included for completeness). 
			// Once all of the constructors parameters have been defined, the AsLastParameter() method is 
			// called which closes the constructor definition and returns the context back to the 
			// original registration context so that additional specification may be defined.
			// In this example, the Color class' constructor that accepts a single string parameter is 
			// specified with a value of "LightBlue" (the same as the registration's name but it has no 
			// relation to the name and may be different). That constructor's signature is: Color(string name).
			// The parameter is specified using the WithValueParameter() method which is used to specify 
			// a value, usually a literal value. The WithValueParameter() method simply accepts a single 
			// argument of type object and the value passed must match the expected type of the matching 
			// ordinal constructor parameter. Therefore, the order and type of constructor parameters that 
			// are specified must match the same order and type of the constructor's signature.
			// The WithValueParameter() method is just one of three different methods that may be used to 
			// specify a constructor's parameter as we will soon see.
			//
			// Wow, that was a lot at once. Sorry about that.
			//
			//---------------------------------------------------------------------------------------------------


			// See Lessons 17 and 18.
			// Constructor Injection: Simple Resolved Parameter
			Register<Color>()
				.As<IColor>()
				.WithName("Red")
				.UsingConstructor()
					.WithResolvedParameter<IColor>()
					.AsLastParameter();

			// The registration above registers the Color class mapped to the IColor interface as a 
			// transient instance with a name (or key) of "Red" using the constructor of the Color class 
			// that accepts a single IColor instance passing in the resolved instance of IColor, 
			// resolved from the same container. The constructor signature specified is: Color(IColor color).
			// This example uses the generic WithResolvedParameter<T>() method to specify a parameter that is 
			// obtained by resolving the specified type from the same container. In this case, the resolved instance 
			// is obtained from the default resolution of the IColor interface which matches the registration 
			// from two examples ago (see the comment, Container lifetime with a precreated instance, above) 
			// which is a mapping of IColor to the concrete, pre-created singleton instance of the RedColor class.
			//
			// Please feel free to explore the overloads of the WithResolvedParameter method.
			//
			//---------------------------------------------------------------------------------------------------


			// See Lessons 19 and 20.
			// Constructor Injection: Named Resolved Parameter
			Register<Color>()
				.As<IColor>()
				.WithName("Light-Blue-2")
				.UsingConstructor()
					.WithResolvedParameter<IColor>("LightBlue")
					.AsLastParameter();

			// The registration above registers the Color class mapped to the IColor interface as a 
			// transient instance with a name (or key) of "Light-Blue-2" using the constructor of the Color class 
			// that accepts a single IColor instance passing in the resolved, named instance of IColor, 
			// resolved from the same container. The constructor signature specified is: Color(IColor color).
			// This example is the same as the previous except that the resolved parameter specifies a named instance 
			// of IColor, LightBlue.
			//
			// Please feel free to explore the overloads of the WithResolvedParameter method.
			//
			//---------------------------------------------------------------------------------------------------


			// See Lessons 21 and 22.
			// Constructor Injection: Value Factory Parameter
			Register<Color>()
				.As<IColor>()
				.WithName("Red-2")
				.UsingConstructor()
					.WithValueFactoryParameter<IColor>(() => new RedColor())
					.AsLastParameter();

			// The registration above registers the Color class mapped to the IColor interface as a 
			// transient instance with a name (or key) of "Red-2" using the constructor of the Color class 
			// that accepts a single IColor instance passing in the resolved, named instance of IColor, 
			// resolved from the same container. The constructor signature specified is: Color(IColor color).
			// This example is the same as the previous except that the constructor parameter is specified using 
			// the ValueFactoryParameter<T>() method which uses a delegate to specify the parameter's value. 
			// The delegate is invoked when ever a new instance of the type is created. In the example above, 
			// the delegate is specified using a simple lambda expression. Any valid method of specifying a 
			// delegate that matches the expected delegate type, Func<object>, may be used. The return value of 
			// the delegate must return a valid value of the expected constructor parameter type.
			//
			// Please feel free to explore the overloads of the WithValueFactoryParameter method.
			//
			//---------------------------------------------------------------------------------------------------


			// See Lessons 21 and 22. (Yep, lessons 21 and 22 again)
			// Order of operations revisited.
			Register<Color>()
				.As<IColor>()
				.UsingConstructor()
					.WithValueFactoryParameter<IColor>(() => new RedColor())
					.AsLastParameter()
				.WithName("Red-2");

			// The example above produces the exact same registration as the previous example.
			// You'll notice the the order of the method chaining is different. The purpose of this example 
			// is to illustrate the many of the clauses (methods) may be specified in any order an to also 
			// show how the context switch to that of the constructor and then back to the original registration 
			// context when the constructor specification is completed (by the call to the AsLastParameter() method).
			//
			// The place where the order of operations is important is when specifying parameters of constructors 
			// or methods when performing constructor or method injection.
			//
			//---------------------------------------------------------------------------------------------------


			// See Lessons 23 and 24.
			// Constructor Injection: Multiple Parameters
			Register<ColorScheme>()
				.As<IColorScheme>()
				.WithName("RedAndBlack")
				.UsingConstructor()
					.WithValueParameter("Red-Black")
					.WithResolvedParameter<IColor>("Red-2")
					.WithValueFactoryParameter<IColor>(() => new BlackColor())
					.AsLastParameter();

			// The registration above registers the ColorScheme class mapped to the IColorScheme interface as a 
			// transient instance with a name (or key) of "RedAndBlack" using the constructor of the ColorScheme class 
			// which expects three parameters with the following signature: 
			//    ColorScheme(string name, IColor primaryColor, IColor secondaryColor)
			// The first parameter is the name of the color scheme and the registration passes the lietral value, 
			// "Red-Black" as the value. The second and third parameters are instances of IColor, the primary and 
			// secondary colors respectively. The primary color is specified using a resolved parameter which will 
			// have the container resolve an instance of IColor with a named of "Red-2" (which we registered in a 
			// previous example above). The secondary color is specified using a value factory parameter which uses 
			// a lambda expression to create an anonymous method which creates a new instance of the BlackColor class 
			// returning the reference to that anonymous method as a Func<object> delegate which will be invoked each 
			// time that an instance of IColorScheme with a name of "RedAndBlack" is requested from the container.
			//
			// This example shows that the order and type of the constructor parameters matches the constructor 
			// signature which is important. It also shows that each parameter may use a different method of 
			// specifying the parameters, if necessary (but not required).
			//
			//---------------------------------------------------------------------------------------------------


			//... more to come ...//

		}
	}
}