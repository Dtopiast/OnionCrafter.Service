using Microsoft.Extensions.Logging;
using OnionCrafter.Service.Logging;

namespace OnionCrafter.Service.Options.ServiceContainers.Logging
{
    /// <summary>
    ///Implementation of the <see cref="IServiceContainerLoggerOptions"/>.
    ///</summary>
    public class ServiceContainerLoggerOptions : IServiceContainerLoggerOptions
    {
        /// <summary>
        /// Default constructor for ServiceContainerLoggerOptions class.
        /// </summary>
        public ServiceContainerLoggerOptions()
        {
            IncludeServiceNameInMessages = true;
            ReplaceZoneStringWithServiceName = "{ServiceName}";
            RemoveServiceLoggingOptions = new LogAction()
            {
                FailureLogLevel = LogLevel.Error,
                FailureMessage = "{ServiceName} wasn't remove",
                SuccessLogLevel = LogLevel.Warning,
                SuccessMessage = "{ServiceName} was remove"
            };

            GetServiceLoggingOptions = new LogAction()
            {
                FailureLogLevel = LogLevel.Error,
                FailureMessage = "{ServiceName} wasn't obtain",
                SuccessLogLevel = LogLevel.Information,
                SuccessMessage = "{ServiceName} was obtain"
            };

            AddServiceLoggingOptions = new LogAction()
            {
                SuccessMessage = "{ServiceName} was create",
                FailureLogLevel = LogLevel.Error,
                FailureMessage = "{ServiceName} wasn't create",
                SuccessLogLevel = LogLevel.Information,
            };
            AnyServiceLoggingOptions = new LogAction()
            {
                SuccessMessage = "{ServiceName} is registered",
                FailureLogLevel = LogLevel.Error,
                FailureMessage = "{ServiceName} isn't registered",
                SuccessLogLevel = LogLevel.Information,
            };
        }

        /// <inheritdoc/>
        public LogAction AddServiceLoggingOptions { get; set; }

        /// <inheritdoc/>
        public LogAction AnyServiceLoggingOptions { get; set; }

        /// <inheritdoc/>
        public LogAction GetServiceLoggingOptions { get; set; }

        /// <inheritdoc/>
        public LogAction RemoveServiceLoggingOptions { get; set; }

        ///<summary>
        /// Gets or sets a value indicating whether to include the service name in log messages.
        /// </summary>
        public bool IncludeServiceNameInMessages { get; set; }

        /// <summary>
        /// Gets or sets the string used to replace the zone placeholder in log messages with the service name.
        /// </summary>
        public string ReplaceZoneStringWithServiceName { get; set; }
    }
}