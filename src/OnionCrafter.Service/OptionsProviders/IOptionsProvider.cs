using OnionCrafter.Service.Options.Globals;
using OnionCrafter.Service.Options.Services;
using OnionCrafter.Service.Services;

namespace OnionCrafter.Service.OptionsProviders
{
    /// <summary>
    /// Interface for a global service with generic options.
    /// </summary>
    //busca el uso de IServiceProvider
    public interface IOptionsProvider<TGlobalServiceOptions> : IBaseService
        where TGlobalServiceOptions : class, IBaseGlobalOptions
    {
        /// <summary>
        /// Gets the global service options.
        /// </summary>
        /// <returns>The global service options.</returns>
        TGlobalServiceOptions GetGlobalServiceOptions();

        /// <summary>
        /// Gets the service options of type TServiceOptions where TServiceOptions is a non-nullable type that implements IBaseServiceOptions.
        /// <typeparam name="TServiceOptions">The type of the service options.</typeparam>
        /// </summary>
        TServiceOptions? GetServiceOptions<TServiceOptions>()
            where TServiceOptions : class, IBaseServiceOptions;

        /// <summary>
        /// Gets the service options of the specified type for the given service options name.
        /// </summary>
        /// <typeparam name="TServiceOptions">The type of the service options.</typeparam>
        /// <param name="serviceName">Name of the service options.</param>
        /// <returns>The service options of the specified type.</returns>
        TServiceOptions? GetServiceOptions<TServiceOptions>(string serviceName)
                   where TServiceOptions : class, IBaseServiceOptions;
    }
}