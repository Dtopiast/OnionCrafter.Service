using OnionCrafter.Base.Commons;
using OnionCrafter.Service.Options.ServiceContainers.Logging;
using OnionCrafter.Service.Utils;

namespace OnionCrafter.Service.Options.ServiceContainers
{
    /// <summary>
    /// Provides options for configuring a service container, including whether to use a logger and logger options.
    /// </summary>
    /// <typeparam name="TLoggerOptions">The type of logger options to use.</typeparam>
    public interface IServiceContainerOptions<TLoggerOptions> :
        IBaseServiceContainerOptions,
        IUseLogger,
        ISetName
        where TLoggerOptions : class, IServiceContainerLoggerOptions
    {
        /// <summary>
        /// Gets or sets the service container name.
        /// </summary>
        public new string? SetName { get; set; }

        /// <summary>
        /// Gets or sets the logger options to use.
        /// </summary>
        public TLoggerOptions LoggerOptions { get; set; }
    }
}