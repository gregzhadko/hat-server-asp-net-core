using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Model
{
    // Add profile data for application users by adding properties to the ServerUser class
    public class ServerUser : IdentityUser
    {
        public virtual IList<ReviewState> ReviewStates { get; set; }
    }
}
