using System;
using System.Collections.Generic;
using Speedioc.Core;
using Speedioc.Registration.Builders;
using Speedioc.Registration.Core;

namespace Speedioc.Registration
{
	/// <summary>
	/// Registers types using the fluent registration API.
	/// </summary>
	/// <remarks>
	///		The <see cref="IRegistrar"/> interface is used at type registration time 
	///		usually with an implementation of <see cref="IRegistry"/>. 
	///		The <see cref="IRegistrationData"/> interface is used during the application 
	///		of the registration data during the configuration of a DrillBit.
	/// </remarks>
	/// <seealso cref="IRegistrar"/>
	/// <seealso cref="IRegistrationData"/>
	/// <seealso cref="IRegistry"/>
	public class Registrar : IRegistrar, IRegistrationData
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Registrar" /> class.
		/// </summary>
		public Registrar()
		{
			Registrations = new List<IRegistration>();
		}

		/// <summary>
		/// Gets the collection of registration objects.
		/// </summary>
		/// <value>
		/// The collection of registration objects.
		/// </value>
		/// <remarks>
		/// The collection of registration objects that were built by this <see cref="Registrar"/> instance.
		/// </remarks>
		public List<IRegistration> Registrations { get; private set; }

		/// <summary>
		/// Gets the collection of registration objects.
		/// </summary>
		/// <value>
		/// The collection of registration objects.
		/// </value>
		/// <remarks>
		/// The collection of registration objects that were built by an
		/// implementation of the <see cref="IRegistrar" /> interface.
		/// </remarks>
		IList<IRegistration> IRegistrationData.Registrations
		{
			get { return Registrations; }
		}

		/// <summary>
		/// Registers the specified type.
		/// </summary>
		/// <param name="type">The concrete type to be registered.</param>
		/// <returns>
		/// An instance of <see cref="IRegistrationBuilder" /> that is
		/// used to fluently build the complete type registration.
		/// </returns>
		public IRegistrationBuilder Register(Type type)
		{
			IRegistrationBuilder registrationBuilder = new RegistrationBuilder(type, Operation.GetInstance);
			Registrations.Add((IRegistration)registrationBuilder);
			return registrationBuilder;
		}

		/// <summary>
		/// Registers the specified type.
		/// </summary>
		/// <typeparam name="T">The concrete type to be registered.</typeparam>
		/// <returns>
		/// An instance of <see cref="IRegistrationBuilder" /> that is
		/// used to fluently build the complete type registration.
		/// </returns>
		public IRegistrationBuilder Register<T>()
		{
			return Register(typeof (T));
		}
	}
}