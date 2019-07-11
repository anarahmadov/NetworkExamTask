using MultiClientChatLast.Domain;
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
        public static int SendedConfirmCode { get; set; }
        public static int EnteredConfirmCode { get; set; }

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

            CreateSpecialFile();
        }

        private void CreateSpecialFile()
        {
            var di = Directory.CreateDirectory($@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\WhatsappJunior");

            File.Create($@"{di.FullName}\contacts.json");
        } 
    }
}
