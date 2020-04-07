using System;
using System.Collections.Generic;
using bcycle_backend.Models.Entities;

namespace bcycle_backend.Models.Requests
{
    public class TripRequest
    {
        public double Distance { get; set; }
        public int Time { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public int? GroupTripId { get; set; }
        public List<TripPoint> Route { get; set; }
    }
}
