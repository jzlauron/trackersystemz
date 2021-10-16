using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerSystem.Models;
namespace TrackerSystem.Controllers
{
    public class DashboardController : Controller
    {
        LoginDataBaseEntities db = new LoginDataBaseEntities();
        

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GoToTickets()
        {
            return RedirectToAction("Index", "Ticket");
        }




        //using render action method to get partial views of model items to the dashboard view
        public PartialViewResult RenderTickets()
        {
            return PartialView(GetAllTicket());
        }
        IEnumerable<Ticket> GetAllTicket()
        {
            using (LoginDataBaseEntities db = new LoginDataBaseEntities())
            {
                return db.Tickets.ToList<Ticket>();
            }
        }
    }
}