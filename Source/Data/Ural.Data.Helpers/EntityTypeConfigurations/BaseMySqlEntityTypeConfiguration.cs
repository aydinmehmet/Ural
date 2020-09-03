using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Ural.Core.BaseTypes;

namespace Ural.Data.Helpers.EntityTypeConfigurations
{
    /// <summary>
    /// The BaseMySqlEntityTypeConfiguration Class 
    /// </summary>
    /// <typeparam name="TBase">The type of the base.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{TBase}" />
    public abstract class BaseMySqlEntityTypeConfiguration<TBase, TKey> : IEntityTypeConfiguration<TBase> where TBase : BaseType<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            builder.HasKey(q => q.Id);

            builder.Property(q => q.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(q => q.GlobalId).IsRequired().HasColumnType("varchar(36)").HasDefaultValueSql("(UUID())");
            builder.Property(q => q.ConcurrencyToken).IsRequired().IsRowVersion();
            builder.Property(q => q.Version).IsRequired().HasColumnType("tinyint unsigned").HasDefaultValueSql("1");
            builder.Property(q => q.CanItBeHardDeleted).IsRequired().HasColumnType("boolean").HasDefaultValueSql("0");
            builder.Property(q => q.CanItBeSoftDeleted).IsRequired().HasColumnType("boolean").HasDefaultValueSql("1");
            builder.Property(q => q.CanItBeUpdated).IsRequired().HasColumnType("boolean").HasDefaultValueSql("1");
            builder.Property(q => q.CreatedBy).HasColumnType("varchar(250)");
            builder.Property(q => q.DateOfCreation).IsRequired().HasColumnType("timestamp");
            builder.Property(q => q.DateOfDeletion).HasColumnType("timestamp");
            builder.Property(q => q.DateOfUpdate).HasColumnType("timestamp");
            builder.Property(q => q.DeletedBy).HasColumnType("varchar(250)");
            builder.Property(q => q.IsItDeleted).HasColumnType("boolean").HasDefaultValueSql("0");
            builder.Property(q => q.UpdatedBy).HasColumnType("varchar(250)");
        }
    }
}
