namespace Ural.Common.DelegatingHandlers
{
    /// <summary>
    /// The HMACDelegatingHandlerOptions Class 
    /// </summary>
    public class HMACDelegatingHandlerOptions
    {
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public string ApplicationId { get; set; }
        /// <summary>
        /// Gets or sets the application key.
        /// </summary>
        /// <value>
        /// The application key.
        /// </value>
        public string ApplicationKey { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
    }
}