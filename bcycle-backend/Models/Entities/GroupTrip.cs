using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using bcycle_backend.Models.Requests;
using bcycle_backend.Models.Responses;

namespace bcycle_backend.Models.Entities
{
    public class GroupTrip
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [MaxLength(255)]
        public string HostId { get; set; }
        [Required]
        [MaxLength(6)]
        public string TripCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<GroupTripPoint> Route { get; set; }
        public List<GroupTripParticipant> Participants { get; set; }
        public List<Trip> Trips { get; set; }

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

        public async Task<GroupTripResponse> AsResponseAsync(Func<string, Task<UserInfo>> userProvider) =>
            new GroupTripResponse
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Host = await userProvider(HostId),
                TripCode = TripCode,
                StartDate = StartDate,
                EndDate = EndDate,
                Route = Route,
                Participants = Participants
                    .Select(p => p.AsResponseAsync(userProvider))
                    .Select(t => t.Result)
                    .ToList(),
                Photos = Trips == null ? new List<string>() : Trips.SelectMany(t => t.Photos).Select(p => p.PhotoUrl)
            };
    }
}
