namespace OnionCrafter.Service.Exceptions
{
    /// <summary>
    /// Exception thrown when service arguments are not supported.
    /// </summary>
    [Serializable]
    public class NotSupportedServiceArgumentsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotSupportedServiceArgumentsException"/> class.
        /// </summary>
        public NotSupportedServiceArgumentsException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotSupportedServiceArgumentsException"/> class with a specified error message.
        /// </summary>
        public NotSupportedServiceArgumentsException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of the NotSupportedServiceArgumentsException class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        public NotSupportedServiceArgumentsException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotSupportedServiceArgumentsException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <returns>
        /// A new instance of the <see cref="NotSupportedServiceArgumentsException"/> class.
        /// </returns>
        protected NotSupportedServiceArgumentsException(
                         System.Runtime.Serialization.SerializationInfo info,
                         System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}