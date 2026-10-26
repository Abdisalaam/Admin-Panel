using LeadersSolutionOperation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeadersSolutionOperation.Controllers
{
    [Authorize]
    public class ContactUsController : Controller
    {
        // GET: News Letter Subscripers
        public ActionResult IndexOfNewsLetters(string msg)
        {
            ViewBag.Message = msg;
            var _Subscripers = ContactUs.GetAllSubsripers();
            return View(_Subscripers);
        }
        // GET: ContactUs/Delete/5
        public ActionResult DeleteNewsLetter(int id)
        {
            var _NewsLetterSbscriper = ContactUs.DeleteNewsLetterSubscriper(id);
            if (_NewsLetterSbscriper == false) { return RedirectToAction("IndexOfNewsLetters", new { msg = "Subscriber is not found with the ID" + "=" + id.ToString() }); }
            return RedirectToAction("IndexOfNewsLetters", new { msg = "Subscriber is deleted with the ID" + "=" + id.ToString() });
        }
        // GET: ContactUs
        public ActionResult Index(string msg)
        {
            ViewBag.Message = msg;
            var _ContactusList = ContactUs.GetAllContacts();
            return View(_ContactusList);
        }

        // GET: ContactUs/Details/5
        public ActionResult Details(int id)
        {
            var _Contactus = ContactUs.GetContactUsByID(id);
            if (_Contactus == null)
            {
                return RedirectToAction("Index", new { msg = "Contact Us is not found with the ID" + "=" + id.ToString() });

            }
            return View(_Contactus);
        }
        // GET: ContactUs/Delete/5
        public ActionResult Delete(int id)
        {
            var _Contactus = ContactUs.GetContactUsByID(id);
            if (_Contactus == null) { return RedirectToAction("Index", new { msg = "Contact Us is not found with the ID" + "=" + id.ToString() }); }
            return View(_Contactus);

        }

        // POST: ContactUs/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ContactUs contactUs)
        {
            try
            {
                var _Contactus = ContactUs.DeleteContactUs(id);
                if (_Contactus == false)
                {
                    return RedirectToAction("Index", new { msg = "Contact Us is not found with the ID" + "=" + id.ToString() });
                }
                else
                {
                    return RedirectToAction("Index", new { msg = "Contact Us is deleted with the ID" + "=" + id.ToString() });

                }
            }
            catch
            {
                return RedirectToAction("Index", new { msg = "Technical error please try again" });
            }
        }
    }
}
