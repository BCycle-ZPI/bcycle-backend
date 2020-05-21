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

        public string GetSharingUrl(string urlBase, string tripSharePrefix) =>
            SharingGuid == null ? null : $"{urlBase}/{tripSharePrefix}/{SharingGuid}";

        public TripResponse AsResponse(string urlBase, string tripSharePrefix) => new TripResponse
        {
            Id = Id,
            Distance = Distance,
            Time = Time,
            Started = Started,
            Finished = Finished,
            SharingUrl = GetSharingUrl(urlBase, tripSharePrefix),
            GroupTripId = GroupTripId,
            Route = Route,
            Photos = Photos.Select(p => p.PhotoUrl).ToList()
        };
    }
}
