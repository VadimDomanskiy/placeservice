using Microsoft.Extensions.DependencyInjection;
using PlaceService.Application.Interfaces;
using PlaceService.Application.Interfaces.Repositories;
using PlaceService.Domain.Entities;

namespace PlaceService.Infrastructure.Persistance.Seeds
{
    public class SeedData
    {
        public static async Task EnsureDataSeeded(IServiceProvider serviceProvider) 
        {
            await EnsureDataTablesSeeded(serviceProvider);
        }
        private static async Task EnsureDataTablesSeeded(IServiceProvider serviceProvider)
        {
            var roomRepository = serviceProvider.GetRequiredService<IProductionRoomRepository>();
            var equipmentRepository = serviceProvider.GetRequiredService<IEquipmentTypeRepository>();
            var contractsRepository = serviceProvider.GetRequiredService<IContractRepository>();

            if (!await roomRepository.ExistAsync(x => true))
            {
                var rooms = new List<ProductionRoomEntity>();
                var equipments = new List<EquipmentTypeEntity>();
                var contracts = new List<EquipmentContractEntity>();

                for (int i = 1; i <= 5; i++)
                {
                    rooms.Add(
                        new ProductionRoomEntity
                        {
                            Code = i,
                            Name = $"Production Room {i}",
                            NormativeArea = i * 200
                        }
                    );

                    equipments.Add(
                        new EquipmentTypeEntity
                        {
                            Code = i,
                            Name = $"Equipment Type {i}",
                            Area = i * 10
                        });

                    contracts.Add(
                        new EquipmentContractEntity
                        {
                            ProductionRoom = rooms[i-1],
                            EquipmentType = equipments[i-1],
                            EquipmentCount = i * 2
                        });

                }

                await roomRepository.InsertRangeAsync(rooms);
                await roomRepository.UnitOfWork.SaveEntitiesAsync();
                await equipmentRepository.InsertRangeAsync(equipments);
                await equipmentRepository.UnitOfWork.SaveEntitiesAsync();
                await contractsRepository.InsertRangeAsync(contracts);
                await contractsRepository.UnitOfWork.SaveEntitiesAsync();
            }
        }
    }
}

