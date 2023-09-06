using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace trips_and_travel_system.Models
{
    public class FAQ
    {
        public FAQ()
        {
            
        }

        public int FAQId { set; get; }
        public string question { set; get; }
        public string answer { set; get; }

        public int postId { set; get; }
        public virtual Post post { set; get; }
    }
}