using MultiClientChatLast.CustomEvents;
using MultiClientChatLast.Domain;
using MultiClientChatLast.Extensions;
using MultiClientChatLast.Views;
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
    public class RegistrationVM : BaseViewModel
    {
        // object of MainViewModel
        MainWindowViewModel mainViewModel;

        public User CurrentUser { get; set; }

        Config config = new Config();

        Random r = new Random();

        public MainCommand SignUp => new MainCommand((body) =>
        {
            App.TryedUser = CurrentUser;

            var randomConfirmCode = r.Next();

            App.SendedConfirmCode = randomConfirmCode;

            Check.SendConfirmCode(App.TryedUser.EmailAddress, randomConfirmCode);

            mainViewModel.FireOnClickedConfirm();           

        });

        public RegistrationVM(MainWindowViewModel viewModel)
        {
            this.mainViewModel = viewModel;

            CurrentUser = new User();
        }

    }
}
