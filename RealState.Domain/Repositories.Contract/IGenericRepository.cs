using RealState.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Domain.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseModel
    {

        Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking = true);

        Task<T?> GetByIdAsync(int id);

        int Add(T entity);

        int Update(T entity);

        int Delete(T entity);

    }
}
