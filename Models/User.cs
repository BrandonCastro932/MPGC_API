using System;
using System.Collections.Generic;

#nullable disable

namespace MPGC_API.Models
{
    public partial class User
    {
        public User()
        {
            UserGames = new HashSet<UserGame>();
        }

        public int Iduser { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public int IduserStatus { get; set; }

        public virtual UserStatus IduserStatusNavigation { get; set; }
        public virtual ICollection<UserGame> UserGames { get; set; }
    }
}
