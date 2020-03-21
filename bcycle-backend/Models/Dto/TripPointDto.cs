using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bcycle_backend.Models;

namespace bcycle_backend.Models.Dto
{
    public class TripPointDto
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime TimeReached { get; set; }

        public TripPointDto()
        {

        }

        public TripPointDto(TripPoint tp)
        {
            this.Latitude = tp.Latitude;
            this.Longitude = tp.Longitude;
            this.TimeReached = tp.TimeReached;
        }

        public TripPoint AsTripPoint(int tripID = 0)
        {
            return new TripPoint() { TripID = tripID, Latitude = Latitude, Longitude = Longitude, TimeReached = TimeReached };
        }
    }
}
