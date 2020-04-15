using System;
using Newtonsoft.Json;

namespace bcycle_backend.Models.Entities
{
    public class TripPoint
    {
        [JsonIgnore] public int Id { get; set; }
        [JsonIgnore] public int TripId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime TimeReached { get; set; }

        [JsonIgnore] public Trip Trip { get; set; }
    }
}
