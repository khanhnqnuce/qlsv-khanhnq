using System; 
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace FDI
{
    public class Utility
    {

        /// <summary>
        /// get original picture by picture url
        /// </summary>
        /// <param name="pictureName"></param>
        /// <returns></returns>
        public static string GetUrlOriginalPicture(string pictureName)
        {
            return ConfigurationManager.AppSettings["URLOriginalImage"] + pictureName;
        }

        /// <summary>
        /// get picture mediums(size 640) by url
        /// </summary>
        /// <param name="pictureName"></param>
        /// <returns></returns>
        public static string GetUrlPictureMediums(string pictureName)
        {
            return ConfigurationManager.AppSettings["URLImagePromotion"] + pictureName;
        }

        public static void SendEmail(string EmailSend, string EmailSendPwd, string EmailReceive, string subject, string bodyContent)
        {
            const int portServer = 587;
            const string smtpServer = "smtp.gmail.com";

            try
            {
                var mailMessage = new MailMessage();
                var smtpServerClient = new SmtpClient(smtpServer);

                mailMessage.From = new MailAddress(EmailReceive);
                mailMessage.To.Add(EmailReceive);
                mailMessage.Subject = subject;
                mailMessage.Body = bodyContent;
                mailMessage.IsBodyHtml = true;

                smtpServerClient.Port = portServer;
                smtpServerClient.Credentials = new NetworkCredential(EmailSend, EmailSendPwd);
                smtpServerClient.EnableSsl = true;

                smtpServerClient.Send(mailMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        /// <summary>
        /// get picture 
        /// </summary>
        /// <param name="pictureName">name of picture</param>
        /// <returns></returns>
        public static string GetUrlPicture(string pictureName)
        {
            return ConfigurationManager.AppSettings["URLImage"] + pictureName;
        }


        /// <summary>
        /// get picture avatar
        /// </summary>
        /// <param name="pictureName">name of picture</param>
        /// <returns></returns>
        public static string GetUrlPictureAvatar(string pictureName)
        {
            return ConfigurationManager.AppSettings["URLAvatar"] + pictureName;
        }


        public static bool CheckChamTraLoiComment(DateTime ngayGuiComment, DateTime? ngayTraLoi)
        {

            if (ngayTraLoi.HasValue)
            {
                var minute = (ngayTraLoi.Value - ngayGuiComment).Minutes;
                var day = (ngayTraLoi.Value - ngayGuiComment).Days;
                var hour = (ngayTraLoi.Value - ngayGuiComment).Hours;
                return minute > 15 || hour > 0 || day > 0;
            }
            else
            {
                var minute = (DateTime.Now - ngayGuiComment).Minutes;
                var day = (DateTime.Now - ngayGuiComment).Days;
                var hour = (DateTime.Now - ngayGuiComment).Hours;
                return minute > 15 || hour > 0 || day > 0;
            }
        }
 
    }
}