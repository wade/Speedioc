using Speedioc.CodeGeneration;
using Speedioc.CodeGeneration.TemplateCodeGen;
using Speedioc.Core;
using Speedioc.Registration;

namespace Speedioc
{
	/// <summary>
	/// Convenience class that is used to obtain an instance of the default container builder.
	/// </summary>
	public static class DefaultContainerBuilderFactory
	{
		/// <summary>
		/// Gets an instance of the default container builder.
		/// </summary>
		/// <param name="containerSettings">The container settings.</param>
		/// <returns>An instance of <see cref="IContainerBuilder"/>.</returns>
		public static IContainerBuilder GetInstance(IContainerSettings containerSettings = null)
		{
			return GetInstance(containerSettings, null);
		}

		// ReSharper disable MethodOverloadWithOptionalParameter

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <param name="containerSettings">The container settings.</param>
		/// <param name="registries">The registries.</param>
		/// <returns></returns>
		public static IContainerBuilder GetInstance(IContainerSettings containerSettings, params IRegistry[] registries)
		{
			IContainerGenerator containerGenerator = new TemplateContainerGenerator();
			IRegistrationAggregator registrationAggregator = new RegistrationAggregator();
			return new GeneratedContainerBulider(containerGenerator, containerSettings, registrationAggregator, registries);
		}

		// ReSharper restore MethodOverloadWithOptionalParameter
	}
}