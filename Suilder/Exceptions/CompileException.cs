using System;

namespace Suilder.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the query cannot be compiled.
    /// </summary>
    public class CompileException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompileException"/> class.
        /// </summary>
        public CompileException() : base("Cannot compile the query.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompileException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CompileException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompileException"/> class with a specified error message 
        /// and a reference to the inner exception that is the cause of this exception
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner 
        /// exception is specified.</param>
        public CompileException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}