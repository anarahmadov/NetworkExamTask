using MultiClientChatLast.Domain;
using MultiClientChatLast.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

                if (selectedConversation != null)
                {

                    Control();
                }

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedConversation)));
            }
        }

        #endregion

        #region commands

        public MainCommand LogOut => new MainCommand((body) =>
        {
            if (MessageBox.Show("Are you sure ?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                App.UserOnSystem = null;
                mainViewModel.FireOnClickedLogLout();
            }
        });

        #endregion

        public ChatPageVM(MainWindowViewModel viewModel)
        {
            var list = App.RegistratedUsers;

            for (int i = 0; i < list.Count; i++)
            {
                occuredConversations.Add(new Conversation()
                {
                    ToUser = list[i].FirstName
                });
            }

            App.UserOnSystem.Conversations = new List<Conversation>();

           RightSideGrid = ChatPage.RightSide;
            SelectedConversation = new Conversation();
            mainViewModel = viewModel;
        }

        private void Control()
        {
            RightSideGrid.Children.Add(new MessagesPage(this));
        }

        private bool HasConversation()
        {
            return App.UserOnSystem.Conversations == null ? false : true;
        }
    }
}
