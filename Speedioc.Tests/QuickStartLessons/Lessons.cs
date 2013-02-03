using NUnit.Framework;
using Speedioc.Core;
using Speedioc.Registration;
using Speedioc.TestDomain;

namespace Speedioc.QuickStartLessons
{
	[TestFixture]
	public class Lessons
	{
		/// <summary>
		/// Gets or sets the Speedioc container used throughout the Quick Start Lessons in this class.
		/// </summary>
		/// <value>
		/// The Speedioc container.
		/// </value>
		/// <remarks>
		/// The container instance is created by the TestFixtureSetup method.
		/// </remarks>
		private IContainer Container { get; set; }

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			// Create a container.
			// There are two prerequisites for creating a container:
			// 1. Container settings
			// 2. One or more registries

			// Container Settings
			// Grab a new instance of the DefaultContainerSettings class specifying an identifier for the container.
			// A container identifier is an arbitrary string and it must follow the rules for assembly names (or identifiers).
			// Also, set the ForceCompile option to true. This is optional and it causes the container to be 
			// regenerated and rebuilt each time it is initialized. 
			// This may not be good for a production app, but it just depends on what you intend to accomplish. 
			// For the scope of these lessons, turn the ForceCompile option on.
			ContainerSettings settings = new DefaultContainerSettings("SpeediocQuickStartLessons");
			settings.ForceCompile = true;

			// Registry
			// Please take the time now to look at the QuickStartRegistry class and read 
			// through all of the comments (lessons) to learn how the registration API works.
			// It is likely that you will reference the registry class throughout the lessons.
			IRegistry registry = new QuickStartRegistry();

			// Get a container builder and bulid the container.
			IContainerBuilder containerBuilder = DefaultContainerBuilderFactory.GetInstance(settings, registry);
			Container = containerBuilder.Build();	// Set the new container to the Container property for use in other test methods.

			// Now that the container has been built, there are no more opportunities to register types.
			// Don't panic. :-)
			// As you read through the tests, which resolves instances from the container, 
			// please reference the QuickStartRegistry class to observe the relationship between each 
			// registration and the tests which request instances based on those registrations.
		}

		[Test]
		public void Lesson01_ResolveATransientInstanceOfTheBoringCarClassUsingTheGenericGetinstanceMethod()
		{
			// Resolve a transient instance of the BoringCar class using the generic GetInstance method.
			
			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			BoringCar boringCar = Container.GetInstance<BoringCar>();

			// Assert
			Assert.That(boringCar, Is.Not.Null);

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.
			
			// Act
			BoringCar boringCar2 = Container.GetInstance<BoringCar>();

			// Assert
			Assert.That(boringCar2, Is.Not.Null);
			Assert.That(boringCar2, Is.Not.SameAs(boringCar));  // If the instances are not the same instance, they are transient.
		}

		[Test]
		public void Lesson02_ResolveATransientInstanceOfTheBoringCarClassUsingTheNonGenericGetinstanceMethod()
		{
			// Resolve a transient instance of the BoringCar class using the non-generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			BoringCar boringCar = (BoringCar)Container.GetInstance(typeof(BoringCar));

			// Assert
			Assert.That(boringCar, Is.Not.Null);

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			BoringCar boringCar2 = Container.GetInstance<BoringCar>();

			// Assert
			Assert.That(boringCar2, Is.Not.Null);
			Assert.That(boringCar2, Is.Not.SameAs(boringCar));  // If the instances are not the same instance, they are transient.
		}

