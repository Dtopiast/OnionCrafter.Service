namespace OnionCrafter.Service.Exceptions
{
    /// <summary>
    /// Exception thrown when a logger implementation is not implemented or configured properly.
    /// </summary>
    [Serializable]
    public class NotImplementedLoggerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotImplementedLoggerException"/> class.
        /// </summary>
        /// <param name="logger">The name of the logger that is not implemented or configured properly.</param>
        public NotImplementedLoggerException(string logger) : base(logger + " , if your project does not require logging set the UseLogger option to false in your implementation. ")
        { }

        /// <summary>
        /// Creates a new instance of LoggerNotImplementedException with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        public NotImplementedLoggerException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotImplementedLoggerException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <returns>A new instance of the <see cref="NotImplementedLoggerException"/> class.</returns>
        protected NotImplementedLoggerException(
                 System.Runtime.Serialization.SerializationInfo info,
                 System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}