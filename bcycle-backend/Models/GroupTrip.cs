using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bcycle_backend.Models
{
    public class GroupTrip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string TripCode { get; set; }
        public string MapImageUrl { get; set; }
        public string HostId { get; set; }

        public List<GroupTripPoint> GroupTripPoints { get; set; }
        public List<GroupTripParticipant> Participants { get; set; }
    }
}
