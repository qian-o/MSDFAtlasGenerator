using MSDFAtlasGenerator.Contracts;
using MSDFAtlasGenerator.ViewModels;

namespace MSDFAtlasGenerator.Views;

/// <summary>
/// SettingsPage.xaml 的交互逻辑
/// </summary>
public partial class SettingsPage : View
{
    public SettingsPage()
    {
        InitializeComponent();

        DataContext = new SettingsViewModel(this);
    }
}
