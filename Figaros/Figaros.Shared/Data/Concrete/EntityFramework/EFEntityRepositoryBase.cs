using Figaros.Shared.Data.Abstract;
using Figaros.Shared.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Figaros.Shared.Data.Concrete.EntityFramework
{
    public class EFEntityRepositoryBase<TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly DbContext _context;

        public EFEntityRepositoryBase(DbContext context)
        {
            _context = context;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().CountAsync(predicate);
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            await Task.Run(() => { _context.Set<TEntity>().Remove(entity); }) ;
            return entity;
        }

        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> entities = _context.Set<TEntity>();

            if (predicate != null)
            {
                entities = entities.Where(predicate);
            }

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    entities = entities.Include(includeProperty);
                }
            }

            return await entities.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> entities = _context.Set<TEntity>();

            if (predicate != null)
            {
                entities = entities.Where(predicate);
            }

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    entities = entities.Include(includeProperty);
                }
            }

            return await entities.SingleOrDefaultAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await Task.Run(() => { _context.Set<TEntity>().Update(entity); });
            return entity;
        }
    }
}
