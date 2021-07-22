using System;
using System.Collections.Generic;

#nullable disable

namespace MPGC_API.Models
{
    public partial class GameState
    {
        public GameState()
        {
            UserGames = new HashSet<UserGame>();
        }

        public int IdgameState { get; set; }
        public string StateName { get; set; }

        public virtual ICollection<UserGame> UserGames { get; set; }
    }
}
