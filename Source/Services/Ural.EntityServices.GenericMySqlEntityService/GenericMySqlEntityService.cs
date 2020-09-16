using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ural.Core.BaseTypes;
using Ural.Infrastructure.GenericMySqlRepository;

namespace Ural.EntityServices.GenericMsSqlEntityService
{
    /// <summary>
    /// The GenericMySqlEntityService Class 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <seealso cref="Ural.EntityServices.GenericMsSqlEntityService.IGenericMySqlEntityService{TEntity, TKey}" />
    public class GenericMySqlEntityService<TEntity, TKey> : IGenericMySqlEntityService<TEntity, TKey> where TEntity : BaseType<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// The unit of work my SQL
        /// </summary>
        private readonly IUnitOfWorkMySql _unitOfWorkMySql;
        /// <summary>
        /// The generic my SQL repository
        /// </summary>
        private readonly IGenericMySqlRepository<TEntity, TKey> _genericMySqlRepository;
        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// The is successful
        /// </summary>
        private bool isSuccessful = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericMySqlEntityService{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="unitOfWorkMsSql">The unit of work ms SQL.</param>
        /// <param name="genericMySqlRepository">The generic my SQL repository.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <exception cref="System.ArgumentNullException">
        /// unitOfWorkMsSql
        /// or
        /// genericMySqlRepository
        /// or
        /// httpContextAccessor
        /// </exception>
        public GenericMySqlEntityService(
            IUnitOfWorkMySql unitOfWorkMsSql,
            IGenericMySqlRepository<TEntity, TKey> genericMySqlRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWorkMySql        = unitOfWorkMsSql ?? throw new ArgumentNullException(nameof(unitOfWorkMsSql));
            _genericMySqlRepository = genericMySqlRepository ?? throw new ArgumentNullException(nameof(genericMySqlRepository));
            _httpContextAccessor    = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        /// <summary>
        /// Inserts the one.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public bool InsertOne(TEntity entity)
        {
            if (entity == null) { ErrorArgumentNull(); }

            entity.CreatedBy = GetUserName();

            _genericMySqlRepository.InsertOne(entity);

            isSuccessful = _unitOfWorkMySql.Save();

            if (!isSuccessful) { ErrorSaveMethod(); }

            return isSuccessful;
        }
        /// <summary>
        /// Inserts the one asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<bool> InsertOneAsync(TEntity entity)
        {
            if (entity == null) { ErrorArgumentNull(); }

            entity.CreatedBy = GetUserName();

            await _genericMySqlRepository.InsertOneAsync(entity).ConfigureAwait(false);

            isSuccessful = await _unitOfWorkMySql.SaveAsync().ConfigureAwait(false);

            if (!isSuccessful) { ErrorSaveMethod(); }

            return isSuccessful;
        }
        /// <summary>
        /// Inserts the many.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public bool InsertMany(IReadOnlyList<TEntity> entities)
        {
            if (entities == null || entities.Count == 0) { ErrorArgumentNull(); }

            string createdBy = GetUserName();

            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].CreatedBy = createdBy;
            }

            _genericMySqlRepository.InsertMany(entities);

            isSuccessful = _unitOfWorkMySql.Save();

            if (!isSuccessful) { ErrorSaveMethod(); }

            return isSuccessful;
        }
        /// <summary>
        /// Inserts the many asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public async Task<bool> InsertManyAsync(IReadOnlyList<TEntity> entities)
        {
            if (entities == null || entities.Count == 0) { ErrorArgumentNull(); }

            string createdBy = GetUserName();

            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].CreatedBy = createdBy;
            }

            await _genericMySqlRepository.InsertManyAsync(entities).ConfigureAwait(false);

            isSuccessful = await _unitOfWorkMySql.SaveAsync().ConfigureAwait(false);

            if (!isSuccessful) { ErrorSaveMethod(); }

            return isSuccessful;
        }
        /// <summary>
        /// Updates the one.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public bool UpdateOne(TEntity entity)
        {
            if (entity == null) { ErrorArgumentNull(); }

            if (!entity.CanItBeUpdated) { ErrorCannotBeUpdated(); }

            entity.UpdatedBy = GetUserName();

            _genericMySqlRepository.UpdateOne(entity);

            isSuccessful = _unitOfWorkMySql.Save();

            if (!isSuccessful) { ErrorUpdateMethod(); }

            return isSuccessful;
        }
        /// <summary>
        /// Updates the one asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<bool> UpdateOneAsync(TEntity entity)
        {
            if (entity == null) { ErrorArgumentNull(); }

            if (!entity.CanItBeUpdated) { ErrorCannotBeUpdated(); }

            entity.UpdatedBy = GetUserName();

            _genericMySqlRepository.UpdateOne(entity);

            isSuccessful = await _unitOfWorkMySql.SaveAsync().ConfigureAwait(false);

            if (!isSuccessful) { ErrorUpdateMethod(); }

            return isSuccessful;
        }
        /// <summary>
        /// Updates the many.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public bool UpdateMany(IReadOnlyList<TEntity> entities)
        {
            if (entities == null || entities.Count == 0) { ErrorArgumentNull(); }

            string updatedBy = GetUserName();

            for (int i = 0; i < entities.Count; i++)
            {
                if (!entities[i].CanItBeUpdated) { ErrorCannotBeUpdated(); }

                entities[i].UpdatedBy = updatedBy;
            }

            _genericMySqlRepository.UpdateMany(entities);

            isSuccessful = _unitOfWorkMySql.Save();

            if (!isSuccessful) { ErrorSaveMethod(); }

            return isSuccessful;
        }
        /// <summary>
        /// Updates the many asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public async Task<bool> UpdateManyAsync(IReadOnlyList<TEntity> entities)
        {
            if (entities == null || entities.Count == 0) { ErrorArgumentNull(); }

            string updatedBy = GetUserName();

            for (int i = 0; i < entities.Count; i++)
            {
                if (!entities[i].CanItBeUpdated) { ErrorCannotBeUpdated(); }

                entities[i].UpdatedBy = updatedBy;
            }

            _genericMySqlRepository.UpdateMany(entities);

            isSuccessful = await _unitOfWorkMySql.SaveAsync().ConfigureAwait(false);

            if (!isSuccessful) { ErrorSaveMethod(); }

            return isSuccessful;
        }
        /// <summary>
        /// Deletes the hard one.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool DeleteHardOne(TKey id)
        {
            TEntity entity = GetById(id);

            return DeleteHardOne(entity);
        }
        /// <summary>
        /// Deletes the hard one asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteHardOneAsync(TKey id)
        {
            TEntity entity = await GetByIdAsync(id).ConfigureAwait(false);

            return await DeleteHardOneAsync(entity).ConfigureAwait(false);
        }
        /// <summary>
        /// Deletes the hard one.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public bool DeleteHardOne(TEntity entity)
        {
            if (entity == null) { ErrorArgumentNull(); }

            if (!entity.CanItBeHardDeleted) { ErrorCannotBeHardDeleted(); }

            _genericMySqlRepository.DeleteHardOne(entity);

            isSuccessful = _unitOfWorkMySql.Save();

            if (!isSuccessful) { ErrorDeleteHardMethod(); }

            return isSuccessful;
        }
        /// <summary>
        /// Deletes the hard one asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<bool> DeleteHardOneAsync(TEntity entity)
        {
            if (entity == null) { ErrorArgumentNull(); }

            if (!entity.CanItBeHardDeleted) { ErrorCannotBeHardDeleted(); }

            _genericMySqlRepository.DeleteHardOne(entity);

            isSuccessful = await _unitOfWorkMySql.SaveAsync().ConfigureAwait(false);

            if (!isSuccessful) { ErrorDeleteHardMethod(); }

            return isSuccessful;
        }
        /// <summary>
        /// Deletes the hard many.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public bool DeleteHardMany(IReadOnlyList<TEntity> entities)
        {
            if (entities == null || entities.Count == 0) { ErrorArgumentNull(); }

            for (int i = 0; i < entities.Count; i++)
            {
                if (!entities[i].CanItBeHardDeleted) { ErrorCannotBeHardDeleted(); }
            }

            _genericMySqlRepository.DeleteHardMany(entities);

            isSuccessful = _unitOfWorkMySql.Save();

            if (!isSuccessful) { ErrorSaveMethod(); }

            return isSuccessful;
        }
        /// <summary>
        /// Deletes the hard many asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public async Task<bool> DeleteHardManyAsync(IReadOnlyList<TEntity> entities)
        {
            if (entities == null || entities.Count == 0) { ErrorArgumentNull(); }

            for (int i = 0; i < entities.Count; i++)
            {
                if (!entities[i].CanItBeHardDeleted) { ErrorCannotBeHardDeleted(); }
            }

            _genericMySqlRepository.DeleteHardMany(entities);

            isSuccessful = await _unitOfWorkMySql.SaveAsync().ConfigureAwait(false);

            if (!isSuccessful) { ErrorSaveMethod(); }

            return isSuccessful;
        }
        /// <summary>
        /// Deletes the soft one.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool DeleteSoftOne(TKey id)
        {
            TEntity entity = GetById(id);

            return DeleteSoftOne(entity);
        }
        /// <summary>
        /// Deletes the soft one asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteSoftOneAsync(TKey id)
        {
            TEntity entity = await GetByIdAsync(id).ConfigureAwait(false);

            return await DeleteSoftOneAsync(entity).ConfigureAwait(false);
        }
        /// <summary>
        /// Deletes the soft one.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public bool DeleteSoftOne(TEntity entity)
        {
            if (entity == null) { ErrorArgumentNull(); }

            if (!entity.CanItBeSoftDeleted) { ErrorCannotBeSoftDeleted(); }

            entity.DeletedBy = GetUserName();

            _genericMySqlRepository.DeleteSoftOne(entity);

            isSuccessful = _unitOfWorkMySql.Save();

            if (!isSuccessful) { ErrorDeleteSoftMethod(); }

            return isSuccessful;
        }
        /// <summary>
        /// Deletes the soft one asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<bool> DeleteSoftOneAsync(TEntity entity)
        {
            if (entity == null) { ErrorArgumentNull(); }

            if (!entity.CanItBeSoftDeleted) { ErrorCannotBeSoftDeleted(); }

            entity.DeletedBy = GetUserName();

            _genericMySqlRepository.DeleteSoftOne(entity);

            isSuccessful = await _unitOfWorkMySql.SaveAsync().ConfigureAwait(false);

            if (!isSuccessful) { ErrorDeleteSoftMethod(); }

            return isSuccessful;
        }
        /// <summary>
        /// Deletes the soft many.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public bool DeleteSoftMany(IReadOnlyList<TEntity> entities)
        {
            if (entities == null || entities.Count == 0) { ErrorArgumentNull(); }

            string deletedBy = GetUserName();

            for (int i = 0; i < entities.Count; i++)
            {
                if (!entities[i].CanItBeSoftDeleted) { ErrorCannotBeSoftDeleted(); }

                entities[i].DeletedBy = deletedBy;
                entities[i].IsItDeleted = true;
            }

            return UpdateMany(entities);
        }
        /// <summary>
        /// Deletes the soft many asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public async Task<bool> DeleteSoftManyAsync(IReadOnlyList<TEntity> entities)
        {
            if (entities == null || entities.Count == 0) { ErrorArgumentNull(); }

            string deletedBy = GetUserName();

            for (int i = 0; i < entities.Count; i++)
            {
                if (!entities[i].CanItBeSoftDeleted) { ErrorCannotBeSoftDeleted(); }

                entities[i].DeletedBy = deletedBy;
                entities[i].IsItDeleted = true;
            }

            return await UpdateManyAsync(entities).ConfigureAwait(false);
        }
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        public IReadOnlyList<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            int? skip                                                     = default,
            int? take                                                     = default,
            bool bringTheDeleted                                          = false)
        {
            return _genericMySqlRepository.GetAll(orderBy, includeProperties, skip, take, bringTheDeleted);
        }
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        public Task<IReadOnlyList<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            int? skip                                                     = default,
            int? take                                                     = default,
            bool bringTheDeleted                                          = false)
        {
            return _genericMySqlRepository.GetAllAsync(orderBy, includeProperties, skip, take, bringTheDeleted);
        }
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
        public IReadOnlyList<TEntity> Get(
            Expression<Func<TEntity, bool>> filter                        = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            int? skip                                                     = default,
            int? take                                                     = default,
            bool bringTheDeleted                                          = false)
        {
            return _genericMySqlRepository.Get(filter, orderBy, includeProperties, skip, take, bringTheDeleted);
        }
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
        public Task<IReadOnlyList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter                        = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            int? skip                                                     = default,
            int? take                                                     = default,
            bool bringTheDeleted                                          = false)
        {
            return _genericMySqlRepository.GetAsync(filter, orderBy, includeProperties, skip, take, bringTheDeleted);
        }
        /// <summary>
        /// Gets the one.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        public TEntity GetOne(
            Expression<Func<TEntity, bool>> filter            = null,
            Expression<Func<TEntity, bool>> includeProperties = null,
            bool bringTheDeleted                              = false)
        {
            return _genericMySqlRepository.GetOne(filter, includeProperties, bringTheDeleted);
        }
        /// <summary>
        /// Gets the one asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        public Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter            = null,
            Expression<Func<TEntity, bool>> includeProperties = null,
            bool bringTheDeleted                              = false)
        {
            return _genericMySqlRepository.GetOneAsync(filter, includeProperties, bringTheDeleted);
        }
        /// <summary>
        /// Gets the first.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        public TEntity GetFirst(
            Expression<Func<TEntity, bool>> filter                        = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            bool bringTheDeleted                                          = false)
        {
            return _genericMySqlRepository.GetFirst(filter, orderBy, includeProperties, bringTheDeleted);
        }
        /// <summary>
        /// Gets the first asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        public Task<TEntity> GetFirstAsync(
            Expression<Func<TEntity, bool>> filter                        = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> includeProperties             = null,
            bool bringTheDeleted                                          = false)
        {
            return _genericMySqlRepository.GetFirstAsync(filter, orderBy, includeProperties, bringTheDeleted);
        }
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        public TEntity GetById(
            TKey id,
            bool bringTheDeleted = false)
        {
            return _genericMySqlRepository.GetById(id, bringTheDeleted);
        }
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        public Task<TEntity> GetByIdAsync(
            TKey id,
            bool bringTheDeleted = false)
        {
            return _genericMySqlRepository.GetByIdAsync(id, bringTheDeleted);
        }
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="countTheDeletedItems">if set to <c>true</c> [count the deleted items].</param>
        /// <returns></returns>
        public int GetCount(
            Expression<Func<TEntity, bool>> filter = null,
            bool countTheDeletedItems              = false)
        {
            return _genericMySqlRepository.GetCount(filter, countTheDeletedItems);
        }
        /// <summary>
        /// Gets the count asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="countTheDeletedItems">if set to <c>true</c> [count the deleted items].</param>
        /// <returns></returns>
        public Task<int> GetCountAsync(
            Expression<Func<TEntity, bool>> filter = null,
            bool countTheDeletedItems              = false)
        {
            return _genericMySqlRepository.GetCountAsync(filter, countTheDeletedItems);
        }
        /// <summary>
        /// Gets the exists.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        public bool GetExists(
            Expression<Func<TEntity, bool>> filter = null,
            bool bringTheDeleted                   = false)
        {
            return _genericMySqlRepository.GetExists(filter, bringTheDeleted);
        }
        /// <summary>
        /// Gets the exists asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="bringTheDeleted">if set to <c>true</c> [bring the deleted].</param>
        /// <returns></returns>
        public Task<bool> GetExistsAsync(
            Expression<Func<TEntity, bool>> filter = null,
            bool bringTheDeleted                   = false)
        {
            return _genericMySqlRepository.GetExistsAsync(filter, bringTheDeleted);
        }
        /// <summary>
        /// Errors the save method.
        /// </summary>
        /// <exception cref="System.ArgumentException">An error occurred during the <<SAVE>> method. Model :{typeof(TEntity).FullName}</exception>
        private void ErrorSaveMethod()
        {
            throw new ArgumentException($"An error occurred during the <<SAVE>> method. Model :{typeof(TEntity).FullName}");
        }
        /// <summary>
        /// Errors the update method.
        /// </summary>
        /// <exception cref="System.ArgumentException">An error occurred during the <<UPDATE>> method. Model :{typeof(TEntity).FullName}</exception>
        private void ErrorUpdateMethod()
        {
            throw new ArgumentException($"An error occurred during the <<UPDATE>> method. Model :{typeof(TEntity).FullName}");
        }
        /// <summary>
        /// Errors the delete hard method.
        /// </summary>
        /// <exception cref="System.ArgumentException">An error occurred during the <<DELETE HARD>> method. . Model :{typeof(TEntity).FullName}</exception>
        private void ErrorDeleteHardMethod()
        {
            throw new ArgumentException($"An error occurred during the <<DELETE HARD>> method. . Model :{typeof(TEntity).FullName}");
        }
        /// <summary>
        /// Errors the delete soft method.
        /// </summary>
        /// <exception cref="System.ArgumentException">An error occurred during the <<DELETE SOFT>> method. . Model :{typeof(TEntity).FullName}</exception>
        private void ErrorDeleteSoftMethod()
        {
            throw new ArgumentException($"An error occurred during the <<DELETE SOFT>> method. . Model :{typeof(TEntity).FullName}");
        }
        /// <summary>
        /// Errors the argument null.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Argument is null. Model :{typeof(TEntity).FullName}</exception>
        private void ErrorArgumentNull()
        {
            throw new ArgumentNullException($"Argument is null. Model :{typeof(TEntity).FullName}");
        }
        /// <summary>
        /// Errors the cannot be hard deleted.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Model can not be hard deleted. Model :{typeof(TEntity).FullName}</exception>
        private void ErrorCannotBeHardDeleted()
        {
            throw new NotSupportedException($"Model can not be hard deleted. Model :{typeof(TEntity).FullName}");
        }
        /// <summary>
        /// Errors the cannot be soft deleted.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Model can not be soft deleted. Model :{typeof(TEntity).FullName}</exception>
        private void ErrorCannotBeSoftDeleted()
        {
            throw new NotSupportedException($"Model can not be soft deleted. Model :{typeof(TEntity).FullName}");
        }
        /// <summary>
        /// Errors the cannot be updated.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Model can not be updated. Model :{typeof(TEntity).FullName}</exception>
        private void ErrorCannotBeUpdated()
        {
            throw new NotSupportedException($"Model can not be updated. Model :{typeof(TEntity).FullName}");
        }
        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <returns></returns>
        private string GetUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        }
    }
}
