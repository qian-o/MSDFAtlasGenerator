using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using MSDFAtlasGenerator.Contracts;
using MSDFAtlasGenerator.Views;

namespace MSDFAtlasGenerator.ViewModels;

public partial class GeneratorViewModel(GeneratorPage view) : ViewModel<GeneratorPage>(view)
{
    [ObservableProperty]
    private string? fontFilePath;

    [ObservableProperty]
    private double fontSize = 64;

    [ObservableProperty]
    private string? outputDirectoryPath;

    [RelayCommand]
    private void BrowseFontFile()
    {
        OpenFileDialog openFileDialog = new()
        {
            Filter = "Font Files (*.ttf, *.otf)|*.ttf;*.otf|All Files (*.*)|*.*",
            Multiselect = false
        };

        if (openFileDialog.ShowDialog() == true)
        {
            FontFilePath = openFileDialog.FileName;
        }
    }

    [RelayCommand]
    private void BrowseOutputDirectory()
    {
        SaveFileDialog saveFileDialog = new()
        {
            Filter = "PNG Files (*.png)|*.png",
            DefaultExt = ".png"
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            OutputDirectoryPath = saveFileDialog.FileName;
        }
    }

    [RelayCommand]
    private void Generate()
    {

    }
}
