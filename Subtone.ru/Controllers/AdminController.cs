using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Subtone.ru.Models;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity;
using System.Web.Mvc.Filters;
using System.Web.Security;

namespace Subtone.ru.Controllers
{
    public class AdminController : Controller
    {
        private ActionResult Index()
        {
            using (UsersContext dbReservation = new UsersContext())
            {
                List<Reservation> reservationsForView = new List<Reservation>();
                IEnumerable<Reservation> Reservations = dbReservation.DateTimeReservation;
                foreach (Reservation currentReservation in Reservations)
                {
                    reservationsForView.Add(currentReservation);
                    ViewBag.Reservations = reservationsForView;
                }
            }
            return View("~/Views/Admin/AdminPanel.cshtml");
        }


        public ActionResult LoginForm()
        {
            return View();
        }


        public ActionResult Verify(Account account)
        {
            bool AccountConfirm = false;
            using (UsersContext dbAccounts = new UsersContext())
            {
                IEnumerable<Account> Accounts = dbAccounts.dbAccount;
                foreach (Account currentAccount in Accounts)
                {
                    if (currentAccount.login == account.login && currentAccount.password
                        == GetHash(account.password))
                    {
                        currentAccount.lastDateTimeSession = DateTime.Now;

                        FormsAuthentication.SetAuthCookie(currentAccount.login, true);
                        AccountConfirm = true;
                    }
                }
                if (AccountConfirm)
                {
                    dbAccounts.SaveChanges();
                    return Index();
                }
            }
            return View("~/Views/Shared/ErrorLogin.cshtml");
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (UsersContext dbReservation = new UsersContext())
                {
                    IEnumerable<Reservation> Reservations = dbReservation.DateTimeReservation;
                    foreach (Reservation currentReservation in Reservations)
                    {
                        if (currentReservation != null && currentReservation.Id == Id)
                        {
                            dbReservation.Entry(currentReservation).State = EntityState.Deleted;

                        }
                    }
                    dbReservation.SaveChanges();
                }
                return Index();
            }
            else
            {
                return Redirect("/Admin/LoginForm");
            }
        }

       // []

        private string GetHash(string inputPass)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(inputPass));

            return Convert.ToBase64String(hash);
        }
    }
}
