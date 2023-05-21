using Microsoft.Extensions.DependencyInjection;
using OnionCrafter.Service.Services;
using OnionCrafter.Service.Services.Options;
using System.Diagnostics.CodeAnalysis;

namespace OnionCrafter.Service.DependencyInjection
{
    /// <summary>
    /// Extension methods for the Service class.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Flag to indicate if the global service configuration has been setup.
        /// </summary>
        private static bool _isSetupGlobalServiceConfiguration;

        /// <summary>
        /// Adds a scoped service of the specified type TImplementation with an implementation of TService to the specified IServiceCollection.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation of the service.</typeparam>
        /// <param name="services">The IServiceCollection to add the service to.</param>
        /// <returns>The IServiceCollection with the service added.</returns>
        public static IServiceCollection AddTypedScoped<TService, TImplementation>(this IServiceCollection services)
            where TService : IBaseService
            where TImplementation : class, TService
        {
            services.AddTypedScoped(typeof(TService), typeof(TImplementation));
            return services;
        }

        /// <summary>
        /// Adds a scoped service of the specified type with a corresponding implementation to the specified <see cref="IServiceCollection"/> instance.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance to which the service should be added.</param>
        /// <param name="TService">The <see cref="Type"/> of the service to be added.</param>
        /// <param name="TImplementation">The <see cref="Type"/> of the implementation to be used for the service.</param>
        /// <returns>The modified <see cref="IServiceCollection"/> instance with the scoped service added.</returns>
        public static IServiceCollection AddTypedScoped(this IServiceCollection services, Type TService, Type TImplementation)
        {
            services.AddTypedService(TService, TImplementation, ServiceLifetime.Scoped);

            return services;
        }

        /// <summary>
        /// Adds a typed scoped service of the type specified in <typeparamref name="TService"/>
        /// with an implementation of the type specified in <typeparamref name="TImplementation"/>
        /// to the specified <see cref="IServiceCollection"/> with <typeparamref name="TOptions"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <typeparam name="TOptions">The type of the options to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="configure">A callback to configure the <typeparamref name="TOptions"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddTypedScoped<TService, TImplementation, TOptions>(this IServiceCollection services, Action<TOptions> configure)
            where TService : IBaseService<TOptions>
            where TImplementation : class, TService
            where TOptions : class, IBaseServiceOptions
        {
            services.AddTypedScoped(typeof(TService), typeof(TImplementation), configure);
            return services;
        }

        /// <summary>
        /// Adds a scoped service of the specified <paramref name="TService"/> type and implementation <paramref name="TImplementation"/>,
        /// and configures it with the specified <paramref name="configure"/> action.
        /// </summary>
        /// <typeparam name="TOptions">The type of the options for the service.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="TService">The type of the service.</param>
        /// <param name="TImplementation">The type of the implementation.</param>
        /// <param name="configure">The action used to configure the service.</param>
        /// <returns>The <see cref="IServiceCollection"/> with the service added.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either <paramref name="services"/>, <paramref name="TService"/>,
        /// <paramref name="TImplementation"/>, or <paramref name="configure"/> are null.</exception>
        /// <exception cref="ArgumentException">Thrown when <typeparamref name="TOptions"/> does not implement <see cref="IServiceOptions"/>.</exception>
        public static IServiceCollection AddTypedScoped<TOptions>(this IServiceCollection services, Type TService, Type TImplementation, Action<TOptions> configure)
          where TOptions : class, IBaseServiceOptions
        {
            services.AddTypedService(TService, TImplementation, ServiceLifetime.Scoped, configure);
            return services;
        }

        /// <summary>
        /// Adds a typed service of the type specified in <paramref name="serviceType"/> with a
        /// factory specified in <paramref name="implementationType"/> to the <see
        /// cref="IServiceCollection"/> with the given <paramref name="lifetime"/> and <paramref name="configure"/>.
        /// </summary>
        /// <typeparam name="TOptions">The type of the options to be configured.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to be added.</param>
        /// <param name="implementationType">The type of the implementation to be used.</param>
        /// <param name="configure">A delegate to configure the <typeparamref name="TOptions"/>.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> of the service to be added.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddTypedService<TOptions>(this IServiceCollection services, Type serviceType, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type implementationType, ServiceLifetime lifetime, Action<TOptions> configure)
            where TOptions : class, IBaseServiceOptions
        {
            services.AddOptions<TOptions>(implementationType.Name).Configure(configure);

            services.AddTypedService(serviceType, implementationType, lifetime);
            return services;
        }

