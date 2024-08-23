using System.Globalization;
using System.IO;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using MSDFAtlasGenerator.Enums;

namespace MSDFAtlasGenerator.Tools;

public partial class GeneratorConfig : ObservableObject
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

    public bool Validate()
    {
        return !string.IsNullOrWhiteSpace(FontFilePath);
    }

    public string GenerateCommandLine(string folder)
    {
        string outputName = Path.GetFileNameWithoutExtension(FontFilePath)!;

        CultureInfo cultureInfo = CultureInfo.InvariantCulture;

        StringBuilder stringBuilder = new();

        stringBuilder.Append(cultureInfo, $" -font {FontFilePath}");
        stringBuilder.Append(cultureInfo, $" -size {FontSize}");

        if (!string.IsNullOrWhiteSpace(CharsetFilePath))
        {
            stringBuilder.Append(cultureInfo, $" -charset {CharsetFilePath}");
        }

        if (IsAllGlyphs)
        {
            stringBuilder.Append(cultureInfo, $" -allglyphs");
        }

        stringBuilder.Append(cultureInfo, $" -type {AtlasType.ToString().ToLowerInvariant()}");
        stringBuilder.Append(cultureInfo, $" -format {AtlasImageFormat.ToString().ToLowerInvariant()}");
        stringBuilder.Append(cultureInfo, $" -imageout {Path.Combine(folder, $"{outputName}.{AtlasImageFormat.ToString().ToLowerInvariant()}")}");

        if (IsOutputJson)
        {
            stringBuilder.Append(cultureInfo, $" -json {Path.Combine(folder, $"{outputName}.json")}");
        }

        if (IsOutputCsv)
        {
            stringBuilder.Append(cultureInfo, $" -csv {Path.Combine(folder, $"{outputName}.csv")}");
        }

        if (IsOutputArFont)
        {
            stringBuilder.Append(cultureInfo, $" -arfont {Path.Combine(folder, $"{outputName}.arfont")}");
        }

        if (IsOutputShadronPreview)
        {
            stringBuilder.Append(cultureInfo, $" -shadron {Path.Combine(folder, $"{outputName}.shadron")}");
        }

        return stringBuilder.ToString();
    }
}
