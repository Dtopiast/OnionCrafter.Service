using Microsoft.Extensions.Logging;
using OnionCrafter.Service.Options.Globals;
using OnionCrafter.Service.Options.Services;
using OnionCrafter.Service.OptionsProviders;
using OnionCrafter.Service.Utils;

namespace OnionCrafter.Service.Services
{
    /// <summary>
    /// BaseService is an abstract class that implements the IService interface and provides a generic type parameter TGlobalServiceOptions which must be a class and implement IGlobalOptions.
    /// </summary>
    public abstract class BaseService<TGlobalOptions> : IService<TGlobalOptions>
         where TGlobalOptions : class, IGlobalOptions
    {
        /// <summary>
        /// Field to store a logger instance.
        /// </summary>
        protected ILogger<BaseService<TGlobalOptions>>? _logger;

        /// <summary>
        /// Field to store the global service options.
        /// </summary>
        protected readonly TGlobalOptions _globalServiceOptions;

        /// <summary>
        /// Flag indicating whether to use a logger or not.
        /// </summary>
        protected bool _useLogger;

        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Constructor for BaseService class which takes IOptionsProvider and ILogger as parameters.
        /// </summary>

        protected BaseService(IOptionsProvider<TGlobalOptions> optionsProvider, ILogger<BaseService<TGlobalOptions>>? logger)
        {
            _globalServiceOptions = optionsProvider.GetGlobalServiceOptions();
            Name = GetType().Name;
            _useLogger = _globalServiceOptions.UseLogger;
            _logger = logger.CheckLoggerImplementation(_useLogger);
        }

        /// <inheritdoc/>
        public virtual TReturn Clone<TReturn>() where TReturn : IBaseService
        {
            return (TReturn)MemberwiseClone();
        }
    }

    /// <summary>
    /// Represents a base service with support for global service options and service-specific options.
    /// </summary>
    /// <typeparam name="TGlobalOptions">The type of global service options.</typeparam>
    /// <typeparam name="TServiceOptions">The type of service-specific options.</typeparam>
    public abstract class BaseService<TGlobalOptions, TServiceOptions> : BaseService<TGlobalOptions>, IService<TGlobalOptions, TServiceOptions>
        where TGlobalOptions : class, IGlobalOptions
        where TServiceOptions : class, IServiceOptions
    {
        /// <summary>
        /// Stores the configuration for the service.
        /// </summary>
        protected readonly TServiceOptions _serviceConfig;

        /// <summary>
        /// Constructor for BaseService class.
        /// </summary>
        /// <param name="optionsProvider">Options provider for global and service options.</param>
        /// <param name="logger">Logger for logging.</param>
        /// <returns>
        /// An instance of BaseService class.
        /// </returns>
        protected BaseService(IOptionsProvider<TGlobalOptions> optionsProvider, ILogger<BaseService<TGlobalOptions, TServiceOptions>>? logger) : base(optionsProvider, logger)
        {
            _serviceConfig = optionsProvider.GetServiceOptions<TServiceOptions>(Name).CheckServiceOptionsImplementation();
            Name = _serviceConfig.SetName ?? Name;
            _useLogger = _serviceConfig.UseLogger;
            _logger = logger.CheckLoggerImplementation(_useLogger);
        }
    }
}