namespace OnionCrafter.Service.Exceptions
{
    [Serializable]
    public class ImplementedOptionException : Exception
    {
        public ImplementedOptionException()
        { }

        public ImplementedOptionException(string message) : base(message)
        {
        }

        public ImplementedOptionException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ImplementedOptionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}