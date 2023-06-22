using Microsoft.Extensions.Logging;
using OnionCrafter.Base.DataAccess;
using OnionCrafter.Base.Utils;
using OnionCrafter.Service.Options.Globals;
using OnionCrafter.Service.Options.ServiceContainers;
using OnionCrafter.Service.Options.ServiceContainers.Logging;
using OnionCrafter.Service.OptionsProviders;
using OnionCrafter.Service.Services;
using OnionCrafter.Service.Utils;

namespace OnionCrafter.Service.ServiceContainers
{
    /// <summary>
    /// Represents a base service container implementation.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys used to identify the services.</typeparam>
    /// <typeparam name="TValue">The type of the services.</typeparam>
    /// <typeparam name="TContainerOptions">The type of the options for the service container.</typeparam>
    /// <typeparam name="TContainerLoggerOptions">The type of the options for the service container logger.</typeparam>
    /// <typeparam name="TGlobalOptions">The type of the global options.</typeparam>
    public abstract class BaseServiceContainer<TKey, TValue, TGlobalOptions, TContainerOptions, TContainerLoggerOptions> : IServiceContainer<TKey, TValue, TGlobalOptions, TContainerOptions, TContainerLoggerOptions>
        where TContainerLoggerOptions : class, IServiceContainerLoggerOptions
        where TGlobalOptions : class, IGlobalOptions
        where TContainerOptions : class, IServiceContainerOptions<TContainerLoggerOptions>
        where TKey : notnull, IEquatable<TKey>, IComparable<TKey>
        where TValue : IBaseService
    {
        /// <summary>
        /// Logger, if not implemented is null
        /// </summary>
        protected ILogger? _logger;

        /// <summary>
        /// Dictionary where the services are stored with their key and value
        /// </summary>
        protected Dictionary<TKey, TValue> _services;

        /// <summary>
        /// Field to store the global service options.
        /// </summary>
        protected readonly TGlobalOptions _globalServiceOptions;

        /// <summary>
        /// Flag indicating whether to use a logger or not.
        /// </summary>
        protected bool _useLogger;

        /// <summary>
        ///Field with the configuration options for the service container.
        /// </summary>
        private readonly TContainerOptions _config;

        /// <summary>
        /// Initializes a new instance of the BaseServiceContainer class with the specified logger and container configuration.
        /// </summary>
        /// <param name="logger">The logger to use for logging.</param>
        /// <param name="optionsProvider">The options provider for the options</param>
        protected BaseServiceContainer(ILogger? logger, IOptionsProvider<TGlobalOptions> optionsProvider)
        {
            _globalServiceOptions = optionsProvider.GetGlobalServiceOptions();
            _useLogger = _globalServiceOptions.UseLogger;
            _config = optionsProvider.GetServiceOptions<TContainerOptions>(Name).CheckServiceOptionsImplementation();
            Name = _config.SetName ?? GetType().Name;
            _useLogger = _config.UseLogger;
            _logger = logger.CheckLoggerImplementation(_config.UseLogger);
            _services = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Gets the level of data access privileges in the service container.
        /// </summary>
        public DataAccesssPrivilegesLevel DataAccesssPrivilegesLevel => DataAccesssPrivilegesLevel.Complete;

        /// <summary>
        /// Gets the data context origin
        /// </summary>
        public DataContextOrigin DataContextOrigin => DataContextOrigin.Memory;

        /// <summary>
        /// Gets the name of the service container
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Adds a service to the container.
        /// </summary>
        /// <param name="key">The key to identify the service.</param>
        /// <param name="value">The service to add.</param>
        /// <returns>A task representing the asynchronous operation that returns true if the service was added, false otherwise.</returns>
        public virtual async Task<bool> AddServiceAsync(TKey key, TValue value)
        {
            var actionResult = !await AnyServiceAsync(key) && _services.TryAdd(key, value);
            LogAction(actionResult, ServiceContainerActionType.Add);
            return await Task.FromResult(actionResult);
        }

        /// <summary>
        /// Determines if a service with the specified key is present in the container.
        /// </summary>
        /// <param name="key">The key to check for.</param>
        /// <returns>A task representing the asynchronous operation that returns true if the service is present, false otherwise.</returns>

        public virtual async Task<bool> AnyServiceAsync(TKey key)
        {
            var actionResult = _services.ContainsKey(key);
            LogAction(actionResult, ServiceContainerActionType.Any);
            return await Task.FromResult(actionResult);
        }

        /// <summary>
        /// Determines if a service is present in the container.
        /// </summary>
        /// <param name="value">The service to check for.</param>
        /// <returns>A task representing the asynchronous operation that returns true if the service is present, false otherwise.</returns>

        public virtual async Task<bool> AnyServiceAsync(TValue value)
        {
            var actionResult = _services.ContainsValue(value);
            LogAction(actionResult, ServiceContainerActionType.Any);
            return await Task.FromResult(actionResult);
        }

        /// <summary>
        /// Gets the count of services in the container.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns the number of services.</returns>
        public virtual async Task<int> CountAsync()
        {
            return await Task.FromResult(_services.Count);
        }

        /// <summary>
        /// Asynchronously releases the resources used by the current instance of the <see cref="ValueTask"/> class.
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            await Task.Run(() =>
            {
                GC.SuppressFinalize(this);
            });
        }

        /// <summary>
        /// Gets a service from the container by its key.
        /// </summary>
        /// <param name="key">The key to identify the service.</param>
        /// <returns>A task representing the asynchronous operation that returns the service if it was found, or null if not.</returns>

        public virtual async Task<TValue?> GetServiceAsync(TKey key)
        {
            bool actionResult = false;
            TValue? result = default;
            if (await AnyServiceAsync(key))
            {
                result = _services.GetValueOrDefault(key);
                actionResult = result != null;
            }
            LogAction(actionResult, ServiceContainerActionType.Get);

            return await Task.FromResult(result);
        }

        /// <summary>
        /// Logs an action performed on the service container.
        /// </summary>
        /// <param name="actionResult">The actionResult of the action.</param>
        /// <param name="serviceContainerActionType">The action performed.</param>
        /// <param name="args">Additional arguments for the action.</param>

        public void LogAction(bool actionResult, ServiceContainerActionType serviceContainerActionType, params object?[] args)
        {
            if (!_config.UseLogger)
                return;

            var logAction = serviceContainerActionType switch
            {
                ServiceContainerActionType.Add => _config.LoggerOptions.AddServiceLoggingOptions,
                ServiceContainerActionType.Remove => _config.LoggerOptions.RemoveServiceLoggingOptions,
                ServiceContainerActionType.Any => _config.LoggerOptions.AnyServiceLoggingOptions,
                ServiceContainerActionType.Get => _config.LoggerOptions.GetServiceLoggingOptions,
                _ => null
            };

            logAction.ThrowIfNull();

            LogLevel logLevel = actionResult ? logAction.SuccessLogLevel : logAction.FailureLogLevel;
            string message = actionResult ? logAction.SuccessMessage : logAction.FailureMessage;

            if (!string.IsNullOrEmpty(_config.LoggerOptions.ReplaceZoneStringWithServiceName))
            {
                message = message.Replace(_config.LoggerOptions.ReplaceZoneStringWithServiceName, Name);
            }

            _logger?.Log(logLevel, message, args);
        }

        /// <summary>
        /// Removes a service from the container by its key.
        /// </summary>
        /// <param name="key">The key to identify the service.</param>
        /// <returns>A task representing the asynchronous operation that returns true if the service was removed, false otherwise.</returns>

        public virtual async Task<bool> RemoveServiceAsync(TKey key)
        {
            bool actionResult = false;
            if (!await AnyServiceAsync(key))
            {
                actionResult = _services.Remove(key);
            }
            LogAction(actionResult, ServiceContainerActionType.Remove);
            return await Task.FromResult(actionResult);
        }
    }
}