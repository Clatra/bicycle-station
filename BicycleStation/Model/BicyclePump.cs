using System.Text.Json.Serialization;

namespace BicycleStation.Model
{
    public class BicyclePump
    {
        public int Id { get; set; }
        //[JsonIgnore]
        public RepairStation? RepairStation { get; set; }
        public int? RepairStationId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }

    }
}
