using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Contracts
{
    public interface IGenericRepository<TEntity, TId>
    {
        public Task<TEntity> CreateAsync(TEntity Entity);
        public Task<TEntity> UpdateAsync(TEntity Entity);

        public Task<TEntity> DeleteAsync(TEntity Entity);
        public Task<IQueryable<TEntity>> GetAllAsync();
        //public Task<TEntity> GetOneAsync(TId id);
        public Task<int> SaveChangesAsync();
    }
}
