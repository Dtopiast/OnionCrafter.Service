using OnionCrafter.Base.DataAccess;
using OnionCrafter.Service.Options.ServiceContainers;

namespace OnionCrafter.Service.ServiceContainers
{
    /// <summary>
    /// Interface representing a base service container that implements <see cref="IDataAccessDetails"/> and <see cref="IAsyncDisposable"/>.
    /// </summary>
    public interface IBaseServiceContainer : IDataAccessDetails, IAsyncDisposable
    {
    }

    /// <summary>
    /// Interface representing a base service container with options of type TBaseContainerOptions.
    /// </summary>
    /// <typeparam name="TBaseContainerOptions">The type of base container options.</typeparam>

    public interface IBaseServiceContainer<TBaseContainerOptions> : IBaseServiceContainer
        where TBaseContainerOptions : class, IBaseServiceContainerOptions
    {
    }
}