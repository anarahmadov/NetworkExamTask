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
    public class LoginPageVM : BaseViewModel
    {
        // object of MainViewModel
        MainWindowViewModel mainViewModel;

        // entered email address
        private string emailAddress;
        public string EmailAddress
        {
            get => emailAddress;
            set
            {
                emailAddress = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(EmailAddress)));
            }
        }

        public LoginPageVM(MainWindowViewModel viewModel)
        {
            this.mainViewModel = viewModel;
        }

        public MainCommand Registration => new MainCommand((body) =>
        {
            mainViewModel.FireOnClickedRegistration();
        });

        public MainCommand Next => new MainCommand((body) =>
        {
            mainViewModel.FireOnClickedNext();

            //if (Check.isRegistrated(EmailAddress))
            //{
            //    mainViewModel.FireOnClickedNext();
              
            //    App.UserOnSystem = App.RegistratedUsers.SingleOrDefault(x => x.EmailAddress == EmailAddress);

            //    App.UserOnSystem.IPAddress = App.LocalIPAddress;

            //    //save user to file
            //    Config.SaveToFile(App.UserOnSystem);

            //    App.SendedConfirmCode = Check.SendConfirmCode(EmailAddress);

            //    App.ConfirmPagesType = ConfirmPages.Login;
            //}
            //else if (EmailAddress == null)
            //{
            //    MessageBox.Show("Please, fill email address space");
            //    EmailAddress = null;
            //}
            //else
            //{
            //    MessageBox.Show("This email address was not registrated");
            //    EmailAddress = null;
            //}
        });
    }

}
