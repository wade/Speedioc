using NUnit.Framework;
using Speedioc.Registration;
using Speedioc.TestDomain;

namespace Speedioc
{
	[TestFixture]
	public partial class ContainerTestBase
	{
		// This is a partial class file: ContainerTestBase.TransientLifetimeTests.cs
		// The main declaration is in the file: ContainerTestBase.cs
		// This file contains tests of the container against instances that are registered with an Transient lifetime.

		[Test]
		public void TransientLifetimeTest_1_ContainerCanGetInstanceOfUnmappedTypeWithWithoutInjectionWithImplicitDefaultLifetime()
		{
			// Arrange
			const int testId = 1;
			IContainerSettings settings = RegisterSettings(testId);
			IRegistry registry = new Registry();
			registry.Register<Car>();
			IContainer container = CreateContainer(settings, registry);

			// Act
			Car car = container.GetInstance<Car>();

			// Assert
			Assert.That(car, Is.Not.Null);
			Assert.That(car.Make, Is.Null);
			Assert.That(car.Model, Is.Null);
			Assert.That(car.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(car.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));
		}

		[Test]
		public void TransientLifetimeTest_2_ContainerCanGetTwoInstancesOfUnmappedTypeWithTransientLifetimeWithoutInjectionWithImplicitDefaultLifetime()
		{
			// Arrange
			const int testId = 2;
			IContainerSettings settings = RegisterSettings(testId);
			IRegistry registry = new Registry();
			registry.Register<Car>();
			IContainer container = CreateContainer(settings, registry);

			// Act
			Car car = container.GetInstance<Car>();
			Car car2 = container.GetInstance<Car>();

			// Assert
			Assert.That(car, Is.Not.Null);
			Assert.That(car.Make, Is.Null);
			Assert.That(car.Model, Is.Null);
			Assert.That(car.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(car.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			Assert.That(car2, Is.Not.Null);
			Assert.That(car2.Make, Is.Null);
			Assert.That(car2.Model, Is.Null);
			Assert.That(car2.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(car2.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			Assert.That(car, Is.Not.SameAs(car2));
		}

		[Test]
		public void TransientLifetimeTest_3_ContainerCanGetInstanceOfUnmappedTypeWithWithoutInjectionWithExplicitLifetime()
		{
			// Arrange
			const int testId = 3;
			IContainerSettings settings = RegisterSettings(testId);
			IRegistry registry = new Registry();
			registry.Register<Car>().WithLifetime(Lifetime.Transient);
			IContainer container = CreateContainer(settings, registry);

			// Act
			Car car = container.GetInstance<Car>();

			// Assert
			Assert.That(car, Is.Not.Null);
			Assert.That(car.Make, Is.Null);
			Assert.That(car.Model, Is.Null);
			Assert.That(car.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(car.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));
		}

		[Test]
		public void TransientLifetimeTest_4_ContainerCanGetTwoInstancesOfUnmappedTypeWithTransientLifetimeWithoutInjectionWithExplicitLifetime()
		{
			// Arrange
			const int testId = 4;
			IContainerSettings settings = RegisterSettings(testId);
			IRegistry registry = new Registry();
			registry.Register<Car>().WithLifetime(Lifetime.Transient);
			IContainer container = CreateContainer(settings, registry);

			// Act
			Car car = container.GetInstance<Car>();
			Car car2 = container.GetInstance<Car>();

			// Assert
			Assert.That(car, Is.Not.Null);
			Assert.That(car.Make, Is.Null);
			Assert.That(car.Model, Is.Null);
			Assert.That(car.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(car.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			Assert.That(car2, Is.Not.Null);
			Assert.That(car2.Make, Is.Null);
			Assert.That(car2.Model, Is.Null);
			Assert.That(car2.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(car2.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			Assert.That(car, Is.Not.SameAs(car2));
		}

		[Test]
		public void TransientLifetimeTest_5_ContainerCanGetInstanceOfMappedTypeWithWithoutInjectionWithImplicitDefaultLifetime()
		{
			// Arrange
			const int testId = 5;
			IContainerSettings settings = RegisterSettings(testId);
			IRegistry registry = new Registry();
			registry.Register<Car>().As(typeof(IVehicle));
			IContainer container = CreateContainer(settings, registry);

			// Act
			IVehicle vehicle = container.GetInstance<IVehicle>();

			// Assert
			Assert.That(vehicle, Is.Not.Null);
			Assert.That(vehicle, Is.TypeOf<Car>());
			Assert.That(vehicle.Make, Is.Null);
			Assert.That(vehicle.Model, Is.Null);
			Assert.That(vehicle.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(vehicle.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));
		}

		[Test]
		public void TransientLifetimeTest_6_ContainerCanGetInstanceOfGenericMappedTypeWithWithoutInjectionWithImplicitDefaultLifetime()
		{
			// Arrange
			const int testId = 6;
			IContainerSettings settings = RegisterSettings(testId);
			IRegistry registry = new Registry();
			registry.Register<Car>().As<IVehicle>();
			IContainer container = CreateContainer(settings, registry);

			// Act
			IVehicle vehicle = container.GetInstance<IVehicle>();

			// Assert
			Assert.That(vehicle, Is.Not.Null);
			Assert.That(vehicle, Is.TypeOf<Car>());
			Assert.That(vehicle.Make, Is.Null);
			Assert.That(vehicle.Model, Is.Null);
			Assert.That(vehicle.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(vehicle.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));
		}

		[Test]
		public void TransientLifetimeTest_7_ContainerCanGetTwoInstancesOfMappedTypeWithTransientLifetimeWithoutInjectionWithImplicitDefaultLifetime()
		{
			// Arrange
			const int testId = 7;
			IContainerSettings settings = RegisterSettings(testId);
			IRegistry registry = new Registry();
			registry.Register<Car>().As(typeof(IVehicle));
			IContainer container = CreateContainer(settings, registry);

			// Act
			IVehicle vehicle = container.GetInstance<IVehicle>();
			IVehicle vehicle2 = container.GetInstance<IVehicle>();

			// Assert
			Assert.That(vehicle, Is.Not.Null);
			Assert.That(vehicle, Is.TypeOf<Car>());
			Assert.That(vehicle.Make, Is.Null);
			Assert.That(vehicle.Model, Is.Null);
			Assert.That(vehicle.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(vehicle.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			Assert.That(vehicle2, Is.Not.Null);
			Assert.That(vehicle2, Is.TypeOf<Car>());
			Assert.That(vehicle2.Make, Is.Null);
			Assert.That(vehicle2.Model, Is.Null);
			Assert.That(vehicle2.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(vehicle2.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			Assert.That(vehicle, Is.Not.SameAs(vehicle2));
		}

		[Test]
		public void TransientLifetimeTest_8_ContainerCanGetTwoInstancesOfGenericMappedTypeWithTransientLifetimeWithoutInjectionWithImplicitDefaultLifetime()
		{
			// Arrange
			const int testId = 8;
			IContainerSettings settings = RegisterSettings(testId);
			IRegistry registry = new Registry();
			registry.Register<Car>().As<IVehicle>();
			IContainer container = CreateContainer(settings, registry);

			// Act
			IVehicle vehicle = container.GetInstance<IVehicle>();
			IVehicle vehicle2 = container.GetInstance<IVehicle>();

			// Assert
			Assert.That(vehicle, Is.Not.Null);
			Assert.That(vehicle, Is.TypeOf<Car>());
			Assert.That(vehicle.Make, Is.Null);
			Assert.That(vehicle.Model, Is.Null);
			Assert.That(vehicle.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(vehicle.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			Assert.That(vehicle2, Is.Not.Null);
			Assert.That(vehicle2, Is.TypeOf<Car>());
			Assert.That(vehicle2.Make, Is.Null);
			Assert.That(vehicle2.Model, Is.Null);
			Assert.That(vehicle2.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(vehicle2.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			Assert.That(vehicle, Is.Not.SameAs(vehicle2));
		}

		[Test]
		public void TransientLifetimeTest_9_ContainerCanGetInstanceOfUnmappedTypeUsingConstructorInjectionWithValueParametersWithImplicitDefaultLifetime()
		{
			// Arrange
			const int testId = 9;
			IContainerSettings settings = RegisterSettings(testId);
			IRegistry registry = new Registry();
			
			registry.Register<Car>()
				.UsingConstructor()
					.WithValueParameter("Ford")
					.WithValueParameter("Mustang")
					.AsLastParameter();

			IContainer container = CreateContainer(settings, registry);

			// Act
			Car car = container.GetInstance<Car>();

			// Assert
			Assert.That(car, Is.Not.Null);
			Assert.That(car.Make, Is.EqualTo("Ford"));
			Assert.That(car.Model, Is.EqualTo("Mustang"));
			Assert.That(car.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(car.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));
		}

		[Test]
		public void TransientLifetimeTest_10_ContainerCanGetInstanceOfUnmappedTypeUsingConstructorInjectionWithValueAndResolvedParametersWithImplicitDefaultLifetime()
		{
			// Arrange
			const int testId = 10;
			IContainerSettings settings = RegisterSettings(testId);
			IRegistry registry = new Registry();

			registry.Register<Car>()
				.UsingConstructor()
					.WithValueParameter("Ford")
					.WithValueParameter("Mustang")
					.WithResolvedParameter<IColorScheme>()
					.AsLastParameter();

			registry.Register<ColorScheme>()
				.As<IColorScheme>()
				.UsingConstructor()
					.WithValueParameter("Red And Black Color Scheme")
					.WithValueFactoryParameter<IColor>(() => new RedColor())
					.WithValueFactoryParameter<IColor>(() => new BlackColor())
					.AsLastParameter();

			IContainer container = CreateContainer(settings, registry);

			// Act
			Car car = container.GetInstance<Car>();

			// Assert
			Assert.That(car, Is.Not.Null);
			Assert.That(car.Make, Is.EqualTo("Ford"));
			Assert.That(car.Model, Is.EqualTo("Mustang"));
			Assert.That(car.ColorScheme.Name, Is.EqualTo("Red And Black Color Scheme"));
			Assert.That(car.ColorScheme.PrimaryColor.Name, Is.EqualTo("Red"));
			Assert.That(car.ColorScheme.SecondaryColor.Name, Is.EqualTo("Black"));
		}

		[Test]
		public void TransientLifetimeTest_11_ContainerCanGetInstanceOfUnmappedTypeUsingConstructorInjectionWithValueAndValueFactoryParametersWithImplicitDefaultLifetime()
		{
			// Arrange
			const int testId = 11;
			IContainerSettings settings = RegisterSettings(testId);
			IRegistry registry = new Registry();

			registry.Register<Car>()
				.UsingConstructor()
					.WithValueParameter("Ford")
					.WithValueParameter("Mustang")
					.WithValueFactoryParameter<IColorScheme>(
						() => new ColorScheme("Custom Color Scheme", new BlueColor(), new OrangeColor()))
					.AsLastParameter();

			IContainer container = CreateContainer(settings, registry);

			// Act
			Car car = container.GetInstance<Car>();

			// Assert
			Assert.That(car, Is.Not.Null);
			Assert.That(car.Make, Is.EqualTo("Ford"));
			Assert.That(car.Model, Is.EqualTo("Mustang"));
			Assert.That(car.ColorScheme.Name, Is.EqualTo("Custom Color Scheme"));
			Assert.That(car.ColorScheme.PrimaryColor.Name, Is.EqualTo("Blue"));
			Assert.That(car.ColorScheme.SecondaryColor.Name, Is.EqualTo("Orange"));
		}

		[Test]
		public void TransientLifetimeTest_12_ContainerCanGetInstanceOfUnmappedTypeUsingValueFieldInjectionWithImplicitDefaultLifetime()
		{
			// Arrange
			const int testId = 12;
			IContainerSettings settings = RegisterSettings(testId);
			IRegistry registry = new Registry();

			registry.Register<Car>()
				.WithField<int>("EngineSizeCubicInches").SetTo(5);

			IContainer container = CreateContainer(settings, registry);

			// Act
			Car car = container.GetInstance<Car>();

			// Assert
			Assert.That(car, Is.Not.Null);
			Assert.That(car.Make, Is.Null);
			Assert.That(car.Model, Is.Null);
			Assert.That(car.ColorScheme.Name, Is.EqualTo("White"));
			Assert.That(car.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(car.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));
			Assert.That(car.EngineSizeCubicInches, Is.EqualTo(5));
		}

		[Test]
		public void TransientLifetimeTest_13_ContainerCanGetInstanceOfUnmappedTypeUsingValueFactoryFieldInjectionWithImplicitDefaultLifetime()
		{
			// Arrange
			const int testId = 13;
			IContainerSettings settings = RegisterSettings(testId);
			IRegistry registry = new Registry();

			registry.Register<Car>()
				.WithField<int>("EngineSizeCubicInches").SetToValueFactory<int>(() => 4 + 1);

			IContainer container = CreateContainer(settings, registry);

			// Act
			Car car = container.GetInstance<Car>();

			// Assert
			Assert.That(car, Is.Not.Null);
			Assert.That(car.Make, Is.Null);
			Assert.That(car.Model, Is.Null);
			Assert.That(car.ColorScheme.Name, Is.EqualTo("White"));
			Assert.That(car.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(car.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));
			Assert.That(car.EngineSizeCubicInches, Is.EqualTo(5));
		}

		[Test]
		public void TransientLifetimeTest_14_ContainerCanGetInstanceOfUnmappedTypeUsingResolvedFieldInjectionWithImplicitDefaultLifetime()
		{
			// Arrange
			const int testId = 14;
			IContainerSettings settings = RegisterSettings(testId);
			IRegistry registry = new Registry();

			registry.Register<Car>()
				.WithField<int>("EngineSizeCubicInches").SetToResolvedValue("DefaultEngineSize");

			registry.Register<int>().WithName("DefaultEngineSize").WithPrimitiveValue(5);

			IContainer container = CreateContainer(settings, registry);

			// Act
			Car car = container.GetInstance<Car>();

			// Assert
			Assert.That(car, Is.Not.Null);
			Assert.That(car.Make, Is.Null);
			Assert.That(car.Model, Is.Null);
			Assert.That(car.ColorScheme.Name, Is.EqualTo("White"));
			Assert.That(car.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(car.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));
			Assert.That(car.EngineSizeCubicInches, Is.EqualTo(5));
		}
	}
}