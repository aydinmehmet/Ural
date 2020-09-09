using System;
using System.Threading.Tasks;

namespace Ural.Infrastructure.GenericPostgreSqlRepository
{
    /// <summary>
    /// The Interface Of UnitOfWorkPostgreSql Class
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWorkPostgreSql : IDisposable
    {
        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        bool Save();
        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveAsync();
    }
}
