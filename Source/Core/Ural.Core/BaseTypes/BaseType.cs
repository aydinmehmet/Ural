using System;
using System.Text.Json;


namespace Ural.Core.BaseTypes
{
    /// <summary>
    /// The BaseType Class 
    /// </summary>
    /// <seealso cref="Ural.Core.BaseTypes.BaseAuditable" />
    /// <seealso cref="Ural.Core.BaseTypes.IBaseType" />
    public class BaseType : BaseAuditable, IBaseType
    {
        /// <summary>
        /// Gets or sets the global identifier.
        /// </summary>
        /// <value>
        /// The global identifier.
        /// </value>
        public Guid GlobalId { get; set; }
        /// <summary>
        /// Gets or sets the concurrency token.
        /// </summary>
        /// <value>
        /// The concurrency token.
        /// </value>
        public byte[] ConcurrencyToken { get; set; }
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public int Version { get; set; }
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
    /// <summary>
    /// The BaseType Class 
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <seealso cref="Ural.Core.BaseTypes.BaseAuditable" />
    /// <seealso cref="Ural.Core.BaseTypes.IBaseType" />
    public class BaseType<TKey> : BaseAuditable, IBaseType<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public TKey Id { get; set; }
        /// <summary>
        /// Gets or sets the global identifier.
        /// </summary>
        /// <value>
        /// The global identifier.
        /// </value>
        public Guid GlobalId { get; set; }
        /// <summary>
        /// Gets or sets the concurrency token.
        /// </summary>
        /// <value>
        /// The concurrency token.
        /// </value>
        public byte[] ConcurrencyToken { get; set; }
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public int Version { get; set; }
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
