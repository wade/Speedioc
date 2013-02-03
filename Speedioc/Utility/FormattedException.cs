// FormattedException 1.0.0
// http://github.com/wade/FormattedException/
// http://nuget.org/packages/FormattedException/

using System.Runtime.Serialization;

// ReSharper disable CheckNamespace
namespace System
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// An extended version of the <see cref="System.Exception"/> class that provides additional convenience constructors with message formatting.
    /// </summary>
    /// <remarks>
    /// This abstrat class acts as a drop-in replacement for the <see cref="System.Exception"/> class for use as a base exception class.
    /// It provides additional convenience constructor overloads that incorporate message formatting capabilities. 
    /// This class should be used to create strongly-typed exception classes with the message formatting convenience constructor overloads. 
    /// This class is compatible with the <see cref="System.Exception"/> class and provides the same, default constructor overloads.
    /// </remarks>
    [Serializable]
    public abstract class FormattedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedException"/> class without a message.
        /// </summary>
        protected FormattedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedException"/> class without a message.
        /// </summary>
        /// <param name="message">
        /// A human-readable description of the error. 
        /// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
        /// </param>
        protected FormattedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedException"/> class without a message.
        /// </summary>
        /// <param name="message">A human-readable description of the error.
        /// The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        protected FormattedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedException" /> class with the specified formatted error message.
        /// </summary>
        /// <param name="message">
        /// A human-readable description of the error. 
        /// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
        /// </param>
        /// <param name="arg0">The object to write using the error message format.</param>
        protected FormattedException(string message, object arg0)
            : base(string.Format(message, arg0))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedException" /> class with the specified formatted error message.
        /// </summary>
        /// <param name="message">
        /// A human-readable description of the error. 
        /// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
        /// </param>
        /// <param name="arg0">The first object to write using the error message format.</param>
        /// <param name="arg1">The second object to write using the error message format.</param>
        protected FormattedException(string message, object arg0, object arg1)
            : base(string.Format(message, arg0, arg1))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedException" /> class with the specified formatted error message.
        /// </summary>
        /// <param name="message">
        /// A human-readable description of the error. 
        /// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
        /// </param>
        /// <param name="arg0">The first object to write using the error message format.</param>
        /// <param name="arg1">The second object to write using the error message format.</param>
        /// <param name="arg2">The third object to write using the error message format.</param>
        protected FormattedException(string message, object arg0, object arg1, object arg2)
            : base(string.Format(message, arg0, arg1, arg2))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedException" /> class with the specified formatted error message.
        /// </summary>
        /// <param name="message">
        /// A human-readable description of the error. 
        /// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
        /// </param>
        /// <param name="args">An array of objects to write using the error message format.</param>
        protected FormattedException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedException" /> class with the specified error message.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="message">
        /// A human-readable description of the error. 
        /// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
        /// </param>
        protected FormattedException(Exception innerException, string message)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedException" /> class with the specified formatted error message.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="message">
        /// A human-readable description of the error. 
        /// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
        /// </param>
        /// <param name="arg0">The object to write using the error message format.</param>
        protected FormattedException(Exception innerException, string message, object arg0)
            : base(string.Format(message, arg0), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedException" /> class with the specified formatted error message.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="message">
        /// A human-readable description of the error. 
        /// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
        /// </param>
        /// <param name="arg0">The first object to write using the error message format.</param>
        /// <param name="arg1">The second object to write using the error message format.</param>
        protected FormattedException(Exception innerException, string message, object arg0, object arg1)
            : base(string.Format(message, arg0, arg1), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedException" /> class with the specified formatted error message.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="message">
        /// A human-readable description of the error. 
        /// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
        /// </param>
        /// <param name="arg0">The first object to write using the error message format.</param>
        /// <param name="arg1">The second object to write using the error message format.</param>
        /// <param name="arg2">The third object to write using the error message format.</param>
        protected FormattedException(Exception innerException, string message, object arg0, object arg1, object arg2)
            : base(string.Format(message, arg0, arg1, arg2), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedException" /> class with the specified formatted error message.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="message">
        /// A human-readable description of the error. 
        /// The caller of this constructor is required to ensure that this string has been localized for the current system culture.
        /// </param>
        /// <param name="args">An array of objects to write using the error message format.</param>
        protected FormattedException(Exception innerException, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedException" /> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo" /> instance that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext" /> instance that contains contextual information about the source or destination.</param>
        /// <remarks>
        /// This constructor overload is provided in order to adhere to custom exception design best practice guidelines.
        /// </remarks>
        protected FormattedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}