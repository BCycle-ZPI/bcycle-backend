using Newtonsoft.Json;

namespace bcycle_backend.Models.Entities
{
    public class GroupTripParticipant
    {
        [JsonIgnore] public int GroupTripId { get; set; }
        public string UserId { get; set; }
        public ParticipantStatus Status { get; set; }

        [JsonIgnore] public GroupTrip GroupTrip { get; set; }
    }
}
