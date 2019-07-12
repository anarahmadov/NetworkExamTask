using MultiClientChatLast.ClassesAboutChat;
using MultiClientChatLast.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiClientChatLast.ViewModels
{
    public class MessagesPageVM : BaseViewModel
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

        private Message sendedMessage;
        public Message SendedMessage
        {
            get => sendedMessage;
            set
            {
                sendedMessage = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SendedMessage)));
            }
        }

        public Message ReceviedMessage { get; set; }

        // for send & receive sended data
        ChatController controller => new ChatController(this);

        public MessagesPageVM()
        {
            ReceviedMessage = new Message();
            SendedMessage = new Message();
            controller.ReceivingMessages();
            AllMessages = new ObservableCollection<Message>();
        }

        public MainCommand Send => new MainCommand((body) =>
        {
            if (SendedMessage.Content != null)
            {
                AllMessages.Add(SendedMessage);
                controller.SendMessage();
                SendedMessage = new Message();
            }
        });
    }
}