        /// <summary>
        /// Adds a typed service to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <param name="lifetime">The lifetime of the service to register.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddTypedService(this IServiceCollection services, Type serviceType, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type implementationType, ServiceLifetime lifetime)

        {
            if (!_isSetupGlobalServiceConfiguration)
                services.SetGlobalServiceConfiguration();

            EnsureValidServiceTypes(serviceType, implementationType);

            var descriptorService = new ServiceDescriptor(serviceType, implementationType, lifetime);
            services.Add(descriptorService);
            return services;
        }

        /// <summary>
        /// Adds a typed singleton service of type <typeparamref name="TService"/> with an
        /// implementation of type <typeparamref name="TImplementation"/> to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddTypedSingleton<TService, TImplementation>(this IServiceCollection services)
            where TService : IBaseService
            where TImplementation : class, TService
        {
            services.AddTypedSingleton(typeof(TService), typeof(TImplementation));
            return services;
        }

        /// <summary>
        /// Adds a typed singleton service to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="TService">The type of the service to register.</param>
        /// <param name="TImplementation">The type of the implementation to use.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddTypedSingleton(this IServiceCollection services, Type TService, Type TImplementation)
        {
            services.AddTypedService(TService, TImplementation, ServiceLifetime.Singleton);
            return services;
        }

        /// <summary>
        /// Adds a typed singleton service of type <typeparamref name="TService"/> with an
        /// implementation of type <typeparamref name="TImplementation"/> to the <see
        /// cref="IServiceCollection"/> with <typeparamref name="TOptions"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <typeparam name="TOptions">The type of the options to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="configure">A configuration action to configure the <typeparamref name="TOptions"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddTypedSingleton<TService, TImplementation, TOptions>(this IServiceCollection services, Action<TOptions> configure)
            where TService : IBaseService<TOptions>
            where TImplementation : class, TService
            where TOptions : class, IBaseServiceOptions
        {
            services.AddTypedSingleton(typeof(TService), typeof(TImplementation), configure);
            return services;
        }

        /// <summary>
        /// Adds a typed singleton service to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TOptions">The type of the options.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="TService">The type of the service to register.</param>
        /// <param name="TImplementation">The type of the implementation to register.</param>
        /// <param name="configure">A callback to configure the options.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddTypedSingleton<TOptions>(this IServiceCollection services, Type TService, Type TImplementation, Action<TOptions> configure)
                 where TOptions : class, IBaseServiceOptions
        {
            services.AddTypedService(TService, TImplementation, ServiceLifetime.Singleton, configure);
            return services;
        }

        /// <summary>
        /// Adds a typed transient service of type <typeparamref name="TService"/> with an
        /// implementation of type <typeparamref name="TImplementation"/> to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddTypedTransient<TService, TImplementation>(this IServiceCollection services)
           where TService : IBaseService
           where TImplementation : class, TService
        {
            services.AddTypedTransient(typeof(TService), typeof(TImplementation));
            return services;
        }

        /// <summary>
        /// Adds a typed transient service to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="TService">The type of the service to register.</param>
        /// <param name="TImplementation">The type of the implementation to use.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddTypedTransient(this IServiceCollection services, Type TService, Type TImplementation)
        {
            services.AddTypedService(TService, TImplementation, ServiceLifetime.Transient);
            return services;
        }