		[Test]
		public void Lesson03_ResolveTheTransientDefaultInstanceOfTheIVehicleInterfaceUsingTheGenericGetinstanceMethod()
		{
			// Resolve the transient default instance of the IVehicle interface using the generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IVehicle vehicle = Container.GetInstance<IVehicle>();

			// Assert
			Assert.That(vehicle, Is.Not.Null);
			Assert.That(vehicle, Is.TypeOf<Car>());
			Assert.That(vehicle.ColorScheme.PrimaryColor, Is.TypeOf<WhiteColor>());
			Assert.That(vehicle.ColorScheme.SecondaryColor, Is.TypeOf<WhiteColor>());

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IVehicle vehicle2 = Container.GetInstance<IVehicle>();

			// Assert
			Assert.That(vehicle2, Is.Not.Null);
			Assert.That(vehicle2, Is.Not.SameAs(vehicle));  // If the instances are not the same instance, they are transient.
		}

		[Test]
		public void Lesson04_ResolveTheTransientDefaultInstanceOfTheIVehicleInterfaceUsingTheNonGenericGetinstanceMethod()
		{
			// Resolve the transient default instance of the IVehicle interface using the non-generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IVehicle vehicle = (IVehicle)Container.GetInstance(typeof(IVehicle));

			// Assert
			Assert.That(vehicle, Is.Not.Null);
			Assert.That(vehicle, Is.TypeOf<Car>());
			Assert.That(vehicle.ColorScheme.PrimaryColor, Is.TypeOf<WhiteColor>());
			Assert.That(vehicle.ColorScheme.SecondaryColor, Is.TypeOf<WhiteColor>());

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IVehicle vehicle2 = Container.GetInstance<IVehicle>();

			// Assert
			Assert.That(vehicle2, Is.Not.Null);
			Assert.That(vehicle2, Is.Not.SameAs(vehicle));  // If the instances are not the same instance, they are transient.
		}

		[Test]
		public void Lesson05_ResolveTheTransientNamedInstanceOfTheIVehicleInterfaceMyCarUsingTheGenericGetinstanceMethod()
		{
			// Resolve the transient named instance of the IVehicle interface "MyCar" using the generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IVehicle vehicle = Container.GetInstance<IVehicle>("MyCar");

			// Assert
			Assert.That(vehicle, Is.Not.Null);
			Assert.That(vehicle, Is.TypeOf<Car>());
			Assert.That(vehicle.ColorScheme.PrimaryColor, Is.TypeOf<WhiteColor>());
			Assert.That(vehicle.ColorScheme.SecondaryColor, Is.TypeOf<WhiteColor>());

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IVehicle vehicle2 = Container.GetInstance<IVehicle>("MyCar");

			// Assert
			Assert.That(vehicle2, Is.Not.Null);
			Assert.That(vehicle2, Is.Not.SameAs(vehicle));  // If the instances are not the same instance, they are transient.
		}

		[Test]
		public void Lesson06_ResolveTheTransientNamedInstanceOfTheIVehicleInterfaceMyCarUsingTheNonGenericGetinstanceMethod()
		{
			// Resolve the transient named instance of the IVehicle interface "MyCar" using the non-generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IVehicle vehicle = (IVehicle)Container.GetInstance(typeof(IVehicle), "MyCar");

			// Assert
			Assert.That(vehicle, Is.Not.Null);
			Assert.That(vehicle, Is.TypeOf<Car>());
			Assert.That(vehicle.ColorScheme.PrimaryColor, Is.TypeOf<WhiteColor>());
			Assert.That(vehicle.ColorScheme.SecondaryColor, Is.TypeOf<WhiteColor>());

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IVehicle vehicle2 = Container.GetInstance<IVehicle>("MyCar");

			// Assert
			Assert.That(vehicle2, Is.Not.Null);
			Assert.That(vehicle2, Is.Not.SameAs(vehicle));  // If the instances are not the same instance, they are transient.
		}

