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
        public Guid? SharingGuid { get; set; }
        public List<GroupTripPoint> Route { get; set; }
        public List<ParticipantResponse> Participants { get; set; }
        public List<string> Photos { get; set; }
    }
}
