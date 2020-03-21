using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcycle_backend.Models.Dto
{
    public class TripDto
    {
        public int ID { get; set; }
        public double Distance { get; set; }
        public int Time { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public string MapImageUrl { get; set; }
        public int? GroupTripID { get; set; }
        public List<TripPointDto> TripPoints { get; set; } = new List<TripPointDto>();
        public List<string> TripPhotos { get; set; } = new List<string>();

        public TripDto()
        {

        }

        public TripDto(Trip trip)
        {
            this.ID = trip.ID;
            this.Distance = trip.Distance;
            this.Time = trip.Time;
            this.Started = trip.Started;
            this.Finished = trip.Finished;
            this.MapImageUrl = trip.MapImageUrl;
            this.GroupTripID = trip.GroupTripID;
            this.TripPoints = trip.TripPoints.Select(tp => new TripPointDto(tp)).ToList();
            this.TripPhotos = trip.TripPhotos.Select(ph => ph.PhotoUrl).ToList();
        }

        public Trip AsTrip()
        {
            return new Trip
            {
                ID = ID, Distance = Distance, Time = Time, Started = Started, Finished = Finished, MapImageUrl = MapImageUrl, GroupTripID = GroupTripID,
                TripPoints = TripPoints.Select(tp => tp.AsTripPoint(ID)).ToList(), TripPhotos = TripPhotos.Select(ph => new TripPhoto
                {
                    TripID = ID, PhotoUrl = ph
                }).ToList()
            };
        }
    }
}
