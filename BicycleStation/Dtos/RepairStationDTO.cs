namespace BicycleStation.Dtos
{
    public class RepairStationDTO
    {
        public int Capacity { get; set; }
        public int VisitorCapacity { get; set; }
        public bool HasPump { get; set; }
        public bool HasTools { get; set; }
    }
}
