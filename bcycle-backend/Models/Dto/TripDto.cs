using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcycle_backend.Models.Dto
{
    public class TripDto
    {
        public int Id { get; set; }
        public double Distance { get; set; }
        public int Time { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public string MapImageUrl { get; set; }
        public int? GroupTripId { get; set; }
        public List<TripPointDto> TripPoints { get; set; } = new List<TripPointDto>();
        public List<string> TripPhotos { get; set; } = new List<string>();
        
        public Trip AsTrip() => new Trip
        {
            Id = Id,
            Distance = Distance,
            Time = Time,
            Started = Started,
            Finished = Finished,
            MapImageUrl = MapImageUrl,
            GroupTripId = GroupTripId,
            TripPoints = TripPoints.Select(tp => tp.AsTripPoint(Id)).ToList(),
            TripPhotos = TripPhotos.Select(ph => new TripPhoto {TripId = Id, PhotoUrl = ph}).ToList()
        };
    }
}
