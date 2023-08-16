namespace BicycleStation.Dtos
{
    public class GetRepairStationDTO
    {
        public int Coordinates { get; set; }
        public int Capacity { get; set; }
        public int VisitorCapacity { get; set; }
        public ICollection<BicyclePumpDTO> BicyclePumps { get; set; }
        public bool HasTools { get; set; }
    }
}
