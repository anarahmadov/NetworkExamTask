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

        // conversations listview
        public ListView ConversationsList { get; set; }

        // has conversation current user
        private bool hasConversation;

        #region listview itemsource

        // registrated users
        private List<User> contactedUsers;
        public List<User> ContactedUsers
        {
            get => contactedUsers;
            set
            {
                contactedUsers = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ContactedUsers)));
            }
        }

        // occured conversations
        private List<Conversation> occuredConversations;
        public List<Conversation> OccuredConversations
        {
            get => occuredConversations;
            set
            {
                occuredConversations = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(OccuredConversations)));
            }
        }

        // binding list
        private IEnumerable<object> bindingList;
        public IEnumerable<object> BindingList
        {
            get => bindingList;
            set
            {
                bindingList = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(BindingList)));
            }
        }

        #endregion

        #region listview selected item binding

        private Conversation selectedConversation = new Conversation();
        public Conversation SelectedConversation

        {
            get => selectedConversation;
            set
            {
                selectedConversation = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedConversation)));
            }
        }

        private User selectedContact = new User();
        public User SelectedContact
        {
            get => selectedContact;
            set
            {
                selectedContact = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedContact)));
            }
        }

        private object bindingItem;
        public object BindingItem
        {
            get => bindingItem;
            set
            {
                bindingItem = value;
                Control();
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(BindingItem)));
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
            ContactedUsers = App.RegistratedUsers;

            //App.UserOnSystem = new User();

            hasConversation = HasConversation();

            if (hasConversation)
            {
                OccuredConversations = App.UserOnSystem.Conversations;
                BindingList = OccuredConversations;
                BindingItem = SelectedConversation;
            }
            else
            {
                BindingList = ContactedUsers;
                BindingItem = SelectedContact;
            }

            mainViewModel = viewModel;
        }

        private void Control()
        {
            if (RightSideGrid != null)
            {
                var lenght = RightSideGrid.Children.Count;

                if (lenght == 0)
                {
                    RightSideGrid.Children.Add(new MessagesPage(this));
                }
            }

            if (BindingItem is User)
            {
                if (!hasConversation)
                {
                    App.UserOnSystem.Conversations = new List<Conversation>()
                    {
                        new Conversation()
                        {
                            FirstName = SelectedContact.FirstName,
                            Messages = new List<Message>()
                        }
                    };
                }

                BindingList = OccuredConversations;
                BindingItem = SelectedConversation;
            }
            else if (BindingItem is Conversation)
            {

            }
        }

        private bool HasConversation()
        {
            return App.UserOnSystem.Conversations == null ? false : true;
        }
    }
}
