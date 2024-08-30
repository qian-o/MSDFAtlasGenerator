using MSDFAtlasGenerator.Contracts;
using MSDFAtlasGenerator.ViewModels;

namespace MSDFAtlasGenerator.Views;

public partial class MainPage : View
{
    public MainPage()
    {
        InitializeComponent();

        DataContext = new MainViewModel(this);
    }
}
