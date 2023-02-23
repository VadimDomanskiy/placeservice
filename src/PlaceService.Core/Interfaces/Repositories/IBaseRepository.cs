using System.Collections.Generic;
using System.Linq.Expressions;

namespace PlaceService.Application.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity[]> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        ValueTask<TEntity> FindAsync(int id, CancellationToken cancellationToken = default);
        ValueTask InsertAsync(TEntity item, CancellationToken cancellationToken = default);
        Task InsertRangeAsync(IEnumerable<TEntity> items, CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsync(int key, CancellationToken cancellationToken = default);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        IUnitOfWork UnitOfWork { get; }
    }
}
