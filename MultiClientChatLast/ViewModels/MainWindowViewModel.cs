using MultiClientChatLast.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Net;

namespace MultiClientChatLast.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<Message> allMessages;
        public ObservableCollection<Message> AllMessages
        {
            get => allMessages;
            set
            {
                allMessages = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(AllMessages)));
            }
        }

        public Socket Socket { get; set; }

        byte[] buffer = new byte[1024];

        private Message currentMessage;
        public Message CurrentMessage
        {
            get => currentMessage;
            set
            {
                currentMessage = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CurrentMessage)));
            }
        }

        public MainCommand Send => new MainCommand((body) =>
        {
            AllMessages.Add(CurrentMessage);
            Task senderTask = Task.Run(() =>
            {                
                Socket.Send(Encoding.ASCII.GetBytes(CurrentMessage.Content));
            });

            Task receiverTask = Task.Run(() =>
            {
                int length = Socket.Receive(buffer);
                if (length != 0)
                {
                    var msg = Encoding.ASCII.GetString(buffer, 0, length);
                AllMessages.Add(new Message() { Content = $"Server 1: {msg}" });
                }
            });

            Task.WaitAll(senderTask, receiverTask);
        });

        public MainWindowViewModel()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("10.1.16.33"), 1031);
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket.Connect(endPoint);

            currentMessage = new Message();
            AllMessages = new ObservableCollection<Message>();
        }


    }
}
