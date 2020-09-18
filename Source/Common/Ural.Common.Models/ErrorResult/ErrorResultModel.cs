using System.Text.Json;

namespace Ural.Common.Models.ErrorResult
{
    /// <summary>
    /// The ErrorResultModel Class 
    /// </summary>
    /// <seealso cref="Ural.Common.Models.ErrorResult.IErrorResult" />
    public class ErrorResultModel : IErrorResult
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }
        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
