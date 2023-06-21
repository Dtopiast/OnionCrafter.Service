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

    public interface IBaseService<TBaseServiceOptions> : IPrototype<IBaseService>
        where TBaseServiceOptions : IBaseServiceOptions
    {
    }
}