		[Test]
		public void Lesson07_ResolveTheTransientNamedInstanceOfTheIVehicleInterfaceMyNextCarUsingTheGenericGetinstanceMethod()
		{
			// Resolve the transient named instance of the IVehicle interface "MyNextCar" using the generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IVehicle vehicle = Container.GetInstance<IVehicle>("MyNextCar");

			// Assert
			Assert.That(vehicle, Is.Not.Null);
			Assert.That(vehicle, Is.TypeOf<Car>());
			Assert.That(vehicle.ColorScheme.PrimaryColor, Is.TypeOf<WhiteColor>());
			Assert.That(vehicle.ColorScheme.SecondaryColor, Is.TypeOf<WhiteColor>());

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IVehicle vehicle2 = Container.GetInstance<IVehicle>("MyNextCar");

			// Assert
			Assert.That(vehicle2, Is.Not.Null);
			Assert.That(vehicle2, Is.Not.SameAs(vehicle));  // If the instances are not the same instance, they are transient.
		}

		[Test]
		public void Lesson08_ResolveTheTransientNamedInstanceOfTheIVehicleInterfaceMyNextCarUsingTheNonGenericGetinstanceMethod()
		{
			// Resolve the transient named instance of the IVehicle interface "MyNextCar" using the non-generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IVehicle vehicle = (IVehicle)Container.GetInstance(typeof(IVehicle), "MyNextCar");

			// Assert
			Assert.That(vehicle, Is.Not.Null);
			Assert.That(vehicle, Is.TypeOf<Car>());
			Assert.That(vehicle.ColorScheme.PrimaryColor, Is.TypeOf<WhiteColor>());
			Assert.That(vehicle.ColorScheme.SecondaryColor, Is.TypeOf<WhiteColor>());

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IVehicle vehicle2 = Container.GetInstance<IVehicle>("MyNextCar");

			// Assert
			Assert.That(vehicle2, Is.Not.Null);
			Assert.That(vehicle2, Is.Not.SameAs(vehicle));  // If the instances are not the same instance, they are transient.
		}

		[Test]
		public void Lesson09_ResolveTheTransientNamedInstanceOfTheIVehicleInterfaceMyNextCar2UsingTheGenericGetinstanceMethod()
		{
			// Resolve the transient named instance of the IVehicle interface "MyNextCar2" using the generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IVehicle vehicle = Container.GetInstance<IVehicle>("MyNextCar2");

			// Assert
			Assert.That(vehicle, Is.Not.Null);
			Assert.That(vehicle, Is.TypeOf<Car>());
			Assert.That(vehicle.ColorScheme.PrimaryColor, Is.TypeOf<WhiteColor>());
			Assert.That(vehicle.ColorScheme.SecondaryColor, Is.TypeOf<WhiteColor>());

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IVehicle vehicle2 = Container.GetInstance<IVehicle>("MyNextCar2");

			// Assert
			Assert.That(vehicle2, Is.Not.Null);
			Assert.That(vehicle2, Is.Not.SameAs(vehicle));  // If the instances are not the same instance, they are transient.
		}

		[Test]
		public void Lesson10_ResolveTheTransientNamedInstanceOfTheIVehicleInterfaceMyNextCar2UsingTheNonGenericGetinstanceMethod()
		{
			// Resolve the transient named instance of the IVehicle interface "MyNextCar2" using the non-generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IVehicle vehicle = (IVehicle)Container.GetInstance(typeof(IVehicle), "MyNextCar2");

			// Assert
			Assert.That(vehicle, Is.Not.Null);
			Assert.That(vehicle, Is.TypeOf<Car>());
			Assert.That(vehicle.ColorScheme.PrimaryColor, Is.TypeOf<WhiteColor>());
			Assert.That(vehicle.ColorScheme.SecondaryColor, Is.TypeOf<WhiteColor>());

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IVehicle vehicle2 = Container.GetInstance<IVehicle>("MyNextCar2");

			// Assert
			Assert.That(vehicle2, Is.Not.Null);
			Assert.That(vehicle2, Is.Not.SameAs(vehicle));  // If the instances are not the same instance, they are transient.
		}

