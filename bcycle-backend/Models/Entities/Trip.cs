using System;
using System.Collections.Generic;
using System.Linq;
using bcycle_backend.Models.Responses;

namespace bcycle_backend.Models.Entities
{
    public class Trip
    {
        public int Id { get; set; }
        public double Distance { get; set; }
        public int Time { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public Guid? SharingGuid { get; set; }
        public string UserId { get; set; }
        public int? GroupTripId { get; set; }

        public GroupTrip GroupTrip { get; set; }
        public List<TripPoint> Route { get; set; }
        public List<TripPhoto> Photos { get; set; }

        // public string MapImageUrl { get; set; }

        public TripResponse AsResponse() => new TripResponse
        {
            Id = Id,
            Distance = Distance,
            Time = Time,
            Started = Started,
            Finished = Finished,
            SharingGuid = SharingGuid,
            GroupTripId = GroupTripId,
            Route = Route,
            Photos = Photos.Select(p => p.PhotoUrl).ToList()
        };
    }
}
