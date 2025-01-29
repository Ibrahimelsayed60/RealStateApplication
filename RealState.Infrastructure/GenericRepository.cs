using Microsoft.EntityFrameworkCore;
using RealState.Domain.Entities;
using RealState.Domain.Repositories.Contract;
using RealState.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking = true)
        {
            if(withAsNoTracking) 
                return await _dbContext.Set<T>().Where(X => !X.isDeleted).AsNoTracking().ToListAsync();

            return await _dbContext.Set<T>().Where(X => !X.isDeleted).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }

        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            var result = _dbContext.SaveChanges();
            return result;
        }

        public int Delete(T entity)
        {
            entity.isDeleted = true;
            
            return Update(entity);
        }
    }
}
