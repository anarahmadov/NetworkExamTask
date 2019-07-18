using MultiClientChatLast.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MultiClientChatLast.Views
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatPage : UserControl
    {
        public ChatPage(MainWindowViewModel viewModel)
        {
            InitializeComponent();

            RightSide = rightSide;
            ChatPageVM vm = new ChatPageVM(viewModel);
            // vm.RightSideGrid = rightSide;

            DataContext = vm;
        }

        public static Grid RightSide { get; set; }
    }
}
