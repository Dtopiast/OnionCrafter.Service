using OnionCrafter.Service.Options.GlobalOptions;

namespace OnionCrafter.Service.Options.ServiceOptions
{
    /// <summary>
    /// Represents the options for a service that can be configured using a generic type.
    /// </summary>
    public class ServiceOptions<TGlobalOptions> : IServiceOptions
    {
        /// <inheritdoc/>
        public string? SetServiceName { get; set; }

        /// <inheritdoc/>

        public bool UseLogger { get; set; }
    }
    /// <summary>
    /// Represents the options for a service.
    /// </summary>
    public class ServiceOptions : ServiceOptions<GlobalOptions.GlobalOptions>
    {

    }
}