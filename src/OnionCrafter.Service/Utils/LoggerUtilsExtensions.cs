using Microsoft.Extensions.Logging;
using OnionCrafter.Service.Exceptions;

namespace OnionCrafter.Service.Utils
{
    /// <summary>
    /// Utility class for checking logger implementation.
    /// </summary>
    public static class LoggerUtilsExtensions
    {
        /// <summary>
        /// Checks if the logger is implemented when it is expected to be enabled.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="isEnabled">Flag indicating if the logger is enabled.</param>
        /// <exception cref="NotImplementedLoggerException">Thrown when the logger is not implemented but it is expected to be enabled.</exception>
        public static TLogger? CheckLoggerImplementation<TLogger>(this TLogger? logger, bool isEnabled)
            where TLogger : ILogger
        {
            if (logger == null && isEnabled)
                throw new NotImplementedLoggerException(nameof(logger));

            return logger;
        }

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
        public static void LogAction<TActions>(bool actionResult, TActions action, params object?[] args)
            where TActions : struct, Enum
        {
        }
    }
}