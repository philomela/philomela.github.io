using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subtone.ru.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public DateTime lastDateTimeSession { get; set; }
    }
}