using AutoMapper;
using PlaceService.Application.Dtos;
using PlaceService.Domain.Entities;

namespace PlaceService.Application.MapperProfiles.ContractMapperProfile
{
    public class ContractProfile : Profile
    {
        public ContractProfile()
        {
            CreateMap<EquipmentContractEntity, EquipmentContractDto>()
                .ForMember(e => e.RoomName, c => c.MapFrom(c => c.ProductionRoom.Name))
                .ForMember(e => e.EquipmentName, c => c.MapFrom(c => c.EquipmentType.Name))
                .ForMember(e => e.EquipmentCount, c => c.MapFrom(c => c.EquipmentCount));
        }
    }
}
