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
        public static User TryedUser { get; set; }
        public static long SendedConfirmCode { get; set; }
        public static long EnteredConfirmCode { get; set; }

        public static List<User> RegistratedUsers { get; set; }

        public static ConfirmPages ConfirmPagesType { get; set; }

        public App()
        {
            #region Connect with TCPClient

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("10.1.16.11"), port);
            Client = new TcpClient();
            Client.Connect(endPoint);

            while (!Client.Connected)
            {
                try
                {
                    Client.Connect(endPoint);
                }
                catch (Exception)
                {
                    MessageBox.Show("NOT CONNECTED");;
                    continue;
                }
                MessageBox.Show("CONNECTED");
                break;
            }

            #endregion

            #region temporary
            //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("10.1.16.11"), 1031);
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

            CreateSpecialFile();

            if (Config.GetAllUsers() == null)
                RegistratedUsers = new List<User>();
            else
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
