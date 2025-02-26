using System.Windows;
using System.Windows.Media;

namespace Test.Integration.WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        lblContent.Content = "Correct text";
        lblContent.Foreground = new SolidColorBrush(Colors.Green);
    }
}