        /// <summary>
        /// Adds a typed transient service of type <typeparamref name="TService"/> with an
        /// implementation of type <typeparamref name="TImplementation"/> to the <see
        /// cref="IServiceCollection"/> with <typeparamref name="TOptions"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <typeparam name="TOptions">The type of the options to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="configure">A configuration action to configure the <typeparamref name="TOptions"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddTypedTransient<TService, TImplementation, TOptions>(this IServiceCollection services, Action<TOptions> configure)
            where TService : IBaseService<TOptions>
            where TImplementation : class, TService
            where TOptions : class, IBaseServiceOptions
        {
            services.AddTypedTransient(typeof(TService), typeof(TImplementation), configure);
            return services;
        }

        /// <summary>
        /// Adds a typed transient service to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TOptions">The type of the options.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="TService">The type of the service to register.</param>
        /// <param name="TImplementation">The type of the implementation to register.</param>
        /// <param name="configure">A callback to configure the <typeparamref name="TOptions"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddTypedTransient<TOptions>(this IServiceCollection services, Type TService, Type TImplementation, Action<TOptions> configure)
                  where TOptions : class, IBaseServiceOptions
        {
            services.AddTypedService(TService, TImplementation, ServiceLifetime.Transient, configure);
            return services;
        }

        /// <summary>
        /// Checks the status of the Global Service Configuration.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection CheckStatusGlobalServiceConfiguration(this IServiceCollection services)
        {
            if (!_isSetupGlobalServiceConfiguration)
            {
                bool isOptionRegistered = services.Any(ds => typeof(IGlobalServiceOptions).IsAssignableFrom(ds.ServiceType));
                if (isOptionRegistered)
                    throw new InvalidOperationException("Global service configuration fue creado en un contexto inseguro, favor de usar el metodo SetGlobalServiceConfiguration");
            }
            else
                throw new InvalidOperationException("Global service configuration has already been set up.");

            return services;
        }

        /// <summary>
        /// Sets up the global service configuration with the specified configuration name.
        /// </summary>
        /// <typeparam name="TGlobalServiceConfiguration">The type of the global service configuration.</typeparam>
        /// <param name="services">The service collection.</param>
        /// <param name="globalServiceConfiguration">The global service configuration.</param>
        /// <param name="configName">The configuration name.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection SetGlobalServiceConfiguration<TGlobalServiceConfiguration>(this IServiceCollection services, Action<TGlobalServiceConfiguration> globalServiceConfiguration, string configName)
                    where TGlobalServiceConfiguration : class, IGlobalServiceOptions
        {
            services.CheckStatusGlobalServiceConfiguration();
            services.AddOptions<TGlobalServiceConfiguration>(configName).Configure(globalServiceConfiguration);
            Environment.SetEnvironmentVariable("GlobalServiceConfiguration", configName);
            _isSetupGlobalServiceConfiguration = true;

            return services;
        }

        /// <summary>
        /// Configures the global service options.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection SetGlobalServiceConfiguration(this IServiceCollection services)
        {
            var Options = new GlobalServiceOptions();
            var globalServiceConfiguration = (GlobalServiceOptions options) =>
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
        public static IServiceCollection SetGlobalServiceConfiguration(this IServiceCollection services, Action<GlobalServiceOptions> globalServiceConfiguration)
        {
            services.SetGlobalServiceConfiguration(globalServiceConfiguration);
            return services;
        }

        /// <summary>
        /// Ensures that the service types are valid.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <param name="implementationType">The type of the implementation of the service.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when either <paramref name="serviceType"/> or <paramref
        /// name="implementationType"/> is null.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown when <paramref name="serviceType"/> is not a subclass of <see
        /// cref="IBaseService"/> or when <paramref name="implementationType"/> is not a subclass of
        /// <paramref name="serviceType"/>.
        /// </exception>
        private static void EnsureValidServiceTypes(Type serviceType, Type implementationType)
        {
            if (serviceType is null)
                throw new ArgumentNullException(nameof(serviceType));

            if (implementationType is null)
                throw new ArgumentNullException(nameof(implementationType));

            if (!serviceType.IsSubclassOf(typeof(IBaseService)))
                throw new InvalidCastException($"Type {serviceType} must be a subclass of {nameof(IBaseService)}.");

            if (!implementationType.IsSubclassOf(serviceType))
                throw new InvalidCastException($"Type {implementationType} must be a subclass of {serviceType}.");
        }
    }
}