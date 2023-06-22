using Microsoft.Extensions.DependencyInjection;
using OnionCrafter.Base.Utils;
using OnionCrafter.Service.Options.ServiceContainers;
using OnionCrafter.Service.Options.ServiceContainers.Logging;
using OnionCrafter.Service.ServiceContainers;

namespace OnionCrafter.Service.DependencyInjection
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/> to register custom service containers.
    /// </summary>
    public static class ServiceContainerExtensionS
    {
        public static readonly Action<ServiceContainerOptions<ServiceContainerLoggerOptions>> DefaultServiceContainerOptions = (options) =>
        {
            options = new ServiceContainerOptions<ServiceContainerLoggerOptions>();
        };

        /// <summary>
        /// Adds a service container to the service collection with the specified options and logging options.
        /// </summary>
        /// <typeparam name="TOptions">The type of service container options.</typeparam>
        /// <typeparam name="TOptionsLogging">The type of service container logger options.</typeparam>
        /// <param name="services">The service collection.</param>
        /// <param name="serviceContainerType">The type of the service container.</param>
        /// <param name="implementationServiceContainerType">The type of the implementation of the service container.</param>
        /// <param name="configure">An action to configure the service container options.</param>
        /// <returns>The modified service collection.</returns>
        public static IServiceCollection AddServiceContainer<TOptions>(this IServiceCollection services, Type serviceContainerType, Type implementationServiceContainerType, Action<TOptions> configure)
            where TOptions : class, IBaseServiceContainerOptions
        {
            EnsureValidServiceContainerTypes(serviceContainerType, implementationServiceContainerType);
            services.AddTypedOptions(configure, implementationServiceContainerType.Name);
            services.AddSingleton(serviceContainerType, implementationServiceContainerType);
            return services;
        }

        /// <summary>
        /// Adds a service container to the service collection with the specified options, implementation, and configuration action.
        /// </summary>
        /// <typeparam name="TServiceContainer">The type of service container.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation of the service container.</typeparam>
        /// <typeparam name="TOptions">The type of service container options.</typeparam>
        /// <param name="services">The service collection.</param>
        /// <param name="configure">An action to configure the service container options.</param>
        /// <returns>The modified service collection.</returns>
        public static IServiceCollection AddServiceContainer<TServiceContainer, TImplementation, TOptions>(this IServiceCollection services, Action<TOptions> configure)
            where TServiceContainer : IBaseServiceContainer
            where TImplementation : class, TServiceContainer
            where TOptions : class, IBaseServiceContainerOptions
        {
            services.AddServiceContainer<TOptions>(typeof(TServiceContainer), typeof(TImplementation), configure);
            return services;
        }

        /// <summary>
        /// Adds a service container to the service collection with the default configuration.
        /// </summary>
        /// <typeparam name="TServiceContainer">The type of service container.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation of the service container.</typeparam>
        /// <param name="services">The service collection.</param>
        /// <returns>The modified service collection.</returns>
        public static IServiceCollection AddServiceContainer<TServiceContainer, TImplementation>(this IServiceCollection services)
            where TServiceContainer : IBaseServiceContainer
            where TImplementation : class, TServiceContainer
        {
            services.AddServiceContainer<ServiceContainerOptions<ServiceContainerLoggerOptions>>(typeof(TServiceContainer), typeof(TImplementation), DefaultServiceContainerOptions);
            return services;
        }

        /// <summary>
        /// Ensures that the service container types are valid.
        /// </summary>
        /// <param name="serviceContainerType">The type of the service container.</param>
        /// <param name="implementationServiceContainerType">The type of the implementation of the service container.</param>
        /// <exception cref="ArgumentNullException">Thrown when either <paramref name="serviceContainerType"/> or <paramref name="implementationServiceContainerType"/> is null.</exception>
        /// <exception cref="InvalidCastException">Thrown when <paramref name="serviceContainerType"/> is not a subclass of <see cref="IBaseServiceContainer"/> or when <paramref name="implementationServiceContainerType"/> is not a subclass of <paramref name="serviceContainerType"/>.</exception>
        private static void EnsureValidServiceContainerTypes(Type serviceContainerType, Type implementationServiceContainerType)
        {
            serviceContainerType.ThrowIfNull();
            implementationServiceContainerType.ThrowIfNull();
            TypeExtensions.EnsureValidImplement((typeof(IBaseServiceContainer)), serviceContainerType);
            TypeExtensions.EnsureValidImplement(serviceContainerType, implementationServiceContainerType);
        }
    }
}