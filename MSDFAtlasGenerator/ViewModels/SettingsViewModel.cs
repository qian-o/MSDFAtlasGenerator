using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MSDFAtlasGenerator.Contracts;
using MSDFAtlasGenerator.Views;
using Wpf.Ui.Appearance;

namespace MSDFAtlasGenerator.ViewModels;

public partial class SettingsViewModel(SettingsPage view) : ViewModel<SettingsPage>(view)
{
    [ObservableProperty]
    private string version = string.Empty;

    [ObservableProperty]
    private bool isLightThemeRadioButtonChecked;

    [ObservableProperty]
    private bool isDarkThemeRadioButtonChecked;

    protected override void ViewLoaded()
    {
        Version = $"MSDF Atlas Generator - {GetAssemblyVersion()}";

        if (ApplicationThemeManager.GetAppTheme() == ApplicationTheme.Light)
        {
            IsLightThemeRadioButtonChecked = true;
            IsDarkThemeRadioButtonChecked = false;
        }
        else
        {
            IsLightThemeRadioButtonChecked = false;
            IsDarkThemeRadioButtonChecked = true;
        }
    }

    [RelayCommand]
    private static void OnLightThemeRadioButtonChecked()
    {
        ApplicationThemeManager.Apply(ApplicationTheme.Light);
    }

    [RelayCommand]
    private static void OnDarkThemeRadioButtonChecked()
    {
        ApplicationThemeManager.Apply(ApplicationTheme.Dark);
    }

    private static string GetAssemblyVersion()
    {
        return Assembly.GetExecutingAssembly().GetName().Version?.ToString(fieldCount: 3) ?? string.Empty;
    }
}
