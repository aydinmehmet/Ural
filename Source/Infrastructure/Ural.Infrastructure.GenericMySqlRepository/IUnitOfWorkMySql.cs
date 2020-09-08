using System;
using System.Threading.Tasks;

namespace Ural.Infrastructure.GenericMySqlRepository
{
    /// <summary>
    /// The Interface Of UnitOfWorkMySql Class
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWorkMySql : IDisposable
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