		[Test]
		public void Lesson11_ResolveTheTransientNamedInstanceOfTheIVehicleInterfaceMyNextCar3UsingTheGenericGetinstanceMethod()
		{
			// Resolve the transient named instance of the IVehicle interface "MyNextCar3" using the generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IVehicle vehicle = Container.GetInstance<IVehicle>("MyNextCar3");

			// Assert
			Assert.That(vehicle, Is.Not.Null);
			Assert.That(vehicle, Is.TypeOf<Car>());
			Assert.That(vehicle.ColorScheme.PrimaryColor, Is.TypeOf<WhiteColor>());
			Assert.That(vehicle.ColorScheme.SecondaryColor, Is.TypeOf<WhiteColor>());

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IVehicle vehicle2 = Container.GetInstance<IVehicle>("MyNextCar3");

			// Assert
			Assert.That(vehicle2, Is.Not.Null);
			Assert.That(vehicle2, Is.Not.SameAs(vehicle));  // If the instances are not the same instance, they are transient.
		}

		[Test]
		public void Lesson12_ResolveTheTransientNamedInstanceOfTheIVehicleInterfaceMyNextCar3UsingTheNonGenericGetinstanceMethod()
		{
			// Resolve the transient named instance of the IVehicle interface "MyNextCar3" using the non-generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IVehicle vehicle = (IVehicle)Container.GetInstance(typeof(IVehicle), "MyNextCar3");

			// Assert
			Assert.That(vehicle, Is.Not.Null);
			Assert.That(vehicle, Is.TypeOf<Car>());
			Assert.That(vehicle.ColorScheme.PrimaryColor, Is.TypeOf<WhiteColor>());
			Assert.That(vehicle.ColorScheme.SecondaryColor, Is.TypeOf<WhiteColor>());

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IVehicle vehicle2 = Container.GetInstance<IVehicle>("MyNextCar3");

			// Assert
			Assert.That(vehicle2, Is.Not.Null);
			Assert.That(vehicle2, Is.Not.SameAs(vehicle));  // If the instances are not the same instance, they are transient.
		}

		[Test]
		public void Lesson13_ResolveTheContainerLifetimeDefaultInstanceOfTheIColorInterfaceUsingTheGenericGetinstanceMethod()
		{
			// Resolve the container lifetime default instance of the IColor interface using the generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IColor color = Container.GetInstance<IColor>();

			// Assert
			Assert.That(color, Is.Not.Null);
			Assert.That(color, Is.TypeOf<RedColor>());
			Assert.That(color.Name, Is.EqualTo("Red"));

			//--------------------------------------------------
			// Prove that the instance has a container lifetime.

			// Act
			IColor color2 = Container.GetInstance<IColor>();

			// Assert
			Assert.That(color2, Is.Not.Null);
			Assert.That(color2, Is.SameAs(color));  // If the instances are the same instance, they are a singleton.
		}

		[Test]
		public void Lesson14_ResolveTheContainerLifetimeDefaultInstanceOfTheIColorInterfaceUsingTheNonGenericGetinstanceMethod()
		{
			// Resolve the container lifetime default instance of the IColor interface using the non-generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IColor color = (IColor)Container.GetInstance(typeof(IColor));

			// Assert
			Assert.That(color, Is.Not.Null);
			Assert.That(color, Is.TypeOf<RedColor>());
			Assert.That(color.Name, Is.EqualTo("Red"));

			//--------------------------------------------------
			// Prove that the instance has a container lifetime.

			// Act
			IColor color2 = Container.GetInstance<IColor>();

			// Assert
			Assert.That(color2, Is.Not.Null);
			Assert.That(color2, Is.SameAs(color));  // If the instances are the same instance, they are a singleton.
		}

		[Test]
		public void Lesson15_ResolveTheTransientNamedInstanceOfTheIColorInterfaceLightBlueUsingTheGenericGetinstanceMethod()
		{
			// Resolve the transient named instance of the IColor interface "LightBlue" using the generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IColor color = Container.GetInstance<IColor>("LightBlue");

			// Assert
			Assert.That(color, Is.Not.Null);
			Assert.That(color, Is.TypeOf<Color>());
			Assert.That(color.Name, Is.EqualTo("LightBlue"));

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IColor color2 = Container.GetInstance<IColor>("LightBlue");

			// Assert
			Assert.That(color2, Is.Not.Null);
			Assert.That(color2, Is.Not.SameAs(color));  // If the instances are not the same instance, they are transient.
		}

