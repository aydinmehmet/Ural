﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ural.Core.BaseTypes;

namespace Ural.EntityServices.GenericMsSqlEntityService
{
    /// <summary>
    /// The Interface Of GenericMsSqlEntityService Class
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public interface IGenericMsSqlEntityService<TEntity, TKey> where TEntity : BaseType<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Inserts the one.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool InsertOne(TEntity entity);
        /// <summary>
        /// Inserts the one asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<bool> InsertOneAsync(TEntity entity);
        /// <summary>
        /// Inserts the many.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        bool InsertMany(IReadOnlyList<TEntity> entities);
        /// <summary>
        /// Inserts the many asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        Task<bool> InsertManyAsync(IReadOnlyList<TEntity> entities);
        /// <summary>
        /// Updates the one.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool UpdateOne(TEntity entity);
        /// <summary>
        /// Updates the one asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<bool> UpdateOneAsync(TEntity entity);
        /// <summary>
        /// Updates the many.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        bool UpdateMany(IReadOnlyList<TEntity> entities);
        /// <summary>
        /// Updates the many asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        Task<bool> UpdateManyAsync(IReadOnlyList<TEntity> entities);
        /// <summary>
        /// Deletes the hard one.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        bool DeleteHardOne(TKey id);
        /// <summary>
        /// Deletes the hard one asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteHardOneAsync(TKey id);
        /// <summary>
        /// Deletes the hard one.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool DeleteHardOne(TEntity entity);
        /// <summary>
        /// Deletes the hard one asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<bool> DeleteHardOneAsync(TEntity entity);
        /// <summary>
        /// Deletes the hard many.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        bool DeleteHardMany(IReadOnlyList<TEntity> entities);
        /// <summary>
        /// Deletes the hard many asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        Task<bool> DeleteHardManyAsync(IReadOnlyList<TEntity> entities);
        /// <summary>
        /// Deletes the soft one.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        bool DeleteSoftOne(TKey id);
        /// <summary>
        /// Deletes the soft one asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteSoftOneAsync(TKey id);
        /// <summary>
        /// Deletes the soft one.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool DeleteSoftOne(TEntity entity);
        /// <summary>
        /// Deletes the soft one asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<bool> DeleteSoftOneAsync(TEntity entity);
        /// <summary>
        /// Deletes the soft many.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        bool DeleteSoftMany(IReadOnlyList<TEntity> entities);
        /// <summary>
        /// Deletes the soft many asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        Task<bool> DeleteSoftManyAsync(IReadOnlyList<TEntity> entities);
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        IReadOnlyList<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            int? skip                                                     = null,
            int? take                                                     = null,
            bool bringTheDeleted                                          = false);
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        Task<IReadOnlyList<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            int? skip                                                     = null,
            int? take                                                     = null,
            bool bringTheDeleted                                          = false);
        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        IReadOnlyList<TEntity> Get(
            Expression<Func<TEntity, bool>> filter                        = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            int? skip                                                     = null,
            int? take                                                     = null,
            bool bringTheDeleted                                          = false);
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        Task<IReadOnlyList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter                        = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            int? skip                                                     = null,
            int? take                                                     = null,
            bool bringTheDeleted                                          = false);
        /// <summary>
        /// Gets the one.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        TEntity GetOne(
            Expression<Func<TEntity, bool>> filter            = null,
            Expression<Func<TEntity, bool>> includeProperties = null,
            bool bringTheDeleted                              = false);
        /// <summary>
        /// Gets the one asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter            = null,
            Expression<Func<TEntity, bool>> includeProperties = null,
            bool bringTheDeleted                              = false);
        /// <summary>
        /// Gets the first.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        TEntity GetFirst(
            Expression<Func<TEntity, bool>> filter                        = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            bool bringTheDeleted                                          = false);
        /// <summary>
        /// Gets the first asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        Task<TEntity> GetFirstAsync(
            Expression<Func<TEntity, bool>> filter                        = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            bool bringTheDeleted                                          = false);
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        TEntity GetById(
            TKey id,
            bool bringTheDeleted = false);
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(
            TKey id,
            bool bringTheDeleted = false);
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
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        bool GetExists(
            Expression<Func<TEntity, bool>> filter = null,
            bool bringTheDeleted                   = false);
        /// <summary>
        /// Gets the exists asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        Task<bool> GetExistsAsync(
            Expression<Func<TEntity, bool>> filter = null,
            bool bringTheDeleted                   = false);
    }
}
