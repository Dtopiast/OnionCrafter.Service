namespace OnionCrafter.Service.Options.Services
{
    /// <summary>
    /// Represents the options for a service that can be configured using a generic type.
    /// </summary>
    public class ServiceOptions : IServiceOptions
    {
        /// <inheritdoc/>
        public string? SetName { get; set; }

        /// <inheritdoc/>

        public bool UseLogger { get; set; }
    }
}