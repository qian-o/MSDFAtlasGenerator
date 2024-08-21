using MSDFAtlasGenerator.Contracts;
using MSDFAtlasGenerator.ViewModels;

namespace MSDFAtlasGenerator.Views;

/// <summary>
/// MainPage.xaml 的交互逻辑
/// </summary>
public partial class MainPage : View
{
    public MainPage()
    {
        InitializeComponent();

        DataContext = new MainViewModel(this);
    }
}
