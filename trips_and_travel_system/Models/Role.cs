using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace trips_and_travel_system.Models
{
    public class Role
    {
        public Role()
        {
            users = new HashSet<User>();
        }
        public int RoleId { set; get; }
        public string RoleName { set; get; }
        public ICollection<User> users { set; get; }
    }
}