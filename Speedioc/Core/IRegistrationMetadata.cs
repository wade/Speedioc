using Speedioc.Registration.Core;

namespace Speedioc.Core
{
	/// <summary>
	/// Implemented by classes that provide extended registration data that is used during container registration.
	/// </summary>
	public interface IRegistrationMetadata
	{
		/// <summary>
		/// Gets the name of the create instance method.
		/// </summary>
		/// <value>
		/// The name of the create instance method.
		/// </value>
		string CreateInstanceMethodName { get; }

		/// <summary>
		/// Gets the name of the thread-local create instance method.
		/// </summary>
		/// <value>
		/// The name of the thread-local create instance method.
		/// </value>
		string CreateThreadLocalInstanceMethodName { get; }

		/// <summary>
		/// Gets or sets the index of the registration in the aggregated registration collection.
		/// </summary>
		/// <value>
		/// The index.
		/// </value>
		int Index { get; set; }

		/// <summary>
		/// Gets or sets the a unique key that identifies the registration.
		/// </summary>
		/// <value>
		/// The registration key.
		/// </value>
		string RegistrationKey { get; set; }

		/// <summary>
		/// Gets the name of the operation method.
		/// </summary>
		/// <value>
		/// The name of the operation method.
		/// </value>
		string OperationMethodName { get; }

		/// <summary>
		/// Gets or sets the registration.
		/// </summary>
		/// <value>
		/// The registration.
		/// </value>
		IRegistration Registration { get; set; }

		/// <summary>
		/// Gets or sets the type of the registration.
		/// </summary>
		/// <value>
		/// The type of the registration.
		/// </value>
		RegistrationType RegistrationType { get; set; }
	}
}