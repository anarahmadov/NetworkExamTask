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
        public static void SendConfirmCode(string emailAddress, long confirmCode)
        {
            Task.Run(() =>
            {
                string mailBodyhtml =
                    $"<h1>Your authorization code is {confirmCode}</h1>";
                var msg = new MailMessage("anar.axmed5514@gmail.com", emailAddress, "Authorization Code", mailBodyhtml);
                msg.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential("anar.axmed5514@gmail.com", "anaranar888");
                smtpClient.EnableSsl = true;
                smtpClient.Send(msg);
            });
        }
    }
}
