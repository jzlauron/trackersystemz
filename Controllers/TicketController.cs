using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerSystem.Models;

namespace TrackerSystem.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewAll()
        {
            return View(GetAllTicket());
        }

        IEnumerable<Ticket> GetAllTicket()
        {
            using (LoginDataBaseEntities db = new LoginDataBaseEntities())
            {
                return db.Tickets.ToList<Ticket>();
            }
        }
        public ActionResult AddOrEdit(int id = 0)
        {
            Ticket tix = new Ticket();
            //for edit
            if(id != 0)
            {
                using(LoginDataBaseEntities db = new LoginDataBaseEntities())
                {
                    tix = db.Tickets.Where(x => x.TicketID == id).FirstOrDefault<Ticket>();
                }
            }
            return View(tix);
        }
        [HttpPost]
        public ActionResult AddOrEdit(Ticket ticket)
        {
            try
            {
                if (ticket.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(ticket.ImageUpload.FileName);
                    string extension = Path.GetExtension(ticket.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    ticket.ImagePath = "~/AppFiles/Images/" + fileName;
                    ticket.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images"), fileName));
                }
                using (LoginDataBaseEntities db = new LoginDataBaseEntities())
                {
                    //insert operation
                    ticket.DateCreated = DateTime.Now;
                    if (ticket.TicketID == 0)
                    {
                        
                        db.Tickets.Add(ticket);
                        db.SaveChanges();
                    }
                    //update operation
                    else
                    {
                        
                        db.Entry(ticket).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true, html = Global.RenderRazorViewToString(this, "ViewAll", GetAllTicket()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                using (LoginDataBaseEntities db = new LoginDataBaseEntities())
                {
                    Ticket tix = db.Tickets.Where(x => x.TicketID == id).FirstOrDefault<Ticket>();
                    db.Tickets.Remove(tix);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = Global.RenderRazorViewToString(this, "ViewAll", GetAllTicket()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}