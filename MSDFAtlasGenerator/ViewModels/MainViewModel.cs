using System.Windows.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MSDFAtlasGenerator.Contracts;
using MSDFAtlasGenerator.Helpers;
using MSDFAtlasGenerator.Views;

namespace MSDFAtlasGenerator.ViewModels;

public partial class MainViewModel(MainPage view) : ViewModel<MainPage>(view)
{
    [ObservableProperty]
    private string? generatorVersion;

    protected override void Initialize()
    {
        GeneratorVersion = Generator.Version.ProductVersion;
    }

    [RelayCommand]
    private static void RequestNavigate(RequestNavigateEventArgs e)
    {
        ProcessHelpers.OpenUri(e.Uri);

        e.Handled = true;
    }
}
