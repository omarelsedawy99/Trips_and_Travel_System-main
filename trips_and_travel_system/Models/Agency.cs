using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace trips_and_travel_system.Models
{
    public class Agency
    {
        public Agency()
        {
            TripPosts = new HashSet<Post>();
        }

        public int AgencyId { set; get; }
        public string agencyName { set; get; }

        public virtual User user { set; get; }

        public ICollection<Post> TripPosts { set; get; }
    }
}