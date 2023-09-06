using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trips_and_travel_system.Controllers
{
    public class Class1
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email_address { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public string phone { get; set; }
        public string photo { get; set; }
        public int role { get; set; }
    }
    public class Class2
    {
        public string agency_name { get; set; }
    }
    public class Class3
    {
        public string title { get; set; }
        public string details { get; set; }
        public string trip_destination { get; set; }
        public string trip_price { get; set; }
        public string trip_date { get; set; }
        public string post_date { get; set; }
        public bool accepted { get; set; }
    }
    public class Class4
    {
        public string question { get; set; }
        public string answer { get; set; }
    }
}