using OnionCrafter.Base.Commons;

namespace OnionCrafter.Service.Services
{
    /// <summary>
    /// Base interface for a service.
    /// </summary>
    public interface IBaseService : IAsyncDisposable, IPrototype<IService>
    {
    }
}