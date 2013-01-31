using Speedioc.Registration;
using Speedioc.TestDomain;

namespace Speedioc.CodeGeneration.TemplateCodeGen
{
	public class TestRegistry : Registry
	{
		public override void RegisterTypes()
		{
			Register<Car>();
			Register<Car>().As<IVehicle>();
			Register<Car>().As<IVehicle>().WithName("WhiteCar");
				
			Register<Car>()
				.As<IVehicle>()
				.WithName("AppDomainLifetimeCar")
				.WithLifetime(Lifetime.AppDomain)
				.PreCreateInstance();

			Register<Car>()
				.As<IVehicle>()
				.WithName("ContainerLifetimeCar")
				.WithLifetime(Lifetime.Container);

			Register<Car>()
				.As<IVehicle>()
				.WithName("ContainerLifetimePreCreatedCar")
				.WithLifetime(Lifetime.Container)
				.PreCreateInstance();

			Register<Car>()
				.As<IVehicle>()
				.WithName("ThreadLifetimeCar")
				.WithLifetime(Lifetime.Thread);

			Register<Car>()
				.As<IVehicle>()
				.WithName("ThreadLifetimePreCreatedCar")
				.WithLifetime(Lifetime.Thread)
				.PreCreateInstance();

			Register<Car>()
				.As<IVehicle>()
				.WithName("Viper")
				.UsingConstructor()
					.WithValueParameter("Dodge")
					.WithValueParameter("Viper")
					.WithValueFactoryParameter<IColorScheme>(
						() => new ColorScheme("ViperRedBlack", new RedColor(), new BlackColor())
					)
					.AsLastParameter();

			Register<Car>()
				.As<IVehicle>()
				.WithName("Viper2")
				.UsingConstructor()
					.WithValueParameter("Dodge")
					.WithValueParameter("Viper")
					.WithResolvedParameter<IColorScheme>("Viper Red And Black")
					.AsLastParameter();

			Register<ColorScheme>()
				.As<IColorScheme>().WithName("Viper Red And Black")
				.UsingConstructor()
					.WithValueParameter("ViperRedBlack")
					.WithValueFactoryParameter<IColor>(() => new RedColor())
					.WithValueFactoryParameter<IColor>(() => new BlackColor())
					.AsLastParameter();

			Register<ColorScheme>()
				.As<IColorScheme>().WithName("Mustang Red And Black")
				.UsingConstructor()
					.WithValueParameter("MustangRedBlack")
					.WithValueFactoryParameter<IColor>(() => new RedColor())
					.WithValueFactoryParameter<IColor>(() => new BlackColor())
					.AsLastParameter();

			Register<Car>()
				.As<IVehicle>()
				.WithName("PropertInjectionCar")
				.WithProperty<string>("Make")
					.SetTo("Ford");

			Register<Car>()
				.As<IVehicle>()
				.WithName("PropertInjectionCar2")
				.WithProperty<string>("Make").SetTo("Ford")
				.WithProperty<string>("Model").SetTo("Mustang")
				.WithProperty<IColorScheme>("ColorScheme").SetToResolvedValue("Mustang Red And Black");

			//// Intentionally invalid mapping: Car to IColor.
			//Register<Car>()
			//	.As<IColor>()
			//	.WithName("PropertInjectionCar")
			//	.WithProperty<string>("Make")
			//		.SetTo("Ford");
		}
	}
}