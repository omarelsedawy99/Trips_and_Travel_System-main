using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using trips_and_travel_system.Models;

namespace trips_and_travel_system.Controllers
{
    public class AgencyController : Controller
    {
        private TripsAndTravelContext db = new TripsAndTravelContext();
        public ActionResult Profile(int id)
        {
            var agency = db.Users.Find(id);
            return View(agency);
        }
        public ActionResult UpdateProfile(User update, HttpPostedFileBase Img)
        {
            var current = db.Users.Find(update.UserId);
            if (Img != null) //check if there is anychange in profile image
            {
                if (Img.ContentType.Contains("image")) //check if there is an old image
                {
                    string path = System.IO.Path.Combine("~/Database/Images/" ,Path.GetFileName(Img.FileName)); //make path of new image
                    Img.SaveAs(Server.MapPath(path)); //save new uploaded profile image file
                    current.photo = path;
                }
            }
            current.agency.agencyName = update.agency.agencyName;
            current.firstName = update.firstName;
            current.lastName = update.lastName;
            current.username = update.username;
            if(update.password != null)
            {
                current.password = update.password;
            }
            current.phone = update.phone;
            db.SaveChanges();
            return RedirectToAction("Profile", new { id = update.UserId });
        }
        public ActionResult CreateNewPost(int id)
        {
            ViewBag.AgencyId = id;
            return View();
        }
        [HttpPost]
        public ActionResult CreateNewPost(Post post, HttpPostedFileBase Img)
        {
            if (Img != null) //check if there is anychange in profile image
            {
                if (Img.ContentType.Contains("image")) //check if there is an old image
                {
                    string path = "~/Database/Images/" + Path.GetFileName(Img.FileName); //make path of new image
                    Img.SaveAs(Server.MapPath(path)); //save new uploaded profile image file
                    post.tripImage = path;
                }
            }
            post.accepted = false;
            post.postDate = DateTime.Now;
            db.Posts.Add(post);
            db.SaveChanges();
            return RedirectToAction("CreateNewPost", new { id = post.AgencyId });
        }
        public ActionResult MyPosts(int id)
        {
            var posts = from post in db.Posts.ToList()
                        where post.AgencyId == id
                        select post;
            return View(posts);
        }
        public ActionResult DeletePost(int postId)
        {
            var post = db.Posts.Find(postId);
            int agencyId = post.AgencyId;
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("MyPosts", new { id = agencyId });
        }
        public ActionResult EditPost(Post update, HttpPostedFileBase Img)
        {
            Post current = db.Posts.Find(update.PostId);
            if (Img != null) //check if there is anychange in profile image
            {
                if (Img.ContentType.Contains("image")) //check if there is an old image
                {
                    string path = "~/Database/Images/" + Path.GetFileName(Img.FileName); //get path of new image
                    Img.SaveAs(Server.MapPath(path)); //save new uploaded profile image file
                    current.tripImage = path;
                }
            }
            else
            {
                current.tripImage = "~/Database/Images/PostDefaultImage.jpg";
            }
            current.title = update.title;
            current.tripDate = update.tripDate;
            current.tripDestination = update.tripDestination;
            current.details = update.details;
            current.tripPrice = update.tripPrice;
            db.SaveChanges();
            return RedirectToAction("MyPosts", new { id = current.AgencyId });
        }
        public ActionResult ReceivedQuestions(int id)
        {
            var requests = from req in db.FAQs.ToList()
                           where req.post.AgencyId == id && req.answer == ""
                           select req;
            return View(requests);
        }
        [HttpPost]
        public ActionResult SubmitAnswer(FAQ update)
        {
            var current = db.FAQs.Find(update.FAQId);
            current.answer = update.answer;
            db.SaveChanges();
            return RedirectToAction("ReceivedQuestions", new { id = current.post.AgencyId });
        }
    }
}