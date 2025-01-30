using RealState.Domain;
using RealState.Domain.Entities;
using RealState.Domain.Repositories.Contract;
using RealState.Infrastructure.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        private Hashtable _repositories;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            _repositories = new Hashtable();
        }

        public async Task<int> CompleteAync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
            => await _dbContext.DisposeAsync();
        

        public IGenericRepository<T> Repository<T>() where T : BaseModel
        {
            var key = typeof(T).Name;
            if(! _repositories.ContainsKey(key))
            {
                var repository = new GenericRepository<T>(_dbContext);
                _repositories.Add(key, repository);
            }
            return _repositories[key] as IGenericRepository<T>;

        }
    }
}
