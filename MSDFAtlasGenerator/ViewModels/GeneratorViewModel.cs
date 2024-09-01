using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using MSDFAtlasGenerator.Contracts;
using MSDFAtlasGenerator.Enums;
using MSDFAtlasGenerator.Helpers;
using MSDFAtlasGenerator.Models;
using MSDFAtlasGenerator.Views;

namespace MSDFAtlasGenerator.ViewModels;

public partial class GeneratorViewModel(GeneratorPage view) : ViewModel<GeneratorPage>(view)
{
    [ObservableProperty]
    private Generator generator = new();

    [ObservableProperty]
    private PreviewData? previewData;

    [ObservableProperty]
    private OutputData outputData = new();

    [RelayCommand]
    private void Preview()
    {
        OutputData.AddLog(new Log(LogType.Info, "--- Preview ---"));

        if (Generator.GeneratePreview(OutputData, out JsonAtlasMetrics? jsonAtlasMetrics, out byte[]? rgba))
        {
            PreviewData = new PreviewData(jsonAtlasMetrics!.Atlas.Width,
                                          jsonAtlasMetrics.Atlas.Height,
                                          rgba,
                                          jsonAtlasMetrics!.Atlas.YOrigin == YDirection.Bottom);

            OutputData.AddLog(new Log(LogType.Info, "Preview completed."));
        }

        OutputData.AddLog(new Log(LogType.Info, "--- End ---"));
    }

    [RelayCommand]
    private async Task Generate()
    {
        OpenFolderDialog openFolderDialog = new();

        if (openFolderDialog.ShowDialog() == true)
        {
            OutputData.AddLog(new Log(LogType.Info, "--- Generation ---"));

            if (await Generator.Generate(openFolderDialog.FolderName, OutputData))
            {
                ProcessHelpers.Start("explorer.exe", openFolderDialog.FolderName);

                OutputData.AddLog(new Log(LogType.Info, "Output folder opened."));
                OutputData.AddLog(new Log(LogType.Info, "Generation completed."));
            }

            OutputData.AddLog(new Log(LogType.Info, "--- End ---"));
        }
    }
}
