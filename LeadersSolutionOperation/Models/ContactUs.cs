using LeadersSolutionOperation.Utilities;
using LeadersSolutionOperation.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace LeadersSolutionOperation.Models
{
    public partial class ContactUs
    {
        public static ContactUs DecryptContact(ContactUs c)
        {
            c.Name = CryptoHelper.Decrypt(c.Name);
            c.Email = CryptoHelper.Decrypt(c.Email);
            c.Mobile = CryptoHelper.Decrypt(c.Mobile);
            c.Service = CryptoHelper.Decrypt(c.Service);           
            return c;
        }
        public static NewsLetters DecryptNewsLetters(NewsLetters n)
        {
            n.Email = CryptoHelper.Decrypt(n.Email);           
            return n;
        }
        public static List<NewsLetters> GetAllSubsripers()
        {
            var db = new LSNewDataModelContainer();
            var list = db.NewsLetters.ToList();
            list = list.Select(DecryptNewsLetters).ToList();
            return list;
        }
        public static bool DeleteNewsLetterSubscriper(int newsLetterId)
        {
            var db = new LSNewDataModelContainer();
            var newsLetter = db.NewsLetters.Find(newsLetterId);
            if (newsLetter != null)
            {
                db.NewsLetters.Remove(newsLetter);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public static List<ContactUs> GetAllContacts()
        {
            var db = new LSNewDataModelContainer();
            var list = db.ContactUs.OrderByDescending(x => x.ContactedDate).ToList();
            list = list.Select(DecryptContact).ToList();
            return list;
        }

        public static ContactUs GetContactUsByID(int contactUsId)
        {
            var db = new LSNewDataModelContainer();
            var contactUs = db.ContactUs.Find(contactUsId);
            if (contactUs != null)
            {
                contactUs = DecryptContact(contactUs);
                return contactUs;
            }
            return null;
        }
        public static bool DeleteContactUs(int contactUsId)
        {
            var db = new LSNewDataModelContainer();
            var contactUs = db.ContactUs.Find(contactUsId);
            if (contactUs != null)
            {
                db.ContactUs.Remove(contactUs);
                db.SaveChanges();
                return true;
            }
            return false;
        }

    }
}