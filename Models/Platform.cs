using System;
using System.Collections.Generic;

#nullable disable

namespace MPGC_API.Models
{
    public partial class Platform
    {
        public Platform()
        {
            GamePlatforms = new HashSet<GamePlatform>();
        }

        public int Idplatform { get; set; }
        public string Platform1 { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<GamePlatform> GamePlatforms { get; set; }
    }
}
