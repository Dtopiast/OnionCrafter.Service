using OnionCrafter.Service.Options.Globals;
using OnionCrafter.Service.Options.Services;

namespace OnionCrafter.Service.Services
{
    /// <summary>
    /// Represents an interface for a generic service with options of type TServiceOptions.
    /// </summary>
    public interface IService<TGlobalOptions> :
        IBaseService
        where TGlobalOptions : class, IBaseGlobalOptions
    {
        /// <summary>
        /// The name of the service
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// Represents an interface for a generic service with options of type TServiceOptions and TGlobalOptions.
    /// </summary>
    public interface IService<TGlobalOptions, TServiceOptions> :
        IBaseService<TServiceOptions>,
        IService<TGlobalOptions>
        where TServiceOptions : class, IBaseServiceOptions
        where TGlobalOptions : class, IBaseGlobalOptions
    {
    }
}