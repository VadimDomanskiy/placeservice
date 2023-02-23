using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlaceService.Application.Command.Create;
using PlaceService.Application.Queries.Contracts;

namespace PlaceService.Api.Controllers
{
    [ApiController]
    [Route("v1/contract")]
    public class ContractController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ContractController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEquipmentContractAsync([FromBody]CreateContractCommand command, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        [HttpGet]
        public async Task<IActionResult> GetContractsAsync([FromQuery]GetListContractsQuery query, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(query, cancellationToken));
        }
    }
 }
