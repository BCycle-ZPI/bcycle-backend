using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bcycle_backend.Models
{
    public class GroupTrip {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string TripCode { get; set; }
        public string MapImageUrl { get; set; }
        public int HostID { get; set; }
        //public int StartPointID {get; set; }
        //public int EndPointID { get; set; }

        public User Host { get; set; }
        //public GroupTripPoint StartPoint { get; set; }
        //public GroupTripPoint EndPoint { get; set; }
        public List<GroupTripPoint> GroupTripPoints { get; set; }
        public List<GroupTripParticipant> Participants { get; set; }
    }
}
