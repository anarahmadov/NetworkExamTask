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
            if (user != null)
            {
                string json = null;

                if (File.ReadAllText(App.ContactsFilePath) != null)
                {
                    json = JsonConvert.SerializeObject(new List<User>() { user });
                }
                else
                {
                    json = JsonConvert.SerializeObject(user);
                }

                using (var writer = File.AppendText(App.ContactsFilePath))
                {
                    writer.Write(json);
                }
            }
        }

        public User ReadFromFile(User user)
        {
            if (user == null)
                MessageBox.Show("Please, fill the required spaces");

            return JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(App.ContactsFilePath))
                .SingleOrDefault(x => x.EmailAdress == user.EmailAdress);
        }
    }
}
