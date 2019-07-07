using MultiClientChatLast.Domain;
using MultiClientChatLast.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiClientChatLast.ClassesAboutChat
{
    public class ChatController
    {
        MainWindowViewModel viewmodel;

        byte[] buffer = new byte[1024];

        public ChatController(MainWindowViewModel viewmodel)
        {
            this.viewmodel = viewmodel;
        }

        public void ReceivingMessages()
        {
            Task receiverTask = Task.Run(() =>
            {
                while (true)
                {
                    if (App.Socket.Connected)
                    {
                        int length = App.Socket.Receive(buffer);

                        if (length != 0)
                        {
                            var msg = Encoding.ASCII.GetString(buffer, 0, length);

                            viewmodel.ReceviedMessage.Content = msg;

                            App.Current.Dispatcher.Invoke(() =>
                            {
                                viewmodel.AllMessages.Add(viewmodel.ReceviedMessage);
                            });
                        }

                        viewmodel.ReceviedMessage = new Message();
                    }
                }
            });
        }

        public void SendMessage()
        {
            Task senderTask = Task.Run(() =>
            {
                App.Socket.Send(Encoding.ASCII.GetBytes(viewmodel.SendedMessage.Content));
            });

            Task.WaitAll(senderTask);
        }
    }
}
