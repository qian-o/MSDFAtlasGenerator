using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using MSDFAtlasGenerator.Enums;
using MSDFAtlasGenerator.Helpers;
using MSDFAtlasGenerator.Models;
using Newtonsoft.Json;

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

        await ProcessHelpers.RunAsync(ToolPath, GetArguments(folder));

        return true;
    }

    public bool GeneratePreview(out JsonAtlasMetrics? jsonAtlasMetrics, out byte[]? rgba)
    {
        jsonAtlasMetrics = null;
        rgba = null;

        if (!Validate())
        {
            return false;
        }

        string outputBin = Path.GetTempFileName();
        string outputJson = Path.GetTempFileName();

        ProcessHelpers.Run(ToolPath, GetPreviewArguments(outputBin, outputJson));

        jsonAtlasMetrics = JsonConvert.DeserializeObject<JsonAtlasMetrics>(File.ReadAllText(outputJson))!;
        rgba = BinToRgba(outputBin);

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

        stringBuilder.Append(cultureInfo, $@" -font ""{FontFilePath}""");
        stringBuilder.Append(cultureInfo, $@" -size {FontSize}");

        if (UpdateCharset())
        {
            stringBuilder.Append(cultureInfo, $@" -charset ""{CharsetPath}""");
        }
        else if (IsAllGlyphs)
        {
            stringBuilder.Append(cultureInfo, $@" -allglyphs");
        }

        stringBuilder.Append(cultureInfo, $@" -type {AtlasType.ToString().ToLowerInvariant()}");
        stringBuilder.Append(cultureInfo, $@" -format {AtlasImageFormat.ToString().ToLowerInvariant()}");
        stringBuilder.Append(cultureInfo, $@" -imageout ""{Path.Combine(folder, $"{outputName}.{AtlasImageFormat.ToString().ToLowerInvariant()}")}""");

        if (IsOutputJson)
        {
            stringBuilder.Append(cultureInfo, $@" -json ""{Path.Combine(folder, $"{outputName}.json")}""");
        }

        if (IsOutputCsv)
        {
            stringBuilder.Append(cultureInfo, $@" -csv ""{Path.Combine(folder, $"{outputName}.csv")}""");
        }

        if (IsOutputArFont)
        {
            stringBuilder.Append(cultureInfo, $@" -arfont ""{Path.Combine(folder, $"{outputName}.arfont")}""");
        }

        if (IsOutputShadronPreview)
        {
            stringBuilder.Append(cultureInfo, $@" -shadron ""{Path.Combine(folder, $"{outputName}.shadron")}""");
        }

        return stringBuilder.ToString();
    }

    private string GetPreviewArguments(string outputBin, string outputJson)
    {
        CultureInfo cultureInfo = CultureInfo.InvariantCulture;

        StringBuilder stringBuilder = new();

        stringBuilder.Append(cultureInfo, $@" -font ""{FontFilePath}""");
        stringBuilder.Append(cultureInfo, $@" -size {FontSize}");
        stringBuilder.Append(cultureInfo, $@" -chars 65");
        stringBuilder.Append(cultureInfo, $@" -type {AtlasType.ToString().ToLowerInvariant()}");
        stringBuilder.Append(cultureInfo, $@" -format bin");
        stringBuilder.Append(cultureInfo, $@" -imageout ""{outputBin}""");
        stringBuilder.Append(cultureInfo, $@" -json ""{outputJson}""");

        return stringBuilder.ToString();
    }

    private byte[] BinToRgba(string outputBin)
    {
        byte[] bin = File.ReadAllBytes(outputBin);

        byte[] rgba = new byte[bin.Length / GetChannelCount() * 4];

        switch (AtlasType)
        {
            case AtlasType.HardMask:
            case AtlasType.SoftMask:
            case AtlasType.SDF:
            case AtlasType.PSDF:
                {
                    for (int i = 0; i < rgba.Length; i += 4)
                    {
                        int index = i / 4;

                        rgba[i + 0] = bin[index];
                        rgba[i + 1] = bin[index];
                        rgba[i + 2] = bin[index];
                        rgba[i + 3] = 255;
                    }
                }
                break;
            case AtlasType.MSDF:
                {
                    for (int i = 0; i < rgba.Length; i += 4)
                    {
                        int index = i / 4 * 3;

                        rgba[i + 0] = bin[index + 0];
                        rgba[i + 1] = bin[index + 1];
                        rgba[i + 2] = bin[index + 2];
                        rgba[i + 3] = 255;
                    }
                }
                break;
            case AtlasType.MTSDF:
                {
                    for (int i = 0; i < rgba.Length; i += 4)
                    {
                        rgba[i + 0] = bin[i + 0];
                        rgba[i + 1] = bin[i + 1];
                        rgba[i + 2] = bin[i + 2];
                        rgba[i + 3] = bin[i + 3];
                    }
                }
                break;
            default:
                throw new InvalidEnumArgumentException();
        }

        return rgba;
    }

    private int GetChannelCount()
    {
        return AtlasType switch
        {
            AtlasType.HardMask => 1,
            AtlasType.SoftMask => 1,
            AtlasType.SDF => 1,
            AtlasType.PSDF => 1,
            AtlasType.MSDF => 3,
            AtlasType.MTSDF => 4,
            _ => 0,
        };
    }
}
