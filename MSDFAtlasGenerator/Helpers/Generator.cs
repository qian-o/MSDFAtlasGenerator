using System.Globalization;
using System.IO;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using MSDFAtlasGenerator.Enums;
using MSDFAtlasGenerator.Helpers;

namespace MSDFAtlasGenerator.Tools;

public partial class Generator : ObservableObject
{
    private readonly string ToolPath = Path.Combine(AppContext.BaseDirectory, "Assets", "Tools", "msdf-atlas-gen.exe");
    private readonly string CharsetPath = Path.Combine(AppContext.BaseDirectory, "Assets", "Tools", "charset.txt");

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

    public async Task<bool> Generate(string folder)
    {
        if (!Validate())
        {
            return false;
        }

        await ProcessHelpers.Run(ToolPath, GetArguments(folder));

        return true;
    }

    public bool GeneratePreview(out byte[] bytes)
    {
        bytes = [];

        if (!Validate())
        {
            return false;
        }

        string outputBin = Path.GetTempFileName();
        string outputJson = Path.GetTempFileName();

        ProcessHelpers.Run(ToolPath, GetPreviewArguments(outputBin, outputJson)).GetAwaiter().GetResult();

        bytes = File.ReadAllBytes(outputBin);

        File.Delete(outputBin);
        File.Delete(outputJson);

        return true;
    }

    private bool Validate()
    {
        return !string.IsNullOrWhiteSpace(FontFilePath);
    }

    private bool UpdateCharset()
    {
        if (string.IsNullOrWhiteSpace(CharsetFilePath))
        {
            return false;
        }

        int[] charset = File.ReadAllText(CharsetFilePath, Encoding.UTF8)
                            .Distinct()
                            .Select(static item => (int)item)
                            .ToArray();

        if (charset.Length == 0)
        {
            return false;
        }

        File.Create(CharsetPath).Dispose();

        File.WriteAllText(CharsetPath, string.Join(" ", charset), Encoding.UTF8);

        return true;
    }

    private string GetArguments(string folder)
    {
        string outputName = Path.GetFileNameWithoutExtension(FontFilePath)!;

        CultureInfo cultureInfo = CultureInfo.InvariantCulture;

        StringBuilder stringBuilder = new();

        stringBuilder.Append(cultureInfo, $" -font {FontFilePath}");
        stringBuilder.Append(cultureInfo, $" -size {FontSize}");

        if (UpdateCharset())
        {
            stringBuilder.Append(cultureInfo, $" -charset {CharsetPath}");
        }
        else if (IsAllGlyphs)
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

    private string GetPreviewArguments(string outputBin, string outputJson)
    {
        CultureInfo cultureInfo = CultureInfo.InvariantCulture;

        StringBuilder stringBuilder = new();

        stringBuilder.Append(cultureInfo, $" -font {FontFilePath}");
        stringBuilder.Append(cultureInfo, $" -size {FontSize}");
        stringBuilder.Append(cultureInfo, $" -chars 65");
        stringBuilder.Append(cultureInfo, $" -type {AtlasType.ToString().ToLowerInvariant()}");
        stringBuilder.Append(cultureInfo, $" -format {AtlasImageFormat.Bin.ToString().ToLowerInvariant()}");
        stringBuilder.Append(cultureInfo, $" -imageout {outputBin}");
        stringBuilder.Append(cultureInfo, $" -json {outputJson}");

        return stringBuilder.ToString();
    }
}
