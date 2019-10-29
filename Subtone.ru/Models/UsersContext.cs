using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Subtone.ru.Models
{
    public class UsersContext : DbContext
    {
        public DbSet<Reservation> DateTimeReservation { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Account> dbAccount { get; set; }
    }
}