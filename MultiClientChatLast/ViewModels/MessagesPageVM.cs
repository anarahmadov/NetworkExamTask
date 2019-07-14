using MultiClientChatLast.ClassesAboutChat;
using MultiClientChatLast.Domain;
using MultiClientChatLast.Views;
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
        // chat page viewmodel object
        private ChatPageVM chatVM;

        // current user name
        public string toUser;

        // all messages 
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

        // typed message
        private Message sendedMessage = new Message();
        public Message SendedMessage
        {
            get => sendedMessage;
            set
            {
                sendedMessage = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SendedMessage)));
            }
        }

        // received message
        public Message ReceviedMessage { get; set; } = new Message();

        // for send & receive sended data
        ChatController controller => new ChatController(this);

        public MessagesPageVM(ChatPageVM chatVM)
        {
            //controller.ReceivingMessages();

            LoadMessages();

            this.chatVM = chatVM;
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

        #region additional methods

        private void LoadMessages()
        {
            AllMessages = new ObservableCollection<Message>(chatVM.SelectedConversation.Messages);

            toUser = chatVM.SelectedConversation.FirstName;
        }

        #endregion
    }
}
