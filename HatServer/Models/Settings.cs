using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatServer.Models
{
    public class Settings
    {
        public int Id { get; set; }
        public bool CanChangeWord { get; set; }
        public int RoundTime { get; set; }
        public bool BadItalicSimulated { get; set; }
    }
}
