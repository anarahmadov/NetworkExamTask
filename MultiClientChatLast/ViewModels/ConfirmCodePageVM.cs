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

        // class for operations writing & reading to contacts file
        Config config = new Config();

        public MainCommand Confirm => new MainCommand((body) =>
        {           
            Task.Run(() =>
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
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            App.EnteredConfirmCode = int.Parse(ConfirmCode);

                            if (App.EnteredConfirmCode == App.SendedConfirmCode)
                            {
                                mainViewModel.FireOnClickedConfirm(App.ConfirmPagesType);
                                App.RegistratedUsers.Add(App.TryedUser);
                                config.SaveToFile(App.TryedUser);
                            }
                            else
                            {
                                MessageBox.Show("Invalid confirm code");
                                ConfirmCode = null;
                            }
                        });
                        break;
                    case ConfirmPages.Login:
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            App.EnteredConfirmCode = int.Parse(ConfirmCode);

                            if(App.EnteredConfirmCode == App.SendedConfirmCode)
                            {
                                mainViewModel.FireOnClickedConfirm(App.ConfirmPagesType);
                            }
                        });
                        break;
                }

                App.Current.Dispatcher.Invoke(() =>
                {
                    mainViewModel.ProgressBarState = Visibility.Hidden;
                });

            });

        });

        public ConfirmCodePageVM(MainWindowViewModel viewModel)
        {
            this.mainViewModel = viewModel;
        }
    }
}