		[Test]
		public void Lesson16_ResolveTheTransientNamedInstanceOfTheIColorInterfaceLightBlueUsingTheNonGenericGetinstanceMethod()
		{
			// Resolve the transient named instance of the IColor interface "LightBlue" using the non-generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IColor color = (IColor)Container.GetInstance(typeof(IColor), "LightBlue");

			// Assert
			Assert.That(color, Is.Not.Null);
			Assert.That(color, Is.TypeOf<Color>());
			Assert.That(color.Name, Is.EqualTo("LightBlue"));

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IColor color2 = Container.GetInstance<IColor>("LightBlue");

			// Assert
			Assert.That(color2, Is.Not.Null);
			Assert.That(color2, Is.Not.SameAs(color));  // If the instances are not the same instance, they are transient.
		}

		[Test]
		public void Lesson17_ResolveTheTransientNamedInstanceOfTheIColorInterfaceRedUsingTheGenericGetinstanceMethod()
		{
			// Resolve the transient named instance of the IColor interface "Red" using the generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IColor color = Container.GetInstance<IColor>("Red");

			// Assert
			Assert.That(color, Is.Not.Null);
			Assert.That(color, Is.TypeOf<Color>());
			Assert.That(color.Name, Is.EqualTo("Red"));

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IColor color2 = Container.GetInstance<IColor>("Red");

			// Assert
			Assert.That(color2, Is.Not.Null);
			Assert.That(color2, Is.Not.SameAs(color));  // If the instances are not the same instance, they are transient.

			//-------------------------------------------------------------------------------
			// Prove that the instance has its inner color injected from a resolved instance.

			// Arrange
			Color concreteColor = (Color) color;

			// Act
			IColor color3 = Container.GetInstance<IColor>();	// Same as the injected constructor parameter.

			// Assert
			Assert.That(concreteColor.InnerColor, Is.SameAs(color3));  // Compare the instances because the resolved lifetime is container (singleton).
		}

		[Test]
		public void Lesson18_ResolveTheTransientNamedInstanceOfTheIColorInterfaceRedUsingTheNonGenericGetinstanceMethod()
		{
			// Resolve the transient named instance of the IColor interface "Red" using the non-generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IColor color = (IColor)Container.GetInstance(typeof(IColor), "Red");

			// Assert
			Assert.That(color, Is.Not.Null);
			Assert.That(color, Is.TypeOf<Color>());
			Assert.That(color.Name, Is.EqualTo("Red"));

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IColor color2 = Container.GetInstance<IColor>("Red");

			// Assert
			Assert.That(color2, Is.Not.Null);
			Assert.That(color2, Is.Not.SameAs(color));  // If the instances are not the same instance, they are transient.

			//-------------------------------------------------------------------------------
			// Prove that the instance has its inner color injected from a resolved instance.

			// Arrange
			Color concreteColor = (Color)color;

			// Act
			IColor color3 = Container.GetInstance<IColor>();	// Same as the injected constructor parameter.

			// Assert
			Assert.That(concreteColor.InnerColor, Is.SameAs(color3));  // Compare the instances because the resolved lifetime is container (singleton).
		}

