using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ural.Core.BaseTypes;

namespace Ural.Infrastructure.GenericMySqlRepository
{
    /// <summary>
    /// The GenericMySqlRepository Class 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <seealso cref="Ural.Infrastructure.GenericMySqlRepository.IGenericMySqlRepository{TEntity, TKey}" />
    public abstract class GenericMySqlRepository<TEntity, TKey> : IGenericMySqlRepository<TEntity, TKey> where TEntity : BaseType<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// The context
        /// </summary>
        protected readonly DbContext _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericMySqlRepository{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        protected GenericMySqlRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        /// <summary>
        /// Inserts the one.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void InsertOne(TEntity entity)
        {
            entity.DateOfCreation = DateTimeOffset.UtcNow;

            _context.Set<TEntity>().Add(entity);
        }
        /// <summary>
        /// Inserts the one asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual Task InsertOneAsync(TEntity entity)
        {
            entity.DateOfCreation = DateTimeOffset.UtcNow;

            _context.Set<TEntity>().AddAsync(entity);

            return Task.CompletedTask;
        }
        /// <summary>
        /// Inserts the many.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void InsertMany(IReadOnlyList<TEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                TEntity item        = entities[i];
                item.DateOfCreation = DateTimeOffset.UtcNow;
            }

            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            _context.AddRange(entities);
            _context.ChangeTracker.DetectChanges();
        }

        /// <summary>
        /// Inserts the many asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual async Task InsertManyAsync(IReadOnlyList<TEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                TEntity entity        = entities[i];
                entity.DateOfCreation = DateTimeOffset.UtcNow;
            }

            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            await _context.AddRangeAsync(entities);
            _context.ChangeTracker.DetectChanges();
        }
        /// <summary>
        /// Updates the one.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void UpdateOne(TEntity entity)
        {
            entity.DateOfUpdate = DateTimeOffset.UtcNow;

            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        /// <summary>
        /// Updates the many.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void UpdateMany(IReadOnlyList<TEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                TEntity entity      = entities[i];
                entity.DateOfUpdate = DateTimeOffset.UtcNow;
            }

            _context.Set<TEntity>().AttachRange(entities);
            _context.Entry(entities).State = EntityState.Modified;
        }
        /// <summary>
        /// Deletes the hard one.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void DeleteHardOne(TKey id)
        {
            TEntity entity = _context.Set<TEntity>().Find(id);

            DeleteHardOne(entity);
        }
        /// <summary>
        /// Deletes the hard one.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void DeleteHardOne(TEntity entity)
        {
            DbSet<TEntity> dbSet = _context.Set<TEntity>();

            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }
        /// <summary>
        /// Deletes the hard many.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void DeleteHardMany(IReadOnlyList<TEntity> entities)
        {
            DbSet<TEntity> dbSet = _context.Set<TEntity>();

            for (int i = 0; i < entities.Count; i++)
            {
                TEntity entity = entities[i];

                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
            }

            dbSet.RemoveRange(entities);
        }
        /// <summary>
        /// Deletes the soft one.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void DeleteSoftOne(TKey id)
        {
            TEntity entity = _context.Set<TEntity>().Find(id);
            DeleteSoftOne(entity);
        }
        /// <summary>
        /// Deletes the soft one.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void DeleteSoftOne(TEntity entity)
        {
            entity.DateOfDeletion = DateTimeOffset.UtcNow;
            entity.IsItDeleted    = true;

            UpdateOne(entity);
        }
        /// <summary>
        /// Deletes the soft many.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void DeleteSoftMany(IReadOnlyList<TEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].DateOfDeletion = DateTimeOffset.UtcNow;
                entities[i].IsItDeleted    = true;
            }

            UpdateMany(entities);
        }
        /// <summary>
        /// Gets the queryable.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter                        = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            int? skip                                                     = null,
            int? take                                                     = null,
            bool includeDeleted                                           = false)

        {
            try
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = false;

                IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

                if (!includeDeleted)
                {
                    query = query.Where(q => q.DateOfDeletion == null);
                }

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (includeProperties != null)
                {
                    query = query.Include(includeProperties);
                }
                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                if (skip.HasValue)
                {
                    query = query.Skip(skip.Value);
                }

                if (take.HasValue)
                {
                    query = query.Take(take.Value);
                }

                return query;
            }
            finally
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        public virtual IReadOnlyList<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            int? skip                                                     = null,
            int? take                                                     = null,
            bool includeDeleted                                           = false)

        {
            IReadOnlyList<TEntity> result = GetQueryable(
                filter           : null,
                orderBy          : orderBy,
                includeProperties: includeProperties,
                skip             : skip,
                take             : take,
                includeDeleted   : includeDeleted)
                .ToList();

            return result;
        }
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            int? skip                                                     = null,
            int? take                                                     = null,
            bool includeDeleted                                           = false)

        {
            IReadOnlyList<TEntity> result = await GetQueryable(
                filter           : null,
                orderBy          : orderBy,
                includeProperties: includeProperties,
                skip             : skip,
                take             : take,
                includeDeleted   : includeDeleted)
                .ToListAsync()
                .ConfigureAwait(false);

            return result;
        }
        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        public virtual IReadOnlyList<TEntity> Get(
            Expression<Func<TEntity, bool>> filter                        = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            int? skip                                                     = null,
            int? take                                                     = null,
            bool includeDeleted                                           = false)

        {
            IReadOnlyList<TEntity> result = GetQueryable(
                filter           : filter,
                orderBy          : orderBy,
                includeProperties: includeProperties,
                skip             : skip,
                take             : take,
                includeDeleted   : includeDeleted)
                .ToList();

            return result;
        }
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        public virtual async Task<IReadOnlyList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter                        = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            int? skip                                                     = null,
            int? take                                                     = null,
            bool includeDeleted                                           = false)

        {
            IReadOnlyList<TEntity> result = await GetQueryable(
                filter           : filter,
                orderBy          : orderBy,
                includeProperties: includeProperties,
                skip             : skip,
                take             : take,
                includeDeleted   : includeDeleted)
                .ToListAsync()
                .ConfigureAwait(false);

            return result;
        }
        /// <summary>
        /// Gets the one.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        public virtual TEntity GetOne(
            Expression<Func<TEntity, bool>> filter            = null,
            Expression<Func<TEntity, bool>> includeProperties = null,
            bool includeDeleted                               = false)

        {
            TEntity result = GetQueryable(
                filter           : filter,
                orderBy          : null,
                includeProperties: includeProperties,
                includeDeleted   : includeDeleted)
                .SingleOrDefault();

            return result;
        }
        /// <summary>
        /// Gets the one asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter            = null,
            Expression<Func<TEntity, bool>> includeProperties = null,
            bool includeDeleted                               = false)

        {
            TEntity result = await GetQueryable(
                filter           : filter,
                orderBy          : null,
                includeProperties: includeProperties,
                includeDeleted   : includeDeleted)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            return result;
        }
        /// <summary>
        /// Gets the first.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        public virtual TEntity GetFirst(
            Expression<Func<TEntity, bool>> filter                        = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            bool includeDeleted                                           = false)

        {
            TEntity result = GetQueryable(
                filter           : filter,
                orderBy          : orderBy,
                includeProperties: includeProperties,
                includeDeleted   : includeDeleted)
                .FirstOrDefault();

            return result;
        }

        /// <summary>
        /// Gets the first asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetFirstAsync(
            Expression<Func<TEntity, bool>> filter                        = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            bool includeDeleted                                           = false)

        {
            TEntity result = await GetQueryable(
                filter           : filter,
                orderBy          : orderBy,
                includeProperties: includeProperties,
                includeDeleted   : includeDeleted)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        public virtual TEntity GetById(
            TKey id,
            bool includeDeleted = false)

        {
            TEntity entity = _context.Set<TEntity>().Find(id);

            if (!includeDeleted && entity.DateOfDeletion != null)
            {
                return null;
            }

            return entity;
        }
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByIdAsync(
            TKey id,
            bool includeDeleted = false)

        {
            TEntity entity = await _context.Set<TEntity>().FindAsync(id).ConfigureAwait(false);

            if (!includeDeleted && entity.DateOfDeletion != null)
            {
                return null;
            }

            return entity;
        }
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="countTheDeletedItems">if set to <c>true</c> [count the deleted items].</param>
        /// <returns></returns>
        public virtual int GetCount(
            Expression<Func<TEntity, bool>> filter = null,
            bool countTheDeletedItems              = false)

        {
            int count = GetQueryable(
                filter        : filter,
                includeDeleted: countTheDeletedItems)
                .Count();

            return count;
        }
        /// <summary>
        /// Gets the count asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="countTheDeletedItems">if set to <c>true</c> [count the deleted items].</param>
        /// <returns></returns>
        public virtual async Task<int> GetCountAsync(
            Expression<Func<TEntity, bool>> filter = null,
            bool countTheDeletedItems              = false)

        {
            int count = await GetQueryable(
                filter        : filter,
                includeDeleted: countTheDeletedItems)
                .CountAsync()
                .ConfigureAwait(false);

            return count;
        }
        /// <summary>
        /// Gets the exists.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        public virtual bool GetExists(
            Expression<Func<TEntity, bool>> filter = null,
            bool includeDeleted                    = false)

        {
            bool exists = GetQueryable(
                filter        : filter,
                includeDeleted: includeDeleted)
                .Any();

            return exists;
        }
        /// <summary>
        /// Gets the exists asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        public virtual async Task<bool> GetExistsAsync(
            Expression<Func<TEntity, bool>> filter = null,
            bool includeDeleted                    = false)

        {
            bool exists = await GetQueryable(
                filter        : filter,
                includeDeleted: includeDeleted)
                .AnyAsync()
                .ConfigureAwait(false);

            return exists;
        }
    }
}
