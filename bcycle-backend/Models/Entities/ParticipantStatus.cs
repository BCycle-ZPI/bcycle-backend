using System.Runtime.Serialization;

namespace bcycle_backend.Models.Entities
{
    public enum ParticipantStatus
    {
        [EnumMember(Value = "ACCEPTED")] Accepted,
        [EnumMember(Value = "REJECTED")] Rejected,
        [EnumMember(Value = "PENDING")] Pending,
        [EnumMember(Value = "REMOVED")] Removed
    }
}
