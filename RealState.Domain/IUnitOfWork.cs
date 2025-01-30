using RealState.Domain.Entities;
using RealState.Domain.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Domain
{
    public interface IUnitOfWork :IAsyncDisposable
    {
        public IGenericRepository<T> Repository<T>() where T : BaseModel;

        Task<int> CompleteAync();

    }
}
