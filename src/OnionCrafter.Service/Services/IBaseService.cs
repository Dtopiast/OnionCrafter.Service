using OnionCrafter.Base.Commons;
using OnionCrafter.Service.Options.GlobalOptions;
using OnionCrafter.Service.Options.ServiceOptions;

namespace OnionCrafter.Service.Services
{
    /// <summary>
    /// Interface for a base service that implements IAsyncDisposable and IPrototype.
    /// </summary>
    public interface IBaseService : IPrototype<IBaseService>
    {
    }

    /// <summary>
    /// Represents an interface for a generic base service with options.
    /// </summary>
    public interface IBaseService<TBaseServiceOptions> : IBaseService
    {
    }
}