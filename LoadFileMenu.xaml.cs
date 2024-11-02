using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace TerraFirmaCraftCalc;

public partial class LoadFileMenu : Window
{
    private string saveFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveFiles");
    private string selectedSaveFile = string.Empty;
    private bool isInitializing; 
    public LoadFileMenu()
    {
        InitializeComponent();
        
        if (!Directory.Exists(saveFolderPath))
        {
            MessageBox.Show("SaveFiles folder does not exist.");
            return;
        }
        LoadFiles();
    }
    
    private void searchBox_Changed(object sender, EventArgs e)
    {
        //Ignores the initial text changed on window start
        if (SearchBox.Text.Equals("Search Here"))
        {
            return;
        } 
        DisplaySearchedFile(SearchBox.Text);
    }

    private void DisplaySearchedFile(String targetSearch)
    {
        string[] fileArray = Directory.GetFiles(saveFolderPath);
        
        FileListBox.Items.Clear();
        
        foreach (string file in fileArray)
        {
            if (Path.GetFileName(file).IndexOf(targetSearch, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                FileListBox.Items.Add(Path.GetFileName(file));
            }
        }
    }
    
    private void LoadFiles()
    {
        string[] files = Directory.GetFiles(saveFolderPath);
        
        foreach (string file in files)
        {
            FileListBox.Items.Add(Path.GetFileName(file));
        }
    }
    
    private void LoadFileButton_Click(object sender, RoutedEventArgs e)
    {
        if (FileListBox.SelectedItem == null)
        {
            MessageBox.Show("Please select a file to load.");
            return;
        }
        
        string selectedFile = FileListBox.SelectedItem.ToString();
        
        string fullPath = Path.Combine(saveFolderPath, selectedFile);
        
        try
        {
            string fileContent = File.ReadAllText(fullPath);
            MessageBox.Show($"File loaded: {fullPath}\nContent:\n{fileContent}");
            selectedSaveFile = fullPath; 
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load file: {ex.Message}");
        }
        Close();
    }

    public string GetSelectedSaveFile()
    {
        return selectedSaveFile;
    }
    
    private void OpenDirectoryButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // Open the SaveFiles directory in the default file explorer
            Close();
            System.Diagnostics.Process.Start("explorer.exe", saveFolderPath); 
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to open directory: {ex.Message}");
        }
    }
}