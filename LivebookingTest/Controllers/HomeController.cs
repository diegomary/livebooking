using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LivebookingTest.Models;
using System.IO;
using System.Xml.Serialization;
using LivebookingTest.Database;
using System.Web.Script.Serialization;
using System.Net.Mail;
using System.Net;

namespace LivebookingTest.Controllers
{
    public class HomeController : Controller
    {       
        public ActionResult Index()
        {
           // SendNotificationEmail();
            return View();
        }

        [HttpPost]
        public ActionResult Booking(DinerDetails dndtl)
        {
            DinerDetailsRepository dndrep = new DinerDetailsRepository();
            dndrep.AddDinerDetail(dndtl);
            ViewBag.AllDiners = dndrep.DinerDetailsRep;
            ModelState.Clear(); // To clear fields for submission
            return View();
        }


        [HttpPost]
        public ActionResult UpdateData(FormCollection frm)
        {
            string[] data = new string[frm.Count];
            for(int y=0; y< frm.Count; y++)
            data[y] = frm[y];
            DinerDetailsRepository dndrep = new DinerDetailsRepository();
            dndrep.DinerDetailsRep.Where(m => m.FirstName.Equals(data[0]) && m.LastName.Equals(data[1])).First().DiningDate = data[2];          
            dndrep.UpdateDinerDetail();
            ViewBag.AllDiners = dndrep.DinerDetailsRep;           
            return View("Booking");
        }
        public ActionResult Booking()
        {
            DinerDetailsRepository dndrep = new DinerDetailsRepository();          
            ViewBag.AllDiners = dndrep.DinerDetailsRep;
            return View();
        }
        public ActionResult Seating()
        {
            return View();        
        }

        [HttpPost]
        public ActionResult UpdateStatus(FormCollection frm)
        {
            string a = frm[0];
            JavaScriptSerializer js = new JavaScriptSerializer();
            string[] data = js.Deserialize<string[]>(a);          
            DinerDetailsRepository dndrep = new DinerDetailsRepository();
            dndrep.DinerDetailsRep.Where(m => m.FirstName.Equals(data[0]) && m.LastName.Equals(data[1])).First().DinerStatus = data[2];
            dndrep.UpdateDinerDetail();
            return View("Booking");
        }

        [HttpPost]
        public ActionResult UpdateCovers(FormCollection frm)
        {
            string a = frm[0];
            JavaScriptSerializer js = new JavaScriptSerializer();
            string[] data = js.Deserialize<string[]>(a);
            DinerDetailsRepository dndrep = new DinerDetailsRepository();
            dndrep.DinerDetailsRep.Where(m => m.FirstName.Equals(data[0]) && m.LastName.Equals(data[1])).First().Covers =Convert.ToInt32(data[2]);
            dndrep.UpdateDinerDetail();
            return View("Booking");
        }
        
        private void SendNotificationEmail()
        {

            string mailServerName = "smtp.tre.it"; // name of Aruba smtp server
            string subject = "Please read this message sent " + System.DateTime.Now.ToString();
            string body = "Hello Diego I'm The Automaton that you created. Someone is looking at your work ---->>>> ";
            body += (Environment.NewLine + "The username of the visitor was: Wil");
            SmtpClient mailClient = new SmtpClient(mailServerName, 25);
            NetworkCredential cred = new NetworkCredential("diego@dmm888.com", "atreius");// the password of the mail Service provider Alice
            mailClient.Credentials = cred;
            mailClient.UseDefaultCredentials = false;
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailAddress afrom = new MailAddress("diego@dmm888.com"); //Should the sender be an Alice member on pain of lost mail? Verify!
            MailAddress ad = new MailAddress("diego@dmm888.com");
            //MailAddress ad1 = new MailAddress("monica@dmm888.com");
            MailMessage message = new MailMessage();
            message.To.Add(ad);
            //message.To.Add(ad1);
            message.From = afrom;
            message.Body = body;
            message.Subject = subject;
            // Attachment at = new Attachment(Server.MapPath(@"logo.jpg"));// the image must exist on pain of exception 
            //// message.Attachments.Add(at);
            //message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            message.Priority = MailPriority.High;
            mailClient.Send(message);
            //This is a test

        }



    }
}
