using AllBirds.Application.Contracts;
using AllBirds.Models;
using AllBirds.Context;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AllBirds.Infrastructure
{
    public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        private readonly AllBirdsContext context;///ليه بنعرفه طول ما اح
        ///هتكتب ايه فى get one 
      ///
        private readonly DbSet<TEntity> dbset;

        public GenericRepository(AllBirdsContext _context)
        {
            context = _context;
            dbset = _context.Set<TEntity>();
        }
        ///Hossam add
        public ValueTask<TEntity> GetOneAsync(TId id)
        {
            return dbset.FindAsync(id);

        }
        public async Task<TEntity> CreateAsync(TEntity Entity) => (await dbset.AddAsync(Entity)).Entity;

        public Task<TEntity> UpdateAsync(TEntity Entity) => Task.FromResult(dbset.Update(Entity).Entity);

        public Task<TEntity> DeleteAsync(TEntity Entity) => Task.FromResult(dbset.Remove(Entity).Entity);

        public Task<IQueryable<TEntity>> GetAllAsync() => Task.FromResult((IQueryable<TEntity>)dbset);

        //public async Task<TEntity> GetOneAsync(TId id) =>
        //    await dbset.FirstOrDefaultAsync(e => e.Id.Equals(id) && e.IsDeleted == false);

        public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
    }
}
