using System.Windows;
using VirtualNvhAnalyzer.App.ViewModels;

namespace VirtualNvhAnalyzer.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}