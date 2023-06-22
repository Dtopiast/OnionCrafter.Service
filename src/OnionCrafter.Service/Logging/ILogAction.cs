using Microsoft.Extensions.Logging;

namespace OnionCrafter.Service.Logging
{
    /// <summary>
    /// Interface representing a log action.
    /// </summary>
    public interface ILogAction : IBaseLogAction
    {
        /// <summary>
        /// Gets or sets the success message for the log action.
        /// </summary>
        string SuccessMessage { get; set; }

        /// <summary>
        /// Gets or sets the log level for the success message.
        /// </summary>
        LogLevel SuccessLogLevel { get; set; }

        /// <summary>
        /// Gets or sets the failure message for the log action.
        /// </summary>
        string FailureMessage { get; set; }

        /// <summary>
        /// Gets or sets the log level for the failure message.
        /// </summary>
        LogLevel FailureLogLevel { get; set; }
    }
}