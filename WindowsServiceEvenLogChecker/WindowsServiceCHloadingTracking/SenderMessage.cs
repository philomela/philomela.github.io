using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace WindowsServiceCHloadingTracking
{
    class SenderMessage
    {
        string _messageText;
        public SenderMessage(string messageText)
        {
            _messageText = messageText;
        }
        public void SendMessage()
        {
            MailAddress from = new MailAddress("Adress", "NameFrom");
            MailMessage message = new MailMessage();
            message.To.Add("AdressTo");
            message.From = from;
            message.Subject = "Subject";
            message.Body = $"<h3>Text</h3><p>{_messageText}</p>";
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtpserver", 25);
            smtp.EnableSsl = false;
            smtp.Send(message);
        }
    }
}
