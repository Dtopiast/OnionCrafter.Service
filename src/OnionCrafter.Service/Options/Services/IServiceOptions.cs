using OnionCrafter.Base.Commons;
using OnionCrafter.Service.Utils;

namespace OnionCrafter.Service.Options.Services
{
    /// <summary>
    /// Interface for service options.
    /// </summary>
    public interface IServiceOptions : IBaseServiceOptions, IUseLogger, ISetName
    {
        /// <summary>
        /// Gets or sets the service name.
        /// </summary>
        public new string? SetName { get; set; }
    }
}