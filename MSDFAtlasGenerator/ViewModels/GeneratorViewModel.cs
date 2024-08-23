using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using MSDFAtlasGenerator.Contracts;
using MSDFAtlasGenerator.Helpers;
using MSDFAtlasGenerator.Tools;
using MSDFAtlasGenerator.Views;

namespace MSDFAtlasGenerator.ViewModels;

public partial class GeneratorViewModel(GeneratorPage view) : ViewModel<GeneratorPage>(view)
{
    private readonly string ToolPath = Path.Combine(AppContext.BaseDirectory, "Assets", "Tools", "msdf-atlas-gen.exe");

    [ObservableProperty]
    private GeneratorConfig config = new();

    [RelayCommand]
    private void Generate()
    {
        if (!Config.Validate())
        {
            return;
        }

        OpenFolderDialog openFolderDialog = new();

        if (openFolderDialog.ShowDialog() == true)
        {
            string cmd = Config.GenerateCommandLine(openFolderDialog.FolderName);

            ProcessHelpers.RunNoWindow(ToolPath, cmd);
        }
    }
}
