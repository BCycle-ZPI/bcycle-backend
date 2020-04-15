using System;
using System.Threading.Tasks;
using bcycle_backend.Models.Responses;
using FirebaseAdmin.Auth;

namespace bcycle_backend.Services
{
    public class UserService
    {
        private readonly FirebaseAuth _auth = FirebaseAuth.DefaultInstance;
        
        public async Task<UserInfo> GetUserInfoAsync(string id)
        {
            try
            {
                return new UserInfo(await _auth.GetUserAsync(id));
            }
            catch (Exception ignored)
            {
                return null;
            }
        }
    }
}
