using System.Windows;

namespace TerraFirmaCraftCalc;

public partial class MainMenu : Window
{
    public MainMenu()
    {
        InitializeComponent();
    }

    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        CalculatorWindow calculatorWindow = new CalculatorWindow();
        Close();
        calculatorWindow.Show();
    }
}