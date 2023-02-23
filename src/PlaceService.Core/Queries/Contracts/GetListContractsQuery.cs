using MediatR;
using PlaceService.Application.Dtos;
using PlaceService.Application.Interfaces;
using PlaceService.Application.ResponseWrapper;

namespace PlaceService.Application.Queries.Contracts
{
    public class GetListContractsQuery : IRequest<Response<List<EquipmentContractDto>>>, IPaginationOptions
    {
        public int Skip { get; set; }
        public int Take { get; set; } = 10;
    }
}
