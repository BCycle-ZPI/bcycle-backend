using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcycle_backend.Models
{
    public class TripPhoto
    {
        public int ID { get; set; }
        public string PhotoUrl { get; set; }
        public int TripID { get; set; }

        public Trip Trip { get; set; }
    }
}
