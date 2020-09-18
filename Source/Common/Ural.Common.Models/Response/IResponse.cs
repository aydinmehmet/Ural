using System;

namespace Ural.Common.Models.Response
{
    /// <summary>
    /// The Interface Of Response Class
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// Gets the request identifier.
        /// </summary>
        /// <value>
        /// The request identifier.
        /// </value>
        public Guid RequestId { get; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IResponse"/> is success.
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
        public DateTimeOffset DateTimeOffsetUtc { get; }
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public object Result { get; set; }
    }
}
