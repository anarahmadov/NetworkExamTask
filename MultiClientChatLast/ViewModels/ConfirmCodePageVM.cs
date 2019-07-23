using MultiClientChatLast.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MultiClientChatLast.ViewModels
{
    public class ConfirmCodePageVM : BaseViewModel
    {
        // object of MainViewModel
        MainWindowViewModel mainViewModel;

        private string confirmCode;
        public string ConfirmCode
        {
            get => confirmCode;

            set
            {
                confirmCode = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ConfirmCode)));
            }
        }

        public MainCommand Confirm => new MainCommand((body) =>
        {
            Task.Run(() =>
            {
                if (ConfirmCode != null)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        mainViewModel.ProgressBarState = Visibility.Visible;
                    });

                    Thread.Sleep(1000);

                    // check that it is which of confirmpage of page
                    switch (App.ConfirmPagesType)
                    {
                        case ConfirmPages.Registration:

                            SignUpConfirmOperations();

                            break;
                        case ConfirmPages.Login:

                            SignInConfirmOperations();

                            break;
                    }

                    App.Current.Dispatcher.Invoke(() =>
                    {
                        mainViewModel.ProgressBarState = Visibility.Hidden;
                    });
                }
                else
                    MessageBox.Show("Enter confirm code");

            });
        });

        private void SignUpConfirmOperations()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                App.EnteredConfirmCode = int.Parse(ConfirmCode);

                if (App.EnteredConfirmCode == App.SendedConfirmCode)
                {
                    mainViewModel.FireOnClickedConfirm(App.ConfirmPagesType);
                    App.RegistratedUsers.Add(App.TryedUser);
                    Config.SaveToFile(App.TryedUser);
                }
                else
                {
                    MessageBox.Show("Invalid confirm code");
                    ConfirmCode = null;
                }
            });

        }

        private void SignInConfirmOperations()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                App.EnteredConfirmCode = int.Parse(ConfirmCode);

                if (App.EnteredConfirmCode == App.SendedConfirmCode)
                {
                    mainViewModel.FireOnClickedConfirm(App.ConfirmPagesType);
                }
                else
                {
                    MessageBox.Show("Invalid confirm code");
                    ConfirmCode = null;
                }
            });
        }

        public ConfirmCodePageVM(MainWindowViewModel viewModel)
        {
            this.mainViewModel = viewModel;
        }
    }
}
