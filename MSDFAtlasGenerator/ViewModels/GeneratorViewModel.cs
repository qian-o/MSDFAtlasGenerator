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
    [NotifyPropertyChangedRecipients]
    private string? fontFilePath;

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private double fontSize = 64;

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private string? charsetFilePath;

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private bool isAllGlyphs;

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private AtlasType atlasType = AtlasType.MSDF;

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private AtlasImageFormat atlasImageFormat = AtlasImageFormat.Png;

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private bool isOutputJson = true;

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private bool isOutputCsv;

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private bool isOutputArFont;

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
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
