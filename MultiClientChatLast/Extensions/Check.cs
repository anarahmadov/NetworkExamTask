using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiClientChatLast.Extensions
{
    public class Check
    {
        static Random random = new Random();

        public static long SendConfirmCode(string emailAddress)
        {
            long confirmCode = random.Next();

            Task.Run(() =>
            {
                string mailBodyhtml =
                    $"<h1>Confirm code </h1><p1>{confirmCode}</p1>";
                var msg = new MailMessage("anar.axmed5514@gmail.com", emailAddress, "Authorization Code", mailBodyhtml);
                msg.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential("anar.axmed5514@gmail.com", "anaranar888");
                smtpClient.EnableSsl = true;
                smtpClient.Send(msg);

            });

            return confirmCode;
        }

        public static bool isRegistrated(string emailAddress)
        {
            var result = App.RegistratedUsers.SingleOrDefault(x => x.EmailAddress == emailAddress);

            return result == null ? false : true;
        }
    }
}
