namespace PlaceService.Domain.Entities
{
    public class ProductionRoomEntity
    {
        public int Id { get; set; }
        public double Code { get; set; }
        public string Name { get; set; }
        public double NormativeArea { get; set; }

        public List<EquipmentContractEntity> EquipmentContracts { get; set; }
    }
}
