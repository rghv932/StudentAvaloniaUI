using Avalonia.Controls;
using StudentAvaloniaUI.ViewModels;

namespace StudentAvaloniaUI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
