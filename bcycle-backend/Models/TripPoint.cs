using System;
using bcycle_backend.Models.Dto;

namespace bcycle_backend.Models
{
    public class TripPoint
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime TimeReached { get; set; }

        public Trip Trip { get; set; }

        public TripPointDto AsDto() =>
            new TripPointDto {Latitude = Latitude, Longitude = Longitude, TimeReached = TimeReached};
    }
}
