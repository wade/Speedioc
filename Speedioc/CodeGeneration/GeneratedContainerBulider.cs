using System;
using System.Collections.Generic;
using Speedioc.Core;
using Speedioc.Registration;
using Speedioc.Registration.Core;

namespace Speedioc.CodeGeneration
{
	/// <summary>
	/// Builds a container that is created from dynamically generated and compiled source code.
	/// </summary>
	public class GeneratedContainerBulider : IContainerBuilder
	{
		private readonly IContainerGenerator _containerGenerator;
		private readonly IContainerSettings _containerSettings;
		private readonly IRegistrationAggregator _registrationAggregator;
		private readonly List<IRegistry> _registries;

		/// <summary>
		/// Initializes a new instance of the <see cref="GeneratedContainerBulider" /> class.
		/// </summary>
		/// <param name="containerGenerator">The container generator.</param>
		/// <param name="registrationAggregator">The registration aggregator.</param>
		/// <param name="containerSettings">The container settings.</param>
		public GeneratedContainerBulider(IContainerGenerator containerGenerator, IContainerSettings containerSettings = null, IRegistrationAggregator registrationAggregator = null)
			: this(containerGenerator, containerSettings, registrationAggregator, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GeneratedContainerBulider" /> class.
		/// </summary>
		/// <param name="containerGenerator">The container generator.</param>
		/// <param name="registrationAggregator">The registration aggregator.</param>
		/// <param name="containerSettings">The container settings.</param>
		/// <param name="registries">The registries.</param>
		/// <exception cref="System.ArgumentNullException">containerGenerator</exception>
		public GeneratedContainerBulider(IContainerGenerator containerGenerator, IContainerSettings containerSettings, IRegistrationAggregator registrationAggregator, params IRegistry[] registries)
		{
			if (null == containerGenerator)
			{
				throw new ArgumentNullException("containerGenerator");
			}

			_containerGenerator = containerGenerator;
			_containerSettings = containerSettings ?? new DefaultContainerSettings();
			_registrationAggregator = registrationAggregator ?? new RegistrationAggregator();
			_registries = new List<IRegistry>();
			
			if (null != registries && registries.Length > 0)
			{
				_registries.AddRange(registries);
			}
		}

		/// <summary>
		/// Adds the registry to the builder.
		/// </summary>
		/// <param name="registry">The registry to be added to the builder.</param>
		/// <remarks>
		/// Registry instances must be applied to a new container in the order that they are added for deterministic behavior.
		/// </remarks>
		public void AddRegistry(IRegistry registry)
		{
			if (null == registry)
			{
				throw new ArgumentNullException("registry");
			}

			_registries.Add(registry);
		}

		/// <summary>
		/// Builds a new container instance.
		/// </summary>
		/// <returns>A new container instance.</returns>
		public IContainer Build()
		{
			if (_registries.Count < 1)
			{
				throw new InvalidOperationException(
					"No registry instances have been added to the builder. " + 
					"The container cannot be built without at least one registry instance. " + 
					"Please call the AddRegistry method to add registry instances to the builder."
					);
			}

			IList<IRegistration> registrations = _registrationAggregator.AggregateRegistrations(_registries);

			return _containerGenerator.GenerateContainer(registrations, _containerSettings);
		}
	}
}