using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HatServer.Models
{
    // Add profile data for application users by adding properties to the ServerUser class
    public class ServerUser : IdentityUser
    {
        public virtual ICollection<PhraseState> PhraseStates { get; set; }
    }
}
