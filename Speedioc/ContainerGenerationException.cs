﻿using System;
using System.Runtime.Serialization;

namespace Speedioc
{
	/// <summary>
	/// Thrown for container generation-specific exceptions.
	/// </summary>
	[Serializable]
	public class ContainerGenerationException : SpeediocException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerGenerationException"/> class without a message.
		/// </summary>
		public ContainerGenerationException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerGenerationException"/> class without a message.
		/// </summary>
		/// <param name="message">
		/// A human-readable description of the error. 
		/// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
		/// </param>
		public ContainerGenerationException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerGenerationException"/> class without a message.
		/// </summary>
		/// <param name="message">A human-readable description of the error.
		/// The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		public ContainerGenerationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerGenerationException" /> class with the specified formatted error message.
		/// </summary>
		/// <param name="message">
		/// A human-readable description of the error. 
		/// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
		/// </param>
		/// <param name="arg0">The object to write using the error message format.</param>
		public ContainerGenerationException(string message, object arg0)
			: base(string.Format(message, arg0))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerGenerationException" /> class with the specified formatted error message.
		/// </summary>
		/// <param name="message">
		/// A human-readable description of the error. 
		/// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
		/// </param>
		/// <param name="arg0">The first object to write using the error message format.</param>
		/// <param name="arg1">The second object to write using the error message format.</param>
		public ContainerGenerationException(string message, object arg0, object arg1)
			: base(string.Format(message, arg0, arg1))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerGenerationException" /> class with the specified formatted error message.
		/// </summary>
		/// <param name="message">
		/// A human-readable description of the error. 
		/// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
		/// </param>
		/// <param name="arg0">The first object to write using the error message format.</param>
		/// <param name="arg1">The second object to write using the error message format.</param>
		/// <param name="arg2">The third object to write using the error message format.</param>
		public ContainerGenerationException(string message, object arg0, object arg1, object arg2)
			: base(string.Format(message, arg0, arg1, arg2))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerGenerationException" /> class with the specified formatted error message.
		/// </summary>
		/// <param name="message">
		/// A human-readable description of the error. 
		/// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
		/// </param>
		/// <param name="args">An array of objects to write using the error message format.</param>
		public ContainerGenerationException(string message, params object[] args)
			: base(string.Format(message, args))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerGenerationException" /> class with the specified error message.
		/// </summary>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		/// <param name="message">
		/// A human-readable description of the error. 
		/// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
		/// </param>
		public ContainerGenerationException(Exception innerException, string message)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerGenerationException" /> class with the specified formatted error message.
		/// </summary>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		/// <param name="message">
		/// A human-readable description of the error. 
		/// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
		/// </param>
		/// <param name="arg0">The object to write using the error message format.</param>
		public ContainerGenerationException(Exception innerException, string message, object arg0)
			: base(string.Format(message, arg0), innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerGenerationException" /> class with the specified formatted error message.
		/// </summary>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		/// <param name="message">
		/// A human-readable description of the error. 
		/// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
		/// </param>
		/// <param name="arg0">The first object to write using the error message format.</param>
		/// <param name="arg1">The second object to write using the error message format.</param>
		public ContainerGenerationException(Exception innerException, string message, object arg0, object arg1)
			: base(string.Format(message, arg0, arg1), innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerGenerationException" /> class with the specified formatted error message.
		/// </summary>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		/// <param name="message">
		/// A human-readable description of the error. 
		/// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
		/// </param>
		/// <param name="arg0">The first object to write using the error message format.</param>
		/// <param name="arg1">The second object to write using the error message format.</param>
		/// <param name="arg2">The third object to write using the error message format.</param>
		public ContainerGenerationException(Exception innerException, string message, object arg0, object arg1, object arg2)
			: base(string.Format(message, arg0, arg1, arg2), innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerGenerationException" /> class with the specified formatted error message.
		/// </summary>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		/// <param name="message">
		/// A human-readable description of the error. 
		/// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
		/// </param>
		/// <param name="args">An array of objects to write using the error message format.</param>
		public ContainerGenerationException(Exception innerException, string message, params object[] args)
			: base(string.Format(message, args), innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerGenerationException" /> class with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo" /> instance that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext" /> instance that contains contextual information about the source or destination.</param>
		/// <remarks>
		/// This constructor overload is provided in order to adhere to custom exception design best practice guidelines.
		/// </remarks>
		protected ContainerGenerationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}