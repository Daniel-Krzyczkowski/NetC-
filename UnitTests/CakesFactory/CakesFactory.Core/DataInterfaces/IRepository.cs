using CakesFactory.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CakesFactory.Core.DataInterfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid id);

        Task<T> SaveAsync(T entity);
    }
}
