using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcycle_backend.Models
{
    public class Trip
    {
        public int ID { get; set; }
        public float Distance { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public string MapImageUrl { get; set; }
        public int HostUserID { get; set; }
        public int GroupTripID { get; set; }
        //public int StartPointID { get; set; }
        //public int EndPointID { get; set; }

        public User Host { get; set; }
        public GroupTrip GroupTrip { get; set; }
        //public TripPoint StartPoint { get; set; }
        //public TripPoint EndPoint { get; set; }
        public List<TripPoint> TripPoints { get; set; }
    }
}
