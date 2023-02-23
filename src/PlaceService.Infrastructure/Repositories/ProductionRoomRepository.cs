using PlaceService.Application.Interfaces;
using PlaceService.Application.Interfaces.Repositories;
using PlaceService.Domain.Entities;
using PlaceService.Infrastructure.Persistance.Context;

namespace PlaceService.Infrastructure.Repositories
{
    public class ProductionRoomRepository: BaseRepository<ProductionRoomEntity>, IProductionRoomRepository
    {
        public override IUnitOfWork UnitOfWork { get; protected set; }
        public ProductionRoomRepository(ApplicationDbContext context) : base(context)
        {
            UnitOfWork = context;
        }
    }
}
