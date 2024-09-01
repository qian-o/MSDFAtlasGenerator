using CommunityToolkit.Mvvm.ComponentModel;
using MSDFAtlasGenerator.Enums;

namespace MSDFAtlasGenerator.Models;

public partial class GeneratorArguments : ObservableObject
{
    [ObservableProperty]
    private string? fontFilePath;

    [ObservableProperty]
    private double fontScale = 2.0;

    [ObservableProperty]
    private double padding = 6.0;

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
}
