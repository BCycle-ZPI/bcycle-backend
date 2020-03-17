using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcycle_backend.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhotoUrl { get; set; }

        public List<Trip> Trips { get; set; }
        public List<GroupTrip> HostedGroupTrips { get; set; }
        public List<GroupTripParticipant> GroupTripParticipations { get; set; }
    }
}
