namespace OnionCrafter.Service.Utils
{
    /// <summary>
    /// Interface for objects that have a name property.
    /// </summary>
    public interface ISetName
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string? SetName { get; protected set; }
    }
}