using FirebaseAdmin.Auth;

namespace bcycle_backend.Models.Responses
{
    public class UserInfo
    {
        public string Id { get; }
        public string DisplayName { get; }
        public string Email { get; }
        public string PhotoUrl { get; }

        public UserInfo(IUserInfo data)
        {
            Id = data.Uid;
            DisplayName = data.DisplayName;
            Email = data.Email;
            PhotoUrl = data.PhotoUrl;
        }
    }
}
