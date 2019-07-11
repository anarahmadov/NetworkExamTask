using MultiClientChatLast.Domain;
using System.ComponentModel;
using System.Collections.ObjectModel;
using MultiClientChatLast.ClassesAboutChat;
using System.Windows.Controls;
using MultiClientChatLast.Views;
using System.Threading;
using System.Windows;
using System.Threading.Tasks;
using System;
using MultiClientChatLast.CustomEvents;
using MultiClientChatLast.Extensions;

namespace MultiClientChatLast.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region properties and fields

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

        // custom events
        public delegate void MyEventHandler(object sender, MyEventArgs args);
        public event MyEventHandler OnChangedPages;

        public Grid MainGrid { get; set; }

        // progressbar state
        private Visibility progressBarState = Visibility.Collapsed;
        public Visibility ProgressBarState
        {
            get => progressBarState;

            set
            {
                progressBarState = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ProgressBarState)));
            }
        }

        //progressbar object
        public CircularProgressBar ProgressBar { get; set; }

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

        // The file that all users stored in
        Config config = new Config();

        #endregion

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

            // sucscribe events
            OnChangedPages = new MyEventHandler(MainWindowViewModel_OnChangedPages);
        }

        #region method of my custom event that named OnChangedPages

        private void MainWindowViewModel_OnChangedPages(object sender, MyEventArgs e)
        {
            SetPage(e);
        }

        #endregion

        #region additional methods
        private void SetPage(MyEventArgs e)
        {
            var type = e.GetTypeOfNext();

            if (type == typeof(LoginPage))
            {
                var index = MainGrid.Children.IndexOf(ProgressBar);

                for (int i = 0, imax = MainGrid.Children.Count; i < imax; i++)
                {
                    if (i == index)
                        continue;
                    MainGrid.Children.RemoveAt(i);
                }
                MainGrid.Children.Add(new LoginPage(this));
            }

            else if (type == typeof(RegistrationPage))
            {
                var index = MainGrid.Children.IndexOf(ProgressBar);

                for (int i = 0, imax = MainGrid.Children.Count; i < imax; i++)
                {
                    if (i == index)
                        continue;
                    MainGrid.Children.RemoveAt(i);
                }
                MainGrid.Children.Add(new RegistrationPage(this));
            }

            else if (type == typeof(ConfirmCodePage))
            {
                var index = MainGrid.Children.IndexOf(ProgressBar);

                for (int i = 0, imax = MainGrid.Children.Count; i < imax; i++)
                {
                    if (i == index)
                        continue;
                    MainGrid.Children.RemoveAt(i);
                }
                MainGrid.Children.Add(new ConfirmCodePage(this));
            }

            else if (type == typeof(MessagesPage))
            {
                var index = MainGrid.Children.IndexOf(ProgressBar);

                for (int i = 0, imax = MainGrid.Children.Count; i < imax; i++)
                {
                    if (i == index)
                        continue;
                    MainGrid.Children.RemoveAt(i);
                }
                MainGrid.Children.Add(new MessagesPage());
            }

            else if (type == typeof(CircularProgressBar))
            {
                MainGrid.Children.Add(new CircularProgressBar());
            }
        }
        #endregion

        #region initialize of commands

        public MainCommand Start => new MainCommand((body) =>
        {
            var index = MainGrid.Children.IndexOf(ProgressBar);

            for (int i = 0, imax = MainGrid.Children.Count; i < imax; i++)
            {
                if (i == index)
                    continue;
                MainGrid.Children.RemoveAt(i);
            }
            MainGrid.Children.Add(new LoginPage(this));
        });

        #endregion

        #region  methods for to fire OnChangedPages

        public void FireOnClickedSignUp()
        {
            OnChangedPages(this, new MyEventArgs(typeof(ConfirmCodePage)));
        }

        public void FireOnClickedRegistration()
        {
            OnChangedPages(this, new MyEventArgs(typeof(RegistrationPage)));
        }

        public void FireOnProgressing()
        {
            OnChangedPages(this, new MyEventArgs(typeof(CircularProgressBar)));
        }

        public void FireOnClickedConfirm(ConfirmPages confirmPageType)
        {           
            switch (confirmPageType)
            {
                case ConfirmPages.Login:
                    OnChangedPages(this, new MyEventArgs(typeof(MessagesPage)));
                    break;
                case ConfirmPages.Registration:
                    OnChangedPages(this, new MyEventArgs(typeof(LoginPage)));
                    break;
            }
        }

        public void FireOnClickedSignIn()
        {
            OnChangedPages(this, new MyEventArgs(typeof(ConfirmCodePage)));
        }

        #endregion

    }
}
