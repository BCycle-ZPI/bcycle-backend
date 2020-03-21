using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bcycle_backend.Models.Dto;

namespace bcycle_backend.Models
{
    public class Trip
    {
        public int ID { get; set; }
        public double Distance { get; set; }
        public int Time { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public string MapImageUrl { get; set; }
        public int UserID { get; set; }
        public int? GroupTripID { get; set; }
        //public int StartPointID { get; set; }
        //public int EndPointID { get; set; }

        public User User { get; set; }
        public GroupTrip GroupTrip { get; set; }
        //public TripPoint StartPoint { get; set; }
        //public TripPoint EndPoint { get; set; }
        public List<TripPoint> TripPoints { get; set; }
        public List<TripPhoto> TripPhotos { get; set; }

        public TripDto AsDto()
        {
            return new TripDto(this);
        }
    }
}
