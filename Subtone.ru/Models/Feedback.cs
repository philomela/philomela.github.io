using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subtone.ru.Models
{
    public class Feedback
    {
        public int feedbackId { get; set; }

        public string nameSender { get; set; }
        public string emailSender { get; set; }
        public string commentMessage { get; set; }
    }
}