		[Test]
		public void Lesson19_ResolveTheTransientNamedInstanceOfTheIColorInterfaceLightBlue2UsingTheGenericGetinstanceMethod()
		{
			// Resolve the transient named instance of the IColor interface "Light-Blue-2" using the generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IColor color = Container.GetInstance<IColor>("Light-Blue-2");

			// Assert
			Assert.That(color, Is.Not.Null);
			Assert.That(color, Is.TypeOf<Color>());
			Assert.That(color.Name, Is.EqualTo("LightBlue"));

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IColor color2 = Container.GetInstance<IColor>("Light-Blue-2");

			// Assert
			Assert.That(color2, Is.Not.Null);
			Assert.That(color2, Is.Not.SameAs(color));  // If the instances are not the same instance, they are transient.

			//-------------------------------------------------------------------------------
			// Prove that the instance has its inner color injected from a resolved instance.

			// Arrange
			Color concreteColor = (Color)color;

			// Act
			IColor color3 = Container.GetInstance<IColor>("LightBlue");	// Same as the injected constructor parameter.

			// Assert
			Assert.That(concreteColor.InnerColor.Name, Is.EqualTo(color3.Name));  // Compare Name property because the instance is transient.
		}

		[Test]
		public void Lesson20_ResolveTheTransientNamedInstanceOfTheIColorInterfaceLightBlue2UsingTheNonGenericGetinstanceMethod()
		{
			// Resolve the transient named instance of the IColor interface "Light-Blue-2" using the non-generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IColor color = (IColor)Container.GetInstance(typeof(IColor), "Light-Blue-2");

			// Assert
			Assert.That(color, Is.Not.Null);
			Assert.That(color, Is.TypeOf<Color>());
			Assert.That(color.Name, Is.EqualTo("LightBlue"));

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IColor color2 = Container.GetInstance<IColor>("Light-Blue-2");

			// Assert
			Assert.That(color2, Is.Not.Null);
			Assert.That(color2, Is.Not.SameAs(color));  // If the instances are not the same instance, they are transient.

			//-------------------------------------------------------------------------------
			// Prove that the instance has its inner color injected from a resolved instance.

			// Arrange
			Color concreteColor = (Color)color;

			// Act
			IColor color3 = Container.GetInstance<IColor>("LightBlue");	// Same as the injected constructor parameter.

			// Assert
			Assert.That(concreteColor.InnerColor.Name, Is.EqualTo(color3.Name));  // Compare Name property because the instance is transient.
		}

		[Test]
		public void Lesson21_ResolveTheTransientNamedInstanceOfTheIColorInterfaceRed2UsingTheGenericGetinstanceMethod()
		{
			// Resolve the transient named instance of the IColor interface "Red-2" using the generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IColor color = Container.GetInstance<IColor>("Red-2");

			// Assert
			Assert.That(color, Is.Not.Null);
			Assert.That(color, Is.TypeOf<Color>());
			Assert.That(color.Name, Is.EqualTo("Red"));

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IColor color2 = Container.GetInstance<IColor>("Red-2");

			// Assert
			Assert.That(color2, Is.Not.Null);
			Assert.That(color2, Is.Not.SameAs(color));  // If the instances are not the same instance, they are transient.

			//-------------------------------------------------------------------------------
			// Prove that the instance has its inner color injected from a resolved instance.

			// Arrange
			Color concreteColor = (Color)color;

			// Act
			IColor color3 = Container.GetInstance<IColor>("Red");	// Same as the injected constructor parameter.

			// Assert
			Assert.That(concreteColor.InnerColor.Name, Is.EqualTo(color3.Name));  // Compare Name property because the instance is transient.
		}

		[Test]
		public void Lesson22_ResolveTheTransientNamedInstanceOfTheIColorInterfaceRed2UsingTheNonGenericGetinstanceMethod()
		{
			// Resolve the transient named instance of the IColor interface "Red-2" using the non-generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IColor color = (IColor)Container.GetInstance(typeof(IColor), "Red-2");

			// Assert
			Assert.That(color, Is.Not.Null);
			Assert.That(color, Is.TypeOf<Color>());
			Assert.That(color.Name, Is.EqualTo("Red"));

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IColor color2 = Container.GetInstance<IColor>("Red-2");

			// Assert
			Assert.That(color2, Is.Not.Null);
			Assert.That(color2, Is.Not.SameAs(color));  // If the instances are not the same instance, they are transient.

			//-------------------------------------------------------------------------------
			// Prove that the instance has its inner color injected from a resolved instance.

			// Arrange
			Color concreteColor = (Color)color;

			// Act
			IColor color3 = Container.GetInstance<IColor>("Red");	// Same as the injected constructor parameter.

			// Assert
			Assert.That(concreteColor.InnerColor.Name, Is.EqualTo(color3.Name));  // Compare Name property because the instance is transient.
		}

