using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bcycle_backend.Models.Dto;

namespace bcycle_backend.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public double Distance { get; set; }
        public int Time { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public string MapImageUrl { get; set; }
        public string UserId { get; set; }
        public int? GroupTripId { get; set; }

        public GroupTrip GroupTrip { get; set; }
        public List<TripPoint> TripPoints { get; set; }
        public List<TripPhoto> TripPhotos { get; set; }

        public TripDto AsDto() => new TripDto
        {
            Id = Id,
            Distance = Distance,
            Time = Time,
            Started = Started,
            Finished = Finished,
            MapImageUrl = MapImageUrl,
            GroupTripId = GroupTripId,
            TripPoints = TripPoints.Select(tp => tp.AsDto()).ToList(),
            TripPhotos = TripPhotos.Select(ph => ph.PhotoUrl).ToList()
        };
    }
}
