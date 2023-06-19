using OnionCrafter.Base.Commons;
using OnionCrafter.Service.Options.GlobalOptions;
using OnionCrafter.Service.Options.ServiceOptions;

namespace OnionCrafter.Service.Services
{
    /// <summary>
    /// Represents an interface for a service.
    /// </summary>
    public interface IService : IBaseService
    {
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// Represents an interface for a generic service with options of type TServiceOptions.
    /// </summary>
    public interface IService<TServiceOptions> : IService, IBaseService<TServiceOptions>
           where TServiceOptions : class, IBaseServiceOptions
    {
    }
}