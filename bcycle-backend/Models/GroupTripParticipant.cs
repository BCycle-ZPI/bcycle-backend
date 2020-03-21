using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcycle_backend.Models
{
    public class GroupTripParticipant
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; } = false;
        public int GroupTripId { get; set; }
        public string UserId { get; set; }

        public GroupTrip GroupTrip { get; set; }
    }
}
