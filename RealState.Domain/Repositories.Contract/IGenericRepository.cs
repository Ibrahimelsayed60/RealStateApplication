using RealState.Domain.Entities;
using RealState.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Domain.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseModel
    {

        Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking = true);

        Task<T?> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

        Task<T?> GetItemWithSpecAsync(ISpecifications<T> spec);

        

        int Add(T entity);

        int Update(T entity);

        int DeleteSoft(T entity);

        int DeleteHard(T entity);

    }
}
