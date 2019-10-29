using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Subtone.ru.Models;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using NLog;

namespace Subtone.ru.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public JsonResult Calendar(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            else
            {
                string[] getParams = id.Split(new char[] { '-' });
                string selectedStudio = getParams[3];
                string selectedDate = getParams[0] + "-" + getParams[1] + "-" + getParams[2];
                List<string> dbArrayFreeTime = new List<string>();

                using (UsersContext dbReservation = new UsersContext())
                {
                    IEnumerable<Reservation> reservations = dbReservation.DateTimeReservation;
                    foreach (Reservation reservation in reservations)
                    {
                        if (reservation.date == selectedDate && reservation.studioId == selectedStudio && reservation.confirmedReservation != 0)
                        {
                            dbArrayFreeTime.Add(reservation.timeBusy);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                var BusyDatesTimes = new { BusyDatesTimes = dbArrayFreeTime };
                return Json(new JavaScriptSerializer().Serialize(BusyDatesTimes), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Confirm(object id)
        {
            int countConfirm = 0;
            using (UsersContext dbReservation = new UsersContext())
            {

                IEnumerable<Reservation> reservations = dbReservation.DateTimeReservation;
                foreach (Reservation reservation in reservations)
                {
                    if (reservation.hashConfirmed == Convert.ToString(id) && reservation.confirmedReservation == 1 && (reservation.dateRequest - DateTime.Now)
                        .TotalDays < 1)
                    {
                        reservation.confirmedReservation = 2;
                        countConfirm++;
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (countConfirm != 0)
                {
                    dbReservation.SaveChanges();
                    return View("~/Views/Home/Confirm.cshtml");
                }
                else
                {
                    return View("~/Views/Shared/Error.cshtml");
                }
            }
        }


        [HttpPost]
        public ActionResult Reserve(Reservation reservation)
        {
            string[] BusyTimes;
            reservation.dateRequest = DateTime.Now;
            if (!string.IsNullOrEmpty(reservation.timeBusy))
            {
                BusyTimes = reservation.timeBusy.TrimEnd(';').Split(new char[] { ';' });
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            reservation.hashConfirmed = GetHashCode().ToString() + new Random().Next().ToString();

            using (UsersContext dbReservation = new UsersContext())
            {
                foreach (string BusyTime in BusyTimes)
                {
                    var clonedReservation = reservation.GetReservationClone();
                    clonedReservation.timeBusy = BusyTime;
                    clonedReservation.confirmedReservation = 1;
                    dbReservation.DateTimeReservation.Add(clonedReservation);
                }

                dbReservation.SaveChanges();
            }

            Task.Run(() => SendMailConfirmed(reservation));

            return View();
        }

        [HttpPost]
        public RedirectResult FeedBack(Feedback feedback)
        {
            using (UsersContext dbReservation = new UsersContext())
            {
                dbReservation.Feedbacks.Add(feedback);
                dbReservation.SaveChanges();
            }
            Task.Run(() => SendMailFeedBack(feedback));
            return RedirectPermanent("/Home/Index");
        }

        void SendMailConfirmed(Reservation reservation)
        {
            try
            {
                MailAddress from = new MailAddress("*", "Subtone team");
                MailAddress to = new MailAddress(reservation.EmailAdress);
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Booking Confirmation";
                m.Body = $"<h2 style=\"font-size:18px;\">You have booked a studio at *</h2><p>Hi {reservation.NamePerson}! " +
                    $"Please confirm your reservation.We look forward to meeting you. To confirm the reservation, " +
                    $"follow the link: http://*/Home/Confirm/" + $"{reservation.hashConfirmed}</a></p><p></p>";
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("*", 25);
                smtp.Credentials = new NetworkCredential("administration@*", "*");
                smtp.EnableSsl = false;
                smtp.Send(m);
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex);
            }
        }
        void SendMailFeedBack(Feedback feedback)
        {
            try
            {
                MailAddress from = new MailAddress(feedback.emailSender, "Subtone Feedbacks");
                MailAddress to = new MailAddress("*");
                MailMessage m = new MailMessage(from, to);
                m.Subject = $"Feedback from {feedback.nameSender}";
                m.Body = $"<p>{feedback.commentMessage} from {feedback.emailSender}</p>";
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("subtone.ru", 25);
                smtp.Credentials = new NetworkCredential("*", "*");
                smtp.EnableSsl = false;
                smtp.Send(m);
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex);
            }
        }
    }
}