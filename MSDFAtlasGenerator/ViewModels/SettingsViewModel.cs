using System.Reflection;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using MSDFAtlasGenerator.Contracts;
using MSDFAtlasGenerator.Views;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace MSDFAtlasGenerator.ViewModels;

public partial class SettingsViewModel(SettingsPage view) : ViewModel<SettingsPage>(view)
{
    [ObservableProperty]
    private string version = string.Empty;

    [ObservableProperty]
    private bool isLightThemeRadioButtonChecked;

    [ObservableProperty]
    private bool isDarkThemeRadioButtonChecked;

    [ObservableProperty]
    private WindowBackdropType[] systemBackdropTypes = Enum.GetValues<WindowBackdropType>();

    [ObservableProperty]
    private WindowBackdropType selectedSystemBackdropType = WindowBackdropType.Mica;

    protected override void Initialize()
    {
        Version = $"MSDF Atlas Generator - {GetAssemblyVersion()}";

        UpdateThemeRadioButton();

        ApplicationThemeManager.Changed += ApplicationThemeManager_Changed;
    }

    private void ApplicationThemeManager_Changed(ApplicationTheme currentApplicationTheme, Color systemAccent)
    {
        UpdateThemeRadioButton();
    }

    partial void OnIsLightThemeRadioButtonCheckedChanged(bool value)
    {
        if (value)
        {
            ApplicationThemeManager.Apply(ApplicationTheme.Light, SelectedSystemBackdropType);
        }
    }

    partial void OnIsDarkThemeRadioButtonCheckedChanged(bool value)
    {
        if (value)
        {
            ApplicationThemeManager.Apply(ApplicationTheme.Dark, SelectedSystemBackdropType);
        }
    }

    partial void OnSelectedSystemBackdropTypeChanged(WindowBackdropType value)
    {
        ApplicationThemeManager.Apply(ApplicationThemeManager.GetAppTheme(), value);
    }

    private static string GetAssemblyVersion()
    {
        return Assembly.GetExecutingAssembly().GetName().Version?.ToString(fieldCount: 3) ?? string.Empty;
    }

    private void UpdateThemeRadioButton()
    {
        ApplicationTheme applicationTheme = ApplicationThemeManager.GetAppTheme();

        IsLightThemeRadioButtonChecked = applicationTheme == ApplicationTheme.Light;
        IsDarkThemeRadioButtonChecked = applicationTheme == ApplicationTheme.Dark;
    }
}
