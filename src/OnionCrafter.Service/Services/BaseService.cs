using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnionCrafter.Service.Exceptions;
using OnionCrafter.Service.Services.Options;
using OnionCrafter.Service.Utils;

namespace OnionCrafter.Service.Services
{
    /// <summary>
    ///Abstract implementation of the <see cref="IService"/> with default general settings.
    /// </summary>
    public abstract class BaseService : BaseService<GlobalServiceOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{TOptions}"/> class with the provided options monitor and logger.
        /// </summary>
        /// <param name="optionsMonitor">The options monitor to retrieve the global service options.</param>
        /// <param name="logger">The logger to log service events and messages.</param>
        protected BaseService(IOptionsMonitor<GlobalServiceOptions> optionsMonitor, ILogger<BaseService<GlobalServiceOptions>>? logger) : base(optionsMonitor, logger)
        {
        }
    }

    /// <summary>
    ///Abstract implementation of the <see cref="IService"/>.
    /// </summary>
    public abstract class BaseService<TGlobalServiceOptions> : IService
        where TGlobalServiceOptions : class, IGlobalServiceOptions
    {
        /// <summary>
        /// Field to store a logger instance.
        /// </summary>
        protected readonly ILogger? _logger;

        /// <summary>
        /// Flag indicating whether to use a logger or not.
        /// </summary>
        protected bool _useLogger;

        /// <summary>
        /// Field to store the global service options.
        /// </summary>
        protected TGlobalServiceOptions _globalServiceOptions;

        /// <summary>
        /// Constructor for BaseService class which takes IOptionsMonitor and ILogger as parameters.
        /// </summary>
        protected BaseService(IOptionsMonitor<TGlobalServiceOptions> optionsMonitor, ILogger<BaseService<TGlobalServiceOptions>>? logger)
        {
            _logger = logger;
            _globalServiceOptions = optionsMonitor.Get(Environment.GetEnvironmentVariable("GlobalServiceConfiguration"));
            _useLogger = _globalServiceOptions.UseLogger;
            Name = GetType().Name;
        }

        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Creates a shallow copy of the service.
        /// </summary>
        /// <returns>A cloned instance of the service.</returns>
        public abstract T Clone<T>()
            where T : IService;

        /// <summary>
        /// Disposes the service asynchronously.
        /// </summary>
        public abstract ValueTask DisposeAsync();

        /// <summary>
        /// Logs an action with the specified result, action type, and arguments.
        /// </summary>
        /// <typeparam name="TServiceAction">The type of the action.</typeparam>
        /// <param name="actionResult">The result of the action.</param>
        /// <param name="action">The action type.</param>
        /// <param name="args">The arguments for the action.</param>
        /// <remarks>
        /// This method logs the specified action with its result and additional arguments.
        /// The action type must be an enum.
        /// </remarks>
        public abstract void LogAction<TServiceAction>(bool actionResult, TServiceAction action, params object?[] args)
            where TServiceAction : struct, Enum;
    }

    /// <summary>
    /// Represents a base service with support for global service options and service-specific options.
    /// Inherits from <see cref="BaseService{TGlobalServiceOptions}"/> and implements <see cref="IService{TServiceOptions}"/>.
    /// </summary>
    /// <typeparam name="TGlobalServiceOptions">The type of global service options.</typeparam>
    /// <typeparam name="TServiceOptions">The type of service-specific options.</typeparam>
    public abstract class BaseService<TGlobalServiceOptions, TServiceOptions> : BaseService<TGlobalServiceOptions>, IService<TServiceOptions>
            where TGlobalServiceOptions : class, IGlobalServiceOptions
            where TServiceOptions : class, IServiceOptions
    {
        /// <summary>
        /// Constructor for BaseService class.
        /// </summary>
        /// <param name="optionsMonitor">Options monitor for global service options.</param>
        /// <param name="logger">Logger for the service.</param>
        /// <param name="config">Options for the service.</param>
        /// <returns>
        /// An instance of the BaseService class.
        /// </returns>
        protected BaseService(IOptionsMonitor<TGlobalServiceOptions> optionsMonitor, ILogger<BaseService<TGlobalServiceOptions>>? logger, IOptionsMonitor<TServiceOptions>? config) : base(optionsMonitor, logger)
        {
            if (config == null)
                throw new ServiceConfigNotFoundException(Name);
            Config = config.Get(Name);
            _useLogger = Config.UseLogger;
            Name = Config.SetServiceName ?? Name;
            _logger.CheckLoggerImplementation(_useLogger);
        }

        /// <summary>
        /// Gets the service options.
        /// </summary>
        public TServiceOptions Config { get; }
    }
}