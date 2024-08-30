using MSDFAtlasGenerator.Contracts;
using MSDFAtlasGenerator.ViewModels;

namespace MSDFAtlasGenerator.Views;

public partial class SettingsPage : View
{
    public SettingsPage()
    {
        InitializeComponent();

        DataContext = new SettingsViewModel(this);
    }
}
