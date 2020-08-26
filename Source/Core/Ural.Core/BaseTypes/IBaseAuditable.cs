using System;

namespace Ural.Core.BaseTypes
{
    /// <summary>
    /// The Interface Of BaseAuditable Class
    /// </summary>
    public interface IBaseAuditable
    {
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// Gets or sets the deleted by.
        /// </summary>
        /// <value>
        /// The deleted by.
        /// </value>
        public string DeletedBy { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is it deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is it deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsItDeleted { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can it be hard deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can it be hard deleted; otherwise, <c>false</c>.
        /// </value>
        public bool CanItBeHardDeleted { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can it be soft deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can it be soft deleted; otherwise, <c>false</c>.
        /// </value>
        public bool CanItBeSoftDeleted { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can it be updated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can it be updated; otherwise, <c>false</c>.
        /// </value>
        public bool CanItBeUpdated { get; set; }
        /// <summary>
        /// Gets or sets the date of creation.
        /// </summary>
        /// <value>
        /// The date of creation.
        /// </value>
        public DateTimeOffset DateOfCreation { get; set; }
        /// <summary>
        /// Gets or sets the date of update.
        /// </summary>
        /// <value>
        /// The date of update.
        /// </value>
        public DateTimeOffset? DateOfUpdate { get; set; }
        /// <summary>
        /// Gets or sets the date of deletion.
        /// </summary>
        /// <value>
        /// The date of deletion.
        /// </value>
        public DateTimeOffset? DateOfDeletion { get; set; }
    }
}
