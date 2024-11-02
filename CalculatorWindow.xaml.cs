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
public partial class CalculatorWindow : Window
{
    private static int _currentRow = 0;
    private readonly List<TextBox> _metalMaxPercentTextBoxes = new List<TextBox>();
    private readonly List<TextBox> _metalMinPercentTextBoxes = new List<TextBox>();
    private readonly List<TextBox> _metalNameTextBoxes = new List<TextBox>();
    private bool _passCheck = false;
    
    public CalculatorWindow()
    {
        InitializeComponent();
    }

    private void AddInputPair_Click(object sender, RoutedEventArgs e)
    {
        AddTextBoxPairs();
    }

    private void AddTextBoxPairs()
    {
        RowDefinition newRow = new RowDefinition();
        newRow.Height = new GridLength(1, GridUnitType.Auto);
        MainGrid.RowDefinitions.Add(newRow);
        
        if (_currentRow > 6)
        {
            MessageBox.Show("Do you really need this many metals?");
            return;
        }
        _currentRow++;
        
        // Create new TextBox for Metal input
        TextBox newMetalTextBox = new TextBox
        {
            Width = 200,
            Text = "Enter Metal Name",
            Margin = new Thickness(0, 0, 400, 10)
        };
        Grid.SetRow(newMetalTextBox, _currentRow);
        Grid.SetColumn(newMetalTextBox, 1);

        // Create new TextBox for Metal Percent input
        TextBox newMinimumMetalPercentTextBox = new TextBox
        {
            Width = 200,
            Text = "Enter Minimum Metal Percent",
            Margin = new Thickness(0, 0, 0, 10)
        };
        Grid.SetRow(newMinimumMetalPercentTextBox, _currentRow);
        Grid.SetColumn(newMinimumMetalPercentTextBox, 1);
        
        TextBox newMaximumMetalPercentTextBox = new TextBox
        {
            Width = 200,
            Text = "Enter Maximum Metal Percent",
            Margin = new Thickness(400, 0, 0, 10)
        };
        Grid.SetRow(newMaximumMetalPercentTextBox, _currentRow);
        Grid.SetColumn(newMaximumMetalPercentTextBox, 1);
        

        // Add the new Label and TextBox to the Grid
        MainGrid.Children.Add(newMinimumMetalPercentTextBox);
        MainGrid.Children.Add(newMaximumMetalPercentTextBox);
        MainGrid.Children.Add(newMetalTextBox);
        
        _metalNameTextBoxes.Add(newMetalTextBox);
        _metalMaxPercentTextBoxes.Add(newMaximumMetalPercentTextBox);
        _metalMinPercentTextBoxes.Add(newMinimumMetalPercentTextBox);
    }

    private void RemoveButtonPair_Click(object sender, RoutedEventArgs e)
    {
        RemoveButtonPairs();
    }

    private void RemoveButtonPairs()
    {
        if (_currentRow == 0)
        {
            return;
        }
            
        var elementsToRemove = MainGrid.Children
            .OfType<UIElement>()
            .Where(el => Grid.GetRow(el) == _currentRow)
            .ToList();

        foreach (var element in elementsToRemove)
        {
            MainGrid.Children.Remove(element);
        }

        _metalMaxPercentTextBoxes.RemoveAt(_currentRow - 1);
        _metalMinPercentTextBoxes.RemoveAt(_currentRow - 1);
        _metalNameTextBoxes.RemoveAt(_currentRow - 1);
        _currentRow--;
    }
    private void CalculateButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(MaxVolumeTextBox.Text, out int maxVolume))
        {
            CalculateMetalRatio ratios = new CalculateMetalRatio(CreateMetalList(), maxVolume);
            MaxVolumeTextBox.BorderBrush = new SolidColorBrush(Colors.Gray);
            if (_passCheck)
            {
                MessageBox.Show( "Max Volume: " + MaxVolumeTextBox.Text + "mb" + "\n \n" + AlloyNameTextBox.Text + " \n \n" + ratios.ToString());   
            }
        }
        else
        {
            MaxVolumeTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            MessageBox.Show("Error, invalid max volume int.");
        }
    }

    private void SavePresetButton_Click(object sender, RoutedEventArgs e)
    {
        DataManager manager = new DataManager();
        List<Metal> metals = new List<Metal>();
        metals = CreateMetalList();
        
        if (_passCheck && int.TryParse(MaxVolumeTextBox.Text, out int maxVolume))
        {
            manager.SerializeData(metals, AlloyNameTextBox.Text, maxVolume);
            MessageBox.Show("Data Saved!");
        }
        else
        {
            MessageBox.Show("Error, Could not save data.");
        }
    }
    
    private void LoadPresetButton_Click(object sender, RoutedEventArgs e)
    {
        LoadFileMenu menu = new LoadFileMenu();
        menu.ShowDialog();
        
        string selectedFile = menu.GetSelectedSaveFile();
        if (string.IsNullOrEmpty(selectedFile))
        {
            return;
        }
        menu.Close();

        // Deserialize the file content using the DataManager
        DataManager manager = new DataManager();
        PresetData loadedData = manager.DeserializeData(selectedFile);
        
        ClearUI();
        AlloyNameTextBox.Text = loadedData.AlloyName;
        MaxVolumeTextBox.Text = loadedData.MaximumVolume.ToString();
        for (int i = 0; i < loadedData.Metals.Count; i++)
        {
            AddTextBoxPairs();
            _metalNameTextBoxes[i].Text = loadedData.Metals[i].Name;
            _metalMinPercentTextBoxes[i].Text = loadedData.Metals[i].MinPercentage.ToString();
            _metalMaxPercentTextBoxes[i].Text = loadedData.Metals[i].MaxPercentage.ToString();
        }

        MessageBox.Show("Preset loaded successfully!");
    }

    private void ClearUI()
    {
        for (int i = _currentRow; i > 0; i--)
        {
            RemoveButtonPairs();
        }
        _currentRow = 0;
    }

    private List<Metal> CreateMetalList()
    {
        List<Metal> metalList = new List<Metal>();
        bool hasInvalidInput = false;
        StringBuilder errorMessages = new StringBuilder();

        for (int i = 0; i < _metalNameTextBoxes.Count; i++)
        {
            if (float.TryParse(_metalMaxPercentTextBoxes[i].Text, out float maxPercent) && 
                float.TryParse(_metalMinPercentTextBoxes[i].Text, out float minPercent))
            {
                string metalName = _metalNameTextBoxes[i].Text;
                float middlePercent = (maxPercent + minPercent) / 2;
                metalList.Add(new Metal(middlePercent, metalName, minPercent, maxPercent));

                // Reset border color if valid
                _metalMaxPercentTextBoxes[i].BorderBrush = new SolidColorBrush(Colors.Gray);
                _metalMinPercentTextBoxes[i].BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            else
            {
                hasInvalidInput = true;
                _metalMaxPercentTextBoxes[i].BorderBrush = new SolidColorBrush(Colors.Red);
                _metalMinPercentTextBoxes[i].BorderBrush = new SolidColorBrush(Colors.Red);
                errorMessages.AppendLine($"Error on row {i + 1}: Invalid percentages. Enter valid integers.");
            }
        }

        if (hasInvalidInput)
        {
            MessageBox.Show(errorMessages.ToString());
        }
        _passCheck = !hasInvalidInput;
        return metalList;
    }
}