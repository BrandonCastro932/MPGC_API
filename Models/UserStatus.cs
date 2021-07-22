using System;
using System.Collections.Generic;

#nullable disable

namespace MPGC_API.Models
{
    public partial class UserStatus
    {
        public UserStatus()
        {
            Users = new HashSet<User>();
        }

        public int IduserStatus { get; set; }
        public string Status { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
