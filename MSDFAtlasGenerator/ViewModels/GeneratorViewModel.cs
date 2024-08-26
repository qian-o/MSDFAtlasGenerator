using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using MSDFAtlasGenerator.Contracts;
using MSDFAtlasGenerator.Enums;
using MSDFAtlasGenerator.Tools;
using MSDFAtlasGenerator.Views;

namespace MSDFAtlasGenerator.ViewModels;

public partial class GeneratorViewModel(GeneratorPage view) : ViewModel<GeneratorPage>(view)
{
    [ObservableProperty]
    private string? previewImagePath;

    [ObservableProperty]
    private string? fontFilePath;

    [ObservableProperty]
    private double fontSize = 64;

    [ObservableProperty]
    private string? charsetFilePath;

    [ObservableProperty]
    private bool isAllGlyphs;

    [ObservableProperty]
    private AtlasType atlasType = AtlasType.MSDF;

    [ObservableProperty]
    private AtlasImageFormat atlasImageFormat = AtlasImageFormat.Png;

    [ObservableProperty]
    private bool isOutputJson = true;

    [ObservableProperty]
    private bool isOutputCsv;

    [ObservableProperty]
    private bool isOutputArFont;

    [ObservableProperty]
    private bool isOutputShadronPreview;

    [ObservableProperty]
    private Generator generator = new();

    [RelayCommand]
    private async Task Generate()
    {
        OpenFolderDialog openFolderDialog = new();

        if (openFolderDialog.ShowDialog() == true)
        {
            UpdateGenerator();

            if (await Generator.Generate(openFolderDialog.FolderName))
            {
            }
        }
    }

    private void UpdateGenerator()
    {
        Generator.FontFilePath = FontFilePath;
        Generator.FontSize = FontSize;
        Generator.CharsetFilePath = CharsetFilePath;
        Generator.IsAllGlyphs = IsAllGlyphs;
        Generator.AtlasType = AtlasType;
        Generator.AtlasImageFormat = AtlasImageFormat;
        Generator.IsOutputJson = IsOutputJson;
        Generator.IsOutputCsv = IsOutputCsv;
        Generator.IsOutputArFont = IsOutputArFont;
        Generator.IsOutputShadronPreview = IsOutputShadronPreview;
    }
}
