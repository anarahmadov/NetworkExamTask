using MultiClientChatLast.Domain;
using System.ComponentModel;
using System.Collections.ObjectModel;
using MultiClientChatLast.ClassesAboutChat;
using System.Windows.Controls;
using MultiClientChatLast.Views;
using System.Threading;
using System.Windows;

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

        public Grid MainGrid { get; set; }

        private Visibility progressBarState = Visibility.Hidden;
        public Visibility ProgressBarState
        {
            get => progressBarState;
            set
            {
                progressBarState = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ProgressBarState)));
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

        ChatController controller => new ChatController(this);

        public MainCommand Send => new MainCommand((body) =>
        {
            if (SendedMessage.Content != null)
            {
                AllMessages.Add(SendedMessage);
                controller.SendMessage();
                SendedMessage = new Message();
            }
        });

        public MainWindowViewModel()
        {
            ReceviedMessage = new Message();
            SendedMessage = new Message();
            //controller.ReceivingMessages();
            AllMessages = new ObservableCollection<Message>();
        }

        public MainCommand Registration => new MainCommand((body) =>
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new RegistrationPage());
        });

        public MainCommand Start => new MainCommand((body) =>
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new LoginPage());
        });

        public MainCommand SignUp => new MainCommand((body) =>
        {
            ProgressBarState = Visibility.Visible;
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new MessagesPage());
        });

    }
}
