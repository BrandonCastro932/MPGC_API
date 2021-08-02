using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace MPGC_API.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Games = new HashSet<Game>();
        }

        public int Idgenre { get; set; }
        public string NameGenre { get; set; }
        public string GenreColor { get; set; }

        //Por el momento se ignorará ya que por el momento no se pienza filtrar por generos
        [JsonIgnore]
        public virtual ICollection<Game> Games { get; set; }
    }
}
