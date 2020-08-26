using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ural.Core.BaseTypes
{
    /// <summary>
    /// The Interface Of BaseType Class
    /// </summary>
    public interface IBaseType
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
    }
    /// <summary>
    /// The Interface Of BaseType Class
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public interface IBaseType<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
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
        [Timestamp]
        public byte[] ConcurrencyToken { get; set; }
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public int Version { get; set; }
    }
}
