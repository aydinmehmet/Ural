using System;
using System.Threading.Tasks;

namespace Ural.Infrastructure.GenericMsSqlRepository
{
    /// <summary>
    /// The Interface Of UnitOfWorkMsSql Class
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWorkMsSql : IDisposable
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
