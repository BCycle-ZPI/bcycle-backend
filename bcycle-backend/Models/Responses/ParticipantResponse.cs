using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using bcycle_backend.Models.Entities;

namespace bcycle_backend.Models.Responses
{
    public class ParticipantResponse
    {
        public UserInfo User { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ParticipantStatus Status { get; set; }
    }
}
