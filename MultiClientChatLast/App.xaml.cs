using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        public App()
        {
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
        }
    }
}
