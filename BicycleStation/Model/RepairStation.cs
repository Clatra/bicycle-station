namespace BicycleStation.Model
{
    // POCO - plain old class object
    public class RepairStation
    {
        public int Id { get; set; }
        public int Coordinates { get; set; }
        public int Capacity { get; set; }
        public int VisitorCapacity { get; set; }
        public ICollection<BicyclePump> BicyclePumps { get; } = new List<BicyclePump>(); 
        public bool HasTools { get; set; }
    }
}
