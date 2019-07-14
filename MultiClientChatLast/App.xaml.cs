using MultiClientChatLast.Domain;
using MultiClientChatLast.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;

namespace MultiClientChatLast
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static TcpClient Client { get; set; }
        private static int port = 25655;

        public static string ContactsFilePath { get; set; }
        public static User TryedUser { get; set; } = new User();
        public static long SendedConfirmCode { get; set; }
        public static long EnteredConfirmCode { get; set; }

        public static List<User> RegistratedUsers { get; set; }

        public static ConfirmPages ConfirmPagesType { get; set; }

        // Current user on system
        public static User UserOnSystem { get; set; } = new User();

        public App()
        {
            #region Connect to SERVER

            //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.1.102"), port);
            //Client = new TcpClient();
            //Client.Connect(endPoint);

            //while (!Client.Connected)
            //{
            //    try
            //    {
            //        Client.Connect(endPoint);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("NOT CONNECTED"); ;
            //        continue;
            //    }
            //    MessageBox.Show("CONNECTED");
            //    break;
            //}

            #endregion

            #region calling method which creates special file

            CreateSpecialFile();

            #endregion

            #region initialize already registrated users

            var list = Config.GetAllUsers();

            if (list == null)
                RegistratedUsers = new List<User>();
            else
                RegistratedUsers = list;

            #endregion
        }

        private void CreateSpecialFile()
        {
            //var di = Directory.CreateDirectory($@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\WhatsappJunior");

            ContactsFilePath = @"contacts.json";

            if (!File.Exists(ContactsFilePath))
            {
                File.Create(ContactsFilePath);
            }
        }
    }

    public enum ConfirmPages
    {
        Registration = 1,
        Login = 2
    }
}
