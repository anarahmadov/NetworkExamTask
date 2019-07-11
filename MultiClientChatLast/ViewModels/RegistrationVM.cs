using MultiClientChatLast.CustomEvents;
using MultiClientChatLast.Domain;
using MultiClientChatLast.Extensions;
using MultiClientChatLast.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MultiClientChatLast.ViewModels
{
    public class RegistrationVM : BaseViewModel
    {
        // object of MainViewModel
        MainWindowViewModel mainViewModel;

        public User CurrentUser { get; set; }

        Config config = new Config();

        public MainCommand SignUp => new MainCommand((body) =>
        {
            //mainViewModel.FireOnProgressing();

            Task.Run(() =>
            {
                config.SaveToFile(CurrentUser);

                App.Current.Dispatcher.Invoke(() =>
                {
                    mainViewModel.ProgressBarState = Visibility.Visible;
                });

                Thread.Sleep(2000);

                App.Current.Dispatcher.Invoke(() =>
                {
                    mainViewModel.ProgressBarState = Visibility.Collapsed;
                    mainViewModel.FireOnClickedSignUp();
                });
            });

        });

        public RegistrationVM(MainWindowViewModel viewModel)
        {
            this.mainViewModel = viewModel;
        }

    }
}
