using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Ural.Infrastructure.GenericMsSqlRepository
{
    /// <summary>
    /// The UnitOfWorkMsSql Class 
    /// </summary>
    /// <seealso cref="Ural.Infrastructure.GenericMsSqlRepository.IUnitOfWorkMsSql" />
    public sealed class UnitOfWorkMsSql : IUnitOfWorkMsSql
    {
        /// <summary>
        /// The context
        /// </summary>
        private DbContext _context;
        /// <summary>
        /// The is successful
        /// </summary>
        private bool isSuccessful = false;
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkMsSql"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        public UnitOfWorkMsSql(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">
        /// UnitOfWorkMsSql Save Method DbUpdateConcurrencyException
        /// or
        /// UnitOfWorkMsSql Save Method Exception
        /// </exception>
        public bool Save()
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();
                    transaction.Commit();

                    isSuccessful = true;
                }
                catch (DbUpdateConcurrencyException exception)
                {
                    transaction.Rollback();
                    throw new InvalidOperationException("UnitOfWorkMsSql Save Method DbUpdateConcurrencyException", exception.InnerException);
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    throw new InvalidOperationException("UnitOfWorkMsSql Save Method Exception", exception.InnerException);
                }
            }

            return isSuccessful;
        }
        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DbUpdateConcurrencyException">UnitOfWorkMsSql SaveAsync Method DbUpdateConcurrencyException</exception>
        /// <exception cref="System.InvalidOperationException">UnitOfWorkMsSql SaveAsync Method Exception</exception>
        public async Task<bool> SaveAsync()
        {
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    
                    isSuccessful = true;
                }
                catch (DbUpdateConcurrencyException exception)
                {
                    await transaction.RollbackAsync();
                    throw new DbUpdateConcurrencyException("UnitOfWorkMsSql SaveAsync Method DbUpdateConcurrencyException", exception.InnerException);
                }
                catch (Exception exception)
                {
                    await transaction.RollbackAsync();
                    throw new InvalidOperationException("UnitOfWorkMsSql SaveAsync Method Exception", exception.InnerException);
                }
            }

            return isSuccessful;
        }
        #region IDisposable Support
        /// <summary>
        /// The disposed value
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_context != null)
                    {
                        _context.Dispose();
                    }
                }

                disposedValue = true;
            }
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
