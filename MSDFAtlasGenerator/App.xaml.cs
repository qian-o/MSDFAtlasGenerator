using System.Windows;
using Wpf.Ui;

namespace MSDFAtlasGenerator;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static ISnackbarService SnackbarService { get; } = new SnackbarService();
}

