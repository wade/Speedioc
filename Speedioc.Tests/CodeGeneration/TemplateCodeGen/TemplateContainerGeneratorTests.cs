using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Speedioc.Core;
using Speedioc.Registration;
using Speedioc.Registration.Core;
using Speedioc.TestDomain;

namespace Speedioc.CodeGeneration.TemplateCodeGen
{
	[TestFixture]
	public class TemplateContainerGeneratorTests : ContainerTestBase
	{
		protected override IContainer CreateContainer(IContainerSettings settings, IRegistry registry)
		{
			IContainerGenerator containerGenerator = new TemplateContainerGenerator();
			IRegistrationAggregator registrationAggregator = new RegistrationAggregator();
			IList<IRegistration> registrations = registrationAggregator.AggregateRegistrations(new List<IRegistry> { registry });
			return containerGenerator.GenerateContainer(registrations, settings);
		}

		protected override string GeneratedFilesDirectoryName
		{
			get { return GetType().Name; }
		}

		[Test]
		public void IntegrationTest()
		{
			IContainerSettings settings = RegisterSettings("TemplateContainerGeneratorTests_IntegrationTest");
			IRegistry testRegistry = new TestRegistry();
			IContainer container = CreateContainer(settings, testRegistry);

			Assert.That(container, Is.Not.Null);

			Car car1 = container.GetInstance<Car>();
			Assert.That(car1, Is.Not.Null);

			Car car2 = container.GetInstance<Car>();
			Assert.That(car2, Is.Not.Null);

			Assert.That(car1, Is.Not.SameAs(car2));

			IVehicle vehicle = container.GetInstance<IVehicle>();
			Assert.That(vehicle, Is.Not.Null);
			Assert.That(vehicle, Is.TypeOf<Car>());
			Assert.That(vehicle.Make, Is.Null);
			Assert.That(vehicle.Model, Is.Null);
			Assert.That(vehicle.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(vehicle.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			IVehicle whiteCar = container.GetInstance<IVehicle>("WhiteCar");
			Assert.That(whiteCar, Is.Not.Null);
			Assert.That(whiteCar, Is.TypeOf<Car>());
			Assert.That(whiteCar.Make, Is.Null);
			Assert.That(whiteCar.Model, Is.Null);
			Assert.That(whiteCar.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(whiteCar.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			IVehicle viper = container.GetInstance<IVehicle>("Viper");
			Assert.That(viper, Is.Not.Null);
			Assert.That(viper, Is.TypeOf<Car>());
			Assert.That(viper.Make, Is.EqualTo("Dodge"));
			Assert.That(viper.Model, Is.EqualTo("Viper"));
			Assert.That(viper.ColorScheme.PrimaryColor.Name, Is.EqualTo("Red"));
			Assert.That(viper.ColorScheme.SecondaryColor.Name, Is.EqualTo("Black"));

			IVehicle viper2 = container.GetInstance<IVehicle>("Viper2");
			Assert.That(viper2, Is.Not.Null);
			Assert.That(viper2, Is.TypeOf<Car>());
			Assert.That(viper2.Make, Is.EqualTo("Dodge"));
			Assert.That(viper2.Model, Is.EqualTo("Viper"));
			Assert.That(viper2.ColorScheme.Name, Is.EqualTo("ViperRedBlack"));
			Assert.That(viper2.ColorScheme.PrimaryColor.Name, Is.EqualTo("Red"));
			Assert.That(viper2.ColorScheme.SecondaryColor.Name, Is.EqualTo("Black"));

			Assert.That(viper, Is.Not.SameAs(viper2));

			IVehicle appDomainLifetimeCar = container.GetInstance<IVehicle>("AppDomainLifetimeCar");
			Assert.That(appDomainLifetimeCar, Is.Not.Null);
			Assert.That(appDomainLifetimeCar, Is.TypeOf<Car>());
			Assert.That(appDomainLifetimeCar.Make, Is.Null);
			Assert.That(appDomainLifetimeCar.Model, Is.Null);
			Assert.That(appDomainLifetimeCar.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(appDomainLifetimeCar.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			IVehicle appDomainLifetimeCar2 = container.GetInstance<IVehicle>("AppDomainLifetimeCar");
			Assert.That(appDomainLifetimeCar2, Is.Not.Null);
			Assert.That(appDomainLifetimeCar2, Is.TypeOf<Car>());
			Assert.That(appDomainLifetimeCar2.Make, Is.Null);
			Assert.That(appDomainLifetimeCar2.Model, Is.Null);
			Assert.That(appDomainLifetimeCar2.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(appDomainLifetimeCar2.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			Assert.That(appDomainLifetimeCar, Is.SameAs(appDomainLifetimeCar2));

			IVehicle containerLifetimeCar = container.GetInstance<IVehicle>("ContainerLifetimeCar");
			Assert.That(containerLifetimeCar, Is.Not.Null);
			Assert.That(containerLifetimeCar, Is.TypeOf<Car>());
			Assert.That(containerLifetimeCar.Make, Is.Null);
			Assert.That(containerLifetimeCar.Model, Is.Null);
			Assert.That(containerLifetimeCar.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(containerLifetimeCar.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			IVehicle containerLifetimeCar2 = container.GetInstance<IVehicle>("ContainerLifetimeCar");
			Assert.That(containerLifetimeCar2, Is.Not.Null);
			Assert.That(containerLifetimeCar2, Is.TypeOf<Car>());
			Assert.That(containerLifetimeCar2.Make, Is.Null);
			Assert.That(containerLifetimeCar2.Model, Is.Null);
			Assert.That(containerLifetimeCar2.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(containerLifetimeCar2.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			Assert.That(containerLifetimeCar, Is.SameAs(containerLifetimeCar2));

			IVehicle containerLifetimePreCreatedCar = container.GetInstance<IVehicle>("ContainerLifetimePreCreatedCar");
			Assert.That(containerLifetimePreCreatedCar, Is.Not.Null);
			Assert.That(containerLifetimePreCreatedCar, Is.TypeOf<Car>());
			Assert.That(containerLifetimePreCreatedCar.Make, Is.Null);
			Assert.That(containerLifetimePreCreatedCar.Model, Is.Null);
			Assert.That(containerLifetimePreCreatedCar.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(containerLifetimePreCreatedCar.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			IVehicle containerLifetimePreCreatedCar2 = container.GetInstance<IVehicle>("ContainerLifetimePreCreatedCar");
			Assert.That(containerLifetimePreCreatedCar2, Is.Not.Null);
			Assert.That(containerLifetimePreCreatedCar2, Is.TypeOf<Car>());
			Assert.That(containerLifetimePreCreatedCar2.Make, Is.Null);
			Assert.That(containerLifetimePreCreatedCar2.Model, Is.Null);
			Assert.That(containerLifetimePreCreatedCar2.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(containerLifetimePreCreatedCar2.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			Assert.That(containerLifetimePreCreatedCar, Is.SameAs(containerLifetimePreCreatedCar2));

			Assert.That(containerLifetimePreCreatedCar, Is.Not.SameAs(containerLifetimeCar));

			IVehicle threadLifetimeCar = container.GetInstance<IVehicle>("ThreadLifetimeCar");
			Assert.That(threadLifetimeCar, Is.Not.Null);
			Assert.That(threadLifetimeCar, Is.TypeOf<Car>());
			Assert.That(threadLifetimeCar.Make, Is.Null);
			Assert.That(threadLifetimeCar.Model, Is.Null);
			Assert.That(threadLifetimeCar.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(threadLifetimeCar.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			IVehicle threadLifetimeCar2 = container.GetInstance<IVehicle>("ThreadLifetimeCar");
			Assert.That(threadLifetimeCar2, Is.Not.Null);
			Assert.That(threadLifetimeCar2, Is.TypeOf<Car>());
			Assert.That(threadLifetimeCar2.Make, Is.Null);
			Assert.That(threadLifetimeCar2.Model, Is.Null);
			Assert.That(threadLifetimeCar2.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(threadLifetimeCar2.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			Assert.That(threadLifetimeCar, Is.SameAs(threadLifetimeCar2));

			IVehicle threadLifetimeCar3 = null;
			Parallel.Invoke(() => { threadLifetimeCar3 = container.GetInstance<IVehicle>("ThreadLifetimeCar"); });
			Thread.Sleep(100);
			Assert.That(threadLifetimeCar3, Is.Not.Null);
			Assert.That(threadLifetimeCar3, Is.TypeOf<Car>());
			Assert.That(threadLifetimeCar3.Make, Is.Null);
			Assert.That(threadLifetimeCar3.Model, Is.Null);
			Assert.That(threadLifetimeCar3.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(threadLifetimeCar3.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			Assert.That(threadLifetimeCar, Is.Not.SameAs(threadLifetimeCar3));

			IVehicle threadLifetimePreCreatedCar = container.GetInstance<IVehicle>("ThreadLifetimePreCreatedCar");
			Assert.That(threadLifetimePreCreatedCar, Is.Not.Null);
			Assert.That(threadLifetimePreCreatedCar, Is.TypeOf<Car>());
			Assert.That(threadLifetimePreCreatedCar.Make, Is.Null);
			Assert.That(threadLifetimePreCreatedCar.Model, Is.Null);
			Assert.That(threadLifetimePreCreatedCar.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(threadLifetimePreCreatedCar.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			IVehicle threadLifetimePreCreatedCar2 = container.GetInstance<IVehicle>("ThreadLifetimePreCreatedCar");
			Assert.That(threadLifetimePreCreatedCar2, Is.Not.Null);
			Assert.That(threadLifetimePreCreatedCar2, Is.TypeOf<Car>());
			Assert.That(threadLifetimePreCreatedCar2.Make, Is.Null);
			Assert.That(threadLifetimePreCreatedCar2.Model, Is.Null);
			Assert.That(threadLifetimePreCreatedCar2.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(threadLifetimePreCreatedCar2.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			Assert.That(threadLifetimePreCreatedCar, Is.SameAs(threadLifetimePreCreatedCar2));

			IVehicle threadLifetimePreCreatedCar3 = null;
			Parallel.Invoke(() => { threadLifetimePreCreatedCar3 = container.GetInstance<IVehicle>("ThreadLifetimePreCreatedCar"); });
			Thread.Sleep(100);
			Assert.That(threadLifetimePreCreatedCar3, Is.Not.Null);
			Assert.That(threadLifetimePreCreatedCar3, Is.TypeOf<Car>());
			Assert.That(threadLifetimePreCreatedCar3.Make, Is.Null);
			Assert.That(threadLifetimePreCreatedCar3.Model, Is.Null);
			Assert.That(threadLifetimePreCreatedCar3.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(threadLifetimePreCreatedCar3.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));

			Assert.That(threadLifetimePreCreatedCar, Is.Not.SameAs(threadLifetimePreCreatedCar3));

			IVehicle propertInjectionCar = container.GetInstance<IVehicle>("PropertInjectionCar");
			Assert.That(propertInjectionCar, Is.Not.Null);
			Assert.That(propertInjectionCar, Is.TypeOf<Car>());
			Assert.That(propertInjectionCar.Make, Is.EqualTo("Ford"));
			Assert.That(propertInjectionCar.Model, Is.Null);
			Assert.That(propertInjectionCar.ColorScheme.PrimaryColor.Name, Is.EqualTo("White"));
			Assert.That(propertInjectionCar.ColorScheme.SecondaryColor.Name, Is.EqualTo("White"));
		}
	}
}