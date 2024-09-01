using MSDFAtlasGenerator.Views;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace MSDFAtlasGenerator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : FluentWindow
{
    public MainWindow()
    {
        InitializeComponent();

        SystemThemeWatcher.Watch(this);

        Loaded += (_, _) => RootNavigation.Navigate(typeof(MainPage));

        App.SnackbarService.SetSnackbarPresenter(SnackbarPresenter);
    }
}