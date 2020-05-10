using Newtonsoft.Json;

namespace bcycle_backend.Models.Entities
{
    public class GroupTripPoint
    {
        [JsonIgnore] public int Id { get; set; }
        [JsonIgnore] public int GroupTripId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Ordinal { get; set; }

        [JsonIgnore] public GroupTrip GroupTrip { get; set; }
    }
}
