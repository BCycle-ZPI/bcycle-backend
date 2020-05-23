using System;
using System.Collections.Generic;
using bcycle_backend.Models.Entities;

namespace bcycle_backend.Models.Responses
{
    public class GroupTripResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public UserInfo Host { get; set; }
        public string TripCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<GroupTripPoint> Route { get; set; }
        public IEnumerable<ParticipantResponse> Participants { get; set; }
        public IEnumerable<string> Photos { get; set; }
    }
}
