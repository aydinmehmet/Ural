using System.Collections.Generic;

namespace Ural.Common.Middlewares
{
    /// <summary>
    /// The LocalizationMiddlewareOptions Class 
    /// </summary>
    public class LocalizationMiddlewareOptions
    {
        /// <summary>
        /// Gets or sets the default.
        /// </summary>
        /// <value>
        /// The default.
        /// </value>
        public string Default { get; set; }
        /// <summary>
        /// Gets the allowed.
        /// </summary>
        /// <value>
        /// The allowed.
        /// </value>
        public List<string> Allowed { get; } = new List<string>();
    }
}
