using System;
using System.Collections.Generic;

#nullable disable

namespace MPGC_API.Models
{
    public partial class GamePlatform
    {
        public int IdgamePlatform { get; set; }
        public int PlatformsIdplatform { get; set; }
        public int GameIdgame { get; set; }

        public virtual Game GameIdgameNavigation { get; set; }
        public virtual Platform PlatformsIdplatformNavigation { get; set; }
    }
}
