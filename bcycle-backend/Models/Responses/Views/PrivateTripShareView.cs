using System.Collections.Generic;
using bcycle_backend.Models.Entities;

namespace bcycle_backend.Models.Responses.Views
{
    public class PrivateTripShareView
    {
        public Person Subject { get; set; }
        public Measures Measures { get; set; }
        public IEnumerable<string> PhotosUrls { get; set; }
        public IEnumerable<TripPoint> Route { get; set; }
    }
}