		[Test]
		public void Lesson23_ResolveTheTransientNamedInstanceOfTheIColorSchemeInterfaceRedAndBlackUsingTheGenericGetinstanceMethod()
		{
			// Resolve the transient named instance of the IColorScheme interface "RedAndBlack" using the generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IColorScheme colorScheme = Container.GetInstance<IColorScheme>("RedAndBlack");

			// Assert
			Assert.That(colorScheme, Is.Not.Null);
			Assert.That(colorScheme, Is.TypeOf<ColorScheme>());
			Assert.That(colorScheme.Name, Is.EqualTo("Red-Black"));
			Assert.That(colorScheme.PrimaryColor.Name, Is.EqualTo("Red"));
			Assert.That(colorScheme.SecondaryColor, Is.TypeOf<BlackColor>());

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IColorScheme colorScheme2 = Container.GetInstance<IColorScheme>("RedAndBlack");

			// Assert
			Assert.That(colorScheme2, Is.Not.Null);
			Assert.That(colorScheme2, Is.Not.SameAs(colorScheme));  // If the instances are not the same instance, they are transient.

			//-------------------------------------------------------------------------------
			// Prove that the instance has its primary color injected from a resolved instance.

			// Arrange
			ColorScheme concreteColorScheme = (ColorScheme)colorScheme;

			// Act
			IColor color = Container.GetInstance<IColor>("Red-2");	// Same as the injected constructor parameter.

			// Assert
			Assert.That(concreteColorScheme.PrimaryColor.Name, Is.EqualTo(color.Name));  // Compare Name property because the instance is transient.
		}

		[Test]
		public void Lesson24_ResolveTheTransientNamedInstanceOfTheIColorSchemeSchemeInterfaceRedAndBlackUsingTheNonGenericGetinstanceMethod()
		{
			// Resolve the transient named instance of the IColorScheme interface "RedAndBlack" using the non-generic GetInstance method.

			// Arrange
			// The container is created in the TestFixtureSetUp method and is assigned to the Container property.
			// There is nothing else to arrange for this test.

			// Act
			IColorScheme colorScheme = (IColorScheme)Container.GetInstance(typeof(IColorScheme), "RedAndBlack");

			// Assert
			Assert.That(colorScheme, Is.Not.Null);
			Assert.That(colorScheme, Is.TypeOf<ColorScheme>());
			Assert.That(colorScheme.Name, Is.EqualTo("Red-Black"));
			Assert.That(colorScheme.PrimaryColor.Name, Is.EqualTo("Red"));
			Assert.That(colorScheme.SecondaryColor, Is.TypeOf<BlackColor>());

			//--------------------------------------------------
			// Prove that the instance has a transient lifetime.

			// Act
			IColorScheme colorScheme2 = Container.GetInstance<IColorScheme>("RedAndBlack");

			// Assert
			Assert.That(colorScheme2, Is.Not.Null);
			Assert.That(colorScheme2, Is.Not.SameAs(colorScheme));  // If the instances are not the same instance, they are transient.

			//-------------------------------------------------------------------------------
			// Prove that the instance has its primary color injected from a resolved instance.

			// Arrange
			ColorScheme concreteColorScheme = (ColorScheme)colorScheme;

			// Act
			IColor color = Container.GetInstance<IColor>("Red-2");	// Same as the injected constructor parameter.

			// Assert
			Assert.That(concreteColorScheme.PrimaryColor.Name, Is.EqualTo(color.Name));  // Compare Name property because the instance is transient.
		}
	}
}