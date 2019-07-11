using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiClientChatLast.ViewModels
{
    public class LoginPageVM
    {
        // object of MainViewModel
        MainWindowViewModel mainViewModel;

        public LoginPageVM(MainWindowViewModel viewModel)
        {
            this.mainViewModel = viewModel;
        }

        public MainCommand Registration => new MainCommand((body) =>
        {
            mainViewModel.FireOnClickedRegistration();
        });

        public MainCommand SigIn => new MainCommand((body) =>
        {
            mainViewModel.ProgressBarState = System.Windows.Visibility.Visible;
        });
    }

}
