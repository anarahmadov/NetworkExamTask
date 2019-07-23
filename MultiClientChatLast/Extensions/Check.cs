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
            if (emailAddress != null)
            {
                long confirmCode = random.Next();

                Task.Run(() =>
                {
                    string mailBodyhtml =
                        $"<h2> Your confirm code : </h2><p1>{confirmCode}</p1>";
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

            return 0;
        }

        public static bool isRegistrated(string emailAddress)
        {
            if (App.RegistratedUsers == null)
            {
                return false;
            }

            var result = App.RegistratedUsers.SingleOrDefault(x => x.EmailAddress == emailAddress);

            return result == null ? false : true;
        }
    }
}
