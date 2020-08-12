using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbcontext;
        public GenericRepository(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<T> GetByIdAsyn(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }
        
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbcontext.Set<T>().AsQueryable(), spec);
        }
    }
}