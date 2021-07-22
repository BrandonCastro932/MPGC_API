using System;
using System.Collections.Generic;

#nullable disable

namespace MPGC_API.Models
{
    public partial class UserGame
    {
        public int IduserGame { get; set; }
        public int IdgameState { get; set; }
        public int Idgame { get; set; }
        public int Iduser { get; set; }

        public virtual Game IdgameNavigation { get; set; }
        public virtual GameState IdgameStateNavigation { get; set; }
        public virtual User IduserNavigation { get; set; }
    }
}
