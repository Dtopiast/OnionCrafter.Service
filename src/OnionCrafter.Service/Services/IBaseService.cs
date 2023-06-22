using OnionCrafter.Base.Commons;
using OnionCrafter.Service.Options.Services;

namespace OnionCrafter.Service.Services
{
    /// <summary>
    /// Interface for a base service that implements IPrototype.
    /// </summary>
    public interface IBaseService : IPrototype<IBaseService>
    {
    }

    /// <summary>
    /// Interface for a base service with specific options that implements IPrototype.
    /// </summary>
    public interface IBaseService<TBaseServiceOptions> : IPrototype<IBaseService>
        where TBaseServiceOptions : IBaseServiceOptions
    {
    }
}