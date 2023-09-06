using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace trips_and_travel_system.Models
{
    public class User
    {
        public User()
        {
            SavedPosts = new HashSet<Post>();
            likedPosts = new HashSet<Post>();
            dislikedPosts = new HashSet<Post>();
        }
        public int UserId { set; get; }
        public string username { set; get; }
        public string email { set; get; }
        public string password { set; get; }
        public string firstName { set; get; }
        public string lastName { set; get; }
        public string phone { set; get; }
        public string photo { set; get; }

        public int roleId { set; get; }
        public virtual Role role { set; get; }

        public virtual Agency agency { set; get; }

        public ICollection<Post> SavedPosts { set; get; }
        public ICollection<Post> likedPosts { set; get; }
        public ICollection<Post> dislikedPosts { set; get; }
    }
}