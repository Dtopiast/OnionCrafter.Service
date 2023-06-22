using OnionCrafter.Base.Utils;
using OnionCrafter.Service.Exceptions;
using OnionCrafter.Service.Options.Services;

namespace OnionCrafter.Service.Utils
{
    /// <summary>
    /// Extension methods for configuring ServiceOptions.
    /// </summary>
    public static class ServiceOptionsExtensions
    {
        /// <summary>
        /// Checks the service options configuration for the specified type of options.
        /// </summary>
        /// <typeparam name="TOptions">The type of options to check.</typeparam>
        /// <param name="serviceOptions">The service options to check.</param>
        /// <returns>The service options.</returns>
        public static TOptions CheckServiceOptionsImplementation<TOptions>(this TOptions? serviceOptions)
            where TOptions : IBaseServiceOptions

        {
            return serviceOptions.ThrowIfNull<TOptions, ServiceConfigNotFoundException>();
        }
    }
}