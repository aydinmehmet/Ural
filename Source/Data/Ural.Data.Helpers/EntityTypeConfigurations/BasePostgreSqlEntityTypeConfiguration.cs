using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq;
using Ural.Core.BaseTypes;

namespace Ural.Data.Helpers.EntityTypeConfigurations
{
    /// <summary>
    /// The BasePostgreSqlEntityTypeConfiguration Class 
    /// </summary>
    /// <typeparam name="TBase">The type of the base.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{TBase}" />
    public abstract class BasePostgreSqlEntityTypeConfiguration<TBase, TKey> : IEntityTypeConfiguration<TBase> where TBase : BaseType<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            builder.HasKey(q => q.Id);

            ValueConverter<byte[], long> converter = new ValueConverter<byte[], long>(v => BitConverter.ToInt64(v, 0), v => BitConverter.GetBytes(v));
            ValueComparer<byte[]> comparer = new ValueComparer<byte[]>((l, r) => (l == null || r == null) ? (l == r) : l.SequenceEqual(r), v => v.GetHashCode());

            builder.Property(q => q.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(q => q.GlobalId).IsRequired().HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
            builder.Property(q => q.Version).IsRequired().HasColumnType("smallint").HasDefaultValueSql("1");
            builder.Property(q => q.CanItBeHardDeleted).IsRequired().HasColumnType("boolean").HasDefaultValueSql("false");
            builder.Property(q => q.CanItBeSoftDeleted).IsRequired().HasColumnType("boolean").HasDefaultValueSql("true");
            builder.Property(q => q.CanItBeUpdated).IsRequired().HasColumnType("boolean").HasDefaultValueSql("true");
            builder.Property(q => q.CreatedBy).HasColumnType("varchar(250)");
            builder.Property(q => q.DateOfCreation).IsRequired().HasColumnType("timestamp with time zone");
            builder.Property(q => q.DateOfDeletion).HasColumnType("timestamp with time zone");
            builder.Property(q => q.DateOfUpdate).HasColumnType("timestamp with time zone");
            builder.Property(q => q.DeletedBy).HasColumnType("varchar(250)");
            builder.Property(q => q.IsItDeleted).HasColumnType("boolean").HasDefaultValueSql("false");
            builder.Property(q => q.UpdatedBy).HasColumnType("varchar(250)");

            builder.Property(q => q.ConcurrencyToken)
                .HasColumnName("xmin")
                .HasColumnType("xid")
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken()
                .HasConversion(converter)
                .Metadata
                .SetValueComparer(comparer);
        }
    }
}
