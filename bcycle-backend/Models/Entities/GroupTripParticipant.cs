using System;
using System.Threading.Tasks;
using bcycle_backend.Models.Responses;

namespace bcycle_backend.Models.Entities
{
    public class GroupTripParticipant
    {
        public int GroupTripId { get; set; }
        public string UserId { get; set; }
        public ParticipantStatus Status { get; set; }
        public GroupTrip GroupTrip { get; set; }

        public async Task<ParticipantResponse> AsResponseAsync(Func<string, Task<UserInfo>> userProvider) =>
            new ParticipantResponse {User = await userProvider(UserId), Status = Status};
    }
}
