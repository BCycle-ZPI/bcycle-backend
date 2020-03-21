using System;

namespace bcycle_backend.Models.Dto
{
    public class TripPointDto
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime TimeReached { get; set; }

        public TripPoint AsTripPoint(int tripId = 0) =>
            new TripPoint {TripId = tripId, Latitude = Latitude, Longitude = Longitude, TimeReached = TimeReached};
    }
}
