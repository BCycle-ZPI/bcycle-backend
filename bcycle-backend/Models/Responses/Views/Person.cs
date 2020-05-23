namespace bcycle_backend.Models.Responses.Views
{
    public class Person
    {
        public string FullName { get; }
        public string AvatarUrl { get; }

        public Person(UserInfo userInfo)
        {
            FullName = userInfo.DisplayName;
            AvatarUrl = userInfo.PhotoUrl;
        }

        public Person(string fullName)
        {
            FullName = fullName;
            AvatarUrl = string.Empty;
        }
    }
}
