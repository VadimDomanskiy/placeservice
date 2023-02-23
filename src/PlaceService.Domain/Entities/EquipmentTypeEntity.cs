namespace PlaceService.Domain.Entities
{
    public class EquipmentTypeEntity
    {
        public int Id { get; set; }
        public double Code { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }

        public List<EquipmentContractEntity> EquipmentContracts { get; set; }
    }
}
