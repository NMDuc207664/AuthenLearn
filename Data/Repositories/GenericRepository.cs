using System.Linq.Expressions;
using AuthenLearn.Applications.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthenLearn.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public virtual void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }
        public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take)
        {
            return await _context.Set<TEntity>()
                .Where(predicate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }
    }
}