using Microsoft.Extensions.DependencyInjection;
using OnionCrafter.Service.Options.Globals;
using OnionCrafter.Service.OptionsProviders;

namespace OnionCrafter.Service.DependencyInjection
{
    /// <summary>
    /// Extension methods for the OptionsProvider.
    /// </summary>
    public static class OptionsProviderExtensions
    {
        /// <summary>
        /// Indicates whether the options provider is set up.
        /// </summary>
        public static bool isSetupTheOptionsProvider;

        /// <summary>
        /// Adds the options provider to the service collection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the options provider to.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddOptionsProvider(this IServiceCollection services)
        {
            services.AddOptionsProvider<OptionsProvider<GlobalOptions>, GlobalOptions>();
            return services;
        }

        /// <summary>
        /// Adds the options provider with a specific type of global options to the service collection.
        /// </summary>
        /// <typeparam name="TGlobalOptions">The type of global options.</typeparam>
        /// <param name="services">The IServiceCollection to add the options provider to.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddOptionsProvider<TGlobalOptions>(this IServiceCollection services)
            where TGlobalOptions : class, IBaseGlobalOptions
        {
            services.AddOptionsProvider<OptionsProvider<TGlobalOptions>, TGlobalOptions>();
            return services;
        }

        /// <summary>
        /// Adds the options provider with the specified types of options provider and global options to the service collection.
        /// </summary>
        /// <typeparam name="TOptionsProvider">The type of options provider.</typeparam>
        /// <typeparam name="TGlobalOptions">The type of global options.</typeparam>
        /// <param name="services">The IServiceCollection to add the options provider to.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddOptionsProvider<TOptionsProvider, TGlobalOptions>(this IServiceCollection services)
            where TGlobalOptions : class, IBaseGlobalOptions
            where TOptionsProvider : class, IOptionsProvider<TGlobalOptions>
        {
            var existingDescriptor = services.SingleOrDefault(descriptor =>
              descriptor.ServiceType == typeof(IOptionsProvider<>));
            if (existingDescriptor != null)
            {
                services.Remove(existingDescriptor);
            }
            services.AddTypedSingleton<TOptionsProvider, TOptionsProvider>();
            isSetupTheOptionsProvider = true;
            return services;
        }
    }
}