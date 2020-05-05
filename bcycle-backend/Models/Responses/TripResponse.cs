using System;
using System.Collections.Generic;
using bcycle_backend.Models.Entities;

namespace bcycle_backend.Models.Responses
{
    public class TripResponse
    {
        public int Id { get; set; }
        public double Distance { get; set; }
        public int Time { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public Guid? SharingGuid { get; set; }
        public int? GroupTripId { get; set; }
        public List<TripPoint> Route { get; set; }
        public List<string> Photos { get; set; }
    }
}
