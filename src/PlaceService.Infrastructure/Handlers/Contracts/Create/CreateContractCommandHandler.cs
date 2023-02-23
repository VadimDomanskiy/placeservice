using AutoMapper;
using MediatR;
using PlaceService.Application.Command.Create;
using PlaceService.Application.Dtos;
using PlaceService.Application.Interfaces.Repositories;
using PlaceService.Application.ResponseWrapper;
using PlaceService.Domain.Entities;
using PlaceService.Infrastructure.Repositories;

namespace PlaceService.Infrastructure.Handlers.Contracts.Create
{
    public class CreateContractCommandHandler : IRequestHandler<CreateContractCommand, Response<EquipmentContractDto>>
    {
        private readonly IMapper _mapper;
        private readonly IContractRepository _contractRepository;
        private readonly IEquipmentTypeRepository _equipmentTypeRepository;
        private readonly IProductionRoomRepository _productionRoomRepository;

        public CreateContractCommandHandler(IMapper mapper,
                                            IContractRepository contractRepository,
                                            IEquipmentTypeRepository equipmentTypeRepository,
                                            IProductionRoomRepository productionRoomRepository)
        {
            _mapper = mapper;
            _contractRepository = contractRepository;
            _equipmentTypeRepository = equipmentTypeRepository;
            _productionRoomRepository = productionRoomRepository;
        }
        public async Task<Response<EquipmentContractDto>> Handle(CreateContractCommand command, CancellationToken cancellationToken)
        {
            if (command.EquipmentCount == 0)
                return Response<EquipmentContractDto>.CreateError
                    ("The number of equipment cannot be 0.");

            var roomEntity = await _productionRoomRepository.FirstOrDefaultAsync(x => x.Code == command.RoomCode, cancellationToken);
            if (roomEntity == null)
                return Response<EquipmentContractDto>.CreateError
                    ($"Production room with code: {command.RoomCode} does not exist.");

            var equipmentTypeEntity = await _equipmentTypeRepository.FirstOrDefaultAsync(x => x.Code == command.EquipmentCode, cancellationToken);
            if (equipmentTypeEntity == null)
                return Response<EquipmentContractDto>.CreateError
                    ($"Equipment type with code {command.EquipmentCode} does not exist.");

            var existingContracts = await _contractRepository.GetAsync(ec => ec.ProductionRoomId == command.RoomCode, cancellationToken);

            var totalAreaTaken = existingContracts.Sum(ec => ec.EquipmentType.Area * ec.EquipmentCount);
            if (totalAreaTaken + equipmentTypeEntity.Area * command.EquipmentCount > roomEntity.NormativeArea)
                return Response<EquipmentContractDto>.CreateError
                    ($"Not enough free area in production room with ID {command.RoomCode}.");

            var contractEntity = new EquipmentContractEntity 
            {
                ProductionRoomId = roomEntity.Id,
                EquipmentTypeId = equipmentTypeEntity.Id,
                EquipmentCount = command.EquipmentCount
            };
  
            await _contractRepository.InsertAsync(contractEntity);
            await _contractRepository.UnitOfWork.SaveChangesAsync();
            
            return Response<EquipmentContractDto>.Create(_mapper.Map<EquipmentContractDto>
                (await _contractRepository.GetByIdAsync(contractEntity.Id, cancellationToken)));
        }
    }
}


