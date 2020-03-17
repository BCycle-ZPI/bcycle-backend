using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcycle_backend.Models
{
    public class TripPoint
    {
        public int ID { get; set; }
        public int TripID { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime TimeReached { get; set; }

        public Trip Trip { get; set; }
    }
}
