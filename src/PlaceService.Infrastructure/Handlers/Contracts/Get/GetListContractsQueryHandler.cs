using AutoMapper;
using MediatR;
using PlaceService.Application.Dtos;
using PlaceService.Application.Interfaces.Repositories;
using PlaceService.Application.Queries.Contracts;
using PlaceService.Application.ResponseWrapper;
using PlaceService.Infrastructure.Repositories;
using System.Collections.Generic;

namespace PlaceService.Infrastructure.Handlers.Contracts.Get
{
    public class GetListContractsQueryHandler : IRequestHandler<GetListContractsQuery, Response<List<EquipmentContractDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IContractRepository _contractRepository;

        public GetListContractsQueryHandler(IMapper mapper,
                                            IContractRepository contractRepository)
        {
            _mapper = mapper;
            _contractRepository = contractRepository;
        }

        public  async Task<Response<List<EquipmentContractDto>>> Handle(GetListContractsQuery query, CancellationToken cancellationToken)
        {
            var contractsEntity = await _contractRepository.GetAsync(query, cancellationToken);

            return Response<List<EquipmentContractDto>>.Create(_mapper.Map<List<EquipmentContractDto>>(contractsEntity));
        }
    }
}
