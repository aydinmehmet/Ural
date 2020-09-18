using System;
using System.Text.Json;

namespace Ural.Common.Models.Response
{
    /// <summary>
    /// The ResponseModel Class 
    /// </summary>
    /// <seealso cref="Ural.Common.Models.Response.IResponse" />
    public class ResponseModel : IResponse
    {
        /// <summary>
        /// Gets the request identifier.
        /// </summary>
        /// <value>
        /// The request identifier.
        /// </value>
        public Guid RequestId => Guid.NewGuid();
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IResponse" /> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; }
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public int StatusCode { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is pagination.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is pagination; otherwise, <c>false</c>.
        /// </value>
        public bool IsPagination { get; set; }
        /// <summary>
        /// Gets the date time offset UTC.
        /// </summary>
        /// <value>
        /// The date time offset UTC.
        /// </value>
        public DateTimeOffset DateTimeOffsetUtc => DateTimeOffset.UtcNow;
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public object Result { get; set; }
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
