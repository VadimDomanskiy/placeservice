using Microsoft.EntityFrameworkCore;
using PlaceService.Application.Interfaces;
using PlaceService.Application.Interfaces.Repositories;
using PlaceService.Domain.Entities;
using PlaceService.Infrastructure.Persistance.Context;
using System.Linq.Expressions;

namespace PlaceService.Infrastructure.Repositories
{
    public class ContractRepository : BaseRepository<EquipmentContractEntity>, IContractRepository
    {
        public override IUnitOfWork UnitOfWork { get; protected set; }
        public ContractRepository(ApplicationDbContext context) : base(context)
        {
            UnitOfWork = context;
        }

        public async Task<EquipmentContractEntity[]> GetAsync(Expression<Func<EquipmentContractEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await DbSet.AsNoTracking()
                              .Include(x => x.EquipmentType)
                              .Include(x => x.ProductionRoom)
                              .Where(expression)
                              .ToArrayAsync(cancellationToken);
        }

        public async Task<List<EquipmentContractEntity>> GetAsync(IPaginationOptions paginationOptions, CancellationToken cancellationToken = default)
        {
            return await DbSet.Include(x => x.EquipmentType)
                              .Include(x => x.ProductionRoom)
                              .Skip(paginationOptions.Skip)
                              .Take(paginationOptions.Take)
                              .ToListAsync(cancellationToken);
        }

        public async override Task<EquipmentContractEntity> GetByIdAsync(int key, CancellationToken cancellationToken = default)
        {
            return await DbSet.Include(x => x.EquipmentType)
                              .Include(x => x.ProductionRoom)
                              .FirstOrDefaultAsync(x => x.Id == key, cancellationToken);
        }
    }

}

