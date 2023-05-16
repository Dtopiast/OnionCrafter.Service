namespace OnionCrafter.Service.Exceptions
{
    /// <summary>
    /// Exception thrown when a service configuration is not found.
    /// </summary>
    [Serializable]
    public class ServiceConfigNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceConfigNotFoundException"/> class.
        /// </summary>
        public ServiceConfigNotFoundException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceConfigNotFoundException"/> class with a specified error message.
        /// </summary>
        public ServiceConfigNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor for ServiceConfigNotFoundException with a message and inner exception.
        /// </summary>
        public ServiceConfigNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceConfigNotFoundException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <returns>
        /// A new instance of the <see cref="ServiceConfigNotFoundException"/> class.
        /// </returns>
        protected ServiceConfigNotFoundException(
                 System.Runtime.Serialization.SerializationInfo info,
                 System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}