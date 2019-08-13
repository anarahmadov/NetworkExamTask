using System.ComponentModel;
using System.Windows.Controls;
using MultiClientChatLast.Views;
using System.Windows;
using MultiClientChatLast.CustomEvents;
using MultiClientChatLast.Extensions;

namespace MultiClientChatLast.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region properties and fields

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

        // The file that all users stored in
        Config config = new Config();

        #endregion

        public MainWindowViewModel()
        {            
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

            else if (type == typeof(ChatPage))
            {
                var index = MainGrid.Children.IndexOf(ProgressBar);

                for (int i = 0, imax = MainGrid.Children.Count; i < imax; i++)
                {
                    if (i == index)
                        continue;
                    MainGrid.Children.RemoveAt(i);
                }
                MainGrid.Children.Add(new ChatPage(this));
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

            else if (type == typeof(CircularProgressBar))
            {
                MainGrid.Children.Add(new CircularProgressBar());
            }
        }
        #endregion

        #region initialize of commands

        public MainCommand Start => new MainCommand((body) =>
        {
            FireOnClickedStart();
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

        public void FireOnClickedBack()
        {
            OnChangedPages(this, new MyEventArgs(typeof(LoginPage)));
        }

        public void FireOnClickedLogLout()
        {
            OnChangedPages(this, new MyEventArgs(typeof(LoginPage)));
        }

        public void FireOnClickedConfirm(ConfirmPages confirmPageType)
        {
            OnChangedPages(this, new MyEventArgs(typeof(RegistrationPage)));

            //switch (confirmPageType)
            //{
            //    case ConfirmPages.Login:
            //        OnChangedPages(this, new MyEventArgs(typeof(ChatPage)));
            //        break;
            //    case ConfirmPages.Registration:
            //        OnChangedPages(this, new MyEventArgs(typeof(LoginPage)));
            //        break;
            //}
        }

        public void FireOnClickedNext()
        {
            OnChangedPages(this, new MyEventArgs(typeof(ConfirmCodePage)));
        }

        public void FireOnClickedStart()
        {
            OnChangedPages(this, new MyEventArgs(typeof(LoginPage)));
        }

        #endregion

    }
}
