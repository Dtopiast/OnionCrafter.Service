using OnionCrafter.Service.Logging;

namespace OnionCrafter.Service.Options.ServiceContainers.Logging
{
    /// <summary>
    /// Interface for implementing options to configure the logging behavior of the service container.
    /// </summary>
    public interface IServiceContainerLoggerOptions : IBaseServiceContainerLoggerOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include the service name in log messages.
        /// </summary>
        bool IncludeServiceNameInMessages { get; set; }

        /// <summary>
        /// Gets or sets the string used to replace the zone placeholder in log messages with the service name.
        /// </summary>
        string ReplaceZoneStringWithServiceName { get; set; }

        /// <summary>
        /// Gets or sets the logging action to be performed when adding service logging options.
        /// </summary>
        LogAction AddServiceLoggingOptions { get; set; }

        /// <summary>
        /// Gets or sets the logging action to be performed when any service logging options are encountered.
        /// </summary>
        LogAction AnyServiceLoggingOptions { get; set; }

        /// <summary>
        /// Gets or sets the logging action to be performed when getting service logging options.
        /// </summary>
        LogAction GetServiceLoggingOptions { get; set; }

        /// <summary>
        /// Gets or sets the logging action to be performed when removing service logging options.
        /// </summary>
        LogAction RemoveServiceLoggingOptions { get; set; }
    }
}