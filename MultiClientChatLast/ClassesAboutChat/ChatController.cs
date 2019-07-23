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
        MessagesPageVM viewmodel;

        byte[] buffer = new byte[11000000];

        public ChatController(MessagesPageVM viewmodel)
        {
            this.viewmodel = viewmodel;
        }

        public void ReceivingMessages()
        {
            Task receiverTask = Task.Run(() =>
            {
                while (true)
                {
                    if (App.Client.Connected)
                    {
                        int length = App.Client.Client.Receive(buffer);

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
                var UserFirstName = viewmodel.chatVM.SelectedConversation.ToUser;

                var message = $"{viewmodel.SendedMessage.Content} {App.LocalIPAddress} {App.RegistratedUsers.Single(x => x.FirstName == UserFirstName).IPAddress}";

                App.Client.Client.Send(Encoding.ASCII.GetBytes(message));
            });

            Task.WaitAll(senderTask);
        }
    }
}
