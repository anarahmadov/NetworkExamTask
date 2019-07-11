using MultiClientChatLast.ViewModels;
using System.Windows.Controls;


namespace MultiClientChatLast.Views
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : UserControl
    {
        public RegistrationPage(MainWindowViewModel viewModel)
        {
            InitializeComponent();

            DataContext = new RegistrationVM(viewModel);
        }
    }
}
