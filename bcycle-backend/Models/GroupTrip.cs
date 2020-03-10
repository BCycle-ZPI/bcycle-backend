using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcycle_backend.Models
{
    public class GroupTrip
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Description { get; set; }
        public String TripCode { get; set; }
        public String MapImageUrl { get; set; }
        public User Host { get; set; }
        public GroupTripPoint StartPoint{ get; set; }
        public GroupTripPoint EndPoint{ get; set; }
        public List<GroupPointParticipant> Participants { get; set; }
    }
}
