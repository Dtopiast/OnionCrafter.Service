namespace OnionCrafter.Service.Options.GlobalOptions
{
    /// <summary>
    /// Represents the global service options.
    /// </summary>
    public class GlobalOptions : IGlobalOptions
    {
        /// <summary>
        ///  /// Gets or sets a value indicating whether to use a logger.
        /// </summary>
        public bool UseLogger { get; set; }
    }
}