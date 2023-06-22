using Microsoft.Extensions.Logging;

namespace OnionCrafter.Service.Logging
{
    /// <summary>
    /// Represents a log action.
    /// </summary>
    public class LogAction : ILogAction
    {
        /// <inheritdoc/>
        public string SuccessMessage { get; set; }

        /// <inheritdoc/>
        public LogLevel SuccessLogLevel { get; set; }

        /// <inheritdoc/>
        public string FailureMessage { get; set; }

        /// <inheritdoc/>
        public LogLevel FailureLogLevel { get; set; }

        /// <summary>
        /// Default constructor for the LogAction class.
        /// Initializes the properties with default values.
        /// </summary>
        public LogAction()
        {
            SuccessMessage = string.Empty;
            SuccessLogLevel = LogLevel.Information;
            FailureMessage = string.Empty;
            FailureLogLevel = LogLevel.Error;
        }
    }
}