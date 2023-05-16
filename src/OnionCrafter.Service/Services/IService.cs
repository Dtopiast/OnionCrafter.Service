using OnionCrafter.Service.Services.Options;

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

        /// <summary>
        /// Logs an action with the specified result, action type, and arguments.
        /// </summary>
        /// <typeparam name="TActions">The type of the action.</typeparam>
        /// <param name="actionResult">The result of the action.</param>
        /// <param name="action">The action type.</param>
        /// <param name="args">The arguments for the action.</param>
        /// <remarks>
        /// This method logs the specified action with its result and additional arguments.
        /// The action type must be an enum.
        /// </remarks>
        void LogAction<TActions>(bool actionResult, TActions action, params object?[] args)
            where TActions : struct, Enum;
    }

    /// <summary>
    /// Interface for a service with specific options.
    /// </summary>
    /// <typeparam name="TServiceOptions">The type of options for the service.</typeparam>
    public interface IService<TServiceOptions> : IService
        where TServiceOptions : IServiceOptions
    {
        /// <summary>
        /// Gets the configuration options for the service.
        /// </summary>
        public TServiceOptions Config { get; }
    }
}