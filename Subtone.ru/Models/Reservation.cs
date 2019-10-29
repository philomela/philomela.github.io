using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subtone.ru.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string date { get; set; }
        public string timeBusy { get; set; }
        public string studioId { get; set; }
        public string NamePerson { get; set; }
        public string SurnamePerson { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAdress { get; set; }
        public string MessagePerson { get; set; }
        public DateTime dateRequest { get; set; }
        public int confirmedReservation { get; set; }
        public string hashConfirmed { get; set; }

        public Reservation GetReservationClone()
        {
            return (Reservation)MemberwiseClone();
        }
        
    }
}