using MultiClientChatLast.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiClientChatLast.Extensions
{
    public class Config
    {
        public static void SaveToFile(User user)
        {
            try
            {
                if (user != null)
                {
                    string json = null;

                    json = JsonConvert.SerializeObject(App.RegistratedUsers);

                    File.WriteAllText(App.ContactsFilePath, json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static User ReadFromFile(User user)
        {
            if (user == null)
                MessageBox.Show("Please, fill the required spaces");

            return JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(App.ContactsFilePath))
                .SingleOrDefault(x => x.EmailAddress == user.EmailAddress);
        }

        public static List<User> GetAllUsers()
        {
            var result = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(App.ContactsFilePath));

            return result;
        }

        public static void SaveChanges()
        {
            App.RegistratedUsers = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(App.ContactsFilePath));
        } 

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
