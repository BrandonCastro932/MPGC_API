using System;
using System.Collections.Generic;

#nullable disable

namespace MPGC_API.Models
{
    public partial class GameScreenshot
    {
        public int Idscreenshot { get; set; }
        public string Sslink { get; set; }
        public int Idgame { get; set; }

        public virtual Game IdgameNavigation { get; set; }
    }
}
