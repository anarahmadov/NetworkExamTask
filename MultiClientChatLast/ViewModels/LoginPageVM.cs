using MultiClientChatLast.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MultiClientChatLast.ViewModels
{
    public class LoginPageVM
    {
        // object of MainViewModel
        MainWindowViewModel mainViewModel;

        // entered email address
        public string EmailAddress { get; set; }

        public LoginPageVM(MainWindowViewModel viewModel)
        {
            this.mainViewModel = viewModel;
        }

        public MainCommand Registration => new MainCommand((body) =>
        {
            mainViewModel.FireOnClickedRegistration();
        });

        public MainCommand SignIn => new MainCommand((body) =>
        {
            if (Check.isRegistrated(EmailAddress))
            {
                mainViewModel.FireOnClickedSignIn();

                App.TryedUser = App.RegistratedUsers.SingleOrDefault(x => x.EmailAddress == EmailAddress);

                App.SendedConfirmCode = Check.SendConfirmCode(EmailAddress);               

                App.ConfirmPagesType = ConfirmPages.Login;
            }
        });
    }

}
