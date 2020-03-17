using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcycle_backend.Models
{
    public class GroupTripParticipant
    {
        public int ID { get; set; }
        public bool IsApproved { get; set; } = false;
        public int GroupTripID { get; set; }
        public int UserID { get; set; }

        public GroupTrip GroupTrip { get; set; }
        public User User { get; set; }
    }
}
