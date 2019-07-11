using MultiClientChatLast.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiClientChatLast.Extensions
{
    public class Config
    {
        public void SaveToFile(User user)
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

        public User ReadFromFile(User user)
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
    }
}
