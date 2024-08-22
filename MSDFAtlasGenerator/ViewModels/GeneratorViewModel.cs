﻿using CommunityToolkit.Mvvm.ComponentModel;
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

    [RelayCommand]
    private void Generate()
    {
        OpenFolderDialog openFolderDialog = new();
        openFolderDialog.ShowDialog();

        Console.WriteLine(IsAllGlyphs);
    }
}
