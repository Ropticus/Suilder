using System;

namespace Suilder.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a functionality is not supported.
    /// </summary>
    public class ClauseNotSupportedException : CompileException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClauseNotSupportedException"/> class.
        /// </summary>
        public ClauseNotSupportedException() : base("The clause is not supported in this engine.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClauseNotSupportedException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ClauseNotSupportedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClauseNotSupportedException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner
        /// exception is specified.</param>
        public ClauseNotSupportedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}