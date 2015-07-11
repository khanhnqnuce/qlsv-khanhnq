using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace FDI.Common
{
    public class Email
    {
        public static string email_send()
        {
            var mail = new MailMessage();
            var smtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("tattung1711@gmail.com");
            mail.To.Add("tattung1711@gmail.com");
            mail.Subject = "Test Mail - 1  ";
            mail.Body = "mail with attachment";

            //System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            //mail.Attachments.Add(attachment);

            smtpServer.Port = 587;
            smtpServer.Credentials = new NetworkCredential("tattung1711@gmail.com", "pass");
            smtpServer.EnableSsl = true;

            smtpServer.Send(mail);
            return "success!";
        }
    }
}