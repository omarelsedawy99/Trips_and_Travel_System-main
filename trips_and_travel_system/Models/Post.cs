using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace trips_and_travel_system.Models
{
    public class Post
    {
        public Post()
        {
            UserPosts = new HashSet<User>();
            likedTravelers = new HashSet<User>();
            dislikedTravelers = new HashSet<User>();
        }

        public int PostId { set; get; }
        public string title { set; get; }
        public string details { set; get; }
        public string tripDestination { set; get; }
        public string tripImage { set; get; }
        public float tripPrice { set; get; }
        public DateTime tripDate { set; get; }
        public DateTime postDate { set; get; }
        public bool accepted { set; get; }


        public int AgencyId { set; get; }
        public virtual Agency Agency { set; get; }

        public ICollection<User> UserPosts { set; get; }
        public ICollection<User> likedTravelers { set; get; }
        public ICollection<User> dislikedTravelers { set; get; }
        public ICollection<FAQ> fAQs { set; get; }
    }
}