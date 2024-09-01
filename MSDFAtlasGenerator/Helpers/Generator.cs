using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using MSDFAtlasGenerator.Enums;
using MSDFAtlasGenerator.Models;
using Newtonsoft.Json;

namespace MSDFAtlasGenerator.Helpers;

public partial class Generator : ObservableObject
{
    private static readonly string ToolPath = Path.Combine(AppContext.BaseDirectory, "Assets", "Tools", "msdf-atlas-gen.exe");
    private static readonly string CharsetPath = Path.Combine(AppContext.BaseDirectory, "Assets", "Tools", "charset.txt");

    public static FileVersionInfo Version => FileVersionInfo.GetVersionInfo(ToolPath);

    public GeneratorArguments Arguments { get; } = new();

    public async Task<bool> Generate(string folder, OutputData outputData)
    {
        if (!Validate())
        {
            return false;
        }

        await ProcessHelpers.RunCmdAsync(ToolPath, GetArguments(folder), outputData);

        return true;
    }

    public bool GeneratePreview(OutputData outputData, out JsonAtlasMetrics? jsonAtlasMetrics, out byte[]? bgra)
    {
        jsonAtlasMetrics = null;
        bgra = null;

        if (!Validate())
        {
            return false;
        }

        string outputBin = Path.GetTempFileName();
        string outputJson = Path.GetTempFileName();

        ProcessHelpers.RunCmd(ToolPath, GetPreviewArguments(outputBin, outputJson), outputData);

        jsonAtlasMetrics = JsonConvert.DeserializeObject<JsonAtlasMetrics>(File.ReadAllText(outputJson))!;
        bgra = BinToBgra(outputBin);

        File.Delete(outputBin);
        File.Delete(outputJson);

        return true;
    }

    private bool Validate()
    {
        return !string.IsNullOrWhiteSpace(Arguments.FontFilePath);
    }

    private bool UpdateCharset()
    {
        if (string.IsNullOrWhiteSpace(Arguments.CharsetFilePath))
        {
            return false;
        }

        int[] charset = File.ReadAllText(Arguments.CharsetFilePath, Encoding.UTF8)
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
        string outputName = Path.GetFileNameWithoutExtension(Arguments.FontFilePath)!;

        CultureInfo cultureInfo = CultureInfo.InvariantCulture;

        StringBuilder stringBuilder = new();

        stringBuilder.Append(cultureInfo, $@" -font ""{Arguments.FontFilePath}""");
        stringBuilder.Append(cultureInfo, $@" -fontscale {Arguments.FontScale}");
        stringBuilder.Append(cultureInfo, $@" -pxpadding {Arguments.Padding}");

        if (UpdateCharset())
        {
            stringBuilder.Append(cultureInfo, $@" -charset ""{CharsetPath}""");
        }
        else if (Arguments.IsAllGlyphs)
        {
            stringBuilder.Append(cultureInfo, $@" -allglyphs");
        }

        stringBuilder.Append(cultureInfo, $@" -type {Arguments.AtlasType.ToString().ToLowerInvariant()}");
        stringBuilder.Append(cultureInfo, $@" -format {Arguments.AtlasImageFormat.ToString().ToLowerInvariant()}");
        stringBuilder.Append(cultureInfo, $@" -imageout ""{Path.Combine(folder, $"{outputName}.{Arguments.AtlasImageFormat.ToString().ToLowerInvariant()}")}""");

        if (Arguments.IsOutputJson)
        {
            stringBuilder.Append(cultureInfo, $@" -json ""{Path.Combine(folder, $"{outputName}.json")}""");
        }

        if (Arguments.IsOutputCsv)
        {
            stringBuilder.Append(cultureInfo, $@" -csv ""{Path.Combine(folder, $"{outputName}.csv")}""");
        }

        if (Arguments.IsOutputArFont)
        {
            stringBuilder.Append(cultureInfo, $@" -arfont ""{Path.Combine(folder, $"{outputName}.arfont")}""");
        }

        if (Arguments.IsOutputShadronPreview)
        {
            stringBuilder.Append(cultureInfo, $@" -shadron ""{Path.Combine(folder, $"{outputName}.shadron")}""");
        }

        return stringBuilder.ToString();
    }

    private string GetPreviewArguments(string outputBin, string outputJson)
    {
        CultureInfo cultureInfo = CultureInfo.InvariantCulture;

        StringBuilder stringBuilder = new();

        stringBuilder.Append(cultureInfo, $@" -font ""{Arguments.FontFilePath}""");
        stringBuilder.Append(cultureInfo, $@" -fontscale {Arguments.FontScale}");
        stringBuilder.Append(cultureInfo, $@" -pxpadding {Arguments.Padding}");
        stringBuilder.Append(cultureInfo, $@" -type {Arguments.AtlasType.ToString().ToLowerInvariant()}");
        stringBuilder.Append(cultureInfo, $@" -format bin");
        stringBuilder.Append(cultureInfo, $@" -imageout ""{outputBin}""");
        stringBuilder.Append(cultureInfo, $@" -json ""{outputJson}""");

        return stringBuilder.ToString();
    }

    private byte[] BinToBgra(string outputBin)
    {
        byte[] bin = File.ReadAllBytes(outputBin);

        byte[] bgra = new byte[bin.Length / GetChannelCount() * 4];

        switch (Arguments.AtlasType)
        {
            case AtlasType.HardMask:
            case AtlasType.SoftMask:
            case AtlasType.SDF:
            case AtlasType.PSDF:
                {
                    for (int i = 0; i < bgra.Length; i += 4)
                    {
                        int index = i / 4;

                        bgra[i + 0] = bin[index];
                        bgra[i + 1] = bin[index];
                        bgra[i + 2] = bin[index];
                        bgra[i + 3] = 255;
                    }
                }
                break;
            case AtlasType.MSDF:
                {
                    for (int i = 0; i < bgra.Length; i += 4)
                    {
                        int index = i / 4 * 3;

                        bgra[i + 0] = bin[index + 0];
                        bgra[i + 1] = bin[index + 1];
                        bgra[i + 2] = bin[index + 2];
                        bgra[i + 3] = 255;
                    }
                }
                break;
            case AtlasType.MTSDF:
                {
                    for (int i = 0; i < bgra.Length; i += 4)
                    {
                        bgra[i + 0] = bin[i + 2];
                        bgra[i + 1] = bin[i + 1];
                        bgra[i + 2] = bin[i + 0];
                        bgra[i + 3] = bin[i + 3];
                    }
                }
                break;
            default:
                throw new InvalidEnumArgumentException();
        }

        return bgra;
    }

    private int GetChannelCount()
    {
        return Arguments.AtlasType switch
        {
            AtlasType.HardMask => 1,
            AtlasType.SoftMask => 1,
            AtlasType.SDF => 1,
            AtlasType.PSDF => 1,
            AtlasType.MSDF => 3,
            AtlasType.MTSDF => 4,
            _ => 0
        };
    }
}
