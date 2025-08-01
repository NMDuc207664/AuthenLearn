using System.Linq.Expressions;

namespace AuthenLearn.Applications.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
    int skip,
    int take,
    Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null);
    }
}
