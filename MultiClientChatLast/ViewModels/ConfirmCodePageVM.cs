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
                App.Current.Dispatcher.Invoke(() =>
                {
                    mainViewModel.ProgressBarState = Visibility.Visible;
                });

                Thread.Sleep(2000);

                App.EnteredConfirmCode = int.Parse(ConfirmCode);

                if (App.EnteredConfirmCode == App.SendedConfirmCode)
                {
                    mainViewModel.FireOnClickedSignUp();
                }
                else
                {
                    MessageBox.Show("Invalid confirm code");
                    ConfirmCode = null;
                }

                App.Current.Dispatcher.Invoke(() =>
                {
                    mainViewModel.ProgressBarState = Visibility.Collapsed;
                });

            });

        });

        public ConfirmCodePageVM(MainWindowViewModel viewModel)
        {
            this.mainViewModel = viewModel;
        }
    }
}
