namespace BicycleStation.Dtos
{
    public class CreateBicyclePumpDTO
    {
        public int Id { get; set; }
        public int RepairStationId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
    }
}
