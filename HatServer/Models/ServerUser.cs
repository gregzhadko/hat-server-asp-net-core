using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace HatServer.Models
{
    // Add profile data for application users by adding properties to the ServerUser class
    public class ServerUser : IdentityUser
    {
        public virtual ICollection<PhraseState> PhraseStates { get; set; }
    }
}
