using System;
using Speedioc.Registration.Core;

namespace Speedioc.Core
{
	/// <summary>
	/// Extended registration data that is used during container registration.
	/// </summary>
	public class RegistrationMetadata : IRegistrationMetadata
	{
		private readonly Lazy<string> _createInstanceMethodNameLazy;
		private readonly Lazy<string> _createThreadLocalInstanceMethodNameLazy;
		private readonly Lazy<string> _operationMethodNameLazy;

		/// <summary>
		/// Initializes a new instance of the <see cref="RegistrationMetadata" /> class.
		/// </summary>
		/// <param name="methodNameGenerator">The method name generator.</param>
		public RegistrationMetadata(IMethodNameGenerator methodNameGenerator)
		{
			_createInstanceMethodNameLazy = new Lazy<string>(methodNameGenerator.GenerateCreateInstanceMethodName);
			_createThreadLocalInstanceMethodNameLazy = new Lazy<string>(methodNameGenerator.GenerateCreateThreadLocalInstanceMethodName);
			_operationMethodNameLazy = new Lazy<string>(methodNameGenerator.GenerateOperationMethodName);
		}

		/// <summary>
		/// Gets the name of the create instance method.
		/// </summary>
		/// <value>
		/// The name of the create instance method.
		/// </value>
		public string CreateInstanceMethodName { get { return _createInstanceMethodNameLazy.Value; } }

		/// <summary>
		/// Gets the name of the thread-local create instance method.
		/// </summary>
		/// <value>
		/// The name of the thread-local create instance method.
		/// </value>
		public string CreateThreadLocalInstanceMethodName { get { return _createThreadLocalInstanceMethodNameLazy.Value; } }

		/// <summary>
		/// Gets or sets the index of the registration in the aggregated registration collection.
		/// </summary>
		/// <value>
		/// The index.
		/// </value>
		public int Index { get; set; }

		/// <summary>
		/// Gets or sets the a unique key that identifies the registration.
		/// </summary>
		/// <value>
		/// The registration key.
		/// </value>
		public string RegistrationKey { get; set; }

		/// <summary>
		/// Gets the name of the operation method.
		/// </summary>
		/// <value>
		/// The name of the operation method.
		/// </value>
		public string OperationMethodName { get { return _operationMethodNameLazy.Value; } }

		/// <summary>
		/// Gets or sets the registration.
		/// </summary>
		/// <value>
		/// The registration.
		/// </value>
		public IRegistration Registration { get; set; }

		/// <summary>
		/// Gets or sets the type of the registration.
		/// </summary>
		/// <value>
		/// The type of the registration.
		/// </value>
		public RegistrationType RegistrationType { get; set; }
	}
}