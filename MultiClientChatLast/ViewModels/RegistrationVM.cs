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

        // user that currently trying to system
        public User CurrentUser { get; set; }

        public MainCommand SignUp => new MainCommand((body) =>
        {
            if (CurrentUser.EmailAddress != null &&
            CurrentUser.FirstName != null &&
            CurrentUser.LastName != null)
            {
                if (!Check.isRegistrated(CurrentUser.EmailAddress))
                {
                    App.TryedUser = CurrentUser;

                    App.SendedConfirmCode = Check.SendConfirmCode(App.TryedUser.EmailAddress);

                    App.ConfirmPagesType = ConfirmPages.Registration;

                    mainViewModel.FireOnClickedSignUp();
                }
                else
                    MessageBox.Show("This email was already registrated");
            }
            else
                MessageBox.Show("Please, fill the compulsory area");
        });

        public MainCommand Back => new MainCommand((body) =>
        {
            mainViewModel.FireOnClickedBack();
        });

        public RegistrationVM(MainWindowViewModel viewModel)
        {
            this.mainViewModel = viewModel;

            CurrentUser = new User();
        }

    }
}
