using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OnionCrafter.Service.Options.Globals;
using OnionCrafter.Service.Options.Services;
using OnionCrafter.Service.Services;

namespace OnionCrafter.Service.OptionsProviders
{
    /// <summary>
    /// Represents a generic global service with options of type TGlobalServiceOptions.
    /// </summary>
    public class OptionsProvider<TGlobalServiceOptions> : IOptionsProvider<TGlobalServiceOptions>
        where TGlobalServiceOptions : class, IBaseGlobalOptions
    {
        /// <summary>
        /// Field to store a reference to the service provider.
        /// </summary>
        protected readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Constructor for OptionProvider class which takes an IServiceProvider as a parameter.
        /// </summary>
        public OptionsProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets the global service options.
        /// </summary>
        /// <returns>The global service options.</returns>

        public TReturn Clone<TReturn>() where TReturn : IBaseService
        {
            return (TReturn)MemberwiseClone();
        }

        /// <inheritdoc/>
        public TGlobalServiceOptions GetGlobalServiceOptions()
        {
            var serviceOptions = _serviceProvider.GetRequiredService<IOptions<TGlobalServiceOptions>>().Value;
            return serviceOptions;
        }

        /// <inheritdoc/>

        public TServiceOptions? GetServiceOptions<TServiceOptions>() where TServiceOptions : class, IBaseServiceOptions
        {
            var serviceOptions = _serviceProvider.GetRequiredService<IOptionsSnapshot<TServiceOptions>>().Value;
            return serviceOptions;
        }

        /// <inheritdoc/>
        public TServiceOptions? GetServiceOptions<TServiceOptions>(string serviceName) where TServiceOptions : class, IBaseServiceOptions
        {
            var serviceOptions = _serviceProvider.GetRequiredService<IOptionsMonitor<TServiceOptions>>().Get(serviceName);
            return serviceOptions;
        }
    }
}