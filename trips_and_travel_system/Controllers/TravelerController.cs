using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using trips_and_travel_system.Models;

namespace trips_and_travel_system.Controllers
{
    public class TravelerController : Controller
    {
        private TripsAndTravelContext db = new TripsAndTravelContext();
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }
    }
}