using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using MSDFAtlasGenerator.Contracts;
using MSDFAtlasGenerator.Enums;
using MSDFAtlasGenerator.Views;

namespace MSDFAtlasGenerator.ViewModels;

public partial class GeneratorViewModel(GeneratorPage view) : ViewModel<GeneratorPage>(view)
{
    [ObservableProperty]
    private string? fontFilePath;

    [ObservableProperty]
    private string? charsetFilePath;

    [ObservableProperty]
    private bool isAllGlyphs;

    [ObservableProperty]
    private AtlasType atlasType = AtlasType.MSDF;

    [ObservableProperty]
    private double fontSize = 64;

    [ObservableProperty]
    private string? outputDirectoryPath;

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
        Console.WriteLine(IsAllGlyphs);
    }
}
