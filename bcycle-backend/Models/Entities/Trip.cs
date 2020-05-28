using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using bcycle_backend.Models.Responses;
using static bcycle_backend.ProjectConstants;

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
        [Required]
        [MaxLength(255)]
        public string UserId { get; set; }
        public int? GroupTripId { get; set; }

        public GroupTrip GroupTrip { get; set; }
        public List<TripPoint> Route { get; set; }
        public List<TripPhoto> Photos { get; set; }

        public TripResponse AsResponse(string urlBase) => new TripResponse
        {
            Id = Id,
            Distance = Distance,
            Time = Time,
            Started = Started,
            Finished = Finished,
            SharingUrl = GetSharingUrl(urlBase),
            GroupTripId = GroupTripId,
            Route = Route,
            Photos = Photos.Select(p => p.PhotoUrl).ToList()
        };

        public string GetSharingUrl(string urlBase)
        {
            var tripTypeSegment = GroupTripId == null ? PrivateTripShareSegment : GroupTripShareSegment;
            return SharingGuid == null ? null : $"{urlBase}/{tripTypeSegment}/{SharingGuid}";
        }
    }
}
