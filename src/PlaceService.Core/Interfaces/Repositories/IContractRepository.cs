using PlaceService.Domain.Entities;
using System.Linq.Expressions;

namespace PlaceService.Application.Interfaces.Repositories
{
    public interface IContractRepository : IBaseRepository<EquipmentContractEntity>
    {
        Task<EquipmentContractEntity[]> GetAsync(Expression<Func<EquipmentContractEntity, bool>> expression, CancellationToken cancellationToken = default);
        Task<List<EquipmentContractEntity>> GetAsync(IPaginationOptions paginationOptions, CancellationToken cancellationToken = default);
    }
}
