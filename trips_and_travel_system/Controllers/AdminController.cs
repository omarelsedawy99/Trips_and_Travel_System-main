using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using trips_and_travel_system.Models;

using Newtonsoft.Json;
using System.IO;

namespace trips_and_travel_system.Controllers
{
    public class AdminController : Controller
    {
        private TripsAndTravelContext db = new TripsAndTravelContext();
        public ActionResult Profile(int id)
        {
            var user = db.Users.Find(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult UpdateProfile(User update, HttpPostedFileBase Img)
        {
            var current = db.Users.Find(update.UserId);
            if (Img != null) //check if there is anychange in profile image
            {
                if (Img.ContentType.Contains("image")) //check if there is an old image
                {
                    string path = System.IO.Path.Combine("~/Database/Images/", Path.GetFileName(Img.FileName)); //make path of new image
                    Img.SaveAs(Server.MapPath(path)); //save new uploaded profile image file
                    current.photo = path;
                }
            }
            current.firstName = update.firstName;
            current.lastName = update.lastName;
            current.username = update.username;
            if (update.password != null)
            {
                current.password = update.password;
            }
            current.phone = update.phone;
            db.SaveChanges();
            return RedirectToAction("Profile", new { id = update.UserId });
        }

        #region Post Section
        public ActionResult Posts()
        {
            var posts = from p in db.Posts.ToList()
                        where p.accepted == true
                        select p;
            return View(posts);
        }
        [HttpPost]
        public ActionResult EditPost(Post update)
        {
            Post current = db.Posts.Find(update.PostId);
            current.title = update.title;
            current.tripDate = update.tripDate;
            current.tripDestination = update.tripDestination;
            current.details = update.details;
            db.SaveChanges();
            return RedirectToAction("Posts");
        }
        public ActionResult DeletePost(int id)
        {
            db.Posts.Remove(db.Posts.Find(id));
            db.SaveChanges();
            return RedirectToAction("Posts");
        }
        public ActionResult PostsRequests()
        {
            var posts = from p in db.Posts.ToList()
                        where p.accepted == false
                        select p;
            return View(posts);
        }
        public ActionResult AcceptRequest(int id)
        {
            var post = db.Posts.Find(id);
            post.accepted = true;
            db.SaveChanges();
            return RedirectToAction("PostsRequests");
        }
        public ActionResult RefuseRequest(int id)
        {
            db.Posts.Remove(db.Posts.Find(id));
            db.SaveChanges();
            return RedirectToAction("PostsRequests");
        }
        #endregion

        #region Users Section

        public ActionResult Users()
        {
            #region seed data

            /*db.Roles.Add(new Role() { RoleName = "Admin" });
            db.Roles.Add(new Role() { RoleName = "Agency" });
            db.Roles.Add(new Role() { RoleName = "Traveler" });
            db.SaveChanges();

            using (StreamReader sr1 = new StreamReader(Server.MapPath("~/Database/Json/Admin_Agency.json")))
            {
                var users = JsonConvert.DeserializeObject<List<Class1>>(sr1.ReadToEnd());
                StreamReader sr2 = new StreamReader(Server.MapPath("~/Database/Json/Agency_name.json"));
                var agencies = JsonConvert.DeserializeObject<List<Class2>>(sr2.ReadToEnd());
                StreamReader sr3 = new StreamReader(Server.MapPath("~/Database/Json/Posts.json"));
                var posts = JsonConvert.DeserializeObject<List<Class3>>(sr3.ReadToEnd());

                db.Users.Add(new User()
                {
                    roleId = users[0].role,
                    username = users[0].username,
                    email = users[0].email_address,
                    password = users[0].password,
                    firstName = users[0].first_name,
                    lastName = users[0].last_name,
                    phone = users[0].phone,
                    photo = users[0].photo
                });
                db.SaveChanges();
                for (int i = 0; i < 99; i++)
                {
                    db.Posts.Add(new Post()
                    {
                        Agency = new Agency()
                        {
                            user = new User()
                            {
                                roleId = users[i + 1].role,
                                username = users[i + 1].username,
                                email = users[i + 1].email_address,
                                password = users[i + 1].password,
                                firstName = users[i + 1].first_name,
                                lastName = users[i + 1].last_name,
                                phone = users[i + 1].phone,
                                photo = users[i + 1].photo
                            },
                            agencyName = agencies[i].agency_name
                        },
                        title = posts[i].title,
                        details = posts[i].details,
                        tripDestination = posts[i].trip_destination,
                        tripImage = "~/Database/Images/PostDefaultImage.jpg",
                        tripPrice = float.Parse(posts[i].trip_price),
                        tripDate = DateTime.Parse(posts[i].trip_date),
                        postDate = DateTime.Parse(posts[i].post_date),
                        accepted = posts[i].accepted
                    });
                }
            }
            db.SaveChanges();

            using (StreamReader sr4 = new StreamReader(Server.MapPath("~/Database/Json/FAQs.json")))
            {
                StreamReader sr = new StreamReader(Server.MapPath("~/Database/Json/Users.json"));
                var users = JsonConvert.DeserializeObject<List<Class1>>(sr.ReadToEnd());
                var faqs = JsonConvert.DeserializeObject<List<Class4>>(sr4.ReadToEnd());
                for (int i = 0; i < 99; i++)
                {
                    db.FAQs.Add(new FAQ()
                    {
                        *//*traveler = new User()
                        {
                            roleId = users[i].role,
                            username = users[i].username,
                            email = users[i].email_address,
                            password = users[i].password,
                            firstName = users[i].first_name,
                            lastName = users[i].last_name,
                            phone = users[i].phone,
                            photo = users[i].photo
                        },*//*
                        postId = i + 1,
                        question = faqs[i].question,
                        answer = faqs[i].answer
                    });
                }
            }
            db.SaveChanges();*/

            #endregion
            return View(db.Users.ToList());
        }
        public ActionResult DeleteUser(int id)
        {
            var user = db.Users.Find(id);
            if (user.agency != null)
            {
                db.Agencies.Remove(user.agency);
            }
            else
            {
                db.Users.Remove(user);
            }
            
            db.SaveChanges();
            return RedirectToAction("Users");
        }
        [HttpPost]
        public ActionResult EditUser(User update)
        {
            var current = db.Users.Find(update.UserId);
            current.username = update.username;
            current.firstName = update.firstName;
            current.lastName = update.lastName;
            current.email = update.email;
            current.phone = update.phone;
            db.SaveChanges();
            return RedirectToAction("Users");
        }

        #endregion
    }
}