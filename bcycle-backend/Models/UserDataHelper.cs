using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcycle_backend.Models
{
    public class UserDataHelper
    {
        public static async Task<int?> GetCurrentUserID()
        {
            // TODO placeholder
            await Task.Delay(1);
            return 0;
        }

        public static async Task<User> GetCurrentUserDetails()
        {
            int? uid = await GetCurrentUserID();
            if (uid == null) return null;
            return await GetUserDetails(uid.Value);
        }
        
        public static async Task<User> GetUserDetails(int id)
        {
            // TODO placeholder
            await Task.Delay(1);
            return null;
        }

        public static async Task<List<User>> GetUserDetails(List<int> ids)
        {
            List<User> users = new List<User>(ids.Count);
            foreach (int id in ids)
            {
                users.Add(await GetUserDetails(id));
            }
            return users;
        }
    }
}
