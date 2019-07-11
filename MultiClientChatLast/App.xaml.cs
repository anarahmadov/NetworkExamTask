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
        public static Socket Socket { get; set; }
        public static string ContactsFilePath { get; set; }
        public static User TryedUser { get; set; }
        public static long SendedConfirmCode { get; set; }
        public static long EnteredConfirmCode { get; set; }

        public static List<User> RegistratedUsers { get; set; }

        public static ConfirmPages ConfirmPagesType { get; set; }

        public App()
        {
            #region temporary
            //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.1.108"), 1031);
            //Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //while (!Socket.Connected)
            //{
            //    try
            //    {
            //        Socket.Connect(endPoint);
            //    }
            //    catch (Exception)
            //    {
            //        Console.WriteLine("NOT CONNECTED");
            //        continue;
            //    }
            //    Console.WriteLine("CONNECTED");
            //    break;
            //}
            #endregion

            TryedUser = new User();

            RegistratedUsers = new List<User>();

            CreateSpecialFile();

            RegistratedUsers = Config.GetAllUsers();
        }

        private void CreateSpecialFile()
        {
            var di = Directory.CreateDirectory($@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\WhatsappJunior");

            ContactsFilePath = $@"{di.FullName}\contacts.json";

            if (!File.Exists(ContactsFilePath))
            {
                using (var sw = new StreamWriter(ContactsFilePath))
                {
                }
            }
        }
    }

    public enum ConfirmPages
    {
        Registration = 1,
        Login = 2
    }
}
