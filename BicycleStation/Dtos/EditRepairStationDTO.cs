namespace BicycleStation.Dtos
{
    public class EditRepairStationDTO
    {
        public int Capacity { get; set; }
        public int Coordinates { get; set; }
        public int VisitorCapacity { get; set; }
        public bool HasTools { get; set; }
    }
}
