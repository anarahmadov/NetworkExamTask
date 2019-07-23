using MultiClientChatLast.Domain;
using MultiClientChatLast.Extensions;
using MultiClientChatLast.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MultiClientChatLast.ViewModels
{
    public class ChatPageVM : BaseViewModel
    {
        // object of MainViewModel
        MainWindowViewModel mainViewModel;

        // right side grid
        public Grid RightSideGrid { get; set; }

        // has conversation current user
        private bool hasConversation;

        #region listview itemsource

        // registrated users
        //private List<User> contactedUsers;
        //public List<User> ContactedUsers
        //{
        //    get => contactedUsers;
        //    set
        //    {
        //        contactedUsers = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs(nameof(ContactedUsers)));
        //    }
        //}

        // occured conversations

        private List<Conversation> occuredConversations = new List<Conversation>();
        public List<Conversation> OccuredConversations
        {
            get => occuredConversations;
            set
            {
                occuredConversations = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(OccuredConversations)));
            }
        }

        #endregion

        #region listview selected item binding

        private Conversation selectedConversation;
        public Conversation SelectedConversation
        {
            get => selectedConversation;
            set
            {
                selectedConversation = value;

                InitializeRightSide();

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedConversation)));
            }
        }

        #endregion

        #region commands

        public MainCommand LogOut => new MainCommand((body) =>
        {
            if (MessageBox.Show("Are you sure ?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                App.UserOnSystem = new User();
                mainViewModel.FireOnClickedLogLout();
            }
        });

        #endregion

        public ChatPageVM(MainWindowViewModel viewModel)
        {
            var list = App.RegistratedUsers;

            if (!HasConversation())
            {
                OccuredConversations = new List<Conversation>();

                for (int i = 0; i < list.Count; i++)
                {
                    OccuredConversations.Add(new Conversation()
                    {
                        ToUser = list[i].FirstName,
                        Messages = new List<Message>()
                    });
                }

                App.UserOnSystem.Conversations = OccuredConversations;
            }
            else
                OccuredConversations = App.UserOnSystem.Conversations;

            // save to file
            Config.SaveToFile(App.UserOnSystem);

            RightSideGrid = ChatPage.RightSide;
            SelectedConversation = new Conversation() { Messages = new List<Message>() };
            mainViewModel = viewModel;
        }

        private void InitializeRightSide()
        {
            RightSideGrid.Children.Add(new MessagesPage(this));
        }

        private bool HasConversation()
        {
            return App.UserOnSystem.Conversations == null ? false : true;
        }
    }
}
