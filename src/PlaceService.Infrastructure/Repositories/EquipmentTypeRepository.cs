using PlaceService.Application.Interfaces;
using PlaceService.Application.Interfaces.Repositories;
using PlaceService.Domain.Entities;
using PlaceService.Infrastructure.Persistance.Context;

namespace PlaceService.Infrastructure.Repositories
{
    public class EquipmentTypeRepository : BaseRepository<EquipmentTypeEntity>, IEquipmentTypeRepository
    {
        public override IUnitOfWork UnitOfWork { get; protected set; }
        public EquipmentTypeRepository(ApplicationDbContext context) : base(context)
        {
            UnitOfWork = context;
        }
    }
}
