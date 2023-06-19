using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OnionCrafter.Service.Options.GlobalOptions;
using OnionCrafter.Service.Options.ServiceOptions;

namespace OnionCrafter.Service.OptionsProviders
{
    /// <summary>
    /// Represents a generic global service with options of type TGlobalServiceOptions.
    /// </summary>
    public class OptionProvider<TGlobalServiceOptions> : IOptionsProvider<TGlobalServiceOptions>
        where TGlobalServiceOptions : class, IBaseGlobalOptions
    {
        /// <summary>
        /// Field to store a reference to the service provider.
        /// </summary>
        protected readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Constructor for OptionProvider class which takes an IServiceProvider as a parameter.
        /// </summary>
        public OptionProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets the global service options.
        /// </summary>
        /// <returns>The global service options.</returns>
        public TGlobalServiceOptions GetGlobalServiceOptions()
        {
            var serviceOptions = _serviceProvider.GetRequiredService<IOptions<TGlobalServiceOptions>>().Value;
            return serviceOptions;
        }

        /// <summary>
        /// Gets the service options of the specified type.
        /// </summary>
        /// <typeparam name="TServiceOptions">The type of service options to get.</typeparam>
        /// <returns>The service options of the specified type.</returns>
        public TServiceOptions? GetServiceOptions<TServiceOptions>() where TServiceOptions : class, IBaseServiceOptions
        {
            var serviceOptions = _serviceProvider.GetRequiredService<IOptions<TServiceOptions>>().Value;
            return serviceOptions;
        }

        /// <summary>
        /// Gets the service options of the specified type for the given service name.
        /// </summary>
        /// <typeparam name="TServiceOptions">The type of service options to get.</typeparam>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>The service options of the specified type for the given service name.</returns>
        public TServiceOptions? GetServiceOptions<TServiceOptions>(string serviceName) where TServiceOptions : class, IBaseServiceOptions
        {
            var serviceOptions = _serviceProvider.GetRequiredService<IOptionsMonitor<TServiceOptions>>().Get(serviceName);
            return serviceOptions;
        }
    }
}