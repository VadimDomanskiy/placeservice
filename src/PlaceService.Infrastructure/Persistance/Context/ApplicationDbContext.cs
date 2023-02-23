using Microsoft.EntityFrameworkCore;
using PlaceService.Application.Interfaces;
using PlaceService.Domain.Entities;

namespace PlaceService.Infrastructure.Persistance.Context
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<EquipmentContractEntity> Contracts { get; set; }
        public DbSet<EquipmentTypeEntity> EquipmentTypes { get; set; }
        public DbSet<ProductionRoomEntity> Rooms { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
