using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OnionCrafter.Service.Exceptions;
using OnionCrafter.Service.Options.General;
using OnionCrafter.Service.Utils;

namespace OnionCrafter.Service.DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    [Obsolete]
    public static class OptionsExtensions
    {
        [Obsolete]
        public static IServiceCollection AddTypedOptions<TOptions>(this IServiceCollection services, Action<TOptions> configure)
           where TOptions : class, IBaseOptions
        {
            services.AddTypedOptions(configure, typeof(TOptions).Name);
            return services;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <param name="implementationType"></param>
        /// <returns></returns>
        [Obsolete]
        public static IServiceCollection AddTypedOptions<TOptions>(this IServiceCollection services, Action<TOptions> configure, string implementationType)
            where TOptions : class, IBaseOptions
        {
            TOptions options = Activator.CreateInstance<TOptions>();
            configure.Invoke(options);
            string serviceName = options is ISetName setNameOptions ? setNameOptions.SetName ?? implementationType : implementationType;
            if (services.Where(x => x.ImplementationType == typeof(IOptions<TOptions>)).Any(x => x.ImplementationInstance is IOptions<ISetName> option ? option.Value.SetName == serviceName : false))
                throw new ImplementedOptionException(serviceName);
            services.AddOptions<TOptions>(serviceName).Configure(configure);
            return services;
        }
    }
}