using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerSystem.Models;
namespace TrackerSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();

        }
        // POST: Login success 
        [HttpPost]
        //To check if any user and pass exists using LoginDataModel entities model
        public ActionResult Authorize(TrackerSystem.Models.User userModel) //Parameter of type user.cs
        {
            using (LoginDataBaseEntities db = new LoginDataBaseEntities())
            {
                var userDetails = db.Users.Where(x => x.Username == userModel.Username && x.Password == userModel.Password).FirstOrDefault(); //finds password and username from db
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong username or password.";
                    return View("Index", userModel);
                }
                else
                {
                    Session["userID"] = userDetails.UserID;
                    Session["userName"] = userDetails.Username;
                    return RedirectToAction("Index", "Dashboard");
                }
            }

        }
        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];

            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            User userModel = new User();
            return View(userModel);
        }
        [HttpPost]
        public ActionResult AddOrEdit(User userModel)
        {
            using (LoginDataBaseEntities db = new LoginDataBaseEntities())
            {
                try
                {
                    db.Users.Add(userModel);
                    db.SaveChanges();

                    ViewBag.Message = "Registration Successful";
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Response.Write(validationError.ErrorMessage );
                            
                        }
                        Console.WriteLine("\n");
                    }
                }
            }
            ModelState.Clear();

            return View("AddOrEdit", new User());
        }
    }
}