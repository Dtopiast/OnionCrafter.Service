using Microsoft.Extensions.DependencyInjection;
using OnionCrafter.Service.Exceptions;
using OnionCrafter.Service.Options.Globals;
using OnionCrafter.Service.Options.Services;

namespace OnionCrafter.Service.DependencyInjection
{
    /// <summary>
    /// Extension methods for the GlobalOptions class.
    /// </summary>
    public static class GlobalOptionsExtensions
    {
        /// <summary>
        /// Flag to indicate if the global service configuration has been setup.
        /// </summary>
        public static bool isSetupGlobalServiceConfiguration;

        /// <summary>
        /// Checks the status of the Global Service Configuration.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection CheckStatusGlobalServiceConfiguration(this IServiceCollection services)
        {
            if (!isSetupGlobalServiceConfiguration)
            {
                bool isOptionRegistered = services.Any(ds => typeof(IBaseGlobalOptions).IsAssignableFrom(ds.ServiceType));
                if (isOptionRegistered)
                    throw new InvalidOperationException("Global service configuration fue creado en un contexto inseguro, favor de usar el metodo SetGlobalServiceConfiguration");
            }
            else
                throw new InvalidOperationException("One global service configuration has already been set up.");

            return services;
        }

        /// <summary>
        /// Sets up the global service configuration with the specified configuration name.
        /// </summary>
        /// <typeparam name="TGlobalServiceConfiguration">The type of the global service configuration.</typeparam>
        /// <param name="services">The service collection.</param>
        /// <param name="globalServiceConfiguration">The global service configuration.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection SetGlobalServiceConfiguration<TGlobalServiceConfiguration>(this IServiceCollection services, Action<TGlobalServiceConfiguration> globalServiceConfiguration)
                    where TGlobalServiceConfiguration : class, IBaseGlobalOptions
        {
            services.CheckStatusGlobalServiceConfiguration();
            services.AddTypedOptions(globalServiceConfiguration);
            isSetupGlobalServiceConfiguration = true;

            return services;
        }

        /// <summary>
        /// Configures the global service options.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection SetGlobalServiceConfiguration(this IServiceCollection services)
        {
            var Options = new GlobalOptions();
            var globalServiceConfiguration = (GlobalOptions options) =>
            {
                options.UseLogger = true;
            };
            services.SetGlobalServiceConfiguration(globalServiceConfiguration);
            return services;
        }

        /// <summary>
        /// Sets the global service configuration.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="globalServiceConfiguration">The global service configuration.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection SetGlobalServiceConfiguration(this IServiceCollection services, Action<GlobalOptions> globalServiceConfiguration)
        {
            services.SetGlobalServiceConfiguration(globalServiceConfiguration);
            return services;
        }

        /// <summary>
        /// Checks if the global options implementation is valid and returns it.
        /// </summary>
        /// <param name="globalOptions">The global options to check.</param>
        /// <returns>The valid global options.</returns>
        public static TOptions CheckGlobalOptionsImplementation<TOptions>(this TOptions? globalOptions)
                   where TOptions : IBaseServiceOptions

        {
            return globalOptions.ThrowIfNull<TOptions, ServiceConfigNotFoundException>();
        }
    }
}