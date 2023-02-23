using MediatR;
using PlaceService.Application.Dtos;
using PlaceService.Application.ResponseWrapper;
using System.ComponentModel.DataAnnotations;

namespace PlaceService.Application.Command.Create
{
    public class CreateContractCommand : IRequest<Response<EquipmentContractDto>>
    {
        [Required]
        public double RoomCode { get; set; }
        [Required]
        public double EquipmentCode { get; set; }
        [Required]
        public int EquipmentCount { get; set; }
    }
}
