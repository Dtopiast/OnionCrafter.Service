namespace OnionCrafter.Service.Exceptions
{

    /// <summary>
    /// Represents an exception that is thrown when a global configuration file is not found.
    /// </summary>
    [Serializable]
    public class GlobalConfigNotFoundException : Exception
    {
        /// <summary>
        /// Exception thrown when the global configuration file is not found.
        /// </summary>
        /// <returns>
        /// An instance of GlobalConfigNotFoundException.
        /// </returns>
        public GlobalConfigNotFoundException() { }
        /// <summary>
        /// Creates a new instance of the GlobalConfigNotFoundException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <returns>A new instance of the GlobalConfigNotFoundException class.</returns>
        public GlobalConfigNotFoundException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the GlobalConfigNotFoundException class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        /// <returns>A new instance of the GlobalConfigNotFoundException class.</returns>
        public GlobalConfigNotFoundException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalConfigNotFoundException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <returns>A new instance of the <see cref="GlobalConfigNotFoundException"/> class.</returns>
        protected GlobalConfigNotFoundException(
                 System.Runtime.Serialization.SerializationInfo info,
                 System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
