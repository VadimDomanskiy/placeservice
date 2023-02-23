namespace PlaceService.Domain.Entities
{
    public class EquipmentContractEntity
    {
        public int Id { get; set; }
        public int ProductionRoomId { get; set; }
        public ProductionRoomEntity ProductionRoom { get; set; }
        public int EquipmentTypeId { get; set; }
        public EquipmentTypeEntity EquipmentType { get; set; }
        public int EquipmentCount { get; set; }
    }
}
