using System;
using System.Collections.Generic;

#nullable disable

namespace MPGC_API.Models
{
    public partial class Game
    {
        public Game()
        {
            GamePlatforms = new HashSet<GamePlatform>();
            GameScreenshots = new HashSet<GameScreenshot>();
            UserGames = new HashSet<UserGame>();
        }

        public int Idgame { get; set; }
        public string Name { get; set; }
        public DateTime Released { get; set; }
        public string BackgroundImage { get; set; }
        public decimal Rating { get; set; }
        public decimal Playtime { get; set; }
        public string Description { get; set; }
        public string UrlMusicTheme { get; set; }
        public int Idgenre { get; set; }

        public virtual Genre IdgenreNavigation { get; set; }
        public virtual ICollection<GamePlatform> GamePlatforms { get; set; }
        public virtual ICollection<GameScreenshot> GameScreenshots { get; set; }
        public virtual ICollection<UserGame> UserGames { get; set; }
    }
}
