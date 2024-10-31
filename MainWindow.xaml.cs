using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TerraFirmaCraftCalc;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private static int currentRow = 0;
    private List<Metal> metalInputs = new List<Metal>();
    private List<TextBox> metalPercentTextBoxes = new List<TextBox>();
    private List<TextBox> metalNameTextBoxes = new List<TextBox>();
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private void AddInputPair_Click(object sender, RoutedEventArgs e)
    {
        RowDefinition newRow = new RowDefinition();
        newRow.Height = new GridLength(1, GridUnitType.Auto);
        MainGrid.RowDefinitions.Add(newRow);
        AddTextBoxPairs();
    }

    private void AddTextBoxPairs()
    {
        if (currentRow > 6)
        {
            MessageBox.Show("Do you really need this many metals?");
            return;
        }
        currentRow++;
        
        // Create new TextBox for Metal input
        TextBox newMetalTextBox = new TextBox();
        newMetalTextBox.Width = 200;
        newMetalTextBox.Text = "Enter Metal Name";
        newMetalTextBox.Margin = new Thickness(0, 0, 200, 10);
        Grid.SetRow(newMetalTextBox, currentRow);
        Grid.SetColumn(newMetalTextBox, 1);

        // Create new TextBox for Metal Percent input
        TextBox newMetalPercentTextBox = new TextBox();
        newMetalPercentTextBox.Width = 200;
        newMetalPercentTextBox.Text = "Enter Metal Percent";
        newMetalPercentTextBox.Margin = new Thickness(0, 0, -200, 10);
        Grid.SetRow(newMetalPercentTextBox, currentRow);
        Grid.SetColumn(newMetalPercentTextBox, 1);

        // Add the new Label and TextBox to the Grid
        MainGrid.Children.Add(newMetalPercentTextBox);
        MainGrid.Children.Add(newMetalTextBox);

        // Add the new Metal Percent TextBox to the list for later retrieval
        //metalInputs.Add(new Metal(int.Parse(newMetalPercentTextBox.Text), newMetalTextBox.Text));
        
        metalNameTextBoxes.Add(newMetalTextBox);
        metalPercentTextBoxes.Add(newMetalPercentTextBox);
    }

    private void RemoveButtonPair_Click(object sender, RoutedEventArgs e)
    {
        {
            if (currentRow == 0)
            {
                MessageBox.Show("Why are you trying to delete the first row? Please dont because you will need it. :)");
                return;
            }
            
            var elementsToRemove = MainGrid.Children
                .OfType<UIElement>()
                .Where(el => Grid.GetRow(el) == currentRow)
                .ToList();

            foreach (var element in elementsToRemove)
            {
                MainGrid.Children.Remove(element);
            }

            metalPercentTextBoxes.RemoveAt(currentRow - 1);
            metalNameTextBoxes.RemoveAt(currentRow - 1);
            currentRow--;
        }
    }
    private void CalculateButton_Click(object sender, RoutedEventArgs e)
    {
        clearMetalInputList();
        for (int i = 0; i < metalNameTextBoxes.Count; i++)
        {
            if (float.TryParse(metalPercentTextBoxes[i].Text, out float percentage))
            {
                string metalName = metalNameTextBoxes[i].Text;
                metalInputs.Add(new (percentage, metalName));   
            }
            else
            {
                MessageBox.Show("Error, invalid inputs. Percents must be int." + "\n" + "Check row " + (i + 1));
            }
        }

        if (int.TryParse(MaxVolumeTextBox.Text, out int maxVolume))
        {
            CalculateMetalRatio ratios = new CalculateMetalRatio(metalInputs, maxVolume);
            MessageBox.Show( "Max Volume: " + MaxVolumeTextBox.Text + "mb" + "\n \n" + AlloyNameTextBox.Text + "\n" + ratios.ToString());
        }
        else
        {
            MessageBox.Show("Error, invalid max volume int.");
        }
    }

    private void clearMetalInputList()
    {
        metalInputs.Clear();
    }
}