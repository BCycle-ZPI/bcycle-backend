using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcycle_backend.Models
{
    public class Trip
    {
        public int ID { get; set; }
        public float Distance{ get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public String MapImageUrl { get; set; }
        public User User { get; set; }
        public GroupTrip GroupTrip { get; set; }
        public TripPoint StartPoint { get; set; }
        public TripPoint EndPoint { get; set; }
        public List<TripPoint> TripPoints{ get; set; }
    }
}
