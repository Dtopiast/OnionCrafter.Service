using OnionCrafter.Base.Commons;

namespace OnionCrafter.Service.Options.ServiceOptions
{
    /// <summary>
    /// Interface for service options.
    /// </summary>
    public interface IServiceOptions : IBaseServiceOptions, IUseLogger
    {
        /// <summary>
        /// Property to get and set the service name.
        /// </summary>
        public string? SetServiceName { get; set; }
    }
}