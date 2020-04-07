using System;
using System.Collections.Generic;
using bcycle_backend.Models.Entities;

namespace bcycle_backend.Models.Requests
{
    public class GroupTripRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<GroupTripPoint> Route { get; set; }
    }
}
