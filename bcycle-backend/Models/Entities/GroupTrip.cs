using System;
using System.Collections.Generic;
using System.Linq;
using bcycle_backend.Models.Requests;

namespace bcycle_backend.Models.Entities
{
    public class GroupTrip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HostId { get; set; }
        public string TripCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<GroupTripPoint> Route { get; set; }
        public List<GroupTripParticipant> Participants { get; set; }

        public void Update(GroupTripRequest data)
        {
            Name = data.Name;
            Description = data.Description;
            StartDate = data.StartDate;
            EndDate = data.EndDate;
            Route = data.Route;
        }

        public GroupTripParticipant FindAmongPending(string userId) => FindAmong(userId, ParticipantStatus.Pending);

        public GroupTripParticipant FindAmongAccepted(string userId) => FindAmong(userId, ParticipantStatus.Accepted);

        private GroupTripParticipant FindAmong(string userId, ParticipantStatus status) =>
            Participants
                .Where(p => p.Status == status)
                .FirstOrDefault(r => r.UserId == userId);
    }
}
