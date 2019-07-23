using MultiClientChatLast.ClassesAboutChat;
using MultiClientChatLast.Domain;
using MultiClientChatLast.Extensions;
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
        public ChatPageVM chatVM;

        // current user name
        private string toUser;
        public string ToUser
        {
            get => toUser;
            set
            {
                toUser = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ToUser)));
            }
        }

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
            controller.ReceivingMessages();

            this.chatVM = chatVM;

            LoadMessages();
        }

        public MainCommand Send => new MainCommand((body) =>
        {
            if (SendedMessage.Content != null)
            {
                AllMessages.Add(SendedMessage);

                chatVM.OccuredConversations.Single(x => x.ToUser == chatVM.SelectedConversation.ToUser).Messages.Add(new Message()
                {
                    Content = SendedMessage.Content
                });

                // save to file
                Config.SaveToFile(App.UserOnSystem);

                controller.SendMessage();

                SendedMessage = new Message();
            }
        });

        #region additional methods

        private void LoadMessages()
        {
            AllMessages = new ObservableCollection<Message>(chatVM.SelectedConversation.Messages);

            toUser = chatVM.SelectedConversation.ToUser;
        }

        #endregion
    }
}
