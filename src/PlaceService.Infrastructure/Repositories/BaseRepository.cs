using Microsoft.EntityFrameworkCore;
using PlaceService.Application.Interfaces;
using PlaceService.Application.Interfaces.Repositories;
using System.Linq.Expressions;

namespace PlaceService.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public abstract IUnitOfWork UnitOfWork { get; protected set; }
        public BaseRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual ValueTask<TEntity> FindAsync(int id, CancellationToken cancellationToken = default) => DbSet.FindAsync(new object[] { id }, cancellationToken);
        public virtual Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default) => DbSet.AnyAsync(expression, cancellationToken);
        public virtual Task<TEntity[]> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default) => DbSet.AsNoTracking().Where(expression).ToArrayAsync(cancellationToken);
        public virtual async ValueTask InsertAsync(TEntity item, CancellationToken cancellationToken = default) => await DbSet.AddAsync(item, cancellationToken);
        public virtual Task InsertRangeAsync(IEnumerable<TEntity> items, CancellationToken cancellationToken = default) => DbSet.AddRangeAsync(items, cancellationToken);
        public virtual Task<TEntity> GetByIdAsync(int key, CancellationToken cancellationToken = default) => FindAsync(key, cancellationToken).AsTask();
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default) => DbSet.FirstOrDefaultAsync(expression, cancellationToken);
    }
}
