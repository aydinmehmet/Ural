using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ural.Core.BaseTypes;

namespace Ural.Infrastructure.GenericMsSqlRepository
{
    /// <summary>
    /// The Interface Of GenericMsSqlRepository Class
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public interface IGenericMsSqlRepository<TEntity, in TKey> where TEntity : BaseType<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Inserts the one.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void InsertOne(TEntity entity);
        /// <summary>
        /// Inserts the one asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task InsertOneAsync(TEntity entity);
        /// <summary>
        /// Inserts the many.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void InsertMany(IReadOnlyList<TEntity> entities);
        /// <summary>
        /// Inserts the many asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        Task InsertManyAsync(IReadOnlyList<TEntity> entities);
        /// <summary>
        /// Updates the one.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void UpdateOne(TEntity entity);
        /// <summary>
        /// Updates the many.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void UpdateMany(IReadOnlyList<TEntity> entities);
        /// <summary>
        /// Deletes the hard one.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteHardOne(TKey id);
        /// <summary>
        /// Deletes the hard one.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void DeleteHardOne(TEntity entity);
        /// <summary>
        /// Deletes the hard many.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void DeleteHardMany(IReadOnlyList<TEntity> entities);
        /// <summary>
        /// Deletes the soft one.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteSoftOne(TKey id);
        /// <summary>
        /// Deletes the soft one.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void DeleteSoftOne(TEntity entity);
        /// <summary>
        /// Deletes the soft many.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void DeleteSoftMany(IReadOnlyList<TEntity> entities);
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        IReadOnlyList<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            int? skip                                                     = null,
            int? take                                                     = null,
            bool includeDeleted                                           = false);
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        Task<IReadOnlyList<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            int? skip                                                     = null,
            int? take                                                     = null,
            bool includeDeleted                                           = false);
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
        IReadOnlyList<TEntity> Get(
            Expression<Func<TEntity, bool>> filter                        = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            int? skip                                                     = null,
            int? take                                                     = null,
            bool includeDeleted                                           = false);
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
        Task<IReadOnlyList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter                        = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            int? skip                                                     = null,
            int? take                                                     = null,
            bool includeDeleted                                           = false);
        /// <summary>
        /// Gets the one.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        TEntity GetOne(
            Expression<Func<TEntity, bool>> filter            = null,
            Expression<Func<TEntity, bool>> includeProperties = null,
            bool includeDeleted                               = false);
        /// <summary>
        /// Gets the one asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter            = null,
            Expression<Func<TEntity, bool>> includeProperties = null,
            bool includeDeleted                               = false);
        /// <summary>
        /// Gets the first.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        TEntity GetFirst(
            Expression<Func<TEntity, bool>> filter                        = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            bool includeDeleted                                           = false);
        /// <summary>
        /// Gets the first asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        Task<TEntity> GetFirstAsync(
            Expression<Func<TEntity, bool>> filter                        = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            bool includeDeleted                                           = false);
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        TEntity GetById(
            TKey id,
            bool includeDeleted = false);
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(
            TKey id,
            bool includeDeleted = false);
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="countTheDeletedItems">if set to <c>true</c> [count the deleted items].</param>
        /// <returns></returns>
        int GetCount(
            Expression<Func<TEntity, bool>> filter = null,
            bool countTheDeletedItems              = false);
        /// <summary>
        /// Gets the count asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="countTheDeletedItems">if set to <c>true</c> [count the deleted items].</param>
        /// <returns></returns>
        Task<int> GetCountAsync(
            Expression<Func<TEntity, bool>> filter = null,
            bool countTheDeletedItems              = false);
        /// <summary>
        /// Gets the exists.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        bool GetExists(
            Expression<Func<TEntity, bool>> filter = null,
            bool includeDeleted                    = false);
        /// <summary>
        /// Gets the exists asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <returns></returns>
        Task<bool> GetExistsAsync(
            Expression<Func<TEntity, bool>> filter = null,
            bool includeDeleted                    = false);
    }